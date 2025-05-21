using System;
using EsportManager.UI.Interfaces;
using EsportManager.BLL.Interfaces;
using System.Collections.Generic;

namespace EsportManager.UI.Screens
{
    public class ForgotPasswordScreen : IScreen
    {
        private readonly IUserService _userService;

        public ForgotPasswordScreen(IUserService userService)
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
            DrawSpecialHeader("ESPORT MANAGER", "[QUÊN MẬT KHẨU]");

            try
            {
                // Vẽ form quên mật khẩu
                DrawForgotPasswordForm();

                // Tính toán vị trí bắt đầu của form
                int formStartLine = 18; // Vị trí bắt đầu form

                // Nhập tên đăng nhập
                int centerX = Math.Max(0, (Console.WindowWidth - 40) / 2);
                SafeSetCursorPosition(centerX, formStartLine + 2);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("→ Tên đăng nhập (@TênID): ");
                Console.ForegroundColor = ConsoleColor.White;
                string username = Console.ReadLine();

                // Lấy câu hỏi bảo mật
                string securityQuestion = _userService.GetSecurityQuestion(username);
                if (string.IsNullOrEmpty(securityQuestion))
                {
                    ShowErrorMessage("Không tìm thấy tài khoản với tên đăng nhập này!");
                    return;
                }

                SafeSetCursorPosition(centerX, formStartLine + 3);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("→ Câu hỏi bảo mật: ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(securityQuestion);
                Console.ForegroundColor = ConsoleColor.White;

                SafeSetCursorPosition(centerX, formStartLine + 4);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("→ Câu trả lời: ");
                Console.ForegroundColor = ConsoleColor.White;
                string answer = Console.ReadLine();

                // Xử lý reset mật khẩu
                bool success = _userService.ResetPassword(username, answer);
                if (success)
                {
                    ShowSuccessMessage("Mật khẩu đã được đặt lại thành 'player123'.\nVui lòng đăng nhập với mật khẩu mới và đổi mật khẩu ngay lập tức.");

                    // Sau khi đặt lại mật khẩu, quay lại màn hình chính
                    DrawTransitionEffect();
                    return;
                }
                else
                {
                    ShowErrorMessage("Câu trả lời không đúng! Vui lòng thử lại.");
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("Lỗi hiển thị form quên mật khẩu: " + ex.Message);
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

        // Vẽ form quên mật khẩu
        private void DrawForgotPasswordForm()
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
            string titleBar = "│" + "QUÊN MẬT KHẨU".PadLeft((formWidth + "QUÊN MẬT KHẨU".Length) / 2 - 1).PadRight(formWidth - 2) + "│";
            string separator = "├" + new string('─', formWidth - 2) + "┤";

            SafeSetCursorPosition(leftPosition, startY - 2);
            Console.Write(topBorder);

            SafeSetCursorPosition(leftPosition, startY - 1);
            Console.Write(titleBar);

            SafeSetCursorPosition(leftPosition, startY);
            Console.Write(separator);

            // Vẽ phần thân form với không gian cho các trường nhập liệu
            for (int i = 1; i <= 5; i++)
            {
                SafeSetCursorPosition(leftPosition, startY + i);
                Console.Write(sideBorder);
            }

            SafeSetCursorPosition(leftPosition, startY + 6);
            Console.Write(separator);

            SafeSetCursorPosition(leftPosition, startY + 7);
            Console.Write("│" + "ESC: Hủy bỏ   ENTER: Xác nhận".PadLeft((formWidth + "ESC: Hủy bỏ   ENTER: Xác nhận".Length) / 2 - 1).PadRight(formWidth - 2) + "│");

            SafeSetCursorPosition(leftPosition, startY + 8);
            Console.Write(bottomBorder);

            Console.ForegroundColor = ConsoleColor.White;
        }

        // Hiệu ứng chuyển tiếp màn hình đẹp
        private void DrawTransitionEffect()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;

            int width = 40;
            string loading = "ĐANG CHUYỂN TRANG";

            for (int i = 0; i < 3; i++)
            {
                Console.Clear();
                Console.WriteLine("\n\n");

                string topBorder = "┌" + new string('─', width) + "┐";
                string bottomBorder = "└" + new string('─', width) + "┘";
                string emptyBorder = "│" + new string(' ', width) + "│";

                Console.WriteLine(CenterText(topBorder));
                Console.WriteLine(CenterText(emptyBorder));

                string dots = new string('.', i + 1);
                string paddedText = loading + dots + new string(' ', width - loading.Length - dots.Length);

                Console.WriteLine(CenterText("│" + paddedText + "│"));
                Console.WriteLine(CenterText(emptyBorder));
                Console.WriteLine(CenterText(bottomBorder));

                System.Threading.Thread.Sleep(200);
            }

            Console.ForegroundColor = ConsoleColor.White;
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