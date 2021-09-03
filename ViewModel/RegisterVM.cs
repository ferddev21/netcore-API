using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace netcore.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "NIK tidak boleh kosong")]
        public string NIK { get; set; }
        public string FullName { get; set; }

        [Required(ErrorMessage = "Nama Depan tidak boleh kosong"),
        MinLength(3, ErrorMessage = "Nama Depan minimal 3 Karakter"),
        RegularExpression("([a-zA-Z]{3,30}\\s*)+", ErrorMessage = "Sepertinya bukan Karakter Nama")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nama Belakang tidak boleh kosong"),
        MinLength(3, ErrorMessage = "Nama Belakang minimal 3 Karakter"),
        RegularExpression("([a-zA-Z]{3,30}\\s*)+", ErrorMessage = "Sepertinya bukan Karakter Nama")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Nomor Telp tidak boleh kosong"),
        RegularExpression("^(\\+62|62|0)8[1-9][0-9]{6,9}$", ErrorMessage = "Bukan Nomor Telp Indonesia")]
        public string Phone { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
        [Required, Range(0, 1, ErrorMessage = "Gender harus diantara 0 atau 1")]
        public int Gender { get; set; }

        [Required(ErrorMessage = "Salary Depan tidak boleh kosong")]
        public int Salary { get; set; }

        [Required(ErrorMessage = "Email tidak boleh kosong"),
        EmailAddress(ErrorMessage = "Alamat Email tidak valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password tidak boleh kosong"),
        MinLength(8, ErrorMessage = "Password Minimal 8 Karakter"),
        RegularExpression("^(?=[^a-z]*[a-z])(?=[^A-Z]*[A-Z])(?=\\D*\\d)[^:&.~\\s]{5,20}$", ErrorMessage = "Password harus mengadung huruf besar,huruf kecil dan angka ")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Degree tidak boleh kosong")]
        public string Degree { get; set; }
        [Required(ErrorMessage = "GPA tidak boleh kosong")]
        public string GPA { get; set; }
        [Required(ErrorMessage = "UniversityId tidak boleh kosong")]
        public int UniversityId { get; set; }

    }
}