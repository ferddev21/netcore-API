using System.ComponentModel.DataAnnotations;

namespace netcore.ViewModel
{
    public class LoginVM
    {
        public string NIK { get; set; }

        [Required(ErrorMessage = "Email tidak boleh kosong"), EmailAddress(ErrorMessage = "Alamat Email tidak valid")]
        public string Email { get; set; }

        public string Password { get; set; }

        [MinLength(8, ErrorMessage = "Password Minimal 8 Karakter"),
        RegularExpression("^(?=[^a-z]*[a-z])(?=[^A-Z]*[A-Z])(?=\\D*\\d)[^:&.~\\s]{5,20}$", ErrorMessage = "Password harus mengadung huruf besar,huruf kecil dan angka ")]
        public string NewPassword { get; set; }

        public string OTP { get; set; }

        public int RoleId { get; set; }
    }
}