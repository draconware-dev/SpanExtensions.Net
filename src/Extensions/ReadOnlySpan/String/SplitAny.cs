using System;
using System.Collections;
using SpanExtensions.Enumerators;

namespace SpanExtensions
{
    public static partial class ReadOnlySpanExtensions
    {
        /// <summary>
        /// Splits a <see cref="ReadOnlySpan{T}"/> into multiple ReadOnlySpans based on the any of the specified <paramref name="delimiters"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to be split.</param>
        /// <param name="delimiters">A <see cref="ReadOnlySpan{T}"/> with the instances of <typeparamref name="T"/> that delimit the various sub-ReadOnlySpans in <paramref name="source"/>.</param>
        /// <returns>An instance of the ref struct <see cref="SpanSplitAnyEnumerator{T}"/>, which works the same way as every <see cref="IEnumerator"/> does and can be used in a foreach construct.</returns>
        public static SpanSplitAnyEnumerator<T> SplitAny<T>(this ReadOnlySpan<T> source, ReadOnlySpan<T> delimiters) where T : IEquatable<T>
        {
            return new SpanSplitAnyEnumerator<T>(source, delimiters);
        }

        /// <summary>
        /// Splits a <see cref="ReadOnlySpan{T}"/> into at most <paramref name="count"/> ReadOnlySpans based on the any of the specified  <paramref name="delimiters"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to be split.</param>
        /// <param name="delimiters">A <see cref="ReadOnlySpan{T}"/> with the instances of <typeparamref name="T"/> that delimit the various sub-ReadOnlySpans in <paramref name="source"/>.</param>
        /// <param name="count">The maximum number of sub-ReadOnlySpans to split into.</param>
        /// <param name="countExceedingBehaviour">The handling of the instances more than count.</param>
        /// <returns>An instance of the ref struct <see cref="SpanSplitAnyWithCountEnumerator{T}"/>, which works the same way as every <see cref="IEnumerator"/> does and can be used in a foreach construct.</returns>
        public static SpanSplitAnyWithCountEnumerator<T> SplitAny<T>(this ReadOnlySpan<T> source, ReadOnlySpan<T> delimiters, int count, CountExceedingBehaviour countExceedingBehaviour = CountExceedingBehaviour.AppendRemainingElements) where T : IEquatable<T>
        {
            return new SpanSplitAnyWithCountEnumerator<T>(source, delimiters, count, countExceedingBehaviour);
        }

        /// <summary>
        /// Splits a <see cref="ReadOnlySpan{Char}"/> into multiple ReadOnlySpans based on the any of the specified <paramref name="delimiters"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Char}"/> to be split.</param>
        /// <param name="delimiters">A <see cref="ReadOnlySpan{Char}"/>, that delimit the various sub-ReadOnlySpans in <paramref name="source"/>.</param>
        /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim results and include empty results.</param>
        /// <returns>An instance of the ref struct <see cref="SpanSplitAnyStringSplitOptionsEnumerator"/>, which works the same way as every <see cref="IEnumerator"/> does and can be used in a foreach construct.</returns>
        public static SpanSplitAnyStringSplitOptionsEnumerator SplitAny(this ReadOnlySpan<char> source, ReadOnlySpan<char> delimiters, StringSplitOptions options)
        {
            return new SpanSplitAnyStringSplitOptionsEnumerator(source, delimiters, options);
        }

        /// <summary>
        /// Splits a <see cref="ReadOnlySpan{Char}"/> into at most <paramref name="count"/> ReadOnlySpans based on the any of the specified <paramref name="delimiters"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Char}"/> to be split.</param>
        /// <param name="delimiters">A <see cref="ReadOnlySpan{Char}"/>, that delimit the various sub-ReadOnlySpans in <paramref name="source"/>.</param>
        /// <param name="count">The maximum number of sub-ReadOnlySpans to split into.</param>
        /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim results and include empty results.</param>
        /// <param name="countExceedingBehaviour">The handling of the instances more than count.</param>
        /// <returns>An instance of the ref struct <see cref="SpanSplitAnyStringSplitOptionsWithCountEnumerator"/>, which works the same way as every <see cref="IEnumerator"/> does and can be used in a foreach construct.</returns>
        public static SpanSplitAnyStringSplitOptionsWithCountEnumerator SplitAny(this ReadOnlySpan<char> source, ReadOnlySpan<char> delimiters, int count, StringSplitOptions options, CountExceedingBehaviour countExceedingBehaviour = CountExceedingBehaviour.AppendRemainingElements)
        {
            return new SpanSplitAnyStringSplitOptionsWithCountEnumerator(source, delimiters, count, options, countExceedingBehaviour);
        }
    }
}