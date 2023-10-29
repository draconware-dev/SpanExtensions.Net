namespace SpanExtensions;

/// <summary>
/// Supports iteration over a <see cref="ReadOnlySpan{T}"/> by splitting it a a specified delimiter of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of elements in the enumerated <see cref="ReadOnlySpan{T}"/></typeparam>
public ref struct SpanSplitEnumerator<T> where T : IEquatable<T>
{
    ReadOnlySpan<T> Span;
    readonly T Delimiter;
     
    public ReadOnlySpan<T> Current { get; internal set; }
     
    public SpanSplitEnumerator(ReadOnlySpan<T> span, T delimiter)
    {
        Span = span;
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
        var span = Span;
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