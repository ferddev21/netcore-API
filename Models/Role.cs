using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace netcore.Models
{
    [Table("tb_m_roles")]
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<AccountRole> AccountRoles { get; set; }
    }
}