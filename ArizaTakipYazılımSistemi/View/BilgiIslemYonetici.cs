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
using ArizaTakipYazılımSistemi.View;
using System.Collections;
using wordAk = Microsoft.Office.Interop.Word;

namespace ArizaTakipYazılımSistemi.View
{
    public partial class BilgiIslemYonetici : Form
    {
        public BilgiIslemYonetici()
        {
            InitializeComponent();
        }
        private void BilgiIslemYonetici_Load(object sender, EventArgs e)
        {
            
            this.CenterToScreen();
            ekranDoldur();
        }
        private BilgiIslemYoneticiDB db = new BilgiIslemYoneticiDB();
        public void ekranDoldur()
        {
            label1.Text = Kullanici.personel.adi.ToUpper() + " " + Kullanici.personel.soyadi.ToUpper();
            label2.Text = Kullanici.personel_bolum.bolum.ToUpper();
            label3.Text = Kullanici.personel_unvan.unvan.ToUpper();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.DataSource = db.atanmaBekleyen();
            dataGridView1.Columns[0].HeaderText = "Arıza Numarası";
            dataGridView1.Columns[1].HeaderText = "Arıza Başlık";
            dataGridView1.Columns[2].HeaderText = "Arıza Konusu";
            dataGridView1.Columns[3].HeaderText = "Arıza Bildirim Saati";
            dataGridView1.Columns[4].HeaderText = "Bildiren Personel";
            dataGridView1.Columns[5].HeaderText = "Bölüm";
            dataGridView1.Columns[6].HeaderText = "Unvan";

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.DataSource = db.tamamlanan();
            dataGridView2.Columns[0].HeaderText = "Arıza Numarası";
            dataGridView2.Columns[1].HeaderText = "Arıza Başlık";
            dataGridView2.Columns[2].HeaderText = "Arıza Konusu";
            dataGridView2.Columns[3].HeaderText = "Çözüm Mesajı";
            dataGridView2.Columns[4].HeaderText = "Arıza Bildirim Saati";
            dataGridView2.Columns[5].HeaderText = "Bildiren Personel";
            dataGridView2.Columns[6].HeaderText = "Bölüm";
            dataGridView2.Columns[7].HeaderText = "Arıza Tamamlanma Saati";

            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView3.DataSource = db.tamamlanmaBekleyen();
            dataGridView3.Columns[0].HeaderText = "Arıza Numarası";
            dataGridView3.Columns[1].HeaderText = "Arıza Başlık";
            dataGridView3.Columns[2].HeaderText = "Arıza Konusu";
            dataGridView3.Columns[3].HeaderText = "Atanma saati";
            dataGridView3.Columns[4].HeaderText = "Bildiren Personel";
            dataGridView3.Columns[5].HeaderText = "Bölüm";

            textBox3.Text = Kullanici.personel.adi;
            textBox4.Text = Kullanici.personel.soyadi;
            textBox5.Text = Kullanici.personel.username;
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";

            dataGridView6.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView7.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            textBox12.Text = "";
            textBox11.Text = "";
            textBox10.Text = "";
            textBox9.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox1.SelectedValue = 0;
            comboBox2.SelectedValue = 0;
            Dictionary<int, string> bolum = new Dictionary<int, string>();
            bolum.Add(0, "-----seçiniz");
            DataTable table_bolum = db.bolum();
            dataGridView7.DataSource = table_bolum;
            DataTable table_unvan = db.unvan();
            dataGridView6.DataSource = table_unvan;
            foreach (DataRow row in table_bolum.Rows)
            {
                int id = Convert.ToInt16(row["bolum_Id"]);
                string bolum_adi = row["bolum"].ToString().ToUpper();
                bolum.Add(id, bolum_adi);
            }
            comboBox1.DataSource = new BindingSource(bolum, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";

            Dictionary<int, string> unvan = new Dictionary<int, string>();
            unvan.Add(0, "-----seçiniz");
            foreach (DataRow row in table_unvan.Rows)
            {
                int id = Convert.ToInt16(row["unvan_Id"]);
                string unvan_adi = row["unvan"].ToString().ToUpper();
                unvan.Add(id, unvan_adi);
            }
            comboBox2.DataSource = new BindingSource(unvan, null);
            comboBox2.DisplayMember = "Value";
            comboBox2.ValueMember = "Key";

            dataGridView5.DataSource = db.kullanicilar();
            dataGridView5.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView5.Columns[0].HeaderText = "Personel Numarası";
            dataGridView5.Columns[1].HeaderText = "Adı";
            dataGridView5.Columns[2].HeaderText = "Soyadı";
            dataGridView5.Columns[3].Visible = false;
            dataGridView5.Columns[4].Visible = false;
            dataGridView5.Columns[5].Visible = false;
            dataGridView5.Columns[6].HeaderText = "Kullanıcı Adı";
            dataGridView5.Columns[7].HeaderText = "Şifre";
            dataGridView5.Columns[8].Visible = false;
            dataGridView5.Columns[9].HeaderText = "Bölüm";
            dataGridView5.Columns[10].Visible = false;
            dataGridView5.Columns[11].HeaderText = "Unvan";

            dataGridView7.Columns[0].HeaderText = "Bölüm numarası";
            dataGridView7.Columns[1].HeaderText = "Bölüm";

            dataGridView6.Columns[0].HeaderText = "Ünvan numarası";
            dataGridView6.Columns[1].HeaderText = "Ünvan";

            //label21.Text = "Genel Raporlama";
        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dataGridView2[0, e.RowIndex].Value.ToString());
                db.arizaDetay(id);
                yoneticiArizaDetay yonAriza = new yoneticiArizaDetay();
                yonAriza.ShowDialog();
            }
            catch (Exception)
            {

            }
            
        }

        private void ciftTik(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Yonetici.secilen_ariza = null;
                int id = Convert.ToInt32(dataGridView1[0, e.RowIndex].Value.ToString());
                db.arizaDetay(id);
                PersonelAtama persAtama = new PersonelAtama();
                persAtama.ShowDialog();
            }
            catch (Exception )
            {

            }
            
        }

        private void iserik(object sender, EventArgs e)
        {
            this.CenterToScreen();
            ekranDoldur();
        }
        ErrorProvider err = new ErrorProvider();
        private void button2_Click(object sender, EventArgs e)
        {
            err.Dispose();
            if (textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "")
            {
                if (textBox6.Text.Equals(textBox7.Text) && textBox8.Text == Kullanici.personel.password)
                {
                    Personel pers = new Personel();
                    pers.personel_id = Kullanici.personel.personel_id;
                    pers.adi = textBox3.Text.ToUpper();
                    pers.soyadi = textBox4.Text.ToUpper();
                    pers.username = textBox5.Text.ToUpper();
                    pers.password = textBox7.Text.ToUpper();
                    pers.bolum_id = Kullanici.personel.bolum_id;
                    pers.unvan_id = Kullanici.personel.unvan_id;
                    pers.yetki = Kullanici.personel.yetki;
                    db.updatePersonel(pers);
                    Kullanici.personel = pers;
                    ekranDoldur();
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox9.Text != "" && textBox10.Text != "" && textBox11.Text != "" && textBox12.Text != "" && Convert.ToInt16(comboBox1.SelectedValue) != 0 && Convert.ToInt16(comboBox2.SelectedValue) != 0)
            {
                Personel personel = new Personel();
                personel.adi = textBox12.Text.ToUpper();
                personel.soyadi = textBox11.Text.ToUpper();
                personel.bolum_id = Convert.ToInt16(comboBox1.SelectedValue);
                personel.unvan_id = Convert.ToInt16(comboBox2.SelectedValue);
                if (personel.unvan_id == 1000)
                    personel.yetki = true;
                personel.username = textBox10.Text.ToUpper();
                personel.password = textBox9.Text.ToUpper();
                if (db.kontrolPersonel(personel))
                {
                    db.ınsertPersonel(personel);
                    ekranDoldur();
                }
                else
                {
                    MessageBox.Show("Aynı isimde kullanıcı bulunmakta veya kullanıcı adınız alınmış durumdadır.");
                }
            }
            else
                MessageBox.Show("Bilgiler yanlış");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (db.bolumSorgula(textBox1.Text.ToUpper()))
                {
                    db.insertBolum(textBox1.Text.ToUpper());
                    ekranDoldur();
                }
                else
                {
                    err.SetError(textBox1, "Bölüm bulunmaktadır.");
                }

            }
            else
            {
                err.SetError(textBox1, "Bu alan boş geçilemez.");
            }
        }

        private void secilen_personel(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dataGridView5[0, e.RowIndex].Value.ToString());
                db.secilenPersonel(id);
                KullaniciDetay kullanici = new KullaniciDetay();
                kullanici.ShowDialog();
            }
            catch (Exception)
            {

            }
            
        }

        private void bolum_sil(object sender, DataGridViewCellMouseEventArgs e)
        {
            DialogResult sil = new DialogResult();
            sil = MessageBox.Show(dataGridView7[0, e.RowIndex].Value.ToString() + " nolu bölümü silmek istiyor musunuz ?", "Uyarı", MessageBoxButtons.YesNo);
            if (sil == DialogResult.Yes)
            {
                db.bolum_sil(Convert.ToInt16(dataGridView6[0, e.RowIndex].Value.ToString()));
                ekranDoldur();
            }
        }

        private void unvan_sil(object sender, DataGridViewCellMouseEventArgs e)
        {
            DialogResult sil = new DialogResult();
            sil = MessageBox.Show(dataGridView6[0, e.RowIndex].Value.ToString() + " nolu ünvanı silmek istiyor musunuz ?", "Uyarı", MessageBoxButtons.YesNo);
            if (sil == DialogResult.Yes)
            {
                db.unvan_sil(Convert.ToInt16(dataGridView6[0, e.RowIndex].Value.ToString()));
                ekranDoldur();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                if (db.unvanSorgula(textBox2.Text.ToUpper()))
                {
                    db.insertUnvan(textBox2.Text.ToUpper());
                    ekranDoldur();
                }
                else
                {
                    err.SetError(textBox2, "Ünvan bulunmaktadır.");
                }
            }
            else
            {
                err.SetError(textBox2, "Bu alan boş geçilemez.");
            }
        }
        string isim;
        ArrayList colums;
        private int tarihKarsilastirma(DateTime son_tarih, DateTime ilk_tarih)
        {
            return DateTime.Compare(son_tarih, ilk_tarih);
        }
        Raporlama rapor = new Raporlama();
        
        private void pdfSave(DataTable dataTable, string isim)
        {
            Personel pers = Degiskenler.Admin.personel;
            Bolum bol = new Bolum();
            Unvan un = new Unvan();
            bol.bolum_id = Degiskenler.Admin.personel.bolum_id;
            bol.bolum = rapor.bolum(bol.bolum_id);
            un.unvan_id = Degiskenler.Admin.personel.unvan_id;
            un.unvan = rapor.unvan(un.unvan_id);
            RichTextBox rich = new RichTextBox();
            rich.Text += "\t\t\t\t\t\t\t\t\t\t" + DateTime.Now.ToString("dd/MM/yyyy") + "\n\n\t\t" + isim.ToUpper();
            rich.Text += "\n\t\t" + pers.personel_id + "\n\t\t" + pers.adi + " " + pers.soyadi + "\n\t\t " + bol.bolum + "\n\t\t" + un.unvan + "\n\n\n";
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    rich.Text += colums[j] + ": " + dataTable.Rows[i][j].ToString() + "\n";
                }
                rich.Text += "\n";
            }
            rich.Text += "\n\n\n" + dataTable.Rows.Count + " adet arıza bilgisi bulunmaktadır.";
            wordAk.Application wordDoc = new wordAk.Application();

            wordAk.Document wordApp;
            object wordObj = System.Reflection.Missing.Value;
            wordApp = wordDoc.Documents.Add(ref wordObj);
            wordDoc.Selection.TypeText(rich.Text);
            MessageBox.Show("Raporunuz oluşturulmuştur.");
            wordDoc.Visible = true;
        }
    }
}
