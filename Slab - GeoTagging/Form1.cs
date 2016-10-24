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
                    if (read_csv(arquivos[i], criado) == 0)
                   {
                        textBox_output.AppendText("Coordenadas não encontradas.\n");
                        nao_encontrados++;
                    }
                }
                textBox_output.AppendText("Total de arquivos não taggeados : " + nao_encontrados + "\n");
            }          
        }
        //public void WriteCoordinatesToImage(string Filename, double dLat, double dLong, double alt)
        //{
        //    byte[] bLat = BitConverter.GetBytes(dLat);
        //    byte[] bLong = BitConverter.GetBytes(dLong);
        //    byte[] balt = BitConverter.GetBytes(alt);

        //    using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(Filename)))
        //    {
        //        //File.Delete(Filename);
        //        using (Image Pic = Image.FromStream(ms))
        //        {
        //            PropertyItem pi = Pic.PropertyItems[0];
        //            pi.Id = 0x0002;
        //            pi.Type = 5;
        //            pi.Len = bLong.Length;
        //            pi.Value = coordtobytearray(dLong);
        //            Pic.SetPropertyItem(pi);

        //            pi.Id = 0x0004;
        //            pi.Value = coordtobytearray(dLat);
        //            Pic.SetPropertyItem(pi);

        //            pi.Id = 0x0006;
        //            pi.Value = coordtobytearray(alt);
        //            Pic.SetPropertyItem(pi);

        //            // Save file into Geotag folder
        //            string geoTagFolder = path_Folder + Path.DirectorySeparatorChar + "geotagged";
        //            Directory.CreateDirectory(geoTagFolder);
        //            string outputfilename = geoTagFolder +
        //                Path.DirectorySeparatorChar +
        //                Path.GetFileNameWithoutExtension(Filename) + "_geotag" +
        //                Path.GetExtension(Filename);

        //            // Just in case
        //            if (File.Exists(outputfilename))
        //                File.Delete(outputfilename);

        //            ImageCodecInfo ici = GetImageCodec("image/jpeg");
        //            EncoderParameters eps = new EncoderParameters(1);
        //            eps.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);

        //            Pic.Save(outputfilename);
        //        }
        //    }
        //}
        public void WriteCoordinatesToImage(string Filename, double dLat, double dLong, double alt)
        {
            using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(Filename)))
            {
                //TXT_outputlog.AppendText("GeoTagging " + Filename + "\n");
                //Application.DoEvents();

                byte[] balt = BitConverter.GetBytes(alt);

                using (Image Pic = Image.FromStream(ms))
                {
                    PropertyItem[] pi = Pic.PropertyItems;

                    pi[0].Id = 0x0004;
                    pi[0].Type = 5;
                    pi[0].Len = sizeof(ulong) * 3;
                    pi[0].Value = coordtobytearray(dLong);
                    Pic.SetPropertyItem(pi[0]);

                    pi[0].Id = 0x0002;
                    pi[0].Type = 5;
                    pi[0].Len = sizeof(ulong) * 3;
                    pi[0].Value = coordtobytearray(dLat);
                    Pic.SetPropertyItem(pi[0]);

                    pi[0].Id = 0x0006;
                    pi[0].Type = 5;
                    pi[0].Len = 8;
                    pi[0].Value = new Rational(alt).GetBytes();
                    Pic.SetPropertyItem(pi[0]);

                    pi[0].Id = 1;
                    pi[0].Len = 2;
                    pi[0].Type = 2;

                    if (dLat < 0)
                    {
                        pi[0].Value = new byte[] { (byte)'S', 0 };
                    }
                    else
                    {
                        pi[0].Value = new byte[] { (byte)'N', 0 };
                    }

                    Pic.SetPropertyItem(pi[0]);

                    pi[0].Id = 3;
                    pi[0].Len = 2;
                    pi[0].Type = 2;
                    if (dLong < 0)
                    {
                        pi[0].Value = new byte[] { (byte)'W', 0 };
                    }
                    else
                    {
                        pi[0].Value = new byte[] { (byte)'E', 0 };
                    }
                    Pic.SetPropertyItem(pi[0]);

                    // Save file into Geotag folder
                    string rootFolder = textBox_img.Text;
                    string geoTagFolder = rootFolder + Path.DirectorySeparatorChar + "geotagged";

                    string outputfilename = geoTagFolder + Path.DirectorySeparatorChar +
                                            Path.GetFileNameWithoutExtension(Filename) + "_geotag" +
                                            Path.GetExtension(Filename);

                    // Just in case
                    if (File.Exists(outputfilename))
                        File.Delete(outputfilename);

                    ImageCodecInfo ici = GetImageCodec("image/jpeg");
                    EncoderParameters eps = new EncoderParameters(1);
                    eps.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);

                    Pic.Save(outputfilename);
                }
            }
        }

        public byte[] GetBytes(double input)
        {
            uint dem = 0;
            uint num = 0;
            byte[] answer = new byte[8];

            Array.Copy(BitConverter.GetBytes((uint)num), 0, answer, 0, sizeof(uint));
            Array.Copy(BitConverter.GetBytes((uint)dem), 0, answer, 4, sizeof(uint));

            return answer;
        }

            static ImageCodecInfo GetImageCodec(string mimetype)
        {
            foreach (ImageCodecInfo ici in ImageCodecInfo.GetImageEncoders())
            {
                if (ici.MimeType == mimetype) return ici;
            }
            return null;
        }
        public int read_csv (string file_path, string hour_created)
        {
            using (TextFieldParser parser = new TextFieldParser(path_File))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                int num_img = 0;
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

                        lat = lat / (10000000.0);
                        lon = lon / (10000000.0);
                        alt = alt / (1000.0);

                        WriteCoordinatesToImage(file_path,lat,lon,alt);
                        return 1;
                    }

                    continue;
                }
                num_img++;
                return 0;
            }
        }

        private byte[] coordtobytearray(double coordin)
        {
            double coord = Math.Abs(coordin);

            byte[] output = new byte[sizeof(double) * 3];

            int d = (int)coord;
            int m = (int)((coord - d) * 60);
            double s = ((((coord - d) * 60) - m) * 60);
            /*
21 00 00 00 01 00 00 00--> 33/1
18 00 00 00 01 00 00 00--> 24/1
06 02 00 00 0A 00 00 00--> 518/10
*/

            Array.Copy(BitConverter.GetBytes((uint)d), 0, output, 0, sizeof(uint));
            Array.Copy(BitConverter.GetBytes((uint)1), 0, output, 4, sizeof(uint));
            Array.Copy(BitConverter.GetBytes((uint)m), 0, output, 8, sizeof(uint));
            Array.Copy(BitConverter.GetBytes((uint)1), 0, output, 12, sizeof(uint));
            Array.Copy(BitConverter.GetBytes((uint)(s * 1.0e7)), 0, output, 16, sizeof(uint));
            Array.Copy(BitConverter.GetBytes((uint)1.0e7), 0, output, 20, sizeof(uint));
            /*
            Array.Copy(BitConverter.GetBytes((uint)d * 1.0e7), 0, output, 0, sizeof(uint));
            Array.Copy(BitConverter.GetBytes((uint)1.0e7), 0, output, 4, sizeof(uint));
            Array.Copy(BitConverter.GetBytes((uint)0), 0, output, 8, sizeof(uint));
            Array.Copy(BitConverter.GetBytes((uint)1), 0, output, 12, sizeof(uint));
            Array.Copy(BitConverter.GetBytes((uint)0), 0, output, 16, sizeof(uint));
            Array.Copy(BitConverter.GetBytes((uint)1), 0, output, 20, sizeof(uint));
            */
            return output;
        }
    }
}
