using System;
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

            CopyAllMethods<T>(sourceType, typeBuilder);

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

        private static void CopyAllMethods<T>(Type sourceType, TypeBuilder typeBuilder)
        {
            foreach (var sourceMethod in sourceType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Where(method => method.DeclaringType != typeof(object)))
            {
                var parameters = sourceMethod.GetParameters().Select(parameter => parameter.ParameterType).ToArray();
                var methodBuilder = typeBuilder.DefineMethod(
                    sourceMethod.Name,
                    sourceMethod.Attributes,
                    sourceMethod.CallingConvention,
                    sourceMethod.ReturnType,
                    parameters);

                var sourceMethodBody = sourceMethod.GetMethodBody();
                var sourceMethodCode = sourceMethodBody.GetILAsByteArray();

                methodBuilder.CreateMethodBody(sourceMethodCode, sourceMethodCode.Length);

                if (IsArraySorterInterfaceMethod(sourceMethod))
                {
                    ImplementInterface<T>(typeBuilder, methodBuilder);
                }
            }
        }

        private static string GetSorterName<T>()
        {
            return string.Format("{0}ArraySorter", typeof(T).Name);
        }

        private static bool IsArraySorterInterfaceMethod(MethodInfo sourceMethod)
        {
            return sourceMethod.Name == "Sort";
        }

        private static void ImplementInterface<T>(TypeBuilder typeBuilder, MethodBuilder methodBuilder)
        {
            var interfaceMethod = typeof(IArraySorter<T>).GetMethods().First();
            typeBuilder.DefineMethodOverride(methodBuilder, interfaceMethod);
        }
    }
}