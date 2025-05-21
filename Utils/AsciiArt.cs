using System;
using System.Collections.Generic;

namespace EsportManager.Utils
{
    public static class AsciiArt
    {
        // Logo chính cho ứng dụng Esport Manager
        public static string[] EsportLogo = new string[]
        {
            @"   ______ _____ ____   ____  ____ ______  ",
            @"  / ____// ___// __ \ / __ \/ __ \/_  __/ ",
            @" / __/   \__ \/ /_/ // / / / /_/ / / /    ",
            @"/ /___  ___/ / ____// /_/ / _, _/ / /     ",
            @"\____/ /____/_/     \____/_/ |_| /_/      ",
            @"    __  ___  ___    _   __  ___    ______  ______ ____  ",
            @"   /  |/  / /   |  / | / / /   |  / ____/ / ____// __ \ ",
            @"  / /|_/ / / /| | /  |/ / / /| | / / __  / __/  / /_/ / ",
            @" / /  / / / ___ |/ /|  / / ___ |/ /_/ / / /___ / _, _/  ",
            @"/_/  /_/ /_/  |_/_/ |_/ /_/  |_|\____/ /_____//_/ |_|   "
        };

        // Trophy art
        public static string[] Trophy = new string[]
        {
            @"     ___________     ",
            @"    '._==_==_=_.'    ",
            @"    .-\:      /-.    ",
            @"   | (|:.     |) |   ",
            @"    '-|:.     |-'    ",
            @"      \::.    /      ",
            @"       '::. .'       ",
            @"         ) (         ",
            @"       _.' '._       ",
            @"      '-------'      "
        };

        // Gamepad art
        public static string[] Gamepad = new string[]
        {
            @"    ____________________________    ",
            @"   /                            \   ",
            @"  |    _________________________|_  ",
            @"  |   /                            \ ",
            @"  |  /                              \ ",
            @"  | |   _                      _    | ",
            @"  | |  (O)                    (O)   | ",
            @"  | |                               | ",
            @"  | |       /\              []      | ",
            @"  | |      /  \             []      | ",
            @"  | |      \  /                     | ",
            @"  | |       \/                      | ",
            @"  | |                               | ",
            @"  |  \_                           _/ ",
            @"  |    \_                       _/   ",
            @"  |      |_____________________|     ",
            @"   \____/                      \____/ "
        };

        // Keyboard art
        public static string[] Keyboard = new string[]
        {
            @"  _________________________________________________  ",
            @" |  _____________________________________________  | ",
            @" | |                                             | | ",
            @" | |                                             | | ",
            @" | |_____________________________________________| | ",
            @" |     _    _    _    _    _    _    _    _    _   | ",
            @" |    / \  / \  / \  / \  / \  / \  / \  / \  / \  | ",
            @" |   | Q || W || E || R || T || Y || U || I || O | | ",
            @" |    \_/  \_/  \_/  \_/  \_/  \_/  \_/  \_/  \_/  | ",
            @" |     _    _    _    _    _    _    _    _    _   | ",
            @" |    / \  / \  / \  / \  / \  / \  / \  / \  / \  | ",
            @" |   | A || S || D || F || G || H || J || K || L | | ",
            @" |    \_/  \_/  \_/  \_/  \_/  \_/  \_/  \_/  \_/  | ",
            @" |_________________________________________________| "
        };

        // PC / Computer art
        public static string[] Computer = new string[]
        {
            @"      _____                    ",
            @"     /     \                   ",
            @"    /       \                  ",
            @"   /         \                 ",
            @"  /           \                ",
            @" /_____________\               ",
            @" |  _________  |               ",
            @" | |         | |               ",
            @" | |  ESPORT | |               ",
            @" | |  GAMING | |               ",
            @" | |_________| |               ",
            @" |   _______   |               ",
            @" |  |   _   |  |               ",
            @" |__|       |__|               ",
            @"    |_______|                  "
        };

        // Fire art
        public static string[] Fire = new string[]
        {
            @"       (                    )",
            @"      (  )               (  )",
            @"       )(                 )( ",
            @"      (  )               (  )",
            @"       )(                 )( ",
            @"     /|  |\             /|  |\",
            @"    / |  | \           / |  | \",
            @"   /__|__|__\         /__|__|__\",
            @"      |  |               |  |",
            @"      |  |               |  |"
        };

        // Star art
        public static string[] Star = new string[]
        {
            @"         .__.",
            @"       .      .",
            @"     .          .",
            @"   .              .",
            @" .____________________.",
            @"           ||",
            @"           ||",
            @"           ||",
            @"           ||",
            @"           ||",
            @"           ||"
        };

        // Hiển thị ASCII art với màu sắc sặc sỡ
        public static void DrawColorfulArt(string[] art, int x, int y, bool randomColors = true)
        {
            Random random = new Random();
            ConsoleColor[] colors = (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor));

            // Loại bỏ màu đen và trắng
            List<ConsoleColor> brightColors = new List<ConsoleColor>();
            foreach (var color in colors)
            {
                if (color != ConsoleColor.Black && color != ConsoleColor.DarkGray
                    && color != ConsoleColor.Gray && color != ConsoleColor.White)
                {
                    brightColors.Add(color);
                }
            }

            for (int i = 0; i < art.Length; i++)
            {
                Console.SetCursorPosition(x, y + i);

                if (randomColors)
                {
                    // Mỗi dòng một màu ngẫu nhiên
                    Console.ForegroundColor = brightColors[random.Next(brightColors.Count)];
                    Console.Write(art[i]);
                }
                else
                {
                    // Mỗi ký tự một màu
                    foreach (char c in art[i])
                    {
                        if (c != ' ')
                        {
                            Console.ForegroundColor = brightColors[random.Next(brightColors.Count)];
                        }
                        Console.Write(c);
                    }
                }
            }

            // Trả lại màu mặc định
            Console.ForegroundColor = ConsoleColor.White;
        }

        // Hiệu ứng chớp nháy sặc sỡ cho ASCII art
        public static void DrawBlinkingArt(string[] art, int x, int y, int times = 3, int delay = 200)
        {
            for (int t = 0; t < times; t++)
            {
                DrawColorfulArt(art, x, y, true);
                System.Threading.Thread.Sleep(delay);

                // Xóa art
                for (int i = 0; i < art.Length; i++)
                {
                    Console.SetCursorPosition(x, y + i);
                    Console.Write(new string(' ', art[i].Length));
                }

                System.Threading.Thread.Sleep(delay / 2);
            }

            // Vẽ lần cuối
            DrawColorfulArt(art, x, y, true);
        }

        // Hiệu ứng trượt từ trái sang phải
        public static void SlideInFromLeft(string[] art, int finalX, int y, int speed = 50)
        {
            int maxLength = 0;
            foreach (string line in art)
            {
                maxLength = Math.Max(maxLength, line.Length);
            }

            for (int currentX = -maxLength; currentX <= finalX; currentX += 2)
            {
                // Xóa hình ảnh cũ
                for (int i = 0; i < art.Length; i++)
                {
                    Console.SetCursorPosition(Math.Max(0, currentX - 2), y + i);
                    Console.Write(new string(' ', maxLength + 2));
                }

                // Vẽ hình ảnh mới
                for (int i = 0; i < art.Length; i++)
                {
                    if (currentX < 0)
                    {
                        // Chỉ vẽ phần nhìn thấy được
                        int visibleStart = Math.Abs(currentX);
                        if (visibleStart < art[i].Length)
                        {
                            Console.SetCursorPosition(0, y + i);
                            Console.Write(art[i].Substring(visibleStart));
                        }
                    }
                    else
                    {
                        Console.SetCursorPosition(currentX, y + i);
                        Console.Write(art[i]);
                    }
                }

                System.Threading.Thread.Sleep(speed);
            }
        }
    }
}