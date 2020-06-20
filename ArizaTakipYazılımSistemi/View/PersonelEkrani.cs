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
using ArizaTakipYazılımSistemi.Model;

namespace ArizaTakipYazılımSistemi.View
{
    public partial class PersonelEkrani : Form
    {
        public PersonelEkrani()
        {
            this.Refresh();
            InitializeComponent();
        }

        private DatabaseOperation db = new DatabaseOperation();

        private void PersonelEkrani_Load(object sender, EventArgs e)
        {           
            this.CenterToScreen();            
            gridDoldur();
        }
        public void gridDoldur()
        {
            if (Kullanici.personel.yetki == false)
                label15.Visible = false;
            //db.kullaniciVerileri();
            label1.Text = Kullanici.personel.adi.ToUpper() + " " + Kullanici.personel.soyadi.ToUpper();
            label2.Text = Kullanici.personel_bolum.bolum;
            label3.Text = Kullanici.personel_unvan.unvan;
            label4.Text = Kullanici.ariza_sayisi + " adet arızanız bulunmaktadır";
            label5.Text = Kullanici.atama_bekleyen_ariza_sayisi + " adet arıza atanmayı bekliyor";
            label6.Text = Kullanici.tamamlanmayi_ariza_sayisi + " adet arıza tamamlanmayı bekliyor";
            dataGridView1.DataSource = db.tamamlanan_arizalar();
            dataGridView1.Columns[0].HeaderText = "Arıza Numarası";
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].HeaderText = "Arıza Başlığı";
            dataGridView1.Columns[3].HeaderText = "Arıza Konusu";
            dataGridView1.Columns[4].HeaderText = "Arıza Mesajı";
            dataGridView1.Columns[5].HeaderText = "Arıza Bildirim Saati";
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].HeaderText = "Arıza Tamamlanma Saati";
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.DataSource = db.tamamlanma_bekleyen();
            dataGridView2.Columns[0].HeaderText = "Arıza Numarası";
            dataGridView2.Columns[1].Visible = false;
            dataGridView2.Columns[2].HeaderText = "Arıza Başlığı";
            dataGridView2.Columns[3].HeaderText = "Arıza Konusu";
            dataGridView2.Columns[4].Visible = false;
            dataGridView2.Columns[5].HeaderText = "Arıza Bildirim Saati";
            dataGridView2.Columns[6].HeaderText = "Arıza Atanma Saati";
            dataGridView2.Columns[7].Visible = false;
            dataGridView2.Columns[8].Visible = false;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView3.DataSource = db.atanmayi_bekleyen();
            dataGridView3.Columns[0].HeaderText = "Arıza Numarası";
            dataGridView3.Columns[1].Visible = false;
            dataGridView3.Columns[2].HeaderText = "Arıza Başlığı";
            dataGridView3.Columns[3].HeaderText = "Arıza Konusu";
            dataGridView3.Columns[4].Visible = false;
            dataGridView3.Columns[5].HeaderText = "Arıza Bildirim Saati";
            dataGridView3.Columns[6].Visible =  false;
            dataGridView3.Columns[7].Visible = false;
            dataGridView3.Columns[8].Visible = false;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            textBox3.Text = Kullanici.personel.adi;
            textBox4.Text = Kullanici.personel.soyadi;
            textBox5.Text = Kullanici.personel.username;
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            TabPage t = tabControl1.TabPages[0];
            tabControl1.SelectedTab = t;
            
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Equals("") == false && textBox2.Text.Equals("") == false)
            {
                Ariza ariza = new Ariza();
                ariza.baslik = textBox1.Text.ToUpper();
                ariza.konu = textBox2.Text.ToUpper();
                ariza.bildirim_saati = DateTime.Now;
                ariza.personel_id = Kullanici.personel.personel_id;
                if (db.arizaKontrol(textBox1.Text.ToUpper()))
                    db.insertAriza(ariza);
                else
                    MessageBox.Show("Aynı başlıkta arıza bildirimizin bulunmaktadır");
                textBox1.Text = "";
                textBox2.Text = "";
                db.kullaniciVerileri();
                gridDoldur();
            }
            else
            {
                MessageBox.Show("Arıza bildirimi için bilgileri eksiksiz doldurunuz.");
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ArizaDetay arizaDetay = new ArizaDetay();
            db.arizaDetay(Convert.ToInt32(dataGridView1[0, e.RowIndex].Value.ToString()));
            arizaDetay.ShowDialog();
        }

        private void aslansın(object sender, DataGridViewCellEventArgs e)
        {
            ArizaDetay arizaDetay = new ArizaDetay();
            db.arizaDetay(Convert.ToInt32(dataGridView2[0, e.RowIndex].Value.ToString()));
            arizaDetay.ShowDialog();
        }
        ErrorProvider err = new ErrorProvider();
        private void button2_Click(object sender, EventArgs e)
        {
            err.Dispose();
            if(textBox3.Text !="" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "")
            {
                if (textBox6.Text.Equals(textBox7.Text) && textBox8.Text == Kullanici.personel.password)
                {
                    Personel pers = new Personel();
                    pers.personel_id = Kullanici.personel.personel_id;
                    pers.adi = textBox3.Text;
                    pers.soyadi = textBox4.Text;
                    pers.username = textBox5.Text;
                    pers.password = textBox7.Text;
                    pers.bolum_id = Kullanici.personel.bolum_id;
                    pers.unvan_id = Kullanici.personel.unvan_id;
                    pers.yetki = Kullanici.personel.yetki;
                    db.updatePersonel(pers);
                    Kullanici.personel = pers;
                    gridDoldur();
                }
                else
                {
                    err.SetError(textBox6, "Şifreler uyuşmuyor veya eski şifreniz yanlış.");
                }
            }
            else
            {
                err.SetError(button2, "Bilgileri eksiksiz giriniz.");
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {
            YoneticiEkrani ynt_ekre = new YoneticiEkrani();
            ynt_ekre.ShowDialog();
        }
    }
}
