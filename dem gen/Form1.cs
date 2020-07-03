using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Net.Http;

namespace dem_gen
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }
        private void сгенерироватьDEMФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(Application.StartupPath + "\\ASCfiles\\" + textBox7.Text + ".asc");
            sw.WriteLine("NCOLS " + textBox1.Text);
            sw.WriteLine("NROWS " + textBox2.Text);
            sw.WriteLine("XLL" + comboBox1.Text + " " + textBox3.Text);
            sw.WriteLine("YLL" + comboBox1.Text + " " + textBox4.Text);
            sw.WriteLine("CELLSIZE " + textBox5.Text);
            sw.WriteLine("NODATA_VALUE " + textBox6.Text);

            int k = int.Parse(textBox1.Text);
            int kol = int.Parse(textBox2.Text);
            Random nl = new Random();
            int nlt = nl.Next(0, 230);
            Random r = new Random();
            double[,] array = new double[k,kol];
            for (int j = 0; j < kol; j++)             
            {
                for
                   (int i = 0; i < k; i++)
                {
                    array[i, j] = nlt + Math.Round(r.NextDouble() * 25, int.Parse(comboBox2.Text));
                    sw.Write(array[i,j] + " ");
                }
            }
            sw.Close();
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!Char.IsDigit(number) && number != 8) 
            {
                e.Handled = true;
            }
        }
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && (e.KeyChar <= 39 || e.KeyChar >= 46) && number != 47 && number != 61) //калькулятор
            {
                e.Handled = true;
            }
        }
        private async void построитьИзображениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox8.Text != null)
            {
                int numOfLines = 6;
                var lines = this.textBox8.Lines;
                var newLines = lines.Skip(numOfLines);
                this.textBox8.Lines = newLines.ToArray();
            }
            else
            {
                using (OpenFileDialog opfd = new OpenFileDialog() { Filter = ".asc файлы|*.asc", ValidateNames = true, Multiselect = false })
                {
                    if (opfd.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamReader sr = new StreamReader(opfd.FileName))
                        {
                            textBox8.Text = await sr.ReadToEndAsync();
                            int numOfLines = 6;
                            var lines = this.textBox8.Lines;
                            var newLines = lines.Skip(numOfLines);
                            this.textBox8.Lines = newLines.ToArray();
                        }
                    }
                }
            }
            {
                if (textBox10.Text != null && textBox9.Text != null)
                {
                    int w = int.Parse(textBox9.Text);
                    int h = int.Parse(textBox10.Text);
                    Bitmap picture = new Bitmap(w, h);
                    string linesss = textBox8.Text;
                    string li = " ";
                    string[] pixels = System.Text.RegularExpressions.Regex.Split(linesss, li);
                    for (int s = 0; s < h * w; s++)
                    {
                        for (int y = 0; y < h; y++)
                        {
                            for (int x = 0; x < w; x++)
                            {


                                picture.SetPixel(x, y, Color.FromArgb(255, int.Parse(pixels[s]), 0, 0));
                            }
                        }
                    }
                    pictureBox1.Image = picture;
                }
            }
        }

        private async void просмотрDEMФайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog opfd = new OpenFileDialog() { Filter = ".asc файлы|*.asc", ValidateNames = true, Multiselect = false })
            {
                if (opfd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader sr = new StreamReader(opfd.FileName))
                    {
                        textBox8.Text = await sr.ReadToEndAsync();
                    }
                }
            }
        }
    }
}
