using System;

namespace SpanExtensions
{
    /// <summary>
    /// Defines the behaviour of a split operation when there are more split instances than there may be.
    /// </summary>
    public enum CountExceedingBehaviour
    {
        /// <summary>
        /// The last element returned will be all the remaining elements appended as one.
        /// </summary>
        AppendLastElements,
        /// <summary>
        /// Every split instance more than permitted will not be returned.
        /// </summary>
        CutLastElements
    }

    /// <summary>
    /// Extension methods for <see cref="CountExceedingBehaviour"/>.
    /// </summary>
    static class CountExceedingBehaviourExtensions
    {
        /// <summary>
        /// Validates whether a specified value is a valid <see cref="CountExceedingBehaviour"/>.
        /// </summary>
        /// <param name="countExceedingBehaviour">The <see cref="CountExceedingBehaviour"/> to validate.</param>
        /// <returns>The specified <see cref="CountExceedingBehaviour"/> if valid.</returns>
        /// <exception cref="InvalidCountExceedingBehaviourException">If <paramref name="countExceedingBehaviour"/> is not valid.</exception>
        public static CountExceedingBehaviour ThrowIfInvalid(this CountExceedingBehaviour countExceedingBehaviour)
        {
#if NET5_0_OR_GREATER
            if(!Enum.IsDefined(countExceedingBehaviour))
#else
            if(!Enum.IsDefined(typeof(CountExceedingBehaviour), countExceedingBehaviour))
#endif
            {
                throw new InvalidCountExceedingBehaviourException(countExceedingBehaviour);
            }

            return countExceedingBehaviour;
        }
    }
}