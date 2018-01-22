using System.ComponentModel.DataAnnotations;

namespace PTApi.ViewModels.Kelas
{
    public class KlsVm
    {
        [Required]
        [StringLength(5)]
        public string KodeKelas { get; set; }
        public string Nama { get; set; }
    }
}