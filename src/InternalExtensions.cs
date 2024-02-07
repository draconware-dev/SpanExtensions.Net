using System;
using System.Runtime.CompilerServices;

namespace SpanExtensions
{
    static class InternalExtensions
    {
        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if <paramref name="value"/> is negative.
        /// </summary>
        /// <param name="value">The argument to validate as non-negative.</param>
        /// <param name="paramName">The name of the parameter with which <paramref name="value"/> corresponds.</param>
        /// <returns><paramref name="value"/>.</returns>
        public static int ThrowIfNegative(this int value
#if NETCOREAPP3_0_OR_GREATER
            , [CallerArgumentExpression(nameof(value))] string? paramName = null
#endif
            )
        {
#if NET8_0_OR_GREATER
            ArgumentOutOfRangeException.ThrowIfNegative(value, paramName);
#elif NETCOREAPP3_0_OR_GREATER
            if(value < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, value, $"{paramName} ('{value}') must be a non-negative value.");
            }
#else
            if(value < 0)
            {
                throw new ArgumentOutOfRangeException(null, value, $" ('{value}') must be a non-negative value.");
            }
#endif

            return value;
        }

        /// <summary>
        /// Determines whether the <see cref="StringSplitOptions.RemoveEmptyEntries"/> bit field is set in the current instance.
        /// </summary>
        /// <param name="options">The <see cref="StringSplitOptions"/> instance to test.</param>
        /// <returns><see langword="true"/> if <paramref name="options"/> has the <see cref="StringSplitOptions.RemoveEmptyEntries"/> bit field set; <see langword="false"/> otherwise.</returns>
        public static bool IsRemoveEmptyEntriesSet(this StringSplitOptions options)
        {
            return options.HasFlag(StringSplitOptions.RemoveEmptyEntries);
        }

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
#if NET5_0_OR_GREATER
            return options.HasFlag(StringSplitOptions.TrimEntries);
#else
            return false;
#endif
        }
    }
}
