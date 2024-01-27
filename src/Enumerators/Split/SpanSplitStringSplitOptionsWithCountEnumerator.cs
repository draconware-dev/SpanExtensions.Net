using System;

namespace SpanExtensions.Enumerators
{
    /// <summary>
    /// Supports iteration over a <see cref="ReadOnlySpan{Char}"/> by splitting it at a specified delimiter and based on specified <see cref="StringSplitOptions"/>  with an upper limit of splits performed.
    /// </summary>
    public ref struct SpanSplitStringSplitOptionsWithCountEnumerator
    {
        ReadOnlySpan<char> Span;
        readonly char Delimiter;
        readonly StringSplitOptions Options;
        readonly int Count;
        readonly CountExceedingBehaviour CountExceedingBehaviour;
        int currentCount;
        readonly int CountMinusOne;

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        public ReadOnlySpan<char> Current { get; internal set; }

        /// <summary>
        /// Constructs a <see cref="SpanSplitStringSplitOptionsWithCountEnumerator"/> from a span and a delimiter. <strong>Only consume this class through <see cref="ReadOnlySpanExtensions.Split(ReadOnlySpan{char}, char, int, StringSplitOptions)"/></strong>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Char}"/> to be split.</param>
        /// <param name="delimiter">A <see cref="char"/> that delimits the various sub-ReadOnlySpans in <paramref name="source"/>.</param>
        /// <param name="count">The maximum number of sub-ReadOnlySpans to split into.</param>
        /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim results and include empty results.</param>
        /// <param name="countExceedingBehaviour">The handling of the instances more than count.</param>
        public SpanSplitStringSplitOptionsWithCountEnumerator(ReadOnlySpan<char> source, char delimiter, int count, StringSplitOptions options, CountExceedingBehaviour countExceedingBehaviour = CountExceedingBehaviour.CutLastElements)
        {
            Span = source;
            Delimiter = delimiter;
            Count = count;
            CountExceedingBehaviour = countExceedingBehaviour;
            Options = options;
            Current = default;
            currentCount = 0;
            CountMinusOne = Math.Max(Count - 1, 0);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        public readonly SpanSplitStringSplitOptionsWithCountEnumerator GetEnumerator()
        {
            return this;
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns><see langword="true"/> if the enumerator was successfully advanced to the next element; <see langword="false"/> if the enumerator has passed the end of the collection.</returns>
        public bool MoveNext()
        {
            ReadOnlySpan<char> span = Span;
            if(span.IsEmpty)
            {
                return false;
            }
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
                        ReadOnlySpan<char> upper = span[(index + 1)..];
                        Span<char> temp = new char[lower.Length + upper.Length];
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
                Span = ReadOnlySpan<char>.Empty;
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
                    Span = span[(index + 1)..];
                    return MoveNext();
                }
            }
            Span = span[(index + 1)..];
            return true;
        }
    }
}