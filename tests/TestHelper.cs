using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Tests
{
    public static class TestHelper
    {
        public static readonly Random random = new();

        /// <summary>
        /// Generates a sequence of a specified number of random integers that are in the specified range.
        /// </summary>
        /// <param name="count">The number of integers to generate.</param>
        /// <param name="minValue">The inclusive lower bound of the random numbers returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. <paramref name="maxValue"/> must be greater than or equal to <paramref name="minValue"/>.</param>
        /// <returns>A sequence of random integers.</returns>
        public static IEnumerable<int> GenerateRandomIntegers(int count, int minValue, int maxValue)
        {
            for(int i = 0; i < count; i++)
            {
                yield return random.Next(minValue, maxValue);
            }
        }

        /// <summary>
        /// Generates a random string of a specified length. The string is generated from an alphabet of lowercase letters, numbers, and white spaces.
        /// </summary>
        /// <param name="length">The length of the generated string.</param>
        /// <returns>A random string.</returns>
        public static string GenerateRandomString(int length)
        {
            const string alphabet = "abcdefghijklmnopqrstuvwxyz0123456789 ";

            StringBuilder builder = new(length);
            for(int i = 0; i < length; i++)
            {
                int alphabetIndex = random.Next(alphabet.Length);
                builder.Append(alphabet[alphabetIndex]);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Generates a message to be displayed when an assertion fails containing the necessary information to reproduce the failure.
        /// </summary>
        /// <typeparam name="T">The element type of the source span.</typeparam>
        /// <param name="source">The elements inside the source span.</param>
        /// <param name="expected">The expected results.</param>
        /// <param name="actual">The actual results.</param>
        /// <param name="method">The method that failed an assertion.</param>
        /// <param name="args">The argument names and values of the method that failed an asserion.</param>
        /// <returns>The message to be displayed when assertion fails.</returns>
        public static string GenerateAssertionMessage<T>(IEnumerable<T> source, IEnumerable<IEnumerable<T>> expected, IEnumerable<IEnumerable<T>> actual, string method, params (string argName, object? argValue)[] args)
        {
            static void AppendKeyValues(StringBuilder builder, string key, params (string subkey, object? value)[] keyValues)
            {
                static IEnumerable<object?> Enumerate(IEnumerable enumerable)
                {
                    List<object?> list = [];

                    IEnumerator enumerator = enumerable.GetEnumerator();
                    while(enumerator.MoveNext())
                    {
                        list.Add(enumerator.Current);
                    }

                    return list;
                }

                static string ToString(object? obj)
                {
                    return obj switch
                    {
                        null => "null",
                        IEnumerable enumerable => '[' + string.Join(", ", Enumerate(enumerable).Select(x => ToString(x))) + ']',
                        _ => obj.ToString()
                    } ?? "null";
                }

                builder.Append(key).AppendLine(":");
                foreach((string subkey, object ? value) in keyValues)
                {
                    builder.Append('\t').Append(subkey).Append(": ").AppendLine(ToString(value));
                }
            }

            static void AppendNestedEnumerable(StringBuilder builder, string key, IEnumerable<IEnumerable<T>> nestedEnumerable)
            {
                builder.Append(key).AppendLine(": [");
                bool emptyCollection = true;
                foreach(IEnumerable<T> enumerable in nestedEnumerable)
                {
                    emptyCollection = false;
                    builder.Append("\t[").AppendJoin(", ", enumerable).AppendLine("],");
                }
                builder.Length -= Environment.NewLine.Length + (!emptyCollection ? 1 : 0); // remove the last endline and comma
                builder.AppendLine().AppendLine("]");
            }

            StringBuilder builder = new();

            builder.AppendLine();
            builder.Append("Runtime Version: ").AppendLine(Environment.Version.ToString());
            builder.Append("Source: [").AppendJoin(", ", source).AppendLine("]");
            builder.Append("Method: ").AppendLine(method);
            AppendKeyValues(builder, "Arguments", args);
            AppendNestedEnumerable(builder, "Expected", expected);
            AppendNestedEnumerable(builder, "Actual", actual);

            return builder.ToString();
        }

        /// <summary>
        /// Tests whether the specified nested collections are equal.
        /// </summary>
        /// <typeparam name="T">The element type in the compared collections.</typeparam>
        /// <param name="first">The first collection to compare.</param>
        /// <param name="second">The second collection to compare.</param>
        /// <returns><see langword="true"/> if both collections are equal; otherwise <see langword="false"/>.</returns>
        public static bool SequencesEqual<T>(IEnumerable<IEnumerable<T>> first, IEnumerable<IEnumerable<T>> second) where T : IEquatable<T>
        {
            IEnumerator<IEnumerable<T>> firstEnumerator = first.GetEnumerator();
            IEnumerator<IEnumerable<T>> secondEnumerator = second.GetEnumerator();

            while(firstEnumerator.MoveNext())
            {
                if(!secondEnumerator.MoveNext()) // first is longer
                {
                    return false;
                }

                IEnumerator<T> firstSubEnumerator = firstEnumerator.Current.GetEnumerator();
                IEnumerator<T> secondSubEnumerator = secondEnumerator.Current.GetEnumerator();

                while(firstSubEnumerator.MoveNext())
                {
                    if(!secondSubEnumerator.MoveNext()) // first is larger
                    {
                        return false;
                    }

                    if(!firstSubEnumerator.Current.Equals(secondSubEnumerator.Current))
                    {
                        return false;
                    }
                }

                if(secondSubEnumerator.MoveNext()) // second is larger
                {
                    return false;
                }
            }

            if(secondEnumerator.MoveNext()) // second is larger
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Tests whether calling the specified method on the specified source with the specified arguments results in the expected result.
        /// </summary>
        /// <typeparam name="T">The element type of the source span.</typeparam>
        /// <param name="expected">The expected results.</param>
        /// <param name="actual">The actual results.</param>
        /// <param name="source">The elements inside the source span.</param>
        /// <param name="method">The method that was called.</param>
        /// <param name="args">The argument names and values of the method that was called.</param>
        public static void AssertMethodResults<T>(IEnumerable<IEnumerable<T>> expected, IEnumerable<IEnumerable<T>> actual, IEnumerable<T> source, string method, params (string argName, object? argValue)[] args) where T : IEquatable<T>
        {
            if (!SequencesEqual(expected, actual))
            {
                Assert.Fail(GenerateAssertionMessage(source, expected, actual, method, args));
            }
        }

#if !NET8_0_OR_GREATER
        /// <summary>Counts the number of times the specified <paramref name="value"/> occurs in the <paramref name="span"/>.</summary>
        /// <typeparam name="T">The element type of the span.</typeparam>
        /// <param name="span">The span to search.</param>
        /// <param name="value">The value for which to search.</param>
        /// <returns>The number of times <paramref name="value"/> was found in the <paramref name="span"/>.</returns>
        public static int Count<T>(this ReadOnlySpan<T> span, T value) where T : IEquatable<T>
        {
            int count = 0;
            foreach(T item in span)
            {
                if(item.Equals(value))
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>Counts the number of times the specified <paramref name="value"/> occurs in the <paramref name="span"/>.</summary>
        /// <typeparam name="T">The element type of the span.</typeparam>
        /// <param name="span">The span to search.</param>
        /// <param name="value">The value for which to search.</param>
        /// <returns>The number of times <paramref name="value"/> was found in the <paramref name="span"/>.</returns>
        public static int Count<T>(this Span<T> span, T value) where T : IEquatable<T>
        {
            return Count((ReadOnlySpan<T>)span, value);
        }
#endif

        /// <summary>Counts the number of times any of the specified <paramref name="values"/> occur in the <paramref name="span"/>.</summary>
        /// <typeparam name="T">The element type of the span.</typeparam>
        /// <param name="span">The span to search.</param>
        /// <param name="values">The values for which to search.</param>
        /// <returns>The number of times any of the <paramref name="values"/> was found in the <paramref name="span"/>.</returns>
        public static int Count<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>
        {
            int count = 0;
            foreach(T item in span)
            {
                if(values.Contains(item))
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>Counts the number of times any of the specified <paramref name="values"/> occur in the <paramref name="span"/>.</summary>
        /// <typeparam name="T">The element type of the span.</typeparam>
        /// <param name="span">The span to search.</param>
        /// <param name="values">The values for which to search.</param>
        /// <returns>The number of times any of the <paramref name="values"/> was found in the <paramref name="span"/>.</returns>
        public static int Count<T>(this Span<T> span, ReadOnlySpan<T> values) where T : IEquatable<T>
        {
            return Count((ReadOnlySpan<T>)span, values);
        }

        /// <summary>
        /// Counts the number of times any of the specified <paramref name="substring"/> occur in the <paramref name="string"/>.
        /// </summary>
        /// <param name="string">The string to search.</param>
        /// <param name="substring">The substring for which to search.</param>
        /// <returns>The number of times any of the <paramref name="substring"/> was found in the <paramref name="string"/>.</returns>
        public static int Count(this string @string, string substring)
        {
#if NET7_0_OR_GREATER
            return Regex.Count(@string, substring);
#else
            return Regex.Matches(@string, substring).Count;
#endif
        }

        /// <summary>Counts the number of times the specified <paramref name="value"/> subsequence occurs in the <paramref name="span"/>.</summary>
        /// <typeparam name="T">The element type of the span.</typeparam>
        /// <param name="span">The span to search.</param>
        /// <param name="value">The value subsequence for which to search.</param>
        /// <returns>The number of times <paramref name="value"/> subsequence was found in the <paramref name="span"/>.</returns>
        public static int CountSequence<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> value)
        {
            string source = string.Join(",", span.ToArray());
            string substring = string.Join(",", value.ToArray());

            return source.Count(substring);
        }

        /// <summary>Counts the number of times the specified <paramref name="value"/> subsequence occurs in the <paramref name="span"/>.</summary>
        /// <typeparam name="T">The element type of the span.</typeparam>
        /// <param name="span">The span to search.</param>
        /// <param name="value">The value subsequence for which to search.</param>
        /// <returns>The number of times <paramref name="value"/> subsequence was found in the <paramref name="span"/>.</returns>
        public static int CountSequence<T>(this Span<T> span, ReadOnlySpan<T> value)
        {
            return CountSequence((ReadOnlySpan<T>)span, value);
        }

        /// <summary>
        /// Splits a sequence into a maximum number of subsequences based on a specified delimiter.
        /// </summary>
        /// <typeparam name="T">The element type of the sequence.</typeparam>
        /// <param name="source">The sequence to be split.</param>
        /// <param name="delimiter">A <typeparamref name="T"/> that delimits the subsequences in <paramref name="source"/>.</param>
        /// <param name="count">The maximum number of splits. If zero, split on every occurence of <paramref name="delimiter"/>.</param>
        /// <returns>A sequence of split subsequences.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="count"/> is negative.</exception>
        public static IEnumerable<IEnumerable<T>> Split<T>(IEnumerable<T> source, T delimiter, int count = 0) where T : IEquatable<T>
        {
#if NET8_0_OR_GREATER
            ArgumentOutOfRangeException.ThrowIfNegative(count);
#else
            if(count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
#endif

            List<T> segment = [];

            foreach(T element in source)
            {
                if(count == 1 || !element.Equals(delimiter))
                {
                    segment.Add(element);
                }
                else
                {
                    yield return segment;
                    segment = [];
                    count--;
                }
            }

            yield return segment;
        }

        /// <summary>
        /// Splits a sequence into a maximum number of subsequences based on specified delimiters.
        /// </summary>
        /// <typeparam name="T">The element type of the sequence.</typeparam>
        /// <param name="source">The sequence to be split.</param>
        /// <param name="delimiters">A <see cref="IEnumerable{T}"/> with the instances of <typeparamref name="T"/> that delimit the subsequences in <paramref name="source"/>.</param>
        /// <param name="count">The maximum number of splits. If zero, split on every occurence of <paramref name="delimiters"/>.</param>
        /// <returns>A sequence of split subsequences.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="count"/> is negative.</exception>
        public static IEnumerable<IEnumerable<T>> SplitAny<T>(IEnumerable<T> source, IEnumerable<T> delimiters, int count = 0) where T : IEquatable<T>
        {
#if NET8_0_OR_GREATER
            ArgumentOutOfRangeException.ThrowIfNegative(count);
#else
            if(count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
#endif

            List<T> segment = [];

            foreach(T element in source)
            {
                if(count == 1 || delimiters.All(delimiter => !element.Equals(delimiter)))
                {
                    segment.Add(element);
                }
                else
                {
                    yield return segment;
                    segment = [];
                    count--;
                }
            }

            yield return segment;
        }

        /// <summary>
        /// Splits a sequence into a maximum number of subsequences based on specified delimiter subsequence.
        /// </summary>
        /// <param name="source">The sequence to be split.</param>
        /// <param name="delimiter">A <see cref="IEnumerable{int}"/> that delimits the subsequences in <paramref name="source"/>.</param>
        /// <param name="count">The maximum number of splits. If zero, split on every occurence of <paramref name="delimiter"/>.</param>
        /// <returns>A sequence of split subsequences.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="count"/> is negative.</exception>
        public static IEnumerable<IEnumerable<int>> Split(IEnumerable<int> source, IEnumerable<int> delimiter, int count = 0)
        {
#if NET8_0_OR_GREATER
            ArgumentOutOfRangeException.ThrowIfNegative(count);
#else
            if(count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
#endif

            string _source = string.Join(",", source);
            string _delimiter = string.Join(",", delimiter);

            string[] splits = _source.Split(_delimiter, count == 0 ? int.MaxValue : count, StringSplitOptions.None);

            return splits.Select(s => s.Trim(',').Split(',').Select(x => int.Parse(x, CultureInfo.InvariantCulture)));
        }

        /// <summary>
        /// Splits a sequence into a maximum number of subsequences based on specified delimiter subsequence.
        /// </summary>
        /// <param name="source">The sequence to be split.</param>
        /// <param name="delimiter">A <see cref="IEnumerable{char}"/> that delimits the subsequences in <paramref name="source"/>.</param>
        /// <param name="count">The maximum number of splits. If zero, split on every occurence of <paramref name="delimiter"/>.</param>
        /// <returns>A sequence of split subsequences.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="count"/> is negative.</exception>
        public static IEnumerable<IEnumerable<char>> Split(IEnumerable<char> source, IEnumerable<char> delimiter, int count = 0)
        {
#if NET8_0_OR_GREATER
            ArgumentOutOfRangeException.ThrowIfNegative(count);
#else
            if(count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
#endif

            string _source = string.Join(",", source);
            string _delimiter = string.Join(",", delimiter);

            string[] splits = _source.Split(_delimiter, count == 0 ? int.MaxValue : count, StringSplitOptions.None);

            return splits.Select(s => s.Trim(',').Split(',').Select(x => x[0]));
        }

        /// <summary>
        /// Get an <see cref="IEnumerable{StringSplitOptions}"/> with all permutations of the <see cref="StringSplitOptions"/> enum flags.
        /// </summary>
        /// <returns>All permutations of the <see cref="StringSplitOptions"/> enum flags.</returns>
        /// <remarks>This implementation assumes that all bits below the largest flag are used.</remarks>
        public static IEnumerable<StringSplitOptions> GetAllStringSplitOptions()
        {
            static bool IsPowerOfTwo(int value)
            {
                return value != 0 && (value & (value - 1)) == 0;
            }

            static int GetNearestPowerOfTwo(int value) // nearest down, i.e. highest power of 2 less than or equal to the given number
            {
                int power = (int)Math.Log2(value);
                return (int)Math.Pow(2, power);
            }

            static IEnumerable<int> PowersOfTwo(int max)
            {
                while(max != 0)
                {
                    yield return max;
                    max /= 2;
                }
                yield return 0;
            }

            int largestEnumValue = Enum.GetValues(typeof(StringSplitOptions)).Cast<int>().Max();
            int largestOneBitFlag = IsPowerOfTwo(largestEnumValue) ? largestEnumValue : GetNearestPowerOfTwo(largestEnumValue);

            Debug.Assert(PowersOfTwo(largestOneBitFlag).All(x => Enum.IsDefined(typeof(StringSplitOptions), x))); // ensure that all bits below highestEnumValue are used, otherwise the following is not valid

            for(int flag = 0; flag < largestOneBitFlag * 2; flag++)
            {
                yield return (StringSplitOptions)flag;
            }
        }
    }
}
