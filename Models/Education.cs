using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace netcore.Models
{
    [Table("tb_m_educations")]
    public class Education
    {
        public Education(string degree, string gPA, int universityId)
        {
            Degree = degree;
            GPA = gPA;
            UniversityId = universityId;
        }

        [Key]
        public int EducationId { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required]
        public string GPA { get; set; }

        public int UniversityId { get; set; }

        public virtual University Universitys { get; set; }

        [JsonIgnore]
        public virtual ICollection<Profilling> Profilling { get; set; }
    }
}