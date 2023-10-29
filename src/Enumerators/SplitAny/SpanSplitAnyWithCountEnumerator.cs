namespace SpanExtensions;
public ref struct SpanSplitAnyWithCountEnumerator<T> where T : IEquatable<T>
{
    ReadOnlySpan<T> Span;
    readonly ReadOnlySpan<T> Delimiters;
    readonly int Count;
    int currentCount;

    public ReadOnlySpan<T> Current { get; internal set; }

    public SpanSplitAnyWithCountEnumerator(ReadOnlySpan<T> span, ReadOnlySpan<T> delimiters, int count)
    {
        Span = span;
        Delimiters = delimiters;
        Count = count;

    }
    public SpanSplitAnyWithCountEnumerator<T> GetEnumerator()
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
        if (currentCount == Count)
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
        currentCount++;
        Current = span[..index];
        Span = span.Slice(index + 1);
        return true;
    }

}