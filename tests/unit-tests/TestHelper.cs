using System.Diagnostics;

namespace SpanExtensions.Tests.UnitTests
{
    public static class TestHelper
    {
        /// <summary>
        /// Verifies that two jagged sequences are equivalent, using a default comparer.
        /// </summary>
        /// <remarks>
        /// This is a wrapper for <see cref="Assert.Equal{T}(IEnumerable{T}, IEnumerable{T})"/> that improves tests messages on failure.
        /// </remarks>
        /// <typeparam name="T">The type of the objects to be compared.</typeparam>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The value to be compared against.</param>
		/// <exception cref="Xunit.Sdk.EqualException">Thrown when the objects are not equal.</exception>
        public static void AssertEqual<T>(IEnumerable<IEnumerable<T>> expected, IEnumerable<IEnumerable<T>> actual)
        {
            if(expected is not T[][])
            {
                expected = expected.Select(x => x.ToArray()).ToArray();
            }
            if(actual is not T[][])
            {
                actual = actual.Select(x => x.ToArray()).ToArray();
            }

            Assert.Equal<object>(expected, actual);
        }

        /// <summary>
        /// Get an array with all permutations of the <see cref="StringSplitOptions"/> enum flags.
        /// </summary>
        /// <returns>All permutations of the <see cref="StringSplitOptions"/> enum flags.</returns>
        public static StringSplitOptions[] GetAllStringSplitOptions()
        {
#if NET5_0_OR_GREATER
            // ensure that no new option was added in an update
            Debug.Assert(Enumerable.SequenceEqual(
                (StringSplitOptions[])Enum.GetValues(typeof(StringSplitOptions)),
                [StringSplitOptions.None, StringSplitOptions.RemoveEmptyEntries, StringSplitOptions.TrimEntries]
            ));

            return [
                StringSplitOptions.None,
                StringSplitOptions.RemoveEmptyEntries,
                StringSplitOptions.TrimEntries,
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
            ];
#else
            return [
                StringSplitOptions.None,
                StringSplitOptions.RemoveEmptyEntries
            ];
#endif
        }
    }
}
