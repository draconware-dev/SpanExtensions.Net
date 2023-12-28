using System;

namespace SpanExtensions
{
    /// <summary>
    /// Supports iteration over a <see cref="ReadOnlySpan{T}"/> by splitting it a a specified delimiter of type <typeparamref name="T"/> with an upper limit of splits performed.
    /// </summary>
    /// <typeparam name="T">The type of elements in the enumerated <see cref="ReadOnlySpan{T}"/></typeparam>
    public ref struct SpanSplitWithCountEnumerator<T> where T : IEquatable<T>
    {
        ReadOnlySpan<T> Span;
        readonly T Delimiter;
        readonly int Count;
        int currentCount;

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator. 
        /// </summary>
        public ReadOnlySpan<T> Current { get; internal set; }

        /// <summary>
        /// Constructs a <see cref="SpanSplitStringSplitOptionsWithCountEnumerator"/> from a span and a delimiter. ONLY CONSUME THIS CLASS THROUGH <see cref="ReadOnlySpanExtensions.Split(ReadOnlySpan{char}, char, StringSplitOptions, int)"/>. 
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Char}"/> to be split.</param>  
        /// <param name="delimiter">An <see cref="char"/> that delimits the various sub-ReadOnlySpans in the <see cref="ReadOnlySpan{Char}"/>.</param>
        /// <param name="count">the upper limit of results returned. </param>
        public SpanSplitWithCountEnumerator(ReadOnlySpan<T> source, T delimiter, int count)
        {
            Span = source;
            Delimiter = delimiter;
            Count = count;
            Current = default;
            currentCount = 0;
        }
        /// <summary></summary>
        public readonly SpanSplitWithCountEnumerator<T> GetEnumerator()
        {
            return this;
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns><code>true</code> if the enumerator was successfully advanced to the next element; <code>false</code> if the enumerator has passed the end of the collection.</returns>
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
            int index = span.IndexOf(Delimiter);
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