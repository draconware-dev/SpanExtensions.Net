using System;

namespace SpanExtensions.Enumerators
{
    /// <summary>
    /// Supports iteration over a <see cref="ReadOnlySpan{Char}"/> by splitting it at a specified delimiter and based on specified <see cref="StringSplitOptions"/>.
    /// </summary>
    public ref struct SpanSplitSequenceStringSplitOptionsEnumerator
    {
        ReadOnlySpan<char> Span;
        readonly ReadOnlySpan<char> Delimiter;
        readonly int DelimiterLength;
        readonly bool DelimiterIsEmpty;
        readonly bool TrimEntries;
        readonly bool RemoveEmptyEntries;
        bool EnumerationDone;

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        public ReadOnlySpan<char> Current { get; internal set; }

        /// <summary>
        /// Constructs a <see cref="SpanSplitSequenceStringSplitOptionsEnumerator"/> from a span and a delimiter. <strong>Only consume this class through <see cref="ReadOnlySpanExtensions.Split(ReadOnlySpan{char}, ReadOnlySpan{char}, StringSplitOptions)"/></strong>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Char}"/> to be split.</param>
        /// <param name="delimiter">An instance of <see cref="ReadOnlySpan{Char}"/> that delimits the various sub-ReadOnlySpans in <paramref name="source"/>.</param>
        /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim results and include empty results.</param>
        public SpanSplitSequenceStringSplitOptionsEnumerator(ReadOnlySpan<char> source, ReadOnlySpan<char> delimiter, StringSplitOptions options)
        {
            Span = source;
            Delimiter = delimiter;
            DelimiterLength = Delimiter.Length;
            DelimiterIsEmpty = Delimiter.IsEmpty;
            TrimEntries = options.IsTrimEntriesSet();
            RemoveEmptyEntries = options.IsRemoveEmptyEntriesSet();
            EnumerationDone = false;
            Current = default;
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        public readonly SpanSplitSequenceStringSplitOptionsEnumerator GetEnumerator()
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

            while(true) // if RemoveEmptyEntries options flag is set, repeat until a non-empty span is found, or the end is reached
            {
                int delimiterIndex = Span.IndexOf(Delimiter);

                if(delimiterIndex == -1 || DelimiterIsEmpty)
                {
                    EnumerationDone = true;

                    Current = Span;

                    if(TrimEntries)
                    {
                        Current = Current.Trim();
                    }

                    return !(RemoveEmptyEntries && Current.IsEmpty);
                }

                Current = Span[..delimiterIndex];
                Span = Span[(delimiterIndex + DelimiterLength)..];

                if(TrimEntries)
                {
                    Current = Current.Trim();
                }

                if(RemoveEmptyEntries && Current.IsEmpty)
                {
                    continue;
                }

                return true;
            }
        }
    }
}