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
    public partial class PersonelAtama : Form
    {
        public PersonelAtama()
        {
            InitializeComponent();
        }
        Dictionary<int, string> personel = new Dictionary<int, string>();
        BilgiIslemYoneticiDB db = new BilgiIslemYoneticiDB();
        private void PersonelAtama_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox1.Text = Yonetici.secilen_ariza.ariza_id.ToString();
            textBox2.Text = Yonetici.secilen_ariza.baslik;
            textBox3.Text = Yonetici.secilen_ariza.konu;

            personel.Add(0, "-----seçiniz");
            int id;
            string adsoyad;
            foreach (DataRow row in db.personel().Rows)
            {
                id = Convert.ToInt32(row["personel_Id"]);
                adsoyad = row["adsoyad"].ToString().ToUpper();
                if (!personel.ContainsKey(id))
                {
                    int ariza_sayisi = db.arizaSayisi(id);
                    if (ariza_sayisi == 0)
                        personel.Add(id, adsoyad + " (**)");
                    else
                        personel.Add(id, adsoyad + " ("+ ariza_sayisi.ToString()+ " adet arıza atanmış)" );
                }
            }
            comboBox1.DataSource = new BindingSource(personel, null);
            comboBox1.DisplayMember = "Value";
            comboBox1.ValueMember = "Key";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(comboBox1.SelectedValue.ToString()) != 0)
            {
                db.personelAta(Yonetici.secilen_ariza.ariza_id, Convert.ToInt32(comboBox1.SelectedValue.ToString()));
                personel.Clear();
                Yonetici.secilen_ariza = null;
                this.Close();
                //this.Dispose();
            }
            else
            {
                MessageBox.Show("personel seçiniz.");
            }
        }
    }
}
