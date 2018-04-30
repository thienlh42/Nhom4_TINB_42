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
    public partial class frmBaoCaoTon : Form
    {
        public int thang;
        public int nam;
        public frmBaoCaoTon()
        {
            InitializeComponent();
        }

        // nên để ở form chính nhưng chạy thử thì để đây
        //Cập nhật tồn đầu
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


        private void btnBaoCao_Click(object sender, EventArgs e)
        {
            BaoCaoTon_DTO r = new BaoCaoTon_DTO();
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
            DataTable dt = BaoCaoTon_BUS.GetBaoCaoTonByThangNam(r);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Tháng, năm này không có trong CSDL");
            }
            colMaSach.ValueMember = "MaSach";
            colMaSach.DisplayMember = "TenSach";
            colMaSach.DataSource = Sach_BUS.SelectTenSachAll();

            dgvTon.DataSource = dt;
            btnReport.Enabled = true;
        }

        private void frmBaoCaoTon_Load(object sender, EventArgs e)
        {
            CapNhatTonDau();
            CapNhatPhatSinhTonCuoi();
            btnReport.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            thang = int.Parse(txtThang.Text);
            nam = int.Parse(txtNam.Text);
           // ReportBaoCaoTon re = new ReportBaoCaoTon(this);
            //re.ShowDialog();
        }
        
    }
}
