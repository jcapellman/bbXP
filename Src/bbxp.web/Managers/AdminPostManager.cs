using System;
using System.Linq;
using System.Threading.Tasks;

using bbxp.lib.Common;
using bbxp.lib.Containers;
using bbxp.lib.Transports.AdminPost;

using bbxp.web.DAL;
using bbxp.web.DAL.Objects;

namespace bbxp.web.Managers {
    public class AdminPostManager : BaseManager {
        public AdminPostManager(ManagerContainer container) : base(container) { }

        private string GenerateURLSafeName(string title) => title.ToLower().Replace(" ", "_").Replace("-", "_");

        public async Task<ReturnSet<bool>> CreatePostAsync(AdminPostRequestItem requestItem) {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var post = new Posts {
                    Body = requestItem.Body,
                    Active = true,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    PostedByUserID = 1, // todo add userid into container
                    Title = requestItem.Title,
                    URLSafename = GenerateURLSafeName(requestItem.Title)
                };
                
                eFactory.Posts.Add(post);
                await eFactory.SaveChangesAsync();

                AddCachedItem(post.URLSafename, new PostManager(mContainer).GetSinglePost(post.ID));
                
                return new ReturnSet<bool>(true);
            }
        }

        public async Task<ReturnSet<bool>> UpdatePostAsync(AdminPostRequestItem requestItem) {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                if (!requestItem.PostID.HasValue) {
                    return new ReturnSet<bool>("PostID not set");
                }

                var post = eFactory.Posts.FirstOrDefault(a => a.ID == requestItem.PostID.Value);

                if (post == null)
                {
                    return new ReturnSet<bool>($"Post ID {requestItem.PostID.Value} does not exist in DB");
                }

                post.Modified = DateTime.Now;
                post.Title = requestItem.Title;
                post.Body = requestItem.Body;
                post.URLSafename = GenerateURLSafeName(requestItem.Title);

                await eFactory.SaveChangesAsync();
                
                AddCachedItem(post.URLSafename, new PostManager(mContainer).GetSinglePost(post.ID));
                
                return new ReturnSet<bool>(true);
            }
        }
    }
}