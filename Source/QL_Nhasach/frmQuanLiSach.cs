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
    public partial class frmQuanLiSach : Form
    {

        public frmQuanLiSach()
        {
            InitializeComponent();
        }
        public void Enable(bool a)
        {
            txtTenSach.ReadOnly = !a;

            cmbTheLoai.Enabled = a;
            btnLuu.Enabled = a;
            btnKhongLuu.Enabled = a;
        }
        public void HienThiThongTinSach()
        {
            dgvSach.DataSource = Sach_BUS.SelectThongTinSachFull();
        }

        public void HienThiDanhSachSach()
        {
            colTheLoai.ValueMember = "MaTheLoai";
            colTheLoai.DisplayMember = "TenTheLoai";
            colTheLoai.DataSource = TheLoai_BUS.GetTheLoaiAll();

            cmbTheLoai.ValueMember = "MaTheLoai";
            cmbTheLoai.DisplayMember = "TenTheLoai";
            cmbTheLoai.DataSource = TheLoai_BUS.GetTheLoaiAll();

            cmbTheLoai.Text = "";
            HienThiThongTinSach();
        }

        private void frmQuanLiSach_Load(object sender, EventArgs e)
        {
            HienThiDanhSachSach();
        }

        private void dgvSach_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            txtMaSach.Text = dgvSach.Rows[dong].Cells[0].Value.ToString();
            txtTenSach.Text = dgvSach.Rows[dong].Cells[1].Value.ToString();
            cmbTheLoai.SelectedValue = (int)(dgvSach.Rows[dong].Cells[2].Value);
            TheLoai_DTO tl = TheLoai_BUS.GetTheLoaiByMa((int)(dgvSach.Rows[dong].Cells[2].Value));
            cmbTheLoai.Text = tl.TenTheLoai;
            txtTacGia.Text = dgvSach.Rows[dong].Cells[3].Value.ToString();
            txtDonBanSach.Text = dgvSach.Rows[dong].Cells[4].Value.ToString();
            txtSoLuongTon.Text = dgvSach.Rows[dong].Cells[5].Value.ToString();
        }
        
        public void them()
        {
            Sach_DTO ds = new Sach_DTO();
            ds.MaTheLoai = int.Parse(cmbTheLoai.SelectedValue.ToString());
            try
            {
                ds.TenSach = txtTenSach.Text;
            }
            catch
            {
                MessageBox.Show("Tên sách không được rỗng!");
                return;
            }
            try
            {
                ds.TacGia = txtTacGia.Text;
            }
            catch 
            {
                MessageBox.Show("Tên tác giả không được rỗng!");
                return;
            }
            ds.SoLuongTon = 0;
            if (txtDonBanSach.Text != "")
            {
                try
                {
                    ds.DonGiaBan = UInt64.Parse(txtDonBanSach.Text);
                    
                }
                catch (FormatException)
                {
                    MessageBox.Show("Đơn giá bán phải là số");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Đơn giá bán không được bỏ trống");
                return;
            }
            
            string ketQua = Sach_BUS.ThemSach(ds);
            if (ketQua != "Success")
            {
                MessageBox.Show(ketQua);
                return;
            } 
            MessageBox.Show("Thêm đầu sách thành công");
            HienThiDanhSachSach();
            
        }
        public void capnhat()
        {
            Sach_DTO ds = new Sach_DTO();
            ds.MaSach = int.Parse(txtMaSach.Text);
            ds.MaTheLoai = int.Parse(cmbTheLoai.SelectedValue.ToString());
            try
            {
                ds.TenSach = txtTenSach.Text;
            }
            catch
            {
                MessageBox.Show("Tên sách không được rỗng!");
                return;
            }
            try
            {
                ds.TacGia = txtTacGia.Text;
            }
            catch
            {
                MessageBox.Show("Tên tác giả không được rỗng!");
                return;
            }
            ds.SoLuongTon = 0;
            if (txtDonBanSach.Text != "")
            {
                try
                {
                    ds.DonGiaBan = UInt64.Parse(txtDonBanSach.Text);

                }
                catch (FormatException)
                {
                    MessageBox.Show("Đơn giá bán phải là số");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Đơn giá bán không được bỏ trống");
                return;
            }

            string ketQua = Sach_BUS.CapNhatSach(ds);
            if (ketQua != "Success")
            {
                MessageBox.Show(ketQua);
                return;
            }
            MessageBox.Show("Cập nhật sách thành công! ");
            HienThiDanhSachSach();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (btnCapNhat.Enabled == false)
            {
                them();
            }
            if (btnThem.Enabled == false)
            {
                capnhat();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Enable(true);
            btnCapNhat.Enabled = false;
            txtTenSach.Text = "";
            txtTacGia.Text = "";
            txtDonBanSach.Text = "";
            txtTenSach.Focus();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            Enable(true);
            btnThem.Enabled = false;
            txtTenSach.Text = "";
            txtTacGia.Text = "";
            txtDonBanSach.Text = "";
            txtTenSach.Focus();
        }

        private void btnKhongLuu_Click(object sender, EventArgs e)
        {
            btnCapNhat.Enabled = true;
            btnThem.Enabled = true;
            HienThiThongTinSach();
        }
    }
}
