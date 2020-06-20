using System;
using System.Collections;
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
using ArizaTakipYazılımSistemi.Degiskenler;
using wordAk = Microsoft.Office.Interop.Word;

namespace ArizaTakipYazılımSistemi.View
{
    public partial class YoneticiEkrani : Form
    {
        private YoneticiDB db = new YoneticiDB();
        public YoneticiEkrani()
        {
            InitializeComponent();
            ekran_doldur();
        }
        private void ekran_doldur()
        {
            dataGridView1.DataSource = db.atanmamisAriza();
            dataGridView1.Columns[0].HeaderText = "Arıza Numarası";
            dataGridView1.Columns[1].HeaderText = "Arıza Başlığı";
            dataGridView1.Columns[2].HeaderText = "Arıza Konusu";
            dataGridView1.Columns[3].HeaderText = "Arıza Bildirim Saati";
            dataGridView1.Columns[4].HeaderText = "Bildiren Personel";
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView2.DataSource = db.atanmisAriza();
            dataGridView2.Columns[0].HeaderText = "Arıza Numarası";
            dataGridView2.Columns[1].HeaderText = "Arıza Başlığı";
            dataGridView2.Columns[2].HeaderText = "Arıza Konusu";
            dataGridView2.Columns[3].HeaderText = "Arıza Bildirim Saati";
            dataGridView2.Columns[4].HeaderText = "Personel Atanma Saati";
            dataGridView2.Columns[5].HeaderText = "Bildiren Personel";
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView3.DataSource = db.giderilmisAriza();
            dataGridView3.Columns[0].HeaderText = "Arıza Numarası";
            dataGridView3.Columns[1].HeaderText = "Arıza Başlığı";
            dataGridView3.Columns[2].HeaderText = "Arıza Konusu";
            dataGridView3.Columns[3].HeaderText = "Çözüm Mesajı";
            dataGridView3.Columns[4].HeaderText = "Arıza Bildirim Saati";
            dataGridView3.Columns[5].HeaderText = "Personel Atanma Saati";
            dataGridView3.Columns[6].HeaderText = "Giderilme Saati";
            dataGridView3.Columns[7].HeaderText = "Bildiren Personel";
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView4.DataSource = db.bolum_kullanicilar();
            dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView4.Columns[0].HeaderText = "Personel Numarası";
            dataGridView4.Columns[1].HeaderText = "Adı";
            dataGridView4.Columns[2].HeaderText = "Soyadı";
            dataGridView4.Columns[3].Visible = false;
            dataGridView4.Columns[4].Visible = false;
            dataGridView4.Columns[5].Visible = false;
            dataGridView4.Columns[6].HeaderText = "Kullanıcı Adı";
            dataGridView4.Columns[7].HeaderText = "Şifre";
            dataGridView4.Columns[8].Visible = false;
            dataGridView4.Columns[9].HeaderText = "Bölüm";
            dataGridView4.Columns[10].Visible = false;
            dataGridView4.Columns[11].HeaderText = "Unvan";

        }
        private void YoneticiEkrani_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void atanmamis(object sender, DataGridViewCellMouseEventArgs e)
        {
            ArizaDetay arizaDetay = new ArizaDetay();
            db.arizaDetay(Convert.ToInt32(dataGridView1[0, e.RowIndex].Value.ToString()));
            arizaDetay.ShowDialog();
        }

        private void atanmis(object sender, DataGridViewCellMouseEventArgs e)
        {
            ArizaDetay arizaDetay = new ArizaDetay();
            db.arizaDetay(Convert.ToInt32(dataGridView2[0, e.RowIndex].Value.ToString()));
            arizaDetay.ShowDialog();
        }

        private void tamamlanmis(object sender, DataGridViewCellMouseEventArgs e)
        {
            ArizaDetay arizaDetay = new ArizaDetay();
            db.arizaDetay(Convert.ToInt32(dataGridView3[0, e.RowIndex].Value.ToString()));
            arizaDetay.ShowDialog();
        }

        private void reload(object sender, EventArgs e)
        {
            ekran_doldur();
        }

        private void personeller(object sender, DataGridViewCellMouseEventArgs e)
        {
            Degiskenler.Admin.kullanici = false;
            int id = Convert.ToInt32(dataGridView4[0, e.RowIndex].Value.ToString());
            db.secilenPersonel(id);
            KullaniciDetay kullanici = new KullaniciDetay();
            kullanici.ShowDialog();
        }
        private int tarihKarsilastirma(DateTime son_tarih, DateTime ilk_tarih)
        {
            return DateTime.Compare(son_tarih, ilk_tarih);
        }
        ErrorProvider err = new ErrorProvider();
        ArrayList colums;
        private Raporlama rapor = new Raporlama();

        private void pdfSave(DataTable dataTable, string isim)
        {
            Personel pers = Controller.Kullanici.personel;
            Bolum bol = new Bolum();
            Unvan un = new Unvan();
            bol.bolum_id = Controller.Kullanici.personel_bolum.bolum_id;
            bol.bolum = rapor.bolum(bol.bolum_id);
            un.unvan_id = Controller.Kullanici.personel_unvan.unvan_id;
            un.unvan = rapor.unvan(un.unvan_id);
            RichTextBox rich = new RichTextBox();
            rich.Clear();
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

        private void PersonelRaporu(DataTable dataTable , string baslik)
        {
            Personel pers = Controller.Kullanici.personel;
            Bolum bol = new Bolum();
            Unvan un = new Unvan();
            bol.bolum_id = Controller.Kullanici.personel_bolum.bolum_id;
            bol.bolum = rapor.bolum(bol.bolum_id);
            un.unvan_id = Controller.Kullanici.personel_unvan.unvan_id;
            un.unvan = rapor.unvan(un.unvan_id);
            RichTextBox rich = new RichTextBox();
            rich.Clear();
            rich.Text += "\t\t\t\t\t\t\t\t\t\t" + DateTime.Now.ToString("dd/MM/yyyy") + "\n\n\t\t" + baslik.ToUpper();
            rich.Text += "\n\t\t" + pers.personel_id + "\n\t\t" + pers.adi + " " + pers.soyadi + "\n\t\t " + bol.bolum + "\n\t\t" + un.unvan + "\n\n\n";
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    if (dataTable.Rows[i][j].ToString().Equals("True"))
                        rich.Text += colums[j] + ": Raporlama yapılabilir\n";
                    else if (dataTable.Rows[i][j].ToString().Equals("False"))
                        rich.Text += colums[j] + ": Raporlama yapılamaz\n";
                    else
                        rich.Text += colums[j] + ": " + dataTable.Rows[i][j].ToString() + "\n";
                }
                rich.Text += "\n";
            }
            rich.Text += "\n\n\n" + dataTable.Rows.Count + " adet personel bilgisi Listelenmiştir.";
            wordAk.Application wordDoc = new wordAk.Application();

            wordAk.Document wordApp;
            object wordObj = System.Reflection.Missing.Value;
            wordApp = wordDoc.Documents.Add(ref wordObj);
            wordDoc.Selection.TypeText(rich.Text);
            MessageBox.Show("Raporunuz oluşturulmuştur.");
            wordDoc.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (tarihKarsilastirma(dateTimePicker2.Value, dateTimePicker1.Value) == 1)
            {
                DateTime tarih_bir = dateTimePicker1.Value;
                DateTime tarih_iki = dateTimePicker2.Value.AddDays(1);
                string isim = Controller.Kullanici.personel_bolum.bolum + " bölümüne ait " + tarih_bir.ToString("dd/MM/yyyy") + "-" + tarih_iki.ToString("dd / MM / yyyy") + " tarihleri arasında atamamış arızalar";
                DataTable dt = new DataTable();
                dt.Clear();
                dt = rapor.ozelBolumAtanmamıs(tarih_bir.ToString("dd/MM/yyyy"), tarih_iki.ToString("dd/MM/yyyy"), Controller.Kullanici.personel_bolum.bolum_id);
                colums = new ArrayList();
                colums.Clear();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    colums.Add(dt.Columns[i]);
                }
                pdfSave(dt, isim);
            }
            else
            {
                err.Clear();
                err.SetError(button1, "Tarih bilgisi doğru ayarlanmadı.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tarihKarsilastirma(dateTimePicker2.Value, dateTimePicker1.Value) == 1)
            {
                DateTime tarih_bir = dateTimePicker1.Value;
                DateTime tarih_iki = dateTimePicker2.Value.AddDays(1);
                string isim = Controller.Kullanici.personel_bolum.bolum + " bölümüne ait " + tarih_bir.ToString("dd/MM/yyyy") + "-" + tarih_iki.ToString("dd / MM / yyyy") + " tarihleri arasında atanmış arızalar";
                DataTable dt = new DataTable();
                dt.Clear();
                dt = rapor.ozelBolumAtanmıs(tarih_bir.ToString("dd/MM/yyyy"), tarih_iki.ToString("dd/MM/yyyy"), Controller.Kullanici.personel_bolum.bolum_id);
                colums = new ArrayList();
                colums.Clear();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    colums.Add(dt.Columns[i]);
                }
                pdfSave(dt, isim);
            }
            else
            {
                err.Clear();
                err.SetError(button1, "Tarih bilgisi doğru ayarlanmadı.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (tarihKarsilastirma(dateTimePicker2.Value, dateTimePicker1.Value) == 1)
            {
                DateTime tarih_bir = dateTimePicker1.Value;
                DateTime tarih_iki = dateTimePicker2.Value.AddDays(1);
                string isim = Controller.Kullanici.personel_bolum.bolum + " bölümüne ait " + tarih_bir.ToString("dd/MM/yyyy") + "-" + tarih_iki.ToString("dd / MM / yyyy") + " tarihleri arasında bulunan bütün arızalar";
                DataTable dt = new DataTable();
                dt.Clear();
                dt = rapor.ozelBolumTüm(tarih_bir.ToString("dd/MM/yyyy"), tarih_iki.ToString("dd/MM/yyyy"), Controller.Kullanici.personel_bolum.bolum_id);
                colums = new ArrayList();
                colums.Clear();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    colums.Add(dt.Columns[i]);
                }
                pdfSave(dt, isim);
            }
            else
            {
                err.Clear();
                err.SetError(button1, "Tarih bilgisi doğru ayarlanmadı.");
            }
        }
        
        private void button7_Click(object sender, EventArgs e)
        {
            DateTime tarih_bir = dateTimePicker1.Value;
            DateTime tarih_iki = dateTimePicker2.Value.AddDays(1);
            string isim = Controller.Kullanici.personel_bolum.bolum + " bölümüne ait " + tarih_bir.ToString("dd/MM/yyyy") + "-" + tarih_iki.ToString("dd / MM / yyyy") + " tarihleri arasında atamamış arızalar";
            DataTable dt = new DataTable();
            dt.Clear();
            dt = rapor.personelListele(Controller.Kullanici.personel_bolum.bolum_id);
            colums = new ArrayList();
            colums.Clear();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                colums.Add(dt.Columns[i]);
            }
            PersonelRaporu(dt, isim);
        }
        //genel atanmamış
        private void button3_Click(object sender, EventArgs e)
        {

            DateTime tarih_bir = dateTimePicker1.Value;
            DateTime tarih_iki = dateTimePicker2.Value.AddDays(1);
            string isim = Controller.Kullanici.personel_bolum.bolum + " bölümüne ait atanmamış arızalar";
            DataTable dt = new DataTable();
            dt.Clear();
            dt = rapor.bolumAtanmamıs(Controller.Kullanici.personel_bolum.bolum_id);
            colums = new ArrayList();
            colums.Clear();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                colums.Add(dt.Columns[i]);
            }
            pdfSave(dt, isim);
        }
        //genel atanmış
        private void button4_Click(object sender, EventArgs e)
        {
            DateTime tarih_bir = dateTimePicker1.Value;
            DateTime tarih_iki = dateTimePicker2.Value.AddDays(1);
            string isim = Controller.Kullanici.personel_bolum.bolum + " bölüme ait " + tarih_bir.ToString("dd/MM/yyyy") + "-" + tarih_iki.ToString("dd / MM / yyyy") + " tarihleri arasında atamamış arızalar";
            DataTable dt = new DataTable();
            dt.Clear();
            dt = rapor.bolumAtanmıs(Controller.Kullanici.personel_bolum.bolum_id);
            colums = new ArrayList();
            colums.Clear();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                colums.Add(dt.Columns[i]);
            }
            pdfSave(dt, isim);
        }

        //tüm arıza
        private void button6_Click(object sender, EventArgs e)
        {
            DateTime tarih_bir = dateTimePicker1.Value;
            DateTime tarih_iki = dateTimePicker2.Value.AddDays(1);
            string isim = Controller.Kullanici.personel_bolum.bolum + " bölüme ait " + tarih_bir.ToString("dd/MM/yyyy") + "-" + tarih_iki.ToString("dd / MM / yyyy") + " tarihleri arasında atamamış arızalar";
            DataTable dt = new DataTable();
            dt.Clear();
            dt = rapor.bolumTümAriza(Controller.Kullanici.personel_bolum.bolum_id);
            colums = new ArrayList();
            colums.Clear();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                colums.Add(dt.Columns[i]);
            }
            pdfSave(dt, isim);
        }
    }
}
