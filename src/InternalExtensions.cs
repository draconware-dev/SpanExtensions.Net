using System;


namespace SpanExtensions
{
    static class InternalExtensions
    {
        /// <summary>
        /// Indicates whether the specified span contains only white-space characters.
        /// </summary>
        /// <param name="span">The source span.</param>
        /// <returns><see langword="true"/> if the span contains only whitespace characters, <see langword="false"/> otherwise.</returns>
        public static bool IsWhiteSpace(this Span<char> span)
        {
            return ((ReadOnlySpan<char>)span).IsWhiteSpace();
        }

#if !NETCOREAPP3_0_OR_GREATER
        /// <summary>
        /// Removes all leading and trailing white-space characters from the span.
        /// </summary>
        /// <param name="span">The source span from which the characters are removed.</param>
        /// <returns>The trimmed character span.</returns>
        public static Span<char> Trim(this Span<char> span)
        {
            ReadOnlySpan<char> rospan = span;
            ReadOnlySpan<char> leftTrimmed = rospan.TrimStart();
            ReadOnlySpan<char> trimmed = rospan.TrimEnd();

            if(span.Length == trimmed.Length)
            {
                return span;
            }
            else
            {
                return span.Slice(rospan.Length - leftTrimmed.Length, trimmed.Length);
            }
        }
#endif
    }
}
