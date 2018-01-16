using System;

namespace PTApi.Data.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Nama { get; set; }
        public string Password { get; set; }
        public DateTime RegisterDate { get; set; }
        public int ActivationStatus { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}