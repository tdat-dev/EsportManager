using System;
using System.Collections.Generic;
using EsportManager.UI.Interfaces;
using EsportManager.BLL.Interfaces;
using EsportManager.Models;
using System.Linq;
using EsportManager.Utils;

namespace EsportManager.UI.Screens
{
    public class AdminMenuScreen : IScreen
    {
        private readonly IUserService _userService;
        // TODO: Thêm các service cần thiết khác

        public AdminMenuScreen(IUserService userService)
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
                DrawSpecialHeader("ESPORT MANAGER", "[ADMIN PANEL]");

                try
                {
                    // Hiển thị menu
                    string[] options = {
                        "1. Quản lý người dùng",
                        "2. Quản lý giải đấu",
                        "3. Quản lý đội tuyển",
                        "4. Thống kê hệ thống",
                        "5. Cấu hình hệ thống",
                        "6. Đăng xuất"
                    };

                    // Vẽ menu đơn giản
                    DrawSimpleMenu(options);

                    // Xử lý lựa chọn
                    int selectedIndex = HandleMenuSelection(options);

                    // Xử lý lựa chọn
                    switch (selectedIndex)
                    {
                        case 0: // Quản lý người dùng
                            ManageUsers();
                            break;
                        case 1: // Quản lý giải đấu
                            // TODO: Implement tournament management
                            ShowInfoMessage("Chức năng quản lý giải đấu đang được phát triển...");
                            break;
                        case 2: // Quản lý đội tuyển
                            // TODO: Implement team management
                            ShowInfoMessage("Chức năng quản lý đội tuyển đang được phát triển...");
                            break;
                        case 3: // Thống kê hệ thống
                            ShowStatistics();
                            break;
                        case 4: // Cấu hình hệ thống
                            // TODO: Implement system settings
                            ShowInfoMessage("Chức năng cấu hình hệ thống đang được phát triển...");
                            break;
                        case 5: // Đăng xuất
                            // Quay lại màn hình chính
                            return;
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine("Lỗi hiển thị menu: " + ex.Message);
                    Console.WriteLine("Vui lòng mở rộng cửa sổ console và nhấn phím bất kỳ để thử lại...");
                    Console.ReadKey(true);
                }
            }
        }

        private void ManageUsers()
        {
            Console.Clear();
            try
            {
                // Thiết lập màu nền đen
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();

                DrawSpecialHeader("ESPORT MANAGER", "[QUẢN LÝ NGƯỜI DÙNG]");

                // Lấy danh sách người dùng
                List<User> users = _userService.GetAllUsers();

                // Hiển thị danh sách người dùng
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition((Console.WindowWidth - 25) / 2, 16);
                Console.WriteLine($"Tổng số người dùng: {users.Count}");

                // Hiển thị bảng người dùng đơn giản
                DrawSimpleTable(users);

                // Hướng dẫn
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.SetCursorPosition((Console.WindowWidth - 40) / 2, Console.CursorTop + 2);
                Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                Console.ReadKey(true);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Lỗi: " + ex.Message);
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

        // Vẽ bảng đơn giản
        private void DrawSimpleTable(List<User> users)
        {
            int startY = 18;
            int maxUsers = Math.Min(users.Count, 10); // Hiển thị tối đa 10 người dùng

            // Vẽ header
            SafeSetCursorPosition(10, startY);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("ID   Tên người dùng      Email                 Vai trò    Trạng thái");
            Console.WriteLine(new string('-', 80).PadLeft(10 + 80));

            // Vẽ dữ liệu
            for (int i = 0; i < maxUsers; i++)
            {
                User user = users[i];
                SafeSetCursorPosition(10, startY + 2 + i);
                Console.ForegroundColor = ConsoleColor.White;

                string id = user.UserID.ToString().PadRight(5);
                string name = (user.DisplayName?.Length > 18 ? user.DisplayName.Substring(0, 15) + "..." : user.DisplayName?.PadRight(18) ?? "".PadRight(18));
                string email = (user.Email?.Length > 20 ? user.Email.Substring(0, 17) + "..." : user.Email?.PadRight(20) ?? "".PadRight(20));
                string role = (user.Role?.PadRight(10) ?? "".PadRight(10));
                string status = user.Status ?? "";

                Console.Write($"{id}{name}{email}{role}{status}");
            }

            // Hiển thị thông tin khi số lượng người dùng nhiều hơn
            if (users.Count > maxUsers)
            {
                SafeSetCursorPosition(10, startY + 2 + maxUsers);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"... và {users.Count - maxUsers} người dùng khác");
            }
        }

        private void ShowStatistics()
        {
            Console.Clear();
            try
            {
                // Thiết lập màu nền đen
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();

                DrawSpecialHeader("ESPORT MANAGER", "[THỐNG KÊ HỆ THỐNG]");

                // Lấy danh sách người dùng
                List<User> users = _userService.GetAllUsers();

                // Tính toán các thống kê từ danh sách người dùng
                int totalUsers = users.Count;
                int activeUsers = users.Count(u => u.Status == "Active");
                int pendingUsers = users.Count(u => u.Status == "Pending");
                int blockedUsers = users.Count(u => u.Status == "Blocked");

                // Vẽ các thanh thống kê đơn giản
                int startY = 18;
                DrawSimpleBar("Tổng số người dùng", totalUsers, totalUsers > 0 ? totalUsers : 1, ConsoleColor.Cyan, startY);
                DrawSimpleBar("Tài khoản đang hoạt động", activeUsers, totalUsers > 0 ? totalUsers : 1, ConsoleColor.Green, startY + 3);
                DrawSimpleBar("Tài khoản chờ duyệt", pendingUsers, totalUsers > 0 ? totalUsers : 1, ConsoleColor.Yellow, startY + 6);
                DrawSimpleBar("Tài khoản bị khóa", blockedUsers, totalUsers > 0 ? totalUsers : 1, ConsoleColor.Red, startY + 9);

                // Hướng dẫn
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.SetCursorPosition((Console.WindowWidth - 40) / 2, startY + 12);
                Console.WriteLine("Nhấn phím bất kỳ để quay lại...");
                Console.ReadKey(true);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Lỗi: " + ex.Message);
            }
        }

        // Vẽ thanh tiến trình đơn giản
        private void DrawSimpleBar(string label, int value, int maxValue, ConsoleColor color, int y)
        {
            int barLength = 40; // Độ dài của thanh
            int x = 20; // Vị trí x bắt đầu

            // Hiển thị nhãn
            SafeSetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(label.PadRight(30));

            // Vẽ thanh tiến trình
            SafeSetCursorPosition(x + 30, y);
            Console.Write("[");

            int filledLength = (int)Math.Ceiling((double)value / maxValue * barLength);
            filledLength = Math.Min(filledLength, barLength);

            Console.ForegroundColor = color;
            Console.Write(new string('#', filledLength));

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(new string('-', barLength - filledLength));

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"] {value}/{maxValue}");
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
    }
}