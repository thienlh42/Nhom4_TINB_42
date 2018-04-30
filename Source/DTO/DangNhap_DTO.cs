using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
namespace DTO
{
    // class DangNhap_DTO là lớp khởi tạo các biến ở dạng cộng đồng để cho DAO dùng
    // Phải sử dụng pubic thì lớp DAO mới đọc được
    public class DangNhap_DTO
    {
        private string _taiKhoan;
        // Biến TaiKhoa có kiểu dữ liệu string dùng cho công cộng
        public string TaiKhoan
        {
            get { return _taiKhoan; }
            set { _taiKhoan = value; }
        }
        private string _matKhau;
        // Biến MatKhau co kieu dữ liệu string dùng cho công cộng
        public string MatKhau
        {
            get { return _matKhau; }
            set { _matKhau = value; }
        }
    }
}
