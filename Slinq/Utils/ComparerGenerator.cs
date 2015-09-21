using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Slinq.Utils
{
    internal static class ComparerGenerator
    {
        private static readonly Dictionary<Type, OpCode> Ldinds = new Dictionary<Type, OpCode>()
        {
            { typeof(byte), OpCodes.Ldind_U1 },
            { typeof(short), OpCodes.Ldind_I2 },
            { typeof(int), OpCodes.Ldind_I4 },
            { typeof(long), OpCodes.Ldind_I8 },
            { typeof(float), OpCodes.Ldind_R4 },
            { typeof(double), OpCodes.Ldind_R8 },
            { typeof(ushort), OpCodes.Ldind_U2 },
            { typeof(uint), OpCodes.Ldind_U4 },
        };

        internal static void GenerateIsLessThan<T>(MethodBuilder methodBuilder)
        {
            GenerateComparer<T>(methodBuilder, "op_LessThan", OpCodes.Clt);
        }

        internal static void GenerateIsGreaterThan<T>(MethodBuilder methodBuilder)
        {
            GenerateComparer<T>(methodBuilder, "op_GreaterThan", OpCodes.Cgt);
        }

        private static void GenerateComparer<T>(MethodBuilder methodBuilder, string comparisonOperatorName, OpCode comparison)
        {
            if (TryGenerateComparisonForPrimitive<T>(methodBuilder, comparison))
            {
                return;
            }

            // for 32 bits it is somehow faster to use comparison operator than CompareTo
            if (IntPtr.Size == 4 
                && TryGenerateComparisonForTypeWithComparisonOperators<T>(methodBuilder, comparisonOperatorName))
            {
                return;
            }

            if (TryGenerateComparisonForIComparable<T>(methodBuilder, comparison))
            {
                return;
            }

            if (IntPtr.Size != 4 
                && TryGenerateComparisonForTypeWithComparisonOperators<T>(methodBuilder, comparisonOperatorName))
            {
                return;
            }

            throw new NotImplementedException("not implemented yet");
        }

        private static bool TryGenerateComparisonForPrimitive<T>(MethodBuilder methodBuilder, OpCode comparison)
        {
            OpCode specificLdind;
            if (!typeof(T).IsPrimitive || !Ldinds.TryGetValue(typeof(T), out specificLdind))
            {
                return false;
            }

            var generator = methodBuilder.GetILGenerator();
            generator.Emit(OpCodes.Ldarg_1);
            generator.Emit(specificLdind);
            generator.Emit(OpCodes.Ldarg_2);
            generator.Emit(specificLdind);
            generator.Emit(comparison);
            generator.Emit(OpCodes.Ret);

            return true;
        }

        private static bool TryGenerateComparisonForIComparable<T>(MethodBuilder methodBuilder, OpCode comparison)
        {
            var compareTo = typeof(T).GetMethod("CompareTo", new[] { typeof(T) });
            if (compareTo == null)
            {
                return false;
            }

            var generator = methodBuilder.GetILGenerator();
            generator.Emit(OpCodes.Ldarg_1);
            generator.Emit(OpCodes.Ldarg_2);
            generator.Emit(OpCodes.Ldobj, typeof(T));
            generator.Emit(OpCodes.Call, compareTo);
            generator.Emit(OpCodes.Ldc_I4_0);
            generator.Emit(comparison);
            generator.Emit(OpCodes.Ret);

            return true;
        }

        private static bool TryGenerateComparisonForTypeWithComparisonOperators<T>(
            MethodBuilder methodBuilder,
            string comparisonOperatorName)
        {
            var comparisonOperator = typeof(T).GetMethod(comparisonOperatorName);
            if (comparisonOperator == null)
            {
                return false;
            }

            var generator = methodBuilder.GetILGenerator();
            generator.Emit(OpCodes.Ldarg_1);
            generator.Emit(OpCodes.Ldobj, typeof(T));
            generator.Emit(OpCodes.Ldarg_2);
            generator.Emit(OpCodes.Ldobj, typeof(T));
            generator.Emit(OpCodes.Call, comparisonOperator);
            generator.Emit(OpCodes.Ret);

            return true;
        }
    }
}