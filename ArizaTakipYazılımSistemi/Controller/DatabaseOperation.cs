using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArizaTakipYazılımSistemi.Model;

namespace ArizaTakipYazılımSistemi.Controller
{
    class DatabaseOperation : Database
    {
        public DatabaseOperation()
        {
            dataAdapter1.SelectCommand = SelectCommand1;
            SelectCommand1.Connection = connection;

        }
        #region data

        public DataSet dataSet1 = new DataSet();
        public DataSet dataSet2 = new DataSet();
        public DataSet dataSet3 = new DataSet();
        public DataSet dataSet4 = new DataSet();
        public DataSet dataSet5 = new DataSet();
        public DataSet dataSet6 = new DataSet();
        public DataSet dataSet7 = new DataSet();
        public DataSet dataSet8 = new DataSet();
        public DataSet dataSet9 = new DataSet();
        public DataTable datatable1 = new DataTable();
        public DataTable datatable2 = new DataTable();
        public DataTable datatable3 = new DataTable();
        //select COUNT(personel_Id), personel_Id from tbl_ariza group by personel_Id, tamamlanma_saati having tamamlanma_saati is null

        #endregion

        public int kullaniciGirisi(Personel personel)
        {
            close();
            open();

            dataAdapter1.SelectCommand.CommandText = "select * from tbl_personel where username = '" + personel.username + "' and password = '" + personel.password + "'";
            dataAdapter1.Fill(dataSet1);
            if (dataSet1.Tables[0].Rows.Count > 0)
            {
                //dataset1
                personel.personel_id = Convert.ToInt32(dataSet1.Tables[0].Rows[0][0]);
                personel.adi = dataSet1.Tables[0].Rows[0][1].ToString();
                personel.soyadi = dataSet1.Tables[0].Rows[0][2].ToString();
                personel.bolum_id = Convert.ToInt32(dataSet1.Tables[0].Rows[0][3]);
                personel.unvan_id = Convert.ToInt32(dataSet1.Tables[0].Rows[0][4]);
                personel.yetki = Convert.ToBoolean(dataSet1.Tables[0].Rows[0][5]);
                Kullanici.personel = personel;
                close();
                if (personel.bolum_id != 10000)
                {
                    //personel ve yönetici
                    return 1;
                }else if (personel.yetki == false && personel.bolum_id == 10000)
                {
                    //it personel 
                    return 3;
                }
                else if (personel.yetki && personel.bolum_id == 10000)
                {
                    //it yönetici
                    return 4;
                }

            }
            return 0;
        }

        public void kullaniciVerileri()
        {
            open();
            dataSet2.Clear();
            dataSet3.Clear();
            dataSet4.Clear();
            dataSet5.Clear();
            dataSet6.Clear();
            Bolum bolum = new Bolum();
            Unvan unvan = new Unvan();
            //dataset2
            dataAdapter1.SelectCommand.CommandText = "select * from tbl_bolum where bolum_Id = " + Kullanici.personel.bolum_id + "";
            dataAdapter1.Fill(dataSet2);
            bolum.bolum = dataSet2.Tables[0].Rows[0][1].ToString();
            bolum.bolum_id = Convert.ToInt32(dataSet2.Tables[0].Rows[0][0]);
            //dataset3
            dataAdapter1.SelectCommand.CommandText = "select * from tbl_unvan where unvan_Id = " + Kullanici.personel.unvan_id + "";
            dataAdapter1.Fill(dataSet3);
            unvan.unvan = dataSet3.Tables[0].Rows[0][1].ToString();
            unvan.unvan_id = Convert.ToInt32(dataSet3.Tables[0].Rows[0][0]);
            Kullanici.personel_bolum = bolum;
            Kullanici.personel_unvan = unvan;
            //dataset4
            dataAdapter1.SelectCommand.CommandText = "select * from tbl_ariza where personel_Id = " + Kullanici.personel.personel_id + "";
            dataAdapter1.Fill(dataSet4);
            Kullanici.ariza_sayisi = dataSet4.Tables[0].Rows.Count;
            //dataset5
            dataAdapter1.SelectCommand.CommandText = "select * from tbl_ariza where personel_Id = " + Kullanici.personel.personel_id + " and atanma_saati is NULL";
            dataAdapter1.Fill(dataSet5);
            Kullanici.atama_bekleyen_ariza_sayisi = dataSet5.Tables[0].Rows.Count;
            //dataset6
            dataAdapter1.SelectCommand.CommandText = "select * from tbl_ariza where personel_Id = " + Kullanici.personel.personel_id + " and atanma_saati is not NULL and tamamlanma_saati is null";
            dataAdapter1.Fill(dataSet6);
            Kullanici.tamamlanmayi_ariza_sayisi = dataSet6.Tables[0].Rows.Count;
            close();
        }

        public void insertAriza(Ariza ariza)
        {
            //store procedure
            open();
            SqlCommand comm = new SqlCommand("ariza_ekle", connection);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("personel_id", SqlDbType.Int).Value = ariza.personel_id;
            comm.Parameters.Add("baslik", SqlDbType.NVarChar, 100).Value = ariza.baslik;
            comm.Parameters.Add("konu", SqlDbType.NVarChar, 10000).Value = ariza.konu;
            comm.ExecuteNonQuery();
            close();
        }

        public DataTable tamamlanan_arizalar()
        {
            open();
            dataAdapter1.SelectCommand.CommandText = "select * from tbl_ariza where personel_Id = " + Kullanici.personel.personel_id + " and mesaj is not null ";
            dataAdapter1.Fill(datatable1);
            close();
            return datatable1;
        }

        public DataTable tamamlanma_bekleyen()
        {
            open();
            dataAdapter1.SelectCommand.CommandText = "select * from tbl_ariza where personel_Id = " + Kullanici.personel.personel_id + " and atanma_saati is not null and tamamlanma_saati is null ";
            dataAdapter1.Fill(datatable2);
            close();
            return datatable2;
        }
        public DataTable atanmayi_bekleyen()
        {
            open();
            datatable3.Clear();
            dataAdapter1.SelectCommand.CommandText = "select *from tbl_ariza where personel_Id = " + Kullanici.personel.personel_id + " and atanma_saati is null";
            dataAdapter1.Fill(datatable3);
            close();
            return datatable3;
        }

        public string arizaPersonel(int personel_id)
        {

            string adsoyad;
            if (personel_id == 0)
                return "personel ataması gerçekleşmemiştir";
            open();
            dataAdapter1.SelectCommand.CommandText = "select * from tbl_personel where personel_Id = " + personel_id + "";
            dataAdapter1.Fill(dataSet7);
            adsoyad = dataSet7.Tables[0].Rows[0][1].ToString().ToUpper() + " " + dataSet7.Tables[0].Rows[0][2].ToString().ToUpper();
            close();
            return adsoyad;
        }

        public void arizaDetay(int ariza_id)
        {
            open();
            dataSet8.Clear();
            dataAdapter1.SelectCommand.CommandText = "select * from tbl_ariza where ariza_id = " + ariza_id + "";
            dataAdapter1.Fill(dataSet8);
            Ariza ariza = new Ariza();
            ariza.ariza_id = Convert.ToInt32(dataSet8.Tables[0].Rows[0][0]);
            ariza.baslik = dataSet8.Tables[0].Rows[0][2].ToString();
            ariza.konu = dataSet8.Tables[0].Rows[0][3].ToString();
            ariza.mesaj = dataSet8.Tables[0].Rows[0][4].ToString();
            ariza.bildirim_saati = Convert.ToDateTime(dataSet8.Tables[0].Rows[0][5].ToString());
            if (dataSet8.Tables[0].Rows[0][6].ToString().Equals("") == false)
                ariza.atanma_saati = Convert.ToDateTime(dataSet8.Tables[0].Rows[0][6].ToString());
            else
                ariza.atanma_saati = Convert.ToDateTime("19/9/1999");
            
            if (dataSet8.Tables[0].Rows[0][7].ToString().Equals("") == false)
                ariza.tamamlanma_saati = Convert.ToDateTime(dataSet8.Tables[0].Rows[0][7].ToString());
            else
                ariza.tamamlanma_saati = Convert.ToDateTime("19/9/1999");
            if (dataSet8.Tables[0].Rows[0][8].ToString().Equals("") == false)
                ariza.gideren_personel_id = Convert.ToInt32(dataSet8.Tables[0].Rows[0][8]);
            else
                ariza.gideren_personel_id = 0;
            Kullanici.ariza = ariza;
            close();
        }

        public bool arizaKontrol(string baslik)
        {
            open();
            dataAdapter1.SelectCommand.CommandText = "select * from tbl_ariza where baslik = '" + baslik + "'";
            dataAdapter1.Fill(dataSet9);
            close();
            if (dataSet9.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


    }
}
