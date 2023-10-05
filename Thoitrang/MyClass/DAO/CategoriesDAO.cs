using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Model;

namespace MyClass.DAO
{
   
    public class CategoriesDAO
    {
        private MyDBContext db = new MyDBContext();


        //INDEX=SELECT* FROM
        public List<Categories> getList()
        {

            return db.Categories.ToList();
            //trả về row trong category
        }
        public List<Categories> getList(string status = "All")//status 1, 2 hiển thị- 0 ẩn đi
        {
            List<Categories> list = null;
            switch (status)
            {
                case "Index"://status = 1,2
                    {
                        list = db.Categories.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash": //status = 0
                    {
                        list = db.Categories.ToList();
                        break;
                    }
                default: //status = 0
                    {
                        list = db.Categories.Where(m => m.Status == 0).ToList();
                        break;
                    }
            }
            return list;
        }
           
    }
}
