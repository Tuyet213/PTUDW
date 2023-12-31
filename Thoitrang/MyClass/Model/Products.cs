﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Model
{
    [Table("Products")]
    public class Products
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Loại sản phẩm không được để trống")]
        [Display(Name = "Loại sản phẩm")]
        public int CatID { get; set; }
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Tên NCC không được để trống")]
        [Display(Name = "Mã NCC")]
        public int SupplierID { get; set; }
        
        [Display(Name = "Tên rút gọn")]
        public string Slug { get; set; }
        [Required]
        //public string Detail { get; set; }
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }
        [Required(ErrorMessage ="Giá không được để trống")]
        [Display(Name = "Giá sản phẩm")]

        public decimal Price { get; set; }
        [Required(ErrorMessage = "Giá bán không được để trống")]
        [Display(Name = "Giá bán")]
        public decimal PriceSale { get; set; }
        [Required(ErrorMessage = "Mô tả không được để trống")]
        [Display(Name = "Số lượng")]
        public int Amount { get; set; }
        [Required(ErrorMessage = "Giá không được để trống")]
        [Display(Name = "Mô tả")]
        public int MetaDesc { get; set; }
        [Required(ErrorMessage = "Từ khóa không được để trống")]
        [Display(Name = "Từ khóa")]
        public int MetaKey { get; set; }
        [Required(ErrorMessage = "Người tạo được để trống")]
        [Display(Name = "Người tạo")]
        public int CreateBy { get; set; }
        [Required(ErrorMessage = "Ngày tạo được để trống")]
        [Display(Name = "Ngày tạo")]
        public DateTime CreateAt { get; set; }
        [Required(ErrorMessage = "Người cập nhật được để trống")]
        [Display(Name = "Người cập nhật")]
        public int UpdateBy { get; set; }
        [Required(ErrorMessage = "ngày cập nhật được để trống")]
        [Display(Name = "Ngày cập nhật")]
        public DateTime UpdateAt { get; set; }
        
        [Display(Name = "Trạng thái")]
        public int? Status { get; set; }


    }
}
