using System;
using System.Collections.Generic;
using EsportManager.Models;
using EsportManager.DAL.Interfaces;
using EsportManager.BLL.Interfaces;
using EsportManager.Utils;

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
            User user = _userRepository.GetByUsername(username);
            if (user != null && SecurityHelper.VerifyPassword(password, user.Password))
            {
                // Chỉ cho phép đăng nhập nếu tài khoản đã được duyệt
                if (user.Status == "Approved")
                {
                    return user;
                }
            }
            return null;
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