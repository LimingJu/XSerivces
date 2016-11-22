using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SharedConfig;
using SharedModel;
using SharedModel.Identity;

namespace LoginSystem.Controllers
{
    public class HomeController : Controller
    {
        DefaultAppDbContext db = new DefaultAppDbContext();
        public async Task<ActionResult> Index()
        {
            var manager = new UserManager<ServiceIdentityUser,string>(new UserStore<ServiceIdentityUser, ServiceIdentityRole, string, IdentityUserLogin, ServiceIdentityUserRole, ServiceIdentityUserClaim>(db));

            ViewData["UserId"] = this.User.Identity.GetUserId() ?? "";
            var currentUser = await manager.FindByIdAsync(ViewData["UserId"].ToString());
            if (currentUser != null)
                ViewData["UserName"] = currentUser.UserName;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About XService.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "contact us for anything.";

            return View();
        }
    }
}