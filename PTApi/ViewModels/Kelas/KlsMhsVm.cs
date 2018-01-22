using System.ComponentModel.DataAnnotations;

namespace PTApi.ViewModels.Kelas
{
    public class KlsMhsVm
    {
        public int Nim { get; set; }
        public KlsMhsDetVm Mahasiswa { get; set; }
        [Range(0, 100)]
        public float NilaiMid { get; set; }
        [Range(0, 100)]
        public float NilaiSem { get; set; }
        public string NilaiMutu { get; set; }
    }
}