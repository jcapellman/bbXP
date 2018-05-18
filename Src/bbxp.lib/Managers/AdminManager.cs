﻿using System;
using System.Linq;

using bbxp.lib.Common;
using bbxp.lib.Containers;

namespace bbxp.lib.Managers {
    public class AdminManager : BaseManager {
        public AdminManager(ManagerContainer container) : base(container) { }

        public ReturnSet<int> AttemptLogin(string username, string password)
        {
            try
            {
                var result = DbContext.Users.FirstOrDefault(a => a.Username == username && a.Password == password && a.Active);

                if (result == null)
                {
                    throw new Exception($"No user matching {username} and {password}");
                }

                return new ReturnSet<int>(result.ID);
            }
            catch (Exception ex)
            {
                return new ReturnSet<int>(ex);
            }
        }     
    }
}