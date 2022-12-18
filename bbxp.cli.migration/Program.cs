using bbxp.cli.migration.SQLServer;
using bbxp.cli.migration.SQLServer.Objects;
using bbxp.lib.HttpHandlers;
using bbxp.lib.JSON;

namespace bbxp.cli.migration
{
    internal class Program
    {
        private const string REST_SERVICE_BASE_URL = "https://localhost:7026/api/";

        static void Main(string[] args)
        {
            var postHttpHandler = new PostHttpHandler(REST_SERVICE_BASE_URL);

            var oldPosts = new List<Posts>();

            using (var ssContext = new bbxpSSContext())
            {
                oldPosts = ssContext.Posts.Where(a => a.Active).OrderBy(a => a.Created).ToList();
            }

            Console.WriteLine($"{oldPosts.Count} posts were queried...");

            foreach (var post in oldPosts)
            {
                var newPost = new PostCreationRequestItem
                {
                    Body = post.Body,
                    Category = "Latest",
                    Title = post.Title,
                    PostDate = post.Created
                };

                var result = postHttpHandler.CreateNewPost(newPost).Result;

                if (result)
                {
                    Console.WriteLine($"{post.Title} migrated successfully");
                }
                else
                {
                    Console.WriteLine($"{post.Title} migration FAILED");
                    Console.ReadKey();
                }
            }
        }
    }
}