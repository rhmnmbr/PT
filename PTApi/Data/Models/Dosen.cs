using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTApi.Data.Models
{
    public class Dosen
    {
        [Key]
        [StringLength(5)]
        public string NIK { get; set; }
        public string Nama { get; set; }
        [Column(TypeName = "date")]
        public DateTime TgLahir { get; set; }
        public string TpLahir { get; set; }
        public string TingkatPend { get; set; }

        public ICollection<Kelas> Kelas { get; set; }
    }
}