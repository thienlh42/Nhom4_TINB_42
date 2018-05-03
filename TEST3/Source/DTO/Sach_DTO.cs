using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
namespace DTO
{
    public class Sach_DTO
    {
        int _maSach;

        public int MaSach
        {
            get { return _maSach; }
            set { _maSach = value; }
        }
        string _tenSach;

        public string TenSach
        {
            get { return _tenSach; }
            set { _tenSach = value; }
        }
        int _maTheLoai;

        public int MaTheLoai
        {
            get { return _maTheLoai; }
            set { _maTheLoai = value; }
        }
        int _soLuongTon;

        public int SoLuongTon
        {
            get { return _soLuongTon; }
            set { _soLuongTon = value; }
        }

        string _TacGia;

        public string TacGia
        {
            get { return _TacGia; }
            set { _TacGia = value; }
        }

        UInt64 _donGiaBan;

        public UInt64 DonGiaBan
        {
            get { return _donGiaBan; }
            set { _donGiaBan = value; }
        }
    }
}
