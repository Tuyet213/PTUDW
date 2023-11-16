//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using MyClass.DAO;
//using MyClass.Model;
//using UDW.Library;

//namespace Thoitrang.Areas.Admin.Controllers
//{
//    public class ProductController : Controller
//    {
//        ProductsDAO productsDAO = new ProductsDAO();
//        CategoriesDAO categoriesDAO = new CategoriesDAO();
//        SuppliersDAO suppliersDAO = new SuppliersDAO();

//        // GET: Admin/Products
//        public ActionResult Index()
//        {
//            return View(productsDAO.getList("Index"));
//        }

//        // GET: Admin/Products/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                TempData["message"] = new XMessage("danger", "Không tìm thấy  sản phẩm");
//                return RedirectToAction("Index");
//            }
//            Products products = productsDAO.getRow(id);
//            if (products == null)
//            {
//                TempData["message"] = new XMessage("danger", "Không tìm thấy sản phẩm");
//                return RedirectToAction("Index");
//            }
//            return View(products);
//        }

//        // GET: Admin/Products/Create
//        public ActionResult Create()
//        {
//            //lấy từ category
//            ViewBag.ListCatID = new SelectList(categoriesDAO.getList("Index"), "Id", "Name");
//            ViewBag.ListSupID = new SelectList(suppliersDAO.getList("Index"), "Id", "Name");

//            return View();
//        }

//        // POST: Admin/Products/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create( Products products)
//        {
//            if (ModelState.IsValid)
//            {
//                products.CreateAt = DateTime.Now;

//                products.CreateBy = Convert.ToInt32(Session["UserID"]);

//                products.Slug = XString.Str_Slug(products.Name);
//                products.UpdateAt = DateTime.Now;

//                products.UpdateBy = Convert.ToInt32(Session["UserID"]);
//                //xu ly cho phan upload hình ảnh
//                var img = Request.Files["Image"];//lay thong tin file
//                if (img.ContentLength != 0)
//                {
//                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
//                    //kiem tra tap tin co hay khong
//                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
//                    {
//                        string slug = products.Slug;
//                        //ten file = Slug + phan mo rong cua tap tin
//                        string imgName = slug + products.Id + img.FileName.Substring(img.FileName.LastIndexOf("."));
//                        products.Image = imgName;
//                        //upload hinh
//                        string PathDir = "~/Public/img/product/";
//                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
//                        img.SaveAs(PathFile);
//                    }
//                }
//                productsDAO.Insert(products);
                
//                productsDAO.Insert(products);
//                TempData["message"] = new XMessage("danger", "Thêm mới sản phẩm thành công");

//                return RedirectToAction("Index");
//            }
//            ViewBag.ListCatID = new SelectList(categoriesDAO.getList("Index"), "Id", "Name");
//            ViewBag.ListSupID = new SelectList(suppliersDAO.getList("Index"), "Id", "Name");

//            return View(products);
//        }

//        // GET: Admin/Products/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                TempData["message"] = new XMessage("danger", "Không tìm thấy san phẩm");
//                return RedirectToAction("Index");
//            }
//            Products products = productsDAO.getRow(id);
//            if (products == null)
//            {
//                TempData["message"] = new XMessage("danger", "Không tìm thấy sản phẩm");
//                return RedirectToAction("Index");
//            }
//            ViewBag.ListCatID = new SelectList(categoriesDAO.getList("Index"), "Id", "Name");
//            ViewBag.ListSupID = new SelectList(suppliersDAO.getList("Index"), "Id", "Name");

//            return View(products);
//        }

//        // POST: Admin/Products/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
//        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit( Products products)
//        {
//            ViewBag.ListCatID = new SelectList(categoriesDAO.getList("Index"), "Id", "Name");
//            ViewBag.ListSupID = new SelectList(suppliersDAO.getList("Index"), "Id", "Name");

//            if (ModelState.IsValid)
//            {
//                //xử lý tự động một số trường
               
//                products.Slug = XString.Str_Slug(products.Name);
//                products.UpdateAt = DateTime.Now;

//                products.UpdateBy = Convert.ToInt32(Session["UserID"]);

//                var img = Request.Files["Image"];//lay thong tin file
//                string PathDir = "~/Public/img/product/";
//                if (img.ContentLength != 0 && products.Image!=null)
//                {
//                    string DelPath = Path.Combine(Server.MapPath(PathDir), products.Image);
//                    System.IO.File.Delete(DelPath);
                   
//                }
//                if (img.ContentLength != 0)
//                {
//                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
//                    //kiem tra tap tin co hay khong
//                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
//                    {
//                        string slug = products.Slug;
//                        //ten file = Slug + phan mo rong cua tap tin
//                        string imgName = slug + products.Id + img.FileName.Substring(img.FileName.LastIndexOf("."));
//                        products.Image = imgName;
//                        //upload hinh
//                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
//                        img.SaveAs(PathFile);
//                    }
//                }
//                //cập nhật mẫu tin
//                productsDAO.Update(products);
//                TempData["message"] = new XMessage("danger", "Thêm mới nhà cung cấp thành công");
//                return RedirectToAction("Index");
//            }
//            return View(products);
//        }

//        // GET: Admin/Products/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
//                return RedirectToAction("Index");
//            }
//            Products products = productsDAO.getRow(id);
//            if (products == null)
//            {
//                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
//                return RedirectToAction("Index");
//            }
//            return View(products);
//        }

//        // POST: Admin/Products/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Products products = productsDAO.getRow(id);
//            //tìm và xóa ảnh nha fcung cấp
//            if (productsDAO.Delete(products) == 1)
//            {

//                string PathDir = "~/Public/img/product/";
//                if (products.Image != null)
//                {
//                    string DelPath = Path.Combine(Server.MapPath(PathDir), products.Image);
//                    System.IO.File.Delete(DelPath);
//                }
//            }


//           productsDAO.Delete(products);

//            return RedirectToAction("Index");
//        }

//        public ActionResult DelTrash(int? id)
//        {
//            if (id == null)
//            {
//                TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
//                return RedirectToAction("Index");
//            }
//           Products products = productsDAO.getRow(id);
//            if (products == null)
//            {
//                TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
//                return RedirectToAction("Index");
//            }
//            products.UpdateAt = DateTime.Now;
//            products.UpdateBy = Convert.ToInt32(Session["UserID"]);
//            products.Status = 0;
//            productsDAO.Update(products);
//            TempData["message"] = new XMessage("success", "Xóa mẫu tin thành công");//hiển thị ở index=>TempData
//            return RedirectToAction("Index");
//        }
//        //Trash 
//        public ActionResult Trash()
//        {
//            return View(suppliersDAO.getList("Trash"));//status=0
//        }
//        public ActionResult Undo(int? id)
//        {
//            if (id == null)
//            {
//                TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
//                return RedirectToAction("Index");
//            }
//            Products products = productsDAO.getRow(id);
//            if (products == null)
//            {
//                TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
//                return RedirectToAction("Index");
//            }
//            products.UpdateAt = DateTime.Now;
//            products.UpdateBy = Convert.ToInt32(Session["UserID"]);
//            products.Status = 2;
//            productsDAO.Update(products);
//            TempData["message"] = new XMessage("success", "Phục hồi mẫu tin thành công");//hiển thị ở index=>TempData
//            return RedirectToAction("Index");
//        }
//        public ActionResult Status(int? id)
//        {
//            if (id == null)
//            {
//                //thong bao that bai
//                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
//                return RedirectToAction("Index");
//            }
//            //truy van dong co id = id yeu cau
//            Products products = productsDAO.getRow(id);
//            if (products == null)
//            {
//                //thong bao that bai
//                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
//                return RedirectToAction("Index");
//            }
//            else
//            {
//                //chuyen doi trang thai cua Satus tu 1<->2
//                products.Status = (products.Status == 1) ? 2 : 1;

//                //cap nhat gia tri UpdateAt
//                products.UpdateAt = DateTime.Now;

//                //cap nhat lai DB
//                productsDAO.Update(products);

//                //thong bao cap nhat trang thai thanh cong
//                TempData["message"] = TempData["message"] = new XMessage("success", "Cập nhật trạng thái thành công");

//                return RedirectToAction("Index");
//            }
//        }

//    }
//}
using MyClass.DAO;
using MyClass.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using Thoitrang.Library;
using UDW.Library;

namespace Thoitrang.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        ProductsDAO productsDAO = new ProductsDAO();
        CategoriesDAO categoriesDAO = new CategoriesDAO();
        SuppliersDAO suppliersDAO = new SuppliersDAO();
        ////////////////////////////////////////////////////////////////////
        // GET: Admin/Products/Index
        public ActionResult Index()
        {
            return View(productsDAO.getList("Index"));
        }
        ////////////////////////////////////////////////////////////////////
        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Không tìm thấy sản phẩm");
                return RedirectToAction("Index");
            }
            Products products = productsDAO.getRow(id);
            if (products == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Không tìm thấy sản phẩm");
                return RedirectToAction("Index");
            }
            return View(products);
        }
        ////////////////////////////////////////////////////////////////////
        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.ListCatID = new SelectList(categoriesDAO.getList("Index"), "Id", "Name");//categories
            ViewBag.ListSupID = new SelectList(suppliersDAO.getList("Index"), "Id", "Name");//suppliers
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Products products)
        {
            if (ModelState.IsValid)
            {
                //xu ly tu dong cho mot so truong
                //CreateAt
                products.CreateAt = DateTime.Now;
                //CreateBy
                products.CreateBy = Convert.ToInt32(Session["UserID"]);
                //slug
                products.Slug = XString.Str_Slug(products.Name);
                //UpdateAt
                products.UpdateAt = DateTime.Now;
                //UpdateBy
                products.UpdateBy = Convert.ToInt32(Session["UserID"]);
                //xu ly cho muc hinh anh
                var img = Request.Files["Image"];//lay thong tin file
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    //kiem tra tap tin co hay khong
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
                    {
                        string slug = products.Slug;
                        //ten file = Slug + phan mo rong cua tap tin
                        string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        products.Image = imgName;
                        //upload hinh vao folder product
                        string PathDir = "~/Public/img/product/";
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }//ket thuc phan upload hinh anh

                //chen them dong du lieu vao DB
                productsDAO.Insert(products);
                //thong bao them moi thanh cong
                TempData["message"] = new XMessage("success", "Thêm mới sản phẩm thành công");
                return RedirectToAction("Index");
            }
            ViewBag.ListCatID = new SelectList(categoriesDAO.getList("Index"), "Id", "Name");//categories
            ViewBag.ListSupID = new SelectList(suppliersDAO.getList("Index"), "Id", "Name");//suppliers
            return View(products);
        }

        ////////////////////////////////////////////////////////////////////
        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Không tìm thấy sản phẩm");
                return RedirectToAction("Index");
            }
            Products products = productsDAO.getRow(id);
            if (products == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Không tìm thấy sản phẩm");
                return RedirectToAction("Index");
            }
            ViewBag.ListCatID = new SelectList(categoriesDAO.getList("Index"), "Id", "Name");//categories
            ViewBag.ListSupID = new SelectList(suppliersDAO.getList("Index"), "Id", "Name");//suppliers
            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Products products)
        {
            if (ModelState.IsValid)
            {
                //xu ly tu dong mot so truong
                //slug
                products.Slug = XString.Str_Slug(products.Name);
                //UpdateAt
                products.UpdateAt = DateTime.Now;
                //UpdateBy
                products.UpdateBy = Convert.ToInt32(Session["UserID"]);

                //truoc khi cap nhat lai anh moi thi xoa anh cu: Copy tu Supplier
                var img = Request.Files["img"];//lay thong tin file
                string PathDir = "~/Public/img/product/";
                if (img.ContentLength != 0 && products.Image != null)//ton tai mot logo cua NCC tu truoc
                {
                    //xoa anh cu
                    string DelPath = Path.Combine(Server.MapPath(PathDir), products.Image);
                    System.IO.File.Delete(DelPath);
                }

                //upload anh moi cua NCC: Copy tu Supplier
                //xu ly cho phan upload hinh anh
                if (img.ContentLength != 0)
                {
                    string[] FileExtentions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };
                    //kiem tra tap tin co hay khong
                    if (FileExtentions.Contains(img.FileName.Substring(img.FileName.LastIndexOf("."))))//lay phan mo rong cua tap tin
                    {
                        string slug = products.Slug;
                        //ten file = Slug + phan mo rong cua tap tin
                        string imgName = slug + img.FileName.Substring(img.FileName.LastIndexOf("."));
                        products.Image = imgName;//abc-def.jpg
                        //upload hinh
                        string PathFile = Path.Combine(Server.MapPath(PathDir), imgName);
                        img.SaveAs(PathFile);
                    }
                }//ket thuc phan upload hinh anh

                //cap nhat mau tin
                productsDAO.Update(products);
                //thong bao cap nhat thanh cong
                TempData["message"] = new XMessage("success", "Cập nhật sản phẩm thành công");
                return RedirectToAction("Index");
            }
            return View(products);
        }

        ////////////////////////////////////////////////////////////////////
        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Không tìm thấy sản phẩm");
            }
            Products products = productsDAO.getRow(id);
            if (products == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Không tìm thấy sản phẩm");
            }
            return View(products);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = productsDAO.getRow(id);

            //tim va xoa anh cua NCC
            if (productsDAO.Delete(products) == 1)
            {
                string PathDir = "~/Public/img/product/";
                if (products.Image != null)//ton tai mot anh cua san pham tu truoc
                {
                    //xoa anh cu
                    string DelPath = Path.Combine(Server.MapPath(PathDir), products.Image);
                    System.IO.File.Delete(DelPath);
                }
            }
            //hien thi thong bao thanh cong
            TempData["message"] = new XMessage("success", "Xóa sản phẩm thành công");
            return RedirectToAction("Trash");
        }

        ////////////////////////////////////////////////////////////////////
        // GET: Admin/Products/Status/5
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            Products products = productsDAO.getRow(id);
            if (products == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Cập nhật trạng thái thất bại");
                return RedirectToAction("Index");
            }
            //cap nhat trang thai
            products.Status = (products.Status == 1) ? 2 : 1;
            //cap nhạt Update At
            products.UpdateAt = DateTime.Now;
            //cap nhat Update By
            products.UpdateBy = Convert.ToInt32(Session["UserID"]);
            //Update DB
            productsDAO.Update(products);
            //hien thi thong bao
            TempData["message"] = new XMessage("success", "Cập nhật trạng thái thành công");
            //tro ve trang Index
            return RedirectToAction("Index");
        }

        ////////////////////////////////////////////////////////////////////
        // GET: Admin/Product/DelTrash/5
        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Xóa mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            Products products = productsDAO.getRow(id);
            if (products == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Xóa mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            //cap nhat trang thai
            products.Status = 0;
            //cap nhạt Update At
            products.UpdateAt = DateTime.Now;
            //cap nhat Update By
            products.UpdateBy = Convert.ToInt32(Session["UserID"]);
            //Update DB
            productsDAO.Update(products);
            //hien thi thong bao
            TempData["message"] = new XMessage("success", "Xóa mẩu tin thành công");
            //tro ve trang Index
            return RedirectToAction("Index");
        }

        ////////////////////////////////////////////////////////////////////
        // GET: Admin/Product/Trash = luc thung rac
        public ActionResult Trash()
        {
            return View(productsDAO.getList("Trash"));
        }

        ////////////////////////////////////////////////////////////////////
        // GET: Admin/Product/Undo/5
        public ActionResult Undo(int? id)
        {
            if (id == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Phục hồi mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            Products products = productsDAO.getRow(id);
            if (products == null)
            {
                //hien thi thong bao
                TempData["message"] = new XMessage("danger", "Phục hồi mẩu tin thất bại");
                return RedirectToAction("Index");
            }
            //cap nhat trang thai status = 2
            products.Status = 2;
            //cap nhạt Update At
            products.UpdateAt = DateTime.Now;
            //cap nhat Update By
            products.UpdateBy = Convert.ToInt32(Session["UserID"]);
            //Update DB
            productsDAO.Update(products);
            //hien thi thong bao
            TempData["message"] = new XMessage("success", "Phục hồi mẩu tin thành công");
            //tro ve trang Index
            return RedirectToAction("Trash");//o lai thung rac de tiep tuc Undo
        }
    }
}