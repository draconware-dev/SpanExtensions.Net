using System;
using System.Numerics;

namespace SpanExtensions
{
    public static partial class ReadOnlySpanExtensions
    {

#if NET7_0_OR_GREATER

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static T Average<T>(this ReadOnlySpan<T> source) where T : INumberBase<T>
        {
            T sum = source.Sum();
            return sum / T.CreateChecked(source.Length);
        }
#else

#if NET5_0_OR_GREATER

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Half}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static Half Average(this ReadOnlySpan<Half> source)
        {
            Half sum = source.Sum();
            return (Half)((float)sum / source.Length);
        }
#endif

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Byte}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static byte Average(this ReadOnlySpan<byte> source)
        {
            byte sum = source.Sum();
            return (byte)(sum / source.Length);
        }

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{UInt16}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ushort Average(this ReadOnlySpan<ushort> source)
        {
            ushort sum = source.Sum();
            return (ushort)(sum / source.Length);
        }

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{uint32}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static uint Average(this ReadOnlySpan<uint> source)
        {
            uint sum = source.Sum();
            return (uint)(sum / source.Length);
        }

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{UInt64}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ulong Average(this ReadOnlySpan<ulong> source)
        {
            ulong sum = source.Sum();
            return sum / (ulong)source.Length;
        }

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{SByte}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static sbyte Average(this ReadOnlySpan<sbyte> source)
        {
            sbyte sum = source.Sum();
            return (sbyte)(sum / source.Length);
        }

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Int16}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static short Average(this ReadOnlySpan<short> source)
        {
            short sum = source.Sum();
            return (short)(sum / source.Length);
        }

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Int32}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static int Average(this ReadOnlySpan<int> source)
        {
            int sum = source.Sum();
            return sum / source.Length;
        }

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Int64}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static long Average(this ReadOnlySpan<long> source)
        {
            long sum = source.Sum();
            return (sum / source.Length);
        }

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Single}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static float Average(this ReadOnlySpan<float> source)
        {
            float sum = source.Sum();
            return (float)(sum / source.Length);
        }

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Double}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static double Average(this ReadOnlySpan<double> source)
        {
            double sum = source.Sum();
            return (double)(sum / source.Length);
        }

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Int64}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static decimal Average(this ReadOnlySpan<decimal> source)
        {
            decimal sum = source.Sum();
            return sum / source.Length;
        }

        /// <summary>
        /// Computes the Average of all the values in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{BigInteger}"/> to operate on.</param>
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static BigInteger Average(this ReadOnlySpan<BigInteger> source)
        {
            BigInteger sum = source.Sum();
            return sum / source.Length;
        }
#endif
    }
}