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
    public class Sach_BUS
    {
        //chọn ra thông tin của 1 cuốn sách từ 2 bảng DAUSACH và SACH
        public static DataTable SelectThongTinSach()
        {
            return Sach_DAO.SelectThongTinSach();
        }
        //Trả về thông tin sách Full
        public static DataTable SelectThongTinSachFull()
        {
            return Sach_DAO.SelectThongTinSachFull();
        }
        //Thêm 1 cuốn sách vào bảng SÁCH
        static public string Insert(Sach_DTO s)
        {
            return Sach_DAO.Insert(s);
        }
        //Cập nhật 1 cuốn sách
        public static string CapNhatSach(Sach_DTO s)
        {
            if (Sach_DAO.SelectSachTheoMa(s.MaSach) != null)
            {
                return Sach_DAO.Update(s);
            }
            else
            {
                return "Mã sách không tồn tại trong CSDL";
            }
        }
        ////Trả về bảng chứa thông tin theo TacGia trong form DanhSachSach
        //static public DataTable SelectSachLikeTacGiaDanhSachSach(CT_TacGia_DTO ct)
        //{
        //    return Sach_DAO.SelectSachLikeTacGiaDanhSachSach(ct);
        //}
        ////Trả về bảng chứa thông tin theo TenDauSach trong form DanhSachSach
        //static public DataTable SelectSachLikeNameDanhSachSach(DauSach_DTO ds)
        //{
        //    return Sach_DAO.SelectSachLikeNameDanhSachSach(ds);
        //}
        ////Trả về bảng chứa thông tin theo NhaXuatBan trong form DanhSachSach
        //static public DataTable SelectSachLikeNhaXuatBanDanhSachSach(Sach_DTO s)
        //{
        //    return Sach_DAO.SelectSachLikeNhaXuatBanDanhSachSach(s);
        //}
        //Trả về bảng chứa thông tin theo MaTheLoai
        public static DataTable SelectSachLikeMaTheLoaiDanhSachSach(Sach_DTO a)
        {
            return Sach_DAO.SelectSachLikeMaTheLoaiDanhSachSach(a);
        }
        //Trả về bảng chứa thông tin theo MaSach
        public static DataTable SelectSachLikeMaSachDanhSachSach(Sach_DTO a)
        {
            return Sach_DAO.SelectSachLikeMaSachDanhSachSach(a);
        }
        //Trả về bảng null
        public static DataTable SelectSachNull()
        {
            return Sach_DAO.SelectSachNull();
        }
        //Update thuộc tính số lượng tồn trong bảng SACH
        static public void UpdateSoLuongTonVaDonGiaBan(Sach_DTO s)
        {
            Sach_DAO.UpdateSoLuongTonVaDonGiaBan(s);
        }
        //Update thuộc tính số lượng tồn
        static public string UpdateSoLuongTon(Sach_DTO s)
        {
            return Sach_DAO.UpdateSoLuongTon(s);
        }
        public static string Laytensach (int ma)
        {
            string sql = "select * from SACH where MaSach=" + ma + "";
            DataTable dt = DataAccess.ThucThiQuery(sql);
            if (dt.Rows.Count == 0)
            {
                return null;
            }
            else
            {
                TheLoai_DTO tl = new TheLoai_DTO();
                //tl.MaTheLoai = (int)dt.Rows[0].ItemArray[0];
                tl.TenTheLoai = dt.Rows[0].ItemArray[1].ToString();
                return tl.TenTheLoai;
            }
        }
        //Lấy tất cả thông tin của đầu sách
        public static DataTable SelectTenSachAll()
        {
            return Sach_DAO.SelectTenSachAll();
        }

        //Thêm sách vào cơ sở dữ liệu
        public static string ThemSach(Sach_DTO ds)
        {
            if (Sach_DAO.SelectSachByName(ds.TenSach) == null)
            {
                return Sach_DAO.Insert(ds);
            }
            else
            {
                return "Đầu sách đã tồn tại trong CSDL";
            }
        }
    }
}
