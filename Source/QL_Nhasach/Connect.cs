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
//
namespace QL_Nhasach
{
    public partial class Connect : Form
    {
        public Connect()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataAccess.connect = txtconnect.Text;
            try
            {
                DataAccess.OpenConnection();
                DataAccess.CloseConnection();
            }
            catch
            {
                MessageBox.Show("Không thể kết nối");
                return;
            }
            this.Hide();
            frmManHinhChinh fr = new frmManHinhChinh();
            fr.ShowDialog();
        }
    }
}
