using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using bbxp.lib.Common;
using bbxp.lib.Containers;
using bbxp.lib.DAL.Objects;
using bbxp.lib.Enums;
using bbxp.lib.Transports.AdminPost;
using Microsoft.EntityFrameworkCore;

namespace bbxp.lib.Managers {
    public class AdminPostManager : BaseManager {
        public AdminPostManager(ManagerContainer container) : base(container) { }

        private string GenerateURLSafeName(string title) => title.ToLower().Replace(" ", "_").Replace("-", "_");

        public ReturnSet<List<AdminPostListingItem>> GetPosts()
        {
            return new ReturnSet<List<AdminPostListingItem>>(DbContext.Posts.Where(a => a.Active).Select(a => new AdminPostListingItem
            {
                PostDate = a.Created,
                PostID = a.ID,
                PostTitle = a.Title
            }).OrderByDescending(a => a.PostDate).ToList());
        }

        public ReturnSet<AdminPostResponseItem> GetPost(int id)
        {
            var post = DbContext.Posts.FirstOrDefault(a => a.ID == id);

            if (post == null)
            {
                return new ReturnSet<AdminPostResponseItem>(new Exception($"Could not find {id}"));
            }

            var response = new AdminPostResponseItem
            {
                ID = post.ID,
                Title = post.Title,
                Body = post.Body,
                Tags = string.Empty
            };

            return new ReturnSet<AdminPostResponseItem>(response);
        }

        public async Task<ReturnSet<bool>> CreatePostAsync(AdminPostRequestItem requestItem) {
           
            var post = new Posts {
                Body = requestItem.Body,
                Active = true,
                Created = DateTime.Now,
                Modified = DateTime.Now,
                PostedByUserID = 1, // todo add userid into container
                Title = requestItem.Title,
                URLSafename = GenerateURLSafeName(requestItem.Title)
            };
                
            DbContext.Posts.Add(post);
            await DbContext.SaveChangesAsync();

            UpdateDGTPost(String.Empty, post);

            AddCachedItem(post.URLSafename, new PostManager(mContainer).GetSinglePost(post.ID));
                
            RemoveCachedItem(MainCacheKeys.PostListing);
            RemoveCachedItem(MainCacheKeys.PostArchive);

            return new ReturnSet<bool>(true);            
        }

        public async Task<ReturnSet<bool>> UpdatePostAsync(AdminPostRequestItem requestItem) {            
            if (!requestItem.PostID.HasValue) {
                return new ReturnSet<bool>("PostID not set");
            }

            var post = DbContext.Posts.FirstOrDefault(a => a.ID == requestItem.PostID.Value);

            if (post == null)
            {
                return new ReturnSet<bool>($"Post ID {requestItem.PostID.Value} does not exist in DB");
            }

            var originalURLSafename = post.URLSafename;

            post.Modified = DateTime.Now;
            post.Title = requestItem.Title;
            post.Body = requestItem.Body;
            post.URLSafename = GenerateURLSafeName(requestItem.Title);

            await DbContext.SaveChangesAsync();

            UpdateDGTPost(originalURLSafename, post);

            AddCachedItem(post.URLSafename, new PostManager(mContainer).GetSinglePost(post.ID));

            RemoveCachedItem(MainCacheKeys.PostListing);
            RemoveCachedItem(MainCacheKeys.PostArchive);

            return new ReturnSet<bool>(true);            
        }

        public ReturnSet<bool> RegenerateCache()
        {
            try
            {
                DbContext.Database.ExecuteSqlCommand("DELETE FROM DGT_Posts");

                var posts = DbContext.Posts.Where(a => a.Active).OrderBy(a => a.ID);

                foreach (var post in posts)
                {
                    var dgtPost = new DGT_Posts
                    {
                        Body = post.Body,
                        Title = post.Title,
                        RelativeURL = post.URLSafename,
                        SafeTagList = string.Empty,
                        TagList = string.Empty,
                        PostDate = post.Created
                    };

                    DbContext.DGT_Posts.Add(dgtPost);
                }

                DbContext.SaveChanges();

                RemoveCachedItem(MainCacheKeys.PostArchive);
                RemoveCachedItem(MainCacheKeys.PostListing);

                return new ReturnSet<bool>(true);
            }
            catch (Exception ex)
            {
                return new ReturnSet<bool>(ex);
            }
        }

        private void UpdateDGTPost(string originalUrlSafename, Posts post)
        {
            DGT_Posts dgtPost = null;

            if (!string.IsNullOrEmpty(originalUrlSafename))
            {
                dgtPost = DbContext.DGT_Posts.FirstOrDefault(a => a.RelativeURL == originalUrlSafename);

                if (dgtPost != null)
                {
                    dgtPost.RelativeURL = post.URLSafename;
                    dgtPost.Body = post.Body;
                    dgtPost.PostDate = post.Created;
                    dgtPost.Title = post.Title;
                    dgtPost.TagList = string.Empty;
                    dgtPost.SafeTagList = string.Empty;

                    DbContext.SaveChanges();

                    return;
                }
            }

            dgtPost = new DGT_Posts
            {
                Body = post.Body,
                Title = post.Title,
                RelativeURL = post.URLSafename,
                PostDate = post.Created,
                SafeTagList = string.Empty,
                TagList = string.Empty
            };

            DbContext.DGT_Posts.Add(dgtPost);
            DbContext.SaveChanges();
        }
    }
}