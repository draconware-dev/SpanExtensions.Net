using System;

namespace SpanExtensions
{
    /// <summary>
    /// The Exception that is thrown for an undefined enumeration value of <see cref="CountExceedingBehaviour"/>.
    /// </summary>
    [Serializable]
    public class InvalidCountExceedingBehaviourException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCountExceedingBehaviourException"/> class with a system-supplied message.
        /// </summary>
        /// <param name="countExceedingBehaviour"> The invalid <see cref="CountExceedingBehaviour"/>.  </param>
        public InvalidCountExceedingBehaviourException(CountExceedingBehaviour countExceedingBehaviour) :
            base($"CountExceedingBehaviour with ID '{(int) countExceedingBehaviour} is not defined. CountExceedingBehaviour only defines {GetCountExceedingBehaviourNamesListed()}.")
        {
            
        }

        static string GetCountExceedingBehaviourNamesListed()
        {
            string[] countExceedingBehaviourNames = Array.Empty<string>();
#if NET5_0_OR_GREATER
            countExceedingBehaviourNames = Enum.GetNames<CountExceedingBehaviour>();
#else
            countExceedingBehaviourNames = (string[]) Enum.GetNames(typeof(CountExceedingBehaviour));
#endif
            switch(countExceedingBehaviourNames.Length)
            {
                case 0:
                    return "";
                case 1:
                    return countExceedingBehaviourNames[0];
                default:
                    string first = countExceedingBehaviourNames[0]; 
                    string end = string.Join(',', countExceedingBehaviourNames, 1, countExceedingBehaviourNames.Length - 1);
                    return $"{first} and {end}";
            }

        }
    }
}