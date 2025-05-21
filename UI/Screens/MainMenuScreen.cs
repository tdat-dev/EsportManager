using System;
using EsportManager.UI.Interfaces;
using EsportManager.BLL.Interfaces;
using EsportManager.Utils;
using System.Collections.Generic;

namespace EsportManager.UI.Screens
{
    public class MainMenuScreen : IScreen
    {
        private readonly IUserService _userService;

        public MainMenuScreen(IUserService userService)
        {
            _userService = userService;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.CursorVisible = false;

                // Thiết lập màu nền đen
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();

                // Hiển thị tiêu đề theo mẫu mới
                DrawSpecialHeader("ESPORT MANAGER", "[MAIN MENU]");

                try
                {
                    // Hiển thị menu
                    string[] options = {
                        "1. Đăng nhập",
                        "2. Đăng ký",
                        "3. Quên mật khẩu",
                        "4. Thoát"
                    };

                    // Vẽ menu đơn giản
                    DrawSimpleMenu(options);

                    // Xử lý lựa chọn
                    IScreen nextScreen = null;
                    switch (HandleMenuSelection(options))
                    {
                        case 0: // Đăng nhập
                            nextScreen = new LoginScreen(_userService);
                            break;
                        case 1: // Đăng ký
                            nextScreen = new RegisterScreen(_userService);
                            break;
                        case 2: // Quên mật khẩu
                            nextScreen = new ForgotPasswordScreen(_userService);
                            break;
                        case 3: // Thoát
                            Console.Clear();
                            Console.WriteLine(CenterText("Cảm ơn bạn đã sử dụng ứng dụng!"));
                            Environment.Exit(0);
                            break;
                    }

                    // Chuyển đến màn hình tiếp theo
                    if (nextScreen != null)
                    {
                        nextScreen.Show();
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Lỗi hiển thị menu: " + ex.Message);
                    Console.WriteLine("Vui lòng mở rộng cửa sổ console và nhấn phím bất kỳ để thử lại...");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ReadKey(true);

                    try
                    {
                        // Thử vẽ menu đơn giản hơn nếu gặp lỗi
                        Console.Clear();
                        Console.WriteLine("\n\n");
                        Console.WriteLine("=== MENU CHÍNH ===");
                        Console.WriteLine("1. Đăng nhập");
                        Console.WriteLine("2. Đăng ký");
                        Console.WriteLine("3. Quên mật khẩu");
                        Console.WriteLine("4. Thoát");
                        Console.WriteLine("\nNhập lựa chọn (1-4): ");

                        var key = Console.ReadKey(true);
                        IScreen nextScreen = null;

                        switch (key.KeyChar)
                        {
                            case '1':
                                nextScreen = new LoginScreen(_userService);
                                break;
                            case '2':
                                nextScreen = new RegisterScreen(_userService);
                                break;
                            case '3':
                                nextScreen = new ForgotPasswordScreen(_userService);
                                break;
                            case '4':
                                Console.Clear();
                                Console.WriteLine(CenterText("Cảm ơn bạn đã sử dụng ứng dụng!"));
                                Environment.Exit(0);
                                break;
                        }

                        if (nextScreen != null)
                        {
                            nextScreen.Show();
                        }
                    }
                    catch
                    {
                        // Nếu vẫn lỗi, hiển thị thông báo và thoát
                        Console.Clear();
                        Console.WriteLine("Lỗi nghiêm trọng khi hiển thị menu. Ứng dụng sẽ đóng.");
                        Console.WriteLine("Nhấn phím bất kỳ để thoát...");
                        Console.ReadKey(true);
                        Environment.Exit(1);
                    }
                }
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

        // Vẽ menu đơn giản
        private void DrawSimpleMenu(string[] options)
        {
            int startY = 18; // Vị trí bắt đầu menu

            for (int i = 0; i < options.Length; i++)
            {
                SafeSetCursorPosition(Math.Max(0, (Console.WindowWidth - options[i].Length) / 2), startY + i);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(options[i]);
            }

            SafeSetCursorPosition(Math.Max(0, (Console.WindowWidth - 38) / 2), startY + options.Length + 1);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("↑↓: Di chuyển   Enter: Chọn");
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

        // Xử lý lựa chọn menu
        private int HandleMenuSelection(string[] options)
        {
            int selectedIndex = 0;
            ConsoleKey key;
            int startY = 18; // Vị trí bắt đầu menu

            do
            {
                // Hiển thị các tùy chọn menu với tùy chọn đang chọn được highlight
                for (int i = 0; i < options.Length; i++)
                {
                    SafeSetCursorPosition(Math.Max(0, (Console.WindowWidth - options[i].Length) / 2), startY + i);

                    // Hiển thị tùy chọn với màu sắc phù hợp
                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(options[i]);
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(options[i]);
                    }
                }

                // Đọc phím
                key = Console.ReadKey(true).Key;

                // Xử lý phím mũi tên
                if (key == ConsoleKey.UpArrow)
                {
                    selectedIndex = (selectedIndex > 0) ? selectedIndex - 1 : options.Length - 1;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selectedIndex = (selectedIndex < options.Length - 1) ? selectedIndex + 1 : 0;
                }

            } while (key != ConsoleKey.Enter);

            return selectedIndex;
        }
    }
}