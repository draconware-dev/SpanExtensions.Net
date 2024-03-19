using System;
using System.Numerics;

namespace SpanExtensions
{
    public static partial class ReadOnlySpanExtensions
    {
#if NET7_0_OR_GREATER

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static T Min<T>(this ReadOnlySpan<T> source) where T : IComparable<T>
        {
            T min = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                T current = source[i];
                if(current.CompareTo(min) < 0)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in <paramref name="source"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="selector"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{TSource}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static TResult Min<TSource, TResult>(this ReadOnlySpan<TSource> source, Func<TSource, TResult> selector) where TResult : IComparable<TResult>
        {
            ArgumentNullException.ThrowIfNull(selector);

            TSource first = source[0];
            TResult min = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                TSource value = source[i];
                TResult current = selector(value);
                if(current.CompareTo(min) < 0)
                {
                    min = current;
                }
            }
            return min;
        }
#else

#if NET5_0_OR_GREATER

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{Half}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static Half Min(this ReadOnlySpan<Half> source)
        {
            Half min = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                Half current = source[i];
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static Half Min<T>(this ReadOnlySpan<T> source, Func<T, Half> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            Half min = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                Half current = selector(value);
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }
#endif

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{Byte}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static byte Min(this ReadOnlySpan<byte> source)
        {
            byte min = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                byte current = source[i];
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{UInt16}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static ushort Min(this ReadOnlySpan<ushort> source)
        {
            ushort min = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                ushort current = source[i];
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{UInt32}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static uint Min(this ReadOnlySpan<uint> source)
        {
            uint min = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                uint current = source[i];
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{UInt64}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static ulong Min(this ReadOnlySpan<ulong> source)
        {
            ulong min = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                ulong current = source[i];
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{SByte}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static sbyte Min(this ReadOnlySpan<sbyte> source)
        {
            sbyte min = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                sbyte current = source[i];
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{Int16}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static short Min(this ReadOnlySpan<short> source)
        {
            short min = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                short current = source[i];
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{Int32}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static int Min(this ReadOnlySpan<int> source)
        {
            int min = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                int current = source[i];
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{Int64}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static long Min(this ReadOnlySpan<long> source)
        {
            long min = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                long current = source[i];
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{Single}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static float Min(this ReadOnlySpan<float> source)
        {
            float min = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                float current = source[i];
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{Double}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static double Min(this ReadOnlySpan<double> source)
        {
            double min = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                double current = source[i];
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{Decimal}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static decimal Min(this ReadOnlySpan<decimal> source)
        {
            decimal min = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                decimal current = source[i];
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{BigInteger}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static BigInteger Min(this ReadOnlySpan<BigInteger> source)
        {
            BigInteger min = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                BigInteger current = source[i];
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static byte Min<T>(this ReadOnlySpan<T> source, Func<T, byte> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            byte min = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                byte current = selector(value);
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static ushort Min<T>(this ReadOnlySpan<T> source, Func<T, ushort> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            ushort min = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                ushort current = selector(value);
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static uint Min<T>(this ReadOnlySpan<T> source, Func<T, uint> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            uint min = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                uint current = selector(value);
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static ulong Min<T>(this ReadOnlySpan<T> source, Func<T, ulong> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            ulong min = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                ulong current = selector(value);
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static sbyte Min<T>(this ReadOnlySpan<T> source, Func<T, sbyte> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            sbyte min = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                sbyte current = selector(value);
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static short Min<T>(this ReadOnlySpan<T> source, Func<T, short> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            short min = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                short current = selector(value);
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static int Min<T>(this ReadOnlySpan<T> source, Func<T, int> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            int min = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                int current = selector(value);
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static long Min<T>(this ReadOnlySpan<T> source, Func<T, long> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            long min = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                long current = selector(value);
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static float Min<T>(this ReadOnlySpan<T> source, Func<T, float> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            float min = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                float current = selector(value);
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static double Min<T>(this ReadOnlySpan<T> source, Func<T, double> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            double min = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                double current = selector(value);
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static decimal Min<T>(this ReadOnlySpan<T> source, Func<T, decimal> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            decimal min = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                decimal current = selector(value);
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }

        /// <summary>
        /// Invokes a transform function on each element in <paramref name="source"/> and returns the minimum resulting value.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="selector"/> is null.</exception>
        public static BigInteger Min<T>(this ReadOnlySpan<T> source, Func<T, BigInteger> selector)
        {
            if(selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T first = source[0];
            BigInteger min = selector(first);
            for(int i = 1; i < source.Length; i++)
            {
                T value = source[i];
                BigInteger current = selector(value);
                if(current < min)
                {
                    min = current;
                }
            }
            return min;
        }
#endif
    }
}