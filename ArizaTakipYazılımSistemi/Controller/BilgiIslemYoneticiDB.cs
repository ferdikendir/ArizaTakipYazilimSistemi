using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArizaTakipYazılımSistemi.Model;

namespace ArizaTakipYazılımSistemi.Controller
{
    class BilgiIslemYoneticiDB : Database
    {
        public BilgiIslemYoneticiDB()
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

        public DataTable datatable1 = new DataTable();
        public DataTable datatable2 = new DataTable();

        public DataTable datatable3 = new DataTable();
        public DataTable datatable4 = new DataTable();
        public DataTable datatable5 = new DataTable();

        public DataTable datatable6 = new DataTable();
        public DataTable datatable7 = new DataTable();

        #endregion
        public DataTable atanmaBekleyen()
        {
            open();
            datatable1.Clear();
            dataAdapter1.SelectCommand.CommandText = "select ariza_id, baslik, konu, bildirim_saati, CONCAT(tbl_personel.adi, ' ', tbl_personel.soyadi) as 'adsoyad', tbl_bolum.bolum, tbl_unvan.unvan from (((tbl_ariza INNER JOIN tbl_personel on tbl_personel.personel_Id = tbl_ariza.personel_Id) INNER JOIN tbl_bolum on tbl_bolum.bolum_Id = tbl_personel.bolum_Id) INNER JOIN tbl_unvan on tbl_unvan.unvan_Id = tbl_personel.unvan_Id) where atanma_saati is null";
            dataAdapter1.Fill(datatable1);
            close();
            return datatable1;
        }
        public DataTable tamamlanan()
        {
            open();
            datatable2.Clear();
            dataAdapter1.SelectCommand.CommandText = "select ariza_id, baslik, konu,mesaj, bildirim_saati, CONCAT(tbl_personel.adi, ' ', tbl_personel.soyadi) as 'adsoyad', tbl_bolum.bolum, tbl_ariza.tamamlanma_saati from(((tbl_ariza inner join tbl_personel on tbl_ariza.personel_Id = tbl_personel.personel_Id) inner join tbl_bolum on tbl_personel.bolum_Id = tbl_bolum.bolum_Id) inner join tbl_unvan on tbl_personel.unvan_Id = tbl_unvan.unvan_Id) where tbl_ariza.tamamlanma_saati is not null";
            dataAdapter1.Fill(datatable2);
            close();
            return datatable2;
        }
        public void arizaDetay(int ariza_id)
        {
            open();
            dataSet1.Clear();
            dataAdapter1.SelectCommand.CommandText = "select * from tbl_ariza where ariza_id = " + ariza_id + "";
            dataAdapter1.Fill(dataSet1);
            Ariza ariza = new Ariza();
            ariza.ariza_id = Convert.ToInt32(dataSet1.Tables[0].Rows[0][0]);
            ariza.baslik = dataSet1.Tables[0].Rows[0][2].ToString();
            ariza.konu = dataSet1.Tables[0].Rows[0][3].ToString();
            ariza.mesaj = dataSet1.Tables[0].Rows[0][4].ToString();
            ariza.bildirim_saati = Convert.ToDateTime(dataSet1.Tables[0].Rows[0][5].ToString());
            if (dataSet1.Tables[0].Rows[0][6].ToString().Equals("") == false)
                ariza.atanma_saati = Convert.ToDateTime(dataSet1.Tables[0].Rows[0][6].ToString());
            if (dataSet1.Tables[0].Rows[0][7].ToString().Equals("") == false)
                ariza.tamamlanma_saati = Convert.ToDateTime(dataSet1.Tables[0].Rows[0][7].ToString());
            if (dataSet1.Tables[0].Rows[0][8].ToString().Equals("") == false)
                ariza.gideren_personel_id = Convert.ToInt16(dataSet1.Tables[0].Rows[0][8]);
            Yonetici.secilen_ariza = ariza;
            close();
        }
        public string arizaPersonel(int personel_id)
        {
            string adsoyad;
            open();
            dataAdapter1.SelectCommand.CommandText = "select * from tbl_personel where personel_Id = " + personel_id + "";
            dataAdapter1.Fill(dataSet2);
            adsoyad = dataSet2.Tables[0].Rows[0][1].ToString().ToUpper() + " " + dataSet2.Tables[0].Rows[0][2].ToString().ToUpper();
            close();
            return adsoyad;
        }
        public DataTable tamamlanmaBekleyen()
        {
            open();
            datatable3.Clear();
            dataAdapter1.SelectCommand.CommandText = "select ariza_id, baslik, konu, atanma_saati, CONCAT(tbl_personel.adi, ' ', tbl_personel.soyadi) as 'adsoyad', tbl_bolum.bolum tbl_ariza from(((tbl_ariza inner join tbl_personel on tbl_ariza.personel_Id = tbl_personel.personel_Id) inner join tbl_bolum on tbl_personel.bolum_Id = tbl_bolum.bolum_Id) inner join tbl_unvan on tbl_personel.unvan_Id = tbl_unvan.unvan_Id) where tbl_ariza.tamamlanma_saati is null and atanma_saati is not null";
            dataAdapter1.Fill(datatable3);
            close();
            return datatable3;
        }
        public DataTable personel()
        {
            open();
            datatable3.Clear();
            dataAdapter1.SelectCommand.CommandText = "  select personel_Id, CONCAT(adi, ' ', soyadi) as 'adsoyad' from tbl_personel where bolum_Id = 10000 and yetki = 'false'";
            dataAdapter1.Fill(datatable3);
            close();
            return datatable3;
        }
        public int arizaSayisi(int persId)
        {
            dataSet5.Clear();
            open();
            dataAdapter1.SelectCommand.CommandText = "select COUNT(ariza_id) from tbl_ariza where gideren_personel_id = " + persId + " and atanma_saati is not null and tamamlanma_saati is null";
            dataAdapter1.Fill(dataSet5);
            close();
            return Convert.ToInt16(dataSet5.Tables[0].Rows[0][0]);
        }
        public void personelAta(int ariza_id, int personel_id)
        {
            open();
            SqlCommand comm = new SqlCommand("ariza_personel_ekle", connection);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("ariza_id", SqlDbType.Int).Value = ariza_id;
            comm.Parameters.Add("gideren_pers_id", SqlDbType.Int).Value = personel_id;
            comm.ExecuteNonQuery();
            close();
        }
        public void ınsertPersonel(Personel personel)
        {
            open();
            SqlCommand comm = new SqlCommand("personel_ekle", connection);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("adi", SqlDbType.NVarChar, 20).Value = personel.adi;
            comm.Parameters.Add("soyadi", SqlDbType.NVarChar, 20).Value = personel.soyadi;
            comm.Parameters.Add("bolum_id", SqlDbType.Int).Value = personel.bolum_id;
            comm.Parameters.Add("unvan_id", SqlDbType.Int).Value = personel.unvan_id;
            comm.Parameters.Add("yetki", SqlDbType.Bit).Value = personel.yetki;
            comm.Parameters.Add("username", SqlDbType.NVarChar, 10).Value = personel.username;
            comm.Parameters.Add("password", SqlDbType.NVarChar, 10).Value = personel.password;
            comm.ExecuteNonQuery();
            close();

        }
        public bool kontrolPersonel(Personel personel)
        {
            dataSet3.Clear();
            open();
            dataAdapter1.SelectCommand.CommandText = "select * from tbl_personel where (adi = '" + personel.adi + "' and soyadi = '" + personel.soyadi + "' and bolum_Id = " + personel.bolum_id + " and unvan_Id=" + personel.unvan_id + ") or (username = '" + personel.username + "')";
            dataAdapter1.Fill(dataSet3);
            close();
            if (dataSet3.Tables[0].Rows.Count > 0)
                return false;
            return true;
        }
        public DataTable bolum()
        {
            datatable5.Clear();
            open();
            dataAdapter1.SelectCommand.CommandText = "select *from tbl_bolum";
            dataAdapter1.Fill(datatable5);
            close();
            return datatable5;
        }
        public DataTable unvan()
        {
            datatable6.Clear();
            open();
            dataAdapter1.SelectCommand.CommandText = "select * from tbl_unvan";
            dataAdapter1.Fill(datatable6);
            close();
            return datatable6;
        }
        public DataTable kullanicilar()
        {
            datatable7.Clear();
            open();
            dataAdapter1.SelectCommand.CommandText = "select * from ((tbl_personel inner join tbl_bolum on tbl_personel.bolum_Id = tbl_bolum.bolum_Id) inner join tbl_unvan on tbl_personel.unvan_Id = tbl_unvan.unvan_Id)";
            dataAdapter1.Fill(datatable7);
            close();
            return datatable7;
        }
        public bool bolumSorgula(string bolum)
        {
            dataSet4.Clear();
            open();
            dataAdapter1.SelectCommand.CommandText = "select * from tbl_bolum where bolum = '" + bolum + "'";
            dataAdapter1.Fill(dataSet4);
            close();
            if (dataSet4.Tables[0].Rows.Count > 0)
                return false;
            else
                return true;
        }
        public void insertBolum(string bolum)
        {
            open();
            SqlCommand comm = new SqlCommand("bolum_ekle", connection);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("bolum", SqlDbType.NVarChar, 20).Value = bolum;
            comm.ExecuteNonQuery();
            close();
        }
        public bool unvanSorgula(string unvan)
        {
            dataSet4.Clear();
            open();
            dataAdapter1.SelectCommand.CommandText = "select * from tbl_unvan where unvan ='" + unvan + "'";
            dataAdapter1.Fill(dataSet4);
            close();
            if (dataSet4.Tables[0].Rows.Count > 0)
                return false;
            return true;
        }
        public void insertUnvan(string unvan)
        {
            open();
            SqlCommand comm = new SqlCommand("unvan_ekle", connection);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("unvan", SqlDbType.NVarChar, 20).Value = unvan;
            comm.ExecuteNonQuery();
            close();
        }
        public void secilenPersonel(int id)
        {
            dataSet4.Clear();
            open();
            dataAdapter1.SelectCommand.CommandText = "Select * from tbl_personel where personel_Id = " + id + "";
            dataAdapter1.Fill(dataSet4);
            Personel personel = new Personel();
            personel.personel_id = Convert.ToInt32(dataSet4.Tables[0].Rows[0][0]);
            personel.adi = dataSet4.Tables[0].Rows[0][1].ToString();
            personel.soyadi = dataSet4.Tables[0].Rows[0][2].ToString();
            personel.bolum_id = Convert.ToInt32(dataSet4.Tables[0].Rows[0][3]);
            personel.unvan_id = Convert.ToInt32(dataSet4.Tables[0].Rows[0][4]);
            personel.yetki = Convert.ToBoolean(dataSet4.Tables[0].Rows[0][5]);
            personel.username = dataSet4.Tables[0].Rows[0][6].ToString();
            personel.password = dataSet4.Tables[0].Rows[0][7].ToString();
            Degiskenler.Admin.personel = personel;
            
            close();
        }
        public void bolum_sil(int bolum_id)
        {
            open();
            SqlCommand comm = new SqlCommand("bolum_sil", connection);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("bolum_id", SqlDbType.Int).Value = bolum_id;
            comm.ExecuteNonQuery();
            close();
        }
        public void unvan_sil(int unvan_id)
        {
            open();
            SqlCommand comm = new SqlCommand("unvan_sil", connection);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("unvan_id", SqlDbType.Int).Value = unvan_id;
            comm.ExecuteNonQuery();
            close();
        }
        public void deletePersonel(int personel_id)
        {
            open();
            SqlCommand comm = new SqlCommand("delete_personel", connection);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("personel_id", SqlDbType.Int).Value = personel_id;
            comm.ExecuteNonQuery();
            close();
        }
    }

}
