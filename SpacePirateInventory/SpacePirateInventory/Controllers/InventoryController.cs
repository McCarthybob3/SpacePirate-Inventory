using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpacePirateInventory.Data.Dapper;
using SpacePirateInventory.Data.DapperRepo;

namespace SpacePirateInventory.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult Index()
        {
            var repo = new CategoryRepository().GetAll();
            return View(repo);
        }

        public ActionResult Details(int id)
        {
            var repo = new PirateItemRepository().GetById(id);
            return View(repo);
        }

    }
}