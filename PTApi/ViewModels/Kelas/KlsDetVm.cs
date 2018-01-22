using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PTApi.ViewModels.Kelas
{
    public class KlsDetVm
    {
        [Required]
        [StringLength(5)]
        public string KodeKelas { get; set; }
        public string Nama { get; set; }

        public KlsDsnVm Dosen { get; set; }
        public List<KlsMhsVm> Mhss { get; set; }
    }
}