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
    public partial class frmQuanLiTheLoai : Form
    {
        public frmQuanLiTheLoai()
        {
            InitializeComponent();
        }

        public void Enable(bool a)
        {
            txtTenTheLoai.ReadOnly = !a;

            btnLuu.Enabled = a;
            btnKhongluu.Enabled = a;
        }

        public void HienThiDanhSachTheLoai()
        {
            Enable(false);
            btnThem.Enabled = true;
            btnSua.Enabled = true;

            dgvTheLoai.DataSource = TheLoai_BUS.GetTheLoaiAll();
        }

        private void frmQuanLiTheLoai_Load(object sender, EventArgs e)
        {
            HienThiDanhSachTheLoai();
        }
        // thêm vào danh sách thể loại
        void Them()
        {
            if (txtTenTheLoai.Text == "")
            {
                MessageBox.Show("Không được bỏ trống tên thể loại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTheLoai.Focus();
            }
            else
            {
                if (MessageBox.Show("Bạn thực sự muốn thêm thể loại này?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    TheLoai_DTO tl = new TheLoai_DTO();
                    tl.TenTheLoai = txtTenTheLoai.Text;
                    string ketQua = TheLoai_BUS.ThemTheLoai(tl);
                    if (ketQua != "Success")
                    {
                        MessageBox.Show(ketQua, "Lỗi");
                        return;
                    }
                    MessageBox.Show("Thêm thể loại thành công");
                    HienThiDanhSachTheLoai();
                }
            }
        }
        // cập nhật lại danh sách thể loại
        void CapNhat()
        {
            if (txtTenTheLoai.Text == "")
            {
                MessageBox.Show("Không được bỏ trống tên thể loại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenTheLoai.Focus();
            }
            else
            {
                if (MessageBox.Show("Bạn thực sự muốn cập nhật thể loại này?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    TheLoai_DTO tl = new TheLoai_DTO();
                    tl.TenTheLoai = txtTenTheLoai.Text;
                    tl.MaTheLoai = int.Parse(txtMaTheLoai.Text);
                    string ketQua = TheLoai_BUS.SuaTheLoai(tl);
                    if (ketQua != "Success")
                    {
                        MessageBox.Show(ketQua, "Lỗi");
                        return;
                    }
                    MessageBox.Show("Cập nhật thành công");
                    HienThiDanhSachTheLoai();
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Enable(true);
            btnSua.Enabled = false;
            txtTenTheLoai.Text = "";
            txtMaTheLoai.Text = "";
            txtTenTheLoai.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Enable(true);
            btnThem.Enabled = false;
            txtTenTheLoai.Text = "";
            txtTenTheLoai.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (btnSua.Enabled == false)
            {
                Them();
            }
            if (btnThem.Enabled == false)
            {
                CapNhat();
            }
        }

        private void btnKhongluu_Click(object sender, EventArgs e)
        {
            HienThiDanhSachTheLoai();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvTheLoai_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            txtMaTheLoai.Text = dgvTheLoai.Rows[dong].Cells[0].Value.ToString();
            txtTenTheLoai.Text = dgvTheLoai.Rows[dong].Cells[1].Value.ToString();
        }
    }
}
