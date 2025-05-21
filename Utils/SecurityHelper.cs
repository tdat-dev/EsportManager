using System;
using System.Security.Cryptography;
using System.Text;

namespace EsportManager.Utils
{
    public static class SecurityHelper
    {
        // Mã hóa mật khẩu sử dụng SHA256
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // Mã hóa chuỗi khác như câu trả lời bảo mật
        public static string HashString(string input)
        {
            return HashPassword(input);
        }

        // Kiểm tra mật khẩu có khớp với mật khẩu đã mã hóa không
        public static bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            string hashedInput = HashPassword(inputPassword);
            return hashedInput.Equals(hashedPassword);
        }

        // Kiểm tra tính hợp lệ của email
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Kiểm tra tính hợp lệ của số điện thoại
        public static bool IsValidPhone(string phone)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(phone, @"^\d{10,11}$");
        }

        // Kiểm tra tính hợp lệ của username (@TênID)
        public static bool IsValidUsername(string username)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(username, @"^@[a-zA-Z0-9]+$");
        }
    }
}