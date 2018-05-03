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
    public class PhieuNhapSach_DAO
    {
        //Trả về tất cả thông tin của bảng PHIEUNHAPSACH
        public static DataTable SelectPhieuNhapSachAll()
        {
            string sql = "select * from PHIEUNHAPSACH";
            return DataAccess.ThucThiQuery(sql);
        }
        //Trả về tất cả thông tin của bảng CT_PHIEUNHAPSACH
        public static DataTable SelectCTPhieuNhapSachByMa(int maPNS)
        {
            string sql = "select * from CT_PHIEUNHAPSACH where MaPNS = " + maPNS + "";
            return DataAccess.ThucThiQuery(sql);
        }
        //Thêm 1 phiếu nhập
        static public string InsertPhieuNhap(PhieuNhapSach_DTO p)
        {
            string sql = "insert into PHIEUNHAPSACH(NgayNhap,TongTien) values('" + p.NgayNhap + "'," + p.TongTien + ")";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Lấy ra đối tượng CT_PhieuNhapSach_DTO bằng MaPhieuNhap và MaSach
        public static CT_PhieuNhapSach_DTO GetPhieuNhapByName(int maphieunhap, int masach)
        {
            string sql = "select * from CT_PHIEUNHAPSACH where ((MaPNS=" + maphieunhap + ")AND(MaSach = " + masach + "))";
            DataTable dt = DataAccess.ThucThiQuery(sql);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                CT_PhieuNhapSach_DTO pn = new CT_PhieuNhapSach_DTO();
                pn.MaPNS = (int)dt.Rows[0].ItemArray[0];
                return pn;
            }
        }
        //Thêm vào bảng CT_PHIEUNHAPSACH
        static public string Insert(CT_PhieuNhapSach_DTO p)
        {
            string sql = "insert into CT_PHIEUNHAPSACH(MaPNS,MaSach,TenSach,TheLoai,SoLuongNhap,DonGiaNhap,ThanhTien) values(" + p.MaPNS + "," + p.MaSach + ",N'" + p.TenSach + "',N'"+p.TenTheLoai+"'," + p.SoLuongNhap + "," + p.DonGiaNhap + "," + p.ThanhTien + ")";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Lấy ra tháng theo MaPNS
        static public DataTable GetThangByMaPNS(int ma)
        {
            string sql = "select Month(NgayNhap) from PHIEUNHAPSACH where MaPNS = " + ma + "";
            return DataAccess.ThucThiQuery(sql);
        }
        //Lấy ra năm theo MaPNS
        static public DataTable GetNamByMaPNS(int ma)
        {
            string sql = "select year(NgayNhap) from PHIEUNHAPSACH where MaPNS = " + ma + "";
            return DataAccess.ThucThiQuery(sql);
        }
        //Lấy ra tiền của các của phiếu nhập sách theo MaPNS
        static public DataTable GetTien(int maPNS)
        {
            string sql = "select TongTien from PHIEUNHAPSACH where MaPNS=" + maPNS + "";
            return DataAccess.ThucThiQuery(sql);
        }
        //Lấy ra tổng thành tiền của các CT_PHIEUNHAPSACH theo MaPNS
        static public DataTable GetTongThanhTien(int maPNS)
        {
            string sql = "select sum(ThanhTien) from CT_PHIEUNHAPSACH where MaPNS=" + maPNS + "";
            return DataAccess.ThucThiQuery(sql);
        }
        //Cập nhật tổng tiền của PHIEUNHAPSACH theo MaPNS
        static public void UpdateTongTien(PhieuNhapSach_DTO p)
        {
            string sql = "update PHIEUNHAPSACH set TongTien=" + p.TongTien + " where MaPNS=" + p.MaPNS + "";
            DataAccess.ThucThiNonQuery(sql);
        }
        //Kiểm tra có phải là PHIEUNHAPSACH đầu tiên
        static public DataTable KiemTraDauTien(int ngay, int thang, int nam, int maSach)
        {
            string sql = "select count(*) from PHIEUNHAPSACH p, CT_PHIEUNHAPSACH c where c.MaPNS=p.MaPNS and day(NgayNhap) between 1 and " + ngay + " and year(NgayNhap) = " + nam + " and MONTH(NgayNhap) = " + thang + " and MaSach=" + maSach + "";
            return DataAccess.ThucThiQuery(sql);
        }
        // Xóa phiếu nhập
        public static string XoaPhieuNhap(PhieuNhapSach_DTO obj)
        {
            string sql = string.Format("DELETE PHIEUNHAPSACH WHERE MaPNS = N'{0}'", obj.MaPNS);
            return DataAccess.ThucThiNonQuery(sql);
        }
        // Xóa sách trong phiếu nhập
        public static string XoaSachtrongPhieu (CT_PhieuNhapSach_DTO obj)
        {
            string sql = string.Format("DELETE CT_PHIEUNHAPSACH WHERE MaPNS = N'{0}'", obj.MaPNS);
            return DataAccess.ThucThiNonQuery(sql);
        }

        //Xóa sách nhập vào
        public static string XoaMotcuonsach (CT_PhieuNhapSach_DTO obj)
        {
            string sql = string.Format("DELETE CT_PHIEUNHAPSACH WHERE MaSach = N'{0}' and MaPNS = N'{1}'", obj.MaSach, obj.MaPNS);
            return DataAccess.ThucThiNonQuery(sql);
        }
        // kiem tra co mã sách không
         public static CT_PhieuNhapSach_DTO Kiemtramasach(int MaSach, int MaPNS)
        {
            string sql = "select * from CT_PHIEUNHAPSACH where MaSach=" + MaSach + " and MaPNS =" + MaPNS + "";
            DataTable dt = DataAccess.ThucThiQuery(sql);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                CT_PhieuNhapSach_DTO ct = new CT_PhieuNhapSach_DTO();
                ct.MaPNS = (int)dt.Rows[0].ItemArray[0];
                ct.MaSach = (int)dt.Rows[0].ItemArray[1];
                ct.TenSach = dt.Rows[0].ItemArray[2].ToString();
                ct.TenTheLoai = dt.Rows[0].ItemArray[3].ToString();
                ct.SoLuongNhap = (int)dt.Rows[0].ItemArray[4];
                ct.DonGiaNhap = UInt64.Parse(dt.Rows[0].ItemArray[5].ToString()); 
                ct.ThanhTien = UInt64.Parse(dt.Rows[0].ItemArray[6].ToString());
                return ct;
            }
        }

        // Update Sách
        static public string UpdateSachNhap(CT_PhieuNhapSach_DTO p)
        {
            string sql = "update CT_PHIEUNHAPSACH set MaSach =(" + p.MaSach + "),TenSach = (N'" + p.TenSach + "'),TheLoai = (N'" + p.TenTheLoai + "'),SoLuongNhap =(" + p.SoLuongNhap + "),DonGiaNhap =(" + p.DonGiaNhap + "), ThanhTien = (" + p.ThanhTien +") where MaSach=" + p.MaSach + " and MaPNS =" + p.MaPNS +"";
            //string sql = "update CT_PHIEUNHAPSACH set MSoLuongNhap =(" + p.SoLuongNhap + "),DonGiaNhap =(" + p.DonGiaNhap + ")" + "where p.MaPNS=" + p.MaPNS + "";
            return DataAccess.ThucThiNonQuery(sql);
        }

        //Lấy ra số lượng của phiếu nhập sách theo MaSach
        static public DataTable GetSoLuongNhap(int maSach)
        {
            string sql = "select SoLuongNhap from CT_PHIEUNHAPSACH where MaSach=" + maSach + "";
            return DataAccess.ThucThiQuery(sql);
        }
        // Kiểm tra maSach có tồn tại trong CT_HOADON
        public static bool KiemtramaSachCoTonTai(int MaSach, int MaHoaDon)
        {
            string sql = "select * from CT_PHIEUNHAPSACH where MaSach=" + MaSach + " and MaPNS=" + MaHoaDon +"";
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

    }
}
