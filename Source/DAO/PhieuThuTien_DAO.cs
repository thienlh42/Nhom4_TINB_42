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
    public class PhieuThuTien_DAO
    {
        //Lấy ra đối tượng phiếu thu trùng với MaPT
        public static PhieuThuTien_DTO GetPhieuThuByMa(int Ma)
        {
            string sql = "select * from PHIEUTHUTIEN where MaPT=" + Ma + "";
            DataTable dt = DataAccess.ThucThiQuery(sql);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                PhieuThuTien_DTO pt = new PhieuThuTien_DTO();
                pt.MaPT = int.Parse(dt.Rows[0].ItemArray[0].ToString());
                pt.NgayLap = dt.Rows[0].ItemArray[2].ToString();
                pt.SoTienThu = UInt64.Parse(dt.Rows[0].ItemArray[3].ToString());
                pt.MaKhachHang = int.Parse(dt.Rows[0].ItemArray[1].ToString());
                return pt;
            }

        }
        //Lấy tất cả phiếu thu
        public static DataTable GetPhieuThuAll()
        {
            string sql = "select * from PHIEUTHUTIEN";
            return DataAccess.ThucThiQuery(sql);
        }
        //Chèn thêm 1 phiếu thu
        public static string Insert(PhieuThuTien_DTO pt)
        {
            string sql = "insert into PHIEUTHUTIEN(NgayLap,SoTienThu,MaKhachHang,TienNoBanDau) values('" + pt.NgayLap + "'," + pt.SoTienThu + "," + pt.MaKhachHang + "," + pt.TienNoBanDau + ")";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Cập nhật 1 phiếu thu
        public static string Update(PhieuThuTien_DTO pt)
        {
            string sql = "Update  PHIEUTHUTIEN set MaKhachHang=" + pt.MaKhachHang + ",NgayLap ='" + pt.NgayLap + "',SoTienThu=" + pt.SoTienThu + " where MaPT=" + pt.MaPT + "";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Xóa phiếu thu
        public static string Delete(PhieuThuTien_DTO pt)
        {
            string sql = "delete from PHIEUTHUTIEN where MaPT= " + pt.MaPT + "";
            return DataAccess.ThucThiNonQuery(sql);
        }

        //public static DataTable GetPhieuThuCondition(int MaPhieuThu)
        //{
        //    string sql = "select * from PHIEUTHUTIEN,KHACHHANG where MaPT = " + MaPhieuThu;
        //    return DataAccess.ThucThiQuery(sql);
        //}

        //Xóa phiếu thu
        public static string DeletebyMaKH(int pt)
        {
            string sql = "delete from PHIEUTHUTIEN where MaKhachHang= " + pt + "";
            return DataAccess.ThucThiNonQuery(sql);
        }

        //Lấy ra Mã phieu thu tiền mới nhất của phiếu thu tiền
        static public DataTable LayMaPhieuMoiNhat(int MaKH)
        {
            string sql = "select MAX(MaPT) from PHIEUTHUTIEN where MaKhachHang =" + MaKH +"";
            return DataAccess.ThucThiQuery(sql);
        }

        //Trả về 1 bảng chứa thông tin của một MaPT giống tên với MaPT cần tìm
        static public DataTable SelectMaPTLikeMaPT(PhieuThuTien_DTO pt)
        {
            string sql = "select * from PHIEUTHUTIEN where MaPT=" + pt.MaPT + "";
            return DataAccess.ThucThiQuery(sql);
        }

        //Lấy ra số tiền nợ ban đầu của phiếu thu tiền
        static public DataTable LayTienNoBanDau(int MaPT)
        {
            string sql = "select TienNoBanDau from PHIEUTHUTIEN where MaPT =" + MaPT + "";
            return DataAccess.ThucThiQuery(sql);
        }
    }
}
