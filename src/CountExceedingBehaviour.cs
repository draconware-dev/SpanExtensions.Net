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
}