using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netcore.Models
{
    [Table("tb_m_accounts")]
    public class Account
    {
        [Key] //Anontation
        public int AccountId { get; set; } //Primary Key
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<Phone> Phone { get; set; }
    }
}