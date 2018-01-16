using System.ComponentModel.DataAnnotations;

namespace PTApi.ViewModels.Mahasiswa
{
    public class MhsVm
    {
        [Required]
        public int Nim { get; set; }
        [Required]
        public string Nama { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public string TgLahir { get; set; }
        public string TpLahir { get; set; }
        public string JenisKelamin { get; set; }
    }
}