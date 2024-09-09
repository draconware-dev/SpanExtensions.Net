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
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements in <paramref name="source"/>.</typeparam>
        /// <param name="source">A <see cref="ReadOnlySpan{T}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static T Max<T>(this ReadOnlySpan<T> source) where T : IComparable<T>
        {
            if(Vector512.IsHardwareAccelerated && Vector512<T>.IsSupported && source.Length > Vector512<T>.Count)
            {
                ref T current = ref MemoryMarshal.GetReference(source);
                ref T secondToLast = ref Unsafe.Add(ref current, source.Length - Vector512<T>.Count);

                Vector512<T> maxVector = Vector512.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector512<T>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector512.Max(maxVector, Vector512.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector512<T>.Count);
                }

                maxVector = Vector512.Max(maxVector, Vector512.LoadUnsafe(ref secondToLast));

                T result = maxVector[0];
                for(int i = 1; i < Vector512<T>.Count; i++)
                {
                    T currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
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

                Vector256<T> maxVector = Vector256.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector256<T>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector256.Max(maxVector, Vector256.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector256<T>.Count);
                }

                maxVector = Vector256.Max(maxVector, Vector256.LoadUnsafe(ref secondToLast));

                T result = maxVector[0];

                for(int i = 1; i < Vector256<T>.Count; i++)
                {
                    T currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            if(Vector128.IsHardwareAccelerated && Vector128<T>.IsSupported && source.Length > Vector128<T>.Count)
            {
                ref T current = ref MemoryMarshal.GetReference(source);
                ref T secondToLast = ref Unsafe.Add(ref current, source.Length - Vector128<T>.Count);

                Vector128<T> maxVector = Vector128.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector128<T>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector128.Max(maxVector, Vector128.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector128<T>.Count);
                }

                maxVector = Vector128.Max(maxVector, Vector128.LoadUnsafe(ref secondToLast));

                T result = maxVector[0];

                for(int i = 1; i < Vector128<T>.Count; i++)
                {
                    T currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            if(Vector64.IsHardwareAccelerated && Vector64<T>.IsSupported && source.Length > Vector64<T>.Count)
            {
                ref T current = ref MemoryMarshal.GetReference(source);
                ref T secondToLast = ref Unsafe.Add(ref current, source.Length - Vector64<T>.Count);

                Vector64<T> maxVector = Vector64.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector64<T>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector64.Max(maxVector, Vector64.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector64<T>.Count);
                }

                maxVector = Vector64.Max(maxVector, Vector64.LoadUnsafe(ref secondToLast));

                T result = maxVector[0];

                for(int i = 1; i < Vector64<T>.Count; i++)
                {
                    T currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

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
#else
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
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{byte}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static byte Max(this ReadOnlySpan<byte> source)
        {
            if(Vector256.IsHardwareAccelerated && Vector256<byte>.IsSupported && source.Length > Vector256<byte>.Count)
            {
                ref byte current = ref MemoryMarshal.GetReference(source);
                ref byte secondToLast = ref Unsafe.Add(ref current, source.Length - Vector256<byte>.Count);

                Vector256<byte> maxVector = Vector256.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector256<byte>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector256.Max(maxVector, Vector256.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector256<byte>.Count);
                }

                maxVector = Vector256.Max(maxVector, Vector256.LoadUnsafe(ref secondToLast));

                byte result = maxVector[0];

                for(int i = 1; i < Vector256<byte>.Count; i++)
                {
                    byte currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
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

                Vector128<byte> maxVector = Vector128.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector128<byte>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector128.Max(maxVector, Vector128.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector128<byte>.Count);
                }

                maxVector = Vector128.Max(maxVector, Vector128.LoadUnsafe(ref secondToLast));

                byte result = maxVector[0];

                for(int i = 1; i < Vector128<byte>.Count; i++)
                {
                    byte currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
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

                Vector64<byte> maxVector = Vector64.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector64<byte>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector64.Max(maxVector, Vector64.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector64<byte>.Count);
                }

                maxVector = Vector64.Max(maxVector, Vector64.LoadUnsafe(ref secondToLast));

                byte result = maxVector[0];

                for(int i = 1; i < Vector64<byte>.Count; i++)
                {
                    byte currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            byte max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                byte current = source[i];
                if(current.CompareTo(max) > 0)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{ushort}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static ushort Max(this ReadOnlySpan<ushort> source)
        {
            if(Vector256.IsHardwareAccelerated && Vector256<ushort>.IsSupported && source.Length > Vector256<ushort>.Count)
            {
                ref ushort current = ref MemoryMarshal.GetReference(source);
                ref ushort secondToLast = ref Unsafe.Add(ref current, source.Length - Vector256<ushort>.Count);

                Vector256<ushort> maxVector = Vector256.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector256<ushort>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector256.Max(maxVector, Vector256.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector256<ushort>.Count);
                }

                maxVector = Vector256.Max(maxVector, Vector256.LoadUnsafe(ref secondToLast));

                ushort result = maxVector[0];

                for(int i = 1; i < Vector256<ushort>.Count; i++)
                {
                    ushort currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
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

                Vector128<ushort> maxVector = Vector128.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector128<ushort>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector128.Max(maxVector, Vector128.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector128<ushort>.Count);
                }

                maxVector = Vector128.Max(maxVector, Vector128.LoadUnsafe(ref secondToLast));

                ushort result = maxVector[0];

                for(int i = 1; i < Vector128<ushort>.Count; i++)
                {
                    ushort currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
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

                Vector64<ushort> maxVector = Vector64.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector64<ushort>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector64.Max(maxVector, Vector64.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector64<ushort>.Count);
                }

                maxVector = Vector64.Max(maxVector, Vector64.LoadUnsafe(ref secondToLast));

                ushort result = maxVector[0];

                for(int i = 1; i < Vector64<ushort>.Count; i++)
                {
                    ushort currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            ushort max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                ushort current = source[i];
                if(current.CompareTo(max) > 0)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{uint}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static uint Max(this ReadOnlySpan<uint> source)
        {
            if(Vector256.IsHardwareAccelerated && Vector256<uint>.IsSupported && source.Length > Vector256<uint>.Count)
            {
                ref uint current = ref MemoryMarshal.GetReference(source);
                ref uint secondToLast = ref Unsafe.Add(ref current, source.Length - Vector256<uint>.Count);

                Vector256<uint> maxVector = Vector256.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector256<uint>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector256.Max(maxVector, Vector256.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector256<uint>.Count);
                }

                maxVector = Vector256.Max(maxVector, Vector256.LoadUnsafe(ref secondToLast));

                uint result = maxVector[0];

                for(int i = 1; i < Vector256<uint>.Count; i++)
                {
                    uint currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
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

                Vector128<uint> maxVector = Vector128.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector128<uint>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector128.Max(maxVector, Vector128.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector128<uint>.Count);
                }

                maxVector = Vector128.Max(maxVector, Vector128.LoadUnsafe(ref secondToLast));

                uint result = maxVector[0];

                for(int i = 1; i < Vector128<uint>.Count; i++)
                {
                    uint currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
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

                Vector64<uint> maxVector = Vector64.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector64<uint>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector64.Max(maxVector, Vector64.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector64<uint>.Count);
                }

                maxVector = Vector64.Max(maxVector, Vector64.LoadUnsafe(ref secondToLast));

                uint result = maxVector[0];

                for(int i = 1; i < Vector64<uint>.Count; i++)
                {
                    uint currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            uint max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                uint current = source[i];
                if(current.CompareTo(max) > 0)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{ulong}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static ulong Max(this ReadOnlySpan<ulong> source)
        {
            if(Vector256.IsHardwareAccelerated && Vector256<ulong>.IsSupported && source.Length > Vector256<ulong>.Count)
            {
                ref ulong current = ref MemoryMarshal.GetReference(source);
                ref ulong secondToLast = ref Unsafe.Add(ref current, source.Length - Vector256<ulong>.Count);

                Vector256<ulong> maxVector = Vector256.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector256<ulong>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector256.Max(maxVector, Vector256.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector256<ulong>.Count);
                }

                maxVector = Vector256.Max(maxVector, Vector256.LoadUnsafe(ref secondToLast));

                ulong result = maxVector[0];

                for(int i = 1; i < Vector256<ulong>.Count; i++)
                {
                    ulong currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
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

                Vector128<ulong> maxVector = Vector128.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector128<ulong>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector128.Max(maxVector, Vector128.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector128<ulong>.Count);
                }

                maxVector = Vector128.Max(maxVector, Vector128.LoadUnsafe(ref secondToLast));

                ulong result = maxVector[0];

                for(int i = 1; i < Vector128<ulong>.Count; i++)
                {
                    ulong currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            ulong max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                ulong current = source[i];
                if(current.CompareTo(max) > 0)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{byte}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static sbyte Max(this ReadOnlySpan<sbyte> source)
        {
            if(Vector256.IsHardwareAccelerated && Vector256<sbyte>.IsSupported && source.Length > Vector256<sbyte>.Count)
            {
                ref sbyte current = ref MemoryMarshal.GetReference(source);
                ref sbyte secondToLast = ref Unsafe.Add(ref current, source.Length - Vector256<sbyte>.Count);

                Vector256<sbyte> maxVector = Vector256.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector256<sbyte>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector256.Max(maxVector, Vector256.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector256<sbyte>.Count);
                }

                maxVector = Vector256.Max(maxVector, Vector256.LoadUnsafe(ref secondToLast));

                sbyte result = maxVector[0];

                for(int i = 1; i < Vector256<sbyte>.Count; i++)
                {
                    sbyte currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
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

                Vector128<sbyte> maxVector = Vector128.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector128<sbyte>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector128.Max(maxVector, Vector128.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector128<sbyte>.Count);
                }

                maxVector = Vector128.Max(maxVector, Vector128.LoadUnsafe(ref secondToLast));

                sbyte result = maxVector[0];

                for(int i = 1; i < Vector128<sbyte>.Count; i++)
                {
                    sbyte currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
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

                Vector64<sbyte> maxVector = Vector64.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector64<sbyte>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector64.Max(maxVector, Vector64.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector64<sbyte>.Count);
                }

                maxVector = Vector64.Max(maxVector, Vector64.LoadUnsafe(ref secondToLast));

                sbyte result = maxVector[0];

                for(int i = 1; i < Vector64<sbyte>.Count; i++)
                {
                    sbyte currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            sbyte max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                sbyte current = source[i];
                if(current.CompareTo(max) > 0)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{short}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static short Max(this ReadOnlySpan<short> source)
        {
            if(Vector256.IsHardwareAccelerated && Vector256<short>.IsSupported && source.Length > Vector256<short>.Count)
            {
                ref short current = ref MemoryMarshal.GetReference(source);
                ref short secondToLast = ref Unsafe.Add(ref current, source.Length - Vector256<short>.Count);

                Vector256<short> maxVector = Vector256.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector256<short>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector256.Max(maxVector, Vector256.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector256<short>.Count);
                }

                maxVector = Vector256.Max(maxVector, Vector256.LoadUnsafe(ref secondToLast));

                short result = maxVector[0];

                for(int i = 1; i < Vector256<short>.Count; i++)
                {
                    short currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
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

                Vector128<short> maxVector = Vector128.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector128<short>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector128.Max(maxVector, Vector128.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector128<short>.Count);
                }

                maxVector = Vector128.Max(maxVector, Vector128.LoadUnsafe(ref secondToLast));

                short result = maxVector[0];

                for(int i = 1; i < Vector128<short>.Count; i++)
                {
                    short currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
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

                Vector64<short> maxVector = Vector64.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector64<short>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector64.Max(maxVector, Vector64.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector64<short>.Count);
                }

                maxVector = Vector64.Max(maxVector, Vector64.LoadUnsafe(ref secondToLast));

                short result = maxVector[0];

                for(int i = 1; i < Vector64<short>.Count; i++)
                {
                    short currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            short max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                short current = source[i];
                if(current.CompareTo(max) > 0)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{int}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static int Max(this ReadOnlySpan<int> source)
        {
            if(Vector256.IsHardwareAccelerated && Vector256<int>.IsSupported && source.Length > Vector256<int>.Count)
            {
                ref int current = ref MemoryMarshal.GetReference(source);
                ref int secondToLast = ref Unsafe.Add(ref current, source.Length - Vector256<int>.Count);

                Vector256<int> maxVector = Vector256.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector256<int>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector256.Max(maxVector, Vector256.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector256<int>.Count);
                }

                maxVector = Vector256.Max(maxVector, Vector256.LoadUnsafe(ref secondToLast));

                int result = maxVector[0];

                for(int i = 1; i < Vector256<int>.Count; i++)
                {
                    int currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
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

                Vector128<int> maxVector = Vector128.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector128<int>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector128.Max(maxVector, Vector128.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector128<int>.Count);
                }

                maxVector = Vector128.Max(maxVector, Vector128.LoadUnsafe(ref secondToLast));

                int result = maxVector[0];

                for(int i = 1; i < Vector128<int>.Count; i++)
                {
                    int currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
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

                Vector64<int> maxVector = Vector64.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector64<int>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector64.Max(maxVector, Vector64.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector64<int>.Count);
                }

                maxVector = Vector64.Max(maxVector, Vector64.LoadUnsafe(ref secondToLast));

                int result = maxVector[0];

                for(int i = 1; i < Vector64<int>.Count; i++)
                {
                    int currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            int max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                int current = source[i];
                if(current.CompareTo(max) > 0)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{long}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static long Max(this ReadOnlySpan<long> source)
        {
            if(Vector256.IsHardwareAccelerated && Vector256<long>.IsSupported && source.Length > Vector256<long>.Count)
            {
                ref long current = ref MemoryMarshal.GetReference(source);
                ref long secondToLast = ref Unsafe.Add(ref current, source.Length - Vector256<long>.Count);

                Vector256<long> maxVector = Vector256.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector256<long>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector256.Max(maxVector, Vector256.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector256<long>.Count);
                }

                maxVector = Vector256.Max(maxVector, Vector256.LoadUnsafe(ref secondToLast));

                long result = maxVector[0];

                for(int i = 1; i < Vector256<long>.Count; i++)
                {
                    long currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
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

                Vector128<long> maxVector = Vector128.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector128<long>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector128.Max(maxVector, Vector128.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector128<long>.Count);
                }

                maxVector = Vector128.Max(maxVector, Vector128.LoadUnsafe(ref secondToLast));

                long result = maxVector[0];

                for(int i = 1; i < Vector128<long>.Count; i++)
                {
                    long currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            long max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                long current = source[i];
                if(current.CompareTo(max) > 0)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{float}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static float Max(this ReadOnlySpan<float> source)
        {
            if(Vector256.IsHardwareAccelerated && Vector256<float>.IsSupported && source.Length > Vector256<float>.Count)
            {
                ref float current = ref MemoryMarshal.GetReference(source);
                ref float secondToLast = ref Unsafe.Add(ref current, source.Length - Vector256<float>.Count);

                Vector256<float> maxVector = Vector256.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector256<float>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector256.Max(maxVector, Vector256.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector256<float>.Count);
                }

                maxVector = Vector256.Max(maxVector, Vector256.LoadUnsafe(ref secondToLast));

                float result = maxVector[0];

                for(int i = 1; i < Vector256<float>.Count; i++)
                {
                    float currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
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

                Vector128<float> maxVector = Vector128.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector128<float>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector128.Max(maxVector, Vector128.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector128<float>.Count);
                }

                maxVector = Vector128.Max(maxVector, Vector128.LoadUnsafe(ref secondToLast));

                float result = maxVector[0];

                for(int i = 1; i < Vector128<float>.Count; i++)
                {
                    float currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
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

                Vector64<float> maxVector = Vector64.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector64<float>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector64.Max(maxVector, Vector64.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector64<float>.Count);
                }

                maxVector = Vector64.Max(maxVector, Vector64.LoadUnsafe(ref secondToLast));

                float result = maxVector[0];

                for(int i = 1; i < Vector64<float>.Count; i++)
                {
                    float currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            float max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                float current = source[i];
                if(current.CompareTo(max) > 0)
                {
                    max = current;
                }
            }
            return max;
        }

        /// <summary>
        /// Returns the maximum value in <paramref name="source"/>.
        /// </summary>
        /// <param name="source">A <see cref="ReadOnlySpan{double}"/> to determine the maximum value of.</param>
        /// <returns>The maximum value in <paramref name="source"/>.</returns>
        public static double Max(this ReadOnlySpan<double> source)
        {
            if(Vector256.IsHardwareAccelerated && Vector256<double>.IsSupported && source.Length > Vector256<double>.Count)
            {
                ref double current = ref MemoryMarshal.GetReference(source);
                ref double secondToLast = ref Unsafe.Add(ref current, source.Length - Vector256<double>.Count);

                Vector256<double> maxVector = Vector256.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector256<double>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector256.Max(maxVector, Vector256.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector256<double>.Count);
                }

                maxVector = Vector256.Max(maxVector, Vector256.LoadUnsafe(ref secondToLast));

                double result = maxVector[0];

                for(int i = 1; i < Vector256<double>.Count; i++)
                {
                    double currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
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

                Vector128<double> maxVector = Vector128.LoadUnsafe(ref current);
                current = ref Unsafe.Add(ref current, Vector128<double>.Count);

                while(Unsafe.IsAddressLessThan(ref current, ref secondToLast))
                {
                    maxVector = Vector128.Max(maxVector, Vector128.LoadUnsafe(ref current));
                    current = ref Unsafe.Add(ref current, Vector128<double>.Count);
                }

                maxVector = Vector128.Max(maxVector, Vector128.LoadUnsafe(ref secondToLast));

                double result = maxVector[0];

                for(int i = 1; i < Vector128<double>.Count; i++)
                {
                    double currentResult = maxVector[i];

                    if(currentResult.CompareTo(result) > 0)
                    {
                        result = currentResult;
                    }
                }

                return result;
            }

            double max = source[0];
            for(int i = 1; i < source.Length; i++)
            {
                double current = source[i];
                if(current.CompareTo(max) > 0)
                {
                    max = current;
                }
            }
            return max;
        }
#endif

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