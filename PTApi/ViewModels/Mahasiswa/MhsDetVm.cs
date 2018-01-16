using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PTApi.ViewModels.Mahasiswa
{
    public class MhsDetVm
    {
        public int Nim { get; set; }
        public string Nama { get; set; }
        public string TpLahir { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public string TgLahir { get; set; }
        public string JenisKelamin { get; set; }
        public List<MhsKlsVm> Klasses { get; set; }
    }
}