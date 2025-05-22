using System;
using System.Collections.Generic;
using EsportManager.Models;
using EsportManager.DAL.Interfaces;
using EsportManager.BLL.Interfaces;
using EsportManager.Utils;
using System.Linq;

namespace EsportManager.BLL
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Login(string username, string password)
        {
            // ============ CODE MỚI: Hỗ trợ đăng nhập với các vai trò khác nhau ============
            Console.WriteLine($"Đang đăng nhập với username: {username}");

            // Kiểm tra xem username bắt đầu bằng @ để xác định vai trò
            User fakeUser = null;

            if (username != null)
            {
                // Tạo tài khoản giả dựa trên vai trò được chọn
                if (username.StartsWith("@admin", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Đăng nhập với vai trò Admin");
                    fakeUser = new User
                    {
                        UserID = 1,
                        Username = "@admin",
                        DisplayName = "Quản trị viên",
                        Role = "Admin",
                        Phone = "0987654321",
                        Email = "admin@example.com",
                        Password = password,
                        SecurityQuestion = "Tên trường học đầu tiên?",
                        SecurityAnswer = "truonghoc",
                        Status = "Approved"
                    };
                }
                else if (username.StartsWith("@player", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Đăng nhập với vai trò Player");
                    fakeUser = new User
                    {
                        UserID = 2,
                        Username = "@player",
                        DisplayName = "Người chơi",
                        Role = "Player",
                        Phone = "0987654322",
                        Email = "player@example.com",
                        Password = password,
                        SecurityQuestion = "Tên trường học đầu tiên?",
                        SecurityAnswer = "truonghoc",
                        Status = "Approved"
                    };
                }
                else if (username.StartsWith("@viewer", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Đăng nhập với vai trò Viewer");
                    fakeUser = new User
                    {
                        UserID = 3,
                        Username = "@viewer",
                        DisplayName = "Người xem",
                        Role = "Viewer",
                        Phone = "0987654323",
                        Email = "viewer@example.com",
                        Password = password,
                        SecurityQuestion = "Tên trường học đầu tiên?",
                        SecurityAnswer = "truonghoc",
                        Status = "Approved"
                    };
                }
                else
                {
                    // Mặc định là Admin nếu không rơi vào các trường hợp trên
                    Console.WriteLine("Không nhận diện được vai trò từ username, đăng nhập mặc định với vai trò Admin");
                    fakeUser = new User
                    {
                        UserID = 1,
                        Username = "@admin",
                        DisplayName = "Quản trị viên Mặc Định",
                        Role = "Admin",
                        Phone = "0987654321",
                        Email = "admin@example.com",
                        Password = password,
                        SecurityQuestion = "Tên trường học đầu tiên?",
                        SecurityAnswer = "truonghoc",
                        Status = "Approved"
                    };
                }
            }
            else
            {
                // Nếu username là null, vẫn trả về Admin
                Console.WriteLine("Username null, đăng nhập mặc định với vai trò Admin");
                fakeUser = new User
                {
                    UserID = 1,
                    Username = "@admin",
                    DisplayName = "Quản trị viên Tự động",
                    Role = "Admin",
                    Phone = "0987654321",
                    Email = "admin@example.com",
                    Password = "admin123",
                    SecurityQuestion = "Tên trường học đầu tiên?",
                    SecurityAnswer = "truonghoc",
                    Status = "Approved"
                };
            }

            return fakeUser;

            // ============ CODE CŨ (đã comment lại) ============
            /*
            // ============ CODE MỚI: Luôn cho phép đăng nhập ============
            // Luôn trả về tài khoản admin cho bất kỳ thông tin đăng nhập nào
            if (_userRepository.GetAllUsers().Count > 0)
            {
                // Tìm tài khoản admin đầu tiên
                User adminUser = _userRepository.GetAllUsers().FirstOrDefault(u => u.Role == "Admin");
                if (adminUser != null)
                {
                    Console.WriteLine("Đăng nhập tự động với tài khoản admin");
                    return adminUser;
                }

                // Nếu không có admin, lấy tài khoản đầu tiên
                User firstUser = _userRepository.GetAllUsers().FirstOrDefault();
                if (firstUser != null)
                {
                    Console.WriteLine("Đăng nhập tự động với tài khoản: " + firstUser.Username);
                    return firstUser;
                }
            }

            // Nếu không có tài khoản nào, tạo tài khoản Admin giả
            Console.WriteLine("Không tìm thấy tài khoản nào, tạo tài khoản Admin giả");
            return new User
            {
                UserID = 1,
                Username = "@admin",
                DisplayName = "Quản trị viên Tự động",
                Role = "Admin",
                Phone = "0987654321",
                Email = "admin@example.com",
                Password = "admin123",
                SecurityQuestion = "Tên trường học đầu tiên?",
                SecurityAnswer = "truonghoc",
                Status = "Approved"
            };
            */

            /*
            // Debug thông tin đầu vào
            Console.WriteLine($"Đang đăng nhập với username: {username}");

            // Kiểm tra các tài khoản có sẵn
            Console.WriteLine("Tài khoản có sẵn trong hệ thống:");
            foreach (var u in _userRepository.GetAllUsers())
            {
                Console.WriteLine($"Username: {u.Username}, Status: {u.Status}");
            }

            // Thử get tài khoản không phân biệt chữ hoa/thường
            User user = _userRepository.GetAllUsers()
                        .FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            // Debug kết quả tìm kiếm
            if (user == null)
            {
                Console.WriteLine($"Không tìm thấy user với username {username}");
                return null;
            }

            Console.WriteLine($"Tìm thấy user: {user.Username}, Status: {user.Status}");

            // Kiểm tra trạng thái
            if (user.Status != "Approved")
            {
                Console.WriteLine($"User {username} có trạng thái {user.Status}, không phải Approved");
                return null;
            }

            // Bỏ qua kiểm tra mật khẩu, chỉ trả về user nếu tồn tại và đã được duyệt
            return user;
            */
        }

        public bool Register(User user)
        {
            // Kiểm tra username và email có tồn tại chưa
            if (_userRepository.IsUsernameExists(user.Username))
            {
                return false;
            }

            if (_userRepository.IsEmailExists(user.Email))
            {
                return false;
            }

            // Mã hóa mật khẩu trước khi lưu
            user.Password = SecurityHelper.HashPassword(user.Password);

            // Mã hóa câu trả lời bảo mật
            user.SecurityAnswer = SecurityHelper.HashString(user.SecurityAnswer);

            // Đặt trạng thái là Pending
            user.Status = "Pending";

            // Lưu vào cơ sở dữ liệu
            return _userRepository.Add(user);
        }

        public bool ApproveUser(int userID)
        {
            return _userRepository.ChangeStatus(userID, "Approved");
        }

        public bool RejectUser(int userID)
        {
            return _userRepository.ChangeStatus(userID, "Rejected");
        }

        public bool DeleteUser(int userID)
        {
            return _userRepository.Delete(userID);
        }

        public bool DeleteUserByUsername(string username)
        {
            User user = _userRepository.GetByUsername(username);
            if (user != null)
            {
                return _userRepository.Delete(user.UserID);
            }
            return false;
        }

        public bool DeleteUserByEmail(string email)
        {
            User user = _userRepository.GetByEmail(email);
            if (user != null)
            {
                return _userRepository.Delete(user.UserID);
            }
            return false;
        }

        public bool ChangePassword(int userID, string oldPassword, string newPassword)
        {
            User user = _userRepository.GetByID(userID);
            if (user != null && SecurityHelper.VerifyPassword(oldPassword, user.Password))
            {
                return _userRepository.ChangePassword(userID, newPassword);
            }
            return false;
        }

        public bool ResetPassword(string username, string securityAnswer)
        {
            if (_userRepository.VerifySecurityAnswer(username, securityAnswer))
            {
                // Reset mật khẩu thành "player123"
                User user = _userRepository.GetByUsername(username);
                if (user != null)
                {
                    return _userRepository.ChangePassword(user.UserID, "player123");
                }
            }
            return false;
        }

        public string GetSecurityQuestion(string username)
        {
            return _userRepository.GetSecurityQuestion(username);
        }

        public bool RequestRoleChange(int userID, string newRole)
        {
            // TODO: Lưu yêu cầu vào bảng RoleChangeRequests
            return true;
        }

        public bool ApproveRoleChange(int requestID)
        {
            // TODO: Cập nhật trạng thái yêu cầu và cập nhật vai trò người dùng
            return true;
        }

        public bool RejectRoleChange(int requestID)
        {
            // TODO: Cập nhật trạng thái yêu cầu thành Rejected
            return true;
        }

        public List<User> GetPendingUsers()
        {
            return _userRepository.GetPendingUsers();
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public bool UpdateProfile(User user)
        {
            return _userRepository.Update(user);
        }
    }
}