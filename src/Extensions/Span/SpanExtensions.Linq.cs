using System;
using System.Numerics;

namespace SpanExtensions
{
    /// <summary>
    /// Extension Methods for <see cref="Span{T}"/>  
    /// </summary>
    public static partial class SpanExtensions
    {

        /// <summary>
        /// Determines whether all elements in <paramref name="source"/> satisfy a condition.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="Span{T}"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>
        /// <param name="predicate">The Condition to be satisfied.</param>   
        /// <returns>A <see cref="bool"/> indicating whether or not every element in <paramref name="source"/> satisified the condition.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        public static bool All<T>(this Span<T> source, Predicate<T> predicate) where T : IEquatable<T>
        {
            return ReadOnlySpanExtensions.All(source, predicate);
        }

        /// <summary>
        /// Determines whether any element in <paramref name="source"/> satisfies a condition. 
        /// </summary>  
        /// <typeparam name="T">The type of elements in the <see cref="Span{T}"/>.</typeparam>   
        /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>    
        /// <param name="predicate">The Condition to be satisfied.</param>  
        /// <returns>A <see cref="bool"/> indicating whether or not any elements satisified the condition.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="predicate"/> is null.</exception>
        public static bool Any<T>(this Span<T> source, Predicate<T> predicate) where T : IEquatable<T>
        {
            return ReadOnlySpanExtensions.Any(source, predicate);
        }

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
        /// Bypasses a specified number of elements in <paramref name="source"/> and then returns the remaining elements.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="Span{T}"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>
        /// <param name="count">The Number of elements to bypass.</param>
        /// <returns>A <see cref="Span{T}"/> that contains the elements that occur after <paramref name="count"/> elements have been bypassed in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static Span<T> Skip<T>(this Span<T> source, int count)
        {
            return source[count..];
        }

        /// <summary> 
        /// Returns a specified number of contiguous elements from the start of a <see cref="Span{T}"/>.
        /// </summary> 
        /// <typeparam name="T">The type of elements in the <see cref="Span{T}"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>  
        /// <param name="count">The Number of elements to take.</param> 
        /// <returns>A <see cref="Span{T}"/> that contains <paramref name="count"/> elements from the start of the input sequence</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static Span<T> Take<T>(this Span<T> source, int count)
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
        public static Span<T> SkipWhile<T>(this Span<T> source, Predicate<T> condition)
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
            return Span<T>.Empty;
        }

        ///  <summary> 
        /// Returns elements from <paramref name="source"/> as long as a specified <paramref name="condition"/> is true, and then skips the remaining elements.   
        /// </summary> 
        /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam> 
        /// <param name="source">The <see cref="ReadOnlySpan{T}"/> to operate on.</param>  
        /// <param name="condition">A function to test each element for a condition.</param>    
        /// <returns>A <see cref="ReadOnlySpan{T}"/> that contains elements from <paramref name="source"/> that occur before the element at which the <paramref name="condition"/> no longer passes.</returns> 
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="condition"/> is null.</exception>
        public static Span<T> TakeWhile<T>(this Span<T> source, Predicate<T> condition)
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
            return Span<T>.Empty;
        }

        /// <summary> 
        /// Returns a new <see cref="Span{T}"/> that contains the elements from source with the last <paramref name="count"/> elements of the source collection omitted.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="Span{T}"/>.</typeparam>
        /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>
        /// <param name="count">The number of elements to omit from the end of <paramref name="source"/>.</param>
        /// <returns>A new <see cref="Span{T}"/> that contains the elements from <paramref name="source"/> minus <paramref name="count"/> elements from the end of the collection.</returns>
        /// <remarks>If <paramref name="count"/> is not a positive number, this method returns <see cref="Span{T}.Empty"/>.</remarks>
        public static Span<T> SkipLast<T>(this Span<T> source, int count)
        {
            if(count < 0)
            {
                return Span<T>.Empty;
            }
            return source[..(source.Length - count)];
        }

        /// <summary> 
        /// Returns a new <see cref="Span{T}"/> that contains the last <paramref name="count"/> elements from <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="Span{T}"/>.</typeparam> 
        /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>
        /// <param name="count">The number of elements to take from the end of <paramref name="source"/>.</param>
        /// <returns>A new <see cref="Span{T}"/> that contains the last <paramref name="count"/> elements from <paramref name="source"/>. </returns>
        /// <remarks>If <paramref name="count"/> is not a positive number, this method returns <see cref="Span{T}.Empty"/>.</remarks>
        public static Span<T> TakeLast<T>(this Span<T> source, int count)
        {
            if(count < 0)
            {
                return Span<T>.Empty;
            }
            return source[(source.Length - count)..];
        }

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
            return ReadOnlySpanExtensions.Min<T>(source)
        }
#else

#if NET5_0_OR_GREATER

        /// <summary> 
        /// Computes the Min of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{Half}"/> to operate on.</param>    
        /// <returns>The Min out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static Half Min(this Span<Half> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }
#endif
        /// <summary> 
        /// Computes the Min of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{Byte}"/> to operate on.</param>    
        /// <returns>The Min out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static byte Min(this Span<byte> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }

        /// <summary> 
        /// Computes the Min of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{UInt16}"/> to operate on.</param>    
        /// <returns>The Min out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ushort Min(this Span<ushort> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }

        /// <summary> 
        /// Computes the Min of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{uint32}"/> to operate on.</param>    
        /// <returns>The Min out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static uint Min(this Span<uint> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }
        /// <summary> 
        /// Computes the Min of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{UInt64}"/> to operate on.</param>    
        /// <returns>The Min out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ulong Min(this Span<ulong> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }

        /// <summary> 
        /// Computes the Min of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{SByte}"/> to operate on.</param>    
        /// <returns>The Min out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static sbyte Min(this Span<sbyte> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }

        /// <summary> 
        /// Computes the Min of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{Int16}"/> to operate on.</param>    
        /// <returns>The Min out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static short Min(this Span<short> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }

        /// <summary> 
        /// Computes the Min of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{Int32}"/> to operate on.</param>    
        /// <returns>The Min out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static int Min(this Span<int> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }

        /// <summary> 
        /// Computes the Min of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{Int64}"/> to operate on.</param>    
        /// <returns>The Min out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static long Min(this Span<long> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }

        /// <summary> 
        /// Computes the Min of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{Single}"/> to operate on.</param>    
        /// <returns>The Min out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static float Min(this Span<float> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }

        /// <summary> 
        /// Computes the Min of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{Double}"/> to operate on.</param>    
        /// <returns>The Min out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static double Min(this Span<double> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }

        /// <summary> 
        /// Computes the Min of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{Decimal}"/> to operate on.</param>    
        /// <returns>The Min out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static decimal Min(this Span<decimal> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }

        /// <summary> 
        /// Computes the Min of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{BigInteger}"/> to operate on.</param>    
        /// <returns>The Min out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static BigInteger Min(this Span<BigInteger> source)
        {
            return ReadOnlySpanExtensions.Min(source);
        }
#endif
#if NET7_0_OR_GREATER

        /// <summary> 
        /// Computes the Max of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam> 
        /// <param name="source">The <see cref="Span{T}"/> to operate on.</param>    
        /// <returns>The Max out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static T Max<T>(this Span<T> source) where T : INumber<T>
        {
            return ReadOnlySpanExtensions.Max<T>(source)
        }
#else

#if NET5_0_OR_GREATER

        /// <summary> 
        /// Computes the Max of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{Half}"/> to operate on.</param>    
        /// <returns>The Max out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static Half Max(this Span<Half> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }
#endif
        /// <summary> 
        /// Computes the Max of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{Byte}"/> to operate on.</param>    
        /// <returns>The Max out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static byte Max(this Span<byte> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }

        /// <summary> 
        /// Computes the Max of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{UInt16}"/> to operate on.</param>    
        /// <returns>The Max out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ushort Max(this Span<ushort> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }

        /// <summary> 
        /// Computes the Max of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{uint32}"/> to operate on.</param>    
        /// <returns>The Max out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static uint Max(this Span<uint> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }
        /// <summary> 
        /// Computes the Max of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{UInt64}"/> to operate on.</param>    
        /// <returns>The Max out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static ulong Max(this Span<ulong> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }

        /// <summary> 
        /// Computes the Max of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{SByte}"/> to operate on.</param>    
        /// <returns>The Max out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static sbyte Max(this Span<sbyte> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }

        /// <summary> 
        /// Computes the Max of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{Int16}"/> to operate on.</param>    
        /// <returns>The Max out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static short Max(this Span<short> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }

        /// <summary> 
        /// Computes the Max of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{Int32}"/> to operate on.</param>    
        /// <returns>The Max out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static int Max(this Span<int> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }

        /// <summary> 
        /// Computes the Max of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{Int64}"/> to operate on.</param>    
        /// <returns>The Max out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static long Max(this Span<long> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }

        /// <summary> 
        /// Computes the Max of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{Single}"/> to operate on.</param>    
        /// <returns>The Max out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static float Max(this Span<float> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }

        /// <summary> 
        /// Computes the Max of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{Double}"/> to operate on.</param>    
        /// <returns>The Max out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static double Max(this Span<double> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }

        /// <summary> 
        /// Computes the Max of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{Decimal}"/> to operate on.</param>    
        /// <returns>The Max out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static decimal Max(this Span<decimal> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }

        /// <summary> 
        /// Computes the Max of all the values in <paramref name="source"/>. 
        /// </summary>  
        /// <param name="source">The <see cref="Span{BigInteger}"/> to operate on.</param>    
        /// <returns>The Max out of all the values in <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static BigInteger Max(this Span<BigInteger> source)
        {
            return ReadOnlySpanExtensions.Max(source);
        }
#endif

    }
}