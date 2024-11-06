using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#if !NET9_0_OR_GREATER

namespace System
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static partial class MemoryExtensions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        /// <summary>
        /// Returns a type that allows for enumeration of each element within a split span
        /// using the provided separator character.
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The source span to be enumerated.</param>
        /// <param name="separator">The separator character to be used to split the provided span.</param>
        /// <returns>Returns a <see cref="SpanSplitEnumerator{T}"/>.</returns>
        public static SpanSplitEnumerator<T> Split<T>(this Span<T> source, T separator) where T : IEquatable<T>
        {
            return new SpanSplitEnumerator<T>(source, separator);
        }

        /// <summary>
        /// Returns a type that allows for enumeration of each element within a split span
        /// using the provided separator span.
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The source span to be enumerated.</param>
        /// <param name="separator">The separator span to be used to split the provided span.</param>
        /// <returns>Returns a <see cref="SpanSplitEnumerator{T}"/>.</returns>
        public static SpanSplitEnumerator<T> Split<T>(this Span<T> source, ReadOnlySpan<T> separator) where T : IEquatable<T>
        {
            return new SpanSplitEnumerator<T>(source, separator, SpanSplitEnumeratorMode.Sequence);
        }

        /// <summary>
        /// Returns a type that allows for enumeration of each element within a split span
        /// using any of the provided elements.
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The source span to be enumerated.</param>
        /// <param name="separators">The separators to be used to split the provided span.</param>
        /// <returns>Returns a <see cref="SpanSplitEnumerator{T}"/>.</returns>
        public static SpanSplitEnumerator<T> SplitAny<T>(this Span<T> source, ReadOnlySpan<T> separators) where T : IEquatable<T>
        {
            return new SpanSplitEnumerator<T>(source, separators, SpanSplitEnumeratorMode.Any);
        }

#if NET8_0
        /// <summary>
        /// Returns a type that allows for enumeration of each element within a split span
        /// using the provided <see cref="SpanSplitEnumerator{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The source span to be enumerated.</param>
        /// <param name="separators">The <see cref="SpanSplitEnumerator{T}"/> to be used to split the provided span.</param>
        /// <returns>Returns a <see cref="SpanSplitEnumerator{T}"/>.</returns>
        /// <remarks>
        /// Unlike <see cref="SplitAny{T}(Span{T}, ReadOnlySpan{T})"/>, the <paramref name="separators"/> is not checked for being empty.
        /// An empty <paramref name="separators"/> will result in no separators being found, regardless of the type of <typeparamref name="T"/>, whereas <see cref="SplitAny{T}(Span{T}, ReadOnlySpan{T})"/> will use all Unicode whitespace characters as separators if <paramref name="separators"/> is empty and <typeparamref name="T"/> is <see cref="char"/>.
        /// </remarks>
        public static SpanSplitEnumerator<T> SplitAny<T>(this Span<T> source, SearchValues<T> separators) where T : IEquatable<T>
        {
            return new SpanSplitEnumerator<T>(source, separators);
        }
#endif

    }
}

#endif