using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System
{
    public static partial class MemoryExtensions
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

            /// <summary>
            /// Gets the current element of the enumeration.
            /// </summary>
            /// <returns>Returns a <see cref="Range"/> instance that indicates the bounds of the current element withing the source span.</returns>
            public Range Current { get; internal set; }

            internal SpanSplitEnumerator(ReadOnlySpan<T> source, T delimiter)
            {
                Span = source;
                Delimiter = delimiter;
                Current = new Range(0, 0);
                DelimiterSpan = default;
                mode = SpanSplitEnumeratorMode.Delimiter;
            }
            internal SpanSplitEnumerator(ReadOnlySpan<T> source, ReadOnlySpan<T> delimiter, SpanSplitEnumeratorMode mode)
            {
                Span = source;
                DelimiterSpan = delimiter;
                Current = new Range(0, 0);
                Delimiter = default!;
                this.mode = mode;
            }

#if NET8_0
            internal SpanSplitEnumerator(ReadOnlySpan<T> source, SearchValues<T> searchValues)
            {
                Span = source;
                Delimiter = default!;
                SearchValues = searchValues;
                Current = new Range(0, 0);
                DelimiterSpan = default;
                mode = SpanSplitEnumeratorMode.Delimiter;
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
                        index = Span[Current.Start..].IndexOf(Delimiter);
                        length = 1;
                        break;

                    case SpanSplitEnumeratorMode.Any:
                        index = Span[Current.Start..].IndexOfAny(DelimiterSpan);
                        length = 1;
                        break;

                    case SpanSplitEnumeratorMode.Sequence:
                        index = Span[Current.Start..].IndexOf(DelimiterSpan);
                        length = DelimiterSpan.Length;
                        break;

                    case SpanSplitEnumeratorMode.EmptySequence:
                        index = -1;
                        length = 1;
                        break;

#if NET8_0
                    case SpanSplitEnumeratorMode.SearchValues:
                        index = Span[Current.Start..].IndexOfAny(SearchValues);
                        length = 1;
                        break;
#endif
                    default:
                        return false;
                }

                if(index < 0)
                {
                    Current = new Range(Span.Length, Span.Length);
                    mode = (SpanSplitEnumeratorMode)(-1);
                    return true;
                }
           
                Current = new Range(Current.End.Value + length, Current.Start.Value + index);
                
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