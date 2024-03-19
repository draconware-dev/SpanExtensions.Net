using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;

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