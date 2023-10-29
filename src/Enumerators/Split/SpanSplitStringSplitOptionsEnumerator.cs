namespace SpanExtensions;

/// <summary> 
/// Supports iteration over a <see cref="ReadOnlySpan{Char}"/> by splitting it at a specified delimiter and based on specified <see cref="StringSplitOptions"/>.  
/// </summary>   
public ref struct SpanSplitStringSplitOptionsEnumerator
{
    ReadOnlySpan<char> Span;
    readonly char Delimiter;  
    readonly StringSplitOptions Options;
    public ReadOnlySpan<char> Current { get; internal set; }

    /// <summary>
    /// Constructs a <see cref="SpanSplitEnumerator{Char}"/> from a span and a delimiter. ONLY CONSUME THIS CLASS THROUGH <see cref="ReadOnlySpanExtensions.Split(ReadOnlySpan{char}, char, StringSplitOptions)"/>. 
    /// </summary>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to be split.</param>  
    public SpanSplitStringSplitOptionsEnumerator(ReadOnlySpan<char> source, char delimiter, StringSplitOptions options)
    {
        Span = source;
        Delimiter = delimiter;
        Options = options;
    }

    public SpanSplitStringSplitOptionsEnumerator GetEnumerator()
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
        if (span.IsEmpty)
        {
            return false;
        }
        int index = span.IndexOf(Delimiter);

        if (index == -1 || index >= span.Length)
        {
            Span = ReadOnlySpan<char>.Empty;
            Current = span;
            return true;
        }
        Current = span[..index];
        
        if(Options.HasFlag(StringSplitOptions.TrimEntries ))
        {
            Current = Current.Trim();
        }
        if (Options.HasFlag(StringSplitOptions.RemoveEmptyEntries ))
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
