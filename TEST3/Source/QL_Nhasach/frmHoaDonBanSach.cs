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
    public partial class frmHoaDonBanSach : Form
    {
        public static string maKH;
        public static string tenKH;
        public string ten;
        public string ngayHoaDon;
        public static string maSach;
        public static string tenSach;
        public static string donGiaBan;
        public static int layMaHD;
        public int rmaHD;
        public frmHoaDonBanSach()
        {
            InitializeComponent();
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
        public void HienThiDanhSachHoaDon()
        {
            //colMaKhachHang.ValueMember = "MaKhachHang";
            //colMaKhachHang.DisplayMember = "TenKhachHang";
            //colMaKhachHang.DataSource = KhachHang_BUS.GetKhachHangAll();
            dgvHoaDon.DataSource = HoaDon_BUS.SelectallHoaDon();
        }

        public void HienThiDanhSachChiTietHoaDon()
        {
            colMaSach.ValueMember = "MaSach";
            colMaSach.DisplayMember = "TenSach";
            colMaSach.DataSource = Sach_BUS.SelectThongTinSachFull();
            dgvChiTietHoaDon.DataSource = HoaDon_BUS.SelectHoaDonCTByMa(layMaHD);
        }        
        private void btnThem_Click(object sender, EventArgs e)
        {
            HoaDon_DTO hd = new HoaDon_DTO();
            try
            {
                hd.MaKhachHang = int.Parse(txtMaKhachHang.Text);
                hd.TenKhachHang = tenKH;
            }
            catch (FormatException)
            {
                MessageBox.Show("Phải chọn mã khách hàng từ danh sách khách hàng");
                btnMaKhachHang_Click(sender, e);
                return;
            }
            hd.TongTien = 0;
            hd.ThanhToan = 0;
            hd.ConLai = 0;
            //Vì trong sql ngày lưu dạng MM-dd-yy nên ta cần chuyển sang định dạng này mới lưu ngày chính sách được
            dtpNgayLapHD.Format = DateTimePickerFormat.Custom;
            dtpNgayLapHD.CustomFormat = "MM-dd-yy";
            hd.NgayLap = dtpNgayLapHD.Text;
            dtpNgayLapHD.Format = DateTimePickerFormat.Short;

            DataTable dt = ThamSo_BUS.GetThamSoAll();
            UInt64 noToiDa = UInt64.Parse(dt.Rows[0].ItemArray[4].ToString());//Số tiền nợ tối đa
            
            DataTable dt2 = ThamSo_BUS.SelectTienNoKH(int.Parse(txtMaKhachHang.Text));
            UInt64 tienNo = UInt64.Parse(dt2.Rows[0].ItemArray[5].ToString());
            if (tienNo > noToiDa)
            {
                MessageBox.Show("Số tiền nợ đã vượt quá số tiền nợ tối đa");
                return;
            }
            else
            {
                if (HoaDon_BUS.ThemHoaDon(hd) == false)
                {
                    MessageBox.Show("Thêm thất bại");
                    return;
                }
                HienThiDanhSachHoaDon();
                txtMaHoaDonCT.Text = txtMaHoaDon.Text;
                txtMaKhachHangCT.Text = txtMaKhachHangCT.Text;
                //this.Size = new Size(1132, 632);
            }            
        }

        private void btnMaKhachHang_Click(object sender, EventArgs e)
        {
            
            frmDanhSachKhachHang f = new frmDanhSachKhachHang();
            f.ShowDialog();//Lấy maKH từ form DanhSachKhachHang
            txtMaKhachHang.Text = maKH;
        }

        private void frmHoaDonBanSach_Load(object sender, EventArgs e)
        {
            this.Size = new Size(725, 590);
            HienThiDanhSachHoaDon();
            HienThiDanhSachChiTietHoaDon();

            //gChucNangThanhToan.Enabled = false;
            //gThanhToan.Enabled = false;
        }

        private void btnMaSach_Click(object sender, EventArgs e)
        {
            frmDanhSachSach f = new frmDanhSachSach();
            f.ShowDialog();//Lấy maSach và donGiaBan từ form DanhSachSach
            txtMaSach.Text = maSach;
            txtTenSach.Text = tenSach;
            txtDonGia.Text = donGiaBan;
        }

        private void btnBoSung_Click(object sender, EventArgs e)
        {
            // kiểm tra xem khách hàng đã thanh toán sách này chưa, nếu chưa mới cho phép thêm sách
            if (int.Parse(txtDaThanhToan.Text) != 0)
            {
                MessageBox.Show("Hóa đơn này đã thanh toán một phần tiền! \nBạn không được thêm sách!");
                return;
            }

            DataTable dt = ThamSo_BUS.GetThamSoAll();
            int noToiDa = int.Parse(dt.Rows[0].ItemArray[4].ToString());
            int luongTonSauKhiBan = int.Parse(dt.Rows[0].ItemArray[2].ToString());
            DataTable dt2 = ThamSo_BUS.SelectTienNoKH(int.Parse(txtMaKhachHang.Text));
            int tienNo = int.Parse(dt2.Rows[0].ItemArray[5].ToString());

            CT_HoaDon_DTO ct = new CT_HoaDon_DTO();
            try
            {
                ct.MaHD = int.Parse(txtMaHoaDonCT.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Mã hóa đơn không được để trống");
                return;
            }
            try
            {
                ct.MaSach = int.Parse(txtMaSach.Text);
                
            }
            catch (FormatException)
            {
                MessageBox.Show("Mã sách phải được chọn từ danh sách");
                btnMaSach_Click(sender, e);
                return;
            }
            try
            {
                ct.SoLuong = uint.Parse(txtSoLuong.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Số lượng mua phải là số và không được để trống");
                return;
            }
            catch (OverflowException)
            {
                MessageBox.Show("Số lượng không được âm");
                return;
            }
            if (tienNo <= noToiDa)
            {
                DataTable dt3 = ThamSo_BUS.SelectSoLuongTon(ct.MaSach);
                uint luongTon = uint.Parse(dt3.Rows[0].ItemArray[4].ToString());
                uint luongMua = 0;
                try
                {
                    luongMua = uint.Parse(txtSoLuong.Text);
                }
                catch (FormatException)
                {
                    MessageBox.Show("Số lượng mua phải là số và không được để trống");
                    txtSoLuong.Text = "";
                    txtThanhTien.Text = "";
                    return;
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Số lượng không được âm");
                    return;
                }
                ct.DonGia = UInt64.Parse(txtDonGia.Text);
                ct.ThanhTien = (UInt64)luongMua * ct.DonGia;
                txtThanhTien.Text = ct.ThanhTien.ToString();
                if ((luongTon - luongMua) < luongTonSauKhiBan)
                {
                    MessageBox.Show("Số lượng tồn của sách này sau khi bán đã nhỏ hơn quy định");
                    return;
                }
                string ketQua = HoaDon_BUS.ThemHoaDonChiTiet(ct);
                if (ketQua != "Success")
                {
                    MessageBox.Show(ketQua);
                    txtMaSach.Text = "";
                    txtSoLuong.Text = "0";
                    txtDonGia.Text = "";
                    return;
                }
                else
                {
                    uint soluongtonnew = luongTon - luongMua;
                    Sach_DTO s = new Sach_DTO();
                    s.MaSach = int.Parse(txtMaSach.Text);
                    s.SoLuongTon = (int)soluongtonnew;
                    ketQua = Sach_BUS.UpdateSoLuongTon(s);
                    if (ketQua != "Success")
                    {
                        MessageBox.Show(ketQua);
                        return;
                    }

                    HoaDon_DTO hd = new HoaDon_DTO();
                    hd.MaHD = ct.MaHD;
                    hd.TongTien = UInt64.Parse(HoaDon_BUS.TongThanhTien(hd).Rows[0].ItemArray[0].ToString());
                    hd.ThanhToan = 0;
                    hd.ConLai = UInt64.Parse(HoaDon_BUS.TongThanhTien(hd).Rows[0].ItemArray[0].ToString());
                    HoaDon_BUS.UpdateTongTien(hd);
                    HoaDon_BUS.UpdateConLai(hd);

                    KhachHang_DTO kh = new KhachHang_DTO();
                    kh.MaKhachHang = int.Parse(txtMaKhachHang.Text);
                    kh.SoTienNo = UInt64.Parse(tienNo.ToString()) + ct.ThanhTien;
                    string ketQua2 = KhachHang_BUS.UpdateTienNo(kh);
                    if (ketQua2 != "Success")
                    {
                        MessageBox.Show(ketQua);
                        return;
                    }
                    MessageBox.Show("Thành công");
                    HienThiDanhSachChiTietHoaDon();
                }
            }
            else
            {
                MessageBox.Show("Tiền nợ quý khách đã quá quy định để mua sách");
            }
        }

        private void dgvHoaDon_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            txtMaHoaDon.Text = dgvHoaDon.Rows[dong].Cells[0].Value.ToString();
            txtMaHoaDonCT.Text = txtMaHoaDon.Text;
            txtMaKhachHang.Text = dgvHoaDon.Rows[dong].Cells[1].Value.ToString();
            tenKH = KhachHang_BUS.Laykhachhang(int.Parse(txtMaKhachHang.Text));
            //txtThu.Text = KhachHang_BUS.Laykhachhang(int.Parse(txtMaKhachHang.Text));
            txtMaKhachHangCT.Text = txtMaKhachHang.Text;
            dtpNgayLapHD.Text = dgvHoaDon.Rows[dong].Cells[2].Value.ToString();
            txtTongTien.Text = dgvHoaDon.Rows[dong].Cells[3].Value.ToString(); ;
            //txtThanhToan.Text = dgvHoaDon.Rows[dong].Cells[4].Value.ToString();
            txtConLai.Text = dgvHoaDon.Rows[dong].Cells[5].Value.ToString();
            txtDaThanhToan.Text = dgvHoaDon.Rows[dong].Cells[4].Value.ToString();
            layMaHD = int.Parse(dgvHoaDon.Rows[dong].Cells[0].Value.ToString());
            
            
            HienThiDanhSachChiTietHoaDon();
        }

        private void dgvChiTietHoaDon_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int dong = e.RowIndex;
            txtMaSach.Text = dgvChiTietHoaDon.Rows[dong].Cells[1].Value.ToString();
            txtTenSach.Text = Sach_BUS.Laytensach(int.Parse(txtMaSach.Text));
            txtDonGia.Text = dgvChiTietHoaDon.Rows[dong].Cells[3].Value.ToString();
            txtSoLuong.Text = dgvChiTietHoaDon.Rows[dong].Cells[2].Value.ToString();
            txtThanhTien.Text = dgvChiTietHoaDon.Rows[dong].Cells[4].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rmaHD = int.Parse(txtMaHoaDon.Text);
            ten = KhachHang_BUS.Laykhachhang(int.Parse(txtMaKhachHang.Text));
            ngayHoaDon = dtpNgayLapHD.Text;
            //ReportHoaDonBanSach r = new ReportHoaDonBanSach(this);
            //r.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtTienTraKhach.Text = "";
            //if (txtConLai.Text == "0")
            //{
            //    try
            //    {
            //        HoaDon_DTO tam = new HoaDon_DTO();
            //        tam.MaHD = int.Parse(txtMaHoaDon.Text);
            //        txtThu.Text = tam.MaHD.ToString();
            //        UInt64 thanhToan = 0;
            //        try
            //        {
            //            thanhToan = UInt64.Parse(txtThanhToan.Text);
            //            tam.ThanhToan = thanhToan;
            //        }
            //        catch (FormatException)
            //        {
            //            MessageBox.Show("Số tiền thanh toán phải là số và không được để trống");
            //            return;
            //        }
            //        UInt64 tongTien = UInt64.Parse(txtTongTien.Text);
            //        if (thanhToan > tongTien)
            //        {
            //            MessageBox.Show("Thanh toán không được lớn hơn tổng tiền");
            //            return;
            //        }
            //        UInt64 conLai = tongTien - thanhToan;
            //        txtConLai.Text = conLai.ToString();


            //        DataTable dt = ThamSo_BUS.GetThamSoAll();
            //        UInt64 noToiDa = UInt64.Parse(dt.Rows[0].ItemArray[4].ToString());
            //        DataTable dt2 = ThamSo_BUS.SelectTienNoKH(int.Parse(txtMaKhachHangCT.Text));
            //        UInt64 tienNo = UInt64.Parse(dt2.Rows[0].ItemArray[5].ToString());

            //        //if ((conLai + tienNo) > noToiDa)
            //        //{
            //        //    string ketQua = HoaDon_BUS.XoaCTHoaDonByMa(int.Parse(txtMaHoaDon.Text));
            //        //    if (ketQua != "Success")
            //        //    {
            //        //        MessageBox.Show(ketQua);
            //        //        return;
            //        //    }
            //        //    ketQua = HoaDon_BUS.XoaHoaDonByMa(int.Parse(txtMaHoaDon.Text));
            //        //    if (ketQua != "Success")
            //        //    {
            //        //        MessageBox.Show(ketQua);
            //        //        return;
            //        //    }
            //        //    MessageBox.Show("Vì số tiền hóa đơn này cộng với số tiền nợ cũ lớn hơn quy định nợ cho phép nên hóa đơn này sẽ bị hủy. Mong quý khách vui lòng trả nợ!");
            //        //    HienThiDanhSachHoaDon();
            //        //    return;
            //        //}
            //        //else
            //        //{
            //            KhachHang_DTO kh = new KhachHang_DTO();
            //            kh.MaKhachHang = int.Parse(txtMaKhachHang.Text);
            //            kh.SoTienNo = conLai + tienNo;
            //            string ketQua = KhachHang_BUS.UpdateTienNo(kh);
            //            if (ketQua != "Success")
            //            {
            //                MessageBox.Show(ketQua);
            //                return;
            //            }
            //            MessageBox.Show("Thanh toán thành công");
            //            tam.ConLai = conLai;
            //            HoaDon_BUS.UpdateThanhToan(tam);
            //            HoaDon_BUS.UpdateConLai(tam);
            //        //}
            //    }
            //    catch (FormatException)
            //    {
            //        MessageBox.Show("Thanh toán phải là số và không được để trống");
            //        return;
            //    }
            //    catch (OverflowException)
            //    {
            //        MessageBox.Show("Thanh toán không được âm");
            //        return;
            //    }
            //    HienThiDanhSachHoaDon();
            //    txtThanhToan.Text = "";
            //}
            //else
            //{
                HoaDon_DTO tam = new HoaDon_DTO();
                // lấy mã khách hàng ngay và luôn
                tam.MaHD = int.Parse(txtMaHoaDon.Text);

                UInt64 DaThanhToan = UInt64.Parse(txtDaThanhToan.Text);
                UInt64 ConLai = UInt64.Parse(txtConLai.Text);
                UInt64 ThanhToan;
                try
                {
                    ThanhToan = UInt64.Parse(txtThanhToan.Text);
                    tam.ThanhToan = ThanhToan;
                }

                    //try
                //{
                //    ThanhToan = UInt64.Parse(txtThanhToan.Text);

                    //}
                catch (FormatException)
                {
                    MessageBox.Show("Số tiền thanh toán phải là số và không được để trống");
                    return;
                }
                UInt64 tongTien = UInt64.Parse(txtTongTien.Text);
                if (ThanhToan > ConLai)
                {
                    tam.ThanhToan = tongTien;
                    tam.ConLai = 0;
                    txtDaThanhToan.Text = tongTien.ToString();
                    txtConLai.Text = "0";
                    UInt64 Tientrakhach = ThanhToan - ConLai;
                    txtTienTraKhach.Text = Tientrakhach.ToString();
                    txtMaHDtra.Text = txtMaHoaDon.Text;
                    //MessageBox.Show("Thanh toán không được lớn hơn số tiền còn lại");
                    HoaDon_BUS.UpdateThanhToan(tam);
                    HoaDon_BUS.UpdateConLai(tam);

                    // update tien no cua khach hang
                    KhachHang_DTO kh2 = new KhachHang_DTO();
                    kh2.MaKhachHang = int.Parse(txtMaKhachHang.Text);

                    DataTable dt3 = ThamSo_BUS.SelectTienNoKH(int.Parse(txtMaKhachHangCT.Text));
                    UInt64 tienNo2 = UInt64.Parse(dt3.Rows[0].ItemArray[5].ToString());

                    kh2.SoTienNo = tienNo2 - ConLai;
                    //string ketQua = KhachHang_BUS.UpdateTienNo(kh);
                    //txtThu.Text = kh2.SoTienNo.ToString();
                    string ketQua2 = KhachHang_BUS.UpdateTienNo(kh2);
                    if (ketQua2 != "Success")
                    {
                        MessageBox.Show(ketQua2);
                        return;
                    }
                    HienThiDanhSachHoaDon();
                    txtThanhToan.Text = "";
                    return;
                }

                // thanh toán tiếp thì Tiền còn lại = Tiền còn lại - Tiền thánh toán thêm
                ConLai = ConLai - ThanhToan;
                DaThanhToan = DaThanhToan + ThanhToan;
                tam.ThanhToan = DaThanhToan;
                tam.ConLai = ConLai;
                txtDaThanhToan.Text = DaThanhToan.ToString();
                txtConLai.Text = ConLai.ToString();

                // update tien no cua khach hang
                KhachHang_DTO kh = new KhachHang_DTO();
                kh.MaKhachHang = int.Parse(txtMaKhachHang.Text);

                DataTable dt2 = ThamSo_BUS.SelectTienNoKH(int.Parse(txtMaKhachHangCT.Text));
                UInt64 tienNo = UInt64.Parse(dt2.Rows[0].ItemArray[5].ToString());
                
                kh.SoTienNo = tienNo - ThanhToan;
                //string ketQua = KhachHang_BUS.UpdateTienNo(kh);
                //txtThu.Text = kh.SoTienNo.ToString();
                string ketQua = KhachHang_BUS.UpdateTienNo(kh);
                if (ketQua != "Success")
                {
                    MessageBox.Show(ketQua);
                    return;
                }

                  //Cập nhật lại bảng
                HoaDon_BUS.UpdateThanhToan(tam);
                HoaDon_BUS.UpdateConLai(tam);
                HienThiDanhSachHoaDon();
                txtThanhToan.Text = "";
            //}
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Size = new Size(725, 590);
            HienThiDanhSachHoaDon();
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            this.Size = new Size(1297, 589);
           
        }

        private void btnXoaSach_Click(object sender, EventArgs e)
        {
            // Lấy ra tiền nợ đang có của khách hàng
            if (ThamSo_BUS.SelectTienNoKH(int.Parse(txtMaKhachHang.Text)) == null)
            {
                MessageBox.Show("Bạn không được xóa sách này, vì khách hàng này đã trả một phần tiền của sách này bên phiếu thu tiền hoặc đã thanh toán trực tiếp!");
                return;
            }
            DataTable dt2 = ThamSo_BUS.SelectTienNoKH(int.Parse(txtMaKhachHang.Text));
            UInt64 tienNoDangCo = UInt64.Parse(dt2.Rows[0].ItemArray[5].ToString());

            //Lấy ra ThanhTien cua MaSach được chọn để xóa
            UInt64 TiencuaSachDcXoa = HoaDon_BUS.ThanhTienSach(int.Parse(txtMaHoaDonCT.Text),int.Parse(txtMaSach.Text));
            KhachHang_DTO kh = new KhachHang_DTO();

            // Tính lại tiền nợ của khách khi xóa sách
            UInt64 tiennoSaukhiXoaSach = tienNoDangCo - TiencuaSachDcXoa;
            kh.MaKhachHang = int.Parse(txtMaKhachHang.Text);
            kh.SoTienNo = tiennoSaukhiXoaSach;
            string ketQua2 = KhachHang_BUS.UpdateTienNo(kh);
            if (ketQua2 != "Success")
            {
                MessageBox.Show("Bạn không được xóa sách này, vì khách hàng này đã trả một phần tiền của sách này bên phiếu thu tiền hoặc đã thanh toán trực tiếp!");
                return;
            }
            // Cập nhật lại tồn sau khi xóa sách
            DataTable dt3 = ThamSo_BUS.SelectSoLuongTon(int.Parse(txtMaSach.Text));
            uint luongTonHienTai = uint.Parse(dt3.Rows[0].ItemArray[4].ToString());

            uint luongsachdamua = uint.Parse(HoaDon_BUS.SoLuongKHMua(int.Parse(txtMaSach.Text), int.Parse(txtMaHoaDonCT.Text)).ToString());

            uint soluongtonnew = luongTonHienTai + luongsachdamua;
            Sach_DTO s = new Sach_DTO();
            s.MaSach = int.Parse(txtMaSach.Text);
            s.SoLuongTon = (int)soluongtonnew;
            ketQua2 = Sach_BUS.UpdateSoLuongTon(s);
            if (ketQua2 != "Success")
            {
                MessageBox.Show(ketQua2);
                return;
            }


            // xóa sách trong CT_HOADON
            string ketQua = HoaDon_BUS.XoaSachtrongCT(int.Parse(txtMaSach.Text), int.Parse(txtMaHoaDonCT.Text));
            if (ketQua != "Success")
            {
                MessageBox.Show(ketQua);
                return;
            }

            // Cap nhật lại Tổng tiền và Còn Lại
            HoaDon_DTO hd = new HoaDon_DTO();
            if (HoaDon_DAO.KiemtramaHD(int.Parse(txtMaHoaDon.Text)))
            {
                hd.MaHD = int.Parse(txtMaHoaDon.Text);
                hd.TongTien = 0;
                hd.ThanhToan = 0;
                hd.ConLai = 0;
            }
            else
            {
                hd.MaHD = int.Parse(txtMaHoaDon.Text);
                hd.TongTien = UInt64.Parse(HoaDon_BUS.TongThanhTien(hd).Rows[0].ItemArray[0].ToString());
                hd.ThanhToan = 0;
                hd.ConLai = UInt64.Parse(HoaDon_BUS.TongThanhTien(hd).Rows[0].ItemArray[0].ToString());
            }

            HoaDon_BUS.UpdateTongTien(hd);
            HoaDon_BUS.UpdateConLai(hd);

            //HienThiDanhSachHoaDon();
            HienThiDanhSachChiTietHoaDon();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            // kiểm tra xem khách hàng này đã thanh toán sách chưa, nếu chưa mới cho phép cập nhật
            if (int.Parse(txtDaThanhToan.Text) != 0)
            {
                MessageBox.Show("Hóa đơn này đã thanh toán một phần tiền! \nBạn không được cập nhật lại số lượng sách!");
                return;
            }

            // Kiểm tra xem có mã sách trong bảng CT_HOADON không
            if (HoaDon_DAO.KiemtramaSach(int.Parse(txtMaSach.Text), int.Parse(txtMaHoaDonCT.Text)))
            {
                MessageBox.Show("Không có mã sách mà bạn cần sửa");
                return;
            }
            // cập nhật tồn sách
            string ketQua;
            DataTable dt3 = ThamSo_BUS.SelectSoLuongTon(int.Parse(txtMaSach.Text));
            int luongTonHienTai = int.Parse(dt3.Rows[0].ItemArray[4].ToString());
            int soLuongthaydoi = int.Parse(txtSoLuong.Text);
            int soLuonglucdau = HoaDon_BUS.SoLuongKHMua(int.Parse(txtMaSach.Text), int.Parse(txtMaHoaDonCT.Text));
            int soluongtonnew;
            if (soLuongthaydoi > soLuonglucdau)
            {
                soluongtonnew = luongTonHienTai - (soLuongthaydoi - soLuonglucdau);
            }
            else
            {
                soluongtonnew = luongTonHienTai + (soLuonglucdau - soLuongthaydoi);
            }
            Sach_DTO s = new Sach_DTO();
            s.MaSach = int.Parse(txtMaSach.Text);
            s.SoLuongTon = (int)soluongtonnew;
            ketQua = Sach_BUS.UpdateSoLuongTon(s);
            if (ketQua != "Success")
            {
                MessageBox.Show(ketQua);
                return;
            }

            // cập nhật tiền nợ của khách
            // Lấy ra tiền nợ đang có của khách hàng
            DataTable dt2 = ThamSo_BUS.SelectTienNoKH(int.Parse(txtMaKhachHang.Text));
            UInt64 tienNoDangCo = UInt64.Parse(dt2.Rows[0].ItemArray[5].ToString());
            UInt64 TienmuaLucdau = UInt64.Parse(soLuonglucdau.ToString()) * UInt64.Parse(txtDonGia.Text) ;
            UInt64 TienmuaLucThaydoi = UInt64.Parse(soLuongthaydoi.ToString()) * UInt64.Parse(txtDonGia.Text);
            UInt64 tienNoSaukhiCapNhat;
            // Tính lại tiền nợ của khách khi thay dởi số lượng sách
            if (TienmuaLucdau < TienmuaLucThaydoi)
            {
                tienNoSaukhiCapNhat = tienNoDangCo + (TienmuaLucThaydoi - TienmuaLucdau);
            }
            else
            {
                tienNoSaukhiCapNhat = tienNoDangCo - (TienmuaLucdau - TienmuaLucThaydoi);
            }

            KhachHang_DTO kh = new KhachHang_DTO();
            kh.MaKhachHang = int.Parse(txtMaKhachHang.Text);
            kh.SoTienNo = tienNoSaukhiCapNhat;
            string ketQua2 = KhachHang_BUS.UpdateTienNo(kh);
            if (ketQua2 != "Success")
            {
                MessageBox.Show(ketQua2);
                return;
            }


            // Cập nhật số lượng sách
            CT_HoaDon_DTO ct = new CT_HoaDon_DTO();
            ct.MaSach = int.Parse(txtMaSach.Text);
            ct.MaHD = int.Parse(txtMaHoaDonCT.Text);
            ct.SoLuong = uint.Parse(txtSoLuong.Text);
            ct.ThanhTien = TienmuaLucThaydoi;
            ketQua = HoaDon_BUS.SuaSoLuongSachKHMua(ct);
            if (ketQua != "Success")
            {
                MessageBox.Show(ketQua);
                return;
            }

            // Cap nhật lại Tổng tiền và Còn Lại
            HoaDon_DTO hd = new HoaDon_DTO();
            hd.MaHD = int.Parse(txtMaHoaDon.Text);
            hd.TongTien = UInt64.Parse(HoaDon_BUS.TongThanhTien(hd).Rows[0].ItemArray[0].ToString());
            hd.ThanhToan = 0;
            hd.ConLai = UInt64.Parse(HoaDon_BUS.TongThanhTien(hd).Rows[0].ItemArray[0].ToString());
            HoaDon_BUS.UpdateTongTien(hd);
            HoaDon_BUS.UpdateConLai(hd);

            //HienThiDanhSachHoaDon();
            HienThiDanhSachChiTietHoaDon();
            
        }

    }
}
