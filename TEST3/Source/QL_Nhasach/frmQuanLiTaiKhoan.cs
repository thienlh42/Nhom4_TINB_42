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
    public partial class frmQuanLiTaiKhoan : Form
    {
        public frmQuanLiTaiKhoan()
        {
            InitializeComponent();
        }

        private QuanLyTaiKhoan_DTO Obj_Qltk = new QuanLyTaiKhoan_DTO();
        private string quyencu;
        void Load_Obj()
        {
            Obj_Qltk.TaiKhoan = txtTaikhoan.Text;
            Obj_Qltk.MatKhau = txtMatkhau.Text;
            Obj_Qltk.MaQuyen = int.Parse(cmbQuyen.SelectedValue.ToString());
        }

        //Hàm ẩn hiện các txt, cmb
        void Enlable(bool a)
        {
            txtTaikhoan.ReadOnly = !a;
            txtMatkhau.ReadOnly = !a;

            cmbQuyen.Enabled = a;
            btnLuu.Enabled = a;
            btnKhongluu.Enabled = a;
        }

        //Hàm hiển thị
        public void hienthi()
        {
            Enlable(false);

            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;

            cmbQuyen.ValueMember = "MaQuyen";
            cmbQuyen.DisplayMember = "TenQuyen";
            cmbQuyen.DataSource = QuanLyTaiKhoan_BUS.getQuyen();

            colQuyen.ValueMember = "MaQuyen";
            colQuyen.DisplayMember = "TenQuyen";
            colQuyen.DataSource = QuanLyTaiKhoan_BUS.getQuyen();
            dgvTaiKhoan.DataSource = QuanLyTaiKhoan_BUS.GetNguoiDungAll();
        }
        private void frmQuanLiTaiKhoan_Load(object sender, EventArgs e)
        {
            hienthi();
        }

        private void dgvTaiKhoan_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            txtTaikhoan.Text = dgvTaiKhoan.Rows[dong].Cells[0].Value.ToString();
            txtMatkhau.Text = dgvTaiKhoan.Rows[dong].Cells[1].Value.ToString();
            cmbQuyen.SelectedValue = dgvTaiKhoan.Rows[dong].Cells[2].Value.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Enlable(true);
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            txtTaikhoan.Text = "";
            txtMatkhau.Text = "";
            txtTaikhoan.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Enlable(true);
            btnThem.Enabled = false;
            btnXoa.Enabled = false;
            txtTaikhoan.ReadOnly = true;    
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtTaikhoan.Text == frmDangNhap.taiKhoan)
            {
                MessageBox.Show("Bạn không thể xóa chính mình được!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Bạn thực sự muốn xóa tài khoản này?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                Load_Obj();
                if (QuanLyTaiKhoan_BUS.XoaTaikhoan(Obj_Qltk) == "Success")
                {
                    hienthi();
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (btnSua.Enabled == false)
            {
                if (txtTaikhoan.Text == "" || txtMatkhau.Text == "")
                {
                    MessageBox.Show("Không được bỏ trông tài khoản và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (MessageBox.Show("Bạn thực sự muốn thêm tài khoản này?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {

                        //load đối tượng
                        Load_Obj();

                        if (QuanLyTaiKhoan_BUS.ThemTaikhoan(Obj_Qltk) == "Success")
                        {
                            hienthi();
                        }

                    }
                }
            }
            if (btnThem.Enabled == false)
            {
                if (txtMatkhau.Text == "")
                    MessageBox.Show("Không được bỏ trống mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    if (quyencu == "Quản lý nhà sách" && quyencu != cmbQuyen.Text && txtTaikhoan.Text == frmDangNhap.taiKhoan)
                    {
                        MessageBox.Show("Bạn không thể sửa quyền của chính mình vì bạn là admin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        hienthi();
                    }
                    else
                    {
                        if (MessageBox.Show("Bạn thực sự muốn sửa tài khoản này?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            //load đối tượng
                            Load_Obj();
                            string ketQua = QuanLyTaiKhoan_BUS.SuaTaikhoan(Obj_Qltk);
                            if ( ketQua != "Success")
                            {
                                MessageBox.Show(ketQua,"Lỗi");
                            }
                            hienthi();
                        }
                    }
            }
        }

        private void btnKhongluu_Click(object sender, EventArgs e)
        {
            hienthi();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmQuanLiTaiKhoan_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

    }
}
