using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
namespace DAO
{
    public class HoaDon_DAO
    {
        //Hiển thị tất cả hóa đơn
        public static DataTable SelectallHoaDon()
        {
            string sql = "select * from HOADON";
            return DataAccess.ThucThiQuery(sql);
        }
        //Hiển thị tất cả CT_HOADON
        public static DataTable SelectHoaDonCTByMa(int maHD)
        {
            string sql = "select * from CT_HOADON where MaHD = " + maHD + "";
            return DataAccess.ThucThiQuery(sql);
        }
        //Thêm 1 hóa đơn
        public static string Insert(HoaDon_DTO hd)
        {
            string sql = "insert into HOADON(MaKhachHang,NgayLap,TongTien,ThanhToan,ConLai,TenKhachHang) values(" + hd.MaKhachHang + ",'" + hd.NgayLap + "'," + hd.TongTien + "," + hd.ThanhToan + "," + hd.ConLai + ",N'" + hd.TenKhachHang + "')";
            return DataAccess.ThucThiNonQuery(sql);
        }

        //Trả về đối tượng CT_HoaDon_DTO theo Mã sách và Mã hóa đơn
        static public CT_HoaDon_DTO SelectSachLikeMaSach(int mahoadon, int masach)
        {
            string sql = "select * from CT_HOADON where ((MaHD=" + mahoadon + ")AND(MaSach=" + masach + ") )";

            DataTable dt = DataAccess.ThucThiQuery(sql);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                CT_HoaDon_DTO ct = new CT_HoaDon_DTO();
                ct.MaSach = int.Parse(dt.Rows[0].ItemArray[0].ToString());
                return ct;
            }
        }
        //Thêm 1 Chi tiết hóa đơn
        public static string InsertChitiet(CT_HoaDon_DTO ct)
        {
            string sql = "insert into CT_HOADON(MaHD,MaSach,SoLuong,DonGia,ThanhTien) values(" + ct.MaHD + "," + ct.MaSach + "," + ct.SoLuong + "," + ct.DonGia + "," + ct.ThanhTien + ")";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Update thuộc tính tổng tiền của hóa đơn
        public static void UpdateTongTien(HoaDon_DTO hd)
        {
            string sql = "update HOADON set TongTien=" + hd.TongTien + " where MaHD=" + hd.MaHD + "";
            DataAccess.ThucThiNonQuery(sql);
        }
        // update thuộc tính tiền thanh toán
        public static void UpdateThanhtoan(HoaDon_DTO hd)
        {
            string sql = "update HOADON set ThanhToan=" + hd.ThanhToan + " where MaHD=" + hd.MaHD + "";
            DataAccess.ThucThiNonQuery(sql);
        }
        // update thuộc tính tiền tiền còn lại
        public static void UpdateConLai(HoaDon_DTO hd)
        {
            string sql = "update HOADON set ConLai=" + hd.ConLai + " where MaHD=" + hd.MaHD + "";
            DataAccess.ThucThiNonQuery(sql);
        }

        //Trả về tổng thành tiền của các CT_HoaDon
        public static DataTable TongThanhTien(HoaDon_DTO hd)
        {
            string sql = "select sum(ThanhTien) from CT_HOADON where MaHD = " + hd.MaHD + "";
            return DataAccess.ThucThiQuery(sql);
        }
        //Xóa hóa đơn bằng mã 
        public static string XoaHoaDonByMa(int ma)
        {
            string sql = "delete from HOADON where MaHD=" + ma + "";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Xóa chi tiết hóa đơn bằng mã
        public static string XoaCTHoaDonByMa(int ma)
        {
            string sql = "delete from CT_HOADON where MaHD=" + ma + "";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Kiểm tra có phải là HOADON đầu tiên
        static public DataTable KiemTraDauTien(int ngay, int thang, int nam)
        {
            string sql = "select count(*) from HOADON where day(NgayLap) between 1 and " + ngay + " and year(NgayLap) = " + nam + " and MONTH(NgayLap) = " + thang + "";
            return DataAccess.ThucThiQuery(sql);
        }

        // 

        // Xóa hóa đơn có chứa mã khách hàng
        public static string XoaHoaDonbyMaKH(int ma)
        {
            string sql = "delete from HOADON where MaKhachHang=" + ma + "";
            return DataAccess.ThucThiNonQuery(sql);
        }

        // Xóa Sách trong hóa đơn
        public static string XoaSachtrongCT(int maSach, int MaHoaDon)
        {
            string sql = "delete from CT_HOADON where MaSach=" + maSach + " and MaHD =" + MaHoaDon + "";
            return DataAccess.ThucThiNonQuery(sql);
        }

        // Lấy Thành tiền của sách mà khách mua
        public static DataTable ThanhTienSach (int MaHD, int maSach)
        {
            string sql = "select ThanhTien from CT_HOADON where MaHD =" + MaHD + "and MaSach=" + maSach + "";
            return DataAccess.ThucThiQuery(sql);
        }

        // Kiểm tra maHD có tồn tại trong CT_HOADON
        public static bool KiemtramaHD(int Ma)
        {
            string sql = "select * from CT_HOADON where MaHD=" + Ma + "";
            DataTable dt = DataAccess.ThucThiQuery(sql);
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Kiểm tra maSach có tồn tại trong CT_HOADON
        public static bool KiemtramaSach(int MaSach, int MaHoaDon)
        {
            string sql = "select * from CT_HOADON where MaSach=" + MaSach + " and MaHD=" + MaHoaDon +"";
            DataTable dt = DataAccess.ThucThiQuery(sql);
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Cập nhật lại số lượng sách mà khách hàng mua
        static public string UpdateSoLuongSachMua(CT_HoaDon_DTO p)
        {
            string sql = "update CT_HOADON set SoLuong =" + p.SoLuong + ", ThanhTien =" + p.ThanhTien + "where MaSach =" + p.MaSach +" and MaHD =" + p.MaHD +"";
            return DataAccess.ThucThiNonQuery(sql);
        }

        //Lấy ra số lượng sách của CT_HOADON sách theo MaSach
        static public DataTable GetSoLuongKHMua(int maSach, int maHoaDon)
        {
            string sql = "select SoLuong from CT_HOADON where MaSach=" + maSach + " and MaHD=" + maHoaDon +"";
            return DataAccess.ThucThiQuery(sql);
        }
    }
}
