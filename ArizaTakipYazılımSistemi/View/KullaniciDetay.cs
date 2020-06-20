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
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using wordAk = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Collections;

namespace ArizaTakipYazılımSistemi.View
{
    public partial class KullaniciDetay : Form
    {
        public KullaniciDetay()
        {
            InitializeComponent();
        }
        private BilgiIslemYoneticiDB db = new BilgiIslemYoneticiDB();
        private void KullaniciDetay_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'arizaTakipDataSet3.tbl_unvan' table. You can move, or remove it, as needed.
            this.tbl_unvanTableAdapter1.Fill(this.arizaTakipDataSet3.tbl_unvan);
            // TODO: This line of code loads data into the 'arizaTakipDataSet2.tbl_bolum' table. You can move, or remove it, as needed.
            this.tbl_bolumTableAdapter1.Fill(this.arizaTakipDataSet2.tbl_bolum);
            panel1.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            this.Refresh();
            
            this.CenterToScreen();
            this.Text = Degiskenler.Admin.personel.adi.ToUpper() + " " + Degiskenler.Admin.personel.soyadi.ToUpper();
            textBox1.Text = Degiskenler.Admin.personel.adi;
            textBox2.Text = Degiskenler.Admin.personel.soyadi;
            textBox3.Text = Degiskenler.Admin.personel.username;
            textBox4.Text = Degiskenler.Admin.personel.password;
            comboBox1.SelectedValue = Degiskenler.Admin.personel.bolum_id;
            comboBox2.SelectedValue = Degiskenler.Admin.personel.unvan_id;
            checkBox1.Checked = Degiskenler.Admin.personel.yetki;
            if (Degiskenler.Admin.kullanici == false)
            {
                label1.Text = "Güncelleme veya silme yetkiniz bulunmamaktadır.";
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                checkBox1.Enabled = false;
                button11.Enabled = false;
                button2.Enabled = false;
            }
            bilgiIslemPersonel();
        }

        private void KullaniciDetay_FormClosed(object sender, FormClosedEventArgs e)
        {
            Degiskenler.Admin.personel = null;
            this.Close();
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult sil = new DialogResult();
            sil = MessageBox.Show(Degiskenler.Admin.personel.personel_id + " nolu personel bilgilerini silmek istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo);
            if (sil == DialogResult.Yes)
            {
                db.deletePersonel(Degiskenler.Admin.personel.personel_id);
                this.Close();
            }
        }

        private int tarihKarsilastirma(DateTime son_tarih, DateTime ilk_tarih)
        {
            return DateTime.Compare(son_tarih, ilk_tarih);
        }
        ErrorProvider err = new ErrorProvider();
        ArrayList colums;

        private void button3_Click(object sender, EventArgs e)
        {
            if (tarihKarsilastirma(dateTimePicker2.Value, dateTimePicker1.Value) == 1)
            {
                //raporlama yapılabilir
                DateTime tarih_bir = dateTimePicker1.Value;
                DateTime tarih_iki = dateTimePicker2.Value.AddDays(1);
                string isim = Degiskenler.Admin.personel.personel_id.ToString() + " numaralı personelin " + tarih_bir.ToString("dd/MM/yyyy") + "-" + tarih_iki.ToString("dd/MM/yyyy") + " tarihleri arasında bildirilen tüm arızaları";
                DataTable dt = new DataTable();
                dt.Clear();
                dt = rapor.ozelAtanmamis(tarih_bir.ToString("dd/MM/yyyy"), tarih_iki.ToString("dd/MM/yyyy"), Degiskenler.Admin.personel.personel_id);
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
                err.SetError(button3, "Tarih bilgisi doğru ayarlanmadı.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (tarihKarsilastirma(dateTimePicker2.Value, dateTimePicker1.Value) == 1)
            {
                //raporlama yapılabilir
                DateTime tarih_bir = dateTimePicker1.Value;
                DateTime tarih_iki = dateTimePicker2.Value.AddDays(1);
                string isim = Degiskenler.Admin.personel.personel_id.ToString() + " numaralı personelin " + tarih_bir.ToString("dd/MM/yyyy") + "-" + tarih_iki.ToString("dd/MM/yyyy") + " tarihleri arasında ataması gerçekleşen tüm arızaları";
                DataTable dt = new DataTable();
                dt.Clear();
                dt = rapor.ozelAtanmis(tarih_bir.ToString("dd/MM/yyyy"), tarih_iki.ToString("dd/MM/yyyy"), Degiskenler.Admin.personel.personel_id);
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
                err.SetError(button4, "Tarih bilgisi doğru ayarlanmadı.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (tarihKarsilastirma(dateTimePicker2.Value, dateTimePicker1.Value) == 1)
            {
                //raporlama yapılabilir
                DateTime tarih_bir = dateTimePicker1.Value;
                DateTime tarih_iki = dateTimePicker2.Value.AddDays(1);
                string isim = Degiskenler.Admin.personel.personel_id.ToString() + " numaralı personelin " + tarih_bir.ToString("dd/MM/yyyy") + "-" + tarih_iki.ToString("dd/MM/yyyy") + " tarihleri arasındaki tüm arızaları";
                DataTable dt = new DataTable();
                dt.Clear();
                dt = rapor.ozelTamamlanmis(tarih_bir, tarih_iki, Degiskenler.Admin.personel.personel_id);
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
                err.SetError(button5, "Tarih bilgisi doğru ayarlanmadı.");
            }
        }
        private Raporlama rapor = new Raporlama();
        
        private void button6_Click(object sender, EventArgs e)
        {
            //atanmamış
            string isim = Degiskenler.Admin.personel.personel_id.ToString() + " numaralı personelin ataması gerçekleşmemiş tüm arızaları";
            DataTable dt = new DataTable();
            dt.Clear();
            dt = rapor.atanmamisAriza();
            colums = new ArrayList();
            colums.Clear();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                colums.Add(dt.Columns[i]);
            }
            pdfSave(dt, isim);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //atanmış
            string isim = Degiskenler.Admin.personel.personel_id.ToString() + " numaralı personelin ataması gerçekleşmiş tüm arızaları";
            DataTable data = new DataTable();
            data.Clear();
            data.Columns.Clear();            
            data = rapor.atanmisAriza();
            colums = new ArrayList();
            colums.Clear();
            for (int i = 0; i < data.Columns.Count; i++)
            {
                colums.Add(data.Columns[i]);
            }
            pdfSave(data, isim);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //tüm 
            string isim = Degiskenler.Admin.personel.personel_id.ToString() + " numaralı personelin tüm arızaları";
            DataTable dt = new DataTable();
            dt.Clear();
            dt = rapor.tumAriza();
            colums = new ArrayList();
            colums.Clear();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                colums.Add(dt.Columns[i]);
            }
            pdfSave(dt, isim);
        }
        
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
            rich.Clear();
            rich.Text += "\t\t\t\t\t\t\t\t\t\t" + DateTime.Now.ToString("dd/MM/yyyy") + "\n\n\t\t" + isim.ToUpper();
            rich.Text += "\n\t\t" + pers.personel_id + "\n\t\t" + pers.adi + " " + pers.soyadi + "\n\t\t " + bol.bolum + "\n\t\t" + un.unvan + "\n\n\n";
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    rich.Text += colums[j]+": " + dataTable.Rows[i][j].ToString() + "\n";
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

        private void bilgiIslemPersonel()
        {
            if (Degiskenler.Admin.personel.bolum_id != 10000)
            {
                panel1.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                panel2.Visible = false;
                panel3.Visible = false;
            }
            else
            {
                panel2.Visible = true;
                panel3.Visible = true;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (tarihKarsilastirma(dateTimePicker2.Value, dateTimePicker1.Value) == 1)
            {
                //raporlama yapılabilir
                DateTime tarih_bir = dateTimePicker1.Value;
                DateTime tarih_iki = dateTimePicker2.Value.AddDays(1);
                string isim = Degiskenler.Admin.personel.personel_id.ToString() + " numaralı personelin " + tarih_bir.ToString("dd/MM/yyyy") + "-" + tarih_iki.ToString("dd/MM/yyyy") + " tarihleri arasında atanan arızalar";
                DataTable dt = new DataTable();
                dt.Clear();
                dt = rapor.ozelGiderilmemis(tarih_bir.ToString("dd/MM/yyyy"), tarih_iki.ToString("dd/MM/yyyy"), Degiskenler.Admin.personel.personel_id);
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
                err.SetError(button9, "Tarih bilgisi doğru ayarlanmadı.");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (tarihKarsilastirma(dateTimePicker2.Value, dateTimePicker1.Value) == 1)
            {
                //raporlama yapılabilir
                DateTime tarih_bir = dateTimePicker1.Value;
                DateTime tarih_iki = dateTimePicker2.Value.AddDays(1);
                string isim = Degiskenler.Admin.personel.personel_id.ToString() + " numaralı personelin " + tarih_bir.ToString("dd/MM/yyyy") + "-" + tarih_iki.ToString("dd/MM/yyyy") + " tarihleri arasında giderilen arızalar";
                DataTable dt = new DataTable();
                dt.Clear();
                dt = rapor.ozelGiderilmis(tarih_bir.ToString("dd/MM/yyyy"), tarih_iki.ToString("dd/MM/yyyy"), Degiskenler.Admin.personel.personel_id);
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
                err.SetError(button10, "Tarih bilgisi doğru ayarlanmadı.");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (tarihKarsilastirma(dateTimePicker2.Value, dateTimePicker1.Value) == 1)
            {
                //raporlama yapılabilir
                DateTime tarih_bir = dateTimePicker1.Value;
                DateTime tarih_iki = dateTimePicker2.Value.AddDays(1);
                string isim = Degiskenler.Admin.personel.personel_id.ToString() + " numaralı personelin " + tarih_bir.ToString("dd/MM/yyyy") + "-" + tarih_iki.ToString("dd/MM/yyyy") + " tarihleri arasında tüm arızalar";
                DataTable dt = new DataTable();
                dt.Clear();
                dt = rapor.ozelAtanmis(tarih_bir.ToString("dd/MM/yyyy"), tarih_iki.ToString("dd/MM/yyyy"), Degiskenler.Admin.personel.personel_id);
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
                err.SetError(button4, "Tarih bilgisi doğru ayarlanmadı.");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {  //raporlama yapılabilir giderilecek tüm arızalar
            string isim = Degiskenler.Admin.personel.personel_id.ToString() + " numaralı personelin adına atanan arızalar";
            DataTable dt = new DataTable();
            dt.Clear();
            dt = rapor.giderilmemis();
            colums = new ArrayList();
            colums.Clear();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                colums.Add(dt.Columns[i]);
            }
            pdfSave(dt, isim);            
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //raporlama yapılabilir giderilmiş tüm arızalar
            string isim = Degiskenler.Admin.personel.personel_id.ToString() + " numaralı personelin tamamlanan arızaları";
            DataTable dt = new DataTable();
            dt.Clear();
            dt = rapor.giderilmis();
            colums = new ArrayList();
            colums.Clear();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                colums.Add(dt.Columns[i]);
            }
            pdfSave(dt, isim);

        }

        private void button14_Click(object sender, EventArgs e)
        {
            //raporlama yapılabilir tüm arızalar
            string isim = Degiskenler.Admin.personel.personel_id.ToString() + " numaralı personelin adına atanan tüm arızalar";
            DataTable dt = new DataTable();
            dt.Clear();
            dt = rapor.tumAtananlar();
            colums = new ArrayList();
            colums.Clear();
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                colums.Add(dt.Columns[i]);
            }
            pdfSave(dt, isim);
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            Personel pers = new Personel();
            pers.personel_id = Degiskenler.Admin.personel.personel_id;
            pers.adi = textBox1.Text;
            pers.soyadi = textBox2.Text;
            pers.username = textBox3.Text;
            pers.password = textBox4.Text;
            pers.bolum_id = Convert.ToInt16(comboBox1.SelectedValue);
            pers.unvan_id = Convert.ToInt16(comboBox2.SelectedValue);
            pers.yetki = checkBox1.Checked;
            if (pers.adi == Degiskenler.Admin.personel.adi &&
                    pers.soyadi == Degiskenler.Admin.personel.soyadi &&
                    pers.unvan_id == Degiskenler.Admin.personel.unvan_id &&
                    pers.bolum_id == Degiskenler.Admin.personel.bolum_id &&
                    pers.username == Degiskenler.Admin.personel.username &&
                    pers.yetki == Degiskenler.Admin.personel.yetki &&
                    pers.password == Degiskenler.Admin.personel.password &&
                    pers.personel_id == Degiskenler.Admin.personel.personel_id)
                MessageBox.Show("Degişiklik algılanmadı");
            else
            {
                DialogResult sil = new DialogResult();
                sil = MessageBox.Show(Degiskenler.Admin.personel.personel_id + " nolu personel bilgilerini güncellemek istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo);
                if (sil == DialogResult.Yes)
                {
                    db.updatePersonel(pers);
                    this.Close();
                }
            }
        }
    }
}

