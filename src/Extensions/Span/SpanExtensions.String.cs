namespace SpanExtensions;

public static partial class SpanExtensions
{

    /// <summary>
    /// Returns a new <see cref="Span{T}"/> in which all the characters in the current instance, beginning at <paramref name="startIndex"/> and continuing through the last position, have been deleted.
    /// </summary>
    /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam> 
    /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>
    /// <param name="startIndex">The zero-based position to begin deleting characters.</param>
    /// <returns>A new <see cref="Span{T}"/> that is equivalent to this <see cref="Span{T}"/> except for the removed characters.</returns>  
    public static Span<T> Remove<T>(this Span<T> source, int startIndex)
    {
        return source[..startIndex];
    }

    /// <summary>
    /// Replaces every occurence of <paramref name="oldT"/> in <paramref name="source"/> with an instance of <paramref name="newT"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
    /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>
    /// <param name="oldT">The <typeparamref name="T"/> to be replaced.</param>
    /// <param name="newT">The <typeparamref name="T"/> <paramref name="oldT"/> is to be replaced by.</param>
    public static void Replace<T>(this Span<T> source, T oldT, T newT) where T : IEquatable<T>
    {
        for (int i = 0; i < source.Length; i++)
        {
            if (source[i].Equals(oldT))
            {
                source[i] = newT;
            }
        }
    }
}
