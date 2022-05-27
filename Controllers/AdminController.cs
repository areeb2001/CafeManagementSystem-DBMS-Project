using PorjectMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PorjectMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(tblAdmin admin)
        {

            using (var context = new CafeManagementSystemEntities())
            {
                bool isvaliduser = context.tblAdmins.Any(x => x.AdminUserName == admin.AdminUserName);
                bool isvalidpassword = context.tblAdmins.Any(x => x.AdminPass == admin.AdminPass);
                if (isvaliduser)
                {
                    if (isvalidpassword)
                    {
                        FormsAuthentication.SetAuthCookie(admin.AdminUserName, false);
                        return RedirectToAction("adminPage");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid user password");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid user password");
                }
            }
            return View();

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        public ActionResult adminPage()
        {s##$$

            return View();
        }
    }
}