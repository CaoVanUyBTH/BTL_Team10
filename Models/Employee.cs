using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Nhom10.Models;
public class Employee
{
    [Key]
    [Required(ErrorMessage ="Không được để trống")]
    public string EmpID {get; set; }
    public string EmpName {get; set; }
    public string Address { get; set; }
}
