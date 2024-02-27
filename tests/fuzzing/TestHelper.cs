using SpanExtensions;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace SpanExtensions.Tests.Fuzzing
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
        /// Get a random element from the specified sequence, or a specified default value if the source is empty.
        /// </summary>
        /// <typeparam name="T">The type of the sequence array.</typeparam>
        /// <param name="source">The target sequence.</param>
        /// <param name="default">The value to return if <paramref name="source"/> is empty.</param>
        /// <returns>A random element from <paramref name="source"/>, or <paramref name="default"/> if <paramref name="source"/> is empty.</returns>
        public static T RandomElementOrDefault<T>(this ReadOnlySpan<T> source, T @default = default) where T : struct
        {
            return !source.IsEmpty ? source[random.Next(source.Length)] : @default;
        }

        /// <summary>
        /// Get a random element from the specified array, or a specified default value if the array is empty.
        /// </summary>
        /// <typeparam name="T">The type of the target array.</typeparam>
        /// <param name="array">The target array.</param>
        /// <param name="default">The value to return if <paramref name="array"/> is empty.</param>
        /// <returns>A random element from <paramref name="array"/>, or <paramref name="default"/> if the array is empty.</returns>
        public static T RandomElementOrDefault<T>(this T[] array, T @default = default) where T : struct
        {
            return RandomElementOrDefault(array.AsReadOnlySpan(), @default);
        }

        /// <summary>
        /// Get a random element from the specified string, or a specified default character if the string is empty.
        /// </summary>
        /// <param name="string">The target string.</param>
        /// <param name="default">The value to return if <paramref name="string"/> is empty.</param>
        /// <returns>A random element from <paramref name="string"/>, or <paramref name="default"/> if <paramref name="string"/> is empty.</returns>
        public static char RandomElementOrDefault(this string @string, char @default = default)
        {
            return RandomElementOrDefault(@string.AsSpan(), @default);
        }

        /// <summary>
        /// Get a random element subsequence of the specified length from the specified sequence,
        /// or a specified default sequence if the source is empty or shorter than the specified length.
        /// </summary>
        /// <typeparam name="T">The type of the target sequence.</typeparam>
        /// <param name="source">The target sequence.</param>
        /// <param name="length">The length of the subsequence.</param>
        /// <param name="default">The value to return if <paramref name="source"/> is empty or shorter than <paramref name="length"/>.</param>
        /// <returns>A random subsequence of length <paramref name="length"/>, or <paramref name="default"/> if <paramref name="source"/> is empty or shorted than that length.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="length"/> is negative.</exception>
        /// <exception cref="ArgumentException">If the length of <paramref name="default"/> did not match <paramref name="length"/>.</exception>
        public static T[] RandomSubsequenceOrDefault<T>(this ReadOnlySpan<T> source, int length, T[]? @default = null) where T : struct
        {
            if(length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length), "Can't be negative.");
            }
            if(@default != null && @default.Length != length)
            {
                throw new ArgumentException("The length must match.", nameof(@default));
            }

            if(source.Length < length)
            {
                return @default ?? new T[length];
            }

            int startIndex = random.Next(source.Length - length);
            return source[startIndex..(startIndex + length)].ToArray();
        }

        /// <summary>
        /// Get a random element subsequence of the specified length from the specified array,
        /// or a specified default sequence if the source is empty or shorter than the specified length.
        /// </summary>
        /// <typeparam name="T">The type of the target array.</typeparam>
        /// <param name="array">The target array.</param>
        /// <param name="length">The length of the subsequence.</param>
        /// <param name="default">The value to return if <paramref name="array"/> is empty or shorter than <paramref name="length"/>.</param>
        /// <returns>A random subsequence of length <paramref name="length"/>, or <paramref name="default"/> if the array is empty or shorted than that length.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="length"/> is negative.</exception>
        /// <exception cref="ArgumentException">If the length of <paramref name="default"/> did not match <paramref name="length"/>.</exception>
        public static T[] RandomSubsequenceOrDefault<T>(this T[] array, int length, T[]? @default = null) where T : struct
        {
            return RandomSubsequenceOrDefault(array.AsReadOnlySpan(), length, @default);
        }

        /// <summary>
        /// Get a random element subsequence of the specified length from the specified string,
        /// or a specified default sequence if the source is empty or shorter than the specified length.
        /// </summary>
        /// <param name="string">The target string.</param>
        /// <param name="length">The length of the string.</param>
        /// <param name="default">The value to return if <paramref name="string"/> is empty or shorter than <paramref name="length"/>.</param>
        /// <returns>A random subsequence of length <paramref name="length"/>, or <paramref name="default"/> if <paramref name="string"/> is empty or shorted than that length.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="length"/> is negative.</exception>
        /// <exception cref="ArgumentException">If the length of <paramref name="default"/> did not match <paramref name="length"/>.</exception>
        public static char[] RandomSubsequenceOrDefault(this string @string, int length, char[]? @default = null)
        {
            return RandomSubsequenceOrDefault(@string.AsSpan(), length, @default);
        }

        /// <summary>
        /// Replaces a random element in the array with the specified replacement.
        /// </summary>
        /// <typeparam name="T">The type of the target array.</typeparam>
        /// <param name="source">The target array.</param>
        /// <param name="replacement">The element to replace with.</param>
        /// <returns>A copy of <paramref name="source"/> with the element at a random position replaced with <paramref name="replacement"/>.</returns>
        public static T[] ReplaceRandomElement<T>(this T[] source, T replacement)
        {
            T[] copy = new T[source.Length];
            source.CopyTo(copy, 0);

            if(source.Length != 0)
            {
                copy[random.Next(source.Length)] = replacement;
            }

            return copy;
        }

        /// <summary>
        /// Replaces the element in the specified position with the specified replacement.
        /// </summary>
        /// <typeparam name="T">The type of the target array.</typeparam>
        /// <param name="source">The target array.</param>
        /// <param name="position">The position of the element to replace with.</param>
        /// <param name="replacement">The element to replace with.</param>
        /// <returns>A copy of <paramref name="source"/> with the element at <paramref name="position"/> replaced with <paramref name="replacement"/>.</returns>
        public static T[] ReplaceAt<T>(this T[] source, int position, T replacement)
        {
            T[] copy = new T[source.Length];
            source.CopyTo(copy, 0);
            copy[position] = replacement;
            return copy;
        }

        /// <summary>
        /// An alternative to <see cref="Enumerable.Range(int, int)"/> that multiplies the current element by a multiplier every time.
        /// </summary>
        /// <param name="start">The first element.</param>
        /// <param name="max">The maximum the last element can be.</param>
        /// <param name="multiplier">The multiplier.</param>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="start"/> isn't positive, or if <paramref name="max"/> or <paramref name="multiplier"/> is negative.</exception>
        public sealed class MultiplierRange(int start, int max, int multiplier) : IEnumerable<int>
        {
            public IEnumerator<int> GetEnumerator()
            {
                if(start <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(start), "Value must be positive.");
                }
                if(max < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(max), "Value can't be negative.");
                }
                if(multiplier < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(multiplier), "Value can't be negative.");
                }

                static IEnumerator<int> Iterate(int start, int max, int multiplier)
                {
                    for(int i = start; i <= max; i *= multiplier)
                    {
                        yield return i;
                    }
                }

                return Iterate(start, max, multiplier);
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        /// <summary>
        /// Iterates over two sequence is order.
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="first">The first sequence to iterate.</param>
        /// <param name="second">THe second sequence ot iterate.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that iterates over the two sequences in order.</returns>
        public static IEnumerable<T> And<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            foreach(T item in first)
            {
                yield return item;
            }
            foreach(T item in second)
            {
                yield return item;
            }
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
                foreach((string subkey, object? value) in keyValues)
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
            if(!SequencesEqual(expected, actual))
            {
                Assert.Fail(GenerateAssertionMessage(source, expected, actual, method, args));
            }
        }

        /// <summary>
        /// Creates a new span over the target array.
        /// </summary>
        /// <typeparam name="T">The array type.</typeparam>
        /// <param name="source">The target array.</param>
        /// <returns>A <see cref="ReadOnlySpan{T}"/> over <paramref name="source"/>.</returns>
        public static ReadOnlySpan<T> AsReadOnlySpan<T>(this T[] source)
        {
            return source.AsSpan();
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

        /// <summary>Counts the number of times the specified <paramref name="value"/> occurs in the <paramref name="array"/>.</summary>
        /// <typeparam name="T">The element type of the array.</typeparam>
        /// <param name="array">The array to search.</param>
        /// <param name="value">The value for which to search.</param>
        /// <returns>The number of times <paramref name="value"/> was found in the <paramref name="array"/>.</returns>
        public static int Count<T>(this T[] array, T value) where T : IEquatable<T>
        {
            return array.AsSpan().Count(value);
        }

        /// <summary>Counts the number of times the specified <paramref name="value"/> occurs in the <paramref name="string"/>.</summary>
        /// <param name="string">The string to search.</param>
        /// <param name="value">The value for which to search.</param>
        /// <returns>The number of times <paramref name="value"/> was found in the <paramref name="string"/>.</returns>
        public static int Count(this string @string, char value)
        {
            return @string.AsSpan().Count(value);
        }

#if !NET8_0_OR_GREATER
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
#endif

        /// <summary>Counts the number of times any of the specified <paramref name="values"/> occur in the <paramref name="array"/>.</summary>
        /// <typeparam name="T">The element type of the array.</typeparam>
        /// <param name="array">The array to search.</param>
        /// <param name="values">The values for which to search.</param>
        /// <returns>The number of times any of the <paramref name="values"/> was found in the <paramref name="array"/>.</returns>
        public static int Count<T>(this T[] array, ReadOnlySpan<T> values) where T : IEquatable<T>
        {
            return array.AsSpan().Count(values);
        }

        /// <summary>Counts the number of times any of the specified <paramref name="values"/> occur in the <paramref name="string"/>.</summary>
        /// <param name="string">The string to search.</param>
        /// <param name="values">The values for which to search.</param>
        /// <returns>The number of times any of the <paramref name="values"/> was found in the <paramref name="string"/>.</returns>
        public static int Count(this string @string, ReadOnlySpan<char> values)
        {
            return @string.AsSpan().Count(values);
        }

        /// <summary>
        /// Counts the (non-overlaping) occurrences of a subsequence in the target sequence.
        /// </summary>
        /// <typeparam name="T">The element type in the sequence.</typeparam>
        /// <param name="sequence">The target sequence.</param>
        /// <param name="subsequence">The subsequence to count.</param>
        /// <returns>The number of occurrences of <paramref name="subsequence"/> in <paramref name="sequence"/>.</returns>
        public static int CountSubsequence<T>(this ReadOnlySpan<T> sequence, ReadOnlySpan<T> subsequence) where T : IEquatable<T>
        {
            if(sequence.IsEmpty || subsequence.IsEmpty)
            {
                return 0;
            }

            int count = 0, position;
            while((position = sequence.IndexOf(subsequence)) != -1)
            {
                count++;
                sequence = sequence[(position + subsequence.Length)..];
            }
            return count;
        }

        /// <summary>
        /// Counts the (non-overlaping) occurrences of a subsequence in the target array.
        /// </summary>
        /// <typeparam name="T">The element type in the array.</typeparam>
        /// <param name="array">The target array.</param>
        /// <param name="subsequence">The subsequence to count.</param>
        /// <returns>The number of occurrences of <paramref name="subsequence"/> in <paramref name="array"/>.</returns>
        public static int CountSubsequence<T>(this T[] array, ReadOnlySpan<T> subsequence) where T : IEquatable<T>
        {
            return CountSubsequence(array.AsSpan(), subsequence);
        }

        /// <summary>
        /// Counts the (non-overlaping) occurrences of a subsequence in the target string.
        /// </summary>
        /// <param name="string">The target string.</param>
        /// <param name="subsequence">The subsequence to count.</param>
        /// <returns>The number of occurrences of <paramref name="subsequence"/> in <paramref name="string"/>.</returns>
        public static int CountSubsequence(this string @string, ReadOnlySpan<char> subsequence)
        {
            return CountSubsequence(@string.AsSpan(), subsequence);
        }

        /// <summary>
        /// Returns an <see cref="Exception"/> indicating that the specified <see cref="CountExceedingBehaviour"/> value wasn't handled.
        /// </summary>
        /// <param name="countExceedingBehaviour">The <see cref="CountExceedingBehaviour"/> value that wasn't handled.</param>
        public static Exception UnhandledCaseException(CountExceedingBehaviour countExceedingBehaviour)
        {
            return new
#if NET7_0_OR_GREATER
                UnreachableException
#else
                NotImplementedException
#endif
                ($"Unhandled {nameof(CountExceedingBehaviour)} enum value: {countExceedingBehaviour}.");
        }

        /// <summary>
        /// Take up to a specified count of elements from an array.
        /// <remarks>Unlike <paramref name="source"/>[..<paramref name="count"/>], this doesn't throw an exception when <paramref name="count"/> is greater than the length of <paramref name="source"/>.</remarks>
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="source">The array to cut.</param>
        /// <param name="count">The number of elements to take.</param>
        /// <returns>The cut array.</returns>
        public static T[] UpTo<T>(this T[] source, int count)
        {
            return source.Length <= count ? source : source[..count];
        }

        /// <summary>
        /// Splits a sequence into a maximum number of subsequences based on a specified delimiter.
        /// </summary>
        /// <typeparam name="T">The element type of the sequence.</typeparam>
        /// <param name="source">The sequence to be split.</param>
        /// <param name="delimiter">A <typeparamref name="T"/> that delimits the subsequences in <paramref name="source"/>.</param>
        /// <param name="count">The maximum number of splits. If zero, split on every occurence of <paramref name="delimiter"/>.</param>
        /// <param name="countExceedingBehaviour">Specifies how the elements after count splits should be handled.</param>
        /// <returns>A sequence of split subsequences.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="count"/> is negative.</exception>
        public static IEnumerable<IEnumerable<T>> Split<T>(IEnumerable<T> source, T delimiter, int count = int.MaxValue, CountExceedingBehaviour countExceedingBehaviour = CountExceedingBehaviour.AppendRemainingElements) where T : IEquatable<T>
        {
#if NET8_0_OR_GREATER
            ArgumentOutOfRangeException.ThrowIfNegative(count);
#else
            if(count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
#endif

            if(count != 0)
            {
                List<T> segment = [];

                foreach(T element in source)
                {
                    if(count == 1 && countExceedingBehaviour == CountExceedingBehaviour.CutRemainingElements && element.Equals(delimiter))
                    {
                        break;
                    }

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
        }

        /// <summary>
        /// Splits a sequence into a maximum number of subsequences based on specified delimiters.
        /// </summary>
        /// <typeparam name="T">The element type of the sequence.</typeparam>
        /// <param name="source">The sequence to be split.</param>
        /// <param name="delimiters">A <see cref="IEnumerable{T}"/> with the instances of <typeparamref name="T"/> that delimit the subsequences in <paramref name="source"/>.</param>
        /// <param name="count">The maximum number of splits. If zero, split on every occurence of <paramref name="delimiters"/>.</param>
        /// <param name="countExceedingBehaviour">Specifies how the elements after count splits should be handled.</param>
        /// <returns>A sequence of split subsequences.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="count"/> is negative.</exception>
        public static IEnumerable<IEnumerable<T>> SplitAny<T>(IEnumerable<T> source, IEnumerable<T> delimiters, int count = int.MaxValue, CountExceedingBehaviour countExceedingBehaviour = CountExceedingBehaviour.AppendRemainingElements) where T : IEquatable<T>
        {
#if NET8_0_OR_GREATER
            ArgumentOutOfRangeException.ThrowIfNegative(count);
#else
            if(count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
#endif

            if(count != 0)
            {

                List<T> segment = [];
                foreach(T element in source)
                {
                    if(count == 1 && countExceedingBehaviour == CountExceedingBehaviour.CutRemainingElements && delimiters.Any(delimiter => element.Equals(delimiter)))
                    {
                        break;
                    }

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
        }

        /// <summary>
        /// Splits a sequence into a maximum number of subsequences based on specified delimiter subsequence.
        /// </summary>
        /// <param name="source">The sequence to be split.</param>
        /// <param name="delimiter">A <see cref="IEnumerable{T}"/> that delimits the subsequences in <paramref name="source"/>.</param>
        /// <param name="count">The maximum number of splits. If zero, split on every occurence of <paramref name="delimiter"/>.</param>
        /// <param name="countExceedingBehaviour">Specifies how the elements after count splits should be handled.</param>
        /// <returns>A sequence of split subsequences.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="count"/> is negative.</exception>
        public static IEnumerable<IEnumerable<T>> Split<T>(IEnumerable<T> source, IEnumerable<T> delimiter, int count = int.MaxValue, CountExceedingBehaviour countExceedingBehaviour = CountExceedingBehaviour.AppendRemainingElements) where T : IEquatable<T>
        {
#if NET8_0_OR_GREATER
            ArgumentOutOfRangeException.ThrowIfNegative(count);
#else
            if(count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
#endif

            if(count == 0)
            {
                return [];
            }

            int delimiterLength = delimiter.Count();
            if(delimiterLength == 0)
            {
                return [source];
            }
            else if(delimiterLength == 1)
            {
                return Split(source, delimiter.First(), count, countExceedingBehaviour);
            }

            string _source = '{' + string.Join("},{", source) + '}';
            string _delimiter = '{' + string.Join("},{", delimiter) + '}';

            string[] _splits = countExceedingBehaviour switch
            {
                CountExceedingBehaviour.AppendRemainingElements => _source.Split(_delimiter, count, StringSplitOptions.None),
                CountExceedingBehaviour.CutRemainingElements => _source.Split(_delimiter, StringSplitOptions.None).UpTo(count),
                _ => throw UnhandledCaseException(countExceedingBehaviour)
            };

            IEnumerable<IEnumerable<string>> splits = _splits.Select(s => s.Trim(',').Split(',').Where(x => x.Length != 0).Select(x => x[1..^1]).Where(x => x.Length != 0));

            return source switch
            {
                IEnumerable<int> => (IEnumerable<IEnumerable<T>>)splits.Select(s => s.Select(x => int.Parse(x, CultureInfo.InvariantCulture))),
                IEnumerable<char> => (IEnumerable<IEnumerable<T>>)splits.Select(s => s.Select(x => x[0])),
                _ => throw new NotImplementedException($"Type {typeof(T)} was not implemented.")
            };
        }

        /// <summary>
        /// Splits a string into a maximum number of substrings based on a specified delimiting string and, optionally, options.
        /// </summary>
        /// <typeparam name="T">The type of the separator. <strong>The only supported types: <see cref="char"/>, <see cref="string"/>, <see cref="char"/>[]</strong>.</typeparam>
        /// <param name="source">The string to be split.</param>
        /// <param name="separator">A char/string/cahr[] that delimits the substrings in this instance.</param>
        /// <param name="count">The maximum number of elements expected in the array.</param>
        /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim substrings and include empty substrings.</param>
        /// <param name="countExceedingBehaviour">Specifies how the elements after count splits should be handled.</param>
        /// <returns>An array that contains at most <paramref name="count"/> substrings from this instance that are delimited by <paramref name="separator"/>.</returns>
        /// <exception cref="UnreachableException"></exception>
        public static string[] Split<T>(this string source, T separator, int count = int.MaxValue, StringSplitOptions options = StringSplitOptions.None, CountExceedingBehaviour countExceedingBehaviour = CountExceedingBehaviour.AppendRemainingElements)
        {
#if NET8_0_OR_GREATER
            ArgumentOutOfRangeException.ThrowIfNegative(count);
#else
            if(count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
#endif

            if(count == 0)
            {
                return [];
            }

            // When count is 1 and RemoveEmptyEntries option is set, it's a special case where splits shouldn't be recursively removed.
            // Since string.Split doesn't have the CutRemainingElements option, we have to manually handle this.
            static string[] FirstSubstring<TSame>(string source, TSame separator, StringSplitOptions options = StringSplitOptions.None)
            {
                string first = separator switch
                {
                    char charSeparator => source.Split(charSeparator)[0],
                    string stringSeparator => source.Split(stringSeparator)[0],
                    char[] charSeparators => source.Split(charSeparators)[0],
                    _ => throw new NotSupportedException($"Invalid separator type: {typeof(T)}")
                };

#if NET5_0_OR_GREATER
                if(options.HasFlag(StringSplitOptions.TrimEntries))
                {
                    first = first.Trim();
                }
#endif

                if(options.HasFlag(StringSplitOptions.RemoveEmptyEntries) && string.IsNullOrEmpty(first))
                {
                    return [];
                }

                return [first];
            }

            return separator switch
            {
                char charSeparator => countExceedingBehaviour switch
                {
                    CountExceedingBehaviour.AppendRemainingElements => source.Split(charSeparator, count, options),
                    CountExceedingBehaviour.CutRemainingElements => count == 1 ? FirstSubstring(source, charSeparator, options) : source.Split(charSeparator, options).UpTo(count),
                    _ => throw UnhandledCaseException(countExceedingBehaviour)
                },
                string stringSeparator => countExceedingBehaviour switch
                {
                    CountExceedingBehaviour.AppendRemainingElements => source.Split(stringSeparator, count, options),
                    CountExceedingBehaviour.CutRemainingElements => count == 1 ? FirstSubstring(source, stringSeparator, options) : source.Split(stringSeparator, options).UpTo(count),
                    _ => throw UnhandledCaseException(countExceedingBehaviour)
                },
                char[] charSeparators => countExceedingBehaviour switch
                {
                    CountExceedingBehaviour.AppendRemainingElements => source.Split(charSeparators, count, options),
                    CountExceedingBehaviour.CutRemainingElements => count == 1 ? FirstSubstring(source, charSeparators, options) : source.Split(charSeparators, options).UpTo(count),
                    _ => throw UnhandledCaseException(countExceedingBehaviour)
                },
                _ => throw new NotSupportedException($"Invalid separator type: {typeof(T)}")
            };
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
