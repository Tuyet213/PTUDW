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
                //hiển thị thông báo là thêm dữ liệu thành công
                TempData["message"] = new XMessage("success","Thêm mới mẫu tin thành công");
                return RedirectToAction("Index");
            }
            //db.Categories.Where(m => m.Status != 0).ToList();
            ViewBag.CatList = new SelectList(categoriesDAO.getList("Index"), "Id", "Name");
            ViewBag.OrderList = new SelectList(categoriesDAO.getList("Index"), "Order", "Name");

            return View(categories);
        }

        // GET: Admin/Category/Status/5 check bằng icon chức năng, hoặc truyền vào url 2000=>thất bại vì ko có id này
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                //hiển thị thông báo là thay đổi status thất bại
                TempData["message"] = new XMessage("danger", "Thay đổi status thất bại");
                return RedirectToAction("Index");
            }
            //nếu có id thì kiểm tra xem có dòng giá trị của id ko
            Categories categories = categoriesDAO.getRow(id);
            //ko
            if (categories == null)
            {
                //hiển thị thông báo là thay đổi status thất bại
                TempData["message"] = new XMessage("danger", "Thay đổi status thất bại");
                return RedirectToAction("Index");
                //return HttpNotFound();
            }



            //cập nhật một số thoogn tin cho Db= id==id
            //UpdateAt
            categories.UpdateAt = DateTime.Now;
            //UpdateBy
            categories.UpdateBy = Convert.ToInt32(Session["UserID"]);
            //status
            categories.Status = (categories.Status == 1) ? 2 : 1;
            //update DB
            categoriesDAO.Update(categories);



            //có
            //hiển thị thông báo là thay đổi status thành công
            TempData["message"] = new XMessage("success", "Thay đổi status thành công");
            return RedirectToAction("Index");
        }
        //// GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
                return RedirectToAction("Index");
            }
            Categories categories = categoriesDAO.getRow(id);

            if (categories == null)
            {
                
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
                return RedirectToAction("Index");
                
            }
            return View(categories);
        }

        //// GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //hiển thị thông báo là thay đổi status thất bại
                TempData["message"] = new XMessage("danger", "Cập nhật mẫu tin thất bại");
                return RedirectToAction("Index");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                //hiển thị thông báo là thay đổi status thất bại
                TempData["message"] = new XMessage("danger", "Cập nhật mẫu tin thất bại");
                return RedirectToAction("Index");
                //return HttpNotFound();
            }
            ViewBag.CatList = new SelectList(categoriesDAO.getList("Index"), "Id", "Name");
            ViewBag.OrderList = new SelectList(categoriesDAO.getList("Index"), "Order", "Name");
            return View(categories);
        }

        //// POST: Admin/Category/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categories categories)
        {
            if (ModelState.IsValid)
            {
                //cập nhật một số trường thông tin tự động
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
                categories.UpdateAt = DateTime.Now;
                categories.UpdateBy = Convert.ToInt32(Session["UserID"]);
                //update DB
                categoriesDAO.Update(categories);

                //khoogn sửa status vì ở index có chức năng thây đổi status r

                //hiển thị thông báo là Cập nhật mẫu tin thành công
                TempData["message"] = new XMessage("success", "Cập nhật mẫu tin thành công");
                //db.Entry(categories).State = EntityState.Modified;
                //db.SaveChanges();

                return RedirectToAction("Index");
            }
            //ViewBag.CatList = new SelectList(categoriesDAO.getList("Index"), "Id", "Name");
            //ViewBag.OrderList = new SelectList(categoriesDAO.getList("Index"), "Order", "Name");
            return View(categories);
            
        }


        //xóa là không thấy, khoogn thấy thì cho status=0

        //// GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
                return RedirectToAction("Index");
            }
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
                return RedirectToAction("Index");
            }
            return View(categories);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categories categories = categoriesDAO.getRow(id);
            categoriesDAO.Delete(categories);
            TempData["message"] = new XMessage("success", "Xóa thành công");
            return RedirectToAction("Trash");

        }
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //chuyển mẫu tin đang ỏ trạng thái 1,2 thành 0=> khoogn hiển thị ở trang index
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
                return RedirectToAction("Index");
            }
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
                return RedirectToAction("Index");
            }
            categories.UpdateAt = DateTime.Now;
            categories.UpdateBy = Convert.ToInt32(Session["UserID"]);
            categories.Status = 0;
            categoriesDAO.Update(categories);
            TempData["message"] = new XMessage("success", "Xóa mẫu tin thành công");//hiển thị ở index=>TempData
            return RedirectToAction("Index");
        }
        //Trash 
        public ActionResult Trash()
        {
            return View(categoriesDAO.getList("Trash"));//status=0
        }
        public ActionResult Undo(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
                return RedirectToAction("Index");
            }
            Categories categories = categoriesDAO.getRow(id);
            if (categories == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
                return RedirectToAction("Index");
            }
            categories.UpdateAt = DateTime.Now;
            categories.UpdateBy = Convert.ToInt32(Session["UserID"]);
            categories.Status = 2;
            categoriesDAO.Update(categories);
            TempData["message"] = new XMessage("success", "Phục hồi mẫu tin thành công");//hiển thị ở index=>TempData
            return RedirectToAction("Index");
        }
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
        //        return RedirectToAction("Index");
        //    }
        //    Categories categories = categoriesDAO.getRow(id);
        //    if (categories == null)
        //    {
        //        TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
        //        return RedirectToAction("Index");
        //    }
        //    return RedirectToAction("Index");
        //}
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Categories categories = categoriesDAO.db.Categories.Find(id);
        //    categoriesDAO.db.Categories.Remove(categories);
        //    categoriesDAO.db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
