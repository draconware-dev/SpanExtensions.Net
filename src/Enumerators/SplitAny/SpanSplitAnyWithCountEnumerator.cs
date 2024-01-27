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
        readonly int Count;
        readonly CountExceedingBehaviour CountExceedingBehaviour;
        int currentCount;
        readonly int CountMinusOne;

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
        public SpanSplitAnyWithCountEnumerator(ReadOnlySpan<T> source, ReadOnlySpan<T> delimiters, int count, CountExceedingBehaviour countExceedingBehaviour = CountExceedingBehaviour.CutLastElements)
        {
            Span = source;
            Delimiters = delimiters;
            Count = count;
            CountExceedingBehaviour = countExceedingBehaviour;
            Current = default;
            currentCount = 0;
            CountMinusOne = Math.Max(Count - 1, 0);
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
            ReadOnlySpan<T> span = Span;
            if(span.IsEmpty)
            {
                return false;
            }
            if(currentCount == Count)
            {
                return false;
            }
            int index = span.IndexOfAny(Delimiters);
            switch(CountExceedingBehaviour)
            {
                case CountExceedingBehaviour.CutLastElements:
                    break;
                case CountExceedingBehaviour.AppendLastElements:
                    if(currentCount == CountMinusOne)
                    {
                        ReadOnlySpan<T> lower = span[..index];
                        ReadOnlySpan<T> upper = span[(index + 1)..];
                        Span<T> temp = new T[lower.Length + upper.Length];
                        lower.CopyTo(temp[..index]);
                        upper.CopyTo(temp[index..]);
                        Current = temp;
                        currentCount++;
                        return true;
                    }
                    break;
                default:
                    throw new InvalidCountExceedingBehaviourException(CountExceedingBehaviour);
            }
            if(index == -1 || index >= span.Length)
            {
                Span = ReadOnlySpan<T>.Empty;
                Current = span;
                return true;
            }
            currentCount++;
            Current = span[..index];
            Span = span[(index + 1)..];
            return true;
        }
    }
}