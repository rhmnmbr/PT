using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PTApi.ViewModels.Dosen
{
    public class DsnDetVm
    {
        public string NIK { get; set; }
        public string Nama { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public string TgLahir { get; set; }
        public string TpLahir { get; set; }
        public string TingkatPend { get; set; }
        public ICollection<DsnKlsVm> Kelas { get; set; }
    }
}