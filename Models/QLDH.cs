using System.ComponentModel.DataAnnotations;

namespace Nhom10.Models
{
    public class QLDH
    {
        [Key]
        public string? QLDHID { get; set; }
        public string? QLDHName { get; set; }
    }
}