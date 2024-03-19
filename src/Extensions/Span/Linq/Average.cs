using System;
using System.Numerics;

namespace SpanExtensions
{
    public static partial class SpanExtensions
    {
#if NET7_0_OR_GREATER

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static T Average<T>(this Span<T> source) where T : INumber<T>
        {
            T sum = source.Sum();
            return sum / T.CreateChecked(source.Length);
        }
#else

#if NET5_0_OR_GREATER

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{Half}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static Half Average(this Span<Half> source)
        {
            return ReadOnlySpanExtensions.Average(source);
        }
#endif

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{Byte}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static byte Average(this Span<byte> source)
        {
            return ReadOnlySpanExtensions.Average(source);
        }

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{UInt16}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ushort Average(this Span<ushort> source)
        {
            return ReadOnlySpanExtensions.Average(source);
        }

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{uint32}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static uint Average(this Span<uint> source)
        {
            return ReadOnlySpanExtensions.Average(source);
        }
        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{UInt64}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ulong Average(this Span<ulong> source)
        {
            return ReadOnlySpanExtensions.Average(source);
        }

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{SByte}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static sbyte Average(this Span<sbyte> source)
        {
            return ReadOnlySpanExtensions.Average(source);
        }

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{Int16}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static short Average(this Span<short> source)
        {
            return ReadOnlySpanExtensions.Average(source);
        }

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{Int32}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static int Average(this Span<int> source)
        {
            return ReadOnlySpanExtensions.Average(source);
        }

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{Int64}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static long Average(this Span<long> source)
        {
            return ReadOnlySpanExtensions.Average(source);
        }

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{Single}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static float Average(this Span<float> source)
        {
            return ReadOnlySpanExtensions.Average(source);
        }

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{Double}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static double Average(this Span<double> source)
        {
            return ReadOnlySpanExtensions.Average(source);
        }

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{Decimal}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static decimal Average(this Span<decimal> source)
        {
            return ReadOnlySpanExtensions.Average(source);
        }

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{BigInteger}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static BigInteger Average(this Span<BigInteger> source)
        {
            return ReadOnlySpanExtensions.Average(source);
        }
#endif
    }
}