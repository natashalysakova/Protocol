using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace Protocols
{
    public partial class AddMechanics : Form
    {
        private List<Mechanic> _list;

        public AddMechanics(List<Mechanic> list)
        {
            InitializeComponent();
            _list = list;
        }

        private void AddMechanics_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_list.Count != 0)
            {
                FileStream fs = new FileStream(Properties.Resources.MechanicNamesListFilename, FileMode.OpenOrCreate,
                    FileAccess.Write);

                BinaryFormatter bf = new BinaryFormatter();

                bf.Serialize(fs, _list);

                fs.Dispose();
            }
            else
            {
                e.Cancel = true;
                MessageBox.Show("Нужно добавить хотя бы одного механика", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void AddMechanics_Load(object sender, EventArgs e)
        {
            UpdateList();
        }

        void UpdateList()
        {
            listView1.Items.Clear();

            for (int i = 0; i < _list.Count; i++)
            {
                Mechanic mechanic = _list[i];
                listView1.Items.Add(mechanic.Id.ToString());

                listView1.Items[i].SubItems.Add(mechanic.Name);
                listView1.Items[i].SubItems.Add(mechanic.Job);

                if (mechanic.Id == Properties.Settings.Default.LastUsedMechanicID)
                    listView1.Items[i].BackColor = Color.LightGreen;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (errorProvider1.GetError(textBox1) == "" && errorProvider1.GetError(textBox2) == "")
            {
                if (_list.Count != 0)
                {
                    _list.Add(new Mechanic(_list.Last().Id + 1, textBox1.Text, textBox2.Text));


                }
                else
                {
                    _list.Add(new Mechanic(0, textBox1.Text, textBox2.Text));
                    Properties.Settings.Default.LastUsedMechanicID = 0;

                }

                Properties.Settings.Default.Save();

                UpdateList();
            }
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text == "")
            {
                errorProvider1.SetError((sender as TextBox), "Пустое поле");
                (sender as TextBox).BackColor = Color.LightPink;
            }
            else
            {
                errorProvider1.SetError((sender as TextBox), null);
                (sender as TextBox).BackColor = Color.LightGreen;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int ind = listView1.SelectedIndices[0];
            if (ind != -1)
            {
                if (listView1.Items.Count != 1)
                {
                    _list.RemoveAt(ind);
                    if (ind == 0)
                    {
                        int newid = _list[ind].Id;
                        Properties.Settings.Default.LastUsedMechanicID = newid;
                    }
                    else
                    {
                        Properties.Settings.Default.LastUsedMechanicID = _list[ind - 1].Id;
                    }
                    Properties.Settings.Default.Save();

                }
                else
                {
                    MessageBox.Show("Невозможно удалить всех механиков", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }

            UpdateList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int ind = listView1.SelectedIndices[0];
            if (ind != -1)
            {
                string s = listView1.Items[ind].SubItems[0].Text;
                Properties.Settings.Default.LastUsedMechanicID =
                    Convert.ToInt32(s);
                Properties.Settings.Default.Save();
            }

            UpdateList();
        }
    }
}
