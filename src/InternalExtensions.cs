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
    }
}
