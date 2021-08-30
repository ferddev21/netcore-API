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
        [ForeignKey("Person")]
        public string NIK { get; set; } //Primary Key
        public string Password { get; set; }
        public virtual Profilling Profilling { get; set; }
        public virtual Person Person { get; set; }

    }
}