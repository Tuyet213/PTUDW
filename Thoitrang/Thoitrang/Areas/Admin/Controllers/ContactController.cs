using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyClass.DAO;
using MyClass.Model;
using UDW.Library;

namespace Thoitrang.Areas.Admin.Controllers
{
    public class ContactController : Controller
    {
        ContactsDAO contactsDAO = new ContactsDAO();

        // GET: Admin/Contact
        public ActionResult Index()
        {
            return View(contactsDAO.getList("Index"));
        }

        // GET: Admin/Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
                return RedirectToAction("Index");
            }
            Contacts contacts = contactsDAO.getRow(id);
            if (contacts == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
                return RedirectToAction("Index");
            }
            return View(contacts);
        }

        // GET: Admin/Contact/Create
        public ActionResult Create()
        {
            //ViewBag.OrderList = new SelectList(suppliersDAO.getList("Index"), "Order", "Name");
            return View();
        }

        // POST: Admin/Contact/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contacts contacts)
        {
            if (ModelState.IsValid)
            {
                contacts.CreatAt = DateTime.Now;
                contacts.UpdateAt = DateTime.Now;
                //contacts.CreateBy = Convert.ToInt32(Session["UserID"]);
                contacts.UpdateBy = Convert.ToInt32(Session["UserID"]);
                return RedirectToAction("Index");
            }
            //ViewBag.OrderList = new SelectList(contactsDAO.getList("Index"), "Order", "Name");
            return View(contacts);
        }

        // GET: Admin/Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
                return RedirectToAction("Index");
            }
            Contacts contacts = contactsDAO.getRow(id);
            if (contacts == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
                return RedirectToAction("Index");
            }
            ViewBag.OrderList = new SelectList(contactsDAO.getList("Index"), "Order", "Name");
            return View(contacts);
        }

        // POST: Admin/Contact/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,Fullname,Phone,Email,Title,Detail,CreatAt,UpdateBy,UpdateAt,Status")] Contacts contacts)
        {
            if (ModelState.IsValid)
            {
                contacts.UpdateBy = Convert.ToInt32(Session["UserID"]);
                contacts.UpdateAt = DateTime.Now; 
                contactsDAO.Insert(contacts);
                TempData["message"] = new XMessage("danger", "Thêm mới nhà cung cấp thành công");
                return RedirectToAction("Index");

            }
            // ViewBag.OrderList = new SelectList(suppliersDAO.getList("Index"), "Order", "Name");
            return View(contacts);
        }

        // GET: Admin/Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
                return RedirectToAction("Index");
            }
            Contacts contacts = contactsDAO.getRow(id);
            if (contacts == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẩu tin");
                return RedirectToAction("Index");
            }
            return View(contacts);
        }

        // POST: Admin/Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contacts contacts = contactsDAO.getRow(id);
            contactsDAO.Delete(contacts);
            return RedirectToAction("Index");
        }

        public ActionResult DelTrash(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
                return RedirectToAction("Index");
            }
            Contacts contacts = contactsDAO.getRow(id);
            if (contacts == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
                return RedirectToAction("Index");
            }
            contacts.UpdateAt = DateTime.Now;
            contacts.UpdateBy = Convert.ToInt32(Session["UserID"]);
            contacts.Status = 0;
            contactsDAO.Update(contacts);
            TempData["message"] = new XMessage("success", "Xóa mẫu tin thành công");//hiển thị ở index=>TempData
            return RedirectToAction("Index");
        }
        //Trash 
        public ActionResult Trash()
        {
            return View(contactsDAO.getList("Trash"));//status=0
        }
        public ActionResult Undo(int? id)
        {
            if (id == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
                return RedirectToAction("Index");
            }
            Contacts contacts = contactsDAO.getRow(id);
            if (contacts == null)
            {
                TempData["message"] = new XMessage("danger", "Không tìm thấy mẫu tin");
                return RedirectToAction("Index");
            }
            contacts.UpdateAt = DateTime.Now;
            contacts.UpdateBy = Convert.ToInt32(Session["UserID"]);
            contacts.Status = 2;
            contactsDAO.Update(contacts);
            TempData["message"] = new XMessage("success", "Phục hồi mẫu tin thành công");//hiển thị ở index=>TempData
            return RedirectToAction("Index");
        }
    }
}
