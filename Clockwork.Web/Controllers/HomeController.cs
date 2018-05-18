﻿using Clockwork.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace Clockwork.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            // This originally used ViewData. Changing to use a viewmodel instead.
            return View(new HomeViewModel()
            {
                Version = mvcName.Version.Major + "." + mvcName.Version.Minor,
                Runtime = isMono ? "Mono" : ".NET"
            });
        }
    }
}