using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace netcore.Models
{
    [Table("tb_m_profillings")]
    public class Profilling
    {
        [Key] //Anontation
        [ForeignKey("Account")]
        public string NIK { get; set; } //Primary Key

        [JsonIgnore]
        public virtual Account Account { get; set; }

        public int EducationId { get; set; }

        [JsonIgnore]
        public virtual Education Educations { get; set; }
    }
}