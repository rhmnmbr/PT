using System.ComponentModel.DataAnnotations;

namespace PTApi.ViewModels.Mahasiswa
{
    public class MhsKlsVm
    {
        public string KodeKelas { get; set; }
        public MhsKlsDetVm Kelas { get; set; }
        [Range(0, 100)]
        public float NilaiMid { get; set; }
        [Range(0, 100)]
        public float NilaiSem { get; set; }
        public string NilaiMutu { get; set; }
    }
}