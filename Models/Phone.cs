
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace netcore.Models
{
    [Table("tb_m_phones")]
    public class Phone
    {
        [Key]
        public int PhoneId { get; set; }
        public string PhoneNumber { get; set; }
        public Account Account { get; set; }
    }
}