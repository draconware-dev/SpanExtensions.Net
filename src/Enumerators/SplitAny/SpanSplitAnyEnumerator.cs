namespace SpanExtensions;

public ref struct SpanSplitAnyEnumerator<T> where T : IEquatable<T>
{
    ReadOnlySpan<T> Span;
    readonly ReadOnlySpan<T> Delimiters;

    public ReadOnlySpan<T> Current { get; internal set; }

    public SpanSplitAnyEnumerator(ReadOnlySpan<T> span, ReadOnlySpan<T> delimiters)
    {
        Span = span;
        Delimiters = delimiters;
    }

    public SpanSplitAnyEnumerator<T> GetEnumerator()
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
            Span = ReadOnlySpan<T>.Empty;
            Current = span;
            return true;
        }
        Current = span[..index];
        Span = span.Slice(index + 1);
        return true;
    }

}
