using System.ComponentModel.DataAnnotations;
namespace Nhom10.Models;
public class QLKH
{
    [Key]
    [Required(ErrorMessage ="Không được để trống")]
    public string? QLKHID {get; set; }
    public string? QLKHName {get; set; }

}