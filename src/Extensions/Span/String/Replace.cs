#if !NET8_0_OR_GREATER // support for this method has been added in .Net 8. Just include it for backward-compatibility. 

using System;

namespace SpanExtensions
{
    public static partial class SpanExtensions
    {
        /// <summary>
        /// Replaces every occurence of <paramref name="oldT"/> in <paramref name="source"/> with an instance of <paramref name="newT"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>
        /// <param name="oldT">The <typeparamref name="T"/> to be replaced.</param>
        /// <param name="newT">The <typeparamref name="T"/> <paramref name="oldT"/> is to be replaced by.</param>
        public static void Replace<T>(this Span<T> source, T oldT, T newT) where T : IEquatable<T>
        {
            for(int i = 0; i < source.Length; i++)
            {
                if(source[i].Equals(oldT))
                {
                    source[i] = newT;
                }
            }
        }
    }
}
#endif