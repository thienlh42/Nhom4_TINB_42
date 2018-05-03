using BUS;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_Nhasach
{
    public partial class frmManHinhChinh : Form
    {
        public frmManHinhChinh()
        {
            InitializeComponent();
        }
        public void hienthi(int q)
        {
            if (q == 1)
            {
                đăngNhậpToolStripMenuItem.Enabled = false;

                // tại sao phải có này
                // vì ban đầu trị Load form frmManHinhChinh lên thì hienthi(4) nên nó sẽ ẩn những nút ngay chỗ else nên khi đăng nhập xong ta phải cho nó hiện lại
                quảnLíToolStripMenuItem.Enabled = true;
                đăngXuấtToolStripMenuItem.Enabled = true;
                đổiMậtKhẩuToolStripMenuItem.Enabled = true;
                quảnLíTàiKhoảnToolStripMenuItem.Enabled = true;
                báoCáoToolStripMenuItem.Enabled = true;
                thayĐổiQuyĐịnhToolStripMenuItem.Enabled = true;
            }
            else if (q == 2)
            {
                đăngNhậpToolStripMenuItem.Enabled = false;
                thayĐổiQuyĐịnhToolStripMenuItem.Enabled = false;
                quảnLíTàiKhoảnToolStripMenuItem.Enabled = false;


                // tương tự như (q==1) phải cho nó hiện lại
                quảnLíToolStripMenuItem.Enabled = true;
                đăngXuấtToolStripMenuItem.Enabled = true;
                đổiMậtKhẩuToolStripMenuItem.Enabled = true;
                báoCáoToolStripMenuItem.Enabled = true;
            }
            else
            {
                quảnLíToolStripMenuItem.Enabled = false;
                đăngXuấtToolStripMenuItem.Enabled = false;
                đổiMậtKhẩuToolStripMenuItem.Enabled = false;
                quảnLíTàiKhoảnToolStripMenuItem.Enabled = false;
                báoCáoToolStripMenuItem.Enabled = false;
                thayĐổiQuyĐịnhToolStripMenuItem.Enabled = false;
            }
        }
        //Cập nhật tồn đầu, cơ sở dữ liệu ban đầu
        public void CapNhatTonDau()
        {
            BaoCaoTon_DTO r = new BaoCaoTon_DTO();
            r.Thang = DateTime.Now.Month;
            r.Nam = DateTime.Now.Year;
            DataTable dt = BaoCaoTon_BUS.GetMaSach();
            int n = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < n; i++)
                {
                    r.MaSach = int.Parse(dt.Rows[i].ItemArray[0].ToString());
                    if (BaoCaoTon_BUS.CheckThongTin(r) == false)
                    {
                        r.TonDau = BaoCaoTon_BUS.GetSoLuongTon(r.MaSach);
                        string ketQua = BaoCaoTon_BUS.ThemTonDau(r);
                        if (ketQua != "Success")
                        {
                            MessageBox.Show(ketQua);
                            return;
                        }
                    }
                }
            }
        }

        // cập nhật nợ đầu, cơ sở dữ liệu ban đầu
        public void CapNhatNoDau()
        {
            BaoCaoCongNo_DTO r = new BaoCaoCongNo_DTO();
            r.Thang = DateTime.Now.Month;
            r.Nam = DateTime.Now.Year;
            DataTable dt = BaoCaoCongNo_BUS.GetMaKhachHang();
            int n = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < n; i++)
                {
                    r.MaKhachHang = int.Parse(dt.Rows[i].ItemArray[0].ToString());
                    if (BaoCaoCongNo_BUS.CheckThongTin(r) == false)
                    {
                        r.NoDau = BaoCaoCongNo_BUS.GetSoTienNo(r.MaKhachHang);
                        string ketQua = BaoCaoCongNo_BUS.ThemTonDau(r);
                        if (ketQua != "Success")
                        {
                            MessageBox.Show(ketQua);
                            return;
                        }
                    }
                }
            }
        }

        private void frmManHinhChinh_Load(object sender, EventArgs e)
        {
            CapNhatTonDau();
            //CapNhatNoDau();
            //Hiển thị với quyền 4 (ko có trong CSDL <=> Khách)
            hienthi(4);
            Program.frm_Dangnhap.MdiParent = this;
            Program.frm_Dangnhap.Run_MAIN = new frmDangNhap.RunMAIN(hienthi);
            Program.frm_Dangnhap.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Đóng tất cả các form đang chạy
            //Duyệt qua tất cả các form là form con của form đang chạy rồi đóng
            foreach (Form frm in this.MdiChildren)
            {
                frm.Hide();
            }

            hienthi(4);
            Program.frm_Dangnhap.MdiParent = this;
            Program.frm_Dangnhap.Run_MAIN = new frmDangNhap.RunMAIN(hienthi);

            MessageBox.Show("Đăng xuất thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            Program.frm_Dangnhap.Show();
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.frm_Doimatkhau.MdiParent = this;
            Program.frm_Doimatkhau.Show();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void quảnLíSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuanLiSach frm = new frmQuanLiSach();
            frm.MdiParent = this;
            frm.Show();
        }

        private void phiếuNhậpSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLapPhieuNhapSach frm = new frmLapPhieuNhapSach();
            frm.MdiParent = this;
            frm.Show();
        }

        private void phiếuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHoaDonBanSach frm = new frmHoaDonBanSach();
            frm.MdiParent = this;
            frm.Show();
        }

        private void lậpPhiếuThuTiềnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLapPhieuThuTien frm = new frmLapPhieuThuTien();
            frm.MdiParent = this;
            frm.Show();
        }

        private void danhSáchSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDanhSachSach frm = new frmDanhSachSach();
            frm.MdiParent = this;
            frm.Show();
        }

        private void danhSáchKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDanhSachKhachHang frm = new frmDanhSachKhachHang();
            frm.MdiParent = this;
            frm.Show();
        }

        // cập nhật lại dữ liệu tồn cuối
        public void CapNhatPhatSinhTonCuoi()
        {
            BaoCaoTon_DTO r = new BaoCaoTon_DTO();
            r.Thang = DateTime.Now.Month;
            r.Nam = DateTime.Now.Year;
            DataTable dt = BaoCaoTon_BUS.GetMaSach();
            int n = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < n; i++)
                {
                    r.MaSach = int.Parse(dt.Rows[i].ItemArray[0].ToString());
                    r.TonCuoi = BaoCaoTon_BUS.GetSoLuongTon(r.MaSach);
                    r.PhatSinh = BaoCaoTon_BUS.GetPhatSinh(r.Thang, r.Nam, r.MaSach);
                    if (BaoCaoTon_BUS.CheckThongTin(r) == false)
                    {
                        string ketQua = BaoCaoTon_BUS.ThemTonCuoiPhatSinh(r);
                        if (ketQua != "Success")
                        {
                            MessageBox.Show(ketQua);
                            return;
                        }
                    }
                    else
                    {
                        string ketQua = BaoCaoTon_BUS.CapNhatTonCuoiPhatSinh(r);
                        if (ketQua != "Success")
                        {
                            MessageBox.Show(ketQua);
                            return;
                        }
                    }
                }
            }
        }

        // Cập nhật lại dữ liệu nợ cuối
        public void CapNhatPhatSinhNoCuoi()
        {
            BaoCaoCongNo_DTO r = new BaoCaoCongNo_DTO();
            r.Thang = DateTime.Now.Month;
            r.Nam = DateTime.Now.Year;
            DataTable dt = BaoCaoCongNo_BUS.GetMaKhachHang();
            int n = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < n; i++)
                {
                    r.MaKhachHang = int.Parse(dt.Rows[i].ItemArray[0].ToString());
                    r.NoCuoi = BaoCaoCongNo_BUS.GetSoTienNo(r.MaKhachHang);
                    r.PhatSinh = BaoCaoCongNo_BUS.GetPhatSinh(r.Thang, r.Nam, r.MaKhachHang);
                    if (BaoCaoCongNo_BUS.CheckThongTin(r) == false)
                    {
                        string ketQua = BaoCaoCongNo_BUS.ThemNoCuoiPhatSinh(r);
                        if (ketQua != "Success")
                        {
                            MessageBox.Show(ketQua);
                            return;
                        }
                    }
                    else
                    {
                        string ketQua = BaoCaoCongNo_BUS.CapNhatNoCuoiPhatSinh(r);
                        if (ketQua != "Success")
                        {
                            MessageBox.Show(ketQua);
                            return;
                        }
                    }

                }
            }
        }

        private void báoCáoTồnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBaoCaoTon frm = new frmBaoCaoTon();
            frm.MdiParent = this;
            frm.Show();
        }

        private void báoCáoCôngNợToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBaoCaoCongNo frm = new frmBaoCaoCongNo();
            frm.MdiParent = this;
            frm.Show();
        }

        private void frmManHinhChinh_FormClosing(object sender, FormClosingEventArgs e)
        {
            CapNhatPhatSinhTonCuoi();
            CapNhatPhatSinhNoCuoi();
            foreach (Form frm in this.MdiChildren)
                frm.Hide();
            Application.Exit();
        }

        private void quảnLíKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuanLyKhachHang frm = new frmQuanLyKhachHang();
            frm.MdiParent = this;
            frm.Show();
        }

        private void quảnLíThểLoạiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmQuanLiTheLoai frm = new frmQuanLiTheLoai();
            frm.MdiParent = this;
            frm.Show();
        }

        private void quảnLíTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.frm_Quanlytaikhoan.MdiParent = this;
            Program.frm_Quanlytaikhoan.Show();
        }

        private void thayĐổiQuyĐịnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmThayDoiQuyDinh frm = new frmThayDoiQuyDinh();
            frm.MdiParent = this;
            frm.Show();
        }

        private void thôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmThongTin frm = new frmThongTin();
            //frm.MdiParent = this;
            //frm.Show();
        }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.frm_Dangnhap.MdiParent = this;
            Program.frm_Dangnhap.Run_MAIN = new frmDangNhap.RunMAIN(hienthi);
            Program.frm_Dangnhap.Show(); 
        }
    }
}
