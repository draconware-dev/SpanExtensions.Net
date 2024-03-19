using System;

namespace SpanExtensions
{
    public static partial class SpanExtensions
    {

        /// <summary>
        /// Returns the element at a specified index in <paramref name="source"/> or a default value if the index is out of range.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to return the last element of.</param> 
        /// <param name="index">The zero-based index of the element to retrieve.</param> 
        /// <param name="defaultValue">The default value to return if <paramref name="source"/> is empty.</param>
        /// <returns>The element at the specified position in <paramref name="source"/>. </returns>
        public static T ElementAtOrDefault<T>(this Span<T> source, int index, T defaultValue)
        {
            return ReadOnlySpanExtensions.ElementAtOrDefault(source, index, defaultValue);
        }

        /// <summary>
        /// Returns the element at a specified index in <paramref name="source"/> or a default value if the index is out of range.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to return the last element of.</param>
        /// <param name="index">The index of the element to retrieve, which is either from the beginning or the end of the sequence.</param>
        /// <param name="defaultValue">The default value to return if <paramref name="source"/> is empty.</param>
        /// <returns>The element at the specified position in <paramref name="source"/>.</returns>
        public static T ElementAtOrDefault<T>(this Span<T> source, Index index, T defaultValue)
        {
            return ReadOnlySpanExtensions.ElementAtOrDefault(source, index, defaultValue);
        }

#if NETCOREAPP1_0_OR_GREATER
        /// <summary>
        /// Returns the element at a specified index in <paramref name="source"/> or a default value if the index is out of range.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to return the last element of.</param> 
        /// <param name="index">The zero-based index of the element to retrieve.</param> 
        /// <returns>The element at the specified position in <paramref name="source"/>. </returns>
        public static T? ElementAtOrDefault<T>(this Span<T> source, int index)
        {
            return ReadOnlySpanExtensions.ElementAtOrDefault<T>(source, index);
        }

        /// <summary>
        /// Returns the element at a specified index in <paramref name="source"/> or a default value if the index is out of range.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to return the last element of.</param>
        /// <param name="index">The index of the element to retrieve, which is either from the beginning or the end of the sequence.</param>
        public static T? ElementAtOrDefault<T>(this Span<T> source, Index index)
        {
            return ReadOnlySpanExtensions.ElementAtOrDefault<T>(source, index);
        }
#elif NETSTANDARD2_1
#pragma warning disable CS8603 // Possible null reference return.
        /// <summary>
        /// Returns the element at a specified index in <paramref name="source"/> or a default value if the index is out of range.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to return the last element of.</param> 
        /// <param name="index">The zero-based index of the element to retrieve.</param> 
        /// <returns>The element at the specified position in <paramref name="source"/>. </returns>
        public static T ElementAtOrDefault<T>(this Span<T> source, int index)
        {
            return ReadOnlySpanExtensions.ElementAtOrDefault<T>(source, index);
        }

        /// <summary>
        /// Returns the element at a specified index in <paramref name="source"/> or a default value if the index is out of range.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to return the last element of.</param>
        /// <param name="index">The index of the element to retrieve, which is either from the beginning or the end of the sequence.</param>
        /// <returns>The element at the specified position in <paramref name="source"/>.</returns>
        public static T ElementAtOrDefault<T>(this Span<T> source, Index index)
        {
            return ReadOnlySpanExtensions.ElementAtOrDefault<T>(source, index);
        }
#pragma warning restore CS8603 // Possible null reference return.
#endif
    }
}
