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
    public partial class frmDanhSachKhachHang : Form
    {
        private static string maKH;//Dùng để lấy mã khách hàng truyền cho form HoaDonBanSach và form LapPhieuThuTien
        private static string tenKH;
        private static string soTienNo;
        public frmDanhSachKhachHang()
        {
            InitializeComponent();
        }
        public void HienThiDanhSach()
        {
            dgvKhachHang.DataSource = KhachHang_BUS.GetKhachHangAll();
        }

        private void frmQuanLyKhachHang_Load(object sender, EventArgs e)
        {
            HienThiDanhSach();
        }

        public void TimKiemTenKhachHang()
        {
            KhachHang_DTO kh = new KhachHang_DTO();
            kh.TenKhachHang = txtTimKiem.Text;
            dgvKhachHang.DataSource = KhachHang_BUS.SelectKhachHangLikeTen(kh);
        }
        public void TimKiemDiaChi()
        {

            KhachHang_DTO kh = new KhachHang_DTO();
            kh.DiaChi = txtTimKiem.Text;
            dgvKhachHang.DataSource = KhachHang_BUS.SelectKhachHangLikeDiaChi(kh);
        }
        public void TimKiemEmail()
        {

            KhachHang_DTO kh = new KhachHang_DTO();
            kh.Email = txtTimKiem.Text;
            dgvKhachHang.DataSource = KhachHang_BUS.SelectKhachHangLikeEmail(kh);
        }
        public void TimKiemDienThoai()
        {
            KhachHang_DTO kh = new KhachHang_DTO();
            kh.SDT = txtTimKiem.Text;
            dgvKhachHang.DataSource = KhachHang_BUS.SelectKhachHangLikeDienThoai(kh);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (rdTenKhachHang.Checked == true)
            {
                TimKiemTenKhachHang();
            }
            if (rdDiaChi.Checked == true)
            {
                TimKiemDiaChi();
            }
            if (rdDienThoai.Checked == true)
            {
                TimKiemDienThoai();
            }
            if (rdEmail.Checked == true)
            {
                TimKiemEmail();
            }
        }

        private void rdTenKhachHang_CheckedChanged(object sender, EventArgs e)
        {
            TimKiemTenKhachHang();
        }

        private void rdDiaChi_CheckedChanged(object sender, EventArgs e)
        {
            TimKiemDiaChi();
        }

        private void rdDienThoai_CheckedChanged(object sender, EventArgs e)
        {
            TimKiemDienThoai();
        }

        private void rdEmail_CheckedChanged(object sender, EventArgs e)
        {
            TimKiemEmail();
        }

        private void btnHienThiTatCa_Click(object sender, EventArgs e)
        {
            HienThiDanhSach();
        }

        private void dgvKhachHang_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            maKH = dgvKhachHang.Rows[dong].Cells[0].Value.ToString();
            tenKH = dgvKhachHang.Rows[dong].Cells[1].Value.ToString();
            soTienNo = dgvKhachHang.Rows[dong].Cells[5].Value.ToString();

            // thêm vào các textbox của thêm khách hàng
            txtMaKhachHang.Text = dgvKhachHang.Rows[dong].Cells[0].Value.ToString();
            txtTenKhachHang.Text = dgvKhachHang.Rows[dong].Cells[1].Value.ToString();
            txtDienThoai.Text = dgvKhachHang.Rows[dong].Cells[3].Value.ToString();
            txtDiaChi.Text = dgvKhachHang.Rows[dong].Cells[2].Value.ToString();
            txtEmail.Text = dgvKhachHang.Rows[dong].Cells[4].Value.ToString();
            txtSoTienNo.Text = dgvKhachHang.Rows[dong].Cells[5].Value.ToString();
        }

        private void btnChon_Click(object sender, EventArgs e)
        {
            frmHoaDonBanSach.maKH = maKH;
            frmHoaDonBanSach.tenKH = tenKH;
            frmLapPhieuThuTien.maKH = maKH;
            frmLapPhieuThuTien.soTienNo = soTienNo;
            this.Close();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnDongY.Enabled = true;
            txtTenKhachHang.Enabled = true;
            txtDienThoai.Enabled = true;
            txtDiaChi.Enabled = true;
            txtEmail.Enabled = true;

            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
            txtEmail.Text = "";
            txtMaKhachHang.Text = "";
            txtSoTienNo.Text = "";
            txtTenKhachHang.Text = "";
        }

        private void btnDongY_Click(object sender, EventArgs e)
        {
            KhachHang_DTO kh = new KhachHang_DTO();
            if (txtTenKhachHang.Text != "")
            {
                kh.TenKhachHang = txtTenKhachHang.Text;
            }
            else
            {
                MessageBox.Show("Tên khách hàng không được để trống", "Thông báo");
                return;
            }
            if (txtDienThoai.Text != "")
            {
                kh.SDT = txtDienThoai.Text;
                try
                {
                    int sdt = int.Parse(txtDienThoai.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Điện thoại phải là số");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Điện thoại không được để trống", "Thông báo");
                return;
            }
            if (txtDiaChi.Text != "")
            {
                kh.DiaChi = txtDiaChi.Text;
            }
            else
            {
                MessageBox.Show("Địa chỉ không được để trống", "Thông báo");
                return;
            }
            if (txtEmail.Text != "")
            {
                kh.Email = txtEmail.Text;
            }
            else
            {
                MessageBox.Show("Email không được để trống", "Thông báo");
                return;
            }
            kh.SoTienNo = 0;
            string ketQua = KhachHang_BUS.ThemKhachHang(kh);
            if (ketQua != "Success")
            {
                MessageBox.Show(ketQua, "Lỗi");
                return;
            }
            MessageBox.Show("Thêm thành công");
            HienThiDanhSach();

            btnDongY.Enabled = false;
            txtTenKhachHang.Enabled = false;
            txtDienThoai.Enabled = false;
            txtDiaChi.Enabled = false;
            txtEmail.Enabled = false;
        }
    }
}
