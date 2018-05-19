﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using bbxp.lib.Common;
using bbxp.lib.Containers;
using bbxp.lib.DAL.Objects;
using bbxp.lib.Transports.AdminPost;

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

            AddCachedItem(post.URLSafename, new PostManager(mContainer).GetSinglePost(post.ID));
                
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

            post.Modified = DateTime.Now;
            post.Title = requestItem.Title;
            post.Body = requestItem.Body;
            post.URLSafename = GenerateURLSafeName(requestItem.Title);

            await DbContext.SaveChangesAsync();
                
            AddCachedItem(post.URLSafename, new PostManager(mContainer).GetSinglePost(post.ID));
                
            return new ReturnSet<bool>(true);            
        }
    }
}