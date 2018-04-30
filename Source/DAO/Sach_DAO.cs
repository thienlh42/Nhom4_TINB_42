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
    public class Sach_DAO
    {

        //chọn ra thông tin của 1 cuốn sách từ 2 bảng DAUSACH và SACH
        public static DataTable SelectThongTinSach()
        {
            string sql = "select MaSach,TenSach,MaTheLoai,DonGiaBan,SoLuongTon from SACH";
            return DataAccess.ThucThiQuery(sql);
        }
        //Trả về thông tin sách Full
        public static DataTable SelectThongTinSachFull()
        {
            string sql = "select MaSach,TenSach,MaTheLoai,TacGia,DonGiaBan,SoLuongTon from SACH";
            return DataAccess.ThucThiQuery(sql);
        }
        //Trả về đối tượng SACH giống với tên
        public static string Insert(Sach_DTO s)
        {
            string sql = "insert into SACH(TenSach,MaTheLoai,TacGia,SoLuongTon,DonGiaBan) values(N'" + s.TenSach+ "'," + s.MaTheLoai+ ",N'"+ s.TacGia+ "'," + s.SoLuongTon + "," + s.DonGiaBan + ")";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Trả về đối tượng Sach_DTO bằng cách lọc theo mã sách chọn phần tử hàng đầu tiên
        public static Sach_DTO SelectSachTheoMa(int ma)
        {
            string sql = "select * from SACH where MaSach=" + ma + "";
            DataTable dt = DataAccess.ThucThiQuery(sql);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                Sach_DTO s = new Sach_DTO();
                s.MaSach = int.Parse(dt.Rows[0].ItemArray[0].ToString());
                return s;
            }
        }
        //Cập nhật 1 cuốn sách
        public static string Update(Sach_DTO s)
        {
            string sql = "update  SACH set TenSach= (N'" + s.TenSach + "'),MaTheLoai=(" + s.MaTheLoai+"),TacGia = (N'"+ s.TacGia +"'),SoLuongTon=(" + s.SoLuongTon +"),DonGiaBan=("+ s.DonGiaBan + ")" + " where MaSach = " + s.MaSach + "";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Trả về bảng null
        public static DataTable SelectSachNull()
        {
            string sql = "select MaSach,TenSach,TacGia,MaTheLoai,DonGiaBan,SoLuongTon from SACH where MaTheLoai=null";
            return DataAccess.ThucThiQuery(sql);
        }
        //Trả về bảng chứa thông tin theo MaTheLoai
        public static DataTable SelectSachLikeMaTheLoaiDanhSachSach(Sach_DTO s)
        {
            string sql = "select MaSach,TenSach,TacGia,MaTheLoai,DonGiaBan,SoLuongTon from SACH where MaTheLoai=" + s.MaTheLoai + "";
            return DataAccess.ThucThiQuery(sql);
        }
        //Trả về bảng chứa thông tin theo MaSach
        public static DataTable SelectSachLikeMaSachDanhSachSach(Sach_DTO s)
        {
            string sql = "select MaSach,TenSach,TacGia,MaTheLoai,DonGiaBan,SoLuongTon from SACH where MaSach=" + s.MaSach + "";
            return DataAccess.ThucThiQuery(sql);
        }
        //Update thuộc tính số lượng tồn trong bảng SACH
        public static void UpdateSoLuongTonVaDonGiaBan(Sach_DTO s)
        {
            string sql = "update SACH set SoLuongTon=(" + s.SoLuongTon + "),DonGiaBan=(" + s.DonGiaBan + ") where MaSach = " + s.MaSach + "";
            DataAccess.ThucThiNonQuery(sql);
        }
        //Update thuộc tính số lượng tồn
        public static string UpdateSoLuongTon(Sach_DTO s)
        {
            string sql = "update SACH set SoLuongTon=(" + s.SoLuongTon + ") where MaSach = " + s.MaSach + "";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Lấy tất cả thông tin của đầu sách
        public static DataTable SelectTenSachAll()
        {
            string sql = "select * from SACH";
            return DataAccess.ThucThiQuery(sql);
        }
        //Trả về đối tượng Sach_DTO bằng cách lọc theo mã sách chọn phần tử hàng đầu tiên
        public static Sach_DTO SelectSachByName(string TenSach)
        {
            string sql = "select * from SACH where TenSach = N'" + TenSach + "'";
            DataTable dt = DataAccess.ThucThiQuery(sql);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                Sach_DTO s = new Sach_DTO();
                s.TenSach = dt.Rows[0].ItemArray[1].ToString();
                return s;
            }
        }
    }
}
