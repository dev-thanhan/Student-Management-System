-- ----------------------------------------------------------------------------
-- File: StudentManagement.sql
-- Hệ quản trị CSDL: MySQL
-- Mô tả: Script tạo database và các bảng cho dự án Quản lý sinh viên (C#)
-- ----------------------------------------------------------------------------

-- 1. TẠO DATABASE
-- Sử dụng utf8mb4 để hỗ trợ đầy đủ tiếng Việt có dấu
CREATE DATABASE IF NOT EXISTS StudentManagement CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE StudentManagement;

-- 2. XÓA BẢNG CŨ (NẾU TỒN TẠI)
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

-- 4. KẾT THÚC
-- Thông báo chạy xong (Comment)
-- Database StudentManagement đã sẵn sàng để sử dụng với MySQL.