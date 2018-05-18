using Clockwork.Web.Models;
using System;
using System.Web.Mvc;

namespace Clockwork.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            // This initially used viewData, changed to use viewModel instead
            return View(new HomeViewModel()
            {
                Version = mvcName.Version.Major + "." + mvcName.Version.Minor,
                Runtime = isMono ? "Mono" : ".NET"
            });
        }
    }
}
