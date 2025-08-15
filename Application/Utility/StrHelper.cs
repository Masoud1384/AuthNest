using System.Text.RegularExpressions;

namespace Application.Utility
{
    public static class ValidationHelper
    {
        public static bool ValidatePhoneNumber(this string phoneNumber)
            => Regex.IsMatch(phoneNumber ?? string.Empty, @"^09\d{9}$");

        public static bool ValidateEmail(this string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address != email)
                    return false;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
