﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LinkSpot.Areas.Admin.Controllers
{
    [Authorize(Roles = "A")]
    public class ListUserController : BaseAdminController
    {
        public ActionResult Index(string SortOrder, string SortBy, string Page)
        {
            ViewBag.SortBy = SortBy;
            ViewBag.SortOrder = SortOrder;
            var users = ObjBs.userBs.GetAll();
            switch (SortBy)
            {
                case "UserEmail":
                    switch (SortOrder)
                    {
                        case "Asc":
                            users = users.OrderBy(x => x.UserEmail).ToList();
                            break;
                        case "Desc":
                            users = users.OrderByDescending(x => x.UserEmail).ToList();
                            break;
                        default:
                            break;
                    }
                    break;
                case "Role":
                    switch (SortOrder)
                    {
                        case "Asc":
                            users = users.OrderBy(x => x.Role).ToList();
                            break;
                        case "Desc":
                            users = users.OrderByDescending(x => x.Role).ToList();
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    users = users.OrderBy(x => x.UserEmail).ToList();
                    break;
            }
            ViewBag.TotalPages = Math.Ceiling(ObjBs.userBs.GetAll().Count() / 10.0);
            int page = int.Parse(Page == null ? "1" : Page);
            ViewBag.Page = page;
            //If page = 2 Skip(10) and Take (Next Ten)
            users = users.Skip((page - 1) * 10).Take(10);
            return View(users);
        }
    }
}
