using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

#if NETCOREAPP3_0_OR_GREATER
using System.Runtime.Intrinsics;
#endif

namespace SpanExtensions
{
    public static partial class ReadOnlySpanExtensions
    {
#if NET7_0_OR_GREATER

#if NET8_0_OR_GREATER

        /// <summary>
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static T Min<T>(this ReadOnlySpan<T> source) where T : IComparable<T>
        {
            if(Vector512.IsHardwareAccelerated && Vector512<T>.IsSupported && source.Length > Vector512<T>.Count)
            {
                ref T current = ref MemoryMarshal.GetReference(source);
                ref T secondToLast = ref Unsafe.Add(ref current, source.Length - Vector512<T>.Count);

                Vector512<T> minVector = Vector512.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector512<T>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector512.Min(minVector, Vector512.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector512<T>.Count);
                }

                minVector = Vector512.Min(minVector, Vector512.LoadUnsafe(ref secondToLast));

                T result = minVector[0];

                for(int i = 1; i < Vector512<T>.Count; i++)
                {
                    T currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            if(Vector256.IsHardwareAccelerated && Vector256<T>.IsSupported && source.Length > Vector256<T>.Count)
            {
                ref T current = ref MemoryMarshal.GetReference(source);
                ref T secondToLast = ref Unsafe.Add(ref current, source.Length - Vector256<T>.Count);

                Vector256<T> minVector = Vector256.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector256<T>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector256.Min(minVector, Vector256.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector256<T>.Count);
                }

                minVector = Vector256.Min(minVector, Vector256.LoadUnsafe(ref secondToLast));

                T result = minVector[0];

                for(int i = 1; i < Vector256<T>.Count; i++)
                {
                    T currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            if(Vector128.IsHardwareAccelerated && Vector128<T>.IsSupported && source.Length > Vector128<T>.Count * 2)
            {
                ref T current = ref MemoryMarshal.GetReference(source);
                ref T secondToLast = ref Unsafe.Add(ref current, source.Length - Vector128<T>.Count);

                Vector128<T> minVector = Vector128.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector128<T>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector128.Min(minVector, Vector128.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector128<T>.Count);
                }

                minVector = Vector128.Min(minVector, Vector128.LoadUnsafe(ref secondToLast));

                T result = minVector[0];

                for(int i = 1; i < Vector128<T>.Count; i++)
                {
                    T currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            if(Vector64.IsHardwareAccelerated && Vector64<T>.IsSupported && source.Length > Vector64<T>.Count * 4)
            {
                ref T current = ref MemoryMarshal.GetReference(source);
                ref T secondToLast = ref Unsafe.Add(ref current, source.Length - Vector64<T>.Count);

                Vector64<T> minVector = Vector64.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector64<T>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector64.Min(minVector, Vector64.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector64<T>.Count);
                }

                minVector = Vector64.Min(minVector, Vector64.LoadUnsafe(ref secondToLast));

                T result = minVector[0];

                for(int i = 1; i < Vector64<T>.Count; i++)
                {
                    T currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

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
#else

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
        /// Returns the minimum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{Byte}"/> to determine the minimum value of.</param>
        /// <returns>The minimum value in <paramref name="source"/>.</returns>
        public static byte Min(this ReadOnlySpan<byte> source)
        {
            if(Vector256.IsHardwareAccelerated && Vector256<byte>.IsSupported && source.Length > Vector256<byte>.Count)
            {
                ref byte current = ref MemoryMarshal.GetReference(source);
                ref byte secondToLast = ref Unsafe.Add(ref current, source.Length - Vector256<byte>.Count);

                Vector256<byte> minVector = Vector256.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector256<byte>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector256.Min(minVector, Vector256.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector256<byte>.Count);
                }

                minVector = Vector256.Min(minVector, Vector256.LoadUnsafe(ref secondToLast));

                byte result = minVector[0];

                for(int i = 1; i < Vector256<byte>.Count; i++)
                {
                    byte currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            if(Vector128.IsHardwareAccelerated && Vector128<byte>.IsSupported && source.Length > Vector128<byte>.Count)
            {
                ref byte current = ref MemoryMarshal.GetReference(source);
                ref byte secondToLast = ref Unsafe.Add(ref current, source.Length - Vector128<byte>.Count);

                Vector128<byte> minVector = Vector128.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector128<byte>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector128.Min(minVector, Vector128.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector128<byte>.Count);
                }

                minVector = Vector128.Min(minVector, Vector128.LoadUnsafe(ref secondToLast));

                byte result = minVector[0];

                for(int i = 1; i < Vector128<byte>.Count; i++)
                {
                    byte currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            if(Vector64.IsHardwareAccelerated && Vector64<byte>.IsSupported && source.Length > Vector64<byte>.Count)
            {
                ref byte current = ref MemoryMarshal.GetReference(source);
                ref byte secondToLast = ref Unsafe.Add(ref current, source.Length - Vector64<byte>.Count);

                Vector64<byte> minVector = Vector64.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector64<byte>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector64.Min(minVector, Vector64.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector64<byte>.Count);
                }

                minVector = Vector64.Min(minVector, Vector64.LoadUnsafe(ref secondToLast));

                byte result = minVector[0];

                for(int i = 1; i < Vector64<byte>.Count; i++)
                {
                    byte currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            byte min = source[0];

            for(int i = 1; i < source.Length; i++)
            {
                byte current = source[i];

                if(current.CompareTo(min) < 0)
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
            if(Vector256.IsHardwareAccelerated && Vector256<ushort>.IsSupported && source.Length > Vector256<ushort>.Count)
            {
                ref ushort current = ref MemoryMarshal.GetReference(source);
                ref ushort secondToLast = ref Unsafe.Add(ref current, source.Length - Vector256<ushort>.Count);

                Vector256<ushort> minVector = Vector256.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector256<ushort>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector256.Min(minVector, Vector256.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector256<ushort>.Count);
                }

                minVector = Vector256.Min(minVector, Vector256.LoadUnsafe(ref secondToLast));

                ushort result = minVector[0];

                for(int i = 1; i < Vector256<ushort>.Count; i++)
                {
                    ushort currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            if(Vector128.IsHardwareAccelerated && Vector128<ushort>.IsSupported && source.Length > Vector128<ushort>.Count)
            {
                ref ushort current = ref MemoryMarshal.GetReference(source);
                ref ushort secondToLast = ref Unsafe.Add(ref current, source.Length - Vector128<ushort>.Count);

                Vector128<ushort> minVector = Vector128.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector128<ushort>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector128.Min(minVector, Vector128.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector128<ushort>.Count);
                }

                minVector = Vector128.Min(minVector, Vector128.LoadUnsafe(ref secondToLast));

                ushort result = minVector[0];

                for(int i = 1; i < Vector128<ushort>.Count; i++)
                {
                    ushort currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            if(Vector64.IsHardwareAccelerated && Vector64<ushort>.IsSupported && source.Length > Vector64<ushort>.Count)
            {
                ref ushort current = ref MemoryMarshal.GetReference(source);
                ref ushort secondToLast = ref Unsafe.Add(ref current, source.Length - Vector64<ushort>.Count);

                Vector64<ushort> minVector = Vector64.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector64<ushort>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector64.Min(minVector, Vector64.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector64<ushort>.Count);
                }

                minVector = Vector64.Min(minVector, Vector64.LoadUnsafe(ref secondToLast));

                ushort result = minVector[0];

                for(int i = 1; i < Vector64<ushort>.Count; i++)
                {
                    ushort currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            ushort min = source[0];

            for(int i = 1; i < source.Length; i++)
            {
                ushort current = source[i];

                if(current.CompareTo(min) < 0)
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
            if(Vector256.IsHardwareAccelerated && Vector256<uint>.IsSupported && source.Length > Vector256<uint>.Count)
            {
                ref uint current = ref MemoryMarshal.GetReference(source);
                ref uint secondToLast = ref Unsafe.Add(ref current, source.Length - Vector256<uint>.Count);

                Vector256<uint> minVector = Vector256.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector256<uint>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector256.Min(minVector, Vector256.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector256<uint>.Count);
                }

                minVector = Vector256.Min(minVector, Vector256.LoadUnsafe(ref secondToLast));

                uint result = minVector[0];

                for(int i = 1; i < Vector256<uint>.Count; i++)
                {
                    uint currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            if(Vector128.IsHardwareAccelerated && Vector128<uint>.IsSupported && source.Length > Vector128<uint>.Count)
            {
                ref uint current = ref MemoryMarshal.GetReference(source);
                ref uint secondToLast = ref Unsafe.Add(ref current, source.Length - Vector128<uint>.Count);

                Vector128<uint> minVector = Vector128.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector128<uint>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector128.Min(minVector, Vector128.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector128<uint>.Count);
                }

                minVector = Vector128.Min(minVector, Vector128.LoadUnsafe(ref secondToLast));

                uint result = minVector[0];

                for(int i = 1; i < Vector128<uint>.Count; i++)
                {
                    uint currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            if(Vector64.IsHardwareAccelerated && Vector64<uint>.IsSupported && source.Length > Vector64<uint>.Count)
            {
                ref uint current = ref MemoryMarshal.GetReference(source);
                ref uint secondToLast = ref Unsafe.Add(ref current, source.Length - Vector64<uint>.Count);

                Vector64<uint> minVector = Vector64.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector64<uint>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector64.Min(minVector, Vector64.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector64<uint>.Count);
                }

                minVector = Vector64.Min(minVector, Vector64.LoadUnsafe(ref secondToLast));

                uint result = minVector[0];

                for(int i = 1; i < Vector64<uint>.Count; i++)
                {
                    uint currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            uint min = source[0];

            for(int i = 1; i < source.Length; i++)
            {
                uint current = source[i];

                if(current.CompareTo(min) < 0)
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
            if(Vector256.IsHardwareAccelerated && Vector256<ulong>.IsSupported && source.Length > Vector256<ulong>.Count)
            {
                ref ulong current = ref MemoryMarshal.GetReference(source);
                ref ulong secondToLast = ref Unsafe.Add(ref current, source.Length - Vector256<ulong>.Count);

                Vector256<ulong> minVector = Vector256.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector256<ulong>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector256.Min(minVector, Vector256.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector256<ulong>.Count);
                }

                minVector = Vector256.Min(minVector, Vector256.LoadUnsafe(ref secondToLast));

                ulong result = minVector[0];

                for(int i = 1; i < Vector256<ulong>.Count; i++)
                {
                    ulong currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            if(Vector128.IsHardwareAccelerated && Vector128<ulong>.IsSupported && source.Length > Vector128<ulong>.Count)
            {
                ref ulong current = ref MemoryMarshal.GetReference(source);
                ref ulong secondToLast = ref Unsafe.Add(ref current, source.Length - Vector128<ulong>.Count);

                Vector128<ulong> minVector = Vector128.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector128<ulong>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector128.Min(minVector, Vector128.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector128<ulong>.Count);
                }

                minVector = Vector128.Min(minVector, Vector128.LoadUnsafe(ref secondToLast));

                ulong result = minVector[0];

                for(int i = 1; i < Vector128<ulong>.Count; i++)
                {
                    ulong currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            ulong min = source[0];

            for(int i = 1; i < source.Length; i++)
            {
                ulong current = source[i];

                if(current.CompareTo(min) < 0)
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
            if(Vector256.IsHardwareAccelerated && Vector256<sbyte>.IsSupported && source.Length > Vector256<sbyte>.Count)
            {
                ref sbyte current = ref MemoryMarshal.GetReference(source);
                ref sbyte secondToLast = ref Unsafe.Add(ref current, source.Length - Vector256<sbyte>.Count);

                Vector256<sbyte> minVector = Vector256.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector256<sbyte>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector256.Min(minVector, Vector256.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector256<sbyte>.Count);
                }

                minVector = Vector256.Min(minVector, Vector256.LoadUnsafe(ref secondToLast));

                sbyte result = minVector[0];

                for(int i = 1; i < Vector256<sbyte>.Count; i++)
                {
                    sbyte currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            if(Vector128.IsHardwareAccelerated && Vector128<sbyte>.IsSupported && source.Length > Vector128<sbyte>.Count)
            {
                ref sbyte current = ref MemoryMarshal.GetReference(source);
                ref sbyte secondToLast = ref Unsafe.Add(ref current, source.Length - Vector128<sbyte>.Count);

                Vector128<sbyte> minVector = Vector128.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector128<sbyte>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector128.Min(minVector, Vector128.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector128<sbyte>.Count);
                }

                minVector = Vector128.Min(minVector, Vector128.LoadUnsafe(ref secondToLast));

                sbyte result = minVector[0];

                for(int i = 1; i < Vector128<sbyte>.Count; i++)
                {
                    sbyte currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            if(Vector64.IsHardwareAccelerated && Vector64<sbyte>.IsSupported && source.Length > Vector64<sbyte>.Count)
            {
                ref sbyte current = ref MemoryMarshal.GetReference(source);
                ref sbyte secondToLast = ref Unsafe.Add(ref current, source.Length - Vector64<sbyte>.Count);

                Vector64<sbyte> minVector = Vector64.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector64<sbyte>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector64.Min(minVector, Vector64.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector64<sbyte>.Count);
                }

                minVector = Vector64.Min(minVector, Vector64.LoadUnsafe(ref secondToLast));

                sbyte result = minVector[0];

                for(int i = 1; i < Vector64<sbyte>.Count; i++)
                {
                    sbyte currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            sbyte min = source[0];

            for(int i = 1; i < source.Length; i++)
            {
                sbyte current = source[i];

                if(current.CompareTo(min) < 0)
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
            if(Vector256.IsHardwareAccelerated && Vector256<short>.IsSupported && source.Length > Vector256<short>.Count)
            {
                ref short current = ref MemoryMarshal.GetReference(source);
                ref short secondToLast = ref Unsafe.Add(ref current, source.Length - Vector256<short>.Count);

                Vector256<short> minVector = Vector256.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector256<short>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector256.Min(minVector, Vector256.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector256<short>.Count);
                }

                minVector = Vector256.Min(minVector, Vector256.LoadUnsafe(ref secondToLast));

                short result = minVector[0];

                for(int i = 1; i < Vector256<short>.Count; i++)
                {
                    short currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            if(Vector128.IsHardwareAccelerated && Vector128<short>.IsSupported && source.Length > Vector128<short>.Count)
            {
                ref short current = ref MemoryMarshal.GetReference(source);
                ref short secondToLast = ref Unsafe.Add(ref current, source.Length - Vector128<short>.Count);

                Vector128<short> minVector = Vector128.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector128<short>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector128.Min(minVector, Vector128.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector128<short>.Count);
                }

                minVector = Vector128.Min(minVector, Vector128.LoadUnsafe(ref secondToLast));

                short result = minVector[0];

                for(int i = 1; i < Vector128<short>.Count; i++)
                {
                    short currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            if(Vector64.IsHardwareAccelerated && Vector64<short>.IsSupported && source.Length > Vector64<short>.Count)
            {
                ref short current = ref MemoryMarshal.GetReference(source);
                ref short secondToLast = ref Unsafe.Add(ref current, source.Length - Vector64<short>.Count);

                Vector64<short> minVector = Vector64.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector64<short>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector64.Min(minVector, Vector64.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector64<short>.Count);
                }

                minVector = Vector64.Min(minVector, Vector64.LoadUnsafe(ref secondToLast));

                short result = minVector[0];

                for(int i = 1; i < Vector64<short>.Count; i++)
                {
                    short currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            short min = source[0];

            for(int i = 1; i < source.Length; i++)
            {
                short current = source[i];

                if(current.CompareTo(min) < 0)
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
            if(Vector256.IsHardwareAccelerated && Vector256<int>.IsSupported && source.Length > Vector256<int>.Count)
            {
                ref int current = ref MemoryMarshal.GetReference(source);
                ref int secondToLast = ref Unsafe.Add(ref current, source.Length - Vector256<int>.Count);

                Vector256<int> minVector = Vector256.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector256<int>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector256.Min(minVector, Vector256.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector256<int>.Count);
                }

                minVector = Vector256.Min(minVector, Vector256.LoadUnsafe(ref secondToLast));

                int result = minVector[0];

                for(int i = 1; i < Vector256<int>.Count; i++)
                {
                    int currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            if(Vector128.IsHardwareAccelerated && Vector128<int>.IsSupported && source.Length > Vector128<int>.Count)
            {
                ref int current = ref MemoryMarshal.GetReference(source);
                ref int secondToLast = ref Unsafe.Add(ref current, source.Length - Vector128<int>.Count);

                Vector128<int> minVector = Vector128.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector128<int>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector128.Min(minVector, Vector128.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector128<int>.Count);
                }

                minVector = Vector128.Min(minVector, Vector128.LoadUnsafe(ref secondToLast));

                int result = minVector[0];

                for(int i = 1; i < Vector128<int>.Count; i++)
                {
                    int currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            if(Vector64.IsHardwareAccelerated && Vector64<int>.IsSupported && source.Length > Vector64<int>.Count)
            {
                ref int current = ref MemoryMarshal.GetReference(source);
                ref int secondToLast = ref Unsafe.Add(ref current, source.Length - Vector64<int>.Count);

                Vector64<int> minVector = Vector64.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector64<int>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector64.Min(minVector, Vector64.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector64<int>.Count);
                }

                minVector = Vector64.Min(minVector, Vector64.LoadUnsafe(ref secondToLast));

                int result = minVector[0];

                for(int i = 1; i < Vector64<int>.Count; i++)
                {
                    int currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            int min = source[0];

            for(int i = 1; i < source.Length; i++)
            {
                int current = source[i];

                if(current.CompareTo(min) < 0)
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
            if(Vector256.IsHardwareAccelerated && Vector256<long>.IsSupported && source.Length > Vector256<long>.Count)
            {
                ref long current = ref MemoryMarshal.GetReference(source);
                ref long secondToLast = ref Unsafe.Add(ref current, source.Length - Vector256<long>.Count);

                Vector256<long> minVector = Vector256.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector256<long>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector256.Min(minVector, Vector256.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector256<long>.Count);
                }

                minVector = Vector256.Min(minVector, Vector256.LoadUnsafe(ref secondToLast));

                long result = minVector[0];

                for(int i = 1; i < Vector256<long>.Count; i++)
                {
                    long currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            if(Vector128.IsHardwareAccelerated && Vector128<long>.IsSupported && source.Length > Vector128<long>.Count)
            {
                ref long current = ref MemoryMarshal.GetReference(source);
                ref long secondToLast = ref Unsafe.Add(ref current, source.Length - Vector128<long>.Count);

                Vector128<long> minVector = Vector128.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector128<long>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector128.Min(minVector, Vector128.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector128<long>.Count);
                }

                minVector = Vector128.Min(minVector, Vector128.LoadUnsafe(ref secondToLast));

                long result = minVector[0];

                for(int i = 1; i < Vector128<long>.Count; i++)
                {
                    long currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            long min = source[0];

            for(int i = 1; i < source.Length; i++)
            {
                long current = source[i];

                if(current.CompareTo(min) < 0)
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
            if(Vector256.IsHardwareAccelerated && Vector256<float>.IsSupported && source.Length > Vector256<float>.Count)
            {
                ref float current = ref MemoryMarshal.GetReference(source);
                ref float secondToLast = ref Unsafe.Add(ref current, source.Length - Vector256<float>.Count);

                Vector256<float> minVector = Vector256.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector256<float>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector256.Min(minVector, Vector256.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector256<float>.Count);
                }

                minVector = Vector256.Min(minVector, Vector256.LoadUnsafe(ref secondToLast));

                float result = minVector[0];

                for(int i = 1; i < Vector256<float>.Count; i++)
                {
                    float currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            if(Vector128.IsHardwareAccelerated && Vector128<float>.IsSupported && source.Length > Vector128<float>.Count)
            {
                ref float current = ref MemoryMarshal.GetReference(source);
                ref float secondToLast = ref Unsafe.Add(ref current, source.Length - Vector128<float>.Count);

                Vector128<float> minVector = Vector128.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector128<float>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector128.Min(minVector, Vector128.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector128<float>.Count);
                }

                minVector = Vector128.Min(minVector, Vector128.LoadUnsafe(ref secondToLast));

                float result = minVector[0];

                for(int i = 1; i < Vector128<float>.Count; i++)
                {
                    float currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            if(Vector64.IsHardwareAccelerated && Vector64<float>.IsSupported && source.Length > Vector64<float>.Count)
            {
                ref float current = ref MemoryMarshal.GetReference(source);
                ref float secondToLast = ref Unsafe.Add(ref current, source.Length - Vector64<float>.Count);

                Vector64<float> minVector = Vector64.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector64<float>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector64.Min(minVector, Vector64.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector64<float>.Count);
                }

                minVector = Vector64.Min(minVector, Vector64.LoadUnsafe(ref secondToLast));

                float result = minVector[0];

                for(int i = 1; i < Vector64<float>.Count; i++)
                {
                    float currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            float min = source[0];

            for(int i = 1; i < source.Length; i++)
            {
                float current = source[i];

                if(current.CompareTo(min) < 0)
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
            if(Vector256.IsHardwareAccelerated && Vector256<double>.IsSupported && source.Length > Vector256<double>.Count)
            {
                ref double current = ref MemoryMarshal.GetReference(source);
                ref double secondToLast = ref Unsafe.Add(ref current, source.Length - Vector256<double>.Count);

                Vector256<double> minVector = Vector256.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector256<double>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector256.Min(minVector, Vector256.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector256<double>.Count);
                }

                minVector = Vector256.Min(minVector, Vector256.LoadUnsafe(ref secondToLast));

                double result = minVector[0];

                for(int i = 1; i < Vector256<double>.Count; i++)
                {
                    double currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            if(Vector128.IsHardwareAccelerated && Vector128<double>.IsSupported && source.Length > Vector128<double>.Count)
            {
                ref double current = ref MemoryMarshal.GetReference(source);
                ref double secondToLast = ref Unsafe.Add(ref current, source.Length - Vector128<double>.Count);

                Vector128<double> minVector = Vector128.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector128<double>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    minVector = Vector128.Min(minVector, Vector128.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector128<double>.Count);
                }

                minVector = Vector128.Min(minVector, Vector128.LoadUnsafe(ref secondToLast));

                double result = minVector[0];

                for(int i = 1; i < Vector128<double>.Count; i++)
                {
                    double currentResult = minVector[i];

                    if(currentResult.CompareTo(result) < 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            double min = source[0];

            for(int i = 1; i < source.Length; i++)
            {
                double current = source[i];

                if(current.CompareTo(min) < 0)
                {
                    min = current;
                }
            }

            return min;
        }
#endif

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
            ExceptionHelpers.ThrowIfNull(selector, nameof(selector));

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
            ExceptionHelpers.ThrowIfNull(selector, nameof(selector));

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
            ExceptionHelpers.ThrowIfNull(selector, nameof(selector));

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
            ExceptionHelpers.ThrowIfNull(selector, nameof(selector));

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
            ExceptionHelpers.ThrowIfNull(selector, nameof(selector));
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
            ExceptionHelpers.ThrowIfNull(selector, nameof(selector));

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
            ExceptionHelpers.ThrowIfNull(selector, nameof(selector));
            
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
            ExceptionHelpers.ThrowIfNull(selector, nameof(selector));

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
            ExceptionHelpers.ThrowIfNull(selector, nameof(selector));

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
            ExceptionHelpers.ThrowIfNull(selector, nameof(selector));

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
            ExceptionHelpers.ThrowIfNull(selector, nameof(selector));
            
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
            ExceptionHelpers.ThrowIfNull(selector, nameof(selector));

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