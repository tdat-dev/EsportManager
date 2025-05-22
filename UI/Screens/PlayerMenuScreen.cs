using System;
using System.Collections.Generic;
using EsportManager.UI.Interfaces;
using EsportManager.BLL.Interfaces;
using EsportManager.Models;
using System.Linq;
using EsportManager.Utils;

namespace EsportManager.UI.Screens
{
    public class PlayerMenuScreen : IScreen
    {
        private readonly IUserService _userService;
        // TODO: Thêm các service cần thiết khác

        public PlayerMenuScreen(IUserService userService)
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
                DrawSpecialHeader("ESPORT MANAGER", "[PLAYER PANEL]");

                try
                {
                    // Hiển thị menu
                    string[] options = {
                        "1. Đăng ký giải đấu",
                        "2. Quản lý đội tuyển (Tạo/Tham gia/Rời đội)",
                        "3. Gửi phản hồi về giải đấu",
                        "4. Quản lý ví quyên góp",
                        "5. Xem thông tin cá nhân",
                        "6. Xem lịch thi đấu",
                        "7. Đăng xuất"
                    };

                    // Vẽ menu đơn giản
                    DrawSimpleMenu(options);

                    // Xử lý lựa chọn
                    int selectedIndex = HandleMenuSelection(options);

                    // Xử lý lựa chọn
                    switch (selectedIndex)
                    {
                        case 0: // Đăng ký giải đấu
                            RegisterForTournament();
                            break;
                        case 1: // Quản lý đội tuyển
                            TeamManagement();
                            break;
                        case 2: // Gửi phản hồi về giải đấu
                            SubmitTournamentFeedback();
                            break;
                        case 3: // Quản lý ví quyên góp
                            DonateWalletManagement();
                            break;
                        case 4: // Xem thông tin cá nhân
                            ViewPersonalInformation();
                            break;
                        case 5: // Xem lịch thi đấu
                            ViewTournamentSchedule();
                            break;
                        case 6: // Đăng xuất
                            return; // Quay về màn hình đăng nhập
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine("Lỗi hiển thị menu Player: " + ex.Message);
                    Console.WriteLine("Nhấn phím bất kỳ để thử lại...");
                    Console.ReadKey(true);
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
            Console.SetCursorPosition(leftPosition, startY);
            Console.Write(topBorder);

            // Vẽ viền bên
            for (int i = 1; i <= 8; i++)
            {
                Console.SetCursorPosition(leftPosition, startY + i);
                Console.Write(sideBorder);
            }

            // Vẽ viền dưới
            Console.SetCursorPosition(leftPosition, startY + 9);
            Console.Write(bottomBorder);

            // Hiển thị tiêu đề với font đặc biệt và viền kép
            Console.ForegroundColor = ConsoleColor.Cyan;

            // Tạo font pixel cho ESPORT MANAGER dựa theo ảnh mẫu
            string[] pixelFont = UIHelper.CreatePixelFont("ESPORT MANAGER");
            int titleWidth = pixelFont[0].Length;
            int centerX = Math.Max(0, (windowWidth - titleWidth) / 2);

            for (int i = 0; i < pixelFont.Length; i++)
            {
                Console.SetCursorPosition(centerX, startY + 2 + i);
                Console.Write(pixelFont[i]);
            }

            // Hiển thị phụ đề bên dưới tiêu đề
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(Math.Max(0, (windowWidth - subtitle.Length) / 2), startY + 7);
            Console.Write(subtitle);

            Console.ForegroundColor = ConsoleColor.White;
        }

        // Hiển thị menu đơn giản
        private void DrawSimpleMenu(string[] options)
        {
            int startY = 18; // Vị trí bắt đầu menu

            for (int i = 0; i < options.Length; i++)
            {
                Console.SetCursorPosition(Math.Max(0, (Console.WindowWidth - options[i].Length) / 2), startY + i);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(options[i]);
            }

            Console.SetCursorPosition(Math.Max(0, (Console.WindowWidth - 38) / 2), startY + options.Length + 1);
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
                    Console.SetCursorPosition(Math.Max(0, (Console.WindowWidth - options[i].Length) / 2), startY + i);

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

        private void RegisterForTournament()
        {
            Console.Clear();
            Console.CursorVisible = true;
            DrawSpecialHeader("ESPORT MANAGER", "[ĐĂNG KÝ GIẢI ĐẤU]");

            try
            {
                Console.SetCursorPosition(0, Console.CursorTop + 3);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(CenterText("=== DANH SÁCH GIẢI ĐẤU ĐANG MỞ ĐĂNG KÝ ==="));
                Console.WriteLine();

                // Danh sách giải đấu mẫu
                string[] tournaments = {
                    "1. VALORANT Championship 2023",
                    "2. League of Legends World Cup",
                    "3. CS:GO Masters",
                    "4. PUBG Mobile Tournament",
                    "5. FIFA 23 Online Championship"
                };

                foreach (string tournament in tournaments)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(CenterText(tournament));
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n" + CenterText("Nhập số thứ tự giải đấu bạn muốn đăng ký (0 để quay lại): "));
                Console.ForegroundColor = ConsoleColor.White;

                string input = Console.ReadLine();

                if (input == "0")
                {
                    return;
                }

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= tournaments.Length)
                {
                    string selectedTournament = tournaments[choice - 1].Substring(3); // Bỏ phần "1. " ở đầu
                    ShowSuccessMessage($"Đăng ký thành công giải đấu: {selectedTournament}\nHãy chờ xác nhận từ ban tổ chức!");
                }
                else
                {
                    ShowErrorMessage("Lựa chọn không hợp lệ. Vui lòng thử lại!");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Lỗi: " + ex.Message);
            }
        }

        private void TeamManagement()
        {
            while (true)
            {
                Console.Clear();
                Console.CursorVisible = false;
                DrawSpecialHeader("ESPORT MANAGER", "[QUẢN LÝ ĐỘI TUYỂN]");

                string[] options = {
                    "1. Tạo đội mới",
                    "2. Tham gia đội",
                    "3. Rời đội",
                    "4. Xem thông tin đội",
                    "5. Quay lại"
                };

                for (int i = 0; i < options.Length; i++)
                {
                    Console.SetCursorPosition(Math.Max(0, (Console.WindowWidth - options[i].Length) / 2), 18 + i);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(options[i]);
                }

                int selectedIndex = HandleMenuSelection(options);

                switch (selectedIndex)
                {
                    case 0: // Tạo đội mới
                        ShowInfoMessage("Chức năng tạo đội mới đang được phát triển...");
                        break;
                    case 1: // Tham gia đội
                        ShowInfoMessage("Chức năng tham gia đội đang được phát triển...");
                        break;
                    case 2: // Rời đội
                        ShowInfoMessage("Chức năng rời đội đang được phát triển...");
                        break;
                    case 3: // Xem thông tin đội
                        ShowInfoMessage("Chức năng xem thông tin đội đang được phát triển...");
                        break;
                    case 4: // Quay lại
                        return;
                }
            }
        }

        private void SubmitTournamentFeedback()
        {
            Console.Clear();
            Console.CursorVisible = true;
            DrawSpecialHeader("ESPORT MANAGER", "[GỬI PHẢN HỒI GIẢI ĐẤU]");

            try
            {
                Console.SetCursorPosition(0, Console.CursorTop + 3);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(CenterText("=== DANH SÁCH GIẢI ĐẤU ĐÃ THAM GIA ==="));
                Console.WriteLine();

                // Danh sách giải đấu mẫu
                string[] tournaments = {
                    "1. VALORANT Championship 2023",
                    "2. League of Legends World Cup",
                    "3. CS:GO Masters"
                };

                foreach (string tournament in tournaments)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(CenterText(tournament));
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n" + CenterText("Nhập số thứ tự giải đấu bạn muốn gửi phản hồi (0 để quay lại): "));
                Console.ForegroundColor = ConsoleColor.White;

                string input = Console.ReadLine();

                if (input == "0")
                {
                    return;
                }

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= tournaments.Length)
                {
                    string selectedTournament = tournaments[choice - 1].Substring(3); // Bỏ phần "1. " ở đầu

                    Console.Clear();
                    DrawSpecialHeader("ESPORT MANAGER", "[GỬI PHẢN HỒI GIẢI ĐẤU]");

                    Console.SetCursorPosition(0, Console.CursorTop + 3);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(CenterText($"Gửi phản hồi cho: {selectedTournament}"));
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(CenterText("Nhập phản hồi của bạn (Enter hai lần để kết thúc):"));
                    Console.WriteLine();

                    string feedback = "";
                    string line;
                    while (!string.IsNullOrEmpty(line = Console.ReadLine()))
                    {
                        feedback += line + "\n";
                    }

                    if (!string.IsNullOrWhiteSpace(feedback))
                    {
                        ShowSuccessMessage($"Đã gửi phản hồi cho giải đấu: {selectedTournament}");
                    }
                    else
                    {
                        ShowErrorMessage("Phản hồi không được để trống!");
                    }
                }
                else
                {
                    ShowErrorMessage("Lựa chọn không hợp lệ. Vui lòng thử lại!");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Lỗi: " + ex.Message);
            }
        }

        private void DonateWalletManagement()
        {
            Console.Clear();
            Console.CursorVisible = false;
            DrawSpecialHeader("ESPORT MANAGER", "[QUẢN LÝ VÍ QUYÊN GÓP]");

            try
            {
                Console.SetCursorPosition(0, Console.CursorTop + 3);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(CenterText("=== THÔNG TIN VÍ QUYÊN GÓP ==="));
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(CenterText("Số dư hiện tại: 5,000,000 VNĐ"));
                Console.WriteLine(CenterText("Số tiền đã nhận: 8,500,000 VNĐ"));
                Console.WriteLine(CenterText("Số tiền đã rút: 3,500,000 VNĐ"));
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(CenterText("=== LỊCH SỬ GIAO DỊCH GẦN ĐÂY ==="));
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(CenterText("+ 1,000,000 VNĐ - Từ: Viewer001 - 12/10/2023"));
                Console.WriteLine(CenterText("+ 500,000 VNĐ - Từ: Viewer123 - 10/10/2023"));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(CenterText("- 2,000,000 VNĐ - Rút tiền - 05/10/2023"));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(CenterText("+ 2,000,000 VNĐ - Từ: Viewer056 - 01/10/2023"));
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(CenterText("Nhấn phím bất kỳ để quay lại..."));
                Console.ReadKey(true);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Lỗi: " + ex.Message);
            }
        }

        private void ViewPersonalInformation()
        {
            Console.Clear();
            Console.CursorVisible = false;
            DrawSpecialHeader("ESPORT MANAGER", "[THÔNG TIN CÁ NHÂN]");

            try
            {
                Console.SetCursorPosition(0, Console.CursorTop + 3);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(CenterText("=== THÔNG TIN NGƯỜI CHƠI ==="));
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(CenterText("Tên hiển thị: Người chơi"));
                Console.WriteLine(CenterText("Tên đăng nhập: @player"));
                Console.WriteLine(CenterText("Email: player@example.com"));
                Console.WriteLine(CenterText("Số điện thoại: 0987654322"));
                Console.WriteLine(CenterText("Vai trò: Player"));
                Console.WriteLine(CenterText("Trạng thái: Đã duyệt"));
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(CenterText("=== THÀNH TÍCH ==="));
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(CenterText("- Hạng nhất VALORANT Championship 2022"));
                Console.WriteLine(CenterText("- Top 3 CS:GO Masters 2023"));
                Console.WriteLine(CenterText("- MVP League of Legends World Cup 2023"));
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(CenterText("Nhấn phím bất kỳ để quay lại..."));
                Console.ReadKey(true);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Lỗi: " + ex.Message);
            }
        }

        private void ViewTournamentSchedule()
        {
            Console.Clear();
            Console.CursorVisible = false;
            DrawSpecialHeader("ESPORT MANAGER", "[LỊCH THI ĐẤU]");

            try
            {
                Console.SetCursorPosition(0, Console.CursorTop + 3);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(CenterText("=== LỊCH THI ĐẤU SẮP TỚI ==="));
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(CenterText("VALORANT Championship 2023"));
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(CenterText("- Tứ kết: 15/11/2023 - 15:00"));
                Console.WriteLine(CenterText("- Bán kết: 20/11/2023 - 15:00"));
                Console.WriteLine(CenterText("- Chung kết: 25/11/2023 - 19:00"));
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(CenterText("League of Legends World Cup"));
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(CenterText("- Vòng bảng: 01/12/2023 - 14:00"));
                Console.WriteLine(CenterText("- Vòng 16: 10/12/2023 - 14:00"));
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(CenterText("Nhấn phím bất kỳ để quay lại..."));
                Console.ReadKey(true);
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Lỗi: " + ex.Message);
            }
        }

        // Hiển thị thông báo thành công
        private void ShowSuccessMessage(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("\n\n");
            Console.WriteLine(CenterText("=== THÔNG BÁO ==="));
            Console.WriteLine();

            // Xử lý thông báo nhiều dòng
            string[] lines = message.Split('\n');
            foreach (string line in lines)
            {
                Console.WriteLine(CenterText(line));
            }

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n" + CenterText("Nhấn phím bất kỳ để tiếp tục..."));
            Console.ReadKey(true);
        }
    }
}