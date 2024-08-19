Use master
Drop Database Qlbannhac
Create Database QLbannhac
Go
Use QLbannhac
Go
--Drop table KhachHang
--Go
Create Table	KhachHang
(
	MaKH	Int	Identity(1,1),
	HoTen	Nvarchar(50)	Not Null,
	TaiKhoan	Varchar(50)	Unique,
	MatKhau	Varchar(50)	Not Null,
	Email	Varchar(100)	Unique,
	DiachiKH	Nvarchar(200),
	DienThoaiKH	Varchar(50),
	NgaySinh	DateTime,
	Constraint	Pk_KhachHang	Primary Key(MaKH)
)
Go
--Drop table Dangnhac
--Go
Create Table	Dangnhac
(
	MaDangnhac	Int	Identity(1,1),
	TenDangnhac	Nvarchar(50)	Not Null,
	Constraint Pk_Dangnhac Primary Key(MaDangnhac)
)
Go
--Drop table Nhac
--Go
Create Table	Nhac
(
	MaNhac	Int	Identity(1,1),
	TenNhac	varchar(100)	Not Null,
	TenCasi varchar(100)   Not Null,
	GiaBan	Decimal(18,0)	Check(GiaBan>=0),
	AnhBia	Varchar(50),
	NgayCapNhat	DateTime,
	SoLuongTon	Int,
	MaDangnhac	Int,
	Constraint	Pk_Nhac	Primary Key(MaNhac),
	Constraint	Fk_Dangnhac	Foreign	Key(MaDangnhac) References	Dangnhac(MaDangnhac),
)
Go
--Drop table DonDatHang
--Go
Create Table	DonDatHang
(
	SoDH	Int Identity(1,1),
	MaKH	Int,
	NgayDH	DateTime,
	NgayGiao	DateTime,
	DaThanhToan	Bit,
	TinhTrangGiaoHang	Bit,
	Constraint	Pk_DonDatHang	Primary Key(SoDH),
	Constraint	Fk_KhachHang	Foreign	Key(MaKH)	References	KhachHang(MaKH)
)
Go
--Drop table ChiTietDatHang
--Go
Create Table	ChiTietDatHang
(
	SoDH	Int,
	MaNhac	Int,
	SoLuong	Int	Check(SoLuong>0),
	DonGia Decimal(18,0)	Check(DonGia>=0),
	Constraint	Pk_ChiTietDatHang	Primary Key(SoDH,MaNhac),
	Constraint	Fk_DonHang	Foreign	Key(SoDH)	References	DonDatHang(SoDH),
	Constraint	Fk_Nhac	Foreign Key(MaNhac)	References	Nhac(MaNhac)
)
Go
--Thêm dữ liệu:
---Dạng nhạc
	Insert into Dangnhac Values (N' Băng Cát-xét')
	Insert into Dangnhac Values (N' Đĩa Than')
	Insert into Dangnhac values (N' CD&DVD')
	Insert into Dangnhac values (N' Ấn Bản')
select *from Dangnhac
---Nhạc
	Insert into Nhac values (N' LINK - THE 4TH STUDIO ALBUM (BOXSET)','HOÀNG THUỲ LINH',500000,'product1.png','01/04/2022',15,2)
	Insert into Nhac values (N' THE DREAMING ALBUM (BLACK VINYL)','KATE BUSH',520000,'product2.jfif','01/04/2022',17,2)
	Insert into Nhac values (N' GREATEST HITS ALBUM','AIR SUPPLY',320000,'product3.jpg','01/04/2022',6,1)
	Insert into Nhac values (N' HAPPIER THAN EVER(LIMITED)','BILLIE EILISH',390000,'product4.png','01/04/2022',8,3)
	Insert into Nhac values (N' ONCE TWICE MELODY (DOUBLE CASSETTE)','BEACH HOUSE',480000,'product5.png','01/04/2022',8,1)
	Insert into Nhac values (N' MIDNIGHT (PREORDER)','TAYLOR SWIFT',500000,'product6.png','01/04/2022',5,1)
	Insert into Nhac values (N' MIDNIGHT (PREORDER)','TAYLOR SWIFT',400000,'product6.png','01/04/2022',5,3)
	Insert into Nhac values (N' MIDNIGHTS (MOONSTONE BLUE EDITION VINYL)','TAYLOR SWIFT',980000,'product24.png','01/04/2022',4,2)
	Insert into Nhac values (N' TELL ME YOU LOVE ME (STANDARD)','DEMI LOVATO',240000,'product7.png','01/04/2022',13,3)
	Insert into Nhac values (N' DAWN FM (VINYL 2LP)','THE WEEKND',1050000,'product8.png','01/04/2022',8,2)
	Insert into Nhac values (N' NGÀY ẤY VÀ SAU NÀY (∨∧) (CASSETTE TAPE)','CÁ HỒI HOANG',350000,'product9.png','01/04/2022',23,1)
	Insert into Nhac values (N' CHEMTRAILS OVER THE COUNTRY CLUB (TARGET EXCLUSIVE)','LANA DEL REY',400000,'product10.png','01/04/2022',35,3)
	Insert into Nhac values (N' ALL 4 NOTHING','LAUV',430000,'product11.png','01/04/2022',2,3)
	Insert into Nhac values (N' AN EVENING WITH SILK SONIC','SILK SONIC',380000,'product12.png','01/04/2022',13,3)
	Insert into Nhac values (N' IF I CANT HAVE LOVE, I WANT POWER','HALSEY',400000,'product13.png','01/04/2022',4,3)
	Insert into Nhac values (N' ĐÁNH ĐỐ (STANDARD VER.)','HOÀNG THUỲ LINH',118000,'product14.jpg','01/04/2022',7,3)
	Insert into Nhac values (N' HUMANITY (TIMES EXCLUSIVE)','LÊ KHOA',250000,'product15.png','01/04/2022',8,3)
	Insert into Nhac values (N' LOVE FOR SALE','TONY BENNETT & LADY GAGA',390000,'product16.png','01/04/2022',9,3)
	Insert into Nhac values (N' WITNESS (STANDARD)','KATY PERRY',430000,'product17.png','01/04/2022',23,3)
	Insert into Nhac values (N' PRAY FOR THE WICKED (VINYL LP)','PANIC AT THE DISCO',640000,'product18.png','01/04/2022',34,2)
	Insert into Nhac values (N' PURE HEROINE','LORDE',312000,'product19.png','01/04/2022',21,3)
	Insert into Nhac values (N' HARRYS HOUSE','HARRY STYLES',450000,'product20.png','01/04/2022',2,1)
	Insert into Nhac values (N' HOUNDS OF LOVE (LIMITED EDITION)','KATE BUSH',2000000,'product21.jpg','01/04/2022',4,1)
	Insert into Nhac values (N' LÊ CÁT TRỌNG LÝ (DELUXE EDITION - PURPLE TINT)','LÊ CÁT TRỌNG LÝ',300000,'product22.png','01/04/2022',5,1)
	Insert into Nhac values (N' HAPPIER THAN EVER','BILLIE EILISH',390000,'product23.png','01/04/2022',1,1)
	Insert into Nhac values (N' ME! (LIMITED CD SINGLE)','TAYLOR SWIFT',800000,'product25.png','01/04/2022',23,3)
	Insert into Nhac values (N' FOLKLORE #1 THE "IN THE TREES" EDITION DELUXE VINYL','TAYLOR SWIFT',850000,'product26.png','01/04/2022',4,2)
	Insert into Nhac values (N' SCREEN VIOLENCE (INDIE-RETAIL EXCLUSIVE) (RED/CLEAR VINYL LP)','CHVRCHES',850000,'product27.png','01/04/2022',6,2)
	Insert into Nhac values (N' THE WHOLE STORY','KATE BUSH',450000,'product28.jpg','01/04/2022',8,1)
	Insert into Nhac values (N' LOST IN LOVE','AIR SUPPLY',850000,'product29.jpg','01/04/2022',5,1)
	Insert into Nhac values (N' STRANGER THINGS: SOUNDTRACK FORM THE NETFLIX SERIES, SEASON 4','NETFLIX',850000,'product30.jpg','01/04/2022',9,1)
select *from Nhac
--Dữ liệu cập nhật: Tài khoản quản trị
Create table Admin
(
	UserAdmin varchar(30) primary key,
	PassAdmin varchar(30) not null,
	Hoten nVarchar(50)
)
Insert into Admin values('admin','220306','TongNguyenMinhTriet')
Insert into Admin values('user','654321','Mr Phi')
select *from Admin