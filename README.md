# EsportManager

EsportManager là ứng dụng console quản lý giải đấu Esports được phát triển bằng C#.

## Giới thiệu

Ứng dụng hỗ trợ quản lý giải đấu Esports với các chức năng:

- Quản lý người dùng (đăng ký, đăng nhập)
- Quản lý giải đấu và trận đấu
- Phân quyền theo 3 vai trò: Admin, Player và Viewer

## Kiến trúc

Dự án được xây dựng theo kiến trúc 3 lớp:

- **UI (User Interface)**: Giao diện người dùng
- **BLL (Business Logic Layer)**: Xử lý logic nghiệp vụ
- **DAL (Data Access Layer)**: Truy xuất dữ liệu

Tuân thủ các nguyên tắc SOLID để đảm bảo mã nguồn dễ bảo trì và mở rộng.

## Cấu trúc thư mục

```
EsportManager/
├── Models/         # Các lớp đối tượng dữ liệu
├── DAL/            # Tầng truy xuất dữ liệu
├── BLL/            # Tầng xử lý logic
├── UI/             # Tầng giao diện người dùng
├── Utils/          # Các tiện ích
└── Program.cs      # Điểm khởi chạy ứng dụng
```

## Chức năng

### Admin

- Quản lý người dùng
- Quản lý giải đấu
- Quản lý trận đấu

### Player

- Tham gia giải đấu
- Xem lịch thi đấu
- Cập nhật thông tin cá nhân

### Viewer

- Xem thông tin giải đấu
- Xem lịch thi đấu
- Xem kết quả trận đấu

## Cách cài đặt và sử dụng

1. Cài đặt .NET SDK
2. Clone repository này
3. Di chuyển vào thư mục dự án
4. Chạy lệnh `dotnet build`
5. Chạy lệnh `dotnet run`

## Công nghệ sử dụng

- C# .NET
- Console Application
