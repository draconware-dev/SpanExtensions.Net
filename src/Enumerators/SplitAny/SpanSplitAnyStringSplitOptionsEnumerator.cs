namespace SpanExtensions;

public ref struct SpanSplitAnyStringSplitOptionsEnumerator
{
    ReadOnlySpan<char> Span;
    readonly ReadOnlySpan<char> Delimiters;
    readonly StringSplitOptions Options;
    public ReadOnlySpan<char> Current { get; internal set; }
    readonly ReadOnlySpan<char> WhiteSpace = " \r\n";

    public SpanSplitAnyStringSplitOptionsEnumerator(ReadOnlySpan<char> span, ReadOnlySpan<char> delimiters, StringSplitOptions options)
    {
        Span = span;
        Delimiters = delimiters;
        Options = options;
    }

    public SpanSplitAnyStringSplitOptionsEnumerator GetEnumerator()
    {
        return this;
    }

    public bool MoveNext()
    {
        var span = Span;
        if (span.IsEmpty)
        {
            return false;
        }
        int index = span.IndexOfAny(Delimiters);

        if (index == -1 || index >= span.Length)
        {
            Span = ReadOnlySpan<char>.Empty;
            Current = span;
            return true;
        }
        Current = span[..index];

        if (Options.HasFlag(StringSplitOptions.TrimEntries))
        {
            Current = Current.Trim();
        }
        if (Options.HasFlag(StringSplitOptions.RemoveEmptyEntries))
        {
            if (Current.IsEmpty)
            {
                Span = span.Slice(index + 1);
                return MoveNext();
            }
        }
        Span = span.Slice(index + 1);
        return true;
    }

}
