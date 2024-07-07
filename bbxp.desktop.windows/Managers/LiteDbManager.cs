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

            if (collection is null)
            {
                return DateTime.MinValue;
            }

            return collection.Max(a => a.Modified);
        }

        public static void UpdateDatabase(List<Posts> posts)
        {
            using var db = new LiteDB.LiteDatabase(AppConstants.DB_FILENAME);

            var collection = db.GetCollection<Posts>();

            collection.InsertBulk(posts);

            db.Commit();
        }
    }
}