using System;
using EsportManager.UI.Interfaces;
using EsportManager.BLL.Interfaces;
using EsportManager.Models;
using EsportManager.Utils;
using System.Collections.Generic;

namespace EsportManager.UI.Screens
{
    public class LoginScreen : IScreen
    {
        private readonly IUserService _userService;

        public LoginScreen(IUserService userService)
        {
            _userService = userService;
        }

        public void Show()
        {
            Console.Clear();
            Console.CursorVisible = true;

            // Thiết lập màu nền đen
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            // Hiển thị tiêu đề theo mẫu mới
            DrawSpecialHeader("ESPORT MANAGER", "[LOGIN]");

            try
            {
                // Nhập username
                Console.SetCursorPosition(0, Console.CursorTop + 3);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("  → Username: ");
                string username = Console.ReadLine();

                // Nhập password
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("  → Password: ");
                string password = ReadPassword();
                Console.WriteLine();

                // Xử lý đăng nhập
                User user = _userService.Login(username, password);
                if (user != null)
                {
                    // Hiển thị thông báo đăng nhập thành công
                    ShowSuccessMessage($"Đăng nhập thành công! Chào mừng {user.DisplayName}");

                    // Chuyển hướng sang màn hình tương ứng với vai trò
                    IScreen nextScreen = null;
                    switch (user.Role)
                    {
                        case "Admin":
                            nextScreen = new AdminMenuScreen(_userService);
                            break;
                        case "Player":
                            // TODO: Tạo và hiển thị PlayerScreen
                            ShowInfoMessage("Chức năng Player đang được phát triển...");
                            break;
                        case "Viewer":
                            // TODO: Tạo và hiển thị ViewerScreen
                            ShowInfoMessage("Chức năng Viewer đang được phát triển...");
                            break;
                    }

                    if (nextScreen != null)
                    {
                        nextScreen.Show();
                    }
                    else
                    {
                        Show(); // Quay lại màn hình đăng nhập
                    }
                }
                else
                {
                    // Hiển thị thông báo đăng nhập thất bại
                    ShowErrorMessage("Đăng nhập thất bại! Tên đăng nhập hoặc mật khẩu không đúng.");

                    // Hiển thị lại màn hình đăng nhập
                    Show();
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("Lỗi hiển thị form đăng nhập: " + ex.Message);
                Console.WriteLine("Vui lòng mở rộng cửa sổ console và nhấn phím bất kỳ để thử lại...");
                Console.ReadKey(true);
                Show();
            }
        }

        // Vẽ tiêu đề với font đặc biệt viền kép
        private void DrawSpecialHeader(string title, string subtitle)
        {
            Console.Clear();
            Console.CursorVisible = false;

            // Lấy kích thước console
            int windowWidth = Console.WindowWidth;
            int startY = 5;

            // Đảm bảo kích thước khung viền phù hợp với kích thước cửa sổ
            int borderWidth = Math.Max(10, windowWidth - 4);

            // Vẽ khung viền đơn
            string topBorder = "╔" + new string('═', borderWidth - 2) + "╗";
            string bottomBorder = "╚" + new string('═', borderWidth - 2) + "╝";
            string sideBorder = "║" + new string(' ', borderWidth - 2) + "║";

            // Đảm bảo không đặt con trỏ tại vị trí âm
            int leftPosition = Math.Max(0, (windowWidth - borderWidth) / 2);

            Console.ForegroundColor = ConsoleColor.DarkGray;

            // Vẽ viền trên
            UIHelper.SafeSetCursorPosition(leftPosition, startY);
            Console.Write(topBorder);

            // Vẽ viền bên
            for (int i = 1; i <= 8; i++)
            {
                UIHelper.SafeSetCursorPosition(leftPosition, startY + i);
                Console.Write(sideBorder);
            }

            // Vẽ viền dưới
            UIHelper.SafeSetCursorPosition(leftPosition, startY + 9);
            Console.Write(bottomBorder);

            // Hiển thị tiêu đề với font đặc biệt và viền kép
            Console.ForegroundColor = ConsoleColor.Cyan;

            // Tạo font pixel cho ESPORT MANAGER dựa theo ảnh mẫu
            string[] pixelFont = UIHelper.CreatePixelFont("ESPORT MANAGER");
            int titleWidth = pixelFont[0].Length;
            int centerX = Math.Max(0, (windowWidth - titleWidth) / 2);

            for (int i = 0; i < pixelFont.Length; i++)
            {
                UIHelper.SafeSetCursorPosition(centerX, startY + 2 + i);
                Console.Write(pixelFont[i]);
            }

            // Hiển thị phụ đề [LOGIN] bên dưới tiêu đề
            Console.ForegroundColor = ConsoleColor.Yellow;
            UIHelper.SafeSetCursorPosition(Math.Max(0, (windowWidth - subtitle.Length) / 2), startY + 7);
            Console.Write(subtitle);

            Console.ForegroundColor = ConsoleColor.White;
        }



        // Phương thức đọc mật khẩu, hiển thị dưới dạng dấu *
        private string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // Xử lý backspace
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b"); // Xóa ký tự trên màn hình
                }
                // Thêm ký tự vào mật khẩu nếu không phải Enter
                else if (key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*"); // Hiển thị * cho mỗi ký tự
                }
            } while (key.Key != ConsoleKey.Enter);

            return password;
        }

        // Hiển thị thông báo thành công
        private void ShowSuccessMessage(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("\n\n");
            Console.WriteLine(CenterText("=== THÔNG BÁO ==="));
            Console.WriteLine();
            Console.WriteLine(CenterText(message));
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n" + CenterText("Nhấn phím bất kỳ để tiếp tục..."));
            Console.ReadKey(true);
        }

        // Hiển thị thông báo lỗi
        private void ShowErrorMessage(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine("\n\n");
            Console.WriteLine(CenterText("=== THÔNG BÁO ==="));
            Console.WriteLine();
            Console.WriteLine(CenterText(message));
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n" + CenterText("Nhấn phím bất kỳ để thử lại..."));
            Console.ReadKey(true);
        }

        // Hiển thị thông báo thông tin
        private void ShowInfoMessage(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine("\n\n");
            Console.WriteLine(CenterText("=== THÔNG BÁO ==="));
            Console.WriteLine();
            Console.WriteLine(CenterText(message));
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n" + CenterText("Nhấn phím bất kỳ để quay lại..."));
            Console.ReadKey(true);
        }

        // Căn giữa văn bản
        private string CenterText(string text)
        {
            int screenWidth = Console.WindowWidth;

            // Nếu kích thước văn bản lớn hơn kích thước cửa sổ, cắt văn bản
            if (text.Length >= screenWidth)
            {
                return text.Substring(0, screenWidth - 3) + "...";
            }

            int leftPadding = (screenWidth - text.Length) / 2;
            return new string(' ', Math.Max(0, leftPadding)) + text;
        }

        // Chia một chuỗi thành mảng các chuỗi con với độ dài tối đa
        private string[] SplitMessage(string message, int maxLength)
        {
            if (message.Length <= maxLength)
                return new string[] { message };

            int parts = (message.Length + maxLength - 1) / maxLength;
            string[] result = new string[parts];

            for (int i = 0; i < parts; i++)
            {
                int startIndex = i * maxLength;
                int length = Math.Min(maxLength, message.Length - startIndex);
                result[i] = message.Substring(startIndex, length);
            }

            return result;
        }
    }
}