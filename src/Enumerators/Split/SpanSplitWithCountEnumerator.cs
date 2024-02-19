using System;

namespace SpanExtensions.Enumerators
{
    /// <summary>
    /// Supports iteration over a <see cref="ReadOnlySpan{T}"/> by splitting it a a specified delimiter of type <typeparamref name="T"/> with an upper limit of splits performed.
    /// </summary>
    /// <typeparam name="T">The type of elements in the enumerated <see cref="ReadOnlySpan{T}"/>.</typeparam>
    public ref struct SpanSplitWithCountEnumerator<T> where T : IEquatable<T>
    {
        ReadOnlySpan<T> Span;
        readonly T Delimiter;
        readonly int Count;
        readonly CountExceedingBehaviour CountExceedingBehaviour;
        int currentCount;
        bool enumerationDone;
        readonly int CountMinusOne;

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        public ReadOnlySpan<T> Current { get; internal set; }

        /// <summary>
        /// Constructs a <see cref="SpanSplitWithCountEnumerator{T}"/> from a span and a delimiter. <strong>Only consume this class through <see cref="ReadOnlySpanExtensions.Split{T}(ReadOnlySpan{T}, T, int, CountExceedingBehaviour)"/></strong>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to be split.</param>
        /// <param name="delimiter">An instance of <typeparamref name="T"/> that delimits the various sub-ReadOnlySpans in <paramref name="source"/>.</param>
        /// <param name="count">The maximum number of sub-ReadOnlySpans to split into.</param>
        /// <param name="countExceedingBehaviour">The handling of the instances more than count.</param>
        public SpanSplitWithCountEnumerator(ReadOnlySpan<T> source, T delimiter, int count, CountExceedingBehaviour countExceedingBehaviour = CountExceedingBehaviour.AppendRemainingElements)
        {
            Span = source;
            Delimiter = delimiter;
            Count = count;
            CountExceedingBehaviour = countExceedingBehaviour;
            Current = default;
            currentCount = 0;
            enumerationDone = false;
            CountMinusOne = Math.Max(Count - 1, 0);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        public readonly SpanSplitWithCountEnumerator<T> GetEnumerator()
        {
            return this;
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns><see langword="true"/> if the enumerator was successfully advanced to the next element; <see langword="false"/> if the enumerator has passed the end of the collection.</returns>
        public bool MoveNext()
        {
            if(enumerationDone)
            {
                return false;
            }

            ReadOnlySpan<T> span = Span;
            if(currentCount == Count)
            {
                return false;
            }
            int index = span.IndexOf(Delimiter);
            switch(CountExceedingBehaviour)
            {
                case CountExceedingBehaviour.CutRemainingElements:
                    break;
                case CountExceedingBehaviour.AppendRemainingElements:
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
                enumerationDone = true;
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
