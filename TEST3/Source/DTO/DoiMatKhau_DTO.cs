using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
namespace DTO
{
    public class DoiMatKhau_DTO
    {
        private string _taiKhoan;
        // Biến public TaiKhoan để các lớp khác có thể gọi tới dc
        public string TaiKhoan
        {
            get { return _taiKhoan; }
            set { _taiKhoan = value; }
        }
        private string _matKhauCu;
        // Biến public MatKhauCu để các lớp khác có thể gọi tới dc
        // Biến này dùng để lưu Mta65 khẩu củ
        public string MatKhauCu
        {
            get { return _matKhauCu; }
            set { _matKhauCu = value; }
        }
        private string _matKhauMoi;
        // Biến public MatKhauMoi để các lớp khác có thể gọi được 
        // BIến này dùng để lưu mật khẩu mới
        public string MatKhauMoi
        {
            get { return _matKhauMoi; }
            set { _matKhauMoi = value; }
        }
        private string _nhapLaiMatKhauMoi;
        // Biến public NhapLaiMatKhauMoi để các lớp khác có thể gọi tới được
        // Biến này dùng để lưu Nhập lại mật khẩu để kiểm tra xem mật khẩu có khớp không
        public string NhapLaiMatKhauMoi
        {
            get { return _nhapLaiMatKhauMoi; }
            set { _nhapLaiMatKhauMoi = value; }
        }
    }
}
