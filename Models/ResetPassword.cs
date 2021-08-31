using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace netcore.Models
{
    [Table("tb_m_reset_passwords")]
    public class ResetPassword
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string OTP { get; set; }
        public string NIK { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}