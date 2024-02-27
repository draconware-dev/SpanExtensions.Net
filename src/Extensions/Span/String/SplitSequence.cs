using System;
using System.Collections;
using SpanExtensions.Enumerators;

namespace SpanExtensions
{
    /// <summary>
    /// Extension Methods for <see cref="Span{T}"/>.
    /// </summary>
    public static partial class SpanExtensions
    {
        /// <summary>
        /// Splits a <see cref="Span{T}"/> into multiple ReadOnlySpans based on the specified <paramref name="delimiter"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to be split.</param>
        /// <param name="delimiter">An instance of <see cref="ReadOnlySpan{T}"/> that delimits the various sub-ReadOnlySpans in <paramref name="source"/>.</param>
        /// <returns>An instance of the ref struct <see cref="SpanSplitSequenceEnumerator{T}"/>, which works the same way as every <see cref="IEnumerator"/> does and can be used in a foreach construct.</returns>
        public static SpanSplitSequenceEnumerator<T> Split<T>(this Span<T> source, ReadOnlySpan<T> delimiter) where T : IEquatable<T>
        {
            return new SpanSplitSequenceEnumerator<T>(source, delimiter);
        }

        /// <summary>
        /// Splits a <see cref="Span{T}"/> into at most <paramref name="count"/> ReadOnlySpans based on the specified <paramref name="delimiter"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to be split.</param>
        /// <param name="delimiter">An instance of <see cref="ReadOnlySpan{T}"/> that delimits the various sub-ReadOnlySpans in <paramref name="source"/>.</param>
        /// <param name="count">The maximum number of sub-ReadOnlySpans to split into.</param>
        /// <param name="countExceedingBehaviour">The handling of the instances more than count.</param>
        /// <returns>An instance of the ref struct , which works the same way as every <see cref="IEnumerator"/> does and can be used in a foreach construct.</returns>
        public static SpanSplitSequenceWithCountEnumerator<T> Split<T>(this Span<T> source, ReadOnlySpan<T> delimiter, int count, CountExceedingBehaviour countExceedingBehaviour = CountExceedingBehaviour.AppendRemainingElements) where T : IEquatable<T>
        {
            return new SpanSplitSequenceWithCountEnumerator<T>(source, delimiter, count, countExceedingBehaviour);
        }

        /// <summary>
        /// Splits a <see cref="Span{Char}"/> into multiple ReadOnlySpans based on the specified <paramref name="delimiter"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{Char}"/> to be split.</param>
        /// <param name="delimiter">An instance of <see cref="ReadOnlySpan{Char}"/> that delimits the various sub-ReadOnlySpans in <paramref name="source"/>.</param>
        /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim results and include empty results.</param>
        /// <returns>An instance of the ref struct <see cref="SpanSplitSequenceStringSplitOptionsEnumerator"/>, which works the same way as every <see cref="IEnumerator"/> does and can be used in a foreach construct.</returns>
        public static SpanSplitSequenceStringSplitOptionsEnumerator Split(this Span<char> source, ReadOnlySpan<char> delimiter, StringSplitOptions options)
        {
            return new SpanSplitSequenceStringSplitOptionsEnumerator(source, delimiter, options);
        }

        /// <summary>
        /// Splits a <see cref="Span{Char}"/> into at most <paramref name="count"/> ReadOnlySpans based on the specified <paramref name="delimiter"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{Char}"/> to be split.</param>
        /// <param name="delimiter">An instance of <see cref="ReadOnlySpan{Char}"/> that delimits the various sub-Spans in the <see cref="ReadOnlySpan{Char}"/>.</param>
        /// /// <param name="count">The maximum number of sub-ReadOnlySpans to split into.</param>
        /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim results and include empty results.</param>
        /// <param name="countExceedingBehaviour">The handling of the instances more than count.</param>
        /// <returns>An instance of the ref struct <see cref="SpanSplitSequenceStringSplitOptionsWithCountEnumerator"/>, which works the same way as every <see cref="IEnumerator"/> does and can be used in a foreach construct.</returns>
        public static SpanSplitSequenceStringSplitOptionsWithCountEnumerator Split(this Span<char> source, ReadOnlySpan<char> delimiter, int count, StringSplitOptions options, CountExceedingBehaviour countExceedingBehaviour = CountExceedingBehaviour.AppendRemainingElements)
        {
            return new SpanSplitSequenceStringSplitOptionsWithCountEnumerator(source, delimiter, count, options, countExceedingBehaviour);
        }
    }
}