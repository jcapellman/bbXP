using bbxp.lib.Common;
using bbxp.lib.Containers;
using bbxp.lib.DAL.Objects;
using bbxp.lib.Enums;
using bbxp.lib.Transports.AdminPost;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bbxp.lib.Managers
{
    public class AdminPostManager : BaseManager
    {
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

        private void refreshDGTPost(string originalSafename, Posts post)
        {
            var dgtPost = UpdateDGTPost(originalSafename, post);

            AddCachedItem(post.URLSafename, PostManager.GeneratePostModel(dgtPost));

            RemoveCachedItem(MainCacheKeys.PostListing);
            RemoveCachedItem(MainCacheKeys.PostArchive);
        }

        public async Task<ReturnSet<bool>> CreatePostAsync(AdminPostRequestItem requestItem)
        {

            var post = new Posts
            {
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

            var tags = GenerateTags(requestItem.TagList);

            UpdateTagsForPost(post.ID, tags);

            refreshDGTPost(string.Empty, post);

            return new ReturnSet<bool>(true);
        }

        public async Task<ReturnSet<bool>> UpdatePostAsync(AdminPostRequestItem requestItem)
        {
            if (!requestItem.PostID.HasValue)
            {
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

            var tags = GenerateTags(requestItem.TagList);

            UpdateTagsForPost(post.ID, tags);

            refreshDGTPost(originalURLSafename, post);

            return new ReturnSet<bool>(true);
        }

        private void UpdateTagsForPost(int postId, IEnumerable<(string tag, string urlSafeTag)> tags)
        {
            var globalTags = DbContext.Tags.Where(a => a.Active).ToList();

            DbContext.Database.ExecuteSqlInterpolated(
                $"UPDATE Posts2Tags SET Active = 0, Modified = GETDATE() WHERE PostID = {postId}");

            foreach (var tag in tags)
            {
                var relation = new Posts2Tags
                {
                    Active = true,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    PostID = postId
                };

                var globalTag = globalTags.FirstOrDefault(a => a.Description == tag.tag);

                if (globalTag != null)
                {
                    relation.TagID = globalTag.ID;
                }
                else
                {
                    globalTag = new Tags
                    {
                        Active = true,
                        Modified = DateTime.Now,
                        Created = DateTime.Now,
                        Description = tag.tag,
                        URLSafeDescription = tag.urlSafeTag
                    };

                    DbContext.Tags.Add(globalTag);
                    DbContext.SaveChanges();

                    relation.TagID = globalTag.ID;
                }

                DbContext.Posts2Tags.Add(relation);

                DbContext.SaveChanges();
            }
        }

        private static (string tag, string urlSafeTag) ParseTag(string tag)
        {
            return (tag, tag.ToLower().Replace(" ", "-").Replace(".", ""));
        }

        private List<(string tag, string urlSafeTag)> GenerateTags(string tags)
        {
            var tagList = new List<(string tagList, string urlSafeTag)>();

            if (string.IsNullOrEmpty(tags))
            {
                return tagList;
            }

            if (!tags.Contains(","))
            {
                tagList.Add(ParseTag(tags));

                return tagList;
            }

            tagList.AddRange(tags.Split(',').Select(ParseTag));

            return tagList;
        }

        public ReturnSet<bool> RegenerateCache()
        {
            try
            {
                DbContext.Database.ExecuteSqlRaw("DELETE FROM DGT_Posts");

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

        private DGT_Posts UpdateDGTPost(string originalUrlSafename, Posts post)
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

                    return dgtPost;
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

            return dgtPost;
        }
    }
}