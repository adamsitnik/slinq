using System;

namespace Slinq
{
    /// <summary>
    /// based on: https://github.com/dotnet/corefx/blob/master/src/System.Linq/src/System/Linq/Errors.cs
    /// to throw the same kind of exceptions so people's catch block can work in the same way as they used to when they were using LINQ
    /// </summary>
    internal static class Error
    {
        internal static Exception ArgumentNull(string argumentName)
        {
            return new ArgumentNullException(argumentName);
        }

        internal static Exception ArgumentOutOfRange(string argumentName)
        {
            return new ArgumentOutOfRangeException(argumentName);
        }

        internal static Exception MoreThanOneElement()
        {
            return new InvalidOperationException("More than one element");
        }

        internal static Exception MoreThanOneMatch()
        {
            return new InvalidOperationException("More than one match");
        }

        internal static Exception NoElements()
        {
            return new InvalidOperationException("No elements");
        }

        internal static Exception NoMatch()
        {
            return new InvalidOperationException("No match");
        }

        internal static Exception NotSupported()
        {
            return new NotSupportedException();
        }
    }
}