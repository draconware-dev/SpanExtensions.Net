using System;
using System.Numerics;

namespace SpanExtensions
{
    /// <summary>
    /// Extension Methods for <see cref="ReadOnlySpan{T}"/>  
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
        public static bool All<T>(this ReadOnlySpan<T> source, Predicate<T> predicate) where T : IEquatable<T>
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
        public static bool Any<T>(this ReadOnlySpan<T> source, Predicate<T> predicate) where T : IEquatable<T>
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
        public static T Sum<T>(this ReadOnlySpan<T> source) where T : INumber<T>
        {
            T number = T.Zero;
            for(int i = 0; i < source.Length; i++)
            {
                checked { number += T.CreateChecked(source[i]); }
            }
            return number;
        }
#else
        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Byte}"/> to operate on.</param> 
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
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

#if NET5_0_OR_GREATER   
  
        /// <summary>
        /// Computes the Sum of all the elements in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The <see cref="ReadOnlySpan{Half}"/> to operate on.</param> 
        /// <returns>The Sum of all the elements in <paramref name="source"/>.</returns>
        public static Half Sum(this ReadOnlySpan<Half> source)
        {
            float number = 0;
            for(int i = 0; i < source.Length; i++)
            {
                number += (float) source[i];
            }
            return (Half)number;
        }
#endif

        /// <summary> 
        /// Bypasses a specified number of elements in <paramref name="source"/> and then returns the remaining elements.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>
        /// <param name="count">The Number of elements to bypass.</param>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> that contains the elements that occur after <paramref name="count"/> elements have been bypassed in <paramref name="source"/>.</returns>
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
        /// <returns>A <see cref="ReadOnlySpan{T}"/> that contains <paramref name="count"/> elements from the start of the input sequence</returns>
        public static ReadOnlySpan<T> Take<T>(this ReadOnlySpan<T> source, int count)
        {
            return source[..count];
        }

        /// <summary> 
        /// Returns a new <see cref="ReadOnlySpan{T}"/> that contains the elements from source with the last <paramref name="count"/> elements of the source collection omitted.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>
        /// <param name="count">The number of elements to omit from the end of <paramref name="source"/>.</param>
        /// <returns>A new <see cref="ReadOnlySpan{T}"/> that contains the elements from <paramref name="source"/> minus <paramref name="count"/> elements from the end of the collection.</returns>
        /// <remarks>If <paramref name="count"/> is not a positive number, this method returns <see cref="ReadOnlySpan{T}.Empty"/>.</remarks>
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
        /// <returns>A new <see cref="ReadOnlySpan{T}"/> that contains the last <paramref name="count"/> elements from <paramref name="source"/>. </returns>
        /// <remarks>If <paramref name="count"/> is not a positive number, this method returns <see cref="ReadOnlySpan{T}.Empty"/>.</remarks>
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
        public static T Average<T>(this ReadOnlySpan<T> source) where T : INumber<T>
        {
            T sum = source.Sum();
            return sum / T.CreateChecked(source.Length);
        }
#else
        /// <summary> 
        /// Computes the Average of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="ReadOnlySpan{Byte}"/> to operate on.</param>    
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        public static byte Average(this ReadOnlySpan<byte> source)
        {
            byte sum = source.Sum();
            return (byte) (sum / source.Length);
        }

        /// <summary> 
        /// Computes the Average of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="ReadOnlySpan{UInt16}"/> to operate on.</param>    
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
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
        public static ulong Average(this ReadOnlySpan<ulong> source)
        {
            ulong sum = source.Sum();
            return (sum / (ulong) source.Length);
        }

        /// <summary> 
        /// Computes the Average of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="ReadOnlySpan{SByte}"/> to operate on.</param>    
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
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
        public static int Average(this ReadOnlySpan<int> source)
        {
            int sum = source.Sum();
            return (int)(sum / source.Length);
        }

        /// <summary> 
        /// Computes the Average of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="ReadOnlySpan{Int64}"/> to operate on.</param>    
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
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
        public static decimal Average(this ReadOnlySpan<decimal> source)
        {
            decimal sum = source.Sum();
            return (sum / source.Length);
        }

        /// <summary> 
        /// Computes the Average of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="ReadOnlySpan{BigInteger}"/> to operate on.</param>    
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        public static BigInteger Average(this ReadOnlySpan<BigInteger> source)
        {
            BigInteger sum = source.Sum();
            return (BigInteger)(sum / source.Length);
        }
#endif

#if NET5_0_OR_GREATER   
  
        /// <summary> 
        /// Computes the Average of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="ReadOnlySpan{Half}"/> to operate on.</param>    
        /// <returns>The Average of all the values in <paramref name="source"/>.</returns>
        public static Half Average(this ReadOnlySpan<Half> source)
        {
            Half sum = source.Sum();
            return (Half)((float)sum / source.Length);
        }
#endif
    }
}