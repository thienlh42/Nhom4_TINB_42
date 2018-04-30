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
    public partial class frmLapPhieuThuTien : Form
    {
        public UInt64 luusotienthu;
        public static string maKH;
        public static string soTienNo;
        public frmLapPhieuThuTien()
        {
            InitializeComponent();
        }
        public void Enable(bool a)
        {
            txtSoTien.ReadOnly = !a;

            btnLuu.Enabled = a;
            btnKhongluu.Enabled = a;
        }

        public void HienThiPhieuThu()
        {
            Enable(false);
            dgvPhieuThuTien.DataSource = PhieuThuTien_BUS.GetPhieuThuAll();
        }

        public void Them()
        {
            PhieuThuTien_DTO pt = new PhieuThuTien_DTO();
            try
            {
                pt.MaKhachHang = int.Parse(txtMaKhachHang.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Mã khách hàng không được bỏ trống");
                return;
            }

            dtpNgayThuTien.Format = DateTimePickerFormat.Custom;
            dtpNgayThuTien.CustomFormat = "MM-dd-yy";
            pt.NgayLap = dtpNgayThuTien.Text;
            dtpNgayThuTien.Format = DateTimePickerFormat.Short;

            DataTable dt = ThamSo_BUS.GetThamSoAll();
            int ktchophep = int.Parse(dt.Rows[0].ItemArray[5].ToString());
            DataTable dt2 = ThamSo_BUS.SelectTienNoKH(int.Parse(txtMaKhachHang.Text));
            UInt64 tienno = UInt64.Parse(dt2.Rows[0].ItemArray[5].ToString());
            pt.TienNoBanDau = tienno;
            UInt64 tienthu;
            try
            {
                tienthu = UInt64.Parse(txtSoTien.Text);
                pt.SoTienThu = UInt64.Parse(txtSoTien.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Số tiền thu chưa nhập đúng quy định");
                return;
            }
            catch (OverflowException)
            {
                MessageBox.Show("Số tiền thu không được âm");
                return;
            }
            if (ktchophep == 1)
            {
                if (tienno < tienthu)
                {
                    MessageBox.Show("Tiền thu đã lớn hơn tiền khách hàng đang nợ");
                    return;
                }
            }
            else
            {
                if (tienno < tienthu)
                {

                    string ketQua2 = PhieuThuTien_BUS.ThemPhieuThu(pt);
                    if (ketQua2 != "Success")
                    {
                        MessageBox.Show(ketQua2, "Lỗi");
                    }
                    else
                    {
                        KhachHang_DTO kh = new KhachHang_DTO();
                        kh.MaKhachHang = int.Parse(txtMaKhachHang.Text);
                        kh.SoTienNo = 0;
                        KhachHang_BUS.UpdateTienNo(kh);
                        MessageBox.Show("Thành công");
                        HienThiPhieuThu();
                    }
                    return;
                }
            }
            UInt64 tiennonew = tienno - tienthu;

            string ketQua = PhieuThuTien_BUS.ThemPhieuThu(pt);
            if (ketQua != "Success")
            {
                MessageBox.Show(ketQua, "Lỗi");
            }
            else
            {
                KhachHang_DTO kh = new KhachHang_DTO();
                kh.MaKhachHang = int.Parse(txtMaKhachHang.Text);
                kh.SoTienNo = tiennonew;
                KhachHang_BUS.UpdateTienNo(kh);
                MessageBox.Show("Thành công");
                HienThiPhieuThu();
            }
        }

        public void CapNhat()
        {
            PhieuThuTien_DTO pt = new PhieuThuTien_DTO();
            pt.MaPT = int.Parse(txtMaPhieuThu.Text);

            dtpNgayThuTien.Format = DateTimePickerFormat.Custom;
            dtpNgayThuTien.CustomFormat = "MM-dd-yy";
            pt.NgayLap = dtpNgayThuTien.Text;
            dtpNgayThuTien.Format = DateTimePickerFormat.Short;

            try
            {
                int kh = int.Parse(txtMaKhachHang.Text);
                pt.MaKhachHang = kh;
            }
            catch (FormatException)
            {
                MessageBox.Show("Mã khách hàng không được bỏ trống");
                return;
            }

            if (int.Parse(txtMaPhieuThu.Text) != PhieuThuTien_BUS.PhieuNhapMoiNhat(int.Parse(txtMaKhachHang.Text)))
            {
                MessageBox.Show("Bạn chỉ được chỉnh sửa phiếu thanh toán mới nhất của khách hàng này." + "\n" + "Mã phiếu thanh toán mới nhất của khách hàng này là: " + PhieuThuTien_BUS.PhieuNhapMoiNhat(int.Parse(txtMaKhachHang.Text)));
                return;
            }

            DataTable dt = ThamSo_BUS.GetThamSoAll();
            int ktchophep = int.Parse(dt.Rows[0].ItemArray[5].ToString());
            DataTable dt2 = ThamSo_BUS.SelectTienNoKH(int.Parse(txtMaKhachHang.Text));
            UInt64 tienno = UInt64.Parse(dt2.Rows[0].ItemArray[5].ToString());
            UInt64 tienthu = 0;
            try
            {
                pt.SoTienThu = UInt64.Parse(txtSoTien.Text);
                tienthu = UInt64.Parse(txtSoTien.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Số tiền thu chưa nhập đúng quy định");
                return;
            }
            catch (OverflowException)
            {
                MessageBox.Show("Số tiền thu không được âm");
                return;
            }
           
            UInt64 tienthunew ;
            UInt64 tiennonew;
            if (ktchophep == 1)
            {
                if (tienthu > luusotienthu)
                {
                    //if (ktchophep == 1)
                    //{
                        if (tienno < tienthu - luusotienthu)
                        {
                            MessageBox.Show("Tiền thu đã lớn hơn tiền khách hàng đang nợ");
                            return;
                        }
                    //}
                    tienthunew = (tienthu - luusotienthu);
                    tiennonew = (UInt64)(tienno) - tienthunew;
                }
                else
                {
                    tienthunew = tienthu;
                    tiennonew = (UInt64)(tienno) + (luusotienthu - tienthu);
                }

                string ketQua = PhieuThuTien_BUS.SuaPhieuThu(pt);
                if (ketQua != "Success")
                {
                    MessageBox.Show(ketQua, "Lỗi");
                }
                else
                {
                    KhachHang_DTO kh = new KhachHang_DTO();
                    kh.MaKhachHang = int.Parse(txtMaKhachHang.Text);
                    kh.SoTienNo = tiennonew;
                    KhachHang_BUS.UpdateTienNo(kh);
                    MessageBox.Show("Thành công");
                    HienThiPhieuThu();
                }
            }
            else
            {
                uint tiennobandau = PhieuThuTien_BUS.LayTienNoBanDau(int.Parse(txtMaPhieuThu.Text));
                if (tienthu > luusotienthu)
                {
                    //if (ktchophep == 1)
                    //{
                    if (tienno < tienthu - luusotienthu)
                    {
                        tiennonew = 0;
                        string ketQua2 = PhieuThuTien_BUS.SuaPhieuThu(pt);
                        if (ketQua2 != "Success")
                        {
                            MessageBox.Show(ketQua2, "Lỗi");
                        }
                        else
                        {
                            KhachHang_DTO kh = new KhachHang_DTO();
                            kh.MaKhachHang = int.Parse(txtMaKhachHang.Text);
                            kh.SoTienNo = tiennonew;
                            KhachHang_BUS.UpdateTienNo(kh);
                            MessageBox.Show("Thành công");
                            HienThiPhieuThu();
                        }
                        return;
                    }
                    //}
                    tienthunew = (tienthu - luusotienthu);
                    tiennonew = (UInt64)(tienno) - tienthunew;
                }
                else
                {
                    if (tienno == 0)
                    {
                        tienthunew = tienthu;
                        tiennonew = tiennobandau - tienthu;
                    }
                    else
                    {
                        tienthunew = tienthu;
                        tiennonew = (UInt64)(tienno) + (luusotienthu - tienthu);
                    }
                }

                string ketQua = PhieuThuTien_BUS.SuaPhieuThu(pt);
                if (ketQua != "Success")
                {
                    MessageBox.Show(ketQua, "Lỗi");
                }
                else
                {
                    KhachHang_DTO kh = new KhachHang_DTO();
                    kh.MaKhachHang = int.Parse(txtMaKhachHang.Text);
                    kh.SoTienNo = tiennonew;
                    KhachHang_BUS.UpdateTienNo(kh);
                    MessageBox.Show("Thành công");
                    HienThiPhieuThu();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmDanhSachKhachHang f = new frmDanhSachKhachHang();
            f.ShowDialog();
            txtMaKhachHang.Text = maKH;
            txtSoTienNo.Text = soTienNo;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            txtMaKhachHang.Text = dgvPhieuThuTien.Rows[dong].Cells[1].Value.ToString();
            txtMaPhieuThu.Text = dgvPhieuThuTien.Rows[dong].Cells[0].Value.ToString();
            txtSoTien.Text = dgvPhieuThuTien.Rows[dong].Cells[3].Value.ToString();
            luusotienthu = UInt64.Parse(dgvPhieuThuTien.Rows[dong].Cells[3].Value.ToString());
            DataTable dt2 = ThamSo_BUS.SelectTienNoKH(int.Parse(txtMaKhachHang.Text));
            UInt64 tienno = UInt64.Parse(dt2.Rows[0].ItemArray[5].ToString());
            txtSoTienNo.Text = tienno.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Enable(true);
            btnCapNhat.Enabled = false;
            btnXoa.Enabled = false;
            txtMaKhachHang.Text = "";
            txtMaPhieuThu.Text = "";
            txtSoTien.Text = "";
            txtSoTienNo.Text = "";
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            Enable(true);
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            txtMaKhachHang.Text = "";
            txtSoTien.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (btnCapNhat.Enabled == false)
            {
                Them();
                btnCapNhat.Enabled = true;
                btnXoa.Enabled = true;
            }
            if (btnThem.Enabled == false)
            {
                CapNhat();
                btnThem.Enabled = true;
                btnXoa.Enabled = true;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            PhieuThuTien_DTO pt = new PhieuThuTien_DTO();
            pt.MaPT = int.Parse(txtMaPhieuThu.Text);
            string ketQua = PhieuThuTien_BUS.XoaPhieuThu(pt);
            if (ketQua != "Success")
            {
                MessageBox.Show(ketQua);
                return;
            }
            MessageBox.Show("Xóa thành công");
            HienThiPhieuThu();
        }

        private void btnKhongluu_Click(object sender, EventArgs e)
        {
            btnCapNhat.Enabled = true;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            HienThiPhieuThu();
        }

        private void frmLapPhieuThuTien_Load(object sender, EventArgs e)
        {
            colMaKhachHang.ValueMember = "MaKhachHang";
            colMaKhachHang.DisplayMember = "TenKhachHang";
            colMaKhachHang.DataSource = KhachHang_BUS.GetKhachHangAll();
            HienThiPhieuThu();
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        public string ten;
        public string diachi;
        public string sodienthoai;
        public string email;
        public string ngayHoaDon;
        public string soTienThu;
        private void brnXuat_Click(object sender, EventArgs e)
        {
            ten = KhachHang_BUS.Laykhachhang(int.Parse(txtMaKhachHang.Text));
            diachi = KhachHang_BUS.Laydiachikhachhang(int.Parse(txtMaKhachHang.Text));
            sodienthoai = KhachHang_BUS.LaysoDTkhachhang(int.Parse(txtMaKhachHang.Text));
            email = KhachHang_BUS.LayEmailkhachhang(int.Parse(txtMaKhachHang.Text)); 
            ngayHoaDon = dtpNgayThuTien.Text;
            soTienThu = txtSoTien.Text;
            //ReportPhieuThuTien r = new ReportPhieuThuTien(this);
            //r.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            PhieuThuTien_DTO pt = new PhieuThuTien_DTO();
            if (txtTimKiem.Text == "")
            {
                HienThiPhieuThu();
            }
            else
            {
                pt.MaPT = int.Parse(txtTimKiem.Text);
                dgvPhieuThuTien.DataSource = PhieuThuTien_BUS.SelectMaPTLikeMaPT(pt);
            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            PhieuThuTien_DTO pt = new PhieuThuTien_DTO();
            if (KhachHang_BUS.LayMakhachhang(txtTimKhachhang.Text) != null)
            {
                pt.MaPT = PhieuThuTien_BUS.PhieuNhapMoiNhat(int.Parse(KhachHang_BUS.LayMakhachhang(txtTimKhachhang.Text)));
                dgvPhieuThuTien.DataSource = PhieuThuTien_BUS.SelectMaPTLikeMaPT(pt);
            }
            else
            {
                HienThiPhieuThu();
            }
            
        }

        private void btnTimMaPT_Click(object sender, EventArgs e)
        {
            txtTimKiem.Enabled = true;
            txtTimKhachhang.Enabled = false;
        }

        private void btnTimKH_Click(object sender, EventArgs e)
        {
            txtTimKhachhang.Enabled = true;
            txtTimKiem.Enabled = false;
        }
    }
}
