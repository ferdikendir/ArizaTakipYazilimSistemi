using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArizaTakipYazılımSistemi.Model;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ArizaTakipYazılımSistemi.Controller
{
    class Database
    {
        protected SqlConnection connection;
        protected string server_address;
        protected SqlDataAdapter dataAdapter1;
        protected SqlCommand SelectCommand1;
        public Database()
        {
            dataAdapter1 = new SqlDataAdapter();
            SelectCommand1 = new SqlCommand();
            connection = new SqlConnection();
            server_address = "Data Source=FKENDIR\\FKENDIR;Initial Catalog=arizaTakip;Integrated Security=True";
            connection.ConnectionString = server_address;
        }
        public void close()
        {
            connection.Close();
        }
        public void open()
        {
            connection.Open();
        }
        public void updatePersonel(Personel personel)
        {
            open();
            SqlCommand comm = new SqlCommand("update_personel", connection);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("personel_id", SqlDbType.Int).Value = personel.personel_id;
            comm.Parameters.Add("adi1", SqlDbType.NVarChar, 20).Value = personel.adi;
            comm.Parameters.Add("soyadi1", SqlDbType.NVarChar, 20).Value = personel.soyadi;
            comm.Parameters.Add("bolum_id", SqlDbType.Int).Value = personel.bolum_id;
            comm.Parameters.Add("unvan_id", SqlDbType.Int).Value = personel.unvan_id;
            comm.Parameters.Add("yetki", SqlDbType.Bit).Value = personel.yetki;
            comm.Parameters.Add("username", SqlDbType.NVarChar, 10).Value = personel.username;
            comm.Parameters.Add("password", SqlDbType.NVarChar, 10).Value = personel.password;
            comm.ExecuteNonQuery();
            close();
        }

        public void raporOlustur()
        {

        }

    }
}
