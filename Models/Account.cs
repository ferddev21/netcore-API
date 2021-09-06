using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace netcore.Models
{

    [Table("tb_m_accounts")]
    public class Account
    {
        [Key] //Anontation
        [ForeignKey("Person")]
        public string NIK { get; set; } //Primary Key
        [Required, MinLength(8)]
        public string Password { get; set; }

        [JsonIgnore]
        public virtual Profilling Profilling { get; set; }

        [JsonIgnore]
        public virtual Person Person { get; set; }


        public virtual ICollection<AccountRole> AccountRoles { get; set; }

    }
}