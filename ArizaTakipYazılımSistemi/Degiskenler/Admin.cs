using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArizaTakipYazılımSistemi.Model;

namespace ArizaTakipYazılımSistemi.Degiskenler
{
    class Admin
    {
        public static Personel personel { get; set; }
        public static Boolean kullanici = true;
        public static Bolum bolum { get; set; }
        public static Unvan unvan { get; set; }
    }
}
