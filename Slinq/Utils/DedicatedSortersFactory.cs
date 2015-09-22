using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using Slinq.Abstract;
using Slinq.Extensions;

namespace Slinq.Utils
{
    public static class DedicatedSortersFactory
    {
        /// <summary>
        /// PoC: generate a type that does not call interface method, does not box value  types and does not copy memory if don't have to
        /// </summary>
        public static IArraySorter<T> CreateDedicatedSorter<T>()
        {
            var assemblyName = new AssemblyName("Slinq.Dynamic");
            var assemblyFileName = string.Format("{0}.dll", assemblyName.Name);
            var assemblyBuilder = Thread.GetDomain().DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule("Sorters", assemblyFileName);
            var sourceType = typeof(SourceArraySorter<T>);

            var typeBuilder = CreateTypeBuilder<T>(moduleBuilder, sourceType);

            typeBuilder.AddInterfaceImplementation(typeof(IArraySorter<T>));

            DefineDefaultParameterlessConstructor(typeBuilder);

            var methodTranslations = new Dictionary<MethodBase, MethodToken>();

            DefineCompareMethod<T>(sourceType, typeBuilder, methodTranslations, "IsGreaterThan", ComparerGenerator.GenerateIsGreaterThan<T>);
            DefineCompareMethod<T>(sourceType, typeBuilder, methodTranslations, "IsLessThan", ComparerGenerator.GenerateIsLessThan<T>);

            var methods = new[]
            {
                "FloorLog2", 
                "Swap", 
                "SwapIfGreaterWithItems", 
                "DownHeap", 
                "Heapsort", 
                "InsertionSort", 
                "PickPivotAndPartition", 
                "IntroSort", 
                "IntrospectiveSort", 
                "Sort"
            };

            Rewrite<T>(sourceType, typeBuilder, moduleBuilder, methodTranslations, methods);

            var dedicatedSorterType = typeBuilder.CreateType();

            assemblyBuilder.Save(assemblyFileName); // todo: remove after PoC is done

            return (IArraySorter<T>)Activator.CreateInstance(dedicatedSorterType, new object[0]);
        }

        private static TypeBuilder CreateTypeBuilder<T>(ModuleBuilder moduleBuilder, Type sourceType)
        {
            return moduleBuilder.DefineType(
                GetSorterName<T>(), 
                sourceType.Attributes, 
                typeof(object), 
                new[]
                {
                    typeof(IArraySorter<T>)
                });
        }

        /// <summary>
        /// c# compiler does it automatically, here we have to create it manually
        /// </summary>
        private static void DefineDefaultParameterlessConstructor(TypeBuilder typeBuilder)
        {
            var constructorBuilder = typeBuilder.DefineConstructor(
                MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, 
                CallingConventions.Standard, 
                Type.EmptyTypes);

            var objectClassConstructor = typeof(object).GetConstructor(Type.EmptyTypes);

            var cilGenerator = constructorBuilder.GetILGenerator();
            cilGenerator.Emit(OpCodes.Ldarg_0);
            cilGenerator.Emit(OpCodes.Call, objectClassConstructor);
            cilGenerator.Emit(OpCodes.Ret);
        }

        private static void DefineCompareMethod<T>(
            Type sourceType, 
            TypeBuilder typeBuilder, 
            Dictionary<MethodBase, MethodToken> methodTranslations, 
            string methodName,
            Action<MethodBuilder> generate)
        {
            var sourceMethod = GetMethod(sourceType, methodName);
            var methodBuilder = CreateMethodBuilder<T>(typeBuilder, sourceMethod);

            SetAggressiveInlining(methodBuilder);

            generate(methodBuilder);

            methodTranslations.Add(sourceMethod, methodBuilder.GetToken());
        }

        private static void Rewrite<T>(Type sourceType, TypeBuilder typeBuilder, ModuleBuilder moduleBuilder, Dictionary<MethodBase, MethodToken> methodsTranslations, string[] methods)
        {
            int genericTypeToken = moduleBuilder.GetTypeToken(typeof(T)).Token;
            foreach (var methodName in methods)
            {
                var sourceMethod = GetMethod(sourceType, methodName);
                var methodBuilder = CreateMethodBuilder<T>(typeBuilder, sourceMethod);

                DeclareLocalVariables(methodBuilder, sourceMethod.GetMethodBody());

                SetAggressiveInlining(methodBuilder);

                methodsTranslations.Add(sourceMethod, methodBuilder.GetToken()); // add the translation before rewrite for recursive methods
                
                byte[] rewrittenMethodBytes = sourceMethod.Rewrite(methodsTranslations, genericTypeToken);

                methodBuilder.CreateMethodBody(rewrittenMethodBytes, rewrittenMethodBytes.Length);

                if (IsArraySorterInterfaceMethod(sourceMethod))
                {
                    ImplementInterface<T>(typeBuilder, methodBuilder);
                }
            }
        }

        private static MethodInfo GetMethod(Type sourceType, string name)
        {
            return sourceType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                             .Single(method => method.Name == name);
        }

        private static MethodBuilder CreateMethodBuilder<T>(TypeBuilder typeBuilder, MethodInfo sourceMethod)
        {
            var parameters = sourceMethod.GetParameters().Select(parameter => parameter.ParameterType).ToArray();
            return typeBuilder.DefineMethod(
                sourceMethod.Name, 
                sourceMethod.Attributes, 
                sourceMethod.CallingConvention, 
                sourceMethod.ReturnType, 
                parameters);
        }

        private static void DeclareLocalVariables(MethodBuilder methodBuilder, MethodBody sourceMethodBody)
        {
            var cilGenerator = methodBuilder.GetILGenerator();
            foreach (var localVariableInfo in sourceMethodBody.LocalVariables)
            {
                cilGenerator.DeclareLocal(localVariableInfo.LocalType, localVariableInfo.IsPinned);
            }

            cilGenerator.GetType().GetField("m_maxStackSize", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(cilGenerator, sourceMethodBody.MaxStackSize); // todo: find better way
        }

        private static void SetAggressiveInlining(MethodBuilder methodBuilder)
        {
            methodBuilder.SetImplementationFlags(
                methodBuilder.GetMethodImplementationFlags() | MethodImplAttributes.AggressiveInlining);
        }

        private static void ImplementInterface<T>(TypeBuilder typeBuilder, MethodBuilder methodBuilder)
        {
            var interfaceMethod = typeof(IArraySorter<T>).GetMethods().Single();
            typeBuilder.DefineMethodOverride(methodBuilder, interfaceMethod);
        }

        private static string GetSorterName<T>()
        {
            return string.Format("{0}ArraySorter", typeof(T).Name);
        }

        private static bool IsArraySorterInterfaceMethod(MethodInfo sourceMethod)
        {
            return sourceMethod.Name == "Sort";
        }
    }
}
