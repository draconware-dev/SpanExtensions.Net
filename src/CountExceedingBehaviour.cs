﻿using System;
using System.Globalization;

namespace SpanExtensions
{
    /// <summary>
    /// Defines the behaviour of a split operation when there are more split instances than desired.
    /// </summary>
    public enum CountExceedingBehaviour
    {
        /// <summary>
        /// The last split returned will include all the remaining elements.
        /// </summary>
        AppendRemainingElements,
        /// <summary>
        /// Splits after the desired split count will be cut.
        /// </summary>
        CutRemainingElements
    }

    /// <summary>
    /// Extension methods for <see cref="CountExceedingBehaviour"/>.
    /// </summary>
    static class CountExceedingBehaviourExtensions
    {
        static string? CountExceedingBehaviourInvalidFormat;

        /// <summary>
        /// Validates whether a specified value is a valid <see cref="CountExceedingBehaviour"/>.
        /// </summary>
        /// <param name="countExceedingBehaviour">The <see cref="CountExceedingBehaviour"/> to validate.</param>
        /// <returns>The specified <see cref="CountExceedingBehaviour"/> if valid.</returns>
        /// <exception cref="ArgumentException">If <paramref name="countExceedingBehaviour"/> is not valid.</exception>
        public static CountExceedingBehaviour ThrowIfInvalid(this CountExceedingBehaviour countExceedingBehaviour)
        {
#if NET5_0_OR_GREATER
            if(!Enum.IsDefined(countExceedingBehaviour))
#else
            if(!Enum.IsDefined(typeof(CountExceedingBehaviour), countExceedingBehaviour))
#endif
            {
                if(CountExceedingBehaviourInvalidFormat == null)
                {
#if NET5_0_OR_GREATER
                    string[] names = Enum.GetNames<CountExceedingBehaviour>();
#else
                    string[] names = Enum.GetNames(typeof(CountExceedingBehaviour));
#endif
                    CountExceedingBehaviourInvalidFormat = $"{nameof(CountExceedingBehaviour)} doesn't define an option with the value '{{0}}'. Valid values are {string.Join(", ", names)}.";
                }

                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, CountExceedingBehaviourInvalidFormat, (int)countExceedingBehaviour), nameof(countExceedingBehaviour));
            }

            return countExceedingBehaviour;
        }

        /// <summary>
        /// Determines whether the current instance is <see cref="CountExceedingBehaviour.AppendRemainingElements"/>.
        /// </summary>
        /// <param name="countExceedingBehaviour">The <see cref="CountExceedingBehaviour"/> instance to test.</param>
        /// <returns><see langword="true"/> if <paramref name="countExceedingBehaviour"/> is <see cref="CountExceedingBehaviour.AppendRemainingElements"/>; <see langword="false"/> otherwise.</returns>
        public static bool IsAppendRemainingElements(this CountExceedingBehaviour countExceedingBehaviour)
        {
            return countExceedingBehaviour == CountExceedingBehaviour.AppendRemainingElements;
        }

        /// <summary>
        /// Determines whether the current instance is <see cref="CountExceedingBehaviour.CutRemainingElements"/>.
        /// </summary>
        /// <param name="countExceedingBehaviour">The <see cref="CountExceedingBehaviour"/> instance to test.</param>
        /// <returns><see langword="true"/> if <paramref name="countExceedingBehaviour"/> is <see cref="CountExceedingBehaviour.CutRemainingElements"/>; <see langword="false"/> otherwise.</returns>
        public static bool IsCutRemainingElements(this CountExceedingBehaviour countExceedingBehaviour)
        {
            return countExceedingBehaviour == CountExceedingBehaviour.CutRemainingElements;
        }
    }
}
