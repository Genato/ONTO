﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ONTO.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public new ActionResult Profile()
        {

            return View();
        }
    }
}