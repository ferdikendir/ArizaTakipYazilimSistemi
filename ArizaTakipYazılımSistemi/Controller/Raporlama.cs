using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ArizaTakipYazılımSistemi.Degiskenler;

namespace ArizaTakipYazılımSistemi.Controller
{
    class Raporlama : Database
    {
        DataTable ariza = new DataTable();
        DataSet data = new DataSet();
        DataSet data1 = new DataSet();
        public Raporlama()
        {
            dataAdapter1.SelectCommand = SelectCommand1;
            SelectCommand1.Connection = connection;

        }
        public string bolum(int bolum_id)
        {
            data.Clear();
            dataAdapter1.SelectCommand.CommandText = "select * from tbl_bolum where bolum_Id = " + bolum_id + "";
            dataAdapter1.Fill(data);
            return data.Tables[0].Rows[0][1].ToString();
        }
        public string unvan(int unvan_id)
        {
            data1.Clear();
            dataAdapter1.SelectCommand.CommandText = "select * from tbl_unvan where unvan_Id = " + unvan_id + "";
            dataAdapter1.Fill(data1);
            return data1.Tables[0].Rows[0][1].ToString();
        }
        //diğer bölüm personel
        public DataTable atanmamisAriza()
        {
            open();
            ariza.Clear();
            dataAdapter1.SelectCommand.CommandText = "select tbl_ariza.ariza_id as 'Arıza Numarası', tbl_ariza.baslik as 'Arıza Başlığı', tbl_ariza.konu as 'Arıza konusu',tbl_ariza.bildirim_saati as 'Bildirim Saati', CONCAT(tbl_personel.adi, ' ', tbl_personel.soyadi) as 'Bildiren Personel',tbl_bolum.bolum as 'Bölüm', tbl_unvan.unvan as 'Ünvan'from(((tbl_ariza inner join tbl_personel on tbl_personel.personel_Id = tbl_ariza.personel_Id)inner join tbl_bolum on tbl_bolum.bolum_Id = tbl_personel.bolum_Id ) inner join tbl_unvan on tbl_unvan.unvan_Id = tbl_personel.unvan_Id) where tbl_ariza.personel_Id =" + Admin.personel.personel_id + " and atanma_saati is null";
            dataAdapter1.Fill(ariza);
            close();
            return ariza;
        }
        public DataTable atanmisAriza()
        {
            open();
            ariza.Clear();
            dataAdapter1.SelectCommand.CommandText = "select tbl_ariza.ariza_id as 'Arıza Numarası', tbl_ariza.baslik as 'Arıza Başlığı', tbl_ariza.konu as 'Arıza Konusu',tbl_ariza.bildirim_saati as 'Bildirim Saati', CONCAT(tbl_personel.adi, ' ', tbl_personel.soyadi) as 'Bildiren Personel',tbl_bolum.bolum as 'Bölüm', tbl_unvan.unvan as 'Ünvan'from(((tbl_ariza inner join tbl_personel on tbl_personel.personel_Id = tbl_ariza.personel_Id)inner join tbl_bolum on tbl_bolum.bolum_Id = tbl_personel.bolum_Id ) inner join tbl_unvan on tbl_unvan.unvan_Id = tbl_personel.unvan_Id) where tbl_ariza.personel_Id =" + Admin.personel.personel_id + " and atanma_saati is not null";
            dataAdapter1.Fill(ariza);
            close();
            return ariza;
        }
        public DataTable tumAriza()
        {
            open();
            ariza.Clear();
            dataAdapter1.SelectCommand.CommandText = "select tbl_ariza.ariza_id as 'Arıza Numarası', tbl_ariza.baslik as 'Arıza Başlığı', tbl_ariza.konu as 'Arıza konusu',tbl_ariza.bildirim_saati as 'Bildirim Saati', CONCAT(tbl_personel.adi, ' ', tbl_personel.soyadi) as 'Bildiren Personel',tbl_bolum.bolum as 'Bölüm', tbl_unvan.unvan as 'Ünvan'from(((tbl_ariza inner join tbl_personel on tbl_personel.personel_Id = tbl_ariza.personel_Id)inner join tbl_bolum on tbl_bolum.bolum_Id = tbl_personel.bolum_Id ) inner join tbl_unvan on tbl_unvan.unvan_Id = tbl_personel.unvan_Id) where tbl_ariza.personel_Id =" + Admin.personel.personel_id + " ";
            dataAdapter1.Fill(ariza);
            close();
            return ariza;
        }
        public DataTable ozelAtanmamis(string tarih_bir, string tarih_iki, int personel_id)
        {
            open();
            ariza.Clear();
            dataAdapter1.SelectCommand.CommandText = "select ariza_id as 'Arıza Numarası', baslik as 'Arıza Başlığı', konu as 'Konu', bildirim_saati as 'Bildirim Saati' from tbl_ariza where bildirim_saati between convert(datetime, '" + tarih_bir + "', 103) and convert(datetime, '" + tarih_bir + "', 103) and personel_Id = " + personel_id + "";
            dataAdapter1.Fill(ariza);
            close();
            return ariza;
        }
        public DataTable ozelAtanmis(string tarih_bir, string tarih_iki, int personel_id)
        {
            open();
            ariza.Clear();
            dataAdapter1.SelectCommand.CommandText = "select ariza_id as 'Arıza Numarası', baslik as 'Arıza Başlığı', konu as 'Konu', bildirim_saati as 'Bildirim Saati', atanma_saati as 'Atanma Saati' from tbl_ariza where atanma_saati between convert(datetime, '" + tarih_bir + "', 103) and convert(datetime, '" + tarih_iki + "', 103) and personel_Id = " + personel_id + "";
            dataAdapter1.Fill(ariza);
            close();
            return ariza;
        }
        public DataTable ozelTamamlanmis(DateTime tarih_bir, DateTime tarih_iki, int personel_id)
        {
            open();
            ariza.Clear();
            dataAdapter1.SelectCommand.CommandText = " select ariza_id as 'Arıza Numarası', baslik as 'Arıza Başlığı', konu as 'Arıza Konusu', bildirim_saati as 'Bildirim Saati', atanma_saati as 'Atanma Saati', tamamlanma_saati as 'Tamamlanma Saati', mesaj as 'Çözüm Mesajı' from tbl_ariza where tamamlanma_saati between convert(datetime, '" + tarih_bir + "', 103) and convert(datetime, '" + tarih_iki + "', 103) and personel_Id = " + personel_id + " ";
            dataAdapter1.Fill(ariza);
            close();
            return ariza;
        }
        //bilgi işlem personel
        public DataTable giderilmemis()
        {
            string query = " select ariza_id, baslik, konu, bildirim_saati, atanma_saati, CONCAT(adi, ' ', soyadi) as 'ad soyad', bolum, unvan  from(((tbl_ariza inner join tbl_personel on tbl_ariza.personel_Id = tbl_personel.personel_Id) inner join tbl_bolum on tbl_personel.bolum_Id = tbl_bolum.bolum_Id) inner join tbl_unvan on tbl_unvan.unvan_Id = tbl_personel.unvan_Id) where gideren_personel_id = " + Degiskenler.Admin.personel.personel_id + " and tamamlanma_saati is null";
            dataAdapter1.SelectCommand.CommandText = query;
            dataAdapter1.Fill(ariza);
            return ariza;
        }
        public DataTable giderilmis()
        {
            string query = " select ariza_id, baslik, konu, bildirim_saati, atanma_saati, tamamlanma_saati, mesaj, CONCAT(adi, ' ', soyadi), bolum, unvan  from(((tbl_ariza inner join tbl_personel on tbl_ariza.personel_Id = tbl_personel.personel_Id) inner join tbl_bolum on tbl_personel.bolum_Id = tbl_bolum.bolum_Id) inner join tbl_unvan on tbl_unvan.unvan_Id = tbl_personel.unvan_Id) where gideren_personel_id = " + Degiskenler.Admin.personel.personel_id + " and tamamlanma_saati is not null";
            dataAdapter1.SelectCommand.CommandText = query;
            dataAdapter1.Fill(ariza);
            return ariza;
        }
        public DataTable tumAtananlar()
        {
            string query = " select ariza_id as 'Arıza Numarası', baslik as 'Başlık', konu as 'Konu', bildirim_saati as 'Bildirim Saati', atanma_saati as 'Atanma Saati', tamamlanma_saati as 'Tamamlanma Saati', mesaj as 'Çözüm Mesajı', CONCAT(adi, ' ', soyadi) as 'Bildiren Personel', bolum as 'Bölüm', unvan as 'Ünvan'  from(((tbl_ariza inner join tbl_personel on tbl_ariza.personel_Id = tbl_personel.personel_Id) inner join tbl_bolum on tbl_personel.bolum_Id = tbl_bolum.bolum_Id) inner join tbl_unvan on tbl_unvan.unvan_Id = tbl_personel.unvan_Id) where gideren_personel_id = " + Degiskenler.Admin.personel.personel_id + ";";
            dataAdapter1.SelectCommand.CommandText = query;
            dataAdapter1.Fill(ariza);
            return ariza;
        }
        public DataTable ozelGiderilmemis(string tarih_bir, string tarih_iki, int personel_id)
        {
            ariza.Clear();
            string query = "select ariza_id, baslik, konu, bildirim_saati, atanma_saati , CONCAT(adi, ' ', soyadi) as 'ad soyad', bolum, unvan from(((tbl_ariza inner join tbl_personel on tbl_ariza.personel_Id = tbl_personel.personel_Id) inner join tbl_bolum on tbl_personel.bolum_Id = tbl_bolum.bolum_Id) inner join tbl_unvan on tbl_unvan.unvan_Id = tbl_personel.unvan_Id) where gideren_personel_id = '"+ personel_id + "' and atanma_saati between convert(datetime, '" +tarih_bir+"', 103) and convert(datetime, '"+tarih_iki+"', 103)";
            dataAdapter1.SelectCommand.CommandText = query;
            dataAdapter1.Fill(ariza);
            return ariza;
        }
        public DataTable ozelGiderilmis(string tarih_bir, string tarih_iki, int personel_id)
        {
            ariza.Clear();
            string query = "select ariza_id, baslik, konu, bildirim_saati, atanma_saati, tamamlanma_saati, mesaj , CONCAT(adi, ' ', soyadi) as 'ad soyad', bolum, unvan from(((tbl_ariza inner join tbl_personel on tbl_ariza.personel_Id = tbl_personel.personel_Id) inner join tbl_bolum on tbl_personel.bolum_Id = tbl_bolum.bolum_Id) inner join tbl_unvan on tbl_unvan.unvan_Id = tbl_personel.unvan_Id) where gideren_personel_id = '" + personel_id + "' and tamamlanma_saati between convert(datetime, '" + tarih_bir + "', 103) and convert(datetime, '" + tarih_iki + "', 103)";
            dataAdapter1.SelectCommand.CommandText = query;
            dataAdapter1.Fill(ariza);
            return ariza;
        }

        public DataTable ozelBolumAtanmamıs (string tarih_bir, string tarih_iki, int bolumId)
        {
            ariza.Clear();
            string query = "select tbl_ariza.ariza_id as 'Arıza numarası', tbl_ariza.baslik as 'Arıza başlığı', tbl_ariza.konu as 'Arıza konusu', tbl_ariza.bildirim_saati as 'Bildirim saati',CONCAT(tbl_personel.adi, ' ', tbl_personel.soyadi) as 'Ad Soyad' from(tbl_ariza inner join tbl_personel on tbl_ariza.personel_Id = tbl_personel.personel_Id and tbl_personel.bolum_Id = '" + bolumId + "')where atanma_saati is null and bildirim_saati between convert(datetime, '" + tarih_bir + "', 103) and convert(datetime, '" + tarih_iki + "', 103)" ;
            dataAdapter1.SelectCommand.CommandText = query;
            dataAdapter1.Fill(ariza);
            return ariza;
        }

        public DataTable ozelBolumAtanmıs(string tarih_bir, string tarih_iki, int bolumId)
        {
            ariza.Clear();
            string query = "select tbl_ariza.ariza_id as 'Arıza numarası', tbl_ariza.baslik as 'Arıza başlığı', tbl_ariza.konu as 'Arıza konusu', tbl_ariza.bildirim_saati as 'Bildirim saati',CONCAT(tbl_personel.adi, ' ', tbl_personel.soyadi) as 'Ad Soyad' from(tbl_ariza inner join tbl_personel on tbl_ariza.personel_Id = tbl_personel.personel_Id and tbl_personel.bolum_Id = '" + bolumId + "')where atanma_saati is not null and bildirim_saati between convert(datetime, '" + tarih_bir + "', 103) and convert(datetime, '" + tarih_iki + "', 103)";
            dataAdapter1.SelectCommand.CommandText = query;
            dataAdapter1.Fill(ariza);
            return ariza;
        }
        public DataTable ozelBolumTüm(string tarih_bir, string tarih_iki, int bolumId)
        {
            ariza.Clear();
            string query = "select ariza_id as 'Arıza numarası', baslik as 'Arıza başlığı', konu as 'Arıza konusu', bildirim_saati as 'Bildirim saati', atanma_saati 'Atanma saati', tamamlanma_saati as 'Tamamlanma saati', mesaj as 'Çözüm Mesajı' , CONCAT(adi, ' ', soyadi) as 'Ad Soyad' from((tbl_ariza inner join tbl_personel on tbl_ariza.personel_Id = tbl_personel.personel_Id) inner join tbl_unvan on tbl_unvan.unvan_Id = tbl_personel.unvan_Id and bolum_Id = '" + bolumId + "')where  bildirim_saati between convert(datetime, '" + tarih_bir + "', 103) and convert(datetime, '" + tarih_iki + "', 103)";
            dataAdapter1.SelectCommand.CommandText = query;
            dataAdapter1.Fill(ariza);
            return ariza;
        }

        public DataTable bolumAtanmamıs(int bolumId)
        {
            ariza.Clear();
            string query = "select tbl_ariza.ariza_id as 'Arıza numarası', tbl_ariza.baslik as 'Arıza başlığı', tbl_ariza.konu as 'Arıza konusu', tbl_ariza.bildirim_saati as 'Bildirim saati',CONCAT(tbl_personel.adi, ' ', tbl_personel.soyadi) as 'Ad Soyad' from(tbl_ariza inner join tbl_personel on tbl_ariza.personel_Id = tbl_personel.personel_Id and tbl_personel.bolum_Id ='" + bolumId + "' )where atanma_saati is null ";
            dataAdapter1.SelectCommand.CommandText = query;
            dataAdapter1.Fill(ariza);
            return ariza;
        }

        public DataTable bolumAtanmıs(int bolumId)
        {
            ariza.Clear();
            string query = "select tbl_ariza.ariza_id as 'Arıza numarası', tbl_ariza.baslik as 'Arıza başlığı', tbl_ariza.konu as 'Arıza konusu', tbl_ariza.bildirim_saati as 'Bildirim saati',CONCAT(tbl_personel.adi, ' ', tbl_personel.soyadi) as 'Ad Soyad' from(tbl_ariza inner join tbl_personel on tbl_ariza.personel_Id = tbl_personel.personel_Id and tbl_personel.bolum_Id ='" + bolumId + "' )where atanma_saati is not null ";
            dataAdapter1.SelectCommand.CommandText = query;
            dataAdapter1.Fill(ariza);
            return ariza;
        }

        public DataTable bolumTümAriza(int bolumId)
        {
            ariza.Clear();
            string query = "select tbl_ariza.ariza_id as 'Arıza numarası', tbl_ariza.baslik as 'Arıza başlığı', tbl_ariza.konu as 'Arıza konusu', tbl_ariza.bildirim_saati as 'Bildirim saati',CONCAT(tbl_personel.adi, ' ', tbl_personel.soyadi) as 'Ad Soyad' from(tbl_ariza inner join tbl_personel on tbl_ariza.personel_Id = tbl_personel.personel_Id and tbl_personel.bolum_Id ='" + bolumId + "' ) ";
            dataAdapter1.SelectCommand.CommandText = query;
            dataAdapter1.Fill(ariza);
            return ariza;
        }

        public DataTable personelListele(int bolumId)
        {
            ariza.Clear();
            string query = "select personel_Id as 'Personel numarası' ,CONCAT(tbl_personel.adi, ' ', tbl_personel.soyadi) as 'Ad Soyad',unvan as 'Ünvan',yetki as 'Raporlama' from (tbl_personel inner join tbl_unvan on tbl_personel.unvan_Id = tbl_unvan.unvan_Id and tbl_personel.bolum_Id = '" + bolumId + "') ";
            dataAdapter1.SelectCommand.CommandText = query;
            dataAdapter1.Fill(ariza);
            return ariza;
        }


    }
}
