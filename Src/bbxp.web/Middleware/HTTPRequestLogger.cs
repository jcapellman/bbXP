﻿using System;

using bbxp.lib.DAL;
using bbxp.lib.DAL.Objects;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace bbxp.web.Middleware {
    public class HTTPRequestLoggerAttribute : TypeFilterAttribute {
        public HTTPRequestLoggerAttribute() : base(typeof(HTTPRequestLoggerFilter))
        {
        }

        private class HTTPRequestLoggerFilter : IActionFilter
        {
            private readonly BbxpDbContext _dbContext;

            public HTTPRequestLoggerFilter(BbxpDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                _dbContext.Requests.Add(new Requests
                {
                    Active = true,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    RequestStr = context.HttpContext.Request.Path
                });

                _dbContext.SaveChanges();
            }
        }
    }
}