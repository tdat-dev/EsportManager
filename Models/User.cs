using System;

namespace EsportManager.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }  // @TênID hoặc @adminTên
        public string DisplayName { get; set; }
        public string Role { get; set; }  // Admin/Player/Viewer
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }  // Đã mã hóa
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }  // Đã mã hóa
        public string Status { get; set; }  // Pending/Approved
    }
}