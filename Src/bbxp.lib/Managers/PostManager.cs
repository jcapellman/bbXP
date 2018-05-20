using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using bbxp.lib.Common;
using bbxp.lib.Containers;
using bbxp.lib.DAL.Objects;
using bbxp.lib.Enums;
using bbxp.lib.Transports.Posts;

namespace bbxp.lib.Managers {
    public class PostManager : BaseManager {
        public PostManager(ManagerContainer container) : base(container) { }
        
        private static string ApplySyntaxHighlighting(string content) {
            var keywords = new List<string> {
                "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const",
                "continue", "decimal", "default", "delegate", "do", "double", "dynamic", "else", "enum", "event", "explicit",
                "extern", "false", "finally", "fixed", "float", "for", "foreach", "get", "goto", "if", "implicit", "in", "int",
                "interface", "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out",
                "override", "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "set",
                "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try",
                "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void", "while" };

            var replace = keywords.ToDictionary(keyword => $"{keyword} ", keyword => $"<span class\"Keyword\">{keyword}&nbsp;</span>");

            var regex = new Regex(string.Join("|", replace.Keys.Select(Regex.Escape)));
            content = regex.Replace(content, m => replace[m.Value]);

            content = content.Replace("{", "{<br/>\n&nbsp&nbsp&nbsp;&nbsp;");
            content = content.Replace("}", "}<br/>\n");

            content = content.Replace("[csharp]", "<div class=\"CodeBlockContainer\"><![CDATA[]]>");
            content = content.Replace("[/csharp]", "]]></div>");

            return content;
        }

        public static PostResponseItem GeneratePostModel(DGT_Posts post) {
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
            var posts = DbContext.DGT_Posts.Where(a => a.SafeTagList.Contains(urlSafeTag)).OrderByDescending(a => a.PostDate).ToList();

            var result = new ReturnSet<List<PostResponseItem>>(posts.Select(GeneratePostModel).ToList());
                
            return result;            
        }
        
        public ReturnSet<PostResponseItem> GetSinglePost(string relativeURL)
        {
            var (isFound, cachedResult) = GetCachedItem<PostResponseItem>(relativeURL);

            if (isFound)
            {
                return new ReturnSet<PostResponseItem>(cachedResult);
            }

            var post = DbContext.DGT_Posts.FirstOrDefault(a => a.RelativeURL == relativeURL);

            if (post == null)
            {
                return new ReturnSet<PostResponseItem>(new Exception($"Could not find post {relativeURL}"));
            }

            var fullPost = GeneratePostModel(post);

            AddCachedItem(relativeURL, fullPost);

            return new ReturnSet<PostResponseItem>(fullPost);           
        }

        public ReturnSet<List<PostResponseItem>> SearchPosts(string query) {
            var (isFound, cachedResult) = GetCachedItem<List<PostResponseItem>>($"bbxpSQ_{query}");

            if (isFound)
            {
                return new ReturnSet<List<PostResponseItem>>(cachedResult);
            }

            var posts = DbContext.DGT_Posts.Where(a => a.Title.Contains(query)).OrderByDescending(b => b.PostDate).ToList();

            var result = posts.Select(GeneratePostModel).ToList();

            AddCachedItem($"bbxpSQ_{query}", result);

            return new ReturnSet<List<PostResponseItem>>(result);            
        }

        public ReturnSet<List<PostResponseItem>> GetHomeListing() {
            var (isFound, cachedResult) = GetCachedItem<List<PostResponseItem>>(MainCacheKeys.PostListing);

            if (isFound)
            {
                return new ReturnSet<List<PostResponseItem>>(cachedResult);
            }

            var posts = DbContext.DGT_Posts.OrderByDescending(a => a.PostDate).Take(mContainer.GSetings.NumPostsToList).ToList();

            var result = posts.Select(GeneratePostModel).ToList();

            AddCachedItem(MainCacheKeys.PostListing, result);

            return new ReturnSet<List<PostResponseItem>>(result);            
        }

        public ReturnSet<List<PostResponseItem>> GetMonthPosts(int year, int month) {
            var (isFound, cachedResult) = GetCachedItem<List<PostResponseItem>>($"{year}-{month}");

            if (isFound)
            {
                return new ReturnSet<List<PostResponseItem>>(cachedResult);
            }

            var posts = DbContext.DGT_Posts.Where(a => a.PostDate.Year == year && a.PostDate.Month == month).OrderByDescending(b => b.PostDate).ToList();

            var response = posts.Select(GeneratePostModel).ToList();

            AddCachedItem($"{year}-{month}", response);
                
            return new ReturnSet<List<PostResponseItem>>(response);
        }
    }
}