using BUS;
using DTO;
using DAO;
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
    public partial class frmDanhSachSach : Form
    {
        public static string layMaSach;
        public static string layDonGiaBan;
        public static string layTenSach;
        public static string layTheLoai;
        public frmDanhSachSach()
        {
            InitializeComponent();
        }
        public void HienThiThongTinSach()
        {
            dgvSach.DataSource = Sach_BUS.SelectThongTinSachFull();
        }
        private void frmDanhSachSach_Load(object sender, EventArgs e)
        {
            colMaTheLoai.ValueMember = "MaTheLoai";
            colMaTheLoai.DisplayMember = "TenTheLoai";
            colMaTheLoai.DataSource = TheLoai_BUS.GetTheLoaiAll();

            cboTimTheLoai.ValueMember = "MaTheLoai";
            cboTimTheLoai.DisplayMember = "TenTheLoai";
            cboTimTheLoai.DataSource = TheLoai_BUS.GetTheLoaiAll();

            cboTimTheLoai.Text = "";
            HienThiThongTinSach();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HienThiThongTinSach();
        }

        public void HienThiDanhSachDauSachTheoMaTheLoai(string ma)
        {
            Sach_DTO s = new Sach_DTO();
            s.MaTheLoai = int.Parse(ma);
            DataTable dt = Sach_BUS.SelectSachLikeMaTheLoaiDanhSachSach(s);

            //////colTenSach.Value = "MaDauSach";
            //////colTenSach.DisplayMember = "TenDauSach";
            //////colTenSach.DataSource = DauSach_BUS.SelectDauSachAll();

            if (dt.Rows.Count == 0)
            {
                dgvSach.DataSource = Sach_BUS.SelectSachNull();
            }
            dgvSach.DataSource = dt;
        }

        private void cboTimTheLoai_SelectedIndexChanged(object sender, EventArgs e)
        {
            string i = cboTimTheLoai.SelectedValue.ToString();
            HienThiDanhSachDauSachTheoMaTheLoai(i);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmLapPhieuNhapSach.maSach = layMaSach;
            layTheLoai = TheLoai_BUS.LayTenTheLoai(int.Parse(layTheLoai));
            frmLapPhieuNhapSach.theloai = layTheLoai;
            frmLapPhieuNhapSach.tensach = layTenSach;
            frmHoaDonBanSach.maSach = layMaSach;
            frmHoaDonBanSach.tenSach = layTenSach;
            frmHoaDonBanSach.donGiaBan = layDonGiaBan;
            this.Close();
        }

        private void dgvSach_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            layMaSach = dgvSach.Rows[dong].Cells[0].Value.ToString();
            layTenSach = dgvSach.Rows[dong].Cells[1].Value.ToString();
            layTheLoai = dgvSach.Rows[dong].Cells[2].Value.ToString();
            layDonGiaBan = dgvSach.Rows[dong].Cells[4].Value.ToString();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}
