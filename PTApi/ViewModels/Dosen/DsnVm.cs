using System.ComponentModel.DataAnnotations;

namespace PTApi.ViewModels.Dosen
{
    public class DsnVm
    {
        [Required]
        [StringLength(5)]
        public string NIK { get; set; }
        [Required]
        public string Nama { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public string TgLahir { get; set; }
        public string TpLahir { get; set; }
        public string TingkatPend { get; set; }
    }
}