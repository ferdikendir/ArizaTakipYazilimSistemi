using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArizaTakipYazılımSistemi.Controller;

namespace ArizaTakipYazılımSistemi.View
{
    public partial class ArizaDetay : Form
    {
        public ArizaDetay()
        {
            InitializeComponent();
        }
        private DatabaseOperation db = new DatabaseOperation();
        private void ArizaDetay_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;

            textBox1.Text = Kullanici.ariza.ariza_id.ToString();
            textBox2.Text = Kullanici.ariza.baslik;
            textBox3.Text = Kullanici.ariza.konu;
            label4.Text = Kullanici.ariza.bildirim_saati.ToString() + " tarihinde bildirdiniz.";
            label8.Text = Kullanici.ariza.atanma_saati.ToString() + " tarinde personel ataması gerçekleşmiştir.";
            if (Kullanici.ariza.tamamlanma_saati.ToString().Equals("19.09.1999 00:00:00"))
            {
                label9.Text = "Arızanız tamamlanmamıştır.";
                label10.Text = "Süre hesaplanamıyor.";
            }
            else
            {
                label9.Text = Kullanici.ariza.tamamlanma_saati.ToString() + " tarinde arızanız giderilmiştir.";
                int day = Convert.ToInt16((Convert.ToDateTime(Kullanici.ariza.tamamlanma_saati) - Convert.ToDateTime(Kullanici.ariza.bildirim_saati)).TotalDays);
                if(day == 0)
                    label10.Text = "Arızanız aynı gün içinde giderilmiştir.";
                else
                    label10.Text = "Arızanız " + day.ToString() + " günde giderilmiştir.";


            }
            textBox4.Text = Kullanici.ariza.mesaj;
            label7.Text = db.arizaPersonel(Kullanici.ariza.gideren_personel_id);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kullanici.ariza = null;
            this.Close();
            this.Dispose();

        }
    }
}
