using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace Slinq.Utils
{
    internal static class CilRewriter
    {
        private const int MetadataTokenSize = 4;

        private static readonly int CallOpCodeSize = OpCodes.Call.Size;

        private static readonly short CallOpCodeValue = OpCodes.Call.Value;

        private static readonly short[] TypeSpecificOpCodes = { OpCodes.Ldelem.Value, OpCodes.Ldelema.Value, OpCodes.Stelem.Value };

        internal static byte[] Rewrite(this MethodInfo sourceMethod, Dictionary<MethodBase, MethodToken> methodsTranslations, int genericTypeToken)
        {
            var sourceMethodBody = sourceMethod.GetMethodBody();
            if (sourceMethodBody == null)
            {
                throw new InvalidOperationException("Interface methods are not supported!");
            }

            var sourceMethodCode = sourceMethodBody.GetILAsByteArray();
            var rewrittenMethodCode = new byte[sourceMethodCode.Length];

            int index = 0;
            while (index < sourceMethodCode.Length)
            {
                if (index + CallOpCodeSize + MetadataTokenSize < sourceMethodCode.Length)
                {
                    byte nextValue = sourceMethodCode[index];
                    if (TryReplaceMethodToken(sourceMethod, methodsTranslations, sourceMethodCode, rewrittenMethodCode, nextValue, index))
                    {
                        index += CallOpCodeSize + MetadataTokenSize;
                        continue;
                    }

                    if (TryReplaceTypeToken(genericTypeToken, nextValue, rewrittenMethodCode, index))
                    {
                        index += CallOpCodeSize + MetadataTokenSize;
                        continue;
                    }
                }

                rewrittenMethodCode[index] = sourceMethodCode[index++];
            }

            if (rewrittenMethodCode.Length != sourceMethodCode.Length)
            {
                throw new Exception("critical error during CIL rewrite, code length mismatch");
            }

            return rewrittenMethodCode;
        }

        private static bool TryReplaceMethodToken(
            MethodInfo sourceMethod,
            Dictionary<MethodBase, MethodToken> methodsTranslations,
            byte[] sourceMethodCode,
            byte[] rewrittenMethodCode,
            byte nextValue,
            int index)
        {
            if (nextValue != CallOpCodeValue)
            {
                return false;
            }

            int possibleMethodToken = BitConverter.ToInt32(sourceMethodCode, index + 1);

            try
            {
                var resolvedMethod = sourceMethod.Module
                    .ResolveMethod(
                        possibleMethodToken,
                        (sourceMethod.DeclaringType == null) ? null : sourceMethod.DeclaringType.GetGenericArguments(),
                        sourceMethod.GetGenericArguments());

                MethodToken newToken;
                if (resolvedMethod != null && methodsTranslations.TryGetValue(resolvedMethod, out newToken))
                {
                    rewrittenMethodCode[index] = nextValue;
                    Insert(newToken.Token, rewrittenMethodCode, index + 1);

                    return true;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
            }

            return false;
        }

        private static bool TryReplaceTypeToken(
            int genericTypeToken,
            byte nextValue,
            byte[] rewrittenMethodCode,
            int index)
        {
            foreach (var typeSpecificOpCode in TypeSpecificOpCodes)
            {
                if (nextValue == typeSpecificOpCode)
                {
                    rewrittenMethodCode[index++] = nextValue;
                    Insert(genericTypeToken, rewrittenMethodCode, index);

                    return true;
                }
            }

            return false;
        }

        private static void Insert(int value, byte[] array, int index)
        {
            array[index++] = (byte)value;
            array[index++] = (byte)(value >> 8);
            array[index++] = (byte)(value >> 16);
            array[index] = (byte)(value >> 24);
        }
    }
}