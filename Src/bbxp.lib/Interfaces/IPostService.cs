﻿using bbxp.lib.DAL;
using bbxp.lib.Transports.Posts;

using System.Collections.Generic;

namespace bbxp.lib.Interfaces
{
    public interface IPostService
    {
        List<PostResponseItem> GetHomeListing(BbxpDbContext context);
    }
}