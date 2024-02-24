using System;
#if NETCOREAPP3_0_OR_GREATER
using System.Runtime.CompilerServices;
#endif

namespace SpanExtensions
{
    static class InternalExtensions
    {
#if NETCOREAPP3_0_OR_GREATER
        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is negative.
        /// </summary>
        /// <param name="value">The argument to validate as non-negative.</param>
        /// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
        /// <returns><paramref name="value"/>.</returns>
        public static int ThrowIfNegative(this int value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
#if NET8_0_OR_GREATER
            ArgumentOutOfRangeException.ThrowIfNegative(value, paramName);
#else
            if(value < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, value, $"{paramName} ('{value}') must be a non-negative value.");
            }
#endif

            return value;
        }
#else
        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is negative.
        /// </summary>
        /// <param name="value">The argument to validate as non-negative.</param>
        /// <returns><paramref name="value"/>.</returns>
        public static int ThrowIfNegative(this int value)
        {
            if(value < 0)
            {
#pragma warning disable S3928 // Parameter names used into ArgumentException constructors should match an existing one
                throw new ArgumentOutOfRangeException(null, value, $" ('{value}') must be a non-negative value.");
#pragma warning restore S3928 // Parameter names used into ArgumentException constructors should match an existing one
            }

            return value;
        }
#endif

#if NETCOREAPP3_0_OR_GREATER
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="options"/> is not a valid flag.
        /// </summary>
        /// <param name="options">The argument to validate as a valid flag.</param>
        /// <param name="paramName">The name of the parameter with which <paramref name="options"/> corresponds.</param>
        /// <returns><paramref name="options"/>.</returns>
        public static StringSplitOptions ThrowIfInvalid(this StringSplitOptions options, [CallerArgumentExpression(nameof(options))] string? paramName = null)
        {
            if((options & ~CombinationOfAllValidStringSplitOptions) != 0)
            {
                throw new ArgumentException("Value of flag is invalid.", paramName);
            }

            return options;
        }
#else
        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if <paramref name="options"/> is not a valid flag.
        /// </summary>
        /// <param name="options">The argument to validate as a valid flag.</param>
        /// <returns><paramref name="options"/>.</returns>
        public static StringSplitOptions ThrowIfInvalid(this StringSplitOptions options)
        {
            if((options & ~CombinationOfAllValidStringSplitOptions) != 0)
            {
                throw new ArgumentException("Value of flag is invalid.");
            }

            return options;
        }
#endif

        /// <summary>
        /// Determines whether the <see cref="StringSplitOptions.RemoveEmptyEntries"/> bit field is set in the current instance.
        /// </summary>
        /// <param name="options">The <see cref="StringSplitOptions"/> instance to test.</param>
        /// <returns><see langword="true"/> if <paramref name="options"/> has the <see cref="StringSplitOptions.RemoveEmptyEntries"/> bit field set; <see langword="false"/> otherwise.</returns>
        public static bool IsRemoveEmptyEntriesSet(this StringSplitOptions options)
        {
            return options.HasFlag(StringSplitOptions.RemoveEmptyEntries);
        }

#if NET5_0_OR_GREATER
        /// <summary>
        /// Determines whether the <see cref="StringSplitOptions.TrimEntries"/> bit field is set in the current instance.
        /// </summary>
        /// <remarks>
        /// In runtimes before .NET 5 this always returns <see langword="false"/>.
        /// </remarks>
        /// <param name="options">The <see cref="StringSplitOptions"/> instance to test.</param>
        /// <returns><see langword="true"/> if <paramref name="options"/> has the <see cref="StringSplitOptions.TrimEntries"/> bit field set; <see langword="false"/> otherwise.</returns>
        public static bool IsTrimEntriesSet(this StringSplitOptions options)
        {
            return options.HasFlag(StringSplitOptions.TrimEntries);
        }
#else
        /// <summary>
        /// Always returns false.
        /// </summary>
        /// <param name="options">The <see cref="StringSplitOptions"/> instance to test.</param>
        /// <returns><see langword="false"/>.</returns>
        public static bool IsTrimEntriesSet(this StringSplitOptions options)
        {
            return false;
        }
#endif

        /// <summary>
        /// Gets all values of an enum and applies the or operation on them.
        /// </summary>
        /// <typeparam name="T">The target enum type.</typeparam>
        /// <returns>The combination of all valid enum values.</returns>
        public static T GetCombinationOfAllValidFlags<T>() where T : struct, Enum
        {
#if NET5_0_OR_GREATER
            T[] flags = Enum.GetValues<T>();
#else
            T[] flags = (T[])Enum.GetValues(typeof(T));
#endif

            int combination = 0;
            foreach(T flag in flags)
            {
                combination |= (int)(object)flag;
            }
            return (T)(object)combination;
        }

        static readonly StringSplitOptions CombinationOfAllValidStringSplitOptions = GetCombinationOfAllValidFlags<StringSplitOptions>();
    }
}
