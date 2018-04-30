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
    public partial class frmBaoCaoCongNo : Form
    {
        public frmBaoCaoCongNo()
        {
            InitializeComponent();
        }
        public int thang;
        public int nam;
        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            BaoCaoCongNo_DTO r = new BaoCaoCongNo_DTO();
            try
            {
                r.Thang = int.Parse(txtThang.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Tháng không được để trống và phải là số");
                return;
            }
            try
            {
                r.Nam = int.Parse(txtNam.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Năm không được để trống và phải là số");
                return;
            }

            DataTable dt = BaoCaoCongNo_BUS.GetBaoCaoCongNoByThangNam(r);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Tháng, năm này không có trong CSDL");
            }
            colMaKhachHang.ValueMember = "MaKhachHang";
            colMaKhachHang.DisplayMember = "TenKhachHang";
            colMaKhachHang.DataSource = KhachHang_BUS.GetKhachHangAll();
            dgvCongNo.DataSource = dt;
            btnXuat.Enabled = true;
        }

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
                        // suy nghĩ xem có nên cập nhật lại nợ đầu không, xem có hợp lí không
                    //else
                    //{
                    //    r.NoDau = BaoCaoCongNo_BUS.GetSoTienNo(r.MaKhachHang);
                    //    string kq = BaoCaoCongNo_BUS.CapNhatNoDauKhachHang(r);
                    //    if (kq != "Success")
                    //    {
                    //        MessageBox.Show(kq);
                    //        return;
                    //    }
                    //}
                }
            }
        }

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

        private void frmBaoCaoCongNo_Load(object sender, EventArgs e)
        {
            CapNhatNoDau();
            CapNhatPhatSinhNoCuoi();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            thang = int.Parse(txtThang.Text);
            nam = int.Parse(txtNam.Text);
            //ReportBaoCaoCongNo re = new ReportBaoCaoCongNo(this);
            //re.ShowDialog();
        }
    }
}
