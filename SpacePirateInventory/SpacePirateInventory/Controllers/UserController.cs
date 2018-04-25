using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SpacePirateInventory.Data.DapperRepo;
using SpacePirateInventory.Models;
using SpacePirateInventory.Models.Queries;
using SpacePirateInventory.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpacePirateInventory.Controllers
{
    public class UserController : Controller
    {

        // GET: Admin
        [Authorize]
        public ActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.displayMenu = "No";

                if (IsAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
                return View();
            }
            else
            {
                ViewBag.Name = "Not Logged IN";
            }
            return View();
        }



        [Authorize]
        public ActionResult Article()
        {
            ViewBag.Message = "User article list.";

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Add(Article model)
        {
            var repo = new ArticleRepository();
         
            var user = User.Identity;
            ViewBag.displayMenu = "Thanks";
            model.UserId = user.GetUserId();
                repo.AddArticle(model);
                return View();
          
        
         
        }

        [Authorize]
    
        public ActionResult Add()
        {
      
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.displayMenu = "No";

                if (IsAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
          
                return View();
            }
            else
            {
                ViewBag.Name = "Not Logged IN";
            }

            return View();
        }
        public Boolean IsAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if ((s[0].ToString() == "Admin")|| s[0].ToString() == "User")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}