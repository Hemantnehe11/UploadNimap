using Nimap_Product_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nimap_Product_Test.Controllers
{
    public class CategoryController : Controller
    {
        CategoryRepository db = new CategoryRepository();
        // GET: Category
        public ActionResult CategoryList()
        {
            

            return View(db.GetCategoryList());
        }

        public ActionResult index()
        {
        return View();
        }

        public ActionResult InsertCategory()
        {

            return View();
        }
        [HttpPost]
        public ActionResult InsertCategory(CategoryDM cat)
        {
          // db.InsertCategory(cat);
            return Json(db.InsertCategory(cat), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BindCategoryByID(int id)
        {
            CategoryDM item = new CategoryDM();

            item = db.BindCategoryByID(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult UpdateCategory(CategoryDM cat)
        {
            // db.InsertCategory(cat);
            return Json(db.UpdateCategory(cat), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteCategory(int id)
        {
            db.DeleteCategory(id);
            return RedirectToAction("CategoryList");
        }

    }
}