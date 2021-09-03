using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace netcore.Models
{
    [Table("tb_m_universitys")]
    public class University
    {
        [Key]
        public int UniversityId { get; set; }
        [Required(ErrorMessage = "Nama Universitas tidak boleh kosong")]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Education> Education { get; set; }
    }
}