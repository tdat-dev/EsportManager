using System;
using System.Collections.Generic;
using EsportManager.Models;

namespace EsportManager.BLL.Interfaces
{
    public interface IUserService
    {
        User Login(string username, string password);
        bool Register(User user);
        bool ApproveUser(int userID);
        bool RejectUser(int userID);
        bool DeleteUser(int userID);
        bool DeleteUserByUsername(string username);
        bool DeleteUserByEmail(string email);
        bool ChangePassword(int userID, string oldPassword, string newPassword);
        bool ResetPassword(string username, string securityAnswer);
        string GetSecurityQuestion(string username);
        bool RequestRoleChange(int userID, string newRole);
        bool ApproveRoleChange(int requestID);
        bool RejectRoleChange(int requestID);
        List<User> GetPendingUsers();
        List<User> GetAllUsers();
        bool UpdateProfile(User user);
    }
}