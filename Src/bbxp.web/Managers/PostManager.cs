﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using bbxp.lib.Common;
using bbxp.lib.Containers;
using bbxp.lib.Enums;
using bbxp.lib.Transports.Posts;
using bbxp.web.DAL;
using bbxp.web.DAL.Objects;

namespace bbxp.web.Managers {
    public class PostManager : BaseManager {
        public PostManager(ManagerContainer container) : base(container) { }
        
        private string ApplySyntaxHighlighting(string content) {
            var keywords = new List<string> {
                "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const",
                "continue", "decimal", "default", "delegate", "do", "double", "dynamic", "else", "enum", "event", "explicit",
                "extern", "false", "finally", "fixed", "float", "for", "foreach", "get", "goto", "if", "implicit", "in", "int",
                "interface", "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out",
                "override", "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "set",
                "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try",
                "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void", "while" };

            var replace = new Dictionary<string, string>();

            foreach (var keyword in keywords) {
                replace.Add($"{keyword} ", $"<span class\"Keyword\">{keyword}&nbsp;</span>");
            }

            var regex = new Regex(string.Join("|", replace.Keys.Select(Regex.Escape)));
            content = regex.Replace(content, m => replace[m.Value]);

            content = content.Replace("{", "{<br/>\n&nbsp&nbsp&nbsp;&nbsp;");
            content = content.Replace("}", "}<br/>\n");

            content = content.Replace("[csharp]", "<div class=\"CodeBlockContainer\"><![CDATA[]]>");
            content = content.Replace("[/csharp]", "]]></div>");

            return content;
        }

        private PostResponseItem GeneratePostModel(DGT_Posts post) {
            var modelItem = new PostResponseItem {
                Body = post.Body,
                PostDate = post.PostDate,
                Title = post.Title,
                RelativeURL = post.RelativeURL,
                Tags = new List<TagResponseItem>()
            };

            if (modelItem.Body.Contains("[csharp]")) {
                modelItem.Body = ApplySyntaxHighlighting(modelItem.Body);
            }

            if (string.IsNullOrEmpty(post.TagList)) {
                return modelItem;
            }

            for (var x = 0; x < post.TagList.Split(',').Count(); x++) {
                var tagItem = new TagResponseItem {
                    DisplayString = post.TagList.Split(',')[x],
                    URLString = post.TagList.Split(',')[x]
                };

                modelItem.Tags.Add(tagItem);
            }

            return modelItem;
        }

        public ReturnSet<List<PostResponseItem>> GetPostsFromTag(string urlSafeTag) {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var posts = eFactory.DGT_Posts.Where(a => a.SafeTagList.Contains(urlSafeTag)).OrderByDescending(a => a.PostDate).ToList();

                var result = new ReturnSet<List<PostResponseItem>>(posts.Select(GeneratePostModel).ToList());
                
                return result;
            }
        }

        public ReturnSet<PostResponseItem> GetSinglePost(int postID) {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var post = eFactory.DGT_Posts.FirstOrDefault(a => a.ID == postID);

                return new ReturnSet<PostResponseItem>(GeneratePostModel(post));
            }
        }

        public async Task<ReturnSet<PostResponseItem>> GetSinglePostAsync(string relativeURL) {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var post = eFactory.DGT_Posts.FirstOrDefault(a => a.RelativeURL == relativeURL);

                var result = new ReturnSet<PostResponseItem>(GeneratePostModel(post));

                await rFactory.WriteJSONAsync(relativeURL, result);

                return result;
            }
        }

        public async Task<ReturnSet<List<PostResponseItem>>> SearchPostsAsync(string query) {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var posts = eFactory.DGT_Posts.Where(a => a.Title.Contains(query)).OrderByDescending(b => b.PostDate).ToList();

                var result = new ReturnSet<List<PostResponseItem>>(posts.Select(GeneratePostModel).ToList());

                await rFactory.WriteJSONAsync($"bbxpSQ_{query}", result);

                return result;
            }
        }

        public async Task<ReturnSet<List<PostResponseItem>>> GetHomeListingAsync() {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var posts = eFactory.DGT_Posts.OrderByDescending(a => a.PostDate).Take(mContainer.GSetings.NumPostsToList).ToList();

                var result = new ReturnSet<List<PostResponseItem>>(posts.Select(GeneratePostModel).ToList());

                await rFactory.WriteJSONAsync(MainCacheKeys.PostListing, result);

                return result;
            }
        }

        public async Task<ReturnSet<List<PostResponseItem>>> GetMonthPostsAsync(int year, int month) {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var posts = eFactory.DGT_Posts.Where(a => a.PostDate.Year == year && a.PostDate.Month == month).OrderByDescending(b => b.PostDate).ToList();

                var response = new ReturnSet<List<PostResponseItem>>(posts.Select(GeneratePostModel).ToList());

                await rFactory.WriteJSONAsync($"{year}-{month}", response);

                return response;                
            }
        }
    }
}