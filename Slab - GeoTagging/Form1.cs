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
using Microsoft.VisualBasic.FileIO;

namespace Slab___GeoTagging
{
    public partial class Form1 : Form
    {
        string path_File;
        string path_Folder;
        string[] arquivos;
        string[] arquivos_inf;
        public Form1()
        {
            InitializeComponent();
        }

        private void button_log_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Logs|*.csv";
            openFileDialog1.ShowDialog();

            if (File.Exists(openFileDialog1.FileName))
            {
                textBox_log.Text = openFileDialog1.FileName;
                this.path_File = textBox_log.Text;
                textBox_output.AppendText("Arquivo .csv selecionado."+"\n");                
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
                this.path_Folder = textBox_img.Text;
                textBox_output.AppendText("Pasta selecionada." + "\n");
            }
        }

        private void button_Sync_Click(object sender, EventArgs e)
        {
             int nao_encontrados = 0;
            if (String.IsNullOrEmpty(path_File) || String.IsNullOrEmpty(path_Folder))
            {
                textBox_output.AppendText("Arquivos não selecionados." + "\n");
            }
            else
            {
                textBox_output.AppendText(path_Folder + "\n");
                textBox_output.AppendText(path_File + "\n");
                textBox_output.AppendText("\n");
                
                this.arquivos = Directory.GetFiles(path_Folder, "*.jpg", System.IO.SearchOption.TopDirectoryOnly);
                for (int i = 0; i < arquivos.Length; i++)
                {
                    FileInfo arquivos_inf = new FileInfo(arquivos[i]);
                    string criado = arquivos_inf.LastWriteTime.ToString("HH:mm:ss");
                    //textBox_output.AppendText(arquivos[i] +" "+ criado + "\n");
                    if (read_csv(path_Folder, criado) == 0)
                   {
                        textBox_output.AppendText("Coordenadas não encontradas.\n");
                        nao_encontrados++;
                    }
                }
                textBox_output.AppendText("Total de arquivos não taggeados : " + nao_encontrados + "\n");
            }          
        }
        void WriteCoordinatesToImage(string Filename, double dLat, double dLong, double alt)
        {
            byte[] bLat = BitConverter.GetBytes(dLat);
            byte[] bLong = BitConverter.GetBytes(dLong);
            byte[] balt = BitConverter.GetBytes(alt);

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

                    pi.Id = 0x0006;
                    pi.Value = balt;
                    Pic.SetPropertyItem(pi);

                    // Save file into Geotag folder
                    string geoTagFolder = path_Folder + Path.DirectorySeparatorChar + "geotagged";
                    string outputfilename = geoTagFolder + Path.DirectorySeparatorChar +
                        Path.GetFileNameWithoutExtension(Filename) + "_geotag" +
                        Path.GetExtension(Filename);

                    // Just in case
                    if (File.Exists(outputfilename))
                        File.Delete(outputfilename);

                    Pic.Save(Filename);
                }
            }
        }
        int read_csv (string path_folder, string hour_created)
        {
            using (TextFieldParser parser = new TextFieldParser(path_File))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] linha = parser.ReadFields();
                    int indice_hora = 1;
                    string hour = linha[indice_hora];
                    
                    if (hour == hour_created)
                    { 
                        for (int i = 1; i < linha.Length; i++)
                        {
                            textBox_output.AppendText(linha[i] + " ,");
                        }
                        textBox_output.AppendText("\n\n");
                        
                        double lat = Convert.ToDouble(linha[2]);
                        double lon = Convert.ToDouble(linha[3]);
                        double alt = Convert.ToDouble(linha[4]);

                        WriteCoordinatesToImage(path_Folder, lat,lon,alt);
                        return 1;
                    }
                    continue;
                }
                return 0;
            }
        }
    }
}
