using System;

namespace SpanExtensions.Enumerators
{
    /// <summary> 
    /// Supports iteration over a <see cref="ReadOnlySpan{Char}"/> by splitting it at specified delimiters and based on specified <see cref="StringSplitOptions"/>.  
    /// </summary>   
    public ref struct SpanSplitSequenceEnumerator<T> where T : IEquatable<T>
    {
        ReadOnlySpan<T> Span;
        readonly ReadOnlySpan<T> Delimiter;

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator. 
        /// </summary>
        public ReadOnlySpan<T> Current { get; internal set; }

        public SpanSplitSequenceEnumerator(ReadOnlySpan<T> source, ReadOnlySpan<T> delimiter)
        {
            Span = source;
            Delimiter = delimiter;
            Current = default;
        }
        /// <summary></summary>
        public readonly SpanSplitSequenceEnumerator<T> GetEnumerator()
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
            int index = span.IndexOf(Delimiter);

            if(index == -1 || index >= span.Length)
            {
                Span = ReadOnlySpan<T>.Empty;
                Current = span;
                return true;
            }
            Current = span[..index];
            Span = span[(index + Delimiter.Length)..];
            return true;
        }
    }
}