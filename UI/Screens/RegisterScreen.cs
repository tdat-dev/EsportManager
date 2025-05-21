using System;
using EsportManager.UI.Interfaces;
using EsportManager.BLL.Interfaces;
using EsportManager.Models;
using EsportManager.Utils;
using System.Collections.Generic;

namespace EsportManager.UI.Screens
{
    public class RegisterScreen : IScreen
    {
        private readonly IUserService _userService;

        public RegisterScreen(IUserService userService)
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
            DrawSpecialHeader("ESPORT MANAGER", "[ĐĂNG KÝ TÀI KHOẢN]");

            try
            {
                // Vẽ form đăng ký
                DrawRegisterForm();

                // Tính toán vị trí bắt đầu của form đăng ký
                int formStartLine = 18; // Vị trí bắt đầu form

                // Nhập thông tin đăng ký
                int centerX = Math.Max(0, (Console.WindowWidth - 40) / 2);

                SafeSetCursorPosition(centerX, formStartLine + 1);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("→ Tên hiển thị: ");
                Console.ForegroundColor = ConsoleColor.White;
                string displayName = Console.ReadLine();

                SafeSetCursorPosition(centerX, formStartLine + 2);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("→ Tên đăng nhập (@TênID): ");
                Console.ForegroundColor = ConsoleColor.White;
                string username = Console.ReadLine();

                SafeSetCursorPosition(centerX, formStartLine + 3);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("→ Email: ");
                Console.ForegroundColor = ConsoleColor.White;
                string email = Console.ReadLine();

                SafeSetCursorPosition(centerX, formStartLine + 4);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("→ Số điện thoại: ");
                Console.ForegroundColor = ConsoleColor.White;
                string phone = Console.ReadLine();

                SafeSetCursorPosition(centerX, formStartLine + 5);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("→ Mật khẩu: ");
                Console.ForegroundColor = ConsoleColor.White;
                string password = ReadPassword();

                SafeSetCursorPosition(centerX, formStartLine + 6);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("→ Xác nhận mật khẩu: ");
                Console.ForegroundColor = ConsoleColor.White;
                string confirmPassword = ReadPassword();

                SafeSetCursorPosition(centerX, formStartLine + 7);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("→ Câu hỏi bảo mật: ");
                Console.ForegroundColor = ConsoleColor.White;
                string securityQuestion = Console.ReadLine();

                SafeSetCursorPosition(centerX, formStartLine + 8);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("→ Câu trả lời bảo mật: ");
                Console.ForegroundColor = ConsoleColor.White;
                string securityAnswer = Console.ReadLine();

                SafeSetCursorPosition(centerX, formStartLine + 9);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("→ Vai trò (Player/Viewer): ");
                Console.ForegroundColor = ConsoleColor.White;
                string role = Console.ReadLine().Trim();

                // Kiểm tra tính hợp lệ
                if (string.IsNullOrEmpty(displayName) || string.IsNullOrEmpty(username) ||
                    string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) ||
                    string.IsNullOrEmpty(securityQuestion) || string.IsNullOrEmpty(securityAnswer))
                {
                    ShowErrorMessage("Các trường không được để trống!");
                    return;
                }

                if (!SecurityHelper.IsValidUsername(username))
                {
                    ShowErrorMessage("Tên đăng nhập không hợp lệ! Phải bắt đầu bằng @ và chỉ chứa chữ cái và số.");
                    return;
                }

                if (!SecurityHelper.IsValidEmail(email))
                {
                    ShowErrorMessage("Email không hợp lệ!");
                    return;
                }

                if (!SecurityHelper.IsValidPhone(phone))
                {
                    ShowErrorMessage("Số điện thoại không hợp lệ!");
                    return;
                }

                if (password != confirmPassword)
                {
                    ShowErrorMessage("Mật khẩu xác nhận không khớp!");
                    return;
                }

                if (string.IsNullOrEmpty(role) || (role.ToLower() != "player" && role.ToLower() != "viewer"))
                {
                    ShowErrorMessage("Vai trò không hợp lệ! Chỉ chấp nhận Player hoặc Viewer.");
                    return;
                }

                // Tạo đối tượng User
                User user = new User
                {
                    DisplayName = displayName,
                    Username = username,
                    Email = email,
                    Phone = phone,
                    Password = password, // Sẽ được mã hóa trong UserService
                    SecurityQuestion = securityQuestion,
                    SecurityAnswer = securityAnswer,
                    Role = role.ToLower() == "player" ? "Player" : "Viewer",
                    Status = "Pending" // Admin sẽ duyệt sau
                };

                // Đăng ký
                bool success = _userService.Register(user);
                if (success)
                {
                    ShowSuccessMessage("Đăng ký thành công! Vui lòng chờ Admin duyệt tài khoản.");
                }
                else
                {
                    ShowErrorMessage("Đăng ký thất bại! Tên đăng nhập hoặc email đã tồn tại.");
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("Lỗi hiển thị form đăng ký: " + ex.Message);
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
            SafeSetCursorPosition(leftPosition, startY);
            Console.Write(topBorder);

            // Vẽ viền bên
            for (int i = 1; i <= 8; i++)
            {
                SafeSetCursorPosition(leftPosition, startY + i);
                Console.Write(sideBorder);
            }

            // Vẽ viền dưới
            SafeSetCursorPosition(leftPosition, startY + 9);
            Console.Write(bottomBorder);

            // Hiển thị tiêu đề với font đặc biệt và viền kép
            Console.ForegroundColor = ConsoleColor.Cyan;

            // Tạo font pixel cho ESPORT MANAGER dựa theo ảnh mẫu
            string[] pixelFont = CreatePixelFont("ESPORT MANAGER");
            int titleWidth = pixelFont[0].Length;
            int centerX = Math.Max(0, (windowWidth - titleWidth) / 2);

            for (int i = 0; i < pixelFont.Length; i++)
            {
                SafeSetCursorPosition(centerX, startY + 2 + i);
                Console.Write(pixelFont[i]);
            }

            // Hiển thị phụ đề bên dưới tiêu đề
            Console.ForegroundColor = ConsoleColor.Yellow;
            SafeSetCursorPosition(Math.Max(0, (windowWidth - subtitle.Length) / 2), startY + 7);
            Console.Write(subtitle);

            Console.ForegroundColor = ConsoleColor.White;
        }

        // Tạo font kiểu pixel theo hình mẫu
        private string[] CreatePixelFont(string text)
        {
            // Font pixel cho mỗi chữ cái theo mẫu (đơn giản hơn, rõ ràng hơn)
            Dictionary<char, string[]> pixelLetters = new Dictionary<char, string[]>
            {
                {'E', new string[]
                {
                    "█████",
                    "█    ",
                    "████ ",
                    "█    ",
                    "█████"
                }},
                {'S', new string[]
                {
                    "█████",
                    "█    ",
                    "█████",
                    "    █",
                    "█████"
                }},
                {'P', new string[]
                {
                    "█████",
                    "█   █",
                    "█████",
                    "█    ",
                    "█    "
                }},
                {'O', new string[]
                {
                    "█████",
                    "█   █",
                    "█   █",
                    "█   █",
                    "█████"
                }},
                {'R', new string[]
                {
                    "█████",
                    "█   █",
                    "████ ",
                    "█  █ ",
                    "█   █"
                }},
                {'T', new string[]
                {
                    "█████",
                    "  █  ",
                    "  █  ",
                    "  █  ",
                    "  █  "
                }},
                {'M', new string[]
                {
                    "█   █",
                    "██ ██",
                    "█ █ █",
                    "█   █",
                    "█   █"
                }},
                {'A', new string[]
                {
                    "█████",
                    "█   █",
                    "█████",
                    "█   █",
                    "█   █"
                }},
                {'N', new string[]
                {
                    "█   █",
                    "██  █",
                    "█ █ █",
                    "█  ██",
                    "█   █"
                }},
                {'G', new string[]
                {
                    "█████",
                    "█    ",
                    "█  ██",
                    "█   █",
                    "█████"
                }},
                {' ', new string[]
                {
                    "     ",
                    "     ",
                    "     ",
                    "     ",
                    "     "
                }}
            };

            // Tạo mảng kết quả
            string[] result = new string[5];
            for (int i = 0; i < 5; i++)
                result[i] = "";

            // Tạo chữ pixel bằng cách ghép các ký tự
            foreach (char c in text)
            {
                char upperC = char.ToUpper(c);
                if (pixelLetters.ContainsKey(upperC))
                {
                    string[] letterPattern = pixelLetters[upperC];
                    for (int i = 0; i < 5; i++)
                    {
                        result[i] += letterPattern[i] + " ";
                    }
                }
                else
                {
                    // Nếu không có trong từ điển, thêm khoảng trắng
                    for (int i = 0; i < 5; i++)
                    {
                        result[i] += "      ";
                    }
                }
            }

            return result;
        }

        // Phương thức an toàn để đặt vị trí con trỏ
        private void SafeSetCursorPosition(int left, int top)
        {
            try
            {
                // Đảm bảo vị trí nằm trong giới hạn cửa sổ console
                int safeLeft = Math.Max(0, Math.Min(left, Console.WindowWidth - 1));
                int safeTop = Math.Max(0, Math.Min(top, Console.WindowHeight - 1));

                Console.SetCursorPosition(safeLeft, safeTop);
            }
            catch
            {
                // Nếu vẫn có lỗi, bỏ qua việc đặt vị trí con trỏ
            }
        }

        // Vẽ form đăng ký
        private void DrawRegisterForm()
        {
            int windowWidth = Console.WindowWidth;
            int startY = 18;

            // Hiển thị form đăng ký đẹp hơn, đồng bộ với thiết kế chung
            int formWidth = 50;
            int leftPosition = Math.Max(0, (windowWidth - formWidth) / 2);

            Console.ForegroundColor = ConsoleColor.DarkCyan;

            // Vẽ khung form
            string topBorder = "┌" + new string('─', formWidth - 2) + "┐";
            string bottomBorder = "└" + new string('─', formWidth - 2) + "┘";
            string sideBorder = "│" + new string(' ', formWidth - 2) + "│";
            string titleBar = "│" + "ĐĂNG KÝ TÀI KHOẢN".PadLeft((formWidth + "ĐĂNG KÝ TÀI KHOẢN".Length) / 2 - 1).PadRight(formWidth - 2) + "│";
            string separator = "├" + new string('─', formWidth - 2) + "┤";

            SafeSetCursorPosition(leftPosition, startY - 2);
            Console.Write(topBorder);

            SafeSetCursorPosition(leftPosition, startY - 1);
            Console.Write(titleBar);

            SafeSetCursorPosition(leftPosition, startY);
            Console.Write(separator);

            // Vẽ phần thân form với không gian cho các trường nhập liệu
            for (int i = 1; i <= 10; i++)
            {
                SafeSetCursorPosition(leftPosition, startY + i);
                Console.Write(sideBorder);
            }

            SafeSetCursorPosition(leftPosition, startY + 11);
            Console.Write(separator);

            SafeSetCursorPosition(leftPosition, startY + 12);
            Console.Write("│" + "ESC: Hủy bỏ   ENTER: Xác nhận".PadLeft((formWidth + "ESC: Hủy bỏ   ENTER: Xác nhận".Length) / 2 - 1).PadRight(formWidth - 2) + "│");

            SafeSetCursorPosition(leftPosition, startY + 13);
            Console.Write(bottomBorder);

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

        // Căn giữa văn bản
        private string CenterText(string text)
        {
            int screenWidth = Console.WindowWidth;

            // Nếu kích thước văn bản lớn hơn kích thước cửa sổ, cắt văn bản
            if (text.Length >= screenWidth)
            {
                return text.Substring(0, screenWidth - 3) + "...";
            }

            int leftPadding = Math.Max(0, (screenWidth - text.Length) / 2);
            return new string(' ', leftPadding) + text;
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