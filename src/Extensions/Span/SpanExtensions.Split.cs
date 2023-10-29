using System.Collections;

namespace SpanExtensions;

public static partial class SpanExtensions
{
    /// <summary>
    /// Splits a <see cref="ReadOnlySpan{T}"/> into multiple ReadOnlySpans based on the specified <paramref name="delimiter"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="span">The <see cref="ReadOnlySpan{T}"/> to be split.</param>
    /// <param name="delimiter">An instance of <typeparamref name="T"/> that delimits the various sub-ReadOnlySpans in the <see cref="ReadOnlySpan{T}"/></param>
    /// <returns>An instance of the ref struct <see cref="SpanSplitEnumerator{T}"/>, which works the same way as every <see cref="IEnumerator"/> does and can be used in a foreach construct.</returns>
    public static SpanSplitEnumerator<T> Split<T>(this ReadOnlySpan<T> span, T delimiter) where T : IEquatable<T>
    {
        return new SpanSplitEnumerator<T>(span, delimiter);
    }

    /// <summary>
    /// Splits a <see cref="ReadOnlySpan{T}"/> into at most <paramref name="count"/> ReadOnlySpans based on the specified <paramref name="delimiter"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="span">The <see cref="ReadOnlySpan{T}"/> to be split</param>
    /// <param name="delimiter">An instance of <typeparamref name="T"/> that delimits the various sub-ReadOnlySpans in the <see cref="ReadOnlySpan{T}"/></param>
    /// <param name="count">the maximum number of results</param>
    /// <returns>An instance of the ref struct <see cref="SpanSplitWithCountEnumerator{T}"/>, which works the same way as every <see cref="IEnumerator"/> does and can be used in a foreach construct.</returns>
    public static SpanSplitWithCountEnumerator<T> Split<T>(this ReadOnlySpan<T> span, T delimiter, int count) where T : IEquatable<T>
    {
        return new SpanSplitWithCountEnumerator<T>(span, delimiter, count);
    }

    /// <summary>
    /// Splits a <see cref="ReadOnlySpan{Char}"/> into multiple ReadOnlySpans based on the specified <paramref name="delimiter"/> and the specified <paramref name="options"/>.
    /// </summary> 
    /// <param name="span">The <see cref="ReadOnlySpan{Char}"/> to be split</param>
    /// <param name="delimiter">A <see cref="char"/> that delimits the various sub-ReadOnlySpans in the <see cref="ReadOnlySpan{Char}"/></param>
    /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim results and include empty results.</param>
    /// <returns>An instance of the ref struct <see cref="SpanSplitStringSplitOptionsEnumerator"/>, which works the same way as every <see cref="IEnumerator"/> does and can be used in a foreach construct.</returns>
    public static SpanSplitStringSplitOptionsEnumerator Split(this ReadOnlySpan<char> span, char delimiter, StringSplitOptions options)
    {
        return new SpanSplitStringSplitOptionsEnumerator(span, delimiter, options);
    }

    /// <summary>
    /// Splits a <see cref="ReadOnlySpan{Char}"/> into at most <paramref name="count"/> ReadOnlySpans based on the specified <paramref name="delimiter"/> and the specified <paramref name="options"/>. 
    /// </summary>  
    /// <param name="span">The <see cref="ReadOnlySpan{Char}"/> to be split</param>  
    /// <param name="delimiter">A <see cref="char"/> that delimits the various sub-ReadOnlySpans in the <see cref="ReadOnlySpan{Char}"/></param>  
    /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim results and include empty results.</param>  
    /// <param name="count">the maximum number of results</param>
    /// <returns>An instance of the ref struct <see cref="SpanSplitAnyStringSplitOptionsWithCountEnumerator"/>, which works the same way as every <see cref="IEnumerator"/> does and can be used in a foreach construct.</returns>
    public static SpanSplitStringSplitOptionsWithCountEnumerator Split(this ReadOnlySpan<char> span, char delimiter, StringSplitOptions options, int count)
    {
        return new SpanSplitStringSplitOptionsWithCountEnumerator(span, delimiter, options, count);
    }

    /// <summary>  
    /// Splits a <see cref="ReadOnlySpan{T}"/> into multiple ReadOnlySpans based on the any of the specified <paramref name="delimiters"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="span">The <see cref="ReadOnlySpan{T}"/> to be split</param>
    /// <param name="delimiters">A <see cref="ReadOnlySpan{T}"/>, that delimit the various sub-ReadOnlySpans in the <see cref="ReadOnlySpan{T}"/>.</param>
    /// <returns>An instance of the ref struct <see cref="SpanSplitAnyEnumerator{T}"/>, which works the same way as every <see cref="IEnumerator"/> does and can be used in a foreach construct.</returns>
    public static SpanSplitAnyEnumerator<T> SplitAny<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> delimiters) where T : IEquatable<T>
    {
        return new SpanSplitAnyEnumerator<T>(span, delimiters);
    }

    /// <summary>
    /// Splits a <see cref="ReadOnlySpan{T}"/> into at most <paramref name="count"/> ReadOnlySpans based on the any of the specified  <paramref name="delimiters"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="span">The <see cref="ReadOnlySpan{T}"/> to be split</param>
    /// <param name="delimiters">A <see cref="ReadOnlySpan{T}"/>, that delimit the various sub-ReadOnlySpans in the <see cref="ReadOnlySpan{T}"/>.</param>
    /// <param name="count">the maximum number of results</param>
    /// <returns>An instance of the ref struct <see cref="SpanSplitAnyWithCountEnumerator{T}"/>, which works the same way as every <see cref="IEnumerator"/> does and can be used in a foreach construct.</returns>
    public static SpanSplitAnyWithCountEnumerator<T> SplitAny<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> delimiters, int count) where T : IEquatable<T>
    {
        return new SpanSplitAnyWithCountEnumerator<T>(span, delimiters, count);
    }

    /// <summary>
    /// Splits a <see cref="ReadOnlySpan{Char}"/> into multiple ReadOnlySpans based on the any of the specified <paramref name="delimiters"/>.
    /// </summary>
    /// <param name="span">The <see cref="ReadOnlySpan{Char}"/> to be split</param>
    /// <param name="delimiters">A <see cref="ReadOnlySpan{Char}"/>, that delimit the various sub-ReadOnlySpans in the <see cref="ReadOnlySpan{Char}"/>.</param>
    /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim results and include empty results.</param>
    /// <returns>An instance of the ref struct <see cref="SpanSplitAnyStringSplitOptionsEnumerator"/>, which works the same way as every <see cref="IEnumerator"/> does and can be used in a foreach construct.</returns>
    public static SpanSplitAnyStringSplitOptionsEnumerator SplitAny(this ReadOnlySpan<char> span, ReadOnlySpan<char> delimiters, StringSplitOptions options)
    {
        return new SpanSplitAnyStringSplitOptionsEnumerator(span, delimiters, options);
    }

    /// <summary>
    /// Splits a <see cref="ReadOnlySpan{Char}"/> into at most <paramref name="count"/> ReadOnlySpans based on the any of the specified <paramref name="delimiters"/>.
    /// </summary>
    /// <param name="span">The <see cref="ReadOnlySpan{Char}"/> to be split</param>
    /// <param name="delimiters">A <see cref="ReadOnlySpan{Char}"/>, that delimit the various sub-ReadOnlySpans in the <see cref="ReadOnlySpan{Char}"/>.</param>
    /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim results and include empty results.</param>
    /// <param name="count">the maximum number of results</param>
    /// <returns>An instance of the ref struct <see cref="SpanSplitAnyStringSplitOptionsWithCountEnumerator"/>, which works the same way as every <see cref="IEnumerator"/> does and can be used in a foreach construct.</returns>
    public static SpanSplitAnyStringSplitOptionsWithCountEnumerator SplitAny(this ReadOnlySpan<char> span, ReadOnlySpan<char> delimiters, StringSplitOptions options, int count)
    {
        return new SpanSplitAnyStringSplitOptionsWithCountEnumerator(span, delimiters, options, count);
    }

    /// <summary>
    /// Splits a <see cref="ReadOnlySpan{T}"/> into multiple ReadOnlySpans based on the specified <paramref name="delimiter"/>.
    /// </summary> 
    /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="span">The <see cref="ReadOnlySpan{T}"/> to be split</param>
    /// <param name="delimiter">An instance of <see cref="ReadOnlySpan{T}"/> that delimits the various sub-ReadOnlySpans in the <see cref="ReadOnlySpan{T}"/></param>
    /// <returns>An instance of the ref struct <see cref="SpanSplitSequenceEnumerator{T}"/>, which works the same way as every <see cref="IEnumerator"/> does and can be used in a foreach construct.</returns>
    public static SpanSplitSequenceEnumerator<T> Split<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> delimiter) where T : IEquatable<T>
    {
        return new SpanSplitSequenceEnumerator<T>(span, delimiter);
    }

    /// <summary>
    /// Splits a <see cref="ReadOnlySpan{T}"/> into at most <paramref name="count"/> ReadOnlySpans based on the specified <paramref name="delimiter"/>.
    /// </summary>
    /// <typeparam name="T">The type of elements in the <see cref="ReadOnlySpan{T}"/>.</typeparam>
    /// <param name="span">The <see cref="ReadOnlySpan{T}"/> to be split</param>
    /// <param name="delimiter">An instance of <see cref="ReadOnlySpan{T}"/> that delimits the various sub-ReadOnlySpans in the <see cref="ReadOnlySpan{T}"/></param>
    /// <param name="count">the maximum number of results</param> 
    /// <returns>An instance of the ref struct , which works the same way as every <see cref="IEnumerator"/> does and can be used in a foreach construct.</returns>
    public static SpanSplitSequenceWithCountEnumerator<T> Split<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> delimiter, int count) where T : IEquatable<T>
    {
        return new SpanSplitSequenceWithCountEnumerator<T>(span, delimiter, count);
    }

    /// <summary>
    /// Splits a <see cref="ReadOnlySpan{Char}"/> into multiple ReadOnlySpans based on the specified <paramref name="delimiter"/>.
    /// </summary>
    /// <param name="span">The <see cref="ReadOnlySpan{Char}"/> to be split</param>
    /// <param name="delimiter">An instance of <see cref="ReadOnlySpan{Char}"/> that delimits the various sub-ReadOnlySpans in the <see cref="ReadOnlySpan{Char}"/></param>
    /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim results and include empty results.</param>
    /// <returns>An instance of the ref struct <see cref="SpanSplitSequenceStringSplitOptionsEnumerator"/>, which works the same way as every <see cref="IEnumerator"/> does and can be used in a foreach construct.</returns>
    public static SpanSplitSequenceStringSplitOptionsEnumerator Split(this ReadOnlySpan<char> span, ReadOnlySpan<char> delimiter, StringSplitOptions options)
    {
        return new SpanSplitSequenceStringSplitOptionsEnumerator(span, delimiter, options);
    }

    /// <summary>
    /// Splits a <see cref="ReadOnlySpan{Char}"/> into at most <paramref name="count"/> ReadOnlySpans based on the specified <paramref name="delimiter"/>.
    /// </summary>
    /// <param name="span">The <see cref="ReadOnlySpan{Char}"/> to be split</param>
    /// <param name="delimiter">An instance of <see cref="ReadOnlySpan{Char}"/> that delimits the various sub-ReadOnlySpans in the <see cref="ReadOnlySpan{Char}"/></param>
    /// <param name="options">A bitwise combination of the enumeration values that specifies whether to trim results and include empty results.</param>
    /// /// <param name="count">the maximum number of results</param>
    /// <returns>An instance of the ref struct <see cref="SpanSplitSequenceStringSplitOptionsWithCountEnumerator"/>, which works the same way as every <see cref="IEnumerator"/> does and can be used in a foreach construct.</returns>
    public static SpanSplitSequenceStringSplitOptionsWithCountEnumerator Split(this ReadOnlySpan<char> span, ReadOnlySpan<char> delimiter, StringSplitOptions options, int count)
    {
        return new SpanSplitSequenceStringSplitOptionsWithCountEnumerator(span, delimiter, options, count);
    }
}
