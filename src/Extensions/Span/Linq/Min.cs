using System;
using System.Numerics;

namespace SpanExtensions
{
    public static partial class SpanExtensions  
    {
#if NET7_0_OR_GREATER

        /// <summary>
        /// Computes the Min of all the values in <paramref name="source"/>. 
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam> 
        /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>    
        /// <returns>The Min out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static T Min<T>(this Span<T> source) where T : INumber<T>
        {
            return ReadOnlySpanExtensions.Min<T>(source);
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="selector"/>.</typeparam>
        /// <param name="source">A <see cref="Span{TSource}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static TResult Min<TSource, TResult>(this Span<TSource> source, Func<TSource, TResult> selector) where TResult : IComparable<TResult>
        {
            return ReadOnlySpanExtensions.Min(source, selector);
        }
#else

#if NET5_0_OR_GREATER

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{Half}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static Half Min(this Span<Half> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }
#endif

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{Byte}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static byte Min(this Span<byte> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{UInt16}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static ushort Min(this Span<ushort> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{UInt32}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static uint Min(this Span<uint> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }
        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{UInt64}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static ulong Min(this Span<ulong> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{SByte}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static sbyte Min(this Span<sbyte> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{Int16}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static short Min(this Span<short> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{Int32}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static int Min(this Span<int> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{Int64}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static long Min(this Span<long> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{Single}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static float Min(this Span<float> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{Double}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static double Min(this Span<double> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{Decimal}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static decimal Min(this Span<decimal> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="Span{BigInteger}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static BigInteger Min(this Span<BigInteger> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }
        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static byte Min<T>(this Span<T> source, Func<T, byte> selector)
        {
            return ReadOnlySpanExtensions.Min(source, selector);
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static ushort Min<T>(this Span<T> source, Func<T, ushort> selector)
        {
            return ReadOnlySpanExtensions.Min(source, selector);
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static uint Min<T>(this Span<T> source, Func<T, uint> selector)
        {
            return ReadOnlySpanExtensions.Min(source, selector);
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static ulong Min<T>(this Span<T> source, Func<T, ulong> selector)
        {
            return ReadOnlySpanExtensions.Min(source, selector);
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static sbyte Min<T>(this Span<T> source, Func<T, sbyte> selector)
        {
            return ReadOnlySpanExtensions.Min(source, selector);
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static short Min<T>(this Span<T> source, Func<T, short> selector)
        {
            return ReadOnlySpanExtensions.Min(source, selector);
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static int Min<T>(this Span<T> source, Func<T, int> selector)
        {
            return ReadOnlySpanExtensions.Min(source, selector);
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static long Min<T>(this Span<T> source, Func<T, long> selector)
        {
            return ReadOnlySpanExtensions.Min(source, selector);
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static float Min<T>(this Span<T> source, Func<T, float> selector)
        {
            return ReadOnlySpanExtensions.Min(source, selector);
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static double Min<T>(this Span<T> source, Func<T, double> selector)
        {
            return ReadOnlySpanExtensions.Min(source, selector);
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static decimal Min<T>(this Span<T> source, Func<T, decimal> selector)
        {
            return ReadOnlySpanExtensions.Min(source, selector);
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="Span{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static BigInteger Min<T>(this Span<T> source, Func<T, BigInteger> selector)
        {
            return ReadOnlySpanExtensions.Min(source, selector);
        }

#endif
    }
}