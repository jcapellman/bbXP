namespace bbxp.lib.DTOs
{
    /// <summary>
    /// Lightweight DTO for post list views - reduces memory allocation by excluding Body content.
    /// Use for list endpoints; use full Posts entity only when Body is needed.
    /// </summary>
    public record PostSummaryDto
    {
        public int Id { get; init; }
        public required string Title { get; init; }
        public required string Category { get; init; }
        public required string URL { get; init; }
        public DateTime PostDate { get; init; }
        
        /// <summary>
        /// Optional preview of body content (first N characters).
        /// Null when full body is not loaded for performance.
        /// </summary>
        public string? BodyPreview { get; init; }
    }
}
