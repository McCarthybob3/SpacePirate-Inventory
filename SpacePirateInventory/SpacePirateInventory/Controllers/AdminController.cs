using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SpacePirateInventory.Data.Dapper;
using SpacePirateInventory.Data.DapperRepo;
using SpacePirateInventory.Models;
using SpacePirateInventory.Models.Tables;

namespace SpacePirateInventory.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize]
        public ActionResult EditItem(int id)
        {
            var model = new ItemEditViewModel();

            var currenciesRepo = new CurrencyRepository();
            var categoriesRepo = new CategoryRepository();
            var ItemRepo = new PirateItemRepository();

            model.Currencies = new SelectList(currenciesRepo.GetAll(), "CurrencyId", "CurrencyName");
            model.Categories = new SelectList(categoriesRepo.GetAll(), "CategoryId", "CategoryName");
            model.Item = ItemRepo.GetItemById(id);

            return View(model);
        }

        
        [Authorize]
        public ActionResult DeleteItem(int id)
        {
            var repo = new PirateItemRepository();
            repo.Delete(id);

            return RedirectToAction("Inventory", "Admin");
        }

        [Authorize]
        public ActionResult DeleteArticle(int id)
        {
            var repo = new ArticleRepository();
            repo.DeleteArticleById(id);

            return RedirectToAction("Articles", "Admin");
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(ItemEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = new PirateItemRepository();
                try
                {
                    var oldItemInfo = repo.GetById(model.Item.ItemId);
                    if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                    {
                        var savepath = Server.MapPath("~/Images");

                        string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                        string extension = Path.GetExtension(model.ImageUpload.FileName);

                        var filePath = Path.Combine(savepath, fileName + extension);

                        int counter = 1;
                        while (System.IO.File.Exists(filePath))
                        {
                            filePath = Path.Combine(savepath, fileName + counter.ToString() + extension);
                            counter++;
                        }

                        model.ImageUpload.SaveAs(filePath);
                        model.Item.ItemPictureURL = Path.GetFileName(filePath);

                        var oldPath = Path.Combine(savepath, fileName + counter.ToString() + extension);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }
                    else
                    {
                        model.Item.ItemPictureURL = oldItemInfo.ItemPictureURL;
                    }
                    repo.Update(model.Item);

                    return RedirectToAction("Inventory", "Admin");
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var currenciesRepo = new CurrencyRepository();
                model.Currencies = new SelectList(currenciesRepo.GetAll(), "CurrencyId", "CurrencyName");
                var categoriesRepo = new CategoryRepository();
                model.Categories = new SelectList(categoriesRepo.GetAll(), "CategoryId", "CategoryName");
                return View(model);
            }
        }



        [Authorize]
        public ActionResult AddItem()
        {
            var model = new ItemAddViewModel();
            var currenciesRepo = new CurrencyRepository();
            var categoriesRepo = new CategoryRepository();

            model.Currencies = new SelectList(currenciesRepo.GetAll(), "CurrencyName", "CurrencyName");
            model.Categories = new SelectList(categoriesRepo.GetAll(), "CategoryName", "CategoryName");
            model.Item = new Item();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Add(ItemAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var repo = new PirateItemRepository();

                try
                {
                    var savepath = Server.MapPath("~/Images");

                    string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                    string extension = Path.GetExtension(model.ImageUpload.FileName);

                    var filePath = Path.Combine(savepath, fileName + extension);

                    int counter = 1;
                    while (System.IO.File.Exists(filePath))
                    {
                        filePath = Path.Combine(savepath, fileName + counter.ToString() + extension);
                        counter++;
                    }

                    model.ImageUpload.SaveAs(filePath);

                    model.Item.ItemPictureURL = Path.GetFileName(filePath);

                    repo.Insert(model.Item);

                    return RedirectToAction("Inventory", "Admin");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                var currenciesRepo = new CurrencyRepository();
                model.Currencies = new SelectList(currenciesRepo.GetAll(), "CurrencyName", "CurrencyName");
                var categoriesRepo = new CategoryRepository();
                model.Categories = new SelectList(categoriesRepo.GetAll(), "CategoryName", "CategoryName");
                return View(model);
            }
        }

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
        public ActionResult Inventory()
        {
            var repo = new PirateItemRepository();
            var model = repo.GetAll().ToList();

            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.displayMenu = "No";

                if (IsAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
                return View(model);
            }
            else
            {
                ViewBag.Name = "Not Logged IN";
            }
            return View();
        }

        [Authorize]
        public ActionResult Users()
        {
            var repo = new UserRepository();
            var model = repo.GetAll().ToList();

            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.displayMenu = "No";

                if (IsAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
                return View(model);
            }
            else
            {
                ViewBag.Name = "Not Logged IN";
            }
            return View();
        }



        [Authorize]
        public ActionResult Articles()
        {
            var repo = new ArticleRepository();
            var model = repo.GetAll().ToList();

            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.displayMenu = "No";

                if (IsAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
                return View(model);
            }
            else
            {
                ViewBag.Name = "Not Logged IN";
            }
            return View();
        }

        //[Authorize]
        //public ActionResult EditItem(int id)
        //{
        //    var model = new ItemEditViewModel();

        //    var currenciesRepo = new CurrencyRepository();
        //    var categoriesRepo = new CategoryRepository();
        //    var ItemRepo = new PirateItemRepository();

        //    model.Currencies = new SelectList(currenciesRepo.GetAll(), "CurrencyId", "CurrencyName");
        //    model.Categories = new SelectList(categoriesRepo.GetAll(), "CategoryId", "CategoryName");
        //    model.Item = ItemRepo.GetItemById(id);

        //    return View(model);
        //}

        [Authorize]
        public ActionResult EditArticle(int id)
        {
            var model = new ArticleEditViewModel();
            var userRepo = new UserRepository();
            var repo = new ArticleRepository();

            model.User = new SelectList(userRepo.GetAll(), "UserId", "UserName");

            model.Article = repo.GetById(id);

            //if (User.Identity.IsAuthenticated)
            //{
            //    var user = User.Identity;
            //    ViewBag.Name = user.Name;

            //    ViewBag.displayMenu = "No";

            //    if (IsAdminUser())
            //    {
            //        ViewBag.displayMenu = "Yes";
            //    }
                return View(model);
            //}
            //else
            //{
            //    ViewBag.Name = "Not Logged IN";
            //}
            //return RedirectToAction("Inventory", "Admin"); ;
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditArticle(ArticleEditViewModel model)
        {
            var userRepo = new UserRepository();
            //var categoryRepo = new CategoryRepository();
            var repo = new ArticleRepository();

            model.User = new SelectList(userRepo.GetAll(), "UserId", "UserName");
            //model.Categories = new SelectList(categoryRepo.GetAll(), "CategoryId", "CategoryName");
           // model.Article = repo.GetById(model.Article.ArticleId);
            
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.displayMenu = "No";

                if (IsAdminUser())
                {
                    ViewBag.displayMenu = "Yes";
                }
                repo.UpdateArticle(model.Article);
                //return View(model);
                return RedirectToAction("Articles", "Admin");

            }
            else
            {
                ViewBag.Name = "Not Logged IN";
            }

            return RedirectToAction("Inventory", "Admin");
        }

        public Boolean IsAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
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