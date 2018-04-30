using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
namespace BUS
{
    public class KhachHang_BUS
    {
        //Trả về bảng chứa toàn bộ thông tin của bảng KHACHHANG
        public static DataTable GetKhachHangAll()
        {
            return KhachHang_DAO.GetKhachHangAll();
        }
        //Thêm dữ liệu vào bảng KHACHHANG và kiểm tra xem có thêm thành công hay không
        public static string ThemKhachHang(KhachHang_DTO kh)
        {
            if (KhachHang_DAO.SelectKhachHangLikeTenKH(kh.TenKhachHang, kh.DiaChi, kh.SDT, kh.Email) == null)
            {
                return KhachHang_DAO.Insert(kh);
            }
            else
            {
                return "Khách này đã có rồi";
            }
        }

        //Sửa thông tin khách hàng trong bảng KHACHHANG
        public static string SuaKhachHang(KhachHang_DTO kh)
        {
            if (KhachHang_DAO.GetKhachHangByMa(kh.MaKhachHang) != null)
            {
                return KhachHang_DAO.Update(kh);
            }
            else
            {
                return "Mã khách hàng không có trong CSDL";
            }
        }
        //Trả về 1 bảng chứa thông tin các khách hàng giống tên với khách hàng cần tìm
        static public DataTable SelectKhachHangLikeTen(KhachHang_DTO kh)
        {
            return KhachHang_DAO.SelectKhachHangLikeTen(kh);
        }
        //Trả về 1 bảng chứa thông tin các khách hàng giống địa chỉ với khách hàng cần tìm
        static public DataTable SelectKhachHangLikeDiaChi(KhachHang_DTO kh)
        {
            return KhachHang_DAO.SelectKhachHangLikeDiaChi(kh);
        }
        //Trả về 1 bảng chứa thông tin các khách hàng giống Email với khách hàng cần tìm
        static public DataTable SelectKhachHangLikeEmail(KhachHang_DTO kh)
        {
            return KhachHang_DAO.SelectKhachHangLikeEmail(kh);
        }
        //Trả về 1 bảng chứa thông tin các khách hàng giống Số điện thoại với khách hàng cần tìm
        static public DataTable SelectKhachHangLikeDienThoai(KhachHang_DTO kh)
        {
            return KhachHang_DAO.SelectKhachHangLikeDienThoai(kh);
        }
        //Update số tiền nợ của 1 khách hàng
        public static string UpdateTienNo(KhachHang_DTO kh)
        {
            return KhachHang_DAO.UpdateTienNo(kh);
        }
        // lấy tên khách hàng
        public static string Laykhachhang(int ma)
        {
            string sql = "select * from KHACHHANG where MaKhachHang=" + ma + "";
            DataTable dt = DataAccess.ThucThiQuery(sql);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                KhachHang_DTO tl = new KhachHang_DTO();
                //tl.MaTheLoai = (int)dt.Rows[0].ItemArray[0];
                tl.TenKhachHang = dt.Rows[0].ItemArray[1].ToString();
                return tl.TenKhachHang;
            }
        }
        // lấy địa chỉ khách hàng
        public static string Laydiachikhachhang(int ma)
        {
            string sql = "select * from KHACHHANG where MaKhachHang=" + ma + "";
            DataTable dt = DataAccess.ThucThiQuery(sql);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                KhachHang_DTO tl = new KhachHang_DTO();
                //tl.MaTheLoai = (int)dt.Rows[0].ItemArray[0];
                tl.DiaChi = dt.Rows[0].ItemArray[2].ToString();
                return tl.DiaChi;
            }
        }

        // lấy địa chỉ khách hàng
        public static string LaysoDTkhachhang(int ma)
        {
            string sql = "select * from KHACHHANG where MaKhachHang=" + ma + "";
            DataTable dt = DataAccess.ThucThiQuery(sql);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                KhachHang_DTO tl = new KhachHang_DTO();
                //tl.MaTheLoai = (int)dt.Rows[0].ItemArray[0];
                tl.SDT = dt.Rows[0].ItemArray[3].ToString();
                return tl.SDT;
            }
        }
        // lấy địa chỉ khách hàng
        public static string LayEmailkhachhang(int ma)
        {
            string sql = "select * from KHACHHANG where MaKhachHang=" + ma + "";
            DataTable dt = DataAccess.ThucThiQuery(sql);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                KhachHang_DTO tl = new KhachHang_DTO();
                //tl.MaTheLoai = (int)dt.Rows[0].ItemArray[0];
                tl.Email = dt.Rows[0].ItemArray[4].ToString();
                return tl.Email;
            }
        }

        // Xóa khách hàng từ mã khách hàng
        public static string XoaKhachHangbyMa(int ma)
        {
            return KhachHang_DAO.XoaKhachHangbyMa(ma);
        }

        // lấy mã khách hàng từ tên khách hàng
        public static string LayMakhachhang(string ten)
        {
            string sql = "select MaKhachHang from KHACHHANG where TenKhachHang= N'" + ten + "'";
            DataTable dt = DataAccess.ThucThiQuery(sql);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                return dt.Rows[0].ItemArray[0].ToString();
            }
        }
    }
}
