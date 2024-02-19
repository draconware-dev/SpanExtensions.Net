using System;
using System.Numerics;

namespace SpanExtensions
{
    public static partial class ReadOnlySpanExtensions
    {
#if NET7_0_OR_GREATER

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static T Max<T>(this ReadOnlySpan<T> source) where T : IComparable<T>
        {
            T max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                T current = source[i];
                if(current.CompareTo(max) > 0)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in <paramref name="source"/>.</typeparam> 
        /// <typeparam name="TResult">The type of the value returned by <paramref name="selector"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{TSource}"/> to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static TResult Max<TSource, TResult>(this ReadOnlySpan<TSource> source, Func<TSource, TResult> selector) where TResult : IComparable<TResult>
        {
            ArgumentNullException.ThrowIfNull(selector);

            TSource first = source[0];
            TResult max = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                TSource value = source[i];
                TResult current = selector(value);
                if(current.CompareTo(max) > 0)
                {
                    max = current;
                }
            }
            return max;
        }
#else

#if NET5_0_OR_GREATER

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{Half}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static Half Max(this ReadOnlySpan<Half> source)
        {
            Half max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                Half current = source[i];
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static Half Max<T>(this ReadOnlySpan<T> source, Func<T, Half> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            Half max = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                Half current = selector(value);
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }
#endif

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{Byte}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static byte Max(this ReadOnlySpan<byte> source)
        {
            byte max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                byte current = source[i];
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{UInt16}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static ushort Max(this ReadOnlySpan<ushort> source)
        {
            ushort max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                ushort current = source[i];
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{UInt32}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static uint Max(this ReadOnlySpan<uint> source)
        {
            uint max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                uint current = source[i];
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{UInt64}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static ulong Max(this ReadOnlySpan<ulong> source)
        {
            ulong max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                ulong current = source[i];
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{SByte}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static sbyte Max(this ReadOnlySpan<sbyte> source)
        {
            sbyte max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                sbyte current = source[i];
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{Int16}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static short Max(this ReadOnlySpan<short> source)
        {
            short max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                short current = source[i];
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{Int32}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static int Max(this ReadOnlySpan<int> source)
        {
            int max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                int current = source[i];
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{Int64}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static long Max(this ReadOnlySpan<long> source)
        {
            long max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                long current = source[i];
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{Single}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static float Max(this ReadOnlySpan<float> source)
        {
            float max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                float current = source[i];
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{Double}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static double Max(this ReadOnlySpan<double> source)
        {
            double max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                double current = source[i];
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{Decimal}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static decimal Max(this ReadOnlySpan<decimal> source)
        {
            decimal max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                decimal current = source[i];
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{BigInteger}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static BigInteger Max(this ReadOnlySpan<BigInteger> source)
        {
            BigInteger max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                BigInteger current = source[i];
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static byte Max<T>(this ReadOnlySpan<T> source, Func<T, byte> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            byte max = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                byte current = selector(value);
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Invokes a transform function on each element of a generic sequence and returns the maximum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static ushort Max<T>(this ReadOnlySpan<T> source, Func<T, ushort> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            ushort max = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                ushort current = selector(value);
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static uint Max<T>(this ReadOnlySpan<T> source, Func<T, uint> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            uint max = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                uint current = selector(value);
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static ulong Max<T>(this ReadOnlySpan<T> source, Func<T, ulong> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            ulong max = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                ulong current = selector(value);
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static sbyte Max<T>(this ReadOnlySpan<T> source, Func<T, sbyte> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            sbyte max = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                sbyte current = selector(value);
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static short Max<T>(this ReadOnlySpan<T> source, Func<T, short> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            short max = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                short current = selector(value);
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static int Max<T>(this ReadOnlySpan<T> source, Func<T, int> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            int max = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                int current = selector(value);
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static long Max<T>(this ReadOnlySpan<T> source, Func<T, long> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            long max = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                long current = selector(value);
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam> 
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static float Max<T>(this ReadOnlySpan<T> source, Func<T, float> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            float max = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                float current = selector(value);
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam> 
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the maximum value of.</param>    
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static double Max<T>(this ReadOnlySpan<T> source, Func<T, double> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            double max = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                double current = selector(value);
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam> 
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the maximum value of.</param>    
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static decimal Max<T>(this ReadOnlySpan<T> source, Func<T, decimal> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            decimal max = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                decimal current = selector(value);
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the maximum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam> 
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the maximum value of.</param>    
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static BigInteger Max<T>(this ReadOnlySpan<T> source, Func<T, BigInteger> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            BigInteger max = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                BigInteger current = selector(value);
                if(current > max)
                {
                    max = current;
                }
            }
            return max;
        }
#endif
    }
}