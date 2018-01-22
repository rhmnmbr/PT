using System.ComponentModel.DataAnnotations;

namespace PTApi.ViewModels.Kelas
{
    public class KlsDsnPutVm
    {
        [Required]
        [StringLength(5)]
        public string KodeKelas { get; set; }
        public KlsDsnPutDetVm Dosen { get; set; }
    }
}