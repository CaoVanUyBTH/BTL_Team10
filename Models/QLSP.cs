using System.ComponentModel.DataAnnotations;

namespace Nhom10.Models
{
    public class QLSP
    {
        [Key]
        public string? QLSPID { get; set; }
        public string? QLSPName { get; set; }
    }
}