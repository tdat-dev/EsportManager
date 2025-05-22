using System;
using System.Collections.Generic;
using EsportManager.UI.Interfaces;
using EsportManager.BLL.Interfaces;
using EsportManager.Models;
using System.Linq;
using EsportManager.Utils;

namespace EsportManager.UI.Screens
{
    public class ViewerMenuScreen : IScreen
    {
        private readonly IUserService _userService;
        // TODO: Thêm các service cần thiết khác

        public ViewerMenuScreen(IUserService userService)
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
                DrawSpecialHeader("ESPORT MANAGER", "[VIEWER PANEL]");

                try
                {
                    // Hiển thị menu
                    string[] options = {
                        "1. Xem giải đấu",
                        "2. Xem lịch thi đấu",
                        "3. Xem thông tin đội tuyển",
                        "4. Thông tin cá nhân",
                        "5. Đăng xuất"
                    };

                    // Vẽ menu đơn giản
                    DrawSimpleMenu(options);

                    // Xử lý lựa chọn
                    int selectedIndex = HandleMenuSelection(options);

                    // Xử lý lựa chọn
                    switch (selectedIndex)
                    {
                        case 0: // Xem giải đấu
                            ViewTournamentList();
                            break;
                        case 1: // Xem lịch thi đấu
                            ShowInfoMessage("Chức năng xem lịch thi đấu đang được phát triển...");
                            break;
                        case 2: // Xem thông tin đội tuyển
                            ShowInfoMessage("Chức năng xem thông tin đội tuyển đang được phát triển...");
                            break;
                        case 3: // Thông tin cá nhân
                            UpdatePersonalProfile();
                            break;
                        case 4: // Đăng xuất
                            return; // Quay về màn hình đăng nhập
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine("Lỗi hiển thị menu Viewer: " + ex.Message);
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

        private void ViewTournamentList()
        {
            Console.Clear();
            Console.CursorVisible = false;
            DrawSpecialHeader("ESPORT MANAGER", "[DANH SÁCH GIẢI ĐẤU]");

            try
            {
                Console.SetCursorPosition(0, Console.CursorTop + 3);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(CenterText("=== DANH SÁCH GIẢI ĐẤU ĐANG DIỄN RA ==="));
                Console.WriteLine();

                // Danh sách giải đấu mẫu
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(CenterText("VALORANT Championship 2023"));
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(CenterText("Thời gian: 15/11/2023 - 25/11/2023"));
                Console.WriteLine(CenterText("Trạng thái: Đang diễn ra"));
                Console.WriteLine(CenterText("Số đội tham gia: 16"));
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(CenterText("League of Legends World Cup"));
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(CenterText("Thời gian: 01/12/2023 - 20/12/2023"));
                Console.WriteLine(CenterText("Trạng thái: Sắp diễn ra"));
                Console.WriteLine(CenterText("Số đội tham gia: 24"));
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(CenterText("CS:GO Masters"));
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(CenterText("Thời gian: 10/10/2023 - 30/10/2023"));
                Console.WriteLine(CenterText("Trạng thái: Đã kết thúc"));
                Console.WriteLine(CenterText("Số đội tham gia: 12"));
                Console.WriteLine(CenterText("Đội vô địch: Natus Vincere"));
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

        private void ViewTournamentStandings()
        {
            Console.Clear();
            Console.CursorVisible = true;
            DrawSpecialHeader("ESPORT MANAGER", "[XẾP HẠNG GIẢI ĐẤU]");

            try
            {
                Console.SetCursorPosition(0, Console.CursorTop + 3);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(CenterText("=== CHỌN GIẢI ĐẤU ĐỂ XEM XẾP HẠNG ==="));
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
                Console.WriteLine("\n" + CenterText("Nhập số thứ tự giải đấu bạn muốn xem (0 để quay lại): "));
                Console.ForegroundColor = ConsoleColor.White;

                string input = Console.ReadLine();

                if (input == "0")
                {
                    return;
                }

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= tournaments.Length)
                {
                    Console.Clear();
                    DrawSpecialHeader("ESPORT MANAGER", "[XẾP HẠNG GIẢI ĐẤU]");

                    Console.SetCursorPosition(0, Console.CursorTop + 3);
                    Console.ForegroundColor = ConsoleColor.Yellow;

                    // Hiển thị xếp hạng tùy theo giải đấu
                    switch (choice)
                    {
                        case 1: // VALORANT
                            Console.WriteLine(CenterText("=== XẾP HẠNG VALORANT CHAMPIONSHIP 2023 ==="));
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(CenterText("1. Team Liquid - 5 thắng, 0 thua"));
                            Console.WriteLine(CenterText("2. Fnatic - 4 thắng, 1 thua"));
                            Console.WriteLine(CenterText("3. Cloud9 - 3 thắng, 2 thua"));
                            Console.WriteLine(CenterText("4. Sentinels - 2 thắng, 3 thua"));
                            break;

                        case 2: // League of Legends
                            Console.WriteLine(CenterText("=== XẾP HẠNG LEAGUE OF LEGENDS WORLD CUP ==="));
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(CenterText("Giải đấu chưa bắt đầu"));
                            break;

                        case 3: // CS:GO
                            Console.WriteLine(CenterText("=== XẾP HẠNG CS:GO MASTERS (KẾT THÚC) ==="));
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(CenterText("1. Natus Vincere - Vô địch"));
                            Console.WriteLine(CenterText("2. Astralis - Á quân"));
                            Console.WriteLine(CenterText("3. Team Vitality - Hạng ba"));
                            Console.WriteLine(CenterText("4. G2 Esports - Hạng tư"));
                            break;
                    }

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(CenterText("Nhấn phím bất kỳ để quay lại..."));
                    Console.ReadKey(true);
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

        private void VoteForContent()
        {
            Console.Clear();
            Console.CursorVisible = true;
            DrawSpecialHeader("ESPORT MANAGER", "[BÌNH CHỌN]");

            try
            {
                Console.SetCursorPosition(0, Console.CursorTop + 3);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(CenterText("=== CHỌN DANH MỤC ĐỂ BÌNH CHỌN ==="));
                Console.WriteLine();

                string[] categories = {
                    "1. Bình chọn Player xuất sắc nhất",
                    "2. Bình chọn Giải đấu hấp dẫn nhất",
                    "3. Bình chọn Môn thể thao yêu thích",
                    "4. Quay lại"
                };

                foreach (string category in categories)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(CenterText(category));
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n" + CenterText("Nhập lựa chọn của bạn: "));
                Console.ForegroundColor = ConsoleColor.White;

                string input = Console.ReadLine();

                if (input == "4")
                {
                    return;
                }

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 3)
                {
                    Console.Clear();
                    DrawSpecialHeader("ESPORT MANAGER", "[BÌNH CHỌN]");

                    Console.SetCursorPosition(0, Console.CursorTop + 3);
                    Console.ForegroundColor = ConsoleColor.Yellow;

                    // Hiển thị các lựa chọn bình chọn tùy theo danh mục
                    string title = "";
                    string[] options = null;

                    switch (choice)
                    {
                        case 1: // Player
                            title = "=== BÌNH CHỌN PLAYER XUẤT SẮC NHẤT ===";
                            options = new string[] {
                                "1. Simple (CS:GO)",
                                "2. Faker (LoL)",
                                "3. TenZ (VALORANT)",
                                "4. Shroud (PUBG)",
                                "5. Quay lại"
                            };
                            break;

                        case 2: // Tournament
                            title = "=== BÌNH CHỌN GIẢI ĐẤU HẤP DẪN NHẤT ===";
                            options = new string[] {
                                "1. VALORANT Championship 2023",
                                "2. League of Legends World Cup",
                                "3. CS:GO Masters",
                                "4. PUBG Mobile Tournament",
                                "5. Quay lại"
                            };
                            break;

                        case 3: // Sport
                            title = "=== BÌNH CHỌN MÔN THỂ THAO YÊU THÍCH ===";
                            options = new string[] {
                                "1. VALORANT",
                                "2. League of Legends",
                                "3. CS:GO",
                                "4. PUBG Mobile",
                                "5. FIFA Online",
                                "6. Quay lại"
                            };
                            break;
                    }

                    Console.WriteLine(CenterText(title));
                    Console.WriteLine();

                    foreach (string option in options)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine(CenterText(option));
                    }

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n" + CenterText("Nhập lựa chọn của bạn: "));
                    Console.ForegroundColor = ConsoleColor.White;

                    string voteInput = Console.ReadLine();

                    if (voteInput == options.Length.ToString())
                    {
                        VoteForContent(); // Quay lại menu bình chọn
                        return;
                    }

                    if (int.TryParse(voteInput, out int voteChoice) && voteChoice >= 1 && voteChoice < options.Length)
                    {
                        string selectedOption = options[voteChoice - 1].Substring(3); // Bỏ phần "1. " ở đầu
                        ShowSuccessMessage($"Bạn đã bình chọn thành công cho: {selectedOption}");
                    }
                    else
                    {
                        ShowErrorMessage("Lựa chọn không hợp lệ. Vui lòng thử lại!");
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

        private void DonateToPlayer()
        {
            Console.Clear();
            Console.CursorVisible = true;
            DrawSpecialHeader("ESPORT MANAGER", "[QUYÊN GÓP CHO NGƯỜI CHƠI]");

            try
            {
                Console.SetCursorPosition(0, Console.CursorTop + 3);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(CenterText("=== DANH SÁCH NGƯỜI CHƠI ==="));
                Console.WriteLine();

                string[] players = {
                    "1. Simple (CS:GO) - ID: player001",
                    "2. Faker (LoL) - ID: player002",
                    "3. TenZ (VALORANT) - ID: player003",
                    "4. Shroud (PUBG) - ID: player004",
                    "5. Quay lại"
                };

                foreach (string player in players)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(CenterText(player));
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n" + CenterText("Nhập số thứ tự người chơi bạn muốn quyên góp: "));
                Console.ForegroundColor = ConsoleColor.White;

                string input = Console.ReadLine();

                if (input == "5")
                {
                    return;
                }

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 4)
                {
                    string selectedPlayer = players[choice - 1].Split('-')[0].Trim();

                    Console.Clear();
                    DrawSpecialHeader("ESPORT MANAGER", "[QUYÊN GÓP CHO NGƯỜI CHƠI]");

                    Console.SetCursorPosition(0, Console.CursorTop + 3);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(CenterText($"Quyên góp cho: {selectedPlayer}"));
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(CenterText("Số dư ví của bạn: 10,000,000 VNĐ"));
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(CenterText("Nhập số tiền bạn muốn quyên góp (VNĐ): "));
                    Console.ForegroundColor = ConsoleColor.White;

                    string amountInput = Console.ReadLine();

                    if (decimal.TryParse(amountInput, out decimal amount) && amount > 0)
                    {
                        if (amount <= 10000000) // Kiểm tra số dư
                        {
                            ShowSuccessMessage($"Đã quyên góp thành công {amount:N0} VNĐ cho {selectedPlayer}");
                        }
                        else
                        {
                            ShowErrorMessage("Số dư không đủ để thực hiện giao dịch này!");
                        }
                    }
                    else
                    {
                        ShowErrorMessage("Số tiền không hợp lệ. Vui lòng nhập lại!");
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

        private void WalletManagement()
        {
            Console.Clear();
            Console.CursorVisible = false;
            DrawSpecialHeader("ESPORT MANAGER", "[QUẢN LÝ VÍ ĐIỆN TỬ]");

            try
            {
                Console.SetCursorPosition(0, Console.CursorTop + 3);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(CenterText("=== THÔNG TIN VÍ ĐIỆN TỬ ==="));
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(CenterText("ID Ví: VIEWER-00123"));
                Console.WriteLine(CenterText("Số dư hiện tại: 10,000,000 VNĐ"));
                Console.WriteLine(CenterText("Số giao dịch: 5"));
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(CenterText("=== LỊCH SỬ GIAO DỊCH GẦN ĐÂY ==="));
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(CenterText("- 1,000,000 VNĐ - Quyên góp cho: Simple - 12/10/2023"));
                Console.WriteLine(CenterText("- 500,000 VNĐ - Quyên góp cho: Faker - 10/10/2023"));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(CenterText("+ 5,000,000 VNĐ - Nạp tiền - 05/10/2023"));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(CenterText("- 500,000 VNĐ - Quyên góp cho: TenZ - 01/10/2023"));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(CenterText("+ 7,000,000 VNĐ - Nạp tiền - 25/09/2023"));
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

        private void UpdatePersonalProfile()
        {
            Console.Clear();
            Console.CursorVisible = true;
            DrawSpecialHeader("ESPORT MANAGER", "[CẬP NHẬT THÔNG TIN CÁ NHÂN]");

            try
            {
                // Hiển thị thông tin hiện tại
                Console.SetCursorPosition(0, Console.CursorTop + 3);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(CenterText("=== THÔNG TIN HIỆN TẠI ==="));
                Console.WriteLine();

                User currentUser = new User
                {
                    DisplayName = "Người xem",
                    Email = "viewer@example.com",
                    Phone = "0987654323"
                };

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(CenterText($"Tên hiển thị: {currentUser.DisplayName}"));
                Console.WriteLine(CenterText($"Email: {currentUser.Email}"));
                Console.WriteLine(CenterText($"Số điện thoại: {currentUser.Phone}"));
                Console.WriteLine();

                // Form cập nhật
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(CenterText("=== CẬP NHẬT THÔNG TIN ==="));
                Console.WriteLine(CenterText("(Để trống nếu không thay đổi)"));
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(CenterText("Tên hiển thị mới: "));
                string newDisplayName = Console.ReadLine();

                Console.Write(CenterText("Email mới: "));
                string newEmail = Console.ReadLine();

                Console.Write(CenterText("Số điện thoại mới: "));
                string newPhone = Console.ReadLine();

                // Xác nhận thay đổi
                if (!string.IsNullOrWhiteSpace(newDisplayName) ||
                    !string.IsNullOrWhiteSpace(newEmail) ||
                    !string.IsNullOrWhiteSpace(newPhone))
                {
                    // Cập nhật thông tin người dùng với dữ liệu mới
                    if (!string.IsNullOrWhiteSpace(newDisplayName))
                        currentUser.DisplayName = newDisplayName;

                    if (!string.IsNullOrWhiteSpace(newEmail))
                        currentUser.Email = newEmail;

                    if (!string.IsNullOrWhiteSpace(newPhone))
                        currentUser.Phone = newPhone;

                    // Hiển thị thông báo thành công
                    ShowSuccessMessage("Thông tin cá nhân đã được cập nhật thành công!");
                }
                else
                {
                    ShowInfoMessage("Không có thông tin nào được thay đổi.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Lỗi: " + ex.Message);
            }
        }

        private void RequestRoleChange()
        {
            Console.Clear();
            Console.CursorVisible = true;
            DrawSpecialHeader("ESPORT MANAGER", "[YÊU CẦU THAY ĐỔI VAI TRÒ]");

            try
            {
                Console.SetCursorPosition(0, Console.CursorTop + 3);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(CenterText("=== YÊU CẦU THAY ĐỔI VAI TRÒ ==="));
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(CenterText("Vai trò hiện tại: Viewer"));
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(CenterText("Chọn vai trò mới bạn muốn đăng ký:"));
                Console.WriteLine();

                string[] roles = {
                    "1. Player",
                    "2. Admin",
                    "3. Hủy"
                };

                foreach (string role in roles)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(CenterText(role));
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n" + CenterText("Nhập lựa chọn của bạn: "));
                Console.ForegroundColor = ConsoleColor.White;

                string input = Console.ReadLine();

                if (input == "3")
                {
                    return;
                }

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 2)
                {
                    string newRole = choice == 1 ? "Player" : "Admin";

                    Console.Clear();
                    DrawSpecialHeader("ESPORT MANAGER", "[YÊU CẦU THAY ĐỔI VAI TRÒ]");

                    Console.SetCursorPosition(0, Console.CursorTop + 3);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(CenterText($"Lý do đăng ký vai trò {newRole}:"));
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.White;
                    string reason = Console.ReadLine();

                    if (!string.IsNullOrWhiteSpace(reason))
                    {
                        ShowSuccessMessage($"Đã gửi yêu cầu thay đổi vai trò thành {newRole}.\nYêu cầu của bạn sẽ được xem xét bởi quản trị viên.");
                    }
                    else
                    {
                        ShowErrorMessage("Vui lòng nhập lý do để yêu cầu được xem xét!");
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

        private void ForgotPassword()
        {
            Console.Clear();
            Console.CursorVisible = true;
            DrawSpecialHeader("ESPORT MANAGER", "[QUÊN MẬT KHẨU]");

            try
            {
                Console.SetCursorPosition(0, Console.CursorTop + 3);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(CenterText("=== KHÔI PHỤC MẬT KHẨU ==="));
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(CenterText("Nhập tên đăng nhập của bạn: "));
                string username = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(username))
                {
                    ShowErrorMessage("Tên đăng nhập không được để trống!");
                    return;
                }

                // Lấy câu hỏi bảo mật (giả định)
                string securityQuestion = "Tên trường học đầu tiên?";

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n" + CenterText($"Câu hỏi bảo mật: {securityQuestion}"));
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(CenterText("Nhập câu trả lời: "));
                string securityAnswer = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(securityAnswer))
                {
                    ShowErrorMessage("Câu trả lời không được để trống!");
                    return;
                }

                // Kiểm tra câu trả lời (giả định đúng)
                string newPassword = "viewer123";

                ShowSuccessMessage($"Mật khẩu của bạn đã được đặt lại thành: {newPassword}\nVui lòng đăng nhập lại và đổi mật khẩu ngay!");
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