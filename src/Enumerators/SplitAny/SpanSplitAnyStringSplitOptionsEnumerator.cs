using System;

namespace SpanExtensions
{
    /// <summary> 
    /// Supports iteration over a <see cref="ReadOnlySpan{Char}"/> by splitting it at specified delimiters and based on specified <see cref="StringSplitOptions"/>.  
    /// </summary>   
    public ref struct SpanSplitAnyStringSplitOptionsEnumerator
    {
        ReadOnlySpan<char> Span;
        readonly ReadOnlySpan<char> Delimiters;
        readonly StringSplitOptions Options;

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator. 
        /// </summary>
        public ReadOnlySpan<char> Current { get; internal set; }

        public SpanSplitAnyStringSplitOptionsEnumerator(ReadOnlySpan<char> span, ReadOnlySpan<char> delimiters, StringSplitOptions options)
        {
            Span = span;
            Delimiters = delimiters;
            Options = options;
            Current = default;
        }
        /// <summary></summary>
        public readonly SpanSplitAnyStringSplitOptionsEnumerator GetEnumerator()
        {
            return this;
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns><code>true</code> if the enumerator was successfully advanced to the next element; <code>false</code> if the enumerator has passed the end of the collection.</returns>
        public bool MoveNext()
        {
            ReadOnlySpan<char> span = Span;
            if(span.IsEmpty)
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