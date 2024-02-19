using System;
using System.Numerics;

namespace SpanExtensions
{
    public static partial class SpanExtensions
    {
#if NET7_0_OR_GREATER

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>
        /// <returns>The Sum of all the <typeparamref name="T"/>s in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static T Sum<T>(this Span<T> source) where T : INumber<T>
        {
            return ReadOnlySpanExtensions.Sum<T>(source);
        }
#else

#if NET5_0_OR_GREATER

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{Half}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static Half Sum(this Span<Half> source)
        {
            return ReadOnlySpanExtensions.Sum(source);
        }
#endif

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{Byte}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static byte Sum(this Span<byte> source)
        {
            return ReadOnlySpanExtensions.Sum(source);
        }

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{UInt16}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ushort Sum(this Span<ushort> source)
        {
            return ReadOnlySpanExtensions.Sum(source);
        }

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{UInt32}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static uint Sum(this Span<uint> source)
        {
            return ReadOnlySpanExtensions.Sum(source);
        }
        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{UInt64}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ulong Sum(this Span<ulong> source)
        {
            return ReadOnlySpanExtensions.Sum(source);
        }

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{SByte}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static sbyte Sum(this Span<sbyte> source)
        {
            return ReadOnlySpanExtensions.Sum(source);
        }

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{Int16}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static short Sum(this Span<short> source)
        {
            return ReadOnlySpanExtensions.Sum(source);
        }

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{Int32}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static int Sum(this Span<int> source)
        {
            return ReadOnlySpanExtensions.Sum(source);
        }

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{Int64}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static long Sum(this Span<long> source)
        {
            return ReadOnlySpanExtensions.Sum(source);
        }

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{Single}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static float Sum(this Span<float> source)
        {
            return ReadOnlySpanExtensions.Sum(source);
        }

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{Double}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static double Sum(this Span<double> source)
        {
            return ReadOnlySpanExtensions.Sum(source);
        }

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{Decimal}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static decimal Sum(this Span<decimal> source)
        {
            return ReadOnlySpanExtensions.Sum(source);
        }

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="Span{BigInteger}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static BigInteger Sum(this Span<BigInteger> source)
        {
            return ReadOnlySpanExtensions.Sum(source);
        }
#endif
    }
}