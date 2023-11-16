﻿using MyClass.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
   public class ProductsDAO
    {
        private MyDBContext db = new MyDBContext();


        //INDEX=SELECT* FROM
        public List<Products> getList()
        {

            return db.Products.ToList();
            //trả về row trong category
        }
        public List<Products> getList(string status = "All")//status 1, 2 hiển thị- 0 ẩn đi
        {
            List<Products> list = null;
            switch (status)
            {
                case "Index"://status = 1,2
                    {
                        list = db.Products.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash": //status = 0
                    {
                        list = db.Products.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default: //status = 0
                    {
                        list = db.Products.ToList();

                        break;
                    }
            }
            return list;
        }
        //CREATE
        public int Insert(Products row)
        {
            db.Products.Add(row);
            return db.SaveChanges();
        }

        //TÌm kiếm mẫu tin
        public Products getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Products.Find(id);
            }
        }
        //Update DB 
        public int Update(Products row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Delete DB
        public int Delete(Products row)
        {
            db.Products.Remove(row);
            return db.SaveChanges();
        }
    }

}
