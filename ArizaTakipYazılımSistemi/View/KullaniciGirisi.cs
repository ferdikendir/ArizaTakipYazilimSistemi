using ArizaTakipYazılımSistemi.Controller;
using ArizaTakipYazılımSistemi.Model;
using System;
using System.Windows.Forms;

namespace ArizaTakipYazılımSistemi.View
{
    public partial class KullaniciGirisi : Form
    {
        public KullaniciGirisi()
        {
            InitializeComponent();
        }

        private void KullaniciGirisi_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DatabaseOperation db = new DatabaseOperation();
            Personel pers = new Personel();
            pers.username = textBox1.Text;
            pers.password = textBox2.Text;
            if(textBox1.Text.Equals("") == false && textBox2.Text.Equals("") == false)
            {
                int deger = db.kullaniciGirisi(pers);
                if(deger == 1)
                {
                    //tüm personeller
                    db.kullaniciVerileri();
                    PersonelEkrani personel_screen = new PersonelEkrani();
                    personel_screen.ShowDialog();
                }else if(deger == 3)
                {
                    db.kullaniciVerileri();
                    //it personel
                    BilgiIslemPersonel bilgi = new BilgiIslemPersonel();
                    bilgi.Show();
                }else if(deger == 4)
                {
                    //it yönetici
                    db.kullaniciVerileri();
                    BilgiIslemYonetici bilgi_islem = new BilgiIslemYonetici();
                    bilgi_islem.ShowDialog();
                    
                }else
                {
                    MessageBox.Show("Kullanıcı bulunamadı");
                }
                textBox1.Text = "";
                textBox2.Text = "";
            }
            else
            {
                MessageBox.Show("Kullanıcı adınıızı ve şifrenizi giriniz.");
            }
        }
    }
}
