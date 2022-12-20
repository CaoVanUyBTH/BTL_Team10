using System.ComponentModel.DataAnnotations;
namespace Nhom10.Models;
public class QLNV
{
    [Key]
    [Required(ErrorMessage ="Không được để trống")]
    public string? QLNVID {get; set; }
    public string? QLNVName {get; set; }
    public string? QLKHID { get; internal set; }
}
