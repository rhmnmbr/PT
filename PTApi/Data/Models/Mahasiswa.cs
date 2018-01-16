using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTApi.Data.Models
{
    public class Mahasiswa
    {
        [Key]
        public int Nim { get; set; }
        public string Nama { get; set; }
        [Column(TypeName = "date")]
        public DateTime TgLahir { get; set; }
        public string TpLahir { get; set; }
        public string JenisKelamin { get; set; }
        public List<MhsKls> Klasses { get; set; }
    }
}