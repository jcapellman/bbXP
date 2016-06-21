using System.Collections.Generic;
using System.Linq;

using bbxp.MVC.Containers;
using bbxp.MVC.Entities;
using bbxp.MVC.Entities.Objects.Table;
using bbxp.MVC.Models;
using System.Text.RegularExpressions;
using System;

namespace bbxp.MVC.Managers {
    public class PostManager : BaseManager {
        public PostManager(ManagerContainer container) : base(container) { }
        
        private string applySyntaxHighlighting(string content) {
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

            var regex = new Regex(String.Join("|", replace.Keys.Select(k => Regex.Escape(k))));
            content = regex.Replace(content, m => replace[m.Value]);

            content = content.Replace("{", "{<br/>\n&nbsp&nbsp&nbsp;&nbsp;");
            content = content.Replace("}", "}<br/>\n");

            content = content.Replace("[csharp]", "<div class=\"CodeBlockContainer\"><![CDATA[]]>");
            content = content.Replace("[/csharp]", "]]></div>");

            return content;
        }

        private PostViewModel generatePostModel(DGT_Posts post) {
            var modelItem = new PostViewModel {
                Body = post.Body,
                PostDate = post.PostDate,
                Title = post.Title,
                Tags = new List<Tag>()
            };

            if (modelItem.Body.Contains("[csharp]")) {
                modelItem.Body = applySyntaxHighlighting(modelItem.Body);
            }

            if (string.IsNullOrEmpty(post.TagList)) {
                return modelItem;
            }

            for (var x = 0; x < post.TagList.Split(',').Count(); x++) {
                var tagItem = new Tag {
                    DisplayString = post.TagList.Split(',')[x],
                    URLString = post.TagList.Split(',')[x]
                };

                modelItem.Tags.Add(tagItem);
            }

            return modelItem;
        }

        public List<PostViewModel> GetPostsFromTag(string urlSafeTag) {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var posts = eFactory.DGT_Posts.Where(a => a.SafeTagList.Contains(urlSafeTag)).OrderByDescending(a => a.PostDate).ToList();

                return posts.Select(generatePostModel).ToList();
            }
        }

        public PostViewModel GetSinglePost(string relativeURL) {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var post = eFactory.DGT_Posts.FirstOrDefault(a => a.RelativeURL == relativeURL);

                return generatePostModel(post);
            }
        }

        public List<PostViewModel> SearchPosts(string query) {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var posts = eFactory.DGT_Posts.Where(a => a.Title.Contains(query)).OrderByDescending(b => b.PostDate).ToList();

                return posts.Select(generatePostModel).ToList();
            }
        }

        public List<PostViewModel> GetHomeListing() {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var posts = eFactory.DGT_Posts.OrderByDescending(a => a.PostDate).Take(mContainer.GSetings.NumPostsToList).ToList();

                return posts.Select(generatePostModel).ToList();
            }
        }

        public List<PostViewModel> GetMonthPosts(int year, int month) {
            using (var eFactory = new EntityFactory(mContainer.GSetings.DatabaseConnection)) {
                var posts = eFactory.DGT_Posts.Where(a => a.PostDate.Year == year && a.PostDate.Month == month).OrderByDescending(b => b.PostDate).ToList();

                return posts.Select(generatePostModel).ToList();
            }
        }
    }
}