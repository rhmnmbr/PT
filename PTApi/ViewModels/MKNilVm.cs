using System.ComponentModel.DataAnnotations;

namespace PTApi.ViewModels
{
    public class MKNilVm
    {
        public int Nim { get; set; }
        public string KodeKelas { get; set;}
        [Range(0, 100)]
        public float NilaiMid { get; set; }
        [Range(0, 100)]
        public float NilaiSem { get; set; }
    }
}