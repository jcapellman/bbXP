namespace bbxp.cli.migration.SQLServer.Objects
{
    public class Posts
    {
        public int ID { get; set; }

        public DateTime Modified { get; set; }

        public DateTime Created { get; set; }

        public bool Active { get; set; }

        public required string Title { get; set; }

        public required string Body { get; set; }

        public int PostedByUserId { get; set; }

        public required string URLSafename { get; set; }
    }
}