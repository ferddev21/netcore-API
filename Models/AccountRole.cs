using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace netcore.Models
{
    [Table("tb_m_accountroles")]
    public class AccountRole
    {
        [ForeignKey("Account")]
        public string NIK { get; set; }

        [JsonIgnore]
        public virtual Account Accounts { get; set; }
        public int RoleId { get; set; }

        [JsonIgnore]
        public virtual Role Roles { get; set; }
    }
}