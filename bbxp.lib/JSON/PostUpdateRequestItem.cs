namespace bbxp.lib.JSON
{
    public class PostUpdateRequestItem
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public required string Body { get; set; }

        public required string Category { get; set; }
    }
}