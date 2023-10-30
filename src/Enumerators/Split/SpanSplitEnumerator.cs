namespace SpanExtensions;

/// <summary>
/// Supports iteration over a <see cref="ReadOnlySpan{T}"/> by splitting it at a specified delimiter of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of elements in the enumerated <see cref="ReadOnlySpan{T}"/></typeparam>
public ref struct SpanSplitEnumerator<T> where T : IEquatable<T>
{
    ReadOnlySpan<T> Span;
    readonly T Delimiter;
     
    public ReadOnlySpan<T> Current { get; internal set; }

    /// <summary>
    /// Constructs a <see cref="SpanSplitEnumerator{T}"/> from a span and a delimiter. ONLY CONSUME THIS CLASS THROUGH <see cref="ReadOnlySpanExtensions.Split{T}(ReadOnlySpan{T}, T)"/>. 
    /// </summary>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to be split.</param>  
    /// <param name="delimiter">An instance of <typeparamref name="T"/> that delimits the various sub-ReadOnlySpans in the <see cref="ReadOnlySpan{T}"/>.</param>
    public SpanSplitEnumerator(ReadOnlySpan<T> source, T delimiter)
    {
        Span = source;
        Delimiter = delimiter;
    }

    public SpanSplitEnumerator<T> GetEnumerator()
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
        if (span.IsEmpty)
        { 
            return false;
        }
        int index = span.IndexOf(Delimiter);

        if (index == -1 || index >= span.Length)   
        {
            Span = ReadOnlySpan<T>.Empty;
            Current = span;
            return true;
        }
        Current = span[..index];   
        Span = span[(index + 1)..]; 
        return true; 
    }
}