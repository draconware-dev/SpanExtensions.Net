using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using SpanExtensions;

static class ExceptionHelpers
{
    internal static void ThrowIfGreaterThanOrEqual<T>(T value, T other,
#if NET8_0_OR_GREATER
        [CallerArgumentExpression(nameof(value))] 
#endif
    string? paramName = null) where T : IComparable<T>
    {
        if(value.CompareTo(other) >= 0)
        {
            ThrowGreaterThanOrEqual(value, other, paramName);
        }
    }
    internal static void ThrowIfLessThan<T>(T value, T other,
#if NET8_0_OR_GREATER
        [CallerArgumentExpression(nameof(value))] 
#endif
    string? paramName = null) where T : IComparable<T>
    {
        if(value.CompareTo(other) < 0)
        {
            ThrowLessThan(value, other, paramName);
        }
    }

    internal static void ThrowIfInvalid(CountExceedingBehaviour countExceedingBehaviour,
#if NET8_0_OR_GREATER
        [CallerArgumentExpression(nameof(countExceedingBehaviour))] 
#endif
    string? paramName = null)
    {
#if NET5_0_OR_GREATER
        if(!Enum.IsDefined(countExceedingBehaviour))
#else
        if(!Enum.IsDefined(typeof(CountExceedingBehaviour), countExceedingBehaviour))
#endif
        {
#if NET5_0_OR_GREATER
            string[] names = Enum.GetNames<CountExceedingBehaviour>();
#else
            string[] names = Enum.GetNames(typeof(CountExceedingBehaviour));
#endif
            throw new ArgumentException($"{nameof(CountExceedingBehaviour)} does not define an option with the value '{countExceedingBehaviour}'. Valid options are {string.Join(", ", names)}.", nameof(countExceedingBehaviour));
        }
    }

    internal static void ThrowIfOutOfBounds<T>(T value, T lowerBound, T upperBound,
#if NET8_0_OR_GREATER
        [CallerArgumentExpression(nameof(value))] 
#endif
    string? paramName = null) where T : IComparable<T>
    {
        ThrowIfGreaterThanOrEqual(value, upperBound, paramName);
        ThrowIfLessThan(value, lowerBound, paramName);
    }

#if NET7_0_OR_GREATER
    internal static void ThrowIfOutOfArrayBounds<T>(T value, T upperBound,
#if NET8_0_OR_GREATER
        [CallerArgumentExpression(nameof(value))] 
#endif
    string? paramName = null) where T : INumber<T>
    {
        ThrowIfGreaterThanOrEqual(value, upperBound, paramName);
        ThrowIfNegative(value, paramName);
    }

    internal static void ThrowIfNegative<T>(T value,
#if NET8_0_OR_GREATER
        [CallerArgumentExpression(nameof(value))] 
#endif
    string? paramName = null) where T : INumberBase<T>
    {
        if(T.IsNegative(value))
        {
            ThrowNegative(value, paramName);
        }
    }

#else
    internal static void ThrowIfOutOfArrayBounds(int value, int upperBound,
#if NET8_0_OR_GREATER
        [CallerArgumentExpression(nameof(value))] 
#endif
    string? paramName = null)
    {
        ThrowIfGreaterThanOrEqual(value, upperBound, paramName);
        ThrowIfNegative(value, paramName);
    }

    internal static void ThrowIfNegative(int value,
#if NET8_0_OR_GREATER
        [CallerArgumentExpression(nameof(value))] 
#endif
    string? paramName = null)
    {
        if(value < 0)
        {
            ThrowNegative(value, paramName);
        }
    }
#endif

    [DoesNotReturn]
    static void ThrowGreaterThanOrEqual<T>(T value, T other, string? paramName)
    {
        throw new ArgumentOutOfRangeException(paramName, value, $"{paramName} ('{value}') must be less than '{other}'. (Parameter '{paramName}')");
    }
    [DoesNotReturn]
    static void ThrowLessThan<T>(T value, T other, string? paramName)
    {
        throw new ArgumentOutOfRangeException(paramName, value, $"{paramName} ('{value}') must be greater than  or equal '{other}'. (Parameter '{paramName}')");
    }

    [DoesNotReturn]
    static void ThrowNegative<T>(T value, string? paramName)
    {
        throw new ArgumentOutOfRangeException(paramName, value, $"{paramName} ('{value}')  must be a non-negative value. (Parameter '{paramName}')");
    }
}