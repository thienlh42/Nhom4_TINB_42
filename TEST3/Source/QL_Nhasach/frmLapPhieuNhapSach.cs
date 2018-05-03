using BUS;
using DAO;
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
    public partial class frmLapPhieuNhapSach : Form
    {
        private static int layNgay;
        private static int layThang;
        private static int layNam;
        public static int layMaPN;
        public frmLapPhieuNhapSach()
        {
            InitializeComponent();
        }
        public static string maSach;
        public static string theloai;
        public static string tensach;
        
        public void HienThiDanhSachPhieuNhap()
        {
            dgvPhieuNhap.DataSource = PhieuNhapSach_BUS.SelectPhieuNhapSachAll();
        }
        public void HienThiCTPhieuNhap()
        {
            dgvChiTietPhieuNhap.DataSource = PhieuNhapSach_BUS.SelectCTPhieuNhapSachByMa(layMaPN);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvPhieuNhap_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            txtMaPhieuNhap.Text = dgvPhieuNhap.Rows[dong].Cells[0].Value.ToString();
            txtMaPhieuNhapCT.Text = dgvPhieuNhap.Rows[dong].Cells[0].Value.ToString();
            DateTime day = dtpNgayNhap.Value;
            layNgay = day.Day;
            layThang = day.Month;
            layNam = day.Year;

            layMaPN = int.Parse(dgvPhieuNhap.Rows[dong].Cells[0].Value.ToString());
            HienThiCTPhieuNhap();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Size = new Size(1132, 632);
            PhieuNhapSach_DTO p = new PhieuNhapSach_DTO();
            p.TongTien = 0;

            dtpNgayNhap.Format = DateTimePickerFormat.Custom;
            dtpNgayNhap.CustomFormat = "MM-dd-yy";
            p.NgayNhap = dtpNgayNhap.Text;
            dtpNgayNhap.Format = DateTimePickerFormat.Short;

            string ketQua = PhieuNhapSach_BUS.ThemPhieuNhap(p);
            if (ketQua == "Success")
            {
                MessageBox.Show("Tạo phiếu thành công");
            }
            else
            {
                MessageBox.Show(ketQua);
            }
            HienThiDanhSachPhieuNhap();
        }

        private void dgvChiTietPhieuNhap_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            txtMaPhieuNhap.Text = dgvChiTietPhieuNhap.Rows[dong].Cells[0].Value.ToString();
            txtMaSach.Text = dgvChiTietPhieuNhap.Rows[dong].Cells[1].Value.ToString();
            txtTenSach.Text = dgvChiTietPhieuNhap.Rows[dong].Cells[2].Value.ToString();
            txtTheLoai.Text = dgvChiTietPhieuNhap.Rows[dong].Cells[3].Value.ToString();
            txtSoLuong.Text = dgvChiTietPhieuNhap.Rows[dong].Cells[4].Value.ToString();
            txtDonGiaNhap.Text = dgvChiTietPhieuNhap.Rows[dong].Cells[5].Value.ToString();
        }

        
        private void button7_Click(object sender, EventArgs e)
        {
            frmDanhSachSach f = new frmDanhSachSach();
            f.ShowDialog();
            txtMaSach.Text = maSach;
            txtTenSach.Text = tensach;
            txtTheLoai.Text = theloai;
        }

        //private void frmLapPhieuNhapSach_Load(object sender, EventArgs e)
        //{
        //    dgvChiTietPhieuNhap.DataSource = Sach_BUS.SelectThongTinSachFull();
        //}

        private void btnThem_Click(object sender, EventArgs e)
        {
            //this.Size = new Size(1132, 632);
            PhieuNhapSach_DTO p = new PhieuNhapSach_DTO();
            p.TongTien = 0;

            dtpNgayNhap.Format = DateTimePickerFormat.Custom;
            dtpNgayNhap.CustomFormat = "MM-dd-yy";
            p.NgayNhap = dtpNgayNhap.Text;
            dtpNgayNhap.Format = DateTimePickerFormat.Short;

            string ketQua = PhieuNhapSach_BUS.ThemPhieuNhap(p);
            if (ketQua == "Success")
            {
                MessageBox.Show("Tạo phiếu thành công");
            }
            else
            {
                MessageBox.Show(ketQua);
            }
            HienThiDanhSachPhieuNhap();
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            this.Size = new Size(1189, 476);
            //dgvPhieuNhap.Visible = false;
            //label10.Text = "Nhập thông tin của sách cần nhập \n vào bảng chi tiết phiếu \n -->";
        }

        private void btnBoSung_Click(object sender, EventArgs e)
        {
            DataTable dt = ThamSo_BUS.GetThamSoAll();
            if (dt.Rows.Count != 0)
            {
                int nhapMin = int.Parse(dt.Rows[0].ItemArray[1].ToString());
                int luongTonMax = int.Parse(dt.Rows[0].ItemArray[3].ToString());
                //int tiLeDonGiaBan = int.Parse(dt.Rows[0].ItemArray[1].ToString());
                CT_PhieuNhapSach_DTO c = new CT_PhieuNhapSach_DTO();
                try
                {
                    c.MaPNS = int.Parse(txtMaPhieuNhap.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Mã phiếu nhập không được để trống");
                    return;
                }
                try
                {
                    c.MaSach = int.Parse(txtMaSach.Text);
                    c.TenSach = txtTenSach.Text;
                    c.TenTheLoai = txtTheLoai.Text;
                }
                catch (FormatException)
                {
                    MessageBox.Show("Mã sách không được để trống");
                    return;
                }
                try
                {
                    if (int.Parse(txtSoLuong.Text) < nhapMin)
                    {
                        MessageBox.Show(string.Format("Số lượng phải lớn hơn số lượng quy định ({0} quyển)", nhapMin));
                        return;
                    }
                    else
                    {
                        c.SoLuongNhap = int.Parse(txtSoLuong.Text);
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Số lượng phải là kiểu số");
                    return;
                }
                try
                {
                    if (txtDonGiaNhap.Text != "")
                    {
                        c.DonGiaNhap = UInt64.Parse(txtDonGiaNhap.Text);
                    }
                    else
                    {
                        MessageBox.Show("Đơn giá nhập không được để trống");
                        return;
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Đơn giá nhập phải là kiểu số");
                    return;
                }
                DataTable dt2 = ThamSo_BUS.SelectSoLuongTon(c.MaSach);
                int luongTon = int.Parse(dt2.Rows[0].ItemArray[4].ToString());
                int soLuongTonNew = luongTon + int.Parse(txtSoLuong.Text);
                c.ThanhTien = (UInt64)c.SoLuongNhap * c.DonGiaNhap;
                if (luongTon < luongTonMax)
                {
                    string ketQua = PhieuNhapSach_BUS.ThemChiTietPhieuNhap(c);
                    if (ketQua != "Success")
                    {
                        MessageBox.Show(ketQua);
                    }
                    else
                    {
                        Sach_DTO s = new Sach_DTO();
                        s.MaSach = int.Parse(txtMaSach.Text);
                        s.SoLuongTon = soLuongTonNew;
                        Sach_BUS.UpdateSoLuongTon(s);

                        PhieuNhapSach_DTO p = new PhieuNhapSach_DTO();
                        p.MaPNS = int.Parse(txtMaPhieuNhapCT.Text);
                        PhieuNhapSach_BUS.CapNhatTongTien(p);
                        HienThiCTPhieuNhap();
                        MessageBox.Show("Thành công");
                        //dgvPhieuNhap = new DataGridView();
                        lblTongtien.Text = "" + p.TongTien;
                        //HienThiDanhSachPhieuNhap();
                    }
                }
                else
                {
                    MessageBox.Show("Chỉ nhập các đầu sách có lượng tồn ít hơn theo quy định");
                }
            }
            else
            {
                MessageBox.Show("Không lấy được các tham số");
                return;
            }
        }

        private void frmLapPhieuNhapSach_Load_1(object sender, EventArgs e)
        {
            this.Size = new Size(419, 475);
            HienThiDanhSachPhieuNhap();
            dtpNgayNhap.Value = DateTime.Now;
        }

        private void txtDonGiaNhap_TextChanged(object sender, EventArgs e)
        {
            try
            {
                UInt64 tem = UInt64.Parse(txtDonGiaNhap.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Đơn giá nhập phải là số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDonGiaNhap.Text = "0";
                txtDonGiaNhap.Focus();
            }
            catch (OverflowException)
            {
                MessageBox.Show("Đơn giá nhập không được âm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDonGiaNhap.Text = "0";
                txtDonGiaNhap.Focus();
            }
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            try
            {
                uint tem = uint.Parse(txtSoLuong.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Số lượng phải là số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoLuong.Text = "0";
                txtSoLuong.Focus();
            }
            catch (OverflowException)
            {
                MessageBox.Show("Số lượng không được âm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoLuong.Text = "0";
                txtSoLuong.Focus();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private PhieuNhapSach_DTO Obj_Qlns = new PhieuNhapSach_DTO();
        private CT_PhieuNhapSach_DTO Obj_Qlpns = new CT_PhieuNhapSach_DTO();
        void Load_Obj()
        {
            Obj_Qlns.MaPNS = int.Parse (txtMaPhieuNhap.Text);
            Obj_Qlpns.MaPNS = int.Parse(txtMaPhieuNhap.Text);
            //Obj_Qltk.MatKhau = txtMatkhau.Text;
            //Obj_Qltk.MaQuyen = int.Parse(cboQuyen.SelectedValue.ToString());
        }

        private CT_PhieuNhapSach_DTO Obj_Qlpns_Masach = new CT_PhieuNhapSach_DTO();
        void Load_Obj_MaSach()
        {
            Obj_Qlpns_Masach.MaSach = int.Parse(txtMaSach.Text);
        }
        private void btnXoaSach_Click(object sender, EventArgs e)
        {
            Load_Obj();
            if (PhieuNhapSach_BUS.XoaPhieuNhap(Obj_Qlns) == "Success" && PhieuNhapSach_BUS.XoaSachtrongPhieu(Obj_Qlpns) == "Success")
            {
                MessageBox.Show("Xóa thành công");
            }
            HienThiDanhSachPhieuNhap();
            //dtpNgayNhap.Value = DateTime.Now;
            
        }

        private void btnXoaSach_Click_1(object sender, EventArgs e)
        {
            CT_PhieuNhapSach_DTO c = new CT_PhieuNhapSach_DTO();
            c.MaSach = int.Parse(txtMaSach.Text);
            c.MaPNS = int.Parse(txtMaPhieuNhapCT.Text);
            c.SoLuongNhap = int.Parse(txtSoLuong.Text);
            c.DonGiaNhap = UInt64.Parse(txtDonGiaNhap.Text);
            DataTable dt2 = ThamSo_BUS.SelectSoLuongTon(c.MaSach);
            int luongTon = int.Parse(dt2.Rows[0].ItemArray[4].ToString());
            int soLuongTonNew = luongTon - int.Parse(txtSoLuong.Text);
            Sach_DTO s = new Sach_DTO();
            s.MaSach = int.Parse(txtMaSach.Text);
            s.SoLuongTon = soLuongTonNew;
            Sach_BUS.UpdateSoLuongTon(s);

            PhieuNhapSach_DTO p = new PhieuNhapSach_DTO();
            p.MaPNS = int.Parse(txtMaPhieuNhapCT.Text);
            PhieuNhapSach_BUS.LayTien(p);
            p.TongTien = p.TongTien - ((UInt64)c.SoLuongNhap * c.DonGiaNhap);
            PhieuNhapSach_BUS.updateTien(p);
            HienThiCTPhieuNhap();
            //dgvPhieuNhap = new DataGridView();
            lblTongtien.Text = "" + p.TongTien;

            if (PhieuNhapSach_BUS.XoaMotSachtrongPhieu(c) == "Success" )
            {
                MessageBox.Show("Xóa thành công");
            }
            HienThiCTPhieuNhap();
            //HienThiDanhSachPhieuNhap();

        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có mã sách trong bảng CT_HOADON không
            try
            {
                if (PhieuNhapSach_DAO.KiemtramaSachCoTonTai(int.Parse(txtMaSach.Text), int.Parse(txtMaPhieuNhapCT.Text)))
                {
                    MessageBox.Show("Không có mã sách mà bạn cần sửa");
                    return;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Mã sách không được để trống");
                return;
            }

            DataTable dt = ThamSo_BUS.GetThamSoAll();
            int nhapMin = int.Parse(dt.Rows[0].ItemArray[1].ToString());
            CT_PhieuNhapSach_DTO ct = new CT_PhieuNhapSach_DTO();

            try
            {
                if (int.Parse(txtSoLuong.Text) < nhapMin)
                {
                    MessageBox.Show(string.Format("Số lượng phải lớn hơn số lượng quy định ({0} quyển)", nhapMin));
                    return;
                }
                else
                {
                    ct.SoLuongNhap = int.Parse(txtSoLuong.Text);
                }
            }
            catch (OverflowException)
            {
                MessageBox.Show("Số lượng nhập không được là số âm");
                return;
            }

            ct.MaPNS = int.Parse(txtMaPhieuNhapCT.Text);
            ct.MaSach = int.Parse(txtMaSach.Text);
            ct.TenSach = txtTenSach.Text;
            ct.TenTheLoai = txtTheLoai.Text;
            try
            {
                ct.DonGiaNhap = UInt64.Parse(txtDonGiaNhap.Text);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Số lượng nhập không được là số âm");
                return;
            }
            //ct.SoLuongNhap = int.Parse(txtSoLuong.Text);
            ct.ThanhTien = (UInt64)ct.SoLuongNhap * ct.DonGiaNhap;

            DataTable dt2 = ThamSo_BUS.SelectSoLuongTon(ct.MaSach);
            int luongTonhientai = int.Parse(dt2.Rows[0].ItemArray[4].ToString());
            int soluongbandau = PhieuNhapSach_BUS.SoLuongNhap(int.Parse(txtMaSach.Text));
            int soluongthaydoi = int.Parse(txtSoLuong.Text);
            int soLuongTonNew;
            if (soluongbandau > soluongthaydoi)
            {
                soLuongTonNew = luongTonhientai - (soluongbandau - soluongthaydoi);
            }
            else
            {
                soLuongTonNew = luongTonhientai + (soluongthaydoi - soluongbandau);
            }
            //int soLuongTonNew = luongTon - int.Parse(txtSoLuong.Text);
            Sach_DTO s = new Sach_DTO();
            s.MaSach = int.Parse(txtMaSach.Text);
            s.SoLuongTon = soLuongTonNew;
            Sach_BUS.UpdateSoLuongTon(s);

            string ketQua = PhieuNhapSach_BUS.SuaNhapSach(ct);
            if (ketQua != "Success")
            {
                MessageBox.Show(ketQua, "Lỗi");
                return;
            }
            MessageBox.Show("Cập nhật thành công");

            // Cập nhật lại tổng tiền bên PhieuNhapSach
            PhieuNhapSach_DTO p = new PhieuNhapSach_DTO();
            p.MaPNS = int.Parse(txtMaPhieuNhapCT.Text);
            PhieuNhapSach_BUS.CapNhatTongTien(p);
            lblTongtien.Text = "" + p.TongTien;
            HienThiCTPhieuNhap();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HienThiDanhSachPhieuNhap();
            this.Size = new Size(419, 475);
        }

    }
}
