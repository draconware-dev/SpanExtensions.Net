namespace SpanExtensions;

public ref struct SpanSplitSequenceEnumerator<T> where T : IEquatable<T>
{
    ReadOnlySpan<T> Span;
    readonly ReadOnlySpan<T> Delimiter;

    public ReadOnlySpan<T> Current { get; internal set; }

    public SpanSplitSequenceEnumerator(ReadOnlySpan<T> span, ReadOnlySpan<T> delimiter)
    {
        Span = span;
        Delimiter = delimiter;
    }

    public SpanSplitSequenceEnumerator<T> GetEnumerator()
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
        int index = span.IndexOf(Delimiter);

        if (index == -1 || index >= span.Length)
        {
            Span = ReadOnlySpan<T>.Empty;
            Current = span;
            return true;
        }
        Current = span[..index];
        Span = span.Slice(index + Delimiter.Length);
        return true;
    }

}
