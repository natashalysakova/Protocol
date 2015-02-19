using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Protocols.Properties;

namespace Protocols
{
    public partial class Form1 : Form
    {
        private readonly Color ErrorColor = Color.MistyRose;
        private readonly Color ValidColor = Color.LightGreen;


        private readonly Font boldFont = new Font("Times New Roman", 12, FontStyle.Bold);
        private readonly Font normalFont = new Font("Times New Roman", 12);
        private readonly Random r = new Random();
        private double DieselSelectedValue;
        private double GasSelectedValue;
        private Diesel dieselValue;
        private Gas gasValue;

        List<Mechanic> mechanics = new List<Mechanic>();



        public Form1()
        {
            InitializeComponent();
            printGasDoc.PrintPage += printGasDoc_PrintPage;
            printDieselDoc.PrintPage += printDieselDoc_PrintPage;
            GasSelectedValue = 1.0;
            DieselSelectedValue = 65.9;

            prewiewPrintDialog.WindowState = FormWindowState.Maximized;
        }

        private void printGasDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            for (int i = 0; i < 561; i += 560)
            {
                e.Graphics.DrawString(
                    "Приложение к договору № " + gasValue.DogovorNumber + " от «" + gasValue.DogovorDate.Day + "» " +
                    GetMonthName(gasValue.DogovorDate.Month) + " " + gasValue.DogovorDate.Year + "г.", normalFont,
                    Brushes.Black, new PointF(200, i + 20));

                e.Graphics.DrawString(
                    "Протокол №" + Settings.Default.GasProtocolNumber + " от «" + gasValue.VypiskaDate.Day + "» " +
                    GetMonthName(gasValue.VypiskaDate.Month) + " " + gasValue.VypiskaDate.Year + "г.",
                    new Font("Times New Roman", 16, FontStyle.Bold), Brushes.Black, new PointF(220, i + 60));


                e.Graphics.DrawString("измерения токсичности отработанных газов",
                    new Font("Times New Roman", 16, FontStyle.Bold), Brushes.Black, new PointF(200, i + 90));

                e.Graphics.DrawString("бензинового автомобиля", new Font("Times New Roman", 16, FontStyle.Bold),
                    Brushes.Black, new PointF(250, i + 120));

                e.Graphics.DrawString("Название предприятия", normalFont, Brushes.Black, new PointF(50, i + 160));
                e.Graphics.DrawString(gasValue.CompanyName, boldFont, Brushes.Black, new PointF(350, i + 160));

                e.Graphics.DrawString("Марка и модель автомобиля", normalFont, Brushes.Black, new PointF(50, i + 180));
                e.Graphics.DrawString(gasValue.VendorModel, boldFont, Brushes.Black, new PointF(350, i + 180));

                e.Graphics.DrawString("Государственный номер", normalFont, Brushes.Black, new PointF(50, i + 200));
                e.Graphics.DrawString(gasValue.GosNumber, boldFont, Brushes.Black, new PointF(350, i + 200));

                e.Graphics.DrawString("Марка, модель. зав. номер дымометра, дата поверки ", normalFont, Brushes.Black,
                    new PointF(50, i + 230));
                e.Graphics.DrawString(Settings.Default.NormaGas, normalFont, Brushes.Black, new PointF(150, i + 250));

                e.Graphics.DrawString("Результаты  измерений", normalFont, Brushes.Black, new PointF(50, i + 270));


                e.Graphics.DrawLine(Pens.Black, new Point(50, i + 292), new Point(750, i + 292));


                e.Graphics.DrawString("Номер измерения", normalFont, Brushes.Black, new PointF(60, i + 294));

                e.Graphics.DrawString("Показатели измерения", normalFont, Brushes.Black, new PointF(240, i + 294));


                e.Graphics.DrawString("Пред. значения пок. СО, %", normalFont, Brushes.Black, new PointF(450, i + 292));


                e.Graphics.DrawLine(Pens.Black, new Point(50, i + 320), new Point(750, i + 320));


                for (int j = 0; j < gasValue.values.Length; j++)
                {
                    e.Graphics.DrawString((j + 1).ToString(), normalFont, Brushes.Black,
                        new PointF(70, i + (j * 20) + 320));

                    e.Graphics.DrawString(gasValue.values[j, 0].ToString(), normalFont, Brushes.Black,
                        new PointF(300, i + (j * 20) + 320));

                    e.Graphics.DrawString(gasValue.maxValue.ToString(), normalFont, Brushes.Black,
                        new PointF(550, i + (j * 20) + 320));
                }


                e.Graphics.DrawLine(Pens.Black, new Point(50, i + 443), new Point(750, i + 443));

                e.Graphics.DrawString("Среднее значение", normalFont, Brushes.Black, new PointF(60, i + 445));
                e.Graphics.DrawString(gasValue.avgValue.ToString(), normalFont, Brushes.Black, new PointF(300, i + 445));
                e.Graphics.DrawString(gasValue.maxValue.ToString(), normalFont, Brushes.Black, new PointF(550, i + 445));


                e.Graphics.DrawLine(Pens.Black, new Point(50, i + 470), new Point(750, i + 470));
                e.Graphics.DrawLine(Pens.Black, new Point(50, i + 294), new Point(50, i + 470));


                e.Graphics.DrawLine(Pens.Black, new Point(440, i + 294), new Point(440, i + 470));
                e.Graphics.DrawLine(Pens.Black, new Point(750, i + 294), new Point(750, i + 470));


                e.Graphics.DrawLine(Pens.Black, new Point(230, i + 294), new Point(230, i + 470));


                e.Graphics.DrawString("Результат соответствия требованиям ДСТУ - 4277 : 2004", normalFont, Brushes.Black,
                    new PointF(250, i + 470));

                e.Graphics.DrawString("Исполнитель:", normalFont, Brushes.Black, new PointF(50, i + 490));


                e.Graphics.DrawString(GetJob(), normalFont, Brushes.Black, new PointF(70, i + 510));
                e.Graphics.DrawString(GetName(), normalFont, Brushes.Black, new PointF(430, i + 510));

                e.Graphics.DrawLine(Pens.Black, new Point(60, i + 530), new Point(190, i + 530));
                e.Graphics.DrawLine(Pens.Black, new Point(210, i + 530), new Point(400, i + 530));
                e.Graphics.DrawLine(Pens.Black, new Point(420, i + 530), new Point(600, i + 530));


                e.Graphics.DrawString("должность", new Font("Times New Roman", 10), Brushes.Black,
                    new PointF(90, i + 530));
                e.Graphics.DrawString("подпись", new Font("Times New Roman", 10), Brushes.Black,
                    new PointF(280, i + 530));
                e.Graphics.DrawString("Ф.И.О.", new Font("Times New Roman", 10), Brushes.Black, new PointF(500, i + 530));
            }
        }

        private string GetName()
        {
            return mechanics.Where(x => x.Id == Properties.Settings.Default.LastUsedMechanicID).ToArray()[0].Name;
        }

        private string GetJob()
        {
            return mechanics.Where(x => x.Id == Properties.Settings.Default.LastUsedMechanicID).ToArray()[0].Job;
        }


        private void printDieselDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            for (int i = 0; i < 561; i += 560)
            {
                e.Graphics.DrawString(
                    "Приложение к договору № " + dieselValue.DogovorNumber + " от «" + dieselValue.DogovorDate.Day + "» " +
                    GetMonthName(dieselValue.DogovorDate.Month) + " " + dieselValue.DogovorDate.Year + "г.", normalFont,
                    Brushes.Black, new PointF(200, i + 20));

                e.Graphics.DrawString(
                    "Протокол №" + Settings.Default.DieselProtocolNumber + " от «" + dieselValue.VypiskaDate.Day + "» " +
                    GetMonthName(dieselValue.VypiskaDate.Month) + " " + dieselValue.VypiskaDate.Year + "г.",
                    new Font("Times New Roman", 16, FontStyle.Bold), Brushes.Black, new PointF(220, i + 40));


                e.Graphics.DrawString("измерения токсичности отработанных газов",
                    new Font("Times New Roman", 16, FontStyle.Bold), Brushes.Black, new PointF(200, i + 70));

                e.Graphics.DrawString("дизеля (газодизеля) автомобиля ", new Font("Times New Roman", 16, FontStyle.Bold),
                    Brushes.Black, new PointF(230, i + 100));

                e.Graphics.DrawString("Название предприятия", normalFont, Brushes.Black, new PointF(50, i + 140));
                e.Graphics.DrawString(dieselValue.CompanyName, boldFont, Brushes.Black, new PointF(350, i + 140));

                e.Graphics.DrawString("Марка и модель автомобиля", normalFont, Brushes.Black, new PointF(50, i + 160));
                e.Graphics.DrawString(dieselValue.VendorModel, boldFont, Brushes.Black, new PointF(350, i + 160));

                e.Graphics.DrawString("Государственный номер", normalFont, Brushes.Black, new PointF(50, i + 180));
                e.Graphics.DrawString(dieselValue.GosNumber, boldFont, Brushes.Black, new PointF(350, i + 180));

                e.Graphics.DrawString("Марка, модель. зав. номер дымометра, дата поверки ", normalFont, Brushes.Black,
                    new PointF(50, i + 210));
                e.Graphics.DrawString(Settings.Default.NormaDiesel, normalFont, Brushes.Black, new PointF(150, i + 230));

                e.Graphics.DrawString("Результаты  измерений", normalFont, Brushes.Black, new PointF(50, i + 250));


                #region HorizontalLines
                e.Graphics.DrawLine(Pens.Black, new Point(50, i + 272), new Point(750, i + 272));
                e.Graphics.DrawLine(Pens.Black, new Point(50, i + 290), new Point(750, i + 290));
                e.Graphics.DrawLine(Pens.Black, new Point(50, i + 443), new Point(750, i + 443));
                e.Graphics.DrawLine(Pens.Black, new Point(50, i + 470), new Point(750, i + 470));
                e.Graphics.DrawLine(Pens.Black, new Point(50, i + 320), new Point(750, i + 320));
                #endregion


                #region VerticalLines
                e.Graphics.DrawLine(Pens.Black, new Point(50, i + 272), new Point(50, i + 470));
                e.Graphics.DrawLine(Pens.Black, new Point(150, i + 272), new Point(150, i + 470));
                e.Graphics.DrawLine(Pens.Black, new Point(440, i + 272), new Point(440, i + 470));
                e.Graphics.DrawLine(Pens.Black, new Point(750, i + 272), new Point(750, i + 470));

                e.Graphics.DrawLine(Pens.Black, new Point(290, i + 292), new Point(290, i + 470));
                e.Graphics.DrawLine(Pens.Black, new Point(600, i + 292), new Point(600, i + 470));

                #endregion


                #region TabelHeader
                e.Graphics.DrawString("Номер", normalFont, Brushes.Black, new PointF(60, i + 274));
                e.Graphics.DrawString("Показатели измерения", normalFont, Brushes.Black, new PointF(200, i + 274));
                e.Graphics.DrawString("К, мֿ¹", normalFont, Brushes.Black, new PointF(200, i + 294));
                e.Graphics.DrawString("N, %", normalFont, Brushes.Black, new PointF(365, i + 294));
                e.Graphics.DrawString("Пред. значения", normalFont, Brushes.Black, new PointF(500, i + 274));
                e.Graphics.DrawString("Кдоп, мֿ¹", normalFont, Brushes.Black, new PointF(475, i + 294));
                e.Graphics.DrawString("Nдоп, %", normalFont, Brushes.Black, new PointF(625, i + 294));
                #endregion



                for (int j = 0; j < dieselValue.values.GetLength(0); j++)
                {
                    e.Graphics.DrawString((j + 1).ToString(), normalFont, Brushes.Black,
                        new PointF(70, i + (j * 20) + 320));

                    e.Graphics.DrawString(dieselValue.values[j, 0].ToString(), normalFont, Brushes.Black,
                        new PointF(200, i + (j * 20) + 320));

                    e.Graphics.DrawString(dieselValue.values[j, 1].ToString(), normalFont, Brushes.Black,
    new PointF(365, i + (j * 20) + 320));

                    e.Graphics.DrawString(dieselValue.maxValueMeters.ToString(), normalFont, Brushes.Black,
                       new PointF(495, i + (j * 20) + 320));

                    e.Graphics.DrawString(dieselValue.maxValue.ToString(), normalFont, Brushes.Black,
                        new PointF(645, i + (j * 20) + 320));
                }


                #region AVGValues
                e.Graphics.DrawString("Среднее", normalFont, Brushes.Black, new PointF(60, i + 445));
                e.Graphics.DrawString(dieselValue.avgValue.ToString(), normalFont, Brushes.Black, new PointF(365, i + 445));
                e.Graphics.DrawString(dieselValue.maxValue.ToString(), normalFont, Brushes.Black, new PointF(645, i + 445));

                e.Graphics.DrawString(dieselValue.avgValueMeters.ToString(), normalFont, Brushes.Black, new PointF(200, i + 445));
                e.Graphics.DrawString(dieselValue.maxValueMeters.ToString(), normalFont, Brushes.Black, new PointF(495, i + 445));

                #endregion

                e.Graphics.DrawString("Результат соответствия требованиям ДСТУ - 4277 : 2004", normalFont, Brushes.Black,
                    new PointF(250, i + 470));



                #region Ispolnitel
                e.Graphics.DrawString("Исполнитель:", normalFont, Brushes.Black, new PointF(50, i + 490));


                e.Graphics.DrawString(GetJob(), normalFont, Brushes.Black, new PointF(70, i + 510));
                e.Graphics.DrawString(GetName(), normalFont, Brushes.Black, new PointF(430, i + 510));

                e.Graphics.DrawLine(Pens.Black, new Point(60, i + 530), new Point(190, i + 530));
                e.Graphics.DrawLine(Pens.Black, new Point(210, i + 530), new Point(400, i + 530));
                e.Graphics.DrawLine(Pens.Black, new Point(420, i + 530), new Point(600, i + 530));


                e.Graphics.DrawString("должность", new Font("Times New Roman", 10), Brushes.Black,
                    new PointF(90, i + 530));
                e.Graphics.DrawString("подпись", new Font("Times New Roman", 10), Brushes.Black,
                    new PointF(280, i + 530));
                e.Graphics.DrawString("Ф.И.О.", new Font("Times New Roman", 10), Brushes.Black, new PointF(500, i + 530));
                #endregion
            }
        }

        private string GetMonthName(int p)
        {
            switch (p)
            {
                case 1:
                    return "Января";
                case 2:
                    return "Февраля";
                case 3:
                    return "Марта";
                case 4:
                    return "Апреля";
                case 5:
                    return "Мая";
                case 6:
                    return "Июня";
                case 7:
                    return "Июля";
                case 8:
                    return "Августа";
                case 9:
                    return "Сентября";
                case 10:
                    return "Октября";
                case 11:
                    return "Ноября";
                case 12:
                    return "Декабря";
                default:
                    return "NULL";
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripLabel1.Text = "Версия: " + Assembly.GetExecutingAssembly().GetName().Version;

            if (!File.Exists(Resources.MechanicNamesListFilename))
            {
                MessageBox.Show("Список механиков пуст. Заполните его, прежде чем начать работу с программой",
                    "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                AddMechanics f = new AddMechanics(mechanics);
                f.ShowDialog();
            }
            else
            {

                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream(Resources.MechanicNamesListFilename, FileMode.Open, FileAccess.Read);

                mechanics = (List<Mechanic>)bf.Deserialize(fs);
                fs.Dispose();
            }

            Settings.Default.GasProtocolNumber++;
            Settings.Default.DieselProtocolNumber++;
            Settings.Default.Save();

            Text = "Протокол №" + Settings.Default.GasProtocolNumber + " - " +
                   tabControl1.SelectedTab.Text.Substring(0, 6);

            dateTimePicker1.Value = dateTimePicker2.Value = GasDogDate.Value = DieselDogDate.Value = DateTime.Today;
            //this.reportViewer1.RefreshReport();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            Settings.Default.GasProtocolNumber++;
            Settings.Default.Save();

            Text = "Протокол №" + Settings.Default.GasProtocolNumber + " - " +
                   tabControl1.SelectedTab.Text.Substring(0, 6);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                Text = "Протокол №" + Settings.Default.GasProtocolNumber + " - " +
                       tabControl1.SelectedTab.Text.Substring(0, 6);
            }
            else
            {
                Text = "Протокол №" + Settings.Default.DieselProtocolNumber + " - " +
                       tabControl1.SelectedTab.Text.Substring(0, 6);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Settings.Default.DieselProtocolNumber++;
            Settings.Default.Save();
            Text = "Протокол №" + Settings.Default.DieselProtocolNumber + " - " +
                   tabControl1.SelectedTab.Text.Substring(0, 6);
        }


        private void button2_Click(object sender, EventArgs e)
        {
            gasValue = null;

            if (gasValue == null && MyGasValidate())
                gasValue = new Gas(GasCompName.Text, GasNewNumb.Text + GasOldNumb.Text, GasModel.Text,
                    Convert.ToInt32(GasDogNumb.Text), GasDogDate.Value, GasSelectedValue, dateTimePicker1.Value);

            if (gasValue != null)
            {
                printGasDoc.Print();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            gasValue = null;
            if (MyGasValidate())
                gasValue = new Gas(GasCompName.Text, GasNewNumb.Text + GasOldNumb.Text, GasModel.Text,
                    Convert.ToInt32(GasDogNumb.Text), GasDogDate.Value, GasSelectedValue, dateTimePicker1.Value);

            if (gasValue != null)
            {
                prewiewPrintDialog.Document = printGasDoc;
                try
                {
                    prewiewPrintDialog.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("В системе не найдено установленных принтеров", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dieselValue = null;

            if (MyDieselValidate())
                dieselValue = new Diesel(DieselCompName.Text, DieselNewNumb.Text + DieselOldNumb.Text, DieselModel.Text,
                    Convert.ToInt32(DieselDogNumb.Text), DieselDogDate.Value, DieselSelectedValue, dateTimePicker2.Value);

            if (dieselValue != null)
            {
                prewiewPrintDialog.Document = printDieselDoc;
                try
                {
                    prewiewPrintDialog.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("В системе не найдено установленных принтеров", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dieselValue = null;

            if (dieselValue == null && MyDieselValidate())
                dieselValue = new Diesel(DieselCompName.Text, DieselNewNumb.Text + DieselOldNumb.Text, DieselModel.Text,
                    Convert.ToInt32(DieselDogNumb.Text), DieselDogDate.Value, DieselSelectedValue, dateTimePicker2.Value);

            if (dieselValue != null)
            {
                printDieselDoc.Print();
            }
        }

        private bool MyGasValidate()
        {
            bool res = true;

            if (GasCompName.BackColor == ErrorColor)
            {
                res = false;
                errorProvider1.SetError(GasCompName, "Заполните поле");
            }

            if (GasDogNumb.BackColor == ErrorColor)
            {
                res = false;
                errorProvider1.SetError(GasDogNumb, "Заполните поле");
            }

            if (GasModel.BackColor == ErrorColor)
            {
                res = false;
                errorProvider1.SetError(GasModel, "Заполните поле");
            }

            if (GasOldNumb.BackColor == ErrorColor)
            {
                res = false;
                errorProvider1.SetError(GasOldNumb, "Заполните поле");
            }

            return res;
        }

        private bool MyDieselValidate()
        {
            bool res = true;

            if (DieselCompName.BackColor == ErrorColor)
            {
                res = false;
                errorProvider1.SetError(DieselCompName, "Заполните поле");
            }

            if (DieselDogNumb.BackColor == ErrorColor)
            {
                res = false;
                errorProvider1.SetError(DieselDogNumb, "Заполните поле");
            }

            if (DieselModel.BackColor == ErrorColor)
            {
                res = false;
                errorProvider1.SetError(DieselModel, "Заполните поле");
            }

            if (DieselOldNumb.BackColor == ErrorColor)
            {
                res = false;
                errorProvider1.SetError(DieselOldNumb, "Заполните поле");
            }


            return res;
        }

        private void GasCompName_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(((TextBox)sender).Text))
            {
                errorProvider1.SetError(((TextBox)sender), "Заполните поле");
                ((TextBox)sender).BackColor = ErrorColor;
            }
            else
            {
                errorProvider1.SetError(((TextBox)sender), null);
                ((TextBox)sender).BackColor = ValidColor;
            }
        }

        private void ProtocolNumb_textChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(((TextBox)sender).Text);
                ((TextBox)sender).BackColor = ValidColor;
                errorProvider1.SetError(((TextBox)sender), null);
            }
            catch
            {
                ((TextBox)sender).BackColor = ErrorColor;
                errorProvider1.SetError(((TextBox)sender), "Введите число");
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            GasSelectedValue = Convert.ToDouble(((RadioButton)sender).Text);
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            DieselSelectedValue = Convert.ToDouble(((RadioButton)sender).Text);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                int dogNumb = 0;
                try
                {
                    dogNumb = Convert.ToInt32(GasDogNumb.Text);
                }
                catch
                {
                }

                gasValue = new Gas(GasCompName.Text, GasNewNumb.Text + GasOldNumb.Text, GasModel.Text,
      dogNumb, GasDogDate.Value, GasSelectedValue, dateTimePicker1.Value);
                printDialog1.Document = printGasDoc;
                printDialog1.ShowDialog();



            }
            else
            {
                int dogNumb = 0;
                try
                {
                    dogNumb = Convert.ToInt32(GasDogNumb.Text);
                }
                catch
                {
                }
                dieselValue = new Diesel(DieselCompName.Text, DieselNewNumb.Text + DieselOldNumb.Text, DieselModel.Text,
                       dogNumb, DieselDogDate.Value, DieselSelectedValue, dateTimePicker2.Value);
                printDialog1.Document = printDieselDoc;
                printDialog1.ShowDialog();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm(mechanics);
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (tabControl1.SelectedIndex == 0)
                {
                    Text = "Протокол №" + Settings.Default.GasProtocolNumber + " - " +
                           tabControl1.SelectedTab.Text.Substring(0, 6);
                }
                else
                {
                    Text = "Протокол №" + Settings.Default.DieselProtocolNumber + " - " +
                           tabControl1.SelectedTab.Text.Substring(0, 6);
                }
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            GasNewNumb.Text = "АН";
            GasOldNumb.Text = "";
            GasModel.Text = "";
            radioButton2.Checked = true;

            Settings.Default.GasProtocolNumber++;
            Settings.Default.Save();

            Text = "Протокол №" + Settings.Default.GasProtocolNumber + " - " +
                   tabControl1.SelectedTab.Text.Substring(0, 6);

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            GasDogDate.Value = DateTime.Today;
            GasDogNumb.Text = "";
            GasCompName.Text = "";

            GasNewNumb.Text = "АН";
            GasOldNumb.Text = "";
            GasModel.Text = "";
            radioButton2.Checked = true;


            Settings.Default.GasProtocolNumber++;
            Settings.Default.Save();

            Text = "Протокол №" + Settings.Default.GasProtocolNumber + " - " +
                   tabControl1.SelectedTab.Text.Substring(0, 6);

        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            DieselNewNumb.Text = "АН";
            DieselOldNumb.Text = "";
            DieselModel.Text = "";
            radioButton10.Checked = true;

            Settings.Default.DieselProtocolNumber++;
            Settings.Default.Save();
            Text = "Протокол №" + Settings.Default.DieselProtocolNumber + " - " +
                   tabControl1.SelectedTab.Text.Substring(0, 6);

        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            DieselDogDate.Value = DateTime.Today;
            DieselDogNumb.Text = "";
            DieselCompName.Text = "";


            DieselNewNumb.Text = "АН";
            DieselOldNumb.Text = "";
            DieselModel.Text = "";
            radioButton10.Checked = true;

        }
    }
}