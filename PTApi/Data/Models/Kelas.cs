using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PTApi.Data.Models
{
    public class Kelas
    {
        [Key]
        [StringLength(5)]
        public string KodeKelas { get; set; }
        public string Nama { get; set; }

        public List<MhsKls> Mhss { get; set; }
        public Dosen Dosen { get; set; }
    }
}