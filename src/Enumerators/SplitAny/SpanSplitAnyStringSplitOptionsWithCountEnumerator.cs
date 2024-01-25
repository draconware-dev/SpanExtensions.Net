using System;

namespace SpanExtensions.Enumerators
{
    /// <summary>
    /// Supports iteration over a <see cref="ReadOnlySpan{Char}"/> by splitting it at specified delimiters and based on specified <see cref="StringSplitOptions"/>.
    /// </summary>
    public ref struct SpanSplitAnyStringSplitOptionsWithCountEnumerator
    {
        ReadOnlySpan<char> Span;
        readonly ReadOnlySpan<char> Delimiters;
        readonly StringSplitOptions Options;
        readonly int Count;
        int currentCount;

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        public ReadOnlySpan<char> Current { get; internal set; }

        /// <summary>
        /// Constructs a <see cref="SpanSplitAnyStringSplitOptionsWithCountEnumerator"/> from a span and a delimiter. <strong>Only consume this class through <see cref="ReadOnlySpanExtensions.SplitAny(ReadOnlySpan{char}, ReadOnlySpan{char}, StringSplitOptions)"/></strong>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Char}"/> to be split.</param>
        /// <param name="delimiters">A <see cref="ReadOnlySpan{Char}"/> with the <see cref="char"/> elements that delimit the various sub-ReadOnlySpans in <paramref name="source"/>.</param>
        /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim results and include empty results.</param>
        /// <param name="count">The maximum number of sub-ReadOnlySpans to split into.</param>
        public SpanSplitAnyStringSplitOptionsWithCountEnumerator(ReadOnlySpan<char> source, ReadOnlySpan<char> delimiters, StringSplitOptions options, int count)
        {
            Span = source;
            Delimiters = delimiters;
            Options = options;
            Count = count;
            Current = default;
            currentCount = 0;
        }

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        public readonly SpanSplitAnyStringSplitOptionsWithCountEnumerator GetEnumerator()
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
            int index = span.IndexOfAny(Delimiters);

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