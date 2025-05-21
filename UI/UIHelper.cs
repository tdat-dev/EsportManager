using System;
using System.Collections.Generic;

namespace EsportManager.UI
{
    public static class UIHelper
    {
        // Các ký tự Unicode để vẽ khung đẹp
        private static class BoxChars
        {
            public const char TopLeft = '╔';
            public const char TopRight = '╗';
            public const char BottomLeft = '╚';
            public const char BottomRight = '╝';
            public const char Horizontal = '═';
            public const char Vertical = '║';
            public const char TRight = '╠';
            public const char TLeft = '╣';
            public const char TUp = '╩';
            public const char TDown = '╦';
            public const char Cross = '╬';
        }

        // ASCII Art chữ cái dạng lớn cho tiêu đề
        private static readonly Dictionary<char, string[]> AsciiLetters = new Dictionary<char, string[]>
        {
            {'A', new string[]
                {
                    "   ██     ",
                    "  ████    ",
                    " ██  ██   ",
                    "████████  ",
                    "██    ██  ",
                    "██    ██  ",
                    "          "
                }
            },
            {'B', new string[]
                {
                    "███████   ",
                    "██    ██  ",
                    "███████   ",
                    "██    ██  ",
                    "██    ██  ",
                    "███████   ",
                    "          "
                }
            },
            {'C', new string[]
                {
                    " ███████  ",
                    "██        ",
                    "██        ",
                    "██        ",
                    "██        ",
                    " ███████  ",
                    "          "
                }
            },
            {'D', new string[]
                {
                    "███████   ",
                    "██    ██  ",
                    "██    ██  ",
                    "██    ██  ",
                    "██    ██  ",
                    "███████   ",
                    "          "
                }
            },
            {'E', new string[]
                {
                    "████████  ",
                    "██        ",
                    "██████    ",
                    "██        ",
                    "██        ",
                    "████████  ",
                    "          "
                }
            },
            {'F', new string[]
                {
                    "████████  ",
                    "██        ",
                    "██████    ",
                    "██        ",
                    "██        ",
                    "██        ",
                    "          "
                }
            },
            {'G', new string[]
                {
                    " ███████  ",
                    "██        ",
                    "██        ",
                    "██   ███  ",
                    "██    ██  ",
                    " ███████  ",
                    "          "
                }
            },
            {'H', new string[]
                {
                    "██    ██  ",
                    "██    ██  ",
                    "████████  ",
                    "██    ██  ",
                    "██    ██  ",
                    "██    ██  ",
                    "          "
                }
            },
            {'I', new string[]
                {
                    "████████  ",
                    "   ██     ",
                    "   ██     ",
                    "   ██     ",
                    "   ██     ",
                    "████████  ",
                    "          "
                }
            },
            {'J', new string[]
                {
                    "    ████  ",
                    "      ██  ",
                    "      ██  ",
                    "      ██  ",
                    "██    ██  ",
                    " ██████   ",
                    "          "
                }
            },
            {'K', new string[]
                {
                    "██    ██  ",
                    "██   ██   ",
                    "████      ",
                    "██  ██    ",
                    "██   ██   ",
                    "██    ██  ",
                    "          "
                }
            },
            {'L', new string[]
                {
                    "██        ",
                    "██        ",
                    "██        ",
                    "██        ",
                    "██        ",
                    "████████  ",
                    "          "
                }
            },
            {'M', new string[]
                {
                    "██    ██  ",
                    "███  ███  ",
                    "████████  ",
                    "██ ██ ██  ",
                    "██    ██  ",
                    "██    ██  ",
                    "          "
                }
            },
            {'N', new string[]
                {
                    "██    ██  ",
                    "███   ██  ",
                    "████  ██  ",
                    "██ ██ ██  ",
                    "██  ████  ",
                    "██   ███  ",
                    "          "
                }
            },
            {'O', new string[]
                {
                    " ██████   ",
                    "██    ██  ",
                    "██    ██  ",
                    "██    ██  ",
                    "██    ██  ",
                    " ██████   ",
                    "          "
                }
            },
            {'P', new string[]
                {
                    "███████   ",
                    "██    ██  ",
                    "██    ██  ",
                    "███████   ",
                    "██        ",
                    "██        ",
                    "          "
                }
            },
            {'Q', new string[]
                {
                    " ██████   ",
                    "██    ██  ",
                    "██    ██  ",
                    "██    ██  ",
                    "██  ████  ",
                    " ██████   ",
                    "      ██  "
                }
            },
            {'R', new string[]
                {
                    "███████   ",
                    "██    ██  ",
                    "██    ██  ",
                    "███████   ",
                    "██  ██    ",
                    "██   ██   ",
                    "          "
                }
            },
            {'S', new string[]
                {
                    " ███████  ",
                    "██        ",
                    " ██████   ",
                    "      ██  ",
                    "      ██  ",
                    "███████   ",
                    "          "
                }
            },
            {'T', new string[]
                {
                    "████████  ",
                    "   ██     ",
                    "   ██     ",
                    "   ██     ",
                    "   ██     ",
                    "   ██     ",
                    "          "
                }
            },
            {'U', new string[]
                {
                    "██    ██  ",
                    "██    ██  ",
                    "██    ██  ",
                    "██    ██  ",
                    "██    ██  ",
                    " ██████   ",
                    "          "
                }
            },
            {'V', new string[]
                {
                    "██    ██  ",
                    "██    ██  ",
                    "██    ██  ",
                    "██    ██  ",
                    " ██  ██   ",
                    "  ████    ",
                    "          "
                }
            },
            {'W', new string[]
                {
                    "██    ██  ",
                    "██    ██  ",
                    "██    ██  ",
                    "██ ██ ██  ",
                    "████████  ",
                    "██    ██  ",
                    "          "
                }
            },
            {'X', new string[]
                {
                    "██    ██  ",
                    " ██  ██   ",
                    "  ████    ",
                    "  ████    ",
                    " ██  ██   ",
                    "██    ██  ",
                    "          "
                }
            },
            {'Y', new string[]
                {
                    "██    ██  ",
                    " ██  ██   ",
                    "  ████    ",
                    "   ██     ",
                    "   ██     ",
                    "   ██     ",
                    "          "
                }
            },
            {'Z', new string[]
                {
                    "████████  ",
                    "     ██   ",
                    "   ██     ",
                    " ██       ",
                    "██        ",
                    "████████  ",
                    "          "
                }
            },
            {' ', new string[]
                {
                    "          ",
                    "          ",
                    "          ",
                    "          ",
                    "          ",
                    "          ",
                    "          "
                }
            }
        };

        // Vẽ tiêu đề kiểu ASCII Art sử dụng Dictionary đã định nghĩa
        public static void DrawBigAsciiTitle(string title, string subtitle = null)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;

            int consoleWidth = Console.WindowWidth;
            int consoleHeight = Console.WindowHeight;
            int titleHeight = 10; // Giảm chiều cao để tránh lỗi

            try
            {
                // Vẽ khung đơn giản
                int boxWidth = Math.Min(consoleWidth, 80); // Giới hạn chiều rộng tối đa
                DrawBox(0, 0, boxWidth, titleHeight, ConsoleColor.Cyan);

                // Chuyển tiêu đề sang chữ hoa
                string upperTitle = title.ToUpper();

                // Không vẽ ASCII art nếu console quá nhỏ
                if (consoleWidth < 60 || consoleHeight < 15)
                {
                    // Sử dụng tiêu đề đơn giản nếu màn hình nhỏ
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    WriteCenter(upperTitle, 2, boxWidth);

                    if (subtitle != null)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteCenter(subtitle, 4, boxWidth);
                    }

                    return;
                }

                // Chiều cao cố định của chữ ASCII
                int asciiHeight = 7;

                // Tạo chuỗi ESPORT MANAGER để hiển thị
                Console.ForegroundColor = ConsoleColor.Yellow;
                WriteCenter("E S P O R T   M A N A G E R", 3, boxWidth);

                // Viết phụ đề nếu có
                if (subtitle != null)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    WriteCenter(subtitle, 5, boxWidth);
                }
            }
            catch (Exception ex)
            {
                // Nếu có lỗi, hiển thị tiêu đề đơn giản
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(title.ToUpper());
                if (subtitle != null)
                {
                    Console.WriteLine(subtitle);
                }

                // Ghi log lỗi nếu cần thiết
                Console.WriteLine($"Lỗi hiển thị tiêu đề: {ex.Message}");
                Console.ReadKey(true);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        // Vẽ tiêu đề kiểu ASCII Art lớn giống mẫu LAPTOP STORE
        public static void DrawAsciiTitle(string title, string subtitle = null)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan; // Màu xanh nhạt cho tiêu đề

            int width = Console.WindowWidth;
            int height = subtitle != null ? 8 : 6;
            int x = 2;
            int y = 1;

            // Vẽ khung ngoài
            DrawBox(x, y, width, height);

            // Chuyển tiêu đề sang kiểu chữ lớn với viền
            string upperTitle = title.ToUpper();
            string titleWithOutline = "";

            // Thêm khoảng trống giữa các chữ cái và tạo viền bao quanh
            foreach (char c in upperTitle)
            {
                titleWithOutline += $" {c} ";
            }

            // Hiển thị tiêu đề
            Console.ForegroundColor = ConsoleColor.Cyan;
            WriteCenter("╔" + new string('═', titleWithOutline.Length + 4) + "╗", y + 2, width);
            WriteCenter("║  " + titleWithOutline + "  ║", y + 3, width);
            WriteCenter("╚" + new string('═', titleWithOutline.Length + 4) + "╝", y + 4, width);

            // Viết phụ đề
            if (subtitle != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow; // Màu vàng cho phụ đề
                WriteCenter(subtitle, y + height - 1, width);
            }

            Console.ForegroundColor = ConsoleColor.White; // Trả lại màu mặc định
        }

        // Phương thức tạo tiêu đề kiểu viền rỗng giống LAPTOP STORE
        public static void DrawOutlinedTitle(string title, string subtitle = null)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;

            int width = Console.WindowWidth;
            int height = 10;
            int y = 1;

            // Hiển thị tiêu đề kiểu viền rỗng
            Console.ForegroundColor = ConsoleColor.Cyan;
            string upperTitle = title.ToUpper();

            // Tính toán chiều rộng tổng của chuỗi chữ
            int totalWidth = upperTitle.Length * 6;
            int startX = (width - totalWidth) / 2;

            // Tạo khung chung cho tiêu đề
            DrawBox(0, 0, width, 6);

            // Hiển thị từng chữ cái với viền rỗng
            for (int i = 0; i < upperTitle.Length; i++)
            {
                // Vẽ từng chữ cái lớn
                int letterX = startX + i * 6;

                // Vẽ viền trên
                Console.SetCursorPosition(letterX, y);
                Console.Write("┌───┐");

                // Vẽ viền hai bên và chữ cái
                Console.SetCursorPosition(letterX, y + 1);
                Console.Write("│ " + upperTitle[i] + " │");

                // Vẽ viền dưới
                Console.SetCursorPosition(letterX, y + 2);
                Console.Write("└───┘");
            }

            // Viết phụ đề nếu có
            if (subtitle != null)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                WriteCenter(subtitle, y + 5, width);
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        // Vẽ khung đẹp với các ký tự Unicode
        public static void DrawBox(int x, int y, int width, int height, ConsoleColor borderColor = ConsoleColor.Cyan)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = borderColor;

            // Vẽ góc trên bên trái
            Console.SetCursorPosition(x, y);
            Console.Write(BoxChars.TopLeft);

            // Vẽ cạnh trên
            Console.Write(new string(BoxChars.Horizontal, width - 2));

            // Vẽ góc trên bên phải
            Console.Write(BoxChars.TopRight);

            // Vẽ các cạnh dọc
            for (int i = 1; i < height - 1; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write(BoxChars.Vertical);
                Console.SetCursorPosition(x + width - 1, y + i);
                Console.Write(BoxChars.Vertical);
            }

            // Vẽ góc dưới bên trái
            Console.SetCursorPosition(x, y + height - 1);
            Console.Write(BoxChars.BottomLeft);

            // Vẽ cạnh dưới
            Console.Write(new string(BoxChars.Horizontal, width - 2));

            // Vẽ góc dưới bên phải
            Console.Write(BoxChars.BottomRight);

            Console.ForegroundColor = oldColor;
        }

        // Vẽ khung đơn giản với dấu +, -, | (giữ lại cho khả năng tương thích)
        public static void DrawSimpleBox(int x, int y, int width, int height)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("+" + new string('-', width - 2) + "+");
            for (int i = 1; i < height - 1; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write("|" + new string(' ', width - 2) + "|");
            }
            Console.SetCursorPosition(x, y + height - 1);
            Console.Write("+" + new string('-', width - 2) + "+");
        }

        // Viết văn bản ở giữa
        public static void WriteCenter(string text, int y, int width = 0)
        {
            if (width == 0)
                width = Console.WindowWidth;

            int x = (width - text.Length) / 2;
            Console.SetCursorPosition(x, y);
            Console.Write(text);
        }

        // Vẽ khung tiêu đề lớn, giống như mẫu ảnh Laptop Store
        public static void DrawTitleBox(string title, string subtitle = null)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;

            try
            {
                int consoleWidth = Console.WindowWidth;
                int titleBoxWidth = Math.Min(consoleWidth - 4, 80); // Giới hạn chiều rộng tối đa
                int titleBoxHeight = subtitle != null ? 8 : 6;
                int x = (consoleWidth - titleBoxWidth) / 2; // Căn giữa theo chiều ngang
                int y = 2;

                // Vẽ khung ngoài
                DrawBox(x, y, titleBoxWidth, titleBoxHeight, ConsoleColor.Cyan);

                // Hiển thị tiêu đề kiểu ascii đơn giản
                Console.ForegroundColor = ConsoleColor.Yellow;

                // Tạo tiêu đề với khoảng trắng giữa các chữ cái
                string asciiTitle = "";
                foreach (char c in title.ToUpper())
                {
                    asciiTitle += c + " ";
                }

                WriteCenter(asciiTitle, y + 2, consoleWidth);
                WriteCenter("★ ★ ★ ★ ★", y + 3, consoleWidth);

                // Viết phụ đề
                if (subtitle != null)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    WriteCenter(subtitle, y + 5, consoleWidth);
                }

                Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception)
            {
                // Xử lý lỗi - hiển thị tiêu đề đơn giản nếu có lỗi
                Console.Clear();
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(title.ToUpper());
                if (subtitle != null)
                {
                    Console.WriteLine(subtitle);
                }
            }
        }

        // Vẽ khung nội dung với tiêu đề
        public static void DrawContentBox(int x, int y, int width, int height, string title = null)
        {
            // Vẽ khung chính
            DrawBox(x, y, width, height);

            // Nếu có tiêu đề, vẽ trên đỉnh khung
            if (!string.IsNullOrEmpty(title))
            {
                SafeSetCursorPosition(x + 2, y);
                Console.Write(" " + title + " ");
            }
        }

        // Vẽ form nhập liệu
        public static void DrawInputForm(int x, int y, string[] labels, bool includePassword = false)
        {
            int maxLength = 0;
            foreach (string label in labels)
            {
                if (label.Length > maxLength)
                    maxLength = label.Length;
            }

            int formWidth = maxLength + 25; // Thêm khoảng trống cho input
            int formHeight = labels.Length * 2 + 2;

            DrawContentBox(x, y, formWidth, formHeight, "FORM NHẬP LIỆU");

            for (int i = 0; i < labels.Length; i++)
            {
                SafeSetCursorPosition(x + 3, y + i * 2 + 1);
                Console.Write(labels[i] + ": ");
            }
        }

        // Đọc mật khẩu, hiển thị dưới dạng dấu *
        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }

        // Hiển thị menu
        public static void DrawMenu(string[] options, int x, int y)
        {
            int maxLength = 0;
            foreach (string option in options)
            {
                if (option.Length > maxLength)
                    maxLength = option.Length;
            }

            int menuWidth = maxLength + 6;
            int menuHeight = options.Length + 2;

            DrawContentBox(x, y, menuWidth, menuHeight, "MENU");

            for (int i = 0; i < options.Length; i++)
            {
                Console.SetCursorPosition(x + 3, y + i + 1);
                Console.Write(options[i]);
            }
        }

        // Hiển thị menu với khả năng chọn
        public static int ShowSelectableMenu(string[] options, int x, int y)
        {
            int maxLength = 0;
            foreach (string option in options)
            {
                if (option.Length > maxLength)
                    maxLength = option.Length;
            }

            int menuWidth = maxLength + 6;
            int menuHeight = options.Length + 2;

            DrawContentBox(x, y, menuWidth, menuHeight, "MENU");

            int selectedIndex = 0;
            ConsoleKey key;

            do
            {
                // Hiển thị tất cả các lựa chọn
                for (int i = 0; i < options.Length; i++)
                {
                    Console.SetCursorPosition(x + 3, y + i + 1);

                    if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(options[i].PadRight(maxLength));
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
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

        // Hiển thị thông báo
        public static void ShowMessage(string message)
        {
            int width = message.Length + 10;
            int height = 5;
            int x = (Console.WindowWidth - width) / 2;
            int y = (Console.WindowHeight - height) / 2;

            DrawContentBox(x, y, width, height, "THÔNG BÁO");

            Console.SetCursorPosition(x + 5, y + 2);
            Console.Write(message);

            Console.SetCursorPosition(x + 5, y + 3);
            Console.Write("Nhấn phím bất kỳ để tiếp tục...");

            Console.ReadKey(true);
        }

        // Tạo tiêu đề đơn giản cho cửa sổ nhỏ
        public static void DrawSimpleTitle(string title, string subtitle = null)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;

            int width = Console.WindowWidth;
            int height = subtitle != null ? 8 : 6; // Tăng chiều cao để tiêu đề đẹp hơn

            try
            {
                // Vẽ khung kép cho tiêu đề
                SafeSetCursorPosition(0, 0);
                Console.Write("╔");
                for (int i = 1; i < width - 1; i++)
                    Console.Write("═");
                Console.Write("╗");

                // Vẽ các cạnh bên
                for (int i = 1; i < height - 1; i++)
                {
                    SafeSetCursorPosition(0, i);
                    Console.Write("║");
                    SafeSetCursorPosition(width - 1, i);
                    Console.Write("║");
                }

                // Vẽ cạnh dưới
                SafeSetCursorPosition(0, height - 1);
                Console.Write("╚");
                for (int i = 1; i < width - 1; i++)
                    Console.Write("═");
                Console.Write("╝");

                // Vẽ tiêu đề chính giữa khung với màu nổi bật
                Console.ForegroundColor = ConsoleColor.Yellow;
                WriteCenter("╔" + new string('═', title.Length + 6) + "╗", height / 2 - 1, width);
                WriteCenter("║  " + title.ToUpper() + "  ║", height / 2, width);
                WriteCenter("╚" + new string('═', title.Length + 6) + "╝", height / 2 + 1, width);

                // Viết phụ đề nếu có
                if (subtitle != null)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    WriteCenter(subtitle, height / 2 + 3, width);
                }
            }
            catch (Exception)
            {
                // Xử lý lỗi - hiển thị tiêu đề đơn giản nếu có lỗi
                Console.Clear();
                SafeSetCursorPosition(0, 0);
                Console.WriteLine(title.ToUpper());
                if (subtitle != null)
                {
                    Console.WriteLine(subtitle);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        // Tạo hiệu ứng animation khi khởi động
        public static void ShowStartupAnimation()
        {
            Console.Clear();
            Console.CursorVisible = false;

            int maxWidth = Console.WindowWidth;
            int maxHeight = Console.WindowHeight;
            int middleY = maxHeight / 2;

            // Hiệu ứng phóng to khung
            for (int size = 1; size <= Math.Min(maxWidth, maxHeight) / 2; size += 2)
            {
                Console.Clear();

                int x = (maxWidth - size * 2) / 2;
                int y = (maxHeight - size) / 2;

                if (x < 0 || y < 0) break;

                DrawSimpleBox(x, y, size * 2, size);
                System.Threading.Thread.Sleep(30);
            }

            // Hiệu ứng xuất hiện chữ
            string appName = "ESPORT MANAGER";
            int startX = (maxWidth - appName.Length) / 2;

            // Hiệu ứng nháy chữ "ESPORT MANAGER"
            for (int i = 0; i < 3; i++)
            {
                Console.Clear();
                System.Threading.Thread.Sleep(200);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(startX, middleY);
                Console.Write(appName);

                System.Threading.Thread.Sleep(200);
            }

            // Hiệu ứng đếm ngược
            string loadingText = "Khởi động hệ thống...";
            Console.SetCursorPosition((maxWidth - loadingText.Length) / 2, middleY + 2);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(loadingText);

            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 3; i > 0; i--)
            {
                Console.SetCursorPosition(maxWidth / 2, middleY + 3);
                Console.Write(i + " ");
                System.Threading.Thread.Sleep(500);
            }

            Console.Clear();
        }

        // Hiệu ứng chuyển tiếp giữa các màn hình
        public static void TransitionEffect()
        {
            Console.CursorVisible = false;
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;

            // Hiệu ứng mờ dần
            Console.Clear();
            for (int i = 0; i < width; i += 2)
            {
                // Vẽ các dòng dọc từ hai bên vào giữa
                for (int j = 0; j < height; j++)
                {
                    // Bên trái
                    Console.SetCursorPosition(i, j);
                    Console.Write("█");

                    // Bên phải
                    int rightPos = width - i - 1;
                    if (rightPos >= 0)
                    {
                        Console.SetCursorPosition(rightPos, j);
                        Console.Write("█");
                    }
                }

                System.Threading.Thread.Sleep(5); // Tốc độ hiệu ứng
            }

            // Xóa hiệu ứng
            Console.Clear();
            System.Threading.Thread.Sleep(100);
        }

        // Vẽ khung thông tin dạng bảng
        public static void DrawInfoBox(int x, int y, string title, Dictionary<string, string> data)
        {
            // Tìm chiều dài lớn nhất của khóa và giá trị
            int maxKeyLength = 0;
            int maxValueLength = 0;
            foreach (var item in data)
            {
                if (item.Key.Length > maxKeyLength)
                    maxKeyLength = item.Key.Length;

                if (item.Value.Length > maxValueLength)
                    maxValueLength = item.Value.Length;
            }

            // Tính toán kích thước khung
            int boxWidth = maxKeyLength + maxValueLength + 7; // 3 ký tự cho cột đầu + 4 cho khoảng cách
            int boxHeight = data.Count + 2; // Mỗi dòng dữ liệu + 1 header + 1 padding

            // Vẽ khung chính
            DrawContentBox(x, y, boxWidth, boxHeight, title);

            // Vẽ header với đường kẻ ngang
            SafeSetCursorPosition(x + 1, y + 1);
            Console.Write(new string('─', boxWidth - 2));

            // Hiển thị dữ liệu
            int currentY = y + 2;
            foreach (var item in data)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                SafeSetCursorPosition(x + 2, currentY);
                Console.Write(item.Key);
                Console.ForegroundColor = ConsoleColor.White;
                SafeSetCursorPosition(x + 2 + maxKeyLength + 1, currentY);
                Console.Write(": " + item.Value);
                currentY++;
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        // Vẽ bảng dữ liệu với màu sắc tùy chỉnh
        public static void DrawTable(int x, int y, string title, List<string> headers, List<List<string>> rows, ConsoleColor headerColor = ConsoleColor.Cyan)
        {
            // Tính toán chiều rộng của mỗi cột
            int[] columnWidths = new int[headers.Count];

            // Kiểm tra độ rộng header
            for (int i = 0; i < headers.Count; i++)
            {
                columnWidths[i] = headers[i].Length + 2; // +2 để căn lề
            }

            // Kiểm tra độ rộng các hàng
            foreach (var row in rows)
            {
                for (int i = 0; i < Math.Min(row.Count, headers.Count); i++)
                {
                    if (row[i].Length + 2 > columnWidths[i])
                    {
                        columnWidths[i] = row[i].Length + 2;
                    }
                }
            }

            // Tính tổng chiều rộng bảng
            int totalWidth = 1; // Bắt đầu với 1 cho cạnh trái
            foreach (int width in columnWidths)
            {
                totalWidth += width + 1; // +1 cho khoảng cách giữa các cột
            }

            // Vẽ khung bảng
            int tableHeight = rows.Count + 4; // +4 cho header và viền
            DrawContentBox(x, y, totalWidth, tableHeight, title);

            // Vẽ header
            Console.ForegroundColor = headerColor;
            int currentX = x + 1;
            SafeSetCursorPosition(x + 1, y + 1);
            for (int i = 0; i < headers.Count; i++)
            {
                SafeSetCursorPosition(currentX, y + 1);
                Console.Write(headers[i].PadRight(columnWidths[i]));
                currentX += columnWidths[i] + 1;
            }

            // Vẽ đường kẻ ngang sau header
            SafeSetCursorPosition(x + 1, y + 2);
            Console.Write(new string('─', totalWidth - 2));

            // Vẽ dữ liệu
            for (int rowIndex = 0; rowIndex < rows.Count; rowIndex++)
            {
                var row = rows[rowIndex];

                // Màu chẵn lẻ cho hàng
                Console.ForegroundColor = rowIndex % 2 == 0 ? ConsoleColor.White : ConsoleColor.Gray;

                currentX = x + 1;
                for (int colIndex = 0; colIndex < Math.Min(row.Count, headers.Count); colIndex++)
                {
                    SafeSetCursorPosition(currentX, y + 3 + rowIndex);
                    Console.Write(row[colIndex].PadRight(columnWidths[colIndex]));
                    currentX += columnWidths[colIndex] + 1;
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        // Phương thức an toàn để đặt vị trí con trỏ
        public static void SafeSetCursorPosition(int left, int top)
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

        // Vẽ thanh tiến trình        
        public static void DrawProgressBar(int x, int y, int width, int value, int maxValue, ConsoleColor barColor = ConsoleColor.Green)
        {
            // Vẽ khung thanh tiến trình
            SafeSetCursorPosition(x, y);
            Console.Write("[");
            SafeSetCursorPosition(x + width + 1, y);
            Console.Write("]");

            // Tính toán chiều dài thanh tiến trình
            int progressLength = (int)((double)value / maxValue * width);

            // Vẽ thanh tiến trình
            Console.ForegroundColor = barColor;
            SafeSetCursorPosition(x + 1, y);
            Console.Write(new string('█', progressLength));

            // Vẽ phần còn lại của thanh tiến trình
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(new string('░', width - progressLength));

            // Hiển thị giá trị phần trăm
            Console.ForegroundColor = ConsoleColor.White;
            string percentage = $"{(double)value / maxValue * 100:F1}%";
            SafeSetCursorPosition(x + width + 3, y);
            Console.Write(percentage);
        }

        // Hiển thị hiệu ứng loading với thanh tiến trình
        public static void ShowLoadingBar(string message, int delayMilliseconds = 50, int width = 40, ConsoleColor barColor = ConsoleColor.Green)
        {
            Console.CursorVisible = false;
            int x = (Console.WindowWidth - width - 10) / 2;
            int y = Console.WindowHeight / 2;

            // Vẽ khung loading
            int boxWidth = width + 10;
            int boxHeight = 5;
            DrawContentBox(x - 5, y - 2, boxWidth, boxHeight, "LOADING");

            // Hiển thị thông điệp
            SafeSetCursorPosition(x, y - 1);
            Console.WriteLine(message);

            // Hiệu ứng loading
            for (int i = 0; i <= 100; i += 5)
            {
                DrawProgressBar(x, y, width, i, 100, barColor);
                System.Threading.Thread.Sleep(delayMilliseconds);
            }

            Console.CursorVisible = true;
        }

        // Tạo font pixel cho tiêu đề
        public static string[] CreatePixelFont(string text)
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
    }
}