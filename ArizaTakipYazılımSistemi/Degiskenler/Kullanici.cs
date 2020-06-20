using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArizaTakipYazılımSistemi.Model;

namespace ArizaTakipYazılımSistemi.Controller
{
    class Kullanici
    {
        public static Unvan personel_unvan { get; set; }
        public static Bolum personel_bolum { get; set; }
        public static Personel personel { get; set; }
        public static int ariza_sayisi { get; set; }
        public static int tamamlanmayi_ariza_sayisi { get; set; }
        public static int atama_bekleyen_ariza_sayisi { get; set; }
        public static Ariza ariza { get; set; }
        
    }
}
