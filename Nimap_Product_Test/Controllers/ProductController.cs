using Nimap_Product_Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Nimap_Product_Test.Controllers
{
    public class ProductController : Controller
    {
        ProductRepository db = new ProductRepository();
        CategoryRepository Catdb = new CategoryRepository();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProuctList(int page = 1)
        {
            List<ProductDM> data = db.GetProductList(page);
           // ViewBag.pagetotal = data.Select(x => x.PageNumber).FirstOrDefault();
            return View(data.ToList());
        }

        public ActionResult InsertProduct()
        {
            ViewBag.Category = new SelectList(Catdb.GetCategoryList().ToList(), "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult InsertProduct(ProductDM data)
        {
            ViewBag.Category = new SelectList(Catdb.GetCategoryList().ToList(), "CategoryId", "CategoryName");
            return Json(db.InsertProduct(data), JsonRequestBehavior.AllowGet);
        }

        public ActionResult BindProductIdByID(int id)
        {
            ProductDM item = new ProductDM();
            ViewBag.Category = new SelectList(Catdb.GetCategoryList().ToList(), "CategoryId", "CategoryName");
            item = db.BindProductByID(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult UpdateProduct(ProductDM pro)
        {
            // db.InsertCategory(cat);
            return Json(db.UpdateProduct(pro), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteProductId(int id)
        {
            db.DeleteProduct(id);
            return RedirectToAction("ProuctList");
        }

        public ActionResult GetRowCountForPageCreation(int page)
        {
            // db.InsertCategory(cat);
            return Json(db.GetRowCount(page), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProuctList_PartialView(int page = 1)
        {
            List<ProductDM> data = db.GetProductList(page);
            // ViewBag.pagetotal = data.Select(x => x.PageNumber).FirstOrDefault();
            return PartialView(data.ToList());
        }
    }
}