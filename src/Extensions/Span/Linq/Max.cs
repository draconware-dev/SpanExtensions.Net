using System;
using System.Numerics;

namespace SpanExtensions
{
    public static partial class SpanExtensions
    {
#if NET7_0_OR_GREATER

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static T Max<T>(this Span<T> source) where T : INumber<T>
        {
            return ReadOnlySpanExtensions.Max<T>(source);
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in <paramref name="source"/>.</typeparam> 
        /// <typeparam name="TResult">The type of the value returned by <paramref name="selector"/>.</typeparam>
        /// <param name="source">A <see cref="Span{TSource}"/> to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static TResult Max<TSource, TResult>(this Span<TSource> source, Func<TSource, TResult> selector) where TResult : IComparable<TResult>
        {
            return ReadOnlySpanExtensions.Max(source, selector);
        }
#else

#if NET5_0_OR_GREATER

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{Half}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static Half Max(this Span<Half> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static Half Max<T>(this Span<T> source, Func<T, Half> selector)
        {
            return ReadOnlySpanExtensions.Max(source, selector);
        }
#endif

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{Byte}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static byte Max(this Span<byte> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{UInt16}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static ushort Max(this Span<ushort> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{UInt32}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static uint Max(this Span<uint> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{UInt64}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static ulong Max(this Span<ulong> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{SByte}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static sbyte Max(this Span<sbyte> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{Int16}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static short Max(this Span<short> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{Int32}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static int Max(this Span<int> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{Int64}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static long Max(this Span<long> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{Single}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static float Max(this Span<float> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{Double}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static double Max(this Span<double> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{Decimal}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static decimal Max(this Span<decimal> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{BigInteger}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static BigInteger Max(this Span<BigInteger> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static byte Max<T>(this Span<T> source, Func<T, byte> selector)
        {
            return ReadOnlySpanExtensions.Max(source, selector);
        }

        /// <summary> 
        /// Invokes a transform function on each element of a generic sequence and returns the maximum resulting value.
        /// </summary>  
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static ushort Max<T>(this Span<T> source, Func<T, ushort> selector)
        {
            return ReadOnlySpanExtensions.Max(source, selector);
        }

        /// <summary> 
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>  
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static uint Max<T>(this Span<T> source, Func<T, uint> selector)
        {
            return ReadOnlySpanExtensions.Max(source, selector);
        }

        /// <summary> 
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>  
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static ulong Max<T>(this Span<T> source, Func<T, ulong> selector)
        {
            return ReadOnlySpanExtensions.Max(source, selector);
        }

        /// <summary> 
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>  
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static sbyte Max<T>(this Span<T> source, Func<T, sbyte> selector)
        {
            return ReadOnlySpanExtensions.Max(source, selector);
        }

        /// <summary> 
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>  
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static short Max<T>(this Span<T> source, Func<T, short> selector)
        {
            return ReadOnlySpanExtensions.Max(source, selector);
        }

        /// <summary> 
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>  
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static int Max<T>(this Span<T> source, Func<T, int> selector)
        {
            return ReadOnlySpanExtensions.Max(source, selector);
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>  
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static long Max<T>(this Span<T> source, Func<T, long> selector)
        {
            return ReadOnlySpanExtensions.Max(source, selector);
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam> 
        /// <param name="source">A <see cref="Span{T}"/> to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static float Max<T>(this Span<T> source, Func<T, float> selector)
        {
            return ReadOnlySpanExtensions.Max(source, selector);
        }

        /// <summary> 
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>  
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam> 
        /// <param name="source">A <see cref="Span{T}"/> to determine the maximum value of.</param>    
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static double Max<T>(this Span<T> source, Func<T, double> selector)
        {
            return ReadOnlySpanExtensions.Max(source, selector);
        }

        /// <summary> 
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>  
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam> 
        /// <param name="source">A <see cref="Span{T}"/> to determine the maximum value of.</param>    
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static decimal Max<T>(this Span<T> source, Func<T, decimal> selector)
        {
            return ReadOnlySpanExtensions.Max(source, selector);
        }

        /// <summary> 
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>  
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam> 
        /// <param name="source">A <see cref="Span{T}"/> to determine the maximum value of.</param>    
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static BigInteger Max<T>(this Span<T> source, Func<T, BigInteger> selector)
        {
            return ReadOnlySpanExtensions.Max(source, selector);
        }
#endif
    }
}