﻿using System;

namespace SpanExtensions.Enumerators
{

    /// <summary>
    /// Supports iteration over a <see cref="ReadOnlySpan{T}"/> by splitting it at specified delimiters of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the enumerated <see cref="ReadOnlySpan{T}"/>.</typeparam>
    public ref struct SpanSplitAnyEnumerator<T> where T : IEquatable<T>
    {
        ReadOnlySpan<T> Span;
        readonly ReadOnlySpan<T> Delimiters;
        bool enumerationDone;

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        public ReadOnlySpan<T> Current { get; internal set; }

        /// <summary>
        /// Constructs a <see cref="SpanSplitAnyEnumerator{T}"/> from a span and a delimiter. <strong>Only consume this class through <see cref="ReadOnlySpanExtensions.SplitAny{T}(ReadOnlySpan{T}, ReadOnlySpan{T})"/></strong>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to be split.</param>
        /// <param name="delimiters">A <see cref="ReadOnlySpan{T}"/> with the instances of <typeparamref name="T"/> that delimit the various sub-ReadOnlySpans in <paramref name="source"/>.</param>
        public SpanSplitAnyEnumerator(ReadOnlySpan<T> source, ReadOnlySpan<T> delimiters)
        {
            Span = source;
            Delimiters = delimiters;
            Current = default;
            enumerationDone = false;
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        public readonly SpanSplitAnyEnumerator<T> GetEnumerator()
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

            ReadOnlySpan<T> span = Span;
            int index = span.IndexOfAny(Delimiters);

            if(index == -1 || index >= span.Length)
            {
                enumerationDone = true;
                Current = span;
                return true;
            }
            Current = span[..index];
            Span = span[(index + 1)..];
            return true;
        }
    }
}