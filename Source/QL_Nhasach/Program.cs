﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_Nhasach
{
    static class Program
    {
        public static frmManHinhChinh frm_MAIN;
        public static frmDangNhap frm_Dangnhap;
        public static frmDoiMatKhau frm_Doimatkhau;
        public static frmQuanLiTaiKhoan frm_Quanlytaikhoan;

        public static void khoitao()
        {
            frm_MAIN = new frmManHinhChinh();
            frm_Dangnhap = new frmDangNhap();
            frm_Doimatkhau = new frmDoiMatKhau();
            frm_Quanlytaikhoan = new frmQuanLiTaiKhoan();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            khoitao();
            //Application.Run(new frmManHinhChinh());
            //Application.Run(new frmHoaDonBanSach());
            //Application.Run(new frmLapPhieuNhapSach());
            Application.Run(new Connect());
        }
    }
}
