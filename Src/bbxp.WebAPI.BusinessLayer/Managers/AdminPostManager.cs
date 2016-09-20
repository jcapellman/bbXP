using System;
using System.Linq;
using System.Threading.Tasks;

using bbxp.PCL.Common;
using bbxp.PCL.Containers;
using bbxp.PCL.Transports.AdminPost;

using bbxp.WebAPI.DataLayer.Entities;
using bbxp.WebAPI.DataLayer.Entities.Objects.Table;

namespace bbxp.WebAPI.BusinessLayer.Managers {
    public class AdminPostManager : BaseManager {
        public AdminPostManager(ManagerContainer container) : base(container) { }

        private string generateURLSafeName(string title) => title.ToLower().Replace(" ", "_").Replace("-", "_");

        public async Task<ReturnSet<bool>> CreatePost(AdminPostRequestItem requestItem) {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var post = new Posts {
                    Body = requestItem.Body,
                    Active = true,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    PostedByUserID = 1, // todo add userid into container
                    Title = requestItem.Title,
                    URLSafename = generateURLSafeName(requestItem.Title)
                };
                
                eFactory.Posts.Add(post);
                await eFactory.SaveChangesAsync();

                rFactory.WriteJSON(post.URLSafename, new PostManager(mContainer).GetSinglePost(post.ID));

                return new ReturnSet<bool>(true);
            }
        }

        public async Task<ReturnSet<bool>> UpdatePost(AdminPostRequestItem requestItem) {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                if (!requestItem.PostID.HasValue) {
                    return new ReturnSet<bool>("PostID not set");
                }

                var post = eFactory.Posts.FirstOrDefault(a => a.ID == requestItem.PostID.Value);

                post.Modified = DateTime.Now;
                post.Title = requestItem.Title;
                post.Body = requestItem.Body;
                post.URLSafename = generateURLSafeName(requestItem.Title);

                await eFactory.SaveChangesAsync();
                
                rFactory.WriteJSON(post.URLSafename, new PostManager(mContainer).GetSinglePost(post.ID));

                return new ReturnSet<bool>(true);
            }
        }
    }
}