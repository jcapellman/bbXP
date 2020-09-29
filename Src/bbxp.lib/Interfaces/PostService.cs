﻿using bbxp.lib.DAL;
using bbxp.lib.Transports.Posts;

using System.Collections.Generic;
using System.Linq;

namespace bbxp.lib.Interfaces
{
    public class PostService : IPostService
    {
        public List<PostResponseItem> GetHomeListing(BbxpDbContext context)
        {
            return context.Posts.OrderByDescending(a => a.Created).Take(10).Select(b => new PostResponseItem
            {
                Body = b.Body,
                PostDate = b.Created,
                RelativeURL = b.URLSafename,
                Title = b.Title
            }).ToList();
        }

        public List<PostResponseItem> GetPost(BbxpDbContext context, string postUrl)
        {
            var post = context.Posts.FirstOrDefault(a => a.URLSafename == postUrl);

            return new List<PostResponseItem> { new PostResponseItem {
                Body = post.Body,
                PostDate = post.Created,
                RelativeURL = post.URLSafename,
                Title = post.Title
                } 
            };
        }
    }
}