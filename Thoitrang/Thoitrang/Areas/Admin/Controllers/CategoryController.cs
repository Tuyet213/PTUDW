using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.Model;
using MyClass.DAO;
using UDW.Library;

namespace Thoitrang.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        CategoriesDAO categoriesDAO = new CategoriesDAO();
        

        // GET: Admin/Category
        //INDEX
        public ActionResult Index()
        {
            return View(categoriesDAO.getList("Index"));
        }




        // GET: Admin/Category/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Categories categories = db.Categories.Find(id);
        //    if (categories == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(categories);
        //}

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            ViewBag.CatList = new SelectList(categoriesDAO.getList("Index"), "Id", "Name");
            ViewBag.OrderList = new SelectList(categoriesDAO.getList("Index"), "Order", "Name");
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Categories categories)
        {
            if (ModelState.IsValid)
            {
                //Xử lý 
                //CreateAt
                categories.CreateAt=DateTime.Now;
                //UpdateAt
                categories.UpdateAt=DateTime.Now;
                //CreateBy
                categories.CreateBy = Convert.ToInt32(Session["UserID"]);
                //UpdateBy
                categories.UpdateBy = Convert.ToInt32(Session["UserID"]);
                //Slug
                categories.Slug = XString.Str_Slug(categories.Name);
                //ParentId
                if (categories.ParentID == null)
                {
                    categories.ParentID = 0;
                }
                //Order
                if (categories.Order == null)
                {
                    categories.Order = 1;
                }
                else
                {
                    categories.Order += 1;
                }
                //Thêm mới dòng dữ liệu 
                categoriesDAO.Insert(categories);
                return RedirectToAction("Index");
            }

            return View(categories);
        }

        //// GET: Admin/Category/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Categories categories = db.Categories.Find(id);
        //    if (categories == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(categories);
        //}

        //// POST: Admin/Category/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,Slug,ParentID,Order,MetaDes,MetaKey,CreateBy,CreateAt,UpdateBy,UpdateAt,Status")] Categories categories)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(categories).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(categories);
        //}

        //// GET: Admin/Category/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Categories categories = db.Categories.Find(id);
        //    if (categories == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(categories);
        //}

        //// POST: Admin/Category/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Categories categories = db.Categories.Find(id);
        //    db.Categories.Remove(categories);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
