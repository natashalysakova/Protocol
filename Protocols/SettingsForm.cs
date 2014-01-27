using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Protocols
{
    public partial class SettingsForm : Form
    {
        private List<Mechanic> _list;

        public SettingsForm(List<Mechanic> list)
        {
            InitializeComponent();
            _list = list;
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.GasProtocolNumber.ToString();
            textBox3.Text = Properties.Settings.Default.DieselProtocolNumber.ToString();

            textBox5.Text = Properties.Settings.Default.NormaGas;
            textBox6.Text = Properties.Settings.Default.NormaDiesel;
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(textBox2.Text))
            {
                try
                {
                    Properties.Settings.Default.GasProtocolNumber = Convert.ToInt32(textBox2.Text);
                }
                catch (Exception)
                {
                    errorProvider1.SetError(textBox2, "Введите целое число");
                    return;
                }
            }


            if (!String.IsNullOrEmpty(textBox4.Text))
            {
                try
                {
                    Properties.Settings.Default.DieselProtocolNumber = Convert.ToInt32(textBox4.Text);
                }
                catch (Exception)
                {
                    errorProvider1.SetError(textBox4, "Введите целое число");
                    return;
                }
            }

            Properties.Settings.Default.NormaGas = textBox5.Text;
            Properties.Settings.Default.NormaDiesel = textBox6.Text;
            Properties.Settings.Default.Save();
            

            DialogResult = DialogResult.OK;
            this.Close();
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(((TextBox) sender).Text);
                errorProvider1.SetError((TextBox)sender, null);
            }
            catch (Exception)
            {
                errorProvider1.SetError((TextBox)sender, "Введите число");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new AddMechanics(_list).ShowDialog();
        }
    }
}
