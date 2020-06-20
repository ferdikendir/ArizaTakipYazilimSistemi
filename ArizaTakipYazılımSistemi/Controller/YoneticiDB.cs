using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArizaTakipYazılımSistemi.Model;

namespace ArizaTakipYazılımSistemi.Controller
{
    class YoneticiDB : DatabaseOperation
    {
        public YoneticiDB()
        {
            dataAdapter1.SelectCommand = SelectCommand1;
            SelectCommand1.Connection = connection;
        }

        public DataSet dataSet10 = new DataSet();
        public DataTable datatable4 = new DataTable();
        public DataTable datatable5 = new DataTable();
        public DataTable datatable6 = new DataTable();
        public DataTable datatable7 = new DataTable();

        public DataTable atanmamisAriza()
        {
            datatable4.Clear();
            dataAdapter1.SelectCommand.CommandText = "select tbl_ariza.ariza_id, tbl_ariza.baslik, tbl_ariza.konu, tbl_ariza.bildirim_saati, CONCAT(tbl_personel.adi, ' ', tbl_personel.soyadi) as 'adsoyad' from (tbl_ariza inner join tbl_personel on tbl_ariza.personel_Id = tbl_personel.personel_Id and tbl_personel.bolum_Id = " + Kullanici.personel.bolum_id + ") where atanma_saati is null";
            dataAdapter1.Fill(datatable4);
            return datatable4;
        }
        public DataTable atanmisAriza()
        {
            datatable5.Clear();
            dataAdapter1.SelectCommand.CommandText = "select tbl_ariza.ariza_id, tbl_ariza.baslik, tbl_ariza.konu, tbl_ariza.bildirim_saati,tbl_ariza.atanma_saati,tbl_ariza.tamamlanma_saati, CONCAT(tbl_personel.adi, ' ', tbl_personel.soyadi) as 'adsoyad' from(tbl_ariza inner join tbl_personel on tbl_ariza.personel_Id = tbl_personel.personel_Id and tbl_personel.bolum_Id = " + Kullanici.personel.bolum_id + ") where atanma_saati is not null ";
            dataAdapter1.Fill(datatable5);
            return datatable5;
        }
        public DataTable giderilmisAriza()
        {
            datatable6.Clear();
            dataAdapter1.SelectCommand.CommandText = "select tbl_ariza.ariza_id, tbl_ariza.baslik, tbl_ariza.konu, tbl_ariza.mesaj, tbl_ariza.bildirim_saati,tbl_ariza.atanma_saati, tbl_ariza.tamamlanma_saati, CONCAT(tbl_personel.adi, ' ', tbl_personel.soyadi) as 'adsoyad' from(tbl_ariza inner join tbl_personel on tbl_ariza.personel_Id = tbl_personel.personel_Id and tbl_personel.bolum_Id = " + Kullanici.personel.bolum_id + ") where tamamlanma_saati is not null ";
            dataAdapter1.Fill(datatable6);
            return datatable6;
        }
        public void secilenPersonel(int id)
        {
            dataSet10.Clear();
            open();
            dataAdapter1.SelectCommand.CommandText = "Select * from tbl_personel where personel_Id = " + id + "";
            dataAdapter1.Fill(dataSet10);
            close();
            Personel personel = new Personel();
            personel.personel_id = Convert.ToInt32(dataSet10.Tables[0].Rows[0][0]);
            personel.adi = dataSet10.Tables[0].Rows[0][1].ToString();
            personel.soyadi = dataSet10.Tables[0].Rows[0][2].ToString();
            personel.bolum_id = Convert.ToInt32(dataSet10.Tables[0].Rows[0][3]);
            personel.unvan_id = Convert.ToInt32(dataSet10.Tables[0].Rows[0][4]);
            personel.yetki = Convert.ToBoolean(dataSet10.Tables[0].Rows[0][5]);
            personel.username = dataSet10.Tables[0].Rows[0][6].ToString();
            personel.password = dataSet10.Tables[0].Rows[0][7].ToString();
            Degiskenler.Admin.personel = personel;

        }
        public DataTable bolum_kullanicilar()
        {
            datatable7.Clear();
            open();
            dataAdapter1.SelectCommand.CommandText = "select * from ((tbl_personel inner join tbl_bolum on tbl_personel.bolum_Id = tbl_bolum.bolum_Id) inner join tbl_unvan on tbl_personel.unvan_Id = tbl_unvan.unvan_Id) where tbl_personel.bolum_Id = "+ Kullanici.personel.bolum_id+"";
            dataAdapter1.Fill(datatable7);
            close();
            return datatable7;
        }
    }
}
