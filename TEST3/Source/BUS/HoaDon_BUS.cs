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
    public class HoaDon_BUS
    {
        //Hiển thị tất cả hóa đơn
        public static DataTable SelectallHoaDon()
        {
            return HoaDon_DAO.SelectallHoaDon();
        }
        //Hiển thị tất cả CT_HOADON
        public static DataTable SelectHoaDonCTByMa(int maHD)
        {
            return HoaDon_DAO.SelectHoaDonCTByMa(maHD);
        }
        //Thêm 1 Hóa Đơn
        public static bool ThemHoaDon(HoaDon_DTO hd)
        {
            if (HoaDon_DAO.Insert(hd) == "Success")
                return true;
            return false;
        }
        //Thêm 1 chi tiết hóa đơn
        public static string ThemHoaDonChiTiet(CT_HoaDon_DTO ct)
        {
            if (HoaDon_DAO.SelectSachLikeMaSach(ct.MaHD, ct.MaSach) == null)
            {
                return HoaDon_DAO.InsertChitiet(ct);
            }
            else
            {
                return "Sách này đã có rồi";
            }
        }
        //Update thuộc tính tổng tiền của hóa đơn
        public static void UpdateTongTien(HoaDon_DTO hd)
        {
            HoaDon_DAO.UpdateTongTien(hd);
        }
        //Update thuộc tính thanh toán của hóa đơn
        public static void UpdateThanhToan(HoaDon_DTO hd)
        {
            HoaDon_DAO.UpdateThanhtoan(hd);
        }
        //Update thuộc tính tổng tiền của hóa đơn
        public static void UpdateConLai(HoaDon_DTO hd)
        {
            HoaDon_DAO.UpdateConLai(hd);
        }

        //Trả về tổng thành tiền của các CT_HoaDon
        public static DataTable TongThanhTien(HoaDon_DTO hd)
        {
            return HoaDon_DAO.TongThanhTien(hd);
        }
        //Xóa hóa đơn bằng mã 
        public static string XoaHoaDonByMa(int ma)
        {
            return HoaDon_DAO.XoaHoaDonByMa(ma);
        }
        //Xóa chi tiết hóa đơn bằng mã
        public static string XoaCTHoaDonByMa(int ma)
        {
            return HoaDon_DAO.XoaCTHoaDonByMa(ma);
        }
        // Xóa hóa dơn từ mã khách hàng
        public static string XoaHoaDonbyMaKH(int ma)
        {
            return HoaDon_DAO.XoaHoaDonbyMaKH(ma);
        }
        //Kiểm tra có phải là HOADON đầu tiên
        public static bool KiemTraDauTien(int ngay, int thang, int nam)
        {
            DataTable dt = HoaDon_DAO.KiemTraDauTien(ngay, thang, nam);
            if (int.Parse(dt.Rows[0].ItemArray[0].ToString()) == 0)
            {
                return true;
            }
            return false;
        }

        // lấy MaHD từ MaKH
        public static string LayMaHoaDon(int ma)
        {
            string sql = "select * from HOADON where MaKhachHang=" + ma + "";
            DataTable dt = DataAccess.ThucThiQuery(sql);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                HoaDon_DTO tl = new HoaDon_DTO();
                //tl.MaTheLoai = (int)dt.Rows[0].ItemArray[0];
                tl.MaHD = int.Parse(dt.Rows[0].ItemArray[0].ToString());
                return tl.MaHD.ToString();
            }
        }
        //Xóa sách trong chi tiết hóa đơn
        public static string XoaSachtrongCT(int maSach, int MaHoaDon)
        {
            return HoaDon_DAO.XoaSachtrongCT(maSach, MaHoaDon);
        }

        //Lấy ra số lượng nhập
        public static UInt64 ThanhTienSach(int maHoaDon, int maSach)
        {
            return UInt64.Parse(HoaDon_DAO.ThanhTienSach(maHoaDon,maSach).Rows[0].ItemArray[0].ToString());
        }

        //Sửa thông tin SoLuong sách mà khách hàng mua trong bảng CT_HOADON
        public static string SuaSoLuongSachKHMua(CT_HoaDon_DTO kh)
        {
            if (!HoaDon_DAO.KiemtramaSach(kh.MaSach, kh.MaHD))
            {
                return HoaDon_DAO.UpdateSoLuongSachMua(kh);
            }
            else
            {
                return "Mã sách không có trong CSDL";
            }
        }

        //Lấy ra số lượng sách khách mua hiện tại
        public static int SoLuongKHMua(int maSach, int maHoaDon)
        {
            return int.Parse(HoaDon_DAO.GetSoLuongKHMua(maSach,maHoaDon).Rows[0].ItemArray[0].ToString());
        }
    }
}
