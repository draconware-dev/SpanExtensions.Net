using System;
using System.Numerics;

namespace SpanExtensions
{
    public static partial class ReadOnlySpanExtensions
    {
#if NET7_0_OR_GREATER

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>
        /// <returns>The Sum of all the <typeparamref name="T"/>s in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static T Sum<T>(this ReadOnlySpan<T> source) where T : INumberBase<T>
        {
            T number = T.Zero;
            for(int i = 0; i < source.Length; i++)
            {
                checked { number += T.CreateChecked(source[i]); }
            }
            return number;
        }
#else

#if NET5_0_OR_GREATER

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Half}"/> to operate on.</param> 
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static Half Sum(this ReadOnlySpan<Half> source)
        {
            float number = 0;
            for(int i = 0; i < source.Length; i++)
            {
                number += (float)source[i];
            }
            return (Half)number;
        }

#endif

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Byte}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static byte Sum(this ReadOnlySpan<byte> source)
        {
            byte number = 0;
            for(int i = 0; i < source.Length; i++)
            {
                number += source[i];
            }
            return number;
        }

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{UInt16}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ushort Sum(this ReadOnlySpan<ushort> source)
        {
            ushort number = 0;
            for(int i = 0; i < source.Length; i++)
            {
                number += source[i];
            }
            return number;
        }

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{UInt32}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static uint Sum(this ReadOnlySpan<uint> source)
        {
            uint number = 0;
            for(int i = 0; i < source.Length; i++)
            {
                number += source[i];
            }
            return number;
        }

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{UInt64}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ulong Sum(this ReadOnlySpan<ulong> source)
        {
            ulong number = 0;
            for(int i = 0; i < source.Length; i++)
            {
                number += source[i];
            }
            return number;
        }

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{SByte}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static sbyte Sum(this ReadOnlySpan<sbyte> source)
        {
            sbyte number = 0;
            for(int i = 0; i < source.Length; i++)
            {
                number += source[i];
            }
            return number;
        }

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Int16}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static short Sum(this ReadOnlySpan<short> source)
        {
            short number = 0;
            for(int i = 0; i < source.Length; i++)
            {
                number += source[i];
            }
            return number;
        }

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Int32}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static int Sum(this ReadOnlySpan<int> source)
        {
            int number = 0;
            for(int i = 0; i < source.Length; i++)
            {
                number += source[i];
            }
            return number;
        }

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Int64}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static long Sum(this ReadOnlySpan<long> source)
        {
            long number = 0;
            for(int i = 0; i < source.Length; i++)
            {
                number += source[i];
            }
            return number;
        }

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Single}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static float Sum(this ReadOnlySpan<float> source)
        {
            float number = 0;
            for(int i = 0; i < source.Length; i++)
            {
                number += source[i];
            }
            return number;
        }

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Double}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static double Sum(this ReadOnlySpan<double> source)
        {
            double number = 0;
            for(int i = 0; i < source.Length; i++)
            {
                number += source[i];
            }
            return number;
        }

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Decimal}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static decimal Sum(this ReadOnlySpan<decimal> source)
        {
            decimal number = 0;
            for(int i = 0; i < source.Length; i++)
            {
                number += source[i];
            }
            return number;
        }

        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{BigInteger}"/> to operate on.</param>
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static BigInteger Sum(this ReadOnlySpan<BigInteger> source)
        {
            BigInteger number = 0;
            for(int i = 0; i < source.Length; i++)
            {
                number += source[i];
            }
            return number;
        }
#endif
    }
}