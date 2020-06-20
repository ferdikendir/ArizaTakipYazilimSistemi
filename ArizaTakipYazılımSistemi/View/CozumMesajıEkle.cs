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
    public partial class CozumMesajıEkle : Form
    {
        public CozumMesajıEkle()
        {
            InitializeComponent();
        }

        private void CozumMesajıEkle_Load(object sender, EventArgs e)
        {
            this.Text = Kullanici.ariza.ariza_id + " numaralı arıza için çözüm mesajı";
            this.CenterToScreen();
        }
        BilgiIslemDB bilgi = new BilgiIslemDB();
        ErrorProvider err = new ErrorProvider();
        private void button1_Click(object sender, EventArgs e)
        {
            err = null;
            if(textBox1.Text != "")
            {
                bilgi.cozumEkle(Kullanici.ariza.ariza_id,textBox1.Text);
                this.Close();
            }
            else
            {
                err.SetError(textBox1,"Bir çözüm mesajı giriniz.");
            }
        }
    }
}
