using System;

namespace SpanExtensions.Enumerators
{
    /// <summary>
    /// Supports iteration over a <see cref="ReadOnlySpan{Char}"/> by splitting it at a specified delimiter and based on specified <see cref="StringSplitOptions"/>  with an upper limit of splits performed.
    /// </summary>
    public ref struct SpanSplitSequenceStringSplitOptionsWithCountEnumerator
    {
        ReadOnlySpan<char> Span;
        readonly ReadOnlySpan<char> Delimiter;
        readonly StringSplitOptions Options;
        readonly int Count;
        readonly CountExceedingBehaviour CountExceedingBehaviour;
        int currentCount;
        bool enumerationDone;
        readonly int CountMinusOne;
        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        public ReadOnlySpan<char> Current { get; internal set; }

        /// <summary>
        /// Constructs a <see cref="SpanSplitSequenceStringSplitOptionsWithCountEnumerator"/> from a span and a delimiter. <strong>Only consume this class through <see cref="ReadOnlySpanExtensions.Split(ReadOnlySpan{char}, ReadOnlySpan{char}, int, StringSplitOptions, CountExceedingBehaviour)"/></strong>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Char}"/> to be split.</param>
        /// <param name="delimiter">An instance of <see cref="ReadOnlySpan{Char}"/> that delimits the various sub-ReadOnlySpans in <paramref name="source"/>.</param>
        /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim results and include empty results.</param>
        /// <param name="count">The maximum number of sub-ReadOnlySpans to split into.</param>
        /// <param name="countExceedingBehaviour">The handling of the instances more than count.</param>
        public SpanSplitSequenceStringSplitOptionsWithCountEnumerator(ReadOnlySpan<char> source, ReadOnlySpan<char> delimiter, int count, StringSplitOptions options, CountExceedingBehaviour countExceedingBehaviour = CountExceedingBehaviour.AppendLastElements)
        {
            Span = source;
            Delimiter = delimiter;
            Count = count;
            Options = options;
            CountExceedingBehaviour = countExceedingBehaviour;
            Current = default;
            currentCount = 0;
            enumerationDone = false;
            CountMinusOne = Math.Max(Count - 1, 0);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        public readonly SpanSplitSequenceStringSplitOptionsWithCountEnumerator GetEnumerator()
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

            ReadOnlySpan<char> span = Span;
            if(currentCount == Count)
            {
                return false;
            }
            int index = span.IndexOf(Delimiter);

            switch(CountExceedingBehaviour)
            {
                case CountExceedingBehaviour.CutLastElements:
                    break;
                case CountExceedingBehaviour.AppendLastElements:
                    if(currentCount == CountMinusOne)
                    {
                        ReadOnlySpan<char> lower = span[..index];
                        ReadOnlySpan<char> upper = span[(index + Delimiter.Length)..];
                        Span<char> temp = new char[lower.Length + upper.Length];
                        lower.CopyTo(temp[..index]);
                        upper.CopyTo(temp[(index + Delimiter.Length - 1)..]);
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

#if NET5_0_OR_GREATER
            if(Options.HasFlag(StringSplitOptions.TrimEntries))
            {
                Current = Current.Trim();
            }
#endif
            if(Options.HasFlag(StringSplitOptions.RemoveEmptyEntries))
            {
                if(Current.IsEmpty)
                {
                    Span = span[(index + Delimiter.Length)..];
                    if(Span.IsEmpty)
                    {
                        enumerationDone = true;
                        return false;
                    }
                    return MoveNext();
                }

                Span = span[(index + 1)..];
                if(Span.IsEmpty)
                {
                    enumerationDone = true;
                }
                return true;
            }
            Span = span[(index + Delimiter.Length)..];
            return true;
        }
    }
}