using System;
using System.Runtime.CompilerServices;

namespace Vectorial1024.Collections.Generic
{
    internal static class ThrowHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowBadEnumerationException()
        {
            throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowArgumentOutOfRangeException(int index)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }
    }
}