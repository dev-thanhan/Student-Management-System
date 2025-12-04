# Student-Management-System
# Ứng dụng quản lý sinh viên, gồm:
    Presentation Layer (GUI - Lớp Giao diện:Windows Forms):là lớp trên cùng, nơi người dùng tương tác trực tiếp với phần mềm.
    Business Logic Layer (BLL - Lớp Nghiệp vụ):là lớp trung gian, đóng vai trò là "bộ não" của ứng dụng.
    Data Access Layer (DAL - Lớp Truy cập Dữ liệu):là lớp thấp nhất, chịu trách nhiệm làm việc trực tiếp với cơ sở dữ liệu (Database).
    Thành phần bổ trợ: Data Transfer Object (DTO):mang dữ liệu đi giữa các lớp
# I. Setup dự án
## 1. Clone repo
## 2. Cài đặt dependencies
## 3. Setup Docker:
### 3.1 Tải Docker(Chỉ tải cấm mở!): https://docs.docker.com/desktop/setup/install/windows-install/
### 3.2 Tạo tài khoản Docker(có thể để sau): https://www.docker.com/
### 3.3 Kiểm tra ảo hóa trên máy:
    Ấn nút windows, gõ "Turn Windows features on or off", 
    tìm đến "Windows Subsystem for Linux" và "Virtual Machine Platform", tích chọn.
    Sau đó ấn OK và khởi động lại máy.
### 3.4. Kiểm tra và neo phiên bản wsl:
    Vào cmd, powershell hoặc terminal, gõ lệnh: wsl --status 
    Nếu hiện ra Default Version: 2 thì không cần làm gì thêm.
    Nếu hiện ra Default Version: 1 thì gõ lệnh sau: wsl --set-default-version 2

### 3.5. Cài đặt docker:
    Vào cái thư mục lưu cái file tải về, ấn chuột phải vào file docker-desktop-installer.exe và chọn "Run as administrator".
    Sau đó ấn "OK" và "Install" để cài đặt.
    Sau khi cài xong, ấn "Close" và khởi động lại máy.

### 3.6. Kiểm tra môi trường ảo:
    Tổ hợp phím Ctrl + Shift + Esc để mở Task Manager.
    Performance > CPU: Thấy "Virtualization: Enabled" là được.

### 3.7. Setup .env:
    Trong thư mục database, tạo file .env và copy nội dung từ file .env.example vào.
    Sau đó chỉnh sửa các thông số trong file .env theo chú thích.

### 3.8. Chạy docker:
    Dưới terminal, gõ lệnh sau:
```bash
    cd database # Nếu đang ở Student-Management-System
```
```bash
    docker compose up -d
```

### 3.9. Kiểm tra docker:
    Dưới terminal, gõ lệnh sau: (nếu có đổi username và password trong .env thì đổi tương úng ở đây)
```bash
    docker exec -it studentmanagement-mysql mysql -u root -p123456 -e "USE StudentManagement; SHOW TABLES;"
```
    Nếu bạn thấy danh sách bảng hiện ra là THÀNH CÔNG