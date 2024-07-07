using System;
using System.Collections.Generic;

using bbxp.lib.Database.Tables;
using bbxp.desktop.windows.Common;

namespace bbxp.desktop.windows.Managers
{
    public class LiteDbManager
    {
        public static IEnumerable<Posts>? GetLocalPosts()
        {
            using var db = new LiteDB.LiteDatabase(AppConstants.DB_FILENAME);

            var collection = db.GetCollection<Posts>();

            if (collection is null)
            {
                return null;
            }

            return collection.FindAll();
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
    }
}