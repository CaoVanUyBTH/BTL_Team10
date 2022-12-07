using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Nhom10.Models;
public class Customer
{
    [Key]
    [Required(ErrorMessage ="Không được để trống")]
    public string CusID {get; set; }
    public string CusName {get; set; }
    public string Address { get; set; }
}