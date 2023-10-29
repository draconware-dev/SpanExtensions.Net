using System.Numerics;

namespace SpanExtensions;

/// <summary>
/// Extension Methods for <see cref="Span{T}"/>  
/// </summary>
public static partial class SpanExtensions
{

    /// <summary>
    /// Determines whether all elements in <paramref name="source"/> satisfy a condition.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="Span{T}"/>.</typeparam>
    /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>
    /// <param name="predicate">The Condition to be satisfied.</param>   
    /// <returns>A <see cref="bool"/> indicating whether or not every element in <paramref name="source"/> satisified the condition.</returns> 
    public static bool All<T>(this Span<T> source, Predicate<T> predicate) where T : IEquatable<T>
    {
        return ReadOnlySpanExtensions.All(source, predicate);
    }

    /// <summary>
    /// Determines whether any element in <paramref name="source"/> satisfies a condition. 
    /// </summary>  
    /// <typeparam name="T">The type of elements in the <see cref="Span{T}"/>.</typeparam>   
    /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>    
    /// <param name="predicate">The Condition to be satisfied.</param>  
    /// <returns>A <see cref="bool"/> indicating whether or not any elements satisified the condition.</returns> 
    public static bool Any<T>(this Span<T> source, Predicate<T> predicate) where T : IEquatable<T>
    {
        return ReadOnlySpanExtensions.Any(source, predicate);
    }

    /// <summary>
    /// Computes the Sum of all the elements in <paramref name="source"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
    /// <param name="source">The <see cref="Span{T}"/> to operate on.</param> 
    /// <returns>The Sum of all the <typeparamref name="T"/>s in <paramref name="source"/>.</returns>
    public static T Sum<T>(this Span<T> source) where T : INumber<T>
    {
        return ReadOnlySpanExtensions.Sum<T>(source);
    }

    /// <summary> 
    /// Bypasses a specified number of elements in <paramref name="source"/> and then returns the remaining elements.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="Span{T}"/>.</typeparam>
    /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>
    /// <param name="count">The Number of elements to bypass.</param>
    /// <returns>A <see cref="Span{T}"/> that contains the elements that occur after <paramref name="count"/> elements have been bypassed in <paramref name="source"/>.</returns>
    public static Span<T> Skip<T>(this Span<T> source, int count)
    {
        return source[count..];
    }

    /// <summary> 
    /// Returns a specified number of contiguous elements from the start of a <see cref="Span{T}"/>.
    /// </summary> 
    /// <typeparam name="T">The type of elements in the <see cref="Span{T}"/>.</typeparam>
    /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>  
    /// <param name="count">The Number of elements to take.</param> 
    /// <returns>A <see cref="Span{T}"/> that contains <paramref name="count"/> elements from the start of the input sequence</returns>
    public static Span<T> Take<T>(this Span<T> source, int count)
    {
        return source[..count];
    }

    /// <summary> 
    /// Returns a new <see cref="Span{T}"/> that contains the elements from source with the last <paramref name="count"/> elements of the source collection omitted.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="Span{T}"/>.</typeparam>
    /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>
    /// <param name="count">The number of elements to omit from the end of <paramref name="source"/>.</param>
    /// <returns>A new <see cref="Span{T}"/> that contains the elements from <paramref name="source"/> minus <paramref name="count"/> elements from the end of the collection.</returns>
    /// <remarks>If <paramref name="count"/> is not a positive number, this method returns <see cref="Span{T}.Empty"/>.</remarks>
    public static Span<T> SkipLast<T>(this Span<T> source, int count)
    {
        if (count < 0)
        {
            return Span<T>.Empty;
        }
        return source[..(source.Length - count)];
    }

    /// <summary> 
    /// Returns a new <see cref="Span{T}"/> that contains the last <paramref name="count"/> elements from <paramref name="source"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="Span{T}"/>.</typeparam> 
    /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>
    /// <param name="count">The number of elements to take from the end of <paramref name="source"/>.</param>
    /// <returns>A new <see cref="Span{T}"/> that contains the last <paramref name="count"/> elements from <paramref name="source"/>. </returns>
    /// <remarks>If <paramref name="count"/> is not a positive number, this method returns <see cref="Span{T}.Empty"/>.</remarks>
    public static Span<T> TakeLast<T>(this Span<T> source, int count)
    {
        if (count < 0)
        {
            return Span<T>.Empty;
        }
        return source[(source.Length - count)..];
    }

    /// <summary> 
    /// Computes the Average of all the values in <paramref name="source"/>. 
    /// </summary>  
    /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam> 
    /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>    
    /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
    public static T Average<T>(this Span<T> source) where T : INumber<T>
    {
        T sum = source.Sum();
        return sum / T.CreateChecked(source.Length);
    }
}
