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
    }
}
