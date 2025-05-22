using System;
using System.Collections.Generic;

namespace EsportManager.Utils
{
    public static class UIHelper
    {
        // Phương thức an toàn để xóa màn hình console
        public static void SafeClear()
        {
            try
            {
                Console.Clear();
            }
            catch (Exception)
            {
                // Nếu không thể xóa màn hình, thử cách khác: in nhiều dòng trống
                try
                {
                    // In 50 dòng trống để "xóa" màn hình một cách giả lập
                    for (int i = 0; i < 50; i++)
                    {
                        Console.WriteLine();
                    }
                    // Đặt con trỏ về đầu
                    Console.SetCursorPosition(0, 0);
                }
                catch
                {
                    // Nếu vẫn lỗi, bỏ qua không làm gì cả
                }
            }
        }

        // Phương thức an toàn để đặt vị trí con trỏ
        public static void SafeSetCursorPosition(int left, int top)
        {
            try
            {
                // Đảm bảo vị trí nằm trong giới hạn cửa sổ console
                int windowWidth = SafeGetWindowWidth();
                int windowHeight = SafeGetWindowHeight();

                int safeLeft = Math.Max(0, Math.Min(left, windowWidth - 1));
                int safeTop = Math.Max(0, Math.Min(top, windowHeight - 1));

                Console.SetCursorPosition(safeLeft, safeTop);
            }
            catch
            {
                // Nếu vẫn có lỗi, bỏ qua việc đặt vị trí con trỏ
            }
        }

        // Tạo font kiểu pixel theo hình mẫu
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

            // Khởi tạo mảng kết quả
            string[] result = new string[5];
            for (int i = 0; i < 5; i++)
            {
                result[i] = "";
            }

            // Tạo chữ pixel cho mỗi ký tự trong chuỗi
            foreach (char c in text.ToUpper())
            {
                if (pixelLetters.ContainsKey(c))
                {
                    string[] letterPattern = pixelLetters[c];
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

        // Phương thức an toàn để lấy chiều rộng cửa sổ console
        public static int SafeGetWindowWidth(int defaultWidth = 80)
        {
            try
            {
                return Console.WindowWidth;
            }
            catch
            {
                return defaultWidth;
            }
        }

        // Phương thức an toàn để lấy chiều cao cửa sổ console
        public static int SafeGetWindowHeight(int defaultHeight = 25)
        {
            try
            {
                return Console.WindowHeight;
            }
            catch
            {
                return defaultHeight;
            }
        }
    }
}