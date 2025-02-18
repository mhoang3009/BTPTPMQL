using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace BTPTPMQL.Models
{
    public class DaiLy: HeThongPhanPhoi
    {
        public string? MaDaiLy { get; set; }
        public string? TenDaiLy { get; set; }
        public string? DiaChi { get; set; }
        public string? DienThoai { get; set; }
        public string? NguoiDaiDien { get; set; }
    }
    
}
