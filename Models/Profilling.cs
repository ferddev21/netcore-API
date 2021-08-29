using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace netcore.Models
{
    [Table("tb_m_profillings")]
    public class Profilling
    {
        [Key] //Anontation
        [ForeignKey("Account")]
        public string NIK { get; set; } //Primary Key
        public Account Account { get; set; }

        public int EducationId { get; set; }
        public Education Educations { get; set; }
    }
}