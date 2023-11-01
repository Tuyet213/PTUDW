using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Model
{
    [Table("Contacts")]
    public class Contacts
    {
        [Key]
        
        [Required(ErrorMessage = "Id không được để trống")]
        public int Id { get; set; }
        [Display(Name = "")]
        [Required(ErrorMessage = " không được để trống")]
        public int UserId { get; set; }
        [Display(Name = "Tên đầy đủ")]
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage ="Tiêu đề không được để trống")]
        [Display(Name ="Tiêu đề")]
        public string Title { get; set; }
        [Required(ErrorMessage ="Chi tiết không được để trống")]
        [Display(Name ="Chi tiết")]
        public string Detail { get; set; }
        public DateTime CreatAt { get; set; }
        public int UpdateBy { get; set; }
        public DateTime UpdateAt { get; set; }
        public int Status { get; set; }
        
    }
}
