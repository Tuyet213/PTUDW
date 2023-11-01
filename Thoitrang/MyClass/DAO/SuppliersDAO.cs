using MyClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class SuppliersDAO
    {
        private MyDBContext db = new MyDBContext();


        //INDEX=SELECT* FROM
        public List<Suppliers> getList()
        {

            return db.Suppliers.ToList();
            //trả về row trong category
        }
        public List<Suppliers> getList(string status = "All")//status 1, 2 hiển thị- 0 ẩn đi
        {
            List<Suppliers> list = null;
            switch (status)
            {
                case "Index"://status = 1,2
                    {
                        list = db.Suppliers.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash": //status = 0
                    {
                        list = db.Suppliers.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default: //status = 0
                    {
                        list = db.Suppliers.ToList();

                        break;
                    }
            }
            return list;
        }
        //CREATE
        public int Insert(Suppliers row)
        {
            db.Suppliers.Add(row);
            return db.SaveChanges();
        }

        //TÌm kiếm mẫu tin
        public Suppliers getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Suppliers.Find(id);
            }
        }
        //Update DB 
        public int Update(Suppliers row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Delete DB
        public int Delete(Suppliers row)
        {
            db.Suppliers.Remove(row);
            return db.SaveChanges();
        }
    }

}
