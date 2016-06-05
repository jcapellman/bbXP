using System.Collections.Generic;
using System.Linq;

using bbxp.MVC.Containers;
using bbxp.MVC.Entities;
using bbxp.MVC.Entities.Objects.Table;
using bbxp.MVC.Models;

namespace bbxp.MVC.Managers {
    public class PostManager : BaseManager {
        public PostManager(ManagerContainer container) : base(container) { }
        
        private PostViewModel generatePostModel(DGT_Posts post) {
            var modelItem = new PostViewModel {
                Body = post.Body,
                PostDate = post.PostDate,
                Title = post.Title,
                Tags = new List<Tag>()
            };

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