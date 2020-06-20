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
    public partial class yoneticiArizaDetay : Form
    {
        public yoneticiArizaDetay()
        {
            InitializeComponent();
        }
        private BilgiIslemYoneticiDB db = new BilgiIslemYoneticiDB();
        private void yoneticiArizaDetay_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            textBox1.Text = Yonetici.secilen_ariza.ariza_id.ToString();
            textBox2.Text = Yonetici.secilen_ariza.baslik;
            textBox3.Text = Yonetici.secilen_ariza.konu;
            textBox4.Text = Yonetici.secilen_ariza.mesaj;
            label5.Text = db.arizaPersonel(Yonetici.secilen_ariza.gideren_personel_id);
            label7.Text = Yonetici.secilen_ariza.bildirim_saati.ToString();
            label8.Text = Yonetici.secilen_ariza.bildirim_saati.ToString();
            label9.Text = Yonetici.secilen_ariza.tamamlanma_saati.ToString();
            label10.Text = (Convert.ToDateTime(Yonetici.secilen_ariza.tamamlanma_saati) - Convert.ToDateTime(Yonetici.secilen_ariza.bildirim_saati)).TotalDays.ToString() + " günde giderilmiştir.";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Yonetici.secilen_ariza = null;
            this.Close();
            this.Dispose();
        }
    }
}
