namespace SpanExtensions;

public ref struct SpanSplitSequenceStringSplitOptionsWithCountEnumerator
{
    ReadOnlySpan<char> Span;
    readonly ReadOnlySpan<char> Delimiter;
    readonly StringSplitOptions Options;
    readonly int Count;
    int currentCount;

    public ReadOnlySpan<char> Current { get; internal set; }

    public SpanSplitSequenceStringSplitOptionsWithCountEnumerator(ReadOnlySpan<char> span, ReadOnlySpan<char> delimiter, StringSplitOptions options, int count)
    {
        Span = span;
        Delimiter = delimiter;
        Options = options;
        Count = count;
    }

    public SpanSplitSequenceStringSplitOptionsWithCountEnumerator GetEnumerator()
    {
        return this;
    }

    public bool MoveNext()
    {
        ReadOnlySpan<char> span = Span;
        if (span.IsEmpty)
        {
            return false;
        }
        if (currentCount == Count)
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
        currentCount++;
        Current = span[..index];

        if (Options.HasFlag(StringSplitOptions.TrimEntries))
        {
            Current = Current.Trim();
        }
        if (Options.HasFlag(StringSplitOptions.RemoveEmptyEntries))
        {
            if (Current.IsEmpty)
            {
                Span = span[(index + Delimiter.Length)..];
                return MoveNext();
            }
        }
        Span = span[(index + Delimiter.Length)..];
        return true;
    }

}
