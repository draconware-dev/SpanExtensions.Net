namespace SpanExtensions
{
    /// <summary>
    /// Defines the behaviour of a split operation when there are more split instances than there may be.
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
}