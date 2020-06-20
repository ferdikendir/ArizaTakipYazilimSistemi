using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArizaTakipYazılımSistemi.Degiskenler;
using ArizaTakipYazılımSistemi.Controller;

namespace ArizaTakipYazılımSistemi.View
{
    public partial class BilgiIslemPersonel : Form
    {
        public BilgiIslemPersonel()
        {
            InitializeComponent();

        }
        private BilgiIslemDB bilgi_islem = new BilgiIslemDB();
        private void BilgiIslemPersonel_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            ekranDoldur();
        }
        private void ekranDoldur()
        {
            bilgi_islem.arizaBilgileri();
            label1.Text = Kullanici.personel.adi.ToUpper() + " " + Kullanici.personel.soyadi.ToUpper();
            label2.Text = Kullanici.personel_bolum.bolum.ToUpper();
            label3.Text = Kullanici.personel_unvan.unvan.ToUpper();
            label6.Text = Degiskenler.BilgiIslem.toplam_ariza + " adet ariza";
            label5.Text = Degiskenler.BilgiIslem.tamamlanan_ariza + " adet tamamlanmış ariza";
            label4.Text = Degiskenler.BilgiIslem.tamamlanacak_ariza + " adet çözüm bekleyen ariza";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = bilgi_islem.cozumBekleyen();
            dataGridView1.Columns[0].HeaderText = "Arıza Numarası";
            dataGridView1.Columns[1].HeaderText = "Arıza Başlık";
            dataGridView1.Columns[2].HeaderText = "Arıza Konusu";
            dataGridView1.Columns[3].HeaderText = "Bildirim Saati";
            dataGridView1.Columns[4].HeaderText = "Atanma Saati";
            dataGridView1.Columns[5].HeaderText = "Bildiren Personel";
            dataGridView1.Columns[6].HeaderText = "Bölüm";
            dataGridView1.Columns[7].HeaderText = "Ünvan";

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.DataSource = bilgi_islem.cozumlenmis();
            dataGridView2.Columns[0].HeaderText = "Arıza Numarası";
            dataGridView2.Columns[1].HeaderText = "Arıza Başlık";
            dataGridView2.Columns[2].HeaderText = "Arıza Konusu";
            dataGridView2.Columns[3].HeaderText = "Arıza Mesajı";
            dataGridView2.Columns[4].HeaderText = "Bildirim Saati";
            dataGridView2.Columns[5].HeaderText = "Tamamlanma Saati";
            dataGridView2.Columns[6].HeaderText = "Bildiren Personel";
            dataGridView2.Columns[7].HeaderText = "Bölüm";
            dataGridView2.Columns[8].HeaderText = "Ünvan";
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dataGridView1[0, e.RowIndex].Value.ToString());
                DialogResult tamamla = new DialogResult();
                tamamla = MessageBox.Show(dataGridView1[0, e.RowIndex].Value.ToString() + " nolu arızayı giderdiniz mi?", "Uyarı", MessageBoxButtons.YesNo);
                if (tamamla == DialogResult.Yes)
                {
                    bilgi_islem.arizaDetay(Convert.ToInt32(dataGridView1[0, e.RowIndex].Value.ToString()));
                    CozumMesajıEkle cozum = new CozumMesajıEkle();
                    cozum.ShowDialog();
                }
            }
            catch (Exception ex)
            {

                
            }
            
        }

        private void BilgiIslemPersonel_Activated(object sender, EventArgs e)
        {
            bilgi_islem.arizaBilgileri();
            ekranDoldur();
        }
    }
}
