using System;

namespace SpanExtensions.Enumerators
{
    /// <summary>
    /// Supports iteration over a <see cref="ReadOnlySpan{Char}"/> by splitting it at a specified delimiter and based on specified <see cref="StringSplitOptions"/>.
    /// </summary>
    public ref struct SpanSplitStringSplitOptionsEnumerator
    {
        ReadOnlySpan<char> Span;
        readonly char Delimiter;
        readonly StringSplitOptions Options;

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        public ReadOnlySpan<char> Current { get; internal set; }

        /// <summary>
        /// Constructs a <see cref="SpanSplitStringSplitOptionsEnumerator"/> from a span and a delimiter. <strong>Only consume this class through <see cref="ReadOnlySpanExtensions.Split(ReadOnlySpan{char}, char, StringSplitOptions)"/></strong>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Char}"/> to be split.</param>
        /// <param name="delimiter">A <see cref="char"/> that delimits the various sub-ReadOnlySpans in <paramref name="source"/>.</param>
        /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim results and include empty results.</param>
        public SpanSplitStringSplitOptionsEnumerator(ReadOnlySpan<char> source, char delimiter, StringSplitOptions options)
        {
            Span = source;
            Delimiter = delimiter;
            Options = options;
            Current = default;
        }

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        public readonly SpanSplitStringSplitOptionsEnumerator GetEnumerator()
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
            int index = span.IndexOf(Delimiter);

            if(index == -1 || index >= span.Length)
            {
                Span = ReadOnlySpan<char>.Empty;
                Current = span;
                return true;
            }
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