-- ----------------------------------------------------------------------------
-- File: StudentManagement.sql
-- Hệ quản trị CSDL: MySQL
-- Mô tả: Script tạo database chuẩn UTF-8 mb4 và dữ liệu giả lập phong phú
-- ----------------------------------------------------------------------------

-- ============================================================================
-- PHẦN 1: KHỞI TẠO DATABASE VÀ BẢNG (SCHEMA)
-- ============================================================================

-- 1. TẠO DATABASE
CREATE DATABASE IF NOT EXISTS StudentManagement CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE StudentManagement;

-- 2. XÓA BẢNG CŨ (NẾU TỒN TẠI)
SET FOREIGN_KEY_CHECKS = 0; -- Tắt kiểm tra khóa ngoại tạm thời để xóa cho nhanh
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
SET FOREIGN_KEY_CHECKS = 1;

-- 3. TẠO CÁC BẢNG DỮ LIỆU

-- Bảng Khoa
CREATE TABLE Khoa (
    MaKhoa     CHAR(10) PRIMARY KEY,
    TenKhoa    VARCHAR(100) NOT NULL,
    DienThoai  VARCHAR(20),
    Email      VARCHAR(100)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Bảng Ngành
CREATE TABLE Nganh (
    MaNganh  CHAR(10) PRIMARY KEY,
    TenNganh VARCHAR(100) NOT NULL,
    MaKhoa   CHAR(10),
    CONSTRAINT FK_Nganh_Khoa FOREIGN KEY (MaKhoa) REFERENCES Khoa(MaKhoa)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Bảng Lớp
CREATE TABLE Lop (
    MaLop       CHAR(10) PRIMARY KEY,
    TenLop      VARCHAR(100) NOT NULL,
    KhoaHoc     VARCHAR(20),
    MaNganh     CHAR(10),
    CoVanHocTap VARCHAR(100),
    CONSTRAINT FK_Lop_Nganh FOREIGN KEY (MaNganh) REFERENCES Nganh(MaNganh)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Bảng Sinh Viên
CREATE TABLE SinhVien (
    MaSV        CHAR(15) PRIMARY KEY,
    HoTen       VARCHAR(100) NOT NULL,
    NgaySinh    DATE,
    GioiTinh    BIT DEFAULT 1,             -- 1: Nam, 0: Nữ
    DiaChi      VARCHAR(200),
    SoDienThoai VARCHAR(20),
    Email       VARCHAR(100),
    MaLop       CHAR(10),
    TrangThai   TINYINT DEFAULT 1,         -- 1: Đang học, 2: Bảo lưu, 3: Tốt nghiệp, 0: Nghỉ
    CONSTRAINT FK_SinhVien_Lop FOREIGN KEY (MaLop) REFERENCES Lop(MaLop)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Bảng Môn Học
CREATE TABLE MonHoc (
    MaMon        CHAR(10) PRIMARY KEY,
    TenMon       VARCHAR(100) NOT NULL,
    SoTinChi     INT DEFAULT 0,
    MonTienQuyet CHAR(10),
    CONSTRAINT FK_MonHoc_MonTienQuyet FOREIGN KEY (MonTienQuyet) REFERENCES MonHoc(MaMon)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Bảng Học Kỳ
CREATE TABLE HocKy (
    MaHocKy   CHAR(10) PRIMARY KEY,
    TenHocKy  VARCHAR(50),
    NamHoc    VARCHAR(20)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Bảng Lớp Học Phần
CREATE TABLE LopHocPhan (
    MaLopHP   CHAR(15) PRIMARY KEY,
    MaMon     CHAR(10) NOT NULL,
    MaHocKy   CHAR(10) NOT NULL,
    SiSoToiDa INT DEFAULT 40,
    GiangVien VARCHAR(100),
    Thu       TINYINT,        
    TietBD    TINYINT,        
    SoTiet    TINYINT,        
    PhongHoc  VARCHAR(50),
    CONSTRAINT FK_LHP_MonHoc FOREIGN KEY (MaMon) REFERENCES MonHoc(MaMon),
    CONSTRAINT FK_LHP_HocKy FOREIGN KEY (MaHocKy) REFERENCES HocKy(MaHocKy)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Bảng Đăng Ký Học Phần
CREATE TABLE DangKyHocPhan (
    MaSV       CHAR(15),
    MaLopHP    CHAR(15),
    NgayDangKy DATETIME DEFAULT CURRENT_TIMESTAMP,
    TrangThai  TINYINT DEFAULT 1,
    PRIMARY KEY (MaSV, MaLopHP),
    CONSTRAINT FK_DKHP_SinhVien FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV),
    CONSTRAINT FK_DKHP_LopHocPhan FOREIGN KEY (MaLopHP) REFERENCES LopHocPhan(MaLopHP)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Bảng Rèn Luyện
CREATE TABLE BangRenLuyen (
    IdRenLuyen  INT PRIMARY KEY AUTO_INCREMENT,
    MaSV        CHAR(15),
    MaHocKy     CHAR(10),
    DiemSo      TINYINT,
    XepLoai     VARCHAR(20),
    NhanXet     VARCHAR(255),
    NgayDanhGia DATE,
    CONSTRAINT FK_RenLuyen_SinhVien FOREIGN KEY (MaSV) REFERENCES SinhVien(MaSV),
    CONSTRAINT FK_RenLuyen_HocKy FOREIGN KEY (MaHocKy) REFERENCES HocKy(MaHocKy)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Tạo bảng Tài khoản
CREATE TABLE TaiKhoan (
    TenDangNhap VARCHAR(50) PRIMARY KEY,
    MatKhau     VARCHAR(255) NOT NULL, -- Lưu mật khẩu đã mã hóa
    HoTen       VARCHAR(100),
    Email       VARCHAR(100),
    Quyen       INT DEFAULT 1, -- 1: Admin, 2: User thường
    TrangThai   BIT DEFAULT 1  -- 1: Hoạt động, 0: Khóa
);

-- ============================================================================
-- PHẦN 2: CHÈN DỮ LIỆU MẪU (SEED DATA)
-- ============================================================================

-- 4. KHOA
INSERT INTO Khoa (MaKhoa, TenKhoa, DienThoai, Email) VALUES
('FIT', 'Công nghệ thông tin', '02838940001', 'fit@university.edu.vn'),
('BA',  'Quản trị kinh doanh', '02838940002', 'ba@university.edu.vn'),
('ENG', 'Ngôn ngữ Anh',        '02838940003', 'eng@university.edu.vn'),
('LAW', 'Luật kinh tế',        '02838940004', 'law@university.edu.vn');

-- 5. NGÀNH
INSERT INTO Nganh (MaNganh, TenNganh, MaKhoa) VALUES
('SE',  'Kỹ thuật phần mềm',   'FIT'),
('IS',  'Hệ thống thông tin',  'FIT'),
('AI',  'Trí tuệ nhân tạo',    'FIT'),
('IB',  'Kinh doanh quốc tế',  'BA'),
('MKT', 'Marketing Digital',   'BA'),
('EL',  'Tiếng Anh thương mại','ENG'),
('LW',  'Luật dân sự',         'LAW');

-- 6. LỚP
INSERT INTO Lop (MaLop, TenLop, KhoaHoc, MaNganh, CoVanHocTap) VALUES
('21SE01', 'KTPM K21 - Lớp 1', 'K21', 'SE', 'Nguyễn Văn A'),
('21SE02', 'KTPM K21 - Lớp 2', 'K21', 'SE', 'Trần Thị B'),
('22IS01', 'HTTT K22 - Lớp 1', 'K22', 'IS', 'Lê Văn C'),
('22AI01', 'AI K22 - Lớp Chất lượng cao', 'K22', 'AI', 'Phạm Minh H'),
('23IB01', 'KDQT K23 - Lớp 1', 'K23', 'IB', 'Phạm Thị D'),
('23MKT1', 'MKT K23 - Lớp Sáng tạo', 'K23', 'MKT', 'Hoàng Tùng');

-- 7. SINH VIÊN (15 bạn)
INSERT INTO SinhVien (MaSV, HoTen, NgaySinh, GioiTinh, DiaChi, SoDienThoai, Email, MaLop, TrangThai) VALUES
-- Lớp SE01
('SV001', 'Nguyễn Thành Nam', '2003-05-15', 1, '123 Lê Lợi, Q1, TP.HCM', '0901234567', 'nam.nt@st.edu.vn', '21SE01', 1),
('SV002', 'Trần Thị Mỹ Linh', '2003-08-20', 0, '456 Nguyễn Huệ, Q1, TP.HCM', '0902345678', 'linh.ttm@st.edu.vn', '21SE01', 1),
('SV003', 'Phạm Văn Hùng',    '2003-02-10', 1, '12 Nguyễn Trãi, Q5, TP.HCM', '0912333444', 'hung.pv@st.edu.vn', '21SE01', 1),
-- Lớp SE02
('SV004', 'Lê Minh Khôi',     '2003-12-10', 1, '789 Võ Văn Ngân, Thủ Đức', '0903456789', 'khoi.lm@st.edu.vn', '21SE02', 1),
('SV005', 'Đỗ Thị Lan Anh',   '2003-11-05', 0, '55 Quang Trung, Gò Vấp', '0988777666', 'anh.dtl@st.edu.vn', '21SE02', 2), -- Bảo lưu
-- Lớp IS01
('SV006', 'Phạm Hồng Ngọc',   '2004-02-14', 0, '12 Đường số 10, Q7', '0904567890', 'ngoc.ph@st.edu.vn', '22IS01', 1),
('SV007', 'Vũ Đức Đam',       '2004-09-02', 1, '88 Lý Thường Kiệt, Q10', '0977111222', 'dam.vd@st.edu.vn', '22IS01', 1),
-- Lớp AI01
('SV008', 'Ngô Bảo Châu',     '2004-01-01', 1, 'Landmark 81, Bình Thạnh', '0999888777', 'chau.nb@st.edu.vn', '22AI01', 1),
('SV009', 'Lý Nhã Kỳ',        '2004-07-19', 0, 'Thảo Điền, Q2', '0966555444', 'ky.ln@st.edu.vn', '22AI01', 1),
-- Lớp IB01
('SV010', 'Hoàng Văn Thái',   '2005-06-01', 1, '34 Hùng Vương, Q5', '0905678901', 'thai.hv@st.edu.vn', '23IB01', 1),
('SV011', 'Trương Quỳnh Anh', '2005-03-08', 0, '101 Pasteur, Q3', '0933222111', 'anh.tq@st.edu.vn', '23IB01', 1),
-- Lớp MKT1
('SV012', 'Sơn Tùng MTP',     '2005-07-05', 1, 'Thái Bình, Việt Nam', '0944555666', 'tung.st@st.edu.vn', '23MKT1', 1),
('SV013', 'Chi Pu',           '2005-10-10', 0, 'Hà Nội, Việt Nam', '0955666777', 'pu.c@st.edu.vn', '23MKT1', 1),
-- Sinh viên đã tốt nghiệp hoặc nghỉ
('SV014', 'Nguyễn Văn Cựu',   '2000-01-01', 1, 'Đã ra trường', '0900000000', 'cuu.nv@st.edu.vn', '21SE01', 3), -- Tốt nghiệp
('SV015', 'Trần Văn Nghỉ',    '2003-01-01', 1, 'Không rõ', '0000000000', 'nghi.tv@st.edu.vn', '21SE02', 0); -- Nghỉ học

-- 8. MÔN HỌC
INSERT INTO MonHoc (MaMon, TenMon, SoTinChi, MonTienQuyet) VALUES
('CS101', 'Nhập môn lập trình', 3, NULL),
('BA101', 'Kinh tế vi mô',      3, NULL),
('EN101', 'Tiếng Anh 1',        4, NULL),
('MATH1', 'Đại số tuyến tính',  3, NULL),
('AI101', 'Nhập môn AI',        3, NULL);

INSERT INTO MonHoc (MaMon, TenMon, SoTinChi, MonTienQuyet) VALUES
('CS102', 'Lập trình hướng đối tượng', 4, 'CS101'),
('CS201', 'Cấu trúc dữ liệu & GT',     3, 'CS101'),
('CS301', 'Cơ sở dữ liệu',             4, 'CS201');

-- 9. HỌC KỲ
INSERT INTO HocKy (MaHocKy, TenHocKy, NamHoc) VALUES
('HK1_2324', 'Học kỳ 1', '2023-2024'),
('HK2_2324', 'Học kỳ 2', '2023-2024'),
('HK1_2425', 'Học kỳ 1', '2024-2025');

-- 10. LỚP HỌC PHẦN
INSERT INTO LopHocPhan (MaLopHP, MaMon, MaHocKy, SiSoToiDa, GiangVien, Thu, TietBD, SoTiet, PhongHoc) VALUES
('LHP001', 'CS101', 'HK1_2324', 40, 'ThS. Nguyễn Văn A', 2, 1, 3, 'C301'),
('LHP002', 'CS101', 'HK1_2324', 40, 'TS. Trần Văn B',    4, 7, 3, 'C302'),
('LHP003', 'CS102', 'HK2_2324', 35, 'ThS. Nguyễn Văn A', 3, 1, 4, 'LAB01'),
('LHP004', 'BA101', 'HK1_2324', 60, 'TS. Lê Thị C',      5, 1, 3, 'B101'),
('LHP005', 'AI101', 'HK1_2425', 30, 'TS. Andrew Ng',     6, 1, 3, 'LAB_AI');

-- 11. ĐĂNG KÝ & ĐIỂM (Data mẫu)
-- Lớp LHP001
INSERT INTO Diem (MaSV, MaLopHP, DiemQT, DiemThi, DiemTongKet, KetQua) VALUES
('SV001', 'LHP001', 9.0, 8.5, 8.7, 'Đạt'),
('SV002', 'LHP001', 7.0, 4.0, 5.2, 'Đạt'),
('SV003', 'LHP001', 5.0, 3.0, 3.8, 'Rớt'),
('SV004', 'LHP001', 8.0, 8.0, 8.0, 'Đạt');

-- Lớp LHP002 (Cùng môn khác lớp)
INSERT INTO Diem (MaSV, MaLopHP, DiemQT, DiemThi, DiemTongKet, KetQua) VALUES
('SV006', 'LHP002', 10.0, 9.5, 9.7, 'Đạt'),
('SV007', 'LHP002', 6.0, 6.0, 6.0, 'Đạt');

-- Lớp LHP005 (AI)
INSERT INTO Diem (MaSV, MaLopHP, DiemQT, DiemThi, DiemTongKet, KetQua) VALUES
('SV008', 'LHP005', 9.5, 10.0, 9.8, 'Đạt'),
('SV009', 'LHP005', 8.5, 9.0, 8.8, 'Đạt');

-- 12. RÈN LUYỆN
INSERT INTO BangRenLuyen (MaSV, MaHocKy, DiemSo, XepLoai, NhanXet, NgayDanhGia) VALUES
('SV001', 'HK1_2324', 90, 'Xuất sắc', 'Tham gia tích cực phong trào Đoàn', '2024-01-15'),
('SV002', 'HK1_2324', 82, 'Giỏi',     'Chăm chỉ, đi học đầy đủ',           '2024-01-15'),
('SV003', 'HK1_2324', 65, 'Khá',      'Cần cố gắng hơn',                   '2024-01-15'),
('SV008', 'HK1_2324', 95, 'Xuất sắc', 'Đạt giải Olympic Tin học',          '2024-01-15');

-- 2 User
INSERT INTO TaiKhoan (TenDangNhap, MatKhau, HoTen, Email, Quyen) 
VALUES ('admin', '123456', 'Quản trị viên', 'admin@st.edu.vn', 1);

INSERT INTO TaiKhoan (TenDangNhap, MatKhau, HoTen, Email, Quyen) 
VALUES ('user', '123456', 'Nhân viên', 'user@st.edu.vn', 2);

SELECT 'Cài đặt Database thành công! Hãy khởi động lại ứng dụng.' AS Message;