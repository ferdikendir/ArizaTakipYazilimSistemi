using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArizaTakipYazılımSistemi.Model
{
    class Personel
    {
        public int personel_id { get; set; }
        public string adi { get; set; }
        public string soyadi { get; set; }
        public int bolum_id { get; set; }
        public int unvan_id { get; set; }
        public bool yetki { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
