using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using EsportManager.Models;
using EsportManager.DAL.Interfaces;
using EsportManager.Utils;

namespace EsportManager.DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public User GetByUsername(string username)
        {
            // TODO: Thực hiện truy vấn SQL thực tế
            // Đây là mã mẫu, bạn cần thay thế bằng truy vấn SQL thực tế

            // Mẫu giả để test
            if (username == "@adminTest")
            {
                return new User
                {
                    UserID = 1,
                    Username = "@adminTest",
                    DisplayName = "Admin Test",
                    Role = "Admin",
                    Email = "admin@test.com",
                    Phone = "0123456789",
                    Status = "Approved"
                };
            }
            return null;
        }

        public User GetByID(int userID)
        {
            // TODO: Thực hiện truy vấn SQL
            return null;
        }

        public User GetByEmail(string email)
        {
            // TODO: Thực hiện truy vấn SQL
            return null;
        }

        public List<User> GetPendingUsers()
        {
            // TODO: Thực hiện truy vấn SQL
            List<User> pendingUsers = new List<User>();

            // Mẫu giả để test
            pendingUsers.Add(new User
            {
                UserID = 2,
                Username = "@user1",
                DisplayName = "User Test 1",
                Role = "Player",
                Email = "user1@test.com",
                Phone = "0123456789",
                Status = "Pending"
            });

            pendingUsers.Add(new User
            {
                UserID = 3,
                Username = "@user2",
                DisplayName = "User Test 2",
                Role = "Viewer",
                Email = "user2@test.com",
                Phone = "0987654321",
                Status = "Pending"
            });

            return pendingUsers;
        }

        public List<User> GetAllUsers()
        {
            // TODO: Thực hiện truy vấn SQL
            List<User> allUsers = new List<User>();

            // Thêm admin
            allUsers.Add(new User
            {
                UserID = 1,
                Username = "@adminTest",
                DisplayName = "Admin Test",
                Role = "Admin",
                Email = "admin@test.com",
                Phone = "0123456789",
                Status = "Approved"
            });

            // Thêm các user từ danh sách pending
            allUsers.AddRange(GetPendingUsers());

            // Thêm một số user đã duyệt
            allUsers.Add(new User
            {
                UserID = 4,
                Username = "@player1",
                DisplayName = "Player 1",
                Role = "Player",
                Email = "player1@test.com",
                Phone = "0123123123",
                Status = "Approved"
            });

            allUsers.Add(new User
            {
                UserID = 5,
                Username = "@viewer1",
                DisplayName = "Viewer 1",
                Role = "Viewer",
                Email = "viewer1@test.com",
                Phone = "0456456456",
                Status = "Approved"
            });

            return allUsers;
        }

        public bool Add(User user)
        {
            // TODO: Thực hiện truy vấn SQL
            return true;
        }

        public bool Update(User user)
        {
            // TODO: Thực hiện truy vấn SQL
            return true;
        }

        public bool Delete(int userID)
        {
            // TODO: Thực hiện truy vấn SQL
            return true;
        }

        public bool ChangePassword(int userID, string newPassword)
        {
            // TODO: Mã hóa mật khẩu và thực hiện truy vấn SQL
            string hashedPassword = SecurityHelper.HashPassword(newPassword);
            // TODO: Cập nhật vào cơ sở dữ liệu
            return true;
        }

        public bool ChangeRole(int userID, string newRole)
        {
            // TODO: Thực hiện truy vấn SQL
            return true;
        }

        public bool ChangeStatus(int userID, string newStatus)
        {
            // TODO: Thực hiện truy vấn SQL
            return true;
        }

        public bool IsUsernameExists(string username)
        {
            // TODO: Thực hiện truy vấn SQL
            return false;
        }

        public bool IsEmailExists(string email)
        {
            // TODO: Thực hiện truy vấn SQL
            return false;
        }

        public string GetSecurityQuestion(string username)
        {
            // TODO: Thực hiện truy vấn SQL
            return "Bạn học trường nào?";
        }

        public bool VerifySecurityAnswer(string username, string answer)
        {
            // TODO: Mã hóa câu trả lời và so sánh với dữ liệu trong DB
            return true;
        }
    }
}