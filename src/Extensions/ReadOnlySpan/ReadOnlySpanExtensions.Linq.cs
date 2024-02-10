using System;
using System.Numerics;

namespace SpanExtensions
{
    /// <summary>
    /// Extension Methods for <see cref="ReadOnlySpan{T}"/>.
    /// </summary>
    public static partial class ReadOnlySpanExtensions
    {
        /// <summary>
        /// Determines whether all elements in <paramref name="source"/> satisfy a condition.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>
        /// <param name="predicate">The condition to be satisfied.</param>
        /// <returns>A <see cref="bool"/> indicating whether or not every element in <paramref name="source"/> satisified the condition.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        public static bool All<T>(this ReadOnlySpan<T> source, Predicate<T> predicate)
        {
            for(int i = 0; i < source.Length; i++)
            {
                if(!predicate(source[i]))
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
        /// <param name="predicate">The condition to be satisfied.</param>
        /// <returns>A <see cref="bool"/> indicating whether or not any elements satisified the condition.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        public static bool Any<T>(this ReadOnlySpan<T> source, Predicate<T> predicate)
        {
            for(int i = 0; i < source.Length; i++)
            {
                if(predicate(source[i]))
                {
                    return true;
                }
            }
            return false;
        }

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

        /// <summary>
        /// Bypasses a specified number of elements in <paramref name="source"/> and then returns the remaining elements.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>
        /// <param name="count">The Number of elements to bypass.</param>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> that contains the elements that occur after <paramref name="count"/> elements have been bypassed in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
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
        /// <returns>A <see cref="ReadOnlySpan{T}"/> that contains <paramref name="count"/> elements from the start of <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ReadOnlySpan<T> Take<T>(this ReadOnlySpan<T> source, int count)
        {
            return source[..count];
        }

        /// <summary>
        /// Bypasses elements in <paramref name="source"/> as long as a <paramref name="condition"/> is true and then returns the remaining elements. The element's index is used in the logic of the predicate function.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>
        /// <param name="condition">A function to test each element for a condition.</param>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> that contains the elements from <paramref name="source"/> starting at the first element in the linear series that does not pass the specified <paramref name="condition" />.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="condition"/> is null.</exception>
        public static ReadOnlySpan<T> SkipWhile<T>(this ReadOnlySpan<T> source, Predicate<T> condition)
        {
            int count = 0;
            while(count < source.Length)
            {
                T t = source[count];
                if(!condition(t))
                {
                    return source.Skip(count);
                }
                count++;
            }
            return ReadOnlySpan<T>.Empty;
        }

        ///  <summary>
        /// Returns elements from <paramref name="source"/> as long as a specified <paramref name="condition"/> is true, and then skips the remaining elements.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>
        /// <param name="condition">A function to test each element for a condition.</param>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> that contains elements from <paramref name="source"/> that occur before the element at which the <paramref name="condition"/> no longer passes.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="condition"/> is null.</exception>
        public static ReadOnlySpan<T> TakeWhile<T>(this ReadOnlySpan<T> source, Predicate<T> condition)
        {
            int count = 0;
            while(count < source.Length)
            {
                T t = source[count];
                if(!condition(t))
                {
                    return source.Take(count);
                }
                count++;
            }
            return ReadOnlySpan<T>.Empty;
        }

        /// <summary>
        /// Returns a new <see cref="ReadOnlySpan{T}"/> that contains the elements from source with the last <paramref name="count"/> elements of the source collection omitted.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>
        /// <param name="count">The number of elements to omit from the end of <paramref name="source"/>.</param>
        /// <returns>A new <see cref="ReadOnlySpan{T}"/> that contains the elements from <paramref name="source"/> minus <paramref name="count"/> elements from the end of the collection.</returns>
        /// <remarks>If <paramref name="count"/> is not a positive number, this method returns <see cref="ReadOnlySpan{T}.Empty"/>.</remarks>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ReadOnlySpan<T> SkipLast<T>(this ReadOnlySpan<T> source, int count)
        {
            if(count < 0)
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
        /// <returns>A new <see cref="ReadOnlySpan{T}"/> that contains the last <paramref name="count"/> elements from <paramref name="source"/>.</returns>
        /// <remarks>If <paramref name="count"/> is not a positive number, this method returns <see cref="ReadOnlySpan{T}.Empty"/>.</remarks>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ReadOnlySpan<T> TakeLast<T>(this ReadOnlySpan<T> source, int count)
        {
            if(count < 0)
            {
                return ReadOnlySpan<T>.Empty;
            }
            return source[(source.Length - count)..];
        }

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