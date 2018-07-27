using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PengolahanCitra1
{
    public partial class Form1 : Form
    {
        Bitmap obj_bentuk1, obj_bentuk2, obj_bentuk3, obj_bentuk4, obj_grayscale1, obj_grayscale2,obj_grayscale3, obj_grayscale4,obj_tepi1, obj_tepi2,obj_tepi3, obj_tepi4;
        Bitmap obj_grayori, obj_grayk;
        Bitmap warna_1, warna_2,warna_3;
        Bitmap flip_ori, flip;
        Bitmap enhance_ori, enhance;
        Bitmap obj_detto, obj_detori;
        Bitmap obj_statto, obj_statori;
        Bitmap obj_histo, obj_hisori;
        Bitmap obj_sharpori, obj_sharpto;
        Bitmap obj_filterori, obj_filterto;
        Bitmap obj_noiseori, obj_noiseto,obj_noiseto2;
        Bitmap obj_1,obj_2,obj_3;
        Bitmap obj_fitur1, obj_fitur2, obj_fitur3, obj_fitur4;
        Bitmap obj_kuantisasi1, obj_kuantisasi2,obj_kuantisasi3;
        Bitmap objBitmap;

        // EKSTRAKSI BENTUK
        int[,] binRed = new int[4, 16];
        int[,] binGreen = new int[4, 16];
        int[,] binBlue = new int[4, 16];
        int[,] jumlahBaris = new int[4, 25];
        int[,] jumlahKolom = new int[4, 25];
        int[,] pixel = new int[4, 256];

        // EKSTRAKSI WARNA
        int[] GrMerah1 = new int[16];
        int[] GrHijau1 = new int[16];
        int[] GrBiru1 = new int[16];
        int[] GrMerah2 = new int[16];
        int[] GrHijau2 = new int[16];
        int[] GrBiru2= new int[16];
        int[] GrMerah3 = new int[16];
        int[] GrHijau3 = new int[16];
        int[] GrBiru3 = new int[16];
        int[] GrMerah4 = new int[16];
        int[] GrHijau4 = new int[16];
        int[] GrBiru4 = new int[16];

        //EKSTRAKSI TEKSTUR
        Bitmap obj_ekstrak;
        /// LBP load
        /// </summary>
        string dire = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);


        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Filter = "jpg(*.jpg)|*.jpg|bmp(*.bmp)|*.bmp|png(*.png)|*.png";
            if (openfile.ShowDialog() == DialogResult.OK && openfile.FileName.Length > 0)
            {
                picOriginal.SizeMode = PictureBoxSizeMode.Zoom;
                picOriginal.Image = Image.FromFile(openfile.FileName);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Filter= "jpg(*.jpg)|*.jpg|bmp(*.bmp)|*.bmp|png(*.png)|*.png";
            if (savefile.ShowDialog() == DialogResult.OK && savefile.FileName.Length>0)
            {
                picOriginal.Image.Save(savefile.FileName);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            
        }

        private void btnGrey_Click(object sender, EventArgs e)
        {
            obj_grayk = new Bitmap(obj_grayori);
            for (int x = 0; x < obj_grayori.Width; x++)
            {
                for (int y = 0; y < obj_grayori.Height; y++)
                {
                    Color pixelColor = obj_grayori.GetPixel(x, y);
                    int red = pixelColor.R;
                    int green = pixelColor.G;
                    int blue = pixelColor.B;
                    int rata = (int)(red + green + blue) / 3;
                    obj_grayk.SetPixel(x, y, Color.FromArgb(rata, rata, rata));
                }
            }
            pictGrey.SizeMode = PictureBoxSizeMode.Zoom;
            pictGrey.Image = obj_grayk;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void pi_Click(object sender, EventArgs e)
        {

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                obj_grayori = new Bitmap(openFileDialog1.FileName);
                picOriginal2.SizeMode = PictureBoxSizeMode.Zoom;
                picOriginal2.Image = obj_grayori;
            }
        }

        private void btnLoad1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                flip_ori = new Bitmap(openFileDialog1.FileName);
                picOriginal3.SizeMode = PictureBoxSizeMode.Zoom;
                picOriginal3.Image = flip_ori;
            }
        }

        private void btnVertical_Click(object sender, EventArgs e)
        {
            flip = new Bitmap(flip_ori);
            for (int x = 0; x < flip_ori.Width; x++)
                for (int y = 0; y < flip_ori.Height; y++)
                {
                    Color w = flip_ori.GetPixel(x, y);
                    flip.SetPixel(x, flip_ori.Height - 1 - y, w);
                }
            picVertical.SizeMode = PictureBoxSizeMode.Zoom;
            picVertical.Image = flip;
        }

        private void btnHorizontal_Click(object sender, EventArgs e)
        {
            flip = new Bitmap(flip_ori);
            for (int x = 0; x < flip_ori.Width; x++)
                for (int y = 0; y < flip_ori.Height; y++)
                {
                    Color w = flip_ori.GetPixel(x, y);
                    flip.SetPixel(flip_ori.Width - 1 - x, y, w);
                }
            picHorizontal.SizeMode = PictureBoxSizeMode.Zoom;
            picHorizontal.Image = flip;

        }

        private void btnBiner_Click(object sender, EventArgs e)
        {
            obj_grayk = new Bitmap(obj_grayori);
            Color pixelColor;
            for (int x = 0; x < obj_grayori.Width; x++)
            {
                for (int y = 0; y < obj_grayk.Height; y++)
                {
                    pixelColor = obj_grayori.GetPixel(x, y);
                    int red = pixelColor.R;
                    int green = pixelColor.G;
                    int blue = pixelColor.B;
                    int rata = (int)(red + green + blue) / 3;
                    if (rata < 128) { rata = 0; } else { rata = 255; }
                    obj_grayk.SetPixel(x, y, Color.FromArgb(rata, rata, rata));
                }
            }
            pictGrey.SizeMode = PictureBoxSizeMode.Zoom;
            pictGrey.Image = obj_grayk;

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Bitmap source = (Bitmap)picOriginal.Image;
            Bitmap copy = new Bitmap(source.Height, source.Width);
            copy = source;
            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    Color w = source.GetPixel(x, y);
                    copy.SetPixel(x, y, w);
                }
            }
            picCopy.SizeMode = PictureBoxSizeMode.StretchImage;
            picCopy.Image = copy;

        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            Bitmap bendera = new Bitmap(300, 240);
            Color merah = Color.FromArgb(255, 0, 0);
            Color putih = Color.FromArgb(255, 255, 255);
            for (int y = 0; y < 60; y++)
            {
                for (int x = 0; x < 300; x++)
                {
                    bendera.SetPixel(x, y, merah);
                }
            }
            for (int y = 60; y < 240; y++)
            {
                for (int x = 0; x < 300; x++)
                {
                    bendera.SetPixel(x, y, putih);
                }
            }
            picBendera.Image = new Bitmap(bendera.Height, bendera.Width);
            picBendera.Image = bendera;
            int number1, number2, number3;
            Int32.TryParse(tb_red.Text, out number1);
            Int32.TryParse(tb_green.Text, out number2);
            Int32.TryParse(tb_blue.Text, out number3);
            Bitmap bitCampur = new Bitmap(600, 600);
            Bitmap bitRed = new Bitmap(600, 600);
            Bitmap bitGreen = new Bitmap(600, 600);
            Bitmap bitBlue = new Bitmap(600, 600);
            Color red = Color.FromArgb(255, 0, 0);
            Color green = Color.FromArgb(0, 255, 0);
            Color blue = Color.FromArgb(0, 0, 255);
            Color campur = Color.FromArgb(number1, number2, number3);
            for (int y = 0; y < 600; y++)
            {
                for (int x = 0; x < 600; x++)
                {
                    bitRed.SetPixel(x, y, red);
                    bitGreen.SetPixel(x, y, green);
                    bitBlue.SetPixel(x, y, blue);
                    bitCampur.SetPixel(x, y, campur);
                }
            }
            boxRed.Image = new Bitmap(bitRed.Height, bitRed.Width);
            boxRed.Image = bitRed;
            boxGreen.Image = new Bitmap(bitGreen.Height, bitGreen.Width);
            boxGreen.Image = bitGreen;
            boxBlue.Image = new Bitmap(bitBlue.Height, bitBlue.Width);
            boxBlue.Image = bitBlue;
            boxMix.Image = new Bitmap(bitCampur.Height, bitCampur.Width);
            boxMix.Image = bitCampur;
        }

        private void btn_16_Click(object sender, EventArgs e)
        {
            obj_grayk = new Bitmap(obj_grayori);
            int k = 16;
            int th = (int)256 / k;
            for (int x = 0; x < obj_grayori.Width; x++)
            {
                for (int y = 0; y < obj_grayk.Height; y++)
                {
                    Color pixelColor = obj_grayori.GetPixel(x, y);
                    int red = pixelColor.R;
                    int green = pixelColor.G;
                    int blue = pixelColor.B;
                    int rata = (int)((red + green + blue) / 3);
                    int kuantisasi = (int)(rata / th);
                    int result = (int)th * kuantisasi;
                    obj_grayk.SetPixel(x, y, Color.FromArgb(result, result, result));
                }
            }
            pictGrey.SizeMode = PictureBoxSizeMode.Zoom;
            pictGrey.Image = obj_grayk;
        }

        private void btn_12_Click(object sender, EventArgs e)
        {
            obj_grayk = new Bitmap(obj_grayori);
            int k = 4;
            int th = (int)256 / k;
            for (int x = 0; x < obj_grayori.Width; x++)
            {
                for (int y = 0; y < obj_grayk.Height; y++)
                {
                    Color pixelColor = obj_grayori.GetPixel(x, y);
                    int red = pixelColor.R;
                    int green = pixelColor.G;
                    int blue = pixelColor.B;
                    int rata = (int)((red + green + blue) / 3);
                    int kuanti4red = (int)(red / th);
                    int kuanti4green = (int)(green / th);
                    int kuanti4blue = (int)(blue / th);
                    int resultred = (int)th * kuanti4red;
                    int resultgreen = (int)th * kuanti4green;
                    int resultblue = (int)th * kuanti4blue;
                    obj_grayk.SetPixel(x, y, Color.FromArgb(resultred, resultgreen, resultblue));
                }
            }
            pictGrey.SizeMode = PictureBoxSizeMode.Zoom;
            pictGrey.Image = obj_grayk;
        }

        private void btn_8_Click(object sender, EventArgs e)
        {
            obj_grayk = new Bitmap(obj_grayori);
            int k = 2;
            int th = (int)256 / k;
            for (int x = 0; x < obj_grayori.Width; x++)
            {
                for (int y = 0; y < obj_grayk.Height; y++)
                {
                    Color pixelColor = obj_grayori.GetPixel(x, y);
                    int red = pixelColor.R;
                    int green = pixelColor.G;
                    int blue = pixelColor.B;
                    //int rata = (int)((red + green + blue) / 3);
                    int kuanti2red = (int)(red/th);
                    int kuanti2green = (int)(green / th);
                    int kuanti2blue = (int)(blue / th);
                    int resultred = (int)th * kuanti2red;
                    int resultgreen = (int)th * kuanti2green;
                    int resultblue = (int)th * kuanti2blue;
                    obj_grayk.SetPixel(x, y, Color.FromArgb(resultred, resultgreen, resultblue));
                }
            }
            pictGrey.SizeMode = PictureBoxSizeMode.Zoom;
            pictGrey.Image = obj_grayk;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLoad4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                obj_grayori = new Bitmap(openFileDialog1.FileName);
                picOri.SizeMode = PictureBoxSizeMode.Zoom;
                picOri.Image = obj_grayori;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            obj_grayk = new Bitmap(obj_grayori);
            int a = Convert.ToInt16(textBox1.Text);
            for (int x = 0; x < obj_grayori.Width; x++)
                for (int y = 0; y < obj_grayk.Height; y++)
                {
                    Color w = obj_grayori.GetPixel(x, y);
                    int red = w.R;
                    int green = w.G;
                    int blue = w.B;
                    red = (int)(a * red);
                    green = (int)(a * green);
                    blue = (int)(a * blue);
                    if (red > 255) { red = 255; }
                    if (green > 255) { green = 255; }
                    if (blue > 255) { blue = 255; }
                    if (red < 0) { red = 0; }
                    if (green < 0) { green = 0; }
                    if (blue < 0) { blue = 0; }
                    obj_grayk.SetPixel(x, y, Color.FromArgb(red, green, blue));
                }
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Image = obj_grayk;
        }

        private void btn_auto_Click(object sender, EventArgs e)
        {
            obj_grayk = new Bitmap(obj_grayori);
            //Mencari nilai maksimum dan minimum
            int xgmax = 0;
            int xgmin = 255;
            for (int x = 0; x < obj_grayori.Width; x++)
                for (int y = 0; y < obj_grayori.Height; y++)
                {
                    Color w = obj_grayori.GetPixel(x, y);
                    int xg = w.R;
                    if (xg < xgmin) xgmin = xg;
                    if (xg > xgmax) xgmax = xg;
                }
            for (int x = 0; x < obj_grayori.Width; x++)
                for (int y = 0; y < obj_grayk.Height; y++)
                {
                    Color w = obj_grayori.GetPixel(x, y);
                    int xg = w.R;
                    int xb = (int)(255 * (xg - xgmin) / (xgmax - xgmin));
                    Color wb = Color.FromArgb(xb, xb, xb);
                    obj_grayk.SetPixel(x, y, wb);
                }
            pictauto.SizeMode = PictureBoxSizeMode.Zoom;
            pictauto.Image = obj_grayk;
        }

        private void btn_invers_Click(object sender, EventArgs e)
        {
            obj_grayk = new Bitmap(obj_grayori);
            for (int x = 0; x < obj_grayori.Width; x++)
                for (int y = 0; y < obj_grayori.Height; y++)
                {
                    Color w = obj_grayori.GetPixel(x, y);
                    int xg = w.R;
                    int xb = (int)(255 - xg);
                    Color wb = Color.FromArgb(xb, xb, xb);
                    obj_grayk.SetPixel(x, y, wb);
                }
            picinvers.SizeMode = PictureBoxSizeMode.Zoom;
            picinvers.Image = obj_grayk;
        }

        private void btn_negative_Click(object sender, EventArgs e)
        {
            enhance = new Bitmap(enhance_ori);
            for (int x = 0; x < enhance_ori.Width; x++)
                for (int y = 0; y < enhance_ori.Height; y++)
                {
                    Color w = enhance_ori.GetPixel(x, y);
                    int merah = 255 - w.R;
                    int hijau = 255 - w.G;
                    int biru = 255 - w.B;
                    Color wb = Color.FromArgb(merah, hijau, biru);
                    enhance.SetPixel(x, y, wb);
                }
            picture_to6.SizeMode = PictureBoxSizeMode.Zoom;
            picture_to6.Image = enhance;
        }

        private void btn_inlog_Click(object sender, EventArgs e)
        {
            enhance = new Bitmap(enhance_ori);
            int c = Convert.ToInt16(tb_c.Text);
            for (int x = 0; x < enhance_ori.Width; x++)
                for (int y = 0; y < enhance_ori.Height; y++)
                {
                    Color w = enhance_ori.GetPixel(x, y);
                    int merah = w.R;
                    int hijau = w.G;
                    int biru = w.B;
                    merah = (int)(c * Math.Exp(merah/c));
                    hijau = (int)(c * Math.Exp(hijau/c));
                    biru = (int)(c * Math.Exp(biru/c));
                    if (merah > 255) { merah = 255; }
                    if (hijau > 255) { hijau = 255; }
                    if (biru > 255) { biru = 255; }
                    if (merah < 0) { merah = 0; }
                    if (hijau < 0) { hijau = 0; }
                    if (biru < 0) { biru = 0; }
                    Color wb = Color.FromArgb(merah, hijau, biru);
                    enhance.SetPixel(x, y, wb);
                }
            picture_to6.SizeMode = PictureBoxSizeMode.Zoom;
            picture_to6.Image = enhance;
        }

        private void btn_log_Click(object sender, EventArgs e)
        {
            enhance = new Bitmap(enhance_ori);
            int c = Convert.ToInt16(tb_c.Text);
            for (int x = 0; x < enhance_ori.Width; x++)
                for (int y = 0; y < enhance_ori.Height; y++)
                {
                    Color w = enhance_ori.GetPixel(x, y);
                    int merah = w.R;
                    int hijau = w.G;
                    int biru = w.B;
                    merah = (int)(c * Math.Log(merah, 10));
                    hijau = (int)(c * Math.Log(hijau, 10));
                    biru = (int)(c * Math.Log(biru, 10));
                    if (merah > 255) { merah = 255; }
                    if (hijau > 255) { hijau = 255; }
                    if (biru > 255) { biru = 255; }
                    if (merah < 0) { merah = 0; }
                    if (hijau < 0) { hijau = 0; }
                    if (biru < 0) { biru = 0; }
                    Color wb = Color.FromArgb(merah, hijau, biru);
                    enhance.SetPixel(x, y, wb);
                }
            picture_to6.SizeMode = PictureBoxSizeMode.Zoom;
            picture_to6.Image = enhance;
        }

        private void btn_power_Click(object sender, EventArgs e)
        {
            enhance = new Bitmap(enhance_ori);
            float c = Convert.ToSingle(tb_c.Text);
            float Y = Convert.ToSingle(tb_y.Text);
            for (int x = 0; x < enhance_ori.Width; x++)
                for (int y = 0; y < enhance_ori.Height; y++) 
                {
                    Color w = enhance_ori.GetPixel(x, y);
                    int merah = w.R;
                    int hijau = w.G;
                    int biru = w.B;
                    merah = (int)(c * merah*Math.Exp(Y));
                    hijau = (int)(c * hijau*Math.Exp(Y));
                    biru = (int)(c * biru*Math.Exp(Y));
                    if (merah > 255) { merah = 255; }
                    if (hijau > 255) { hijau = 255; }
                    if (biru > 255) { biru = 255; }
                    if (merah < 0) { merah = 0; }
                    if (hijau < 0) { hijau = 0; }
                    if (biru < 0) { biru = 0; }
                    enhance.SetPixel(x, y, Color.FromArgb(merah, hijau, biru));
                }
            picture_to6.SizeMode = PictureBoxSizeMode.Zoom;
            picture_to6.Image = enhance;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                obj_1 = new Bitmap(openFileDialog1.FileName);
                pictureLoad1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureLoad1.Image = obj_1;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                obj_2 = new Bitmap(openFileDialog1.FileName);
                pictureLoad2.SizeMode = PictureBoxSizeMode.Zoom;
                pictureLoad2.Image = obj_2;
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            obj_histo = new Bitmap(obj_hisori);
            for (int x = 0; x < obj_hisori.Width; x++)
            {
                for (int y = 0; y < obj_histo.Height; y++)
                {
                    Color pixelColor = obj_hisori.GetPixel(x, y);
                    int red = pixelColor.R;
                    int green = pixelColor.G;
                    int blue = pixelColor.B;
                    int rata = (int)(red + green + blue) / 3;
                    obj_histo.SetPixel(x, y, Color.FromArgb(rata, rata, rata));
                }
            }
            pictureHisto.SizeMode = PictureBoxSizeMode.Zoom;
            pictureHisto.Image = obj_histo;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Bitmap HistoAsal = (Bitmap)pictureHisto.Image;
            Color warna;

            for (int y = 0; y < HistoAsal.Height; y++)
            {
                for (int x = 0; x < HistoAsal.Width; x++)
                {
                    warna = HistoAsal.GetPixel(x, y);
                    int merah = warna.R;
                    int hijau = warna.G;
                    int biru = warna.B;
                    int indexR = merah / 16;
                    int indexG = hijau / 16;
                    int indexB = biru / 16;

                    GrMerah1[indexR]++;
                    GrHijau1[indexG]++;
                    GrBiru1[indexB]++;
                }
            }
           
            for (int i = 0; i < 16; i++)
            {
                this.chart3.Series["Red"].Points.AddXY(i, GrMerah1[i]);
                this.chart3.Series["Green"].Points.AddXY(16 + i, GrHijau1[i]);
                this.chart3.Series["Blue"].Points.AddXY(32 + i, GrBiru1[i]);
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                obj_statori = new Bitmap(openFileDialog1.FileName);
                picOridet.SizeMode = PictureBoxSizeMode.Zoom;
                picOridet.Image = obj_statori;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            obj_statto = new Bitmap(obj_statori);
            for (int x = 0; x < obj_statori.Width; x++)
                for (int y = 0; y < obj_statto.Height; y++)
                {
                    Color w = obj_statori.GetPixel(x, y);
                    int red = w.R;
                    int green = w.G;
                    int blue = w.B;
                    if ((red < 95) || (red > 140) || (green < 90) || (green > 130) || (blue < 20) && (blue > 70))
                    { red = 0; green = 0; blue = 0; }
                    obj_statto.SetPixel(x, y, Color.FromArgb(red, green, blue));
                }
            picture_static.SizeMode = PictureBoxSizeMode.Zoom;
            picture_static.Image = obj_statto;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int r = 120, g = 133, b = 170;
            obj_statto = new Bitmap(obj_statori);
            for (int x = 0; x < obj_statori.Width; x++)
                for (int y = 0; y < obj_statto.Height; y++)
                {
                    Color w = obj_statori.GetPixel(x, y);
                    int red = w.R;
                    int green = w.G;
                    int blue = w.B;
                    int d = Math.Abs(red - r) + Math.Abs(green - g) + Math.Abs(blue - b);
                    if (d > 150)
                    {
                        red = 0; green = 0; blue = 0;
                    }

                    obj_statto.SetPixel(x, y, Color.FromArgb(red, green, blue));
                }
            picture_distance.SizeMode = PictureBoxSizeMode.Zoom;
            picture_distance.Image = obj_statto;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            obj_histo = new Bitmap(obj_hisori);
            this.chart1.Series["Red"].Points.Clear();
            this.chart1.Series["Green"].Points.Clear();
            this.chart1.Series["Blue"].Points.Clear();
            this.chart2.Series["Red"].Points.Clear();
            this.chart2.Series["Green"].Points.Clear();
            this.chart2.Series["Blue"].Points.Clear();
            this.chart3.Series["Red"].Points.Clear();
            this.chart3.Series["Green"].Points.Clear();
            this.chart3.Series["Blue"].Points.Clear();
            this.chart10.Series["Red"].Points.Clear();
            this.chart10.Series["Green"].Points.Clear();
            this.chart10.Series["Blue"].Points.Clear();

        }

        private void button13_Click(object sender, EventArgs e)
        {
            obj_histo = new Bitmap(obj_hisori);
            float[] h = new float[256];
            float[] c = new float[256];
            int i;
            for (i = 0; i < 256; i++) h[i] = 0;
            for (int x = 0; x < obj_hisori.Width; x++)
                for (int y = 0; y < obj_histo.Height; y++)
                {
                    Color w = obj_hisori.GetPixel(x, y);
                    int xg = w.R;
                    h[xg] = h[xg] + 1;
                }
            c[0] = h[0];
            for (i = 1; i < 256; i++) c[i] = c[i - 1] + h[i];
            int nx = obj_hisori.Width;
            int ny = obj_hisori.Height;
            for (int x = 0; x < obj_hisori.Width; x++)
                for (int y = 0; y < obj_histo.Height; y++)
                {
                    Color w = obj_hisori.GetPixel(x, y);
                    int xg = w.R;
                    int xb = (int)(255 * c[xg] / nx / ny);
                    Color wb = Color.FromArgb(xb, xb, xb);
                    obj_histo.SetPixel(x, y, wb);
                }
            pictureHisto2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureHisto2.Image = obj_histo;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            obj_filterto = new Bitmap(obj_filterto);
            float[] a = new float[5];
            a[1] = (float)0.2;
            a[2] = (float)0.2;
            a[3] = (float)0.2;
            a[4] = (float)0.2;
            a[0] = (float)0.2;
            for (int x = 1; x < obj_filterori.Width - 1; x++)
                for (int y = 1; y < obj_filterori.Height - 1; y++)
                {
                    Color w1 = obj_filterori.GetPixel(x - 1, y);
                    Color w2 = obj_filterori.GetPixel(x + 1, y);
                    Color w3 = obj_filterori.GetPixel(x, y - 1);
                    Color w4 = obj_filterori.GetPixel(x, y + 1);
                    Color w = obj_filterori.GetPixel(x, y);
                    int x1 = w1.R;
                    int x2 = w2.R;
                    int x3 = w3.R;
                    int x4 = w4.R;
                    int xg = w.R;
                    int xb = (int)(a[0] * xg);
                    xb = (int)(xb + a[1] * x1 + a[2] * x2 + a[3] * x3 + a[3] * x4);
                    if (xb < 0) xb = 0;
                    if (xb > 255) xb = 255;
                    Color wb = Color.FromArgb(xb, xb, xb);
                    obj_filterto.SetPixel(x, y, wb);
                }
            picturefilter4.SizeMode = PictureBoxSizeMode.Zoom;
            picturefilter4.Image = obj_filterto;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                obj_filterori = new Bitmap(openFileDialog1.FileName);
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox2.Image = obj_filterori;
            }
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            float[] a = new float[10];
            a[1] = (float)0.1;
            a[2] = (float)0.1;
            a[3] = (float)0.1;
            a[4] = (float)0.1;
            a[5] = (float)0.2;
            a[6] = (float)0.1;
            a[7] = (float)0.1;
            a[8] = (float)0.1;
            a[9] = (float)0.1;
            obj_filterto = new Bitmap(obj_filterori);
            for (int x = 1; x < obj_filterori.Width - 1; x++)
                for (int y = 1; y < obj_filterori.Height - 1; y++)
                {
                    Color w1 = obj_filterori.GetPixel(x - 1, y - 1);
                    Color w2 = obj_filterori.GetPixel(x - 1, y);
                    Color w3 = obj_filterori.GetPixel(x - 1, y + 1);
                    Color w4 = obj_filterori.GetPixel(x, y - 1);
                    Color w5 = obj_filterori.GetPixel(x, y);
                    Color w6 = obj_filterori.GetPixel(x, y + 1);
                    Color w7 = obj_filterori.GetPixel(x + 1, y - 1);
                    Color w8 = obj_filterori.GetPixel(x + 1, y);
                    Color w9 = obj_filterori.GetPixel(x + 1, y + 1);
                    int x1 = (w1.R + w1.G + w1.B) / 3;
                    int x2 = (w2.R + w2.G + w2.B) / 3;
                    int x3 = (w3.R + w3.G + w3.B) / 3;
                    int x4 = (w4.R + w4.G + w4.B) / 3;
                    int x5 = (w5.R + w5.G + w5.B) / 3;
                    int x6 = (w6.R + w6.G + w6.B) / 3;
                    int x7 = (w7.R + w7.G + w7.B) / 3;
                    int x8 = (w8.R + w8.G + w8.B) / 3;
                    int x9 = (w9.R + w9.G + w9.B) / 3;
                    int xb = (int)(a[1] * x1 + a[2] * x2 + a[3] * x3);
                    xb = (int)(xb + a[4] * x4 + a[5] * x5 + a[6] * x6);
                    xb = (int)(xb + a[7] * x7 + a[8] * x8 + a[9] * x9);
                    if (xb < 0) xb = 0;
                    if (xb > 255) xb = 255;
                    Color wb = Color.FromArgb(xb, xb, xb);
                obj_filterto.SetPixel(x, y, wb);
                }
            picturefilter8.SizeMode = PictureBoxSizeMode.Zoom;
            picturefilter8.Image = obj_filterto;
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            float[] a = new float[5];
            a[1] = (float)0.2;
            a[2] = (float)0.2;
            a[3] = (float)0.2;
            a[4] = (float)0.2;
            a[0] = (float)0.2;
            obj_filterto= new Bitmap(obj_filterori);
            for (int x = 1; x < obj_filterori.Width - 1; x++)
                for (int y = 1; y < obj_filterori.Height - 1; y++)
                {
                    Color w1 = obj_filterori.GetPixel(x - 1, y);
                    Color w2 = obj_filterori.GetPixel(x + 1, y);
                    Color w3 = obj_filterori.GetPixel(x, y - 1);
                    Color w4 = obj_filterori.GetPixel(x, y + 1);
                    Color w = obj_filterori.GetPixel(x, y);
                    int x1 = (w1.R + w1.G + w1.B) / 3;
                    int x2 = (w2.R + w2.G + w2.B) / 3;
                    int x3 = (w3.R + w3.G + w3.B) / 3;
                    int x4 = (w4.R + w4.G + w4.B) / 3;
                    int xg = (w.R + w.G + w.B) / 3;
                    int xb = (int)(a[0] * xg);
                    xb = (int)(xb + a[1] * x1 + a[2] * x2 + a[3] * x3 + a[3] * x4);
                    if (xb < 0) xb = 0;
                if (xb > 255) xb = 255;
                    Color wb = Color.FromArgb(xb, xb, xb);
                    obj_filterto.SetPixel(x, y, wb);
                }
            picturefilter4.SizeMode = PictureBoxSizeMode.Zoom;
            picturefilter4.Image = obj_filterto;
        }

        private void btnLoad9_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                obj_noiseori = new Bitmap(openFileDialog1.FileName);
                pictureLoad9.SizeMode = PictureBoxSizeMode.Zoom;
                pictureLoad9.Image = obj_noiseori;
            }
        }

        private void btnNoiseG_Click(object sender, EventArgs e)
        {
            obj_noiseto = new Bitmap(obj_noiseori);
            Random r = new Random();
            for (int x = 0; x < obj_noiseori.Width; x++)
                for (int y = 0; y < obj_noiseori.Height; y++)
                {
                    Color w = obj_noiseori.GetPixel(x, y);
                    int xr = w.R;
                    int xg = w.G;
                    int xb = w.B;
                    //int xb = xg;
                    int nr = r.Next(0, 100);
                    if (nr < 20)
                    {
                        int ns = r.Next(0, 256) - 128;
                        xr= (int)(xr + ns);
                        xg = (int)(xg + ns);
                        xb = (int)(xg + ns);

                        if (xr < 0) xr = -xr;
                        if (xr > 255) xr = 255;

                        if (xg < 0) xg = -xg;
                        if (xg > 255) xg = 255;

                        if (xb < 0) xb = -xb;
                        if (xb > 255) xb = 255;
                    }
                    Color wb = Color.FromArgb(xr, xg, xb);
                    obj_noiseto.SetPixel(x, y, wb);
                }
            pictureNoise.SizeMode = PictureBoxSizeMode.Zoom;
            pictureNoise.Image = obj_noiseto;
        }

        private void btnNoiseS_Click(object sender, EventArgs e)
        {
            obj_noiseto = new Bitmap(obj_noiseori);
            Random r = new Random();
            for (int x = 0; x < obj_noiseori.Width; x++)
                for (int y = 0; y < obj_noiseori.Height; y++)
                {
                    Color w = obj_noiseori.GetPixel(x, y);
                    int xr = w.R;
                    int xg = w.G;
                    int xb = w.B;
                    int nr = r.Next(0, 100);
                    if (nr < 20) xr = 0;
                    if (nr < 20) xg = 0;
                    if (nr < 20) xb = 0;
                    Color wb = Color.FromArgb(xr, xg, xb);
                    obj_noiseto.SetPixel(x, y, wb);
                }
            pictureNoise.SizeMode = PictureBoxSizeMode.Zoom;
            pictureNoise.Image = obj_noiseto;
        }

        private void btnNoiseSNP_Click(object sender, EventArgs e)
        {
            obj_noiseto = new Bitmap(obj_noiseori);
            Random r = new Random();
            for (int x = 0; x < obj_noiseori.Width; x++)
                for (int y = 0; y < obj_noiseori.Height; y++)
                {
                    Color w = obj_noiseori.GetPixel(x, y);
                    int xr = w.R;
                    int xg = w.G;
                    int xb = w.B;
                    int nr = r.Next(0, 100);
                    if (nr < 20) xr = 255;
                    if (nr < 20) xg = 255;
                    if (nr < 20) xb = 255;
                    Color wb = Color.FromArgb(xr, xg, xb);
                    obj_noiseto.SetPixel(x, y, wb);
                }
            pictureNoise.SizeMode = PictureBoxSizeMode.Zoom;
            pictureNoise.Image = obj_noiseto;
        }

        private void btnRata_Click(object sender, EventArgs e)
        {
            obj_noiseto2 = new Bitmap(obj_noiseto);
            for (int x = 1; x < obj_noiseto.Width - 1; x++)
                for (int y = 1; y < obj_noiseto.Height - 1; y++)
                {
                    Color w1 = obj_noiseto.GetPixel(x - 1, y - 1);
                    Color w2 = obj_noiseto.GetPixel(x - 1, y);
                    Color w3 = obj_noiseto.GetPixel(x - 1, y + 1);
                    Color w4 = obj_noiseto.GetPixel(x, y - 1);
                    Color w5 = obj_noiseto.GetPixel(x, y);
                    Color w6 = obj_noiseto.GetPixel(x, y + 1);
                    Color w7 = obj_noiseto.GetPixel(x + 1, y - 1);
                    Color w8 = obj_noiseto.GetPixel(x + 1, y);
                    Color w9 = obj_noiseto.GetPixel(x + 1, y + 1);
                    int r = (int)((w1.R + w2.R + w3.R + w4.R + w5.R
                        + w6.R + w7.R + w8.R + w9.R) / 9);
                    int g = (int)((w1.G + w2.G + w3.G + w4.G + w5.G
                        + w6.G + w7.G + w8.G + w9.G) / 9);
                    int b = (int)((w1.B + w2.B + w3.B + w4.B + w5.B
                        + w6.B + w7.B + w8.B + w9.B) / 9);
                    Color wb = Color.FromArgb(r,g,b);
                    obj_noiseto2.SetPixel(x, y, wb);
                }
            pictureReduksi.SizeMode = PictureBoxSizeMode.Zoom;
            pictureReduksi.Image = obj_noiseto2;
        }

        private void btnGaus_Click(object sender, EventArgs e)
        {
            obj_noiseto2 = new Bitmap(obj_noiseto);
            for (int x = 1; x < obj_noiseto.Width - 1; x++)
                for (int y = 1; y < obj_noiseto.Height - 1; y++)
                {
                    Color w1 = obj_noiseto.GetPixel(x - 1, y - 1);
                    Color w2 = obj_noiseto.GetPixel(x - 1, y);
                    Color w3 = obj_noiseto.GetPixel(x - 1, y + 1);
                    Color w4 = obj_noiseto.GetPixel(x, y - 1);
                    Color w5 = obj_noiseto.GetPixel(x, y);
                    Color w6 = obj_noiseto.GetPixel(x, y + 1);
                    Color w7 = obj_noiseto.GetPixel(x + 1, y - 1);
                    Color w8 = obj_noiseto.GetPixel(x + 1, y);
                    Color w9 = obj_noiseto.GetPixel(x + 1, y + 1);
                    int r = (int)((w1.R + w2.R + w3.R + w4.R + 5 * w5.R
                        + w6.R + w7.R + w8.R + w9.R) / 14);
                    int g = (int)((w1.G + w2.G + w3.G + w4.G + 5 * w5.G
                    + w6.G + w7.G + w8.G + w9.G) / 14);
                    int b = (int)((w1.B + w2.B + w3.B + w4.B + 5 * w5.B
                    + w6.B + w7.B + w8.B + w9.B) / 14);
                    Color wb = Color.FromArgb(r ,g ,b );
                    obj_noiseto2.SetPixel(x, y, wb);
                }
            pictureReduksi.SizeMode = PictureBoxSizeMode.Zoom;
            pictureReduksi.Image = obj_noiseto2;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                obj_detori = new Bitmap(openFileDialog1.FileName);
                pictureBoxori.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBoxori.Image = obj_detori;
            }
        }

        private void btnRobert_Click(object sender, EventArgs e)
        {
            obj_detto = new Bitmap(obj_detori);
            for (int x = 1; x < obj_detori.Width; x++)
                for (int y = 1; y < obj_detori.Height; y++)
                {
                    Color w1 = obj_detori.GetPixel(x - 1, y);
                    Color w2 = obj_detori.GetPixel(x, y);
                    Color w3 = obj_detori.GetPixel(x, y - 1);
                    Color w4 = obj_detori.GetPixel(x, y);
                    int r = (int)((-w1.R + w2.R) + (-w3.R + w4.R));
                    int g = (int)((-w1.G + w2.G) + (-w3.G + w4.G));
                    int b = (int)((-w1.B + w2.B) + (-w3.B + w4.B));
                    if (r < 0) r = -r;
                    if (r > 255) r = 255;
                    if (g < 0) g = -g;
                    if (g > 255) g = 255;
                    if (b < 0) b = -b;
                    if (b > 255) b = 255;
                    Color wb = Color.FromArgb(r, g, b);
                    obj_detto.SetPixel(x, y, wb);
                }
            pictureBoxto.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxto.Image = obj_detto;
        }

        private void tabPage8_Click(object sender, EventArgs e)
        {

        }

        private void pictureHisto_Click(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                warna_1 = new Bitmap(openFileDialog1.FileName);
                pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
                warna_1 = new Bitmap(warna_1, new Size(100, 100));
                pictureBox3.Image = warna_1;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Bitmap HistoAsal = (Bitmap)pictureBox3.Image;
            Color warna;

            for (int y = 0; y < HistoAsal.Height; y++)
            {
                for (int x = 0; x < HistoAsal.Width; x++)
                {
                    warna = HistoAsal.GetPixel(x, y);
                    int merah = warna.R;
                    int hijau = warna.G;
                    int biru = warna.B;
                    int indexR2 = merah / 16;
                    int indexG2 = hijau / 16;
                    int indexB2 = biru / 16;

                    GrMerah2[indexR2]++;
                    GrHijau2[indexG2]++;
                    GrBiru2[indexB2]++;
                }
            }

            for (int i = 0; i < 16; i++)
            {
                this.chart2.Series["Red"].Points.AddXY(i, GrMerah2[i]);
                this.chart2.Series["Green"].Points.AddXY(16 + i, GrHijau2[i]);
                this.chart2.Series["Blue"].Points.AddXY(32 + i, GrBiru2[i]);
            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                obj_bentuk4 = new Bitmap(openFileDialog1.FileName);
                pictureBox14.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox14.Image = obj_bentuk4;
            }
        }

        public int getMinShiftLBP(int[] polaLBP)
        {
            int minimal = 255;
            int temp = 0;
            for (int i = 0; i < 8; i++)
            {
                temp = polaLBP[7];
                polaLBP[7] = polaLBP[6];
                polaLBP[6] = polaLBP[5];
                polaLBP[5] = polaLBP[4];
                polaLBP[4] = polaLBP[3];
                polaLBP[3] = polaLBP[2];
                polaLBP[2] = polaLBP[1];
                polaLBP[1] = polaLBP[0];
                polaLBP[0] = temp;

                int value = 0;
                for (int j = 0; j < 8; j++)
                {
                    value += ((int)Math.Pow(2, j)) * polaLBP[j];
                }
                if (minimal > value) minimal = value;
            }
            return minimal;
        }
        public int getValueLBP(Bitmap outputBitmap, Bitmap bmp, int x, int y)
        {
            int value = 0;
            int[] polaLBP = new int[8];
            for (int i = 0; i < 8; i++)
            {
                switch (i)
                {
                    case 0: if (bmp.GetPixel(x - 1, y - 1).R >= bmp.GetPixel(x, y).R) polaLBP[7] = 1; else polaLBP[7] = 0; break;
                    case 1: if (bmp.GetPixel(x, y - 1).R >= bmp.GetPixel(x, y).R) polaLBP[6] = 1; else polaLBP[6] = 0; break;
                    case 2: if (bmp.GetPixel(x + 1, y - 1).R >= bmp.GetPixel(x, y).R) polaLBP[5] = 1; else polaLBP[5] = 0; break;
                    case 3: if (bmp.GetPixel(x - 1, y).R >= bmp.GetPixel(x, y).R) polaLBP[4] = 1; else polaLBP[4] = 0; break;
                    case 4: if (bmp.GetPixel(x + 1, y + 1).R >= bmp.GetPixel(x, y).R) polaLBP[3] = 1; else polaLBP[3] = 0; break;
                    case 5: if (bmp.GetPixel(x, y + 1).R >= bmp.GetPixel(x, y).R) polaLBP[2] = 1; else polaLBP[2] = 0; break;
                    case 6: if (bmp.GetPixel(x - 1, y + 1).R >= bmp.GetPixel(x, y).R) polaLBP[1] = 1; else polaLBP[1] = 0; break;
                    case 7: if (bmp.GetPixel(x - 1, y).R >= bmp.GetPixel(x, y).R) polaLBP[0] = 1; else polaLBP[0] = 0; break;

                }
            }
            value = getMinShiftLBP(polaLBP);
            outputBitmap.SetPixel(x, y, Color.FromArgb(value, value, value));
            return value;
        }


        private void button38_Click(object sender, EventArgs e)
        {
            int[] jarak = new int[3];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    jarak[0] += Math.Abs(jumlahBaris[3, i] - jumlahBaris[0, i]);
                    jarak[0] += Math.Abs(jumlahKolom[3, i] - jumlahKolom[0, i]);
                    jarak[1] += Math.Abs(jumlahBaris[3, i] - jumlahBaris[1, i]);
                    jarak[1] += Math.Abs(jumlahKolom[3, i] - jumlahKolom[1, i]);
                    jarak[2] += Math.Abs(jumlahBaris[3, i] - jumlahBaris[2, i]);
                    jarak[2] += Math.Abs(jumlahKolom[3, i] - jumlahKolom[2, i]);
                }
            }
            label11.Text = jarak[0].ToString();

            if (jarak[0] < jarak[1] && jarak[0] < jarak[2])
            {
                label11.Text = "Data mirip dengan gambar 1";
                pictureBox15.Image = obj_bentuk1;
            }
            else if (jarak[1] < jarak[0] && jarak[1] < jarak[2])
            {
                label11.Text = "Data mirip dengan gambar 2";
                pictureBox15.Image = obj_bentuk2;
            }
            else if (jarak[2] < jarak[0] && jarak[2] < jarak[1])
            {
                label11.Text = "Data mirip dengan gambar 3";
                pictureBox15.Image = obj_bentuk3;
            }
        }

        private void button41_Click(object sender, EventArgs e)
        {
            
        }

        private void button40_Click(object sender, EventArgs e)
        {

            int[] binPixel1 = new int[256];
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                objBitmap = new Bitmap(openFileDialog1.FileName);
                objBitmap = new Bitmap(objBitmap, new Size(100, 100));
                obj_fitur1 = new Bitmap(openFileDialog1.FileName);
                obj_fitur1 = new Bitmap(obj_fitur1, new Size(100, 100));
                pictureBox6.Image = obj_fitur1;
            }
            //Grayscale
            Bitmap bmpGrey = objBitmap;
            for (int y = 0; y < objBitmap.Height; y++)
            {
                for (int x = 0; x < objBitmap.Width; x++)
                {
                    Color color = objBitmap.GetPixel(x, y);
                    int red = color.R;
                    int green = color.G;
                    int blue = color.B;
                    int grey = (int)(red + green + blue) / 3;
                    color = Color.FromArgb(grey, grey, grey);
                    bmpGrey.SetPixel(x, y, color);
                }
            }
            //LBP
            Bitmap bmpLbp = bmpGrey;
            for (int y = 1; y < bmpGrey.Height - 1; y++)
            {
                for (int x = 1; x < bmpGrey.Width - 1; x++)
                {
                    int[] lbp = new int[8];
                    lbp[0] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x - 1, y - 1).R ? 0 : 1;
                    lbp[1] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x, y - 1).R ? 0 : 1;
                    lbp[2] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x + 1, y - 1).R ? 0 : 1;
                    lbp[3] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x - 1, y).R ? 0 : 1;
                    lbp[4] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x + 1, y).R ? 0 : 1;
                    lbp[5] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x - 1, y + 1).R ? 0 : 1;
                    lbp[6] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x, y + 1).R ? 0 : 1;
                    lbp[7] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x + 1, y + 1).R ? 0 : 1;
                    int lbpColor = getMinimumLBP(lbp);
                    Color color = Color.FromArgb(lbpColor, lbpColor, lbpColor);
                    bmpLbp.SetPixel(x, y, color);
                    binPixel1[lbpColor]++;
                }
            }
            pictureBox6.Image = bmpLbp;
            //Set Histogram
            this.chart4.Series["pixel"].Points.Clear();
            for (int i = 0; i <= 255; i++)
            {
                this.chart4.Series["pixel"].Points.AddXY(i, binPixel1[i]);
                pixel[0, i] = binPixel1[i];
            }
        }

        private int getMinimumLBP(int[] angka)
        {
            String[] lbp = new String[8];
            int min = 0;

            for (int i = 0; i < 8; i++)
            {
                for (int j = i; j < i + 8; j++)
                {
                    if (j >= 8)
                        lbp[i] += angka[j - 8];
                    else
                        lbp[i] += angka[j];
                }
            }
            for (int i = 0; i < 8; i++)
            {
                if (min < Convert.ToInt32(lbp[i], 2))
                    min = Convert.ToInt32(lbp[i], 2);
            }
            return min;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button42_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                obj_bentuk4 = new Bitmap(openFileDialog1.FileName);
                obj_bentuk4 = new Bitmap(obj_bentuk4, new Size(100, 100));
                pictureBox14.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox14.Image = obj_bentuk4;
            }
        }

        private void chart10_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }


        private void button39_Click(object sender, EventArgs e)
        {
            this.chart7.Series["Row"].Points.Clear();
            this.chart7.Series["Coloumn"].Points.Clear();
            this.chart8.Series["Row"].Points.Clear();
            this.chart8.Series["Column"].Points.Clear();
            this.chart9.Series["Row"].Points.Clear();
            this.chart9.Series["Column"].Points.Clear();
            this.chart11.Series["Column"].Points.Clear();
            this.chart11.Series["Row"].Points.Clear();
        }

        /// LBP
        int[,] dataLBP = new int[10000, 10000];
        int[] loadLbp = new int[256]; 
        private Bitmap greyScale(Bitmap gambar)
        {
            for (int i = 0; i < gambar.Width; i++)
            {
                for (int j = 0; j < gambar.Height; j++)
                {
                    Color pixelColor = gambar.GetPixel(i, j);
                    int a = pixelColor.A;
                    int r = pixelColor.R;
                    int g = pixelColor.G;
                    int b = pixelColor.B;

                    int avg = (r + g + b) / 3;
                    Color newColor = Color.FromArgb(a, avg, avg, avg);
                    gambar.SetPixel(i, j, newColor);
                }
            }
            return gambar;
        }


        private int[] isiArray(int[] nilaiLbp)
        {
            for (int i = 0; i < nilaiLbp.Length; i++)
            {
                nilaiLbp[i] = 0;
            }
            return nilaiLbp;
        }



        private int[] submit(int[] nilaiLbp, int hasil)
        {
            for (int i = 0; i < nilaiLbp.Length; i++)
            {
                if (i == hasil)
                {
                    nilaiLbp[i]++;
                }
            }
            return nilaiLbp;
        }

        public int convert(int[] data)
        {
            int retval = 0;
            int[] tmp = new int[8];
            int cnt = 7;
            for (int i = 0; i < 8; i++)
            {
                tmp[i] = data[cnt];
                cnt--;
            }

            for (int i = 0; i < 8; i++)
            {
                if (tmp[i] == 1)
                {
                    retval += (int)Math.Pow(2, i);
                }
            }
            return retval;
        }

        private int binary(int[] tem)
        {
            int min = 256;
            int tmpdata = 0;
            for (int i = 0; i < 8; i++)
            {
                tmpdata = convert(tem);

                if (tmpdata < min)
                    min = tmpdata;
                tem = Swap(tem);

            }
            return min;
        }

        public int[] Swap(int[] data)
        {
            int tmp = data[0];
            data[0] = data[1];
            data[1] = data[2];
            data[2] = data[3];
            data[3] = data[4];
            data[4] = data[5];
            data[5] = data[6];
            data[6] = data[7];
            data[7] = tmp;
            return data;

        }

        private int[] lbp(int x, int y, Bitmap gambar)
        {
            int[] data = new int[8];
            //hasilLbp[0]=
            //if(gambar.GetPixel(x,y).R < )
            if (gambar.GetPixel(x, y).R > gambar.GetPixel(x - 1, y - 1).R) data[0] = 0;
            else data[0] = 1;
            if (gambar.GetPixel(x, y).R > gambar.GetPixel(x, y - 1).R) data[1] = 0;
            else data[1] = 1;
            if (gambar.GetPixel(x, y).R > gambar.GetPixel(x + 1, y - 1).R) data[2] = 0;
            else data[2] = 1;
            if (gambar.GetPixel(x, y).R > gambar.GetPixel(x + 1, y).R) data[3] = 0;
            else data[3] = 1;
            if (gambar.GetPixel(x, y).R > gambar.GetPixel(x + 1, y + 1).R) data[4] = 0;
            else data[4] = 1;
            if (gambar.GetPixel(x, y).R > gambar.GetPixel(x, y + 1).R) data[5] = 0;
            else data[5] = 1;
            if (gambar.GetPixel(x, y).R > gambar.GetPixel(x - 1, y + 1).R) data[6] = 0;
            else data[6] = 1;
            if (gambar.GetPixel(x, y).R > gambar.GetPixel(x - 1, y).R) data[7] = 0;
            else data[7] = 1;

            return data;
        }


        private void button23_Click(object sender, EventArgs e)
        {
            int[] binPixel1 = new int[256];
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                objBitmap = new Bitmap(openFileDialog1.FileName);
                objBitmap = new Bitmap(objBitmap, new Size(100, 100));
                obj_fitur2 = new Bitmap(openFileDialog1.FileName);
                obj_fitur2 = new Bitmap(obj_fitur2, new Size(100, 100));
                pictureBox7.Image = obj_fitur2;
            }
            //Grayscale
            Bitmap bmpGrey = objBitmap;
            for (int y = 0; y < objBitmap.Height; y++)
            {
                for (int x = 0; x < objBitmap.Width; x++)
                {
                    Color color = objBitmap.GetPixel(x, y);
                    int red = color.R;
                    int green = color.G;
                    int blue = color.B;
                    int grey = (int)(red + green + blue) / 3;
                    color = Color.FromArgb(grey, grey, grey);
                    bmpGrey.SetPixel(x, y, color);
                }
            }
            //LBP
            Bitmap bmpLbp = bmpGrey;
            for (int y = 1; y < bmpGrey.Height - 1; y++)
            {
                for (int x = 1; x < bmpGrey.Width - 1; x++)
                {
                    int[] lbp = new int[8];
                    lbp[0] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x - 1, y - 1).R ? 0 : 1;
                    lbp[1] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x, y - 1).R ? 0 : 1;
                    lbp[2] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x + 1, y - 1).R ? 0 : 1;
                    lbp[3] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x - 1, y).R ? 0 : 1;
                    lbp[4] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x + 1, y).R ? 0 : 1;
                    lbp[5] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x - 1, y + 1).R ? 0 : 1;
                    lbp[6] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x, y + 1).R ? 0 : 1;
                    lbp[7] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x + 1, y + 1).R ? 0 : 1;
                    int lbpColor = getMinimumLBP(lbp);
                    Color color = Color.FromArgb(lbpColor, lbpColor, lbpColor);
                    bmpLbp.SetPixel(x, y, color);
                    binPixel1[lbpColor]++;
                }
            }
            pictureBox7.Image = bmpLbp;
            //Set Histogram
            this.chart5.Series["pixel"].Points.Clear();
            for (int i = 0; i <= 255; i++)
            {
                this.chart5.Series["pixel"].Points.AddXY(i, binPixel1[i]);
                pixel[0, i] = binPixel1[i];
            }
        }

        int[] value = new int[572];
        int[] values = new int[573];

        private void button25_Click(object sender, EventArgs e)
        {
            //2
            int[] binRow = new int[100];
            int[] binColumn = new int[100];
            Bitmap objBitmap2 = (Bitmap)pictureBox10.Image;
            //Gray Scale
            for (int x = 0; x < objBitmap2.Height; x++)
            {
                for (int y = 0; y < objBitmap2.Width; y++)
                {
                    Color w = objBitmap2.GetPixel(x, y);
                    int red = w.R;
                    int green = w.G;
                    int blue = w.B;
                    int gray = (red + green + blue) / 3;
                    Color color = Color.FromArgb(gray, gray, gray);
                    objBitmap2.SetPixel(x, y, color);
                }
            }
            //Metode Sobel
            Bitmap objBitmap1 = new Bitmap(objBitmap2);
            for (int x = 1; x < objBitmap2.Width - 1; x++)
                for (int y = 1; y < objBitmap2.Height - 1; y++)
                {
                    Color w1 = objBitmap2.GetPixel(x - 1, y - 1);
                    Color w2 = objBitmap2.GetPixel(x - 1, y);
                    Color w3 = objBitmap2.GetPixel(x - 1, y + 1);
                    Color w4 = objBitmap2.GetPixel(x, y - 1);
                    Color w5 = objBitmap2.GetPixel(x, y);
                    Color w6 = objBitmap2.GetPixel(x, y + 1);
                    Color w7 = objBitmap2.GetPixel(x + 1, y - 1);
                    Color w8 = objBitmap2.GetPixel(x + 1, y);
                    Color w9 = objBitmap2.GetPixel(x + 1, y + 1);
                    int x1 = w1.R;
                    int x2 = w2.R;
                    int x3 = w3.R;
                    int x4 = w4.R;
                    int x5 = w5.R;
                    int x6 = w6.R;
                    int x7 = w7.R;
                    int x8 = w8.R;
                    int x9 = w9.R;
                    int xh = (int)(-x1 - 2 * x4 - x7 + x3 + 2 * x6 + x9);
                    int xv = (int)(-x1 - 2 * x2 - x3 + x7 + 2 * x8 + x9);
                    int xb = (int)(xh + xv);
                    if (xb < 0) xb = -xb;
                    if (xb > 255) xb = 255;
                    Color wb = Color.FromArgb(xb, xb, xb);
                    objBitmap1.SetPixel(x, y, wb);
                }
            //Gambar Proyeksi
            Bitmap objProyeksi2 = new Bitmap(objBitmap1);
            for (int i = 0; i < 25; i++)
                for (int j = 0; j < 25; j++)
                {
                    int jumlah_putih = 0;
                    for (int x = (i * 4); x < ((i + 1) * 4); x++)
                    {
                        for (int y = (j * 4); y < ((j + 1) * 4); y++)
                        {
                            Color col = objBitmap1.GetPixel(x, y);
                            int r = col.R;
                            int g = col.G;
                            int b = col.B;
                            int c = (r + g + b) / 3;
                            if (c > 127)
                                jumlah_putih++;
                        }
                    }
                    if (jumlah_putih >= 4)
                    {
                        for (int x = (i * 4); x < ((i + 1) * 4); x++)
                        {
                            for (int y = (j * 4); y < ((j + 1) * 4); y++)
                            {
                                Color col = Color.FromArgb(0, 0, 0);
                                objProyeksi2.SetPixel(x, y, col);
                            }
                        }
                        binRow[i]++; binColumn[j]++;
                    }
                    else
                    {
                        for (int x = (i * 4); x < ((i + 1) * 4); x++)
                        {
                            for (int y = (j * 4); y < ((j + 1) * 4); y++)
                            {
                                Color col = Color.FromArgb(255, 255, 255);
                                objProyeksi2.SetPixel(x, y, col);
                            }
                        }
                    }
                }
            for (int i = 1; i <= 25; i++)
            {
                this.chart9.Series["Row"].Points.AddXY(i, binRow[i - 1]);
                this.chart9.Series["Column"].Points.AddXY(i, binColumn[i - 1]);
            }
            pictureBox10.Image = objProyeksi2;
            for (int i = 0; i < 25; i++)
            {
                jumlahBaris[1, i] = binRow[i];
                jumlahKolom[1, i] = binColumn[i];
            }
        }

        private void button43_Click(object sender, EventArgs e)
        {
            double jarak1 = 0, jarak2 = 0, jarak3 = 0;
            jarak1 = jarak2 = jarak3 = 0;
            for (int i = 0; i < 256; i++)
            {
                jarak1 += Math.Sqrt((pixel[0, i] - pixel[3, i]) * (pixel[0, i] - pixel[3, i]));
                jarak2 += Math.Sqrt((pixel[1, i] - pixel[3, i]) * (pixel[1, i] - pixel[3, i]));
                jarak3 += Math.Sqrt((pixel[2, i] - pixel[3, i]) * (pixel[2, i] - pixel[3, i]));
            }
            double min = 9999999999999;
            if (min > jarak1) min = jarak1;
            if (min > jarak2) min = jarak2;
            if (min > jarak3) min = jarak3;
            if (min == jarak1)
            {
                label13.Text = "HASIL : Data mirip dengan gambar 1";
                pictureBox16.Image = obj_fitur1;
            }
            else if (min == jarak2)
            {
                label13.Text = "HASIL : Data mirip dengan gambar 2";
                pictureBox16.Image = obj_fitur2;
            }
            else if (min == jarak3)
            {
                label13.Text = "HASIL : Data mirip dengan gambar 3";
                pictureBox16.Image = obj_fitur3;
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            int[] binPixel1 = new int[256];
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                objBitmap = new Bitmap(openFileDialog1.FileName);
                objBitmap = new Bitmap(objBitmap, new Size(100, 100));
                obj_fitur4 = new Bitmap(openFileDialog1.FileName);
                obj_fitur4 = new Bitmap(obj_fitur4, new Size(100, 100));
                pictureBox17.Image = obj_fitur4;
            }
            //Grayscale
            Bitmap bmpGrey = objBitmap;
            for (int y = 0; y < objBitmap.Height; y++)
            {
                for (int x = 0; x < objBitmap.Width; x++)
                {
                    Color color = objBitmap.GetPixel(x, y);
                    int red = color.R;
                    int green = color.G;
                    int blue = color.B;
                    int grey = (int)(red + green + blue) / 3;
                    color = Color.FromArgb(grey, grey, grey);
                    bmpGrey.SetPixel(x, y, color);
                }
            }
            //LBP
            Bitmap bmpLbp = bmpGrey;
            for (int y = 1; y < bmpGrey.Height - 1; y++)
            {
                for (int x = 1; x < bmpGrey.Width - 1; x++)
                {
                    int[] lbp = new int[8];
                    lbp[0] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x - 1, y - 1).R ? 0 : 1;
                    lbp[1] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x, y - 1).R ? 0 : 1;
                    lbp[2] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x + 1, y - 1).R ? 0 : 1;
                    lbp[3] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x - 1, y).R ? 0 : 1;
                    lbp[4] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x + 1, y).R ? 0 : 1;
                    lbp[5] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x - 1, y + 1).R ? 0 : 1;
                    lbp[6] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x, y + 1).R ? 0 : 1;
                    lbp[7] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x + 1, y + 1).R ? 0 : 1;
                    int lbpColor = getMinimumLBP(lbp);
                    Color color = Color.FromArgb(lbpColor, lbpColor, lbpColor);
                    bmpLbp.SetPixel(x, y, color);
                    binPixel1[lbpColor]++;
                }
            }
            pictureBox17.Image = bmpLbp;
            //Set Histogram
            this.chart12.Series["pixel"].Points.Clear();
            for (int i = 0; i <= 255; i++)
            {
                this.chart12.Series["pixel"].Points.AddXY(i, binPixel1[i]);
                pixel[0, i] = binPixel1[i];
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            //3
            int[] binRow = new int[100];
            int[] binColumn = new int[100];
            Bitmap objBitmap = (Bitmap)pictureBox9.Image;
            //Gray Scale
            for (int x = 0; x < objBitmap.Height; x++)
            {
                for (int y = 0; y < objBitmap.Width; y++)
                {
                    Color w = objBitmap.GetPixel(x, y);
                    int red = w.R;
                    int green = w.G;
                    int blue = w.B;
                    int gray = (red + green + blue) / 3;
                    Color color = Color.FromArgb(gray, gray, gray);
                    objBitmap.SetPixel(x, y, color);
                }
            }
            //Metode Sobel
            Bitmap objBitmap1 = new Bitmap(objBitmap);
            for (int x = 1; x < objBitmap.Width - 1; x++)
                for (int y = 1; y < objBitmap.Height - 1; y++)
                {
                    Color w1 = objBitmap.GetPixel(x - 1, y - 1);
                    Color w2 = objBitmap.GetPixel(x - 1, y);
                    Color w3 = objBitmap.GetPixel(x - 1, y + 1);
                    Color w4 = objBitmap.GetPixel(x, y - 1);
                    Color w5 = objBitmap.GetPixel(x, y);
                    Color w6 = objBitmap.GetPixel(x, y + 1);
                    Color w7 = objBitmap.GetPixel(x + 1, y - 1);
                    Color w8 = objBitmap.GetPixel(x + 1, y);
                    Color w9 = objBitmap.GetPixel(x + 1, y + 1);
                    int x1 = w1.R;
                    int x2 = w2.R;
                    int x3 = w3.R;
                    int x4 = w4.R;
                    int x5 = w5.R;
                    int x6 = w6.R;
                    int x7 = w7.R;
                    int x8 = w8.R;
                    int x9 = w9.R;
                    int xh = (int)(-x1 - 2 * x4 - x7 + x3 + 2 * x6 + x9);
                    int xv = (int)(-x1 - 2 * x2 - x3 + x7 + 2 * x8 + x9);
                    int xb = (int)(xh + xv);
                    if (xb < 0) xb = -xb;
                    if (xb > 255) xb = 255;
                    Color wb = Color.FromArgb(xb, xb, xb);
                    objBitmap1.SetPixel(x, y, wb);
                }
            //Gambar Proyeksi
            Bitmap objBitmap2 = new Bitmap(objBitmap1);
            for (int i = 0; i < 25; i++)
                for (int j = 0; j < 25; j++)
                {
                    int jumlah_putih = 0;
                    for (int x = (i * 4); x < ((i + 1) * 4); x++)
                    {
                        for (int y = (j * 4); y < ((j + 1) * 4); y++)
                        {
                            Color col = objBitmap1.GetPixel(x, y);
                            int r = col.R;
                            int g = col.G;
                            int b = col.B;
                            int c = (r + g + b) / 3;
                            if (c > 127)
                                jumlah_putih++;
                        }
                    }
                    if (jumlah_putih >= 4)
                    {
                        for (int x = (i * 4); x < ((i + 1) * 4); x++)
                        {
                            for (int y = (j * 4); y < ((j + 1) * 4); y++)
                            {
                                Color col = Color.FromArgb(0, 0, 0);
                                objBitmap2.SetPixel(x, y, col);
                            }
                        }
                        binRow[i]++; binColumn[j]++;
                    }
                    else
                    {
                        for (int x = (i * 4); x < ((i + 1) * 4); x++)
                        {
                            for (int y = (j * 4); y < ((j + 1) * 4); y++)
                            {
                                Color col = Color.FromArgb(255, 255, 255);
                                objBitmap2.SetPixel(x, y, col);
                            }
                        }
                    }
                }
            for (int i = 1; i <= 25; i++)
            {
                this.chart8.Series["Row"].Points.AddXY(i, binRow[i - 1]);
                this.chart8.Series["Column"].Points.AddXY(i, binColumn[i - 1]);
            }
            pictureBox9.Image = objBitmap2;
            for (int i = 0; i < 25; i++)
            {
                jumlahBaris[2, i] = binRow[i];
                jumlahKolom[2, i] = binColumn[i];
            }
        }

        private void button30_Click_1(object sender, EventArgs e)
        {
            //3
            int[] binRow = new int[100];
            int[] binColumn = new int[100];
            Bitmap objBitmap = (Bitmap)pictureBox14.Image;
            //Gray Scale
            for (int x = 0; x < objBitmap.Height; x++)
            {
                for (int y = 0; y < objBitmap.Width; y++)
                {
                    Color w = objBitmap.GetPixel(x, y);
                    int red = w.R;
                    int green = w.G;
                    int blue = w.B;
                    int gray = (red + green + blue) / 3;
                    Color color = Color.FromArgb(gray, gray, gray);
                    objBitmap.SetPixel(x, y, color);
                }
            }
            //Metode Sobel
            Bitmap objBitmap1 = new Bitmap(objBitmap);
            for (int x = 1; x < objBitmap.Width - 1; x++)
                for (int y = 1; y < objBitmap.Height - 1; y++)
                {
                    Color w1 = objBitmap.GetPixel(x - 1, y - 1);
                    Color w2 = objBitmap.GetPixel(x - 1, y);
                    Color w3 = objBitmap.GetPixel(x - 1, y + 1);
                    Color w4 = objBitmap.GetPixel(x, y - 1);
                    Color w5 = objBitmap.GetPixel(x, y);
                    Color w6 = objBitmap.GetPixel(x, y + 1);
                    Color w7 = objBitmap.GetPixel(x + 1, y - 1);
                    Color w8 = objBitmap.GetPixel(x + 1, y);
                    Color w9 = objBitmap.GetPixel(x + 1, y + 1);
                    int x1 = w1.R;
                    int x2 = w2.R;
                    int x3 = w3.R;
                    int x4 = w4.R;
                    int x5 = w5.R;
                    int x6 = w6.R;
                    int x7 = w7.R;
                    int x8 = w8.R;
                    int x9 = w9.R;
                    int xh = (int)(-x1 - 2 * x4 - x7 + x3 + 2 * x6 + x9);
                    int xv = (int)(-x1 - 2 * x2 - x3 + x7 + 2 * x8 + x9);
                    int xb = (int)(xh + xv);
                    if (xb < 0) xb = -xb;
                    if (xb > 255) xb = 255;
                    Color wb = Color.FromArgb(xb, xb, xb);
                    objBitmap1.SetPixel(x, y, wb);
                }
            //Gambar Proyeksi
            Bitmap objBitmap2 = new Bitmap(objBitmap1);
            for (int i = 0; i < 25; i++)
                for (int j = 0; j < 25; j++)
                {
                    int jumlah_putih = 0;
                    for (int x = (i * 4); x < ((i + 1) * 4); x++)
                    {
                        for (int y = (j * 4); y < ((j + 1) * 4); y++)
                        {
                            Color col = objBitmap1.GetPixel(x, y);
                            int r = col.R;
                            int g = col.G;
                            int b = col.B;
                            int c = (r + g + b) / 3;
                            if (c > 127)
                                jumlah_putih++;
                        }
                    }
                    if (jumlah_putih >= 4)
                    {
                        for (int x = (i * 4); x < ((i + 1) * 4); x++)
                        {
                            for (int y = (j * 4); y < ((j + 1) * 4); y++)
                            {
                                Color col = Color.FromArgb(0, 0, 0);
                                objBitmap2.SetPixel(x, y, col);
                            }
                        }
                        binRow[i]++; binColumn[j]++;
                    }
                    else
                    {
                        for (int x = (i * 4); x < ((i + 1) * 4); x++)
                        {
                            for (int y = (j * 4); y < ((j + 1) * 4); y++)
                            {
                                Color col = Color.FromArgb(255, 255, 255);
                                objBitmap2.SetPixel(x, y, col);
                            }
                        }
                    }
                }
            for (int i = 1; i <= 25; i++)
            {
                this.chart11.Series["Row"].Points.AddXY(i, binRow[i - 1]);
                this.chart11.Series["Column"].Points.AddXY(i, binColumn[i - 1]);
            }
            pictureBox14.Image = objBitmap2;
            for (int i = 0; i < 25; i++)
            {
                jumlahBaris[3, i] = binRow[i];
                jumlahKolom[3, i] = binColumn[i];
            }
        }

        int[] index = new int[572];

        private void button24_Click(object sender, EventArgs e)
        {
            int[] binPixel1 = new int[256];
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                objBitmap = new Bitmap(openFileDialog1.FileName);
                objBitmap = new Bitmap(objBitmap, new Size(100, 100));
                obj_fitur3 = new Bitmap(openFileDialog1.FileName);
                obj_fitur3 = new Bitmap(obj_fitur3, new Size(100, 100));
                pictureBox8.Image = obj_fitur3;
            }
            //Grayscale
            Bitmap bmpGrey = objBitmap;
            for (int y = 0; y < objBitmap.Height; y++)
            {
                for (int x = 0; x < objBitmap.Width; x++)
                {
                    Color color = objBitmap.GetPixel(x, y);
                    int red = color.R;
                    int green = color.G;
                    int blue = color.B;
                    int grey = (int)(red + green + blue) / 3;
                    color = Color.FromArgb(grey, grey, grey);
                    bmpGrey.SetPixel(x, y, color);
                }
            }
            //LBP
            Bitmap bmpLbp = bmpGrey;
            for (int y = 1; y < bmpGrey.Height - 1; y++)
            {
                for (int x = 1; x < bmpGrey.Width - 1; x++)
                {
                    int[] lbp = new int[8];
                    lbp[0] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x - 1, y - 1).R ? 0 : 1;
                    lbp[1] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x, y - 1).R ? 0 : 1;
                    lbp[2] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x + 1, y - 1).R ? 0 : 1;
                    lbp[3] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x - 1, y).R ? 0 : 1;
                    lbp[4] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x + 1, y).R ? 0 : 1;
                    lbp[5] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x - 1, y + 1).R ? 0 : 1;
                    lbp[6] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x, y + 1).R ? 0 : 1;
                    lbp[7] = bmpGrey.GetPixel(x, y).R > bmpGrey.GetPixel(x + 1, y + 1).R ? 0 : 1;
                    int lbpColor = getMinimumLBP(lbp);
                    Color color = Color.FromArgb(lbpColor, lbpColor, lbpColor);
                    bmpLbp.SetPixel(x, y, color);
                    binPixel1[lbpColor]++;
                }
            }
            pictureBox8.Image = bmpLbp;
            //Set Histogram
            this.chart6.Series["pixel"].Points.Clear();
            for (int i = 0; i <= 255; i++)
            {
                this.chart6.Series["pixel"].Points.AddXY(i, binPixel1[i]);
                pixel[0, i] = binPixel1[i];
            }
        }


        private void button34_Click(object sender, EventArgs e)
        {
            Bitmap HistoAsal = (Bitmap)pictureBox12.Image;
            Color warna;

            for (int y = 0; y < HistoAsal.Height; y++)
            {
                for (int x = 0; x < HistoAsal.Width; x++)
                {
                    warna = HistoAsal.GetPixel(x, y);
                    int merah = warna.R;
                    int hijau = warna.G;
                    int biru = warna.B;
                    int indexR4 = merah / 16;
                    int indexG4 = hijau / 16;
                    int indexB4 = biru / 16;

                    GrMerah4[indexR4]++;
                    GrHijau4[indexG4]++;
                    GrBiru4[indexB4]++;
                }
            }
            
            for (int i = 0; i < 16; i++)
            {
                this.chart10.Series["Red"].Points.AddXY(i, GrMerah4[i]);
                this.chart10.Series["Green"].Points.AddXY(16 + i, GrHijau4[i]);
                this.chart10.Series["Blue"].Points.AddXY(32 + i, GrBiru4[i]);
            }
        }

        private int Compare(int[] rx, int[] r4, int[] gx, int[] g4, int[] bx, int[] b4)
        {
            int value = 0;
            int jumr = 0, jumg = 0, jumb = 0;
            for (int i = 0; i < 16; i++)
            {
                jumr += Math.Abs(rx[i] - r4[i]);
                jumg += Math.Abs(gx[i] - g4[i]);
                jumb += Math.Abs(bx[i] - b4[i]);
            }

            value = (jumr + jumg + jumb);
            return value;
        }

        private void button36_Click(object sender, EventArgs e)
        {
            int jarak1 = Compare(GrMerah1, GrMerah4, GrHijau1, GrHijau4, GrBiru1, GrBiru4);
            int jarak2 = Compare(GrMerah2, GrMerah4, GrHijau2, GrHijau4, GrBiru2, GrBiru4);
            int jarak3 = Compare(GrMerah3, GrMerah4, GrHijau3, GrHijau4, GrBiru3, GrBiru4);

            textBox8.Text = jarak1.ToString();
            textBox11.Text = jarak2.ToString();
            textBox12.Text = jarak3.ToString();
            if (jarak1 < jarak2 && jarak1 < jarak3)
            {
                label10.Text = "Data mirip dengan gambar 1";
                pictureBox13.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox13.Image = obj_hisori;
            }
            else if (jarak2 < jarak1 && jarak2 < jarak3)
            {
                label10.Text = "Data mirip dengan gambar 2";
                pictureBox13.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox13.Image = warna_1;
            }
            else if (jarak3 < jarak1 && jarak3 < jarak2)
            {
                label10.Text = "Data mirip dengan gambar 3";
                pictureBox13.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox13.Image = warna_2;
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                warna_3 = new Bitmap(openFileDialog1.FileName);
                warna_3 = new Bitmap(warna_3, new Size(100, 100));
                pictureBox12.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox12.Image = warna_3;
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            obj_kuantisasi1 = new Bitmap(obj_hisori);
            obj_kuantisasi2 = new Bitmap(warna_1);
            obj_kuantisasi3 = new Bitmap(warna_2);
            int k = 16;
            int th = (int)256 / k;
            for (int x = 0; x < obj_hisori.Width; x++)
            {
                for (int y = 0; y < obj_hisori.Height; y++)
                {
                    Color pixelColor = obj_hisori.GetPixel(x, y);
                    int red = pixelColor.R;
                    int green = pixelColor.G;
                    int blue = pixelColor.B;
                    int rata = (int)((red + green + blue) / 3);
                    int kuanti4red = (int)(red / th);
                    int kuanti4green = (int)(green / th);
                    int kuanti4blue = (int)(blue / th);
                    int resultred = (int)th * kuanti4red;
                    int resultgreen = (int)th * kuanti4green;
                    int resultblue = (int)th * kuanti4blue;
                    obj_kuantisasi1.SetPixel(x, y, Color.FromArgb(resultred, resultgreen, resultblue));
                }
            }
            for (int x = 0; x < warna_1.Width; x++)
            {
                for (int y = 0; y < warna_1.Height; y++)
                {
                    Color pixelColor = warna_1.GetPixel(x, y);
                    int red = pixelColor.R;
                    int green = pixelColor.G;
                    int blue = pixelColor.B;
                    int rata = (int)((red + green + blue) / 3);
                    int kuanti4red = (int)(red / th);
                    int kuanti4green = (int)(green / th);
                    int kuanti4blue = (int)(blue / th);
                    int resultred = (int)th * kuanti4red;
                    int resultgreen = (int)th * kuanti4green;
                    int resultblue = (int)th * kuanti4blue;
                    obj_kuantisasi2.SetPixel(x, y, Color.FromArgb(resultred, resultgreen, resultblue));
                }
            }
            for (int x = 0; x < warna_2.Width; x++)
            {
                for (int y = 0; y < warna_2.Height; y++)
                {
                    Color pixelColor = warna_2.GetPixel(x, y);
                    int red = pixelColor.R;
                    int green = pixelColor.G;
                    int blue = pixelColor.B;
                    int rata = (int)((red + green + blue) / 3);
                    int kuanti4red = (int)(red / th);
                    int kuanti4green = (int)(green / th);
                    int kuanti4blue = (int)(blue / th);
                    int resultred = (int)th * kuanti4red;
                    int resultgreen = (int)th * kuanti4green;
                    int resultblue = (int)th * kuanti4blue;
                    obj_kuantisasi3.SetPixel(x, y, Color.FromArgb(resultred, resultgreen, resultblue));
                }
            }
            pictureHisto.SizeMode = PictureBoxSizeMode.Zoom;
            pictureHisto.Image = obj_kuantisasi1;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.Image = obj_kuantisasi2;
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.Image = obj_kuantisasi3;
        }

        private void button31_Click(object sender, EventArgs e)
        {
            //1
            int[] binRow = new int[100];
            int[] binColumn = new int[100];
            Bitmap objBitmap = (Bitmap)pictureBox11.Image;
            //Gray Scale
            for (int x = 0; x < objBitmap.Height; x++)
            {
                for (int y = 0; y < objBitmap.Width; y++)
                {
                    Color w = objBitmap.GetPixel(x, y);
                    int red = w.R;
                    int green = w.G;
                    int blue = w.B;
                    int gray = (red + green + blue) / 3;
                    Color color = Color.FromArgb(gray, gray, gray);
                    objBitmap.SetPixel(x, y, color);
                }
            }
            //Metode Sobel
            Bitmap objBitmap1 = new Bitmap(objBitmap);
            for (int x = 1; x < objBitmap.Width - 1; x++)
                for (int y = 1; y < objBitmap.Height - 1; y++)
                {
                    Color w1 = objBitmap.GetPixel(x - 1, y - 1);
                    Color w2 = objBitmap.GetPixel(x - 1, y);
                    Color w3 = objBitmap.GetPixel(x - 1, y + 1);
                    Color w4 = objBitmap.GetPixel(x, y - 1);
                    Color w5 = objBitmap.GetPixel(x, y);
                    Color w6 = objBitmap.GetPixel(x, y + 1);
                    Color w7 = objBitmap.GetPixel(x + 1, y - 1);
                    Color w8 = objBitmap.GetPixel(x + 1, y);
                    Color w9 = objBitmap.GetPixel(x + 1, y + 1);
                    int x1 = w1.R;
                    int x2 = w2.R;
                    int x3 = w3.R;
                    int x4 = w4.R;
                    int x5 = w5.R;
                    int x6 = w6.R;
                    int x7 = w7.R;
                    int x8 = w8.R;
                    int x9 = w9.R;
                    int xh = (int)(-x1 - 2 * x4 - x7 + x3 + 2 * x6 + x9);
                    int xv = (int)(-x1 - 2 * x2 - x3 + x7 + 2 * x8 + x9);
                    int xb = (int)(xh + xv);
                    if (xb < 0) xb = -xb;
                    if (xb > 255) xb = 255;
                    Color wb = Color.FromArgb(xb, xb, xb);
                    objBitmap1.SetPixel(x, y, wb);
                }
            //Gambar Proyeksi
            Bitmap objBitmap2 = new Bitmap(objBitmap1);
            for (int i = 0; i < 25; i++)
                for (int j = 0; j < 25; j++)
                {
                    int jumlah_putih = 0;
                    for (int x = (i * 4); x < ((i + 1) * 4); x++)
                    {
                        for (int y = (j * 4); y < ((j + 1) * 4); y++)
                        {
                            Color col = objBitmap1.GetPixel(x, y);
                            int r = col.R;
                            int g = col.G;
                            int b = col.B;
                            int c = (r + g + b) / 3;
                            if (c > 127)
                                jumlah_putih++;
                        }
                    }
                    if (jumlah_putih >= 4)
                    {
                        for (int x = (i * 4); x < ((i + 1) * 4); x++)
                        {
                            for (int y = (j * 4); y < ((j + 1) * 4); y++)
                            {
                                Color col = Color.FromArgb(0,0,0);
                                objBitmap2.SetPixel(x, y, col);
                            }
                        }
                        binRow[i]++; binColumn[j]++;
                    }
                    else
                    {
                        for (int x = (i * 4); x < ((i + 1) * 4); x++)
                        {
                            for (int y = (j * 4); y < ((j + 1) * 4); y++)
                            {
                                Color col = Color.FromArgb(255, 255, 255);
                                objBitmap2.SetPixel(x, y, col);
                            }
                        }
                    }
                }
            for (int i = 1; i <= 25; i++)
            {
                this.chart7.Series["Row"].Points.AddXY(i, binRow[i - 1]);
                this.chart7.Series["Column"].Points.AddXY(i, binColumn[i - 1]);
            }
            pictureBox11.Image = objBitmap2;
            for (int i = 0; i < 25; i++)
            {
                jumlahBaris[0, i] = binRow[i];
                jumlahKolom[0, i] = binColumn[i];
            }
        }

       

        private void button28_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                obj_bentuk2 = new Bitmap(openFileDialog1.FileName);
                obj_bentuk2 = new Bitmap(obj_bentuk2, new Size(100, 100));
                pictureBox10.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox10.Image = obj_bentuk2;
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            obj_grayscale1 = new Bitmap(obj_bentuk1);
            obj_grayscale2= new Bitmap(obj_bentuk2);
            obj_grayscale3 = new Bitmap(obj_bentuk3);
            obj_grayscale4 = new Bitmap(obj_bentuk4);
            for (int x = 0; x < obj_bentuk1.Width; x++)
            {
                for (int y = 0; y < obj_bentuk1.Height; y++)
                {
                    Color pixelColor = obj_bentuk1.GetPixel(x, y);
                    int red = pixelColor.R;
                    int green = pixelColor.G;
                    int blue = pixelColor.B;
                    int rata = (int)(red + green + blue) / 3;
                    obj_grayscale1.SetPixel(x, y, Color.FromArgb(rata, rata, rata));
                }
            }
            for (int x = 0; x < obj_bentuk2.Width; x++)
            {
                for (int y = 0; y < obj_bentuk2.Height; y++)
                {
                    Color pixelColor = obj_bentuk2.GetPixel(x, y);
                    int red = pixelColor.R;
                    int green = pixelColor.G;
                    int blue = pixelColor.B;
                    int rata = (int)(red + green + blue) / 3;
                    obj_grayscale2.SetPixel(x, y, Color.FromArgb(rata, rata, rata));
                }
            }
            for (int x = 0; x < obj_bentuk3.Width; x++)
            {
                for (int y = 0; y < obj_bentuk3.Height; y++)
                {
                    Color pixelColor = obj_bentuk3.GetPixel(x, y);
                    int red = pixelColor.R;
                    int green = pixelColor.G;
                    int blue = pixelColor.B;
                    int rata = (int)(red + green + blue) / 3;
                    obj_grayscale3.SetPixel(x, y, Color.FromArgb(rata, rata, rata));
                }
            }
            for (int x = 0; x < obj_bentuk4.Width; x++)
            {
                for (int y = 0; y < obj_bentuk4.Height; y++)
                {
                    Color pixelColor = obj_bentuk4.GetPixel(x, y);
                    int red = pixelColor.R;
                    int green = pixelColor.G;
                    int blue = pixelColor.B;
                    int rata = (int)(red + green + blue) / 3;
                    obj_grayscale4.SetPixel(x, y, Color.FromArgb(rata, rata, rata));
                }
            }
            pictureBox14.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox14.Image = obj_grayscale4;
            pictureBox11.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox11.Image = obj_grayscale1;
            pictureBox10.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox10.Image = obj_grayscale2;
            pictureBox9.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox9.Image = obj_grayscale3;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                obj_bentuk3 = new Bitmap(openFileDialog1.FileName);
                obj_bentuk3 = new Bitmap(obj_bentuk3, new Size(100, 100));
                pictureBox9.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox9.Image = obj_bentuk3;
            }
        }
        

        private void button29_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                obj_bentuk1 = new Bitmap(openFileDialog1.FileName);
                obj_bentuk1 = new Bitmap(obj_bentuk1, new Size(100, 100));
                pictureBox11.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox11.Image = obj_bentuk1;
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Bitmap HistoAsal = (Bitmap)pictureBox5.Image;
            Color warna;

            for (int y = 0; y < HistoAsal.Height; y++)
            {
                for (int x = 0; x < HistoAsal.Width; x++)
                {
                    warna = HistoAsal.GetPixel(x, y);
                    int merah = warna.R;
                    int hijau = warna.G;
                    int biru = warna.B;
                    int indexR3 = merah / 16;
                    int indexG3 = hijau / 16;
                    int indexB3 = biru / 16;

                    GrMerah3[indexR3]++;
                    GrHijau3[indexG3]++;
                    GrBiru3[indexB3]++;
                }
            }
            
            for (int i = 0; i < 16; i++)
            {
                this.chart1.Series["Red"].Points.AddXY(i, GrMerah3[i]);
                this.chart1.Series["Green"].Points.AddXY(16 + i, GrHijau3[i]);
                this.chart1.Series["Blue"].Points.AddXY(32 + i, GrBiru3[i]);
            }
        }


        private void button20_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                warna_2 = new Bitmap(openFileDialog1.FileName);
                warna_2 = new Bitmap(warna_2, new Size(100, 100));
                pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox5.Image = warna_2;
            }
        }

        private void btnPrewitt_Click(object sender, EventArgs e)
        {
            obj_detto = new Bitmap(obj_detori);
            for (int x = 1; x < obj_detori.Width - 1; x++)
                for (int y = 1; y < obj_detori.Height - 1; y++)
                {
                    Color w1 = obj_detori.GetPixel(x - 1, y - 1);
                    Color w2 = obj_detori.GetPixel(x - 1, y);
                    Color w3 = obj_detori.GetPixel(x - 1, y + 1);
                    Color w4 = obj_detori.GetPixel(x, y - 1);
                    Color w5 = obj_detori.GetPixel(x, y);
                    Color w6 = obj_detori.GetPixel(x, y + 1);
                    Color w7 = obj_detori.GetPixel(x + 1, y - 1);
                    Color w8 = obj_detori.GetPixel(x + 1, y);
                    Color w9 = obj_detori.GetPixel(x + 1, y + 1);
                    int rh = (int)(-w1.R - w4.R - w7.R + w3.R + w6.R + w9.R);
                    int gh = (int)(-w1.G - w4.G - w7.G + w3.G + w6.G + w9.G);
                    int bh = (int)(-w1.B - w4.B - w7.B + w3.B + w6.B + w9.B);
                    int rv = (int)(-w1.R - w2.R - w3.R + w7.R + w8.R + w9.R);
                    int gv = (int)(-w1.G - w2.G - w3.G + w7.G + w8.G + w9.G);
                    int bv = (int)(-w1.B - w2.B - w3.B + w7.B + w8.B + w9.B);
                    int r = (int)(rh + rv);
                    if (r < 0) r = -r;
                    if (r > 255) r = 255;
                    int g = (int)(gh + gv);
                    if (g < 0) g = -g;
                    if (g > 255) g = 255;
                    int b = (int)(bh + bv);
                    if (b < 0) b = -b;
                    if (b > 255) b = 255;
                    Color wb = Color.FromArgb(r, g, b);
                    obj_detto.SetPixel(x, y, wb);
                }
            pictureBoxto.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxto.Image = obj_detto;
        }

        private void btnSobel_Click(object sender, EventArgs e)
        {
            obj_detto = new Bitmap(obj_detori);
            for (int x = 1; x < obj_detori.Width - 1; x++)
                for (int y = 1; y < obj_detori.Height - 1; y++)
                {
                    Color w1 = obj_detori.GetPixel(x - 1, y - 1);
                    Color w2 = obj_detori.GetPixel(x - 1, y);
                    Color w3 = obj_detori.GetPixel(x - 1, y + 1);
                    Color w4 = obj_detori.GetPixel(x, y - 1);
                    Color w5 = obj_detori.GetPixel(x, y);
                    Color w6 = obj_detori.GetPixel(x, y + 1);
                    Color w7 = obj_detori.GetPixel(x + 1, y - 1);
                    Color w8 = obj_detori.GetPixel(x + 1, y);
                    Color w9 = obj_detori.GetPixel(x + 1, y + 1);
                    int rh = (int)(-w1.R - 2 * w4.R - w7.R + w3.R + 2 * w6.R + w9.R);
                    int gh = (int)(-w1.G - 2 * w4.G - w7.G + w3.G + 2 * w6.G + w9.G);
                    int bh = (int)(-w1.B - 2 * w4.B - w7.B + w3.B + 2 * w6.B + w9.B);
                    int rv = (int)(-w1.R - 2 * w2.R - w3.R + w7.R + 2 * w8.R + w9.R);
                    int gv = (int)(-w1.G - 2 * w2.G - w3.G + w7.G + 2 * w8.G + w9.G);
                    int bv = (int)(-w1.B - 2 * w2.B - w3.B + w7.B + 2 * w8.B + w9.B);
                    int r = (int)(rh + rv);
                    if (r < 0) r = -r;
                    if (r > 255) r = 255;
                    int g = (int)(gh + gv);
                    if (g < 0) g = -g;
                    if (g > 255) g = 255;
                    int b = (int)(bh + bv);
                    if (b < 0) b = -b;
                    if (b > 255) b = 255;
                    Color wb = Color.FromArgb(r, g, b);
                    obj_detto.SetPixel(x, y, wb);
                }
            pictureBoxto.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxto.Image = obj_detto;
        }

        private void btnLaplacian_Click(object sender, EventArgs e)
        {
            obj_detto = new Bitmap(obj_detori);
            for (int x = 1; x < obj_detori.Width - 1; x++)
                for (int y = 1; y < obj_detori.Height - 1; y++)
                {
                    Color w1 = obj_detori.GetPixel(x - 1, y - 1);
                    Color w2 = obj_detori.GetPixel(x - 1, y);
                    Color w3 = obj_detori.GetPixel(x - 1, y + 1);
                    Color w4 = obj_detori.GetPixel(x, y - 1);
                    Color w5 = obj_detori.GetPixel(x, y);
                    Color w6 = obj_detori.GetPixel(x, y + 1);
                    Color w7 = obj_detori.GetPixel(x + 1, y - 1);
                    Color w8 = obj_detori.GetPixel(x + 1, y);
                    Color w9 = obj_detori.GetPixel(x + 1, y + 1);
                    int r = (int)((w1.R -2 * w2.R + w3.R - 2 * w4.R + 4*w5.R
                        -2* w6.R + w7.R -2* w8.R + w9.R));
                    int g = (int)((w1.G - 2 * w2.G + w3.G - 2 * w4.G + 4*w5.G
                        -2*w6.G + w7.G -2*w8.G + w9.G));
                    int b = (int)((w1.B - 2 * w2.B + w3.B - 2 * w4.B + 4*w5.B
                        -2*w6.B + w7.B -2* w8.B + w9.B));
                    if (r < 0) r = -r;
                    if (r > 255) r = 255;
                    if (g < 0) g = -g;
                    if (g > 255) g = 255;
                    if (b < 0) b = -b;
                    if (b > 255) b = 255;
                    Color wb = Color.FromArgb(r, g, b);
                    obj_detto.SetPixel(x, y, wb);
                }
            pictureBoxto.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxto.Image = obj_detto;
        }

        private void button16_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                obj_sharpori = new Bitmap(openFileDialog1.FileName);
                pictureOriSharp.SizeMode = PictureBoxSizeMode.Zoom;
                pictureOriSharp.Image = obj_sharpori;
            }
        }

        private void btnSharp_Click(object sender, EventArgs e)
        {
            obj_sharpto = new Bitmap(obj_sharpori);
            for (int x = 1; x < obj_sharpori.Width - 1; x++)
                for (int y = 1; y < obj_sharpori.Height - 1; y++)
                {
                    Color w = obj_sharpori.GetPixel(x, y);
                    Color w1 = obj_sharpori.GetPixel(x - 1, y - 1);
                    Color w2 = obj_sharpori.GetPixel(x - 1, y);
                    Color w3 = obj_sharpori.GetPixel(x - 1, y + 1);
                    Color w4 = obj_sharpori.GetPixel(x, y - 1);
                    Color w5 = obj_sharpori.GetPixel(x, y);
                    Color w6 = obj_sharpori.GetPixel(x, y + 1);
                    Color w7 = obj_sharpori.GetPixel(x + 1, y - 1);
                    Color w8 = obj_sharpori.GetPixel(x + 1, y);
                    Color w9 = obj_sharpori.GetPixel(x + 1, y + 1);
                    int rh = (int)(-w1.R - w4.R - w7.R + w3.R + w6.R + w9.R);
                    int gh = (int)(-w1.G - w4.G - w7.G + w3.G + w6.G + w9.G);
                    int bh = (int)(-w1.B - w4.B - w7.B + w3.B + w6.B + w9.B);
                    int rv = (int)(-w1.R - w2.R - w3.R + w7.R + w8.R + w9.R);
                    int gv = (int)(-w1.G - w2.G - w3.G + w7.G + w8.G + w9.G);
                    int bv = (int)(-w1.B - w2.B - w3.B + w7.B + w8.B + w9.B);
                    int rr = (int)((w1.R + w2.R + w3.R + w4.R + w5.R + w6.R + w7.R + w8.R + w9.R) / 9);
                    int gr = (int)((w1.G + w2.G + w3.G + w4.G + w5.G + w6.G + w7.G + w8.G + w9.G) / 9);
                    int br = (int)((w1.B + w2.B + w3.B + w4.B + w5.B + w6.B + w7.B + w8.B + w9.B) / 9);
                    int r = (int)(rr + rh + rv);
                    if (r < 0) r = -r;
                    if (r > 255) r = 255;
                    int g = (int)(gr + gh + gv);
                    if (g < 0) g = -g;
                    if (g > 255) g = 255;
                    int b = (int)(br + bh + bv);
                    if (b < 0) b = -b;
                    if (b > 255) b = 255;
                    Color wb = Color.FromArgb(r, g, b);
                    obj_sharpto.SetPixel(x, y, wb);
                }
            pictureSharp.SizeMode = PictureBoxSizeMode.Zoom;
            pictureSharp.Image = obj_sharpto;
        }

        private void btnFilterMedian_Click(object sender, EventArgs e)
        {
            int[] red = new int[10];
            int[] green = new int[10];
            int[] blue = new int[10];
            obj_noiseto2 = new Bitmap(obj_noiseto);
            for (int x = 1; x < obj_noiseori.Width - 1; x++)
                for (int y = 1; y < obj_noiseori.Height - 1; y++)
                {
                    int tx = x - 1;
                    int ty = y - 1;
                    //memasukkan piksel pada array
                    for (int index = 1; index < 10; index++)
            {
                red[index] = obj_noiseto.GetPixel(tx, ty).R ;
                green[index] = obj_noiseto.GetPixel(tx, ty).G;
                blue[index] = obj_noiseto.GetPixel(tx, ty).B;

                if (ty - (y - 1) == 2)
                {
                    tx += 1;
                    ty = y - 1;
                }
                else { ty += 1; }
            }

            for (int i = 1; i < 9; i++)
                        for (int j = 1; j < 9; j++)
                        {
                            if (red[j] > red[j + 1])
                            {
                                int a = red[j];
                                red[j] = red[j + 1];
                                red[j + 1] = a;
                            }
                            if (green[j] > green[j + 1])
                            {
                                int a = green[j];
                                green[j] = green[j + 1];
                                green[j + 1] = a;
                            }
                            if (blue[j] > blue[j + 1])
                            {
                                int a = blue[j];
                                blue[j] = blue[j + 1];
                                blue[j + 1] = a;
                            }
                        }
                    Color wb = Color.FromArgb(red[6], green[6], blue[6]);
                    obj_noiseto2.SetPixel(x, y, wb);
                }
            pictureReduksi.SizeMode = PictureBoxSizeMode.Zoom;
            pictureReduksi.Image = obj_noiseto2;
        }

        private void btnloadhi_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                obj_hisori = new Bitmap(openFileDialog1.FileName);
                obj_hisori = new Bitmap(obj_hisori, new Size(100, 100));
                pictureHisto.SizeMode = PictureBoxSizeMode.Zoom;
                pictureHisto.Image = obj_hisori;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            obj_3 = new Bitmap(obj_1);
            obj_3 = new Bitmap(obj_2);
            int height = obj_1.Height;
            int width = obj_1.Width;
            if (obj_1.Height > obj_2.Height) height = obj_2.Height;
            if (obj_1.Width > obj_2.Width) width = obj_2.Width;
            Color pixelColor1;
            Color pixelColor2;
            pictureTrans.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureTrans.Image = obj_3;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    pixelColor1 = obj_1.GetPixel(x, y);
                    int red = pixelColor1.R;
                    int green = pixelColor1.G;
                    int blue = pixelColor1.B;

                    pixelColor2 = obj_2.GetPixel(x, y);
                    int red2 = pixelColor2.R;
                    int green2 = pixelColor2.G;
                    int blue2 = pixelColor2.B;

                    red = (int)((0.35 * red + 0.75 * red2) / 2);
                    green = (int)((0.35 * green + 0.75 * green2) / 2);
                    blue = (int)((0.35 * blue + 0.75 * blue2) / 2);
                    obj_3.SetPixel(x, y, Color.FromArgb(red, green, blue));

                }
            }
        }

        private void btn_root_Click(object sender, EventArgs e)
        {
            enhance = new Bitmap(enhance_ori);
            float c = Convert.ToSingle(tb_c.Text);
            float Y = Convert.ToSingle(tb_y.Text);
            for (int x = 0; x < enhance_ori.Width; x++)
                for (int y = 0; y < enhance_ori.Height; y++)
                {
                    Color w = enhance_ori.GetPixel(x, y);
                    int merah = w.R;
                    int hijau = w.G;
                    int biru = w.B;
                    merah = (int)(c * merah*Math.Exp(1/Y));
                    hijau = (int)(c * hijau*Math.Exp(1/Y));
                    biru = (int)(c * biru*Math.Exp(1/Y));
                    if (merah > 255) { merah = 255; }
                    if (hijau > 255) { hijau = 255; }
                    if (biru > 255) { biru = 255; }
                    if (merah < 0) { merah = 0; }
                    if (hijau < 0) { hijau = 0; }
                    if (biru < 0) { biru = 0; }
                    enhance.SetPixel(x, y, Color.FromArgb(merah, hijau, biru));
                }
            picture_to6.SizeMode = PictureBoxSizeMode.Zoom;
            picture_to6.Image = enhance;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                enhance_ori = new Bitmap(openFileDialog1.FileName);
                picture_ori6.SizeMode = PictureBoxSizeMode.Zoom;
                picture_ori6.Image = enhance_ori;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            obj_grayk = new Bitmap(obj_grayori);
            int a = Convert.ToInt16(textBox2.Text);
            for (int x = 0; x < obj_grayori.Width; x++)
                for (int y = 0; y < obj_grayk.Height; y++)
                {
                    Color w = obj_grayori.GetPixel(x, y);
                    int red = w.R;
                    int green = w.G;
                    int blue = w.B;
                    if ((red + a) <= 255) { red = red + a; }
                    if ((green + a) <= 255) { green = green + a; }
                    if ((blue + a) <= 255) { blue = blue + a; }
                    obj_grayk.SetPixel(x, y, Color.FromArgb(red, green, blue));
                }
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.Image = obj_grayk;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        public class Class1 : Form1
        {

        }

    }
}