using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netcore.Models
{
    [Table("tb_m_educations")]
    public class Education
    {
        [Key]
        public int EducationId { get; set; }
        public string Degree { get; set; }
        public string GPA { get; set; }

        public int UniversityId { get; set; }
        public University Universitys { get; set; }

        public ICollection<Profilling> Profilling { get; set; }
    }
}