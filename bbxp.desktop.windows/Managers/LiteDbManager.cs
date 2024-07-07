using System;
using System.Collections.Generic;

using bbxp.lib.Database.Tables;
using bbxp.desktop.windows.Common;
using System.Linq;

namespace bbxp.desktop.windows.Managers
{
    public class LiteDbManager
    {
        public static List<Posts> GetLocalPosts()
        {
            using var db = new LiteDB.LiteDatabase(AppConstants.DB_FILENAME);

            var collection = db.GetCollection<Posts>();

            if (collection is null)
            {
                return [];
            }

            return collection.FindAll().ToList();
        }

        public static DateTime GetMostRecentPostUpdate()
        {
            using var db = new LiteDB.LiteDatabase(AppConstants.DB_FILENAME);

            var collection = db.GetCollection<Posts>();

            if (collection is null || collection.Count() == 0)
            {
                return DateTime.MinValue;
            }

            return collection.Max(a => a.Modified);
        }

        public static void UpdateDatabase(List<Posts> posts)
        {
            using var db = new LiteDB.LiteDatabase(AppConstants.DB_FILENAME);

            var collection = db.GetCollection<Posts>();

            // If the collection is empty - no need to loop through here
            if (collection.Count() == 0)
            {
                collection.InsertBulk(posts);
            } else
            {
                foreach (var post in posts)
                {
                    var existingPost = collection.FindById(post.Id);

                    if (existingPost is null)
                    {
                        collection.Insert(post);
                    } else
                    {
                        existingPost = post;

                        collection.Update(existingPost);
                    }
                }
            }

            db.Commit();
        }
    }
}