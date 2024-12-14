using System;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#if !NET9_0_OR_GREATER

namespace SpanExtensions
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public static partial class MemoryExtensions
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    {
        /// <summary>
        /// Enables enumerating each split within a <see cref="ReadOnlySpan{T}"/> that has been divided using one or more separators.
        /// </summary>
        public ref struct SpanSplitEnumerator<T> where T : IEquatable<T>
        {
            readonly ReadOnlySpan<T> Span;
            readonly T Delimiter;
            readonly ReadOnlySpan<T> DelimiterSpan;
            SpanSplitEnumeratorMode mode;
#if NET8_0
            readonly SearchValues<T> SearchValues = null!;
#endif

            int currentStartIndex;
            int currentEndIndex;
            int nextStartIndex;
            /// <summary>
            /// Gets the current element of the enumeration.
            /// </summary>
            /// <returns>Returns a <see cref="Range"/> instance that indicates the bounds of the current element within the source span.</returns>
            public readonly Range Current => new Range(currentStartIndex, currentEndIndex);

            internal SpanSplitEnumerator(ReadOnlySpan<T> source, T delimiter)
            {
                Span = source;
                Delimiter = delimiter;
                DelimiterSpan = default;
                mode = SpanSplitEnumeratorMode.Delimiter;
                currentStartIndex = 0;
                currentEndIndex = 0;
                nextStartIndex = 0;
            }

            internal SpanSplitEnumerator(ReadOnlySpan<T> source, ReadOnlySpan<T> delimiter, SpanSplitEnumeratorMode mode)
            {
                Span = source;
                DelimiterSpan = delimiter;
                Delimiter = default!;
                this.mode = mode;
                currentStartIndex = 0;
                currentEndIndex = 0;
                nextStartIndex = 0;
            }

#if NET8_0
            internal SpanSplitEnumerator(ReadOnlySpan<T> source, SearchValues<T> searchValues)
            {
                Span = source;
                Delimiter = default!;
                SearchValues = searchValues;
                DelimiterSpan = default;
                mode = SpanSplitEnumeratorMode.Delimiter;
                currentStartIndex = 0;
                currentEndIndex = 0;
                nextStartIndex = 0;
            }
#endif
            /// <summary>
            /// Returns an enumerator that iterates through a collection.
            /// </summary>
            public readonly SpanSplitEnumerator<T> GetEnumerator()
            {
                return this;
            }

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns><see langword="true"/> if the enumerator was successfully advanced to the next element; <see langword="false"/> if the enumerator has passed the end of the collection.</returns>
            public bool MoveNext()
            {
                int index;
                int length;

                switch(mode)
                {
                    case SpanSplitEnumeratorMode.Delimiter:
                        index = Span[nextStartIndex..].IndexOf(Delimiter);
                        length = 1;
                        break;

                    case SpanSplitEnumeratorMode.Any:
                        index = Span[nextStartIndex..].IndexOfAny(DelimiterSpan);
                        length = 1;
                        break;

                    case SpanSplitEnumeratorMode.Sequence:
                        index = Span[nextStartIndex..].IndexOf(DelimiterSpan);
                        length = DelimiterSpan.Length;
                        break;

                    case SpanSplitEnumeratorMode.EmptySequence:
                        index = -1;
                        length = 1;
                        break;

#if NET8_0
                    case SpanSplitEnumeratorMode.SearchValues:
                        index = Span[nextStartIndex..].IndexOfAny(SearchValues);
                        length = 1;
                        break;
#endif
                    default:
                        return false;
                }

                currentStartIndex = nextStartIndex;
                
                if(index < 0)
                {
                    currentEndIndex = Span.Length;
                    nextStartIndex = Span.Length;
                    
                    mode = (SpanSplitEnumeratorMode)(-1);
                    return true;
                }

                currentEndIndex = currentStartIndex + index;
                nextStartIndex = currentEndIndex + length;
                
                return true;
            }
        }

        internal enum SpanSplitEnumeratorMode
        {
            Delimiter,
            Any,
            Sequence,
            EmptySequence,
            SearchValues
        }
    }
}
#endif