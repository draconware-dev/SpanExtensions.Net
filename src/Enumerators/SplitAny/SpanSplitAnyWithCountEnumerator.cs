using System;

namespace SpanExtensions.Enumerators
{
    /// <summary>
    /// Supports iteration over a <see cref="ReadOnlySpan{T}"/> by splitting it at specified delimiters of type <typeparamref name="T"/> with an upper limit of splits performed.
    /// </summary>
    /// <typeparam name="T">The type of elements in the enumerated <see cref="ReadOnlySpan{T}"/>.</typeparam>
    public ref struct SpanSplitAnyWithCountEnumerator<T> where T : IEquatable<T>
    {
        ReadOnlySpan<T> Span;
        readonly ReadOnlySpan<T> Delimiters;
        const int DelimiterLength = 1;
        readonly CountExceedingBehaviour CountExceedingBehaviour;
        int CurrentCount;
        bool EnumerationDone;

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        public ReadOnlySpan<T> Current { get; internal set; }

        /// <summary>
        /// Constructs a <see cref="SpanSplitAnyEnumerator{T}"/> from a span and a delimiter. <strong>Only consume this class through <see cref="ReadOnlySpanExtensions.SplitAny{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, int, CountExceedingBehaviour)"/></strong>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to be split.</param>
        /// <param name="delimiters">A <see cref="ReadOnlySpan{T}"/> with the instances of <typeparamref name="T"/> that delimit the various sub-ReadOnlySpans in <paramref name="source"/>.</param>
        /// <param name="count">The maximum number of sub-ReadOnlySpans to split into.</param>
        /// <param name="countExceedingBehaviour">The handling of the instances more than count.</param>
        public SpanSplitAnyWithCountEnumerator(ReadOnlySpan<T> source, ReadOnlySpan<T> delimiters, int count, CountExceedingBehaviour countExceedingBehaviour = CountExceedingBehaviour.IncludeRemainingElements)
        {
            Span = source;
            Delimiters = delimiters;
            CurrentCount = Math.Max(1, count);
            CountExceedingBehaviour = countExceedingBehaviour.ThrowIfInvalid();
            EnumerationDone = false;
            Current = default;
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        public readonly SpanSplitAnyWithCountEnumerator<T> GetEnumerator()
        {
            return this;
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns><see langword="true"/> if the enumerator was successfully advanced to the next element; <see langword="false"/> if the enumerator has passed the end of the collection.</returns>
        public bool MoveNext()
        {
            if(EnumerationDone)
            {
                return false;
            }

            int delimiterIndex = Span.IndexOfAny(Delimiters);

            if(delimiterIndex == -1 || CurrentCount == 1)
            {
                EnumerationDone = true;

                Current = delimiterIndex == -1 || CountExceedingBehaviour == CountExceedingBehaviour.IncludeRemainingElements ? Span : Span[..delimiterIndex];

                return true;
            }

            Current = Span[..delimiterIndex];
            Span = Span[(delimiterIndex + DelimiterLength)..];

            CurrentCount--;
            return true;
        }
    }
}