using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace Slab___GeoTagging
{
    public partial class Form1 : Form
    {
        string log_Folder;
        string[] path_Folder;
        public Form1()
        {
            InitializeComponent();
        }

        private void button_log_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Logs|*.csv|*.xls|*.xlsx";
            openFileDialog1.ShowDialog();

            if (File.Exists(openFileDialog1.FileName))
            {
                textBox_log.Text = openFileDialog1.FileName;
                string logFolder = textBox_log.Text;
                this.log_Folder = logFolder;
                textBox_output.AppendText("Csv selecionado.\n");
                //textBox_output.AppendText(logFolder);


            }
        }

        private void button_img_Click(object sender, EventArgs e)
        {
            try
            {
                folderBrowserDialog1.SelectedPath = Path.GetDirectoryName(textBox_log.Text);
            }
            catch
            {
            }
            folderBrowserDialog1.ShowDialog();
            if (folderBrowserDialog1.SelectedPath != "")
            {
                textBox_img.Text = folderBrowserDialog1.SelectedPath;
                string pathFolder = textBox_img.Text;
                textBox_output.AppendText(pathFolder);
                textBox_output.AppendText("Pasta selecionada.\n");
                string[] arquivos = Directory.GetFiles(pathFolder, "*.jpg", SearchOption.TopDirectoryOnly);
                this.path_Folder = arquivos;
                foreach (string arq in arquivos)
                {
                    int i = 0;
                    textBox_output.AppendText(arq + "\n");
                    textBox_output.AppendText(Convert.ToString(File.GetCreationTime(arquivos[i]))+"\n");
                }
            }
        }

        private void button_sync_Click(object sender, EventArgs e)
        {
            StreamReader rd = new StreamReader(log_Folder);
            string linha = null;
            string[] lineSeparator = null;
            while ((linha = rd.ReadLine()) != null)
            {
                lineSeparator = linha.Split(';');
            }

        }

        void WriteCoordinatesToImage(string Filename, double dLat, double dLong)
        {
            byte[] bLat = BitConverter.GetBytes(dLat);
            byte[] bLong = BitConverter.GetBytes(dLong);

            using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(Filename)))
            {
                File.Delete(Filename);
                using (Image Pic = Image.FromStream(ms))
                {
                    PropertyItem pi = Pic.PropertyItems[0];
                    pi.Id = 0x0002;
                    pi.Type = 5;
                    pi.Len = bLong.Length;
                    pi.Value = bLong;
                    Pic.SetPropertyItem(pi);

                    pi.Id = 0x0004;
                    pi.Value = bLat;
                    Pic.SetPropertyItem(pi);

                    Pic.Save(Filename);
                }
            }
        }
    }
}
