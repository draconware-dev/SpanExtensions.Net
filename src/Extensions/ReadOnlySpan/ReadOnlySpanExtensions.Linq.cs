using System.Numerics;

namespace SpanExtensions;

/// <summary>
/// Extension Methods for <see cref="ReadOnlySpan{T}"/>  
/// </summary>
public static partial class ReadOnlySpanExtensions
{

    /// <summary>
    /// Determines whether all elements in <paramref name="source"/> satisfy a condition.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>
    /// <param name="predicate">The Condition to be satisfied.</param>   
    /// <returns>A <see cref="bool"/> indicating whether or not every element in <paramref name="source"/> satisified the condition.</returns> 
    public static bool All<T>(this ReadOnlySpan<T> source, Predicate<T> predicate) where T : IEquatable<T>
    {
        for (int i = 0; i < source.Length; i++)
        {
            if (!predicate(source[i]))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Determines whether any element in <paramref name="source"/> satisfies a condition. 
    /// </summary>  
    /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>   
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>    
    /// <param name="predicate">The Condition to be satisfied.</param>  
    /// <returns>A <see cref="bool"/> indicating whether or not any elements satisified the condition.</returns> 
    public static bool Any<T>(this ReadOnlySpan<T> source, Predicate<T> predicate) where T : IEquatable<T>
    {
        for (int i = 0; i < source.Length; i++)
        {
            if (predicate(source[i]))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Computes the Sum of all the elements in <paramref name="source"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param> 
    /// <returns>The Sum of all the <typeparamref name="T"/>s in <paramref name="source"/>.</returns>
    public static T Sum<T>(this ReadOnlySpan<T> source) where T : INumber<T>
    {
        T number = T.Zero;
        for (int i = 0; i < source.Length; i++)
        {
            checked { number += T.CreateChecked(source[i]); }
        }
        return number;
    }

    /// <summary> 
    /// Bypasses a specified number of elements in <paramref name="source"/> and then returns the remaining elements.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>
    /// <param name="count">The Number of elements to bypass.</param>
    /// <returns>A <see cref="ReadOnlySpan{T}"/> that contains the elements that occur after <paramref name="count"/> elements have been bypassed in <paramref name="source"/>.</returns>
    public static ReadOnlySpan<T> Skip<T>(this ReadOnlySpan<T> source, int count)
    {
        return source[count..];
    }

    /// <summary> 
    /// Returns a specified number of contiguous elements from the start of a <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>
    /// <param name="count">The Number of elements to take.</param>
    /// <returns>A <see cref="ReadOnlySpan{T}"/> that contains <paramref name="count"/> elements from the start of the input sequence</returns>
    public static ReadOnlySpan<T> Take<T>(this ReadOnlySpan<T> source, int count) 
    {
        return source[..count];
    }

    /// <summary> 
    /// Returns a new <see cref="ReadOnlySpan{T}"/> that contains the elements from source with the last <paramref name="count"/> elements of the source collection omitted.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>
    /// <param name="count">The number of elements to omit from the end of <paramref name="source"/>.</param>
    /// <returns>A new <see cref="ReadOnlySpan{T}"/> that contains the elements from <paramref name="source"/> minus <paramref name="count"/> elements from the end of the collection.</returns>
    /// <remarks>If <paramref name="count"/> is not a positive number, this method returns <see cref="ReadOnlySpan{T}.Empty"/>.</remarks>
    public static ReadOnlySpan<T> SkipLast<T>(this ReadOnlySpan<T> source, int count)
    {
        if (count < 0)
        {
            return ReadOnlySpan<T>.Empty;
        }
        return source[..(source.Length - count)];
    }

    /// <summary> 
    /// Returns a new <see cref="ReadOnlySpan{T}"/> that contains the last <paramref name="count"/> elements from <paramref name="source"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam> 
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>
    /// <param name="count">The number of elements to take from the end of <paramref name="source"/>.</param>
    /// <returns>A new <see cref="ReadOnlySpan{T}"/> that contains the last <paramref name="count"/> elements from <paramref name="source"/>. </returns>
    /// <remarks>If <paramref name="count"/> is not a positive number, this method returns <see cref="ReadOnlySpan{T}.Empty"/>.</remarks>
    public static ReadOnlySpan<T> TakeLast<T>(this ReadOnlySpan<T> source, int count)
    {
        if(count < 0)
        {
            return ReadOnlySpan<T>.Empty;
        }
        return source[(source.Length - count)..];
    }

    /// <summary> 
    /// Computes the Average of all the values in <paramref name="source"/>. 
    /// </summary>  
    /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam> 
    /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>    
    /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
    public static T Average<T>(this ReadOnlySpan<T> source) where T : INumber<T>
    {
        T sum = source.Sum();
        return sum / T.CreateChecked(source.Length);
    }  
}  
