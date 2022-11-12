using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Body_Fat_Percentage
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            ActiveControl = txtYearOld;
            txtYearOld.MaxLength = 3;
            txtWeight.MaxLength = 6;
            txtHeight.MaxLength = 6;
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            var txtBox = sender as TextBox;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                if (txtBox.Name.Equals(nameof(txtYearOld)))
                {
                    if ((e.KeyChar == '.'))
                    {
                        e.Handled = true;
                    }
                }

                if ((e.KeyChar != '.'))
                {
                    e.Handled = true;
                }
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1) || (e.KeyChar == '-'))
            {
                e.Handled = true;
            }
        }

        private void btnCal_Click(object sender, EventArgs e)
        {
            double.TryParse(txtYearOld.Text, out double yearOld);
            double.TryParse(txtWeight.Text, out double weight);
            double.TryParse(txtHeight.Text, out double height);

            if (yearOld < 20)
            {
                MessageBox.Show("ระบุอายุตั้งแต่ 20 ปีขึ้นไป");
                txtYearOld.Focus();
                return;
            }

            if (weight < 10d)
            {
                MessageBox.Show("ระบุน้ำหนักอีกครั้ง");
                txtWeight.Focus();
                return;
            }

            if (height < 50d)
            {
                MessageBox.Show("ระบุส่วนสูงอีกครั้ง");
                txtHeight.Focus();
                return;
            }

            double BMI = weight / Math.Pow(height / 100, 2);

            lbBMIresult.Text = BMI.ToString("0.##");

            double BFP;

            if (rdoMale.Checked)
            {
                BFP = (1.2d * BMI) + (0.23 * yearOld) - 16.2d;
            }
            else
            {
                BFP = (1.2d * BMI) + (0.23 * yearOld) - 5.4d;
            }

            lbBFPresult.Text = BFP.ToString("0.##") + "%";

            double bodyFat = weight * BFP / 100d;

            lbBodyFat.Text = "≈" + bodyFat.ToString("0.##");
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var txtBox = sender as TextBox;

                if (txtBox.Name.Equals(nameof(txtYearOld)))
                {
                    txtWeight.Focus();
                }

                if (txtBox.Name.Equals(nameof(txtWeight)))
                {
                    txtHeight.Focus();
                }

                if (txtBox.Name.Equals(nameof(txtHeight)))
                {
                    btnCal.Focus();
                }
            }
        }
    }
}
