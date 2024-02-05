using System;

namespace SpanExtensions.Enumerators
{
    /// <summary>
    /// Supports iteration over a <see cref="ReadOnlySpan{T}"/> by splitting it at a specified delimiter of type <typeparamref name="T"/> with an upper limit of splits performed.
    /// </summary>
    /// <typeparam name="T">The type of elements in the enumerated <see cref="ReadOnlySpan{T}"/>.</typeparam>
    public ref struct SpanSplitSequenceWithCountEnumerator<T> where T : IEquatable<T>
    {
        ReadOnlySpan<T> Span;
        readonly ReadOnlySpan<T> Delimiter;
        readonly int DelimiterLength;
        readonly CountExceedingBehaviour CountExceedingBehaviour;
        int CurrentCount;
        bool EnumerationDone;

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        public ReadOnlySpan<T> Current { get; internal set; }

        /// <summary>
        /// Constructs a <see cref="SpanSplitSequenceWithCountEnumerator{T}"/> from a span and a delimiter. <strong>Only consume this class through <see cref="ReadOnlySpanExtensions.Split{T}(ReadOnlySpan{T}, ReadOnlySpan{T}, int, CountExceedingBehaviour)"/></strong>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to be split.</param>
        /// <param name="delimiter">An instance of <see cref="ReadOnlySpan{T}"/> that delimits the various sub-ReadOnlySpans in <paramref name="source"/>.</param>
        /// <param name="count">The maximum number of sub-ReadOnlySpans to split into.</param>
        /// <param name="countExceedingBehaviour">The handling of the instances more than count.</param>
        public SpanSplitSequenceWithCountEnumerator(ReadOnlySpan<T> source, ReadOnlySpan<T> delimiter, int count, CountExceedingBehaviour countExceedingBehaviour = CountExceedingBehaviour.IncludeRemainingElements)
        {
            Span = source;
            Delimiter = delimiter;
            DelimiterLength = delimiter.Length;
            CurrentCount = count.ThrowIfNegative();
            CountExceedingBehaviour = countExceedingBehaviour.ThrowIfInvalid();
            EnumerationDone = count == 0;
            Current = default;
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        public readonly SpanSplitSequenceWithCountEnumerator<T> GetEnumerator()
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

            int delimiterIndex = Span.IndexOf(Delimiter);

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