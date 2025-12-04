-- ----------------------------------------------------------------------------
-- File: StudentManagement.sql
-- Hệ quản trị CSDL: MySQL
-- Mô tả: Script tạo database, bảng và dữ liệu mẫu cho dự án Quản lý sinh viên
-- ----------------------------------------------------------------------------

-- ============================================================================
-- PHẦN 1: KHỞI TẠO DATABASE VÀ BẢNG (SCHEMA)
-- ============================================================================

-- 1. TẠO DATABASE
-- Sử dụng utf8mb4 để hỗ trợ đầy đủ tiếng Việt có dấu
CREATE DATABASE IF NOT EXISTS StudentManagement CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE StudentManagement;

-- 2. XÓA BẢNG CŨ (NẾU TỒN TẠI) ĐỂ TRÁNH LỖI KHI CHẠY LẠI
-- Thứ tự xóa: Bảng con (có khóa ngoại) xóa trước, bảng cha xóa sau
DROP TABLE IF EXISTS BangRenLuyen;
DROP TABLE IF EXISTS Diem;
DROP TABLE IF EXISTS DangKyHocPhan;
DROP TABLE IF EXISTS LopHocPhan;
DROP TABLE IF EXISTS HocKy;
DROP TABLE IF EXISTS MonHoc;
DROP TABLE IF EXISTS SinhVien;
DROP TABLE IF EXISTS Lop;
DROP TABLE IF EXISTS Nganh;
DROP TABLE IF EXISTS Khoa;

-- 3. TẠO CÁC BẢNG DỮ LIỆU

-- Bảng Khoa
CREATE TABLE Khoa (
    MaKhoa     CHAR(10) PRIMARY KEY,
    TenKhoa    VARCHAR(100) NOT NULL,
    DienThoai  VARCHAR(20),
    Email      VARCHAR(100)
);

-- Bảng Ngành
CREATE TABLE Nganh (
    MaNganh  CHAR(10) PRIMARY KEY,
    TenNganh VARCHAR(100) NOT NULL,
    MaKhoa   CHAR(10),
    CONSTRAINT FK_Nganh_Khoa FOREIGN KEY (MaKhoa) REFERENCES Khoa(MaKhoa)
);

-- Bảng Lớp
CREATE TABLE Lop (
    MaLop       CHAR(10) PRIMARY KEY,
    TenLop      VARCHAR(100) NOT NULL,
    KhoaHoc     VARCHAR(20),
    MaNganh     CHAR(10),
    CoVanHocTap VARCHAR(100),
    CONSTRAINT FK_Lop_Nganh FOREIGN KEY (MaNganh) REFERENCES Nganh(MaNganh)
);

-- Bảng Sinh Viên
CREATE TABLE SinhVien (
    MaSV        CHAR(15) PRIMARY KEY,
    HoTen       VARCHAR(100) NOT NULL,
    NgaySinh    DATE,
    GioiTinh    BIT DEFAULT 1,             -- 1: Nam, 0: Nữ (Map với bool trong C#)
    DiaChi      VARCHAR(200),
    SoDienThoai VARCHAR(20),
    Email       VARCHAR(100),
    MaLop       CHAR(10),
    TrangThai   TINYINT DEFAULT 1,         -- Map với Enum StudentStatus trong C#
    CONSTRAINT FK_SinhVien_Lop FOREIGN KEY (MaLop) REFERENCES Lop(MaLop)
);

-- Bảng Môn Học
CREATE TABLE MonHoc (
    MaMon        CHAR(10) PRIMARY KEY,
    TenMon       VARCHAR(100) NOT NULL,
    SoTinChi     INT DEFAULT 0,
    MonTienQuyet CHAR(10),
    -- Khóa ngoại đệ quy: Môn tiên quyết phải là một môn đã có trong bảng này
    CONSTRAINT FK_MonHoc_MonTienQuyet FOREIGN KEY (MonTienQuyet) REFERENCES MonHoc(MaMon)
);

-- Bảng Học Kỳ
CREATE TABLE HocKy (
    MaHocKy   CHAR(10) PRIMARY KEY,
    TenHocKy  VARCHAR(50),
    NamHoc    VARCHAR(20)
);

-- Bảng Lớp Học Phần
CREATE TABLE LopHocPhan (
    MaLopHP   CHAR(15) PRIMARY KEY,
    MaMon     CHAR(10) NOT NULL,
    MaHocKy   CHAR(10) NOT NULL,
    SiSoToiDa INT DEFAULT 40,
    GiangVien VARCHAR(100),
    Thu       TINYINT,        -- Thứ trong tuần (2-8)
    TietBD    TINYINT,        -- Tiết bắt đầu
    SoTiet    TINYINT,        -- Số tiết học
    PhongHoc  VARCHAR(50),
    CONSTRAINT FK_LHP_MonHoc FOREIGN KEY (MaMon) REFERENCES MonHoc(MaMon),
    CONSTRAINT FK_LHP_HocKy FOREIGN KEY (MaHocKy) REFERENCES HocKy(MaHocKy)
);

-- Bảng Đăng Ký Học Phần
CREATE TABLE DangKyHocPhan (
    MaSV       CHAR(15),
    MaLopHP    CHAR(15),
    NgayDangKy DATETIME DEFAULT CURRENT_TIMESTAMP,
    TrangThai  TINYINT DEFAULT 1,
    PRIMARY KEY (MaSV, MaLopHP),
    CONSTRAINT FK_DKHP_SinhVien FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV),
    CONSTRAINT FK_DKHP_LopHocPhan FOREIGN KEY (MaLopHP) REFERENCES LopHocPhan(MaLopHP)
);

-- Bảng Điểm
CREATE TABLE Diem (
    MaSV        CHAR(15),
    MaLopHP     CHAR(15),
    DiemQT      DECIMAL(4,2),
    DiemThi     DECIMAL(4,2),
    DiemTongKet DECIMAL(4,2),
    KetQua      VARCHAR(10),
    PRIMARY KEY (MaSV, MaLopHP),
    CONSTRAINT FK_Diem_SinhVien FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV),
    CONSTRAINT FK_Diem_LopHocPhan FOREIGN KEY (MaLopHP) REFERENCES LopHocPhan(MaLopHP)
);

-- Bảng Rèn Luyện
CREATE TABLE BangRenLuyen (
    IdRenLuyen  INT PRIMARY KEY AUTO_INCREMENT, -- Tự động tăng ID
    MaSV        CHAR(15),
    MaHocKy     CHAR(10),
    DiemSo      TINYINT,
    XepLoai     VARCHAR(20),
    NhanXet     VARCHAR(255),
    NgayDanhGia DATE,
    CONSTRAINT FK_RenLuyen_SinhVien FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV),
    CONSTRAINT FK_RenLuyen_HocKy FOREIGN KEY (MaHocKy) REFERENCES HocKy(MaHocKy)
);

-- ============================================================================
-- PHẦN 2: CHÈN DỮ LIỆU MẪU (SEED DATA)
-- ============================================================================

-- 4. CHÈN DỮ LIỆU BẢNG KHOA
INSERT INTO Khoa (MaKhoa, TenKhoa, DienThoai, Email) VALUES
('FIT', 'Công nghệ thông tin', '02838940001', 'fit@university.edu.vn'),
('BA',  'Quản trị kinh doanh', '02838940002', 'ba@university.edu.vn'),
('ENG', 'Ngôn ngữ Anh',        '02838940003', 'eng@university.edu.vn');

-- 5. CHÈN DỮ LIỆU BẢNG NGÀNH
INSERT INTO Nganh (MaNganh, TenNganh, MaKhoa) VALUES
('SE',  'Kỹ thuật phần mềm',   'FIT'),
('IS',  'Hệ thống thông tin',  'FIT'),
('IB',  'Kinh doanh quốc tế',  'BA'),
('MKT', 'Marketing',           'BA'),
('EL',  'Tiếng Anh thương mại','ENG');

-- 6. CHÈN DỮ LIỆU BẢNG LỚP
INSERT INTO Lop (MaLop, TenLop, KhoaHoc, MaNganh, CoVanHocTap) VALUES
('21SE01', 'KTPM K21 - Lớp 1', 'K21', 'SE', 'Nguyễn Văn A'),
('21SE02', 'KTPM K21 - Lớp 2', 'K21', 'SE', 'Trần Thị B'),
('22IS01', 'HTTT K22 - Lớp 1', 'K22', 'IS', 'Lê Văn C'),
('22IB01', 'KDQT K22 - Lớp 1', 'K22', 'IB', 'Phạm Thị D');

-- 7. CHÈN DỮ LIỆU BẢNG SINH VIÊN
INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, MaLop, TrangThai) VALUES
('SV001', 'Nguyễn Thành Nam', '2003-05-15', 1, '123 Lê Lợi, TP.HCM', '0901234567', 'nam.nt@st.edu.vn', '21SE01', 1),
('SV002', 'Trần Thị Mỹ Linh', '2003-08-20', 0, '456 Nguyễn Huệ, TP.HCM', '0902345678', 'linh.ttm@st.edu.vn', '21SE01', 1),
('SV003', 'Lê Minh Khôi',     '2003-12-10', 1, '789 Võ Văn Ngân, Thủ Đức', '0903456789', 'khoi.lm@st.edu.vn', '21SE02', 1),
('SV004', 'Phạm Hồng Ngọc',   '2004-02-14', 0, '12 Đường số 10, Q7', '0904567890', 'ngoc.ph@st.edu.vn', '22IS01', 1),
('SV005', 'Hoàng Văn Thái',   '2004-06-01', 1, '34 Hùng Vương, Q5', '0905678901', 'thai.hv@st.edu.vn', '22IB01', 2);

-- 8. CHÈN DỮ LIỆU BẢNG MÔN HỌC
INSERT INTO MonHoc (MaMon, TenMon, SoTinChi, MonTienQuyet) VALUES
('CS101', 'Nhập môn lập trình', 3, NULL),
('BA101', 'Kinh tế vi mô',      3, NULL),
('EN101', 'Tiếng Anh 1',        4, NULL);

INSERT INTO MonHoc (MaMon, TenMon, SoTinChi, MonTienQuyet) VALUES
('CS102', 'Lập trình hướng đối tượng', 4, 'CS101'),
('CS201', 'Cấu trúc dữ liệu',          3, 'CS101');

-- 9. CHÈN DỮ LIỆU BẢNG HỌC KỲ
INSERT INTO HocKy (MaHocKy, TenHocKy, NamHoc) VALUES
('HK1_2324', 'Học kỳ 1', '2023-2024'),
('HK2_2324', 'Học kỳ 2', '2023-2024'),
('HK1_2425', 'Học kỳ 1', '2024-2025');

-- 10. CHÈN DỮ LIỆU BẢNG LỚP HỌC PHẦN
INSERT INTO LopHocPhan (MaLopHP, MaMon, MaHocKy, SiSoToiDa, GiangVien, Thu, TietBD, SoTiet, PhongHoc) VALUES
('LHP001', 'CS101', 'HK1_2324', 40, 'ThS. Nguyễn Văn A', 2, 1, 3, 'C301'),
('LHP002', 'CS101', 'HK1_2324', 40, 'TS. Trần Văn B',    4, 7, 3, 'C302'),
('LHP003', 'CS102', 'HK2_2324', 35, 'ThS. Nguyễn Văn A', 3, 1, 4, 'LAB01'),
('LHP004', 'BA101', 'HK1_2324', 60, 'TS. Lê Thị C',      5, 1, 3, 'B101');

-- 11. CHÈN DỮ LIỆU BẢNG ĐĂNG KÝ HỌC PHẦN
INSERT INTO DangKyHocPhan (MaSV, MaLopHP, NgayDangKy, TrangThai) VALUES
('SV001', 'LHP001', '2023-08-15 08:00:00', 2),
('SV002', 'LHP001', '2023-08-15 08:05:00', 2),
('SV003', 'LHP002', '2023-08-16 09:00:00', 2),
('SV001', 'LHP003', '2024-01-10 08:00:00', 1);

-- 12. CHÈN DỮ LIỆU BẢNG ĐIỂM
INSERT INTO Diem (MaSV, MaLopHP, DiemQT, DiemThi, DiemTongKet, KetQua) VALUES
('SV001', 'LHP001', 9.0, 8.5, 8.7, 'Đạt'),
('SV002', 'LHP001', 7.0, 4.0, 5.2, 'Đạt'),
('SV003', 'LHP002', 5.0, 3.0, 3.8, 'Rớt');

-- 13. CHÈN DỮ LIỆU BẢNG RÈN LUYỆN
INSERT INTO BangRenLuyen (MaSV, MaHocKy, DiemSo, XepLoai, NhanXet, NgayDanhGia) VALUES
('SV001', 'HK1_2324', 90, 'Xuất sắc', 'Tham gia tích cực phong trào', '2024-01-15'),
('SV002', 'HK1_2324', 82, 'Giỏi',     'Chăm chỉ, đi học đầy đủ',      '2024-01-15'),
('SV003', 'HK1_2324', 65, 'Khá',      'Cần cố gắng hơn',              '2024-01-15');

-- 14. KẾT THÚC
SELECT 'Database and data template installation successful!' AS Message;