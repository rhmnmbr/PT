using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTApi.Data.Models
{
    public class MhsKls
    {
        public int Nim { get; set; }
        public Mahasiswa Mahasiswa { get; set; }

        public string KodeKelas { get; set; }
        public Kelas Kelas { get; set; }

        [Range(0, 100)]
        public float NilaiMid { get; set; }
        [Range(0, 100)]
        public float NilaiSem { get; set; }
        [NotMapped]
        public string NilaiMutu
        {
            get
            {
                var am = "";
                var nil = (NilaiMid + NilaiSem * 3) / 4;
                if (nil < 31) { am = "E"; } else
                if (nil < 51) { am = "D"; } else
                if (nil < 61) { am = "C"; } else
                if (nil < 71) { am = "B"; } else { am = "A"; }
                return am;
            }
        }
    }
}