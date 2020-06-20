using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArizaTakipYazılımSistemi.Model;
using System.Data.SqlClient;

namespace ArizaTakipYazılımSistemi.Controller
{
    class BilgiIslemDB : Database
    {
        public BilgiIslemDB()
        {
            dataAdapter1.SelectCommand = SelectCommand1;
            SelectCommand1.Connection = connection;
        }

        DataSet dataset1 = new DataSet();
        DataTable datatable1 = new DataTable();
        DataTable datatable2 = new DataTable();

        public void arizaBilgileri()
        {
            open();
            dataset1.Clear();
            dataAdapter1.SelectCommand.CommandText = "select * from tbl_ariza where gideren_personel_id = " + Kullanici.personel.personel_id + "";
            dataAdapter1.Fill(dataset1);
            Degiskenler.BilgiIslem.toplam_ariza = dataset1.Tables[0].Rows.Count;
            dataset1.Clear();
            dataAdapter1.SelectCommand.CommandText = "select * from tbl_ariza where gideren_personel_id = " + Kullanici.personel.personel_id + " and tamamlanma_saati is not null";
            dataAdapter1.Fill(dataset1);
            Degiskenler.BilgiIslem.tamamlanan_ariza = dataset1.Tables[0].Rows.Count;
            dataset1.Clear();
            dataAdapter1.SelectCommand.CommandText = "select * from tbl_ariza where gideren_personel_id = " + Kullanici.personel.personel_id + " and tamamlanma_saati is  null";
            dataAdapter1.Fill(dataset1);
            Degiskenler.BilgiIslem.tamamlanacak_ariza = dataset1.Tables[0].Rows.Count;
            dataset1.Clear();
            close();
        }
        public DataTable cozumBekleyen()
        {
            datatable1.Clear();
            open();
            dataAdapter1.SelectCommand.CommandText = "select ariza_id, baslik, konu, bildirim_saati,atanma_saati, CONCAT(tbl_personel.adi, ' ', tbl_personel.soyadi) as 'adsoyad', tbl_bolum.bolum, tbl_unvan.unvan from (((tbl_ariza INNER JOIN tbl_personel on tbl_personel.personel_Id = tbl_ariza.personel_Id) INNER JOIN tbl_bolum on tbl_bolum.bolum_Id = tbl_personel.bolum_Id) INNER JOIN tbl_unvan on tbl_unvan.unvan_Id = tbl_personel.unvan_Id) where gideren_personel_id = " + Kullanici.personel.personel_id + " and tamamlanma_saati is null";
            dataAdapter1.Fill(datatable1);
            close();
            return datatable1;
        }
        public DataTable cozumlenmis()
        {
            datatable2.Clear();
            open();
            dataAdapter1.SelectCommand.CommandText = "select ariza_id, baslik, konu,mesaj, bildirim_saati,tamamlanma_saati,  CONCAT(tbl_personel.adi, ' ', tbl_personel.soyadi) as 'adsoyad', tbl_bolum.bolum, tbl_unvan.unvan from (((tbl_ariza INNER JOIN tbl_personel on tbl_personel.personel_Id = tbl_ariza.personel_Id) INNER JOIN tbl_bolum on tbl_bolum.bolum_Id = tbl_personel.bolum_Id) INNER JOIN tbl_unvan on tbl_unvan.unvan_Id = tbl_personel.unvan_Id) where gideren_personel_id = " + Kullanici.personel.personel_id + " and tamamlanma_saati is not null";
            dataAdapter1.Fill(datatable2);
            close();
            return datatable2;
        }
        public void arizaDetay(int ariza_id)
        {
            open();
            dataset1.Clear();
            dataAdapter1.SelectCommand.CommandText = "select * from tbl_ariza where ariza_id = " + ariza_id + "";
            dataAdapter1.Fill(dataset1);
            Ariza ariza = new Ariza();
            ariza.ariza_id = Convert.ToInt32(dataset1.Tables[0].Rows[0][0]);
            ariza.baslik = dataset1.Tables[0].Rows[0][2].ToString();
            ariza.konu = dataset1.Tables[0].Rows[0][3].ToString();
            ariza.mesaj = dataset1.Tables[0].Rows[0][4].ToString();
            ariza.bildirim_saati = Convert.ToDateTime(dataset1.Tables[0].Rows[0][5].ToString());
            ariza.atanma_saati = Convert.ToDateTime(dataset1.Tables[0].Rows[0][6].ToString());
            if (dataset1.Tables[0].Rows[0][7].ToString().Equals("") == false)
                ariza.tamamlanma_saati = Convert.ToDateTime(dataset1.Tables[0].Rows[0][7].ToString());
            else
                ariza.tamamlanma_saati = Convert.ToDateTime("19/9/1999");

            ariza.gideren_personel_id = Kullanici.personel.personel_id;

            Kullanici.ariza = ariza;
            close();
        }
        public void cozumEkle(int ariza_id, string mesaj)
        {
            open();
            SqlCommand comm = new SqlCommand("update_ariza", connection);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.Add("ariza_id", SqlDbType.Int).Value = ariza_id;
            comm.Parameters.Add("mesaj", SqlDbType.NVarChar, 10000).Value = mesaj;
            comm.ExecuteNonQuery();
            close();
        }

    }
}
