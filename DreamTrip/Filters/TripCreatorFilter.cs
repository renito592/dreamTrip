﻿using DreamTrip.Models;
using DreamTrip.Repositories.Abstractions;
using DreamTrip.Repositories.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DreamTrip.Filters
{
    public class TripCreatorFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            bool userRole = AuthUser.LoggedUser.Role;
            if (!userRole)
            {
                context.HttpContext.Response.Redirect("/Trip/List");
                context.Result = new EmptyResult();
            }
            base.OnActionExecuting(context);
        }
    }
}
