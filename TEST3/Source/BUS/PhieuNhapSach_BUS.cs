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
    public class PhieuNhapSach_BUS
    {
        //Trả về tất cả thông tin của bang PHIEUNHAPSACH
        public static DataTable SelectPhieuNhapSachAll()
        {
            return PhieuNhapSach_DAO.SelectPhieuNhapSachAll();
        }
        //Trả về tất cả thông tin của bảng CT_PHIEUNHAPSACH
        public static DataTable SelectCTPhieuNhapSachByMa(int maPNS)
        {
            return PhieuNhapSach_DAO.SelectCTPhieuNhapSachByMa(maPNS);
        }
        //Thêm 1 phiếu nhập sách
        public static string ThemPhieuNhap(PhieuNhapSach_DTO p)
        {
            return PhieuNhapSach_DAO.InsertPhieuNhap(p);
        }
        //Thêm vào bảng CHITIET_PHIEUNHAP
        public static string ThemChiTietPhieuNhap(CT_PhieuNhapSach_DTO p)
        {
            if (PhieuNhapSach_DAO.GetPhieuNhapByName(p.MaPNS, p.MaSach) == null)
            {
                return PhieuNhapSach_DAO.Insert(p);
            }
            else
            {
                return "Sách này đã có trong chi tiet phieu nhap";
            }
        }
        //Lấy ra tháng theo MaPNS
        public static DataTable GetThangByMaPNS(int ma)
        {
            return PhieuNhapSach_DAO.GetThangByMaPNS(ma);
        }
        //Lấy ra năm theo MaPNS
        public static DataTable GetNamByMaPNS(int ma)
        {
            return PhieuNhapSach_DAO.GetNamByMaPNS(ma);
        }
        //Lấy ra tiền 
        public static void LayTien(PhieuNhapSach_DTO p)
        {
            p.TongTien = UInt64.Parse(PhieuNhapSach_DAO.GetTien(p.MaPNS).Rows[0].ItemArray[0].ToString());
        }

        //Cập nhật tổng tiền
        public static void CapNhatTongTien(PhieuNhapSach_DTO p)
        {
            p.TongTien = UInt64.Parse(PhieuNhapSach_DAO.GetTongThanhTien(p.MaPNS).Rows[0].ItemArray[0].ToString());
            PhieuNhapSach_DAO.UpdateTongTien(p);
        }
        public static void updateTien(PhieuNhapSach_DTO p)
        {
            //p.TongTien = UInt64.Parse(PhieuNhapSach_DAO.GetTongThanhTien(p.MaPNS).Rows[0].ItemArray[0].ToString());
            PhieuNhapSach_DAO.UpdateTongTien(p);
        }
        //Kiểm tra có phải là PHIEUNHAPSACH đầu tiên
        public static bool KiemTraDauTien(int ngay, int thang, int nam, int maSach)
        {
            DataTable dt = PhieuNhapSach_DAO.KiemTraDauTien(ngay, thang, nam, maSach);
            if (int.Parse(dt.Rows[0].ItemArray[0].ToString()) == 0)
            {
                return true;
            }
            return false;
        }
        //Phương thức xóa phiếu
        public static string XoaPhieuNhap (PhieuNhapSach_DTO obj)
        {
            return PhieuNhapSach_DAO.XoaPhieuNhap(obj);
        }

        // phương thức xóa sách trong phiếu sau khi xóa phiếu nhập
        public static string XoaSachtrongPhieu(CT_PhieuNhapSach_DTO obj)
        {
            return PhieuNhapSach_DAO.XoaSachtrongPhieu(obj);
        }

        // phương thức xóa sách trong phiếu chi tiết sách
        public static string XoaMotSachtrongPhieu(CT_PhieuNhapSach_DTO obj)
        {
            return PhieuNhapSach_DAO.XoaMotcuonsach(obj);
        }
        //Sửa thông tin sách trong bảng trong bảng CT_PhieuNhapSach
        public static string SuaNhapSach(CT_PhieuNhapSach_DTO kh)
        {
            if (PhieuNhapSach_DAO.Kiemtramasach(kh.MaSach, kh.MaPNS) != null)
            {
                return PhieuNhapSach_DAO.UpdateSachNhap(kh);
            }
            else
            {
                return "Mã sách không có trong CSDL";
            }
        }

        //Lấy ra số lượng nhập
        public static int SoLuongNhap(int maSach)
        {
            return int.Parse(PhieuNhapSach_DAO.GetSoLuongNhap(maSach).Rows[0].ItemArray[0].ToString());
        }
    }
}
