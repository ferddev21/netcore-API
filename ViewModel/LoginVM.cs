namespace netcore.ViewModel
{
    public class LoginVM
    {
        public string NIK { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string OTP { get; set; }
    }
}