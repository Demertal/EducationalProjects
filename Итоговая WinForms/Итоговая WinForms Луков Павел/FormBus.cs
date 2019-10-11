using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Windows.Forms;
using Итоговая_WinForms_Луков_Павел.Models;

namespace Итоговая_WinForms_Луков_Павел
{
    public partial class FormBus : Form
    {
        private const string InFront = "Вид спереди",
            Behind = "Вид сзади",
            InLeft = "Вид слева";

        private int _pic = 1; //вид автобуса
        private Bitmap _img;
        public FormBus()
        {
            InitializeComponent();
        }

        public FormBus(Bus ob)
        {
            InitializeComponent();
            busBindingSource.DataSource = ob;
            LoadBrandBus();
            groupBox1.Text = InFront;

            _img = new Bitmap("...\\Images\\" + (busBindingSource.DataSource as Bus).Id + "\\1.png");
            pictureBox1.Image = _img;
        }

        private void LoadBrandBus()
        {
            try
            {
                DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<BrandBus>));

                using (FileStream fs = new FileStream(Form1.BrandBusFileNameGet, FileMode.OpenOrCreate))
                {

                    var list = (List<BrandBus>)jsonFormatter.ReadObject(fs);
                    foreach (var ob in list)
                    {
                        if (ob.Id != (busBindingSource.DataSource as Bus).IdBrandBus) continue;
                        brandBusBindingSource.DataSource = ob;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            try
            {
                if (string.IsNullOrEmpty(openFileDialog1.FileName)) return;
                string fileName = "";
                switch (_pic)
                {
                    case 1:
                        fileName = "...\\Images\\" + (busBindingSource.DataSource as Bus).Id + "\\1.png";
                        break;
                    case 2:
                        fileName = "...\\Images\\" + (busBindingSource.DataSource as Bus).Id + "\\2.png";
                        break;
                    case 3:
                        fileName = "...\\Images\\" + (busBindingSource.DataSource as Bus).Id + "\\3.png";
                        break;
                }

                _img.Dispose();
                if (File.Exists(fileName)) File.Delete(fileName);
                File.Copy(openFileDialog1.FileName, fileName);
                _img = new Bitmap(fileName);
                pictureBox1.Image = _img;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _pic -= 1;
            if (_pic < 1) _pic = 3;
            switch (_pic)
            {
                case 1:
                    groupBox1.Text = InFront;
                    _img = new Bitmap("...\\Images\\" + (busBindingSource.DataSource as Bus).Id + "\\1.png");
                    pictureBox1.Image = _img;
                    break;
                case 2:
                    groupBox1.Text = InLeft;
                    _img = new Bitmap("...\\Images\\" + (busBindingSource.DataSource as Bus).Id + "\\2.png");
                    pictureBox1.Image = _img;
                    break;
                case 3:
                    groupBox1.Text = Behind;
                    _img = new Bitmap("...\\Images\\" + (busBindingSource.DataSource as Bus).Id + "\\3.png");
                    pictureBox1.Image = _img;
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _pic += 1;
            if (_pic > 3) _pic = 1;
            switch (_pic)
            {
                case 1:
                    groupBox1.Text = InFront;
                    _img = new Bitmap("...\\Images\\" + (busBindingSource.DataSource as Bus).Id + "\\1.png");
                    pictureBox1.Image = _img;
                    break;
                case 2:
                    groupBox1.Text = InLeft;
                    _img = new Bitmap("...\\Images\\" + (busBindingSource.DataSource as Bus).Id + "\\2.png");
                    pictureBox1.Image = _img;
                    break;
                case 3:
                    groupBox1.Text = Behind;
                    _img = new Bitmap("...\\Images\\" + (busBindingSource.DataSource as Bus).Id + "\\3.png");
                    pictureBox1.Image = _img;
                    break;
            }
        }
    }
}
