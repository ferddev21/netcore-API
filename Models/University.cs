using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netcore.Models
{
    [Table("tb_m_universitys")]
    public class University
    {
        [Key]
        public int UniversityId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Education> Education { get; set; }
    }
}