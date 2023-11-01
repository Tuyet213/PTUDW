using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.Model
{
    [Table("Links")]
    public class Links
    {
        [Key]
        [Required(ErrorMessage ="Id Không được dể trống")]
        [Display(Name="Id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên Không được dể trống")]
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Tên rút gọn Không được dể trống")]
        [Display(Name = "Tên rút gọn")]
        public string Slug { get; set; }
        public int? TableId { get; set; }
        public string Type { get; set; }


    }
}
