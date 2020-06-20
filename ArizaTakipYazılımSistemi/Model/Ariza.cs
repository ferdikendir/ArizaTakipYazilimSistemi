using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArizaTakipYazılımSistemi.Model
{
    class Ariza
    {
        public int ariza_id { get; set; }
        public int personel_id { get; set; }
        public string baslik { get; set; }
        public string konu { get; set; }
        public string mesaj { get; set; }
        public DateTime bildirim_saati { get; set; }
        public DateTime atanma_saati { get; set; }
        public DateTime tamamlanma_saati { get; set; }
        public int gideren_personel_id { get; set; }

    }
}
