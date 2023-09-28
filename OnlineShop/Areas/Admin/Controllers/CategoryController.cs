using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category model)
        {
            var currentCulture = Session[CommonConstants.CurrentCulture];
            model.Language = currentCulture.ToString();
            if (ModelState.IsValid)
            {
                var db = new CategoryDao();
                var result = db.Insert(model);
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", StaticResources.Resources.InsertCategoryFail);
                }
            }
            else
            {

            }
            return View(model);
        }
    }
}