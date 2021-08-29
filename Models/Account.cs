using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netcore.Models
{
    [Table("tb_m_accounts")]
    public class Account
    {
        [Key] //Anontation
        [ForeignKey("Person")]
        public string NIK { get; set; } //Primary Key
        public string Password { get; set; }
        public Profilling Profilling { get; set; }
        public Person Person { get; set; }

    }
}