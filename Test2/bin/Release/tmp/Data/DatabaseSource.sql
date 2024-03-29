USE [master]
GO
/****** Object:  Database [QuanLyCuaHangMyPham]    Script Date: 12/3/2019 8:46:25 AM ******/
CREATE DATABASE [QuanLyCuaHangMyPham]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuanLyCuaHangMyPham', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.TRNLONGJ\MSSQL\DATA\QuanLyCuaHangMyPham.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QuanLyCuaHangMyPham_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.TRNLONGJ\MSSQL\DATA\QuanLyCuaHangMyPham_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuanLyCuaHangMyPham].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET  MULTI_USER 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET QUERY_STORE = OFF
GO
USE [QuanLyCuaHangMyPham]
GO
/****** Object:  UserDefinedFunction [dbo].[fuConvertToUnsign1]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fuConvertToUnsign1] ( @strInput NVARCHAR(4000) ) 
RETURNS NVARCHAR(4000) 
AS 
BEGIN 
	IF @strInput IS NULL 

		RETURN @strInput 

	IF @strInput = '' 

		RETURN @strInput 
				
		DECLARE @RT NVARCHAR(4000) 

		DECLARE @SIGN_CHARS NCHAR(136) 
		
		DECLARE @UNSIGN_CHARS NCHAR (136) 
		
		SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệế ìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵý ĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍ ÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' +NCHAR(272)+ NCHAR(208) 
		
		SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeee iiiiiooooooooooooooouuuuuuuuuuyyyyy AADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIII OOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' 
		
		DECLARE @COUNTER int 
		
		DECLARE @COUNTER1 int SET @COUNTER = 1 
		
		WHILE (@COUNTER <=LEN(@strInput)) 
		
		BEGIN 
		
			SET @COUNTER1 = 1 
			
			WHILE (@COUNTER1 <=LEN(@SIGN_CHARS)+1) 
			
			BEGIN 
			
				IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@strInput,@COUNTER ,1) ) 
				BEGIN 
					IF @COUNTER=1 
						SET @strInput = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)-1) 
					ELSE 
						SET @strInput = SUBSTRING(@strInput, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@strInput, @COUNTER+1,LEN(@strInput)- @COUNTER) 
						BREAK 
				END 
				SET @COUNTER1 = @COUNTER1 +1 
			END SET @COUNTER = @COUNTER +1 
		END SET @strInput = replace(@strInput,' ','-') 
		RETURN @strInput 
END
GO
/****** Object:  Table [dbo].[Account]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[DisplayName] [nvarchar](100) NOT NULL,
	[idStaff] [int] NOT NULL,
	[Password] [nvarchar](1000) NOT NULL,
	[Type] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[DateOrder] [datetime] NOT NULL,
	[DatePayment] [datetime] NULL,
	[idCustomer] [int] NULL,
	[idOrder] [int] NOT NULL,
	[discount] [int] NOT NULL,
	[totalPrices] [float] NOT NULL,
	[status] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillInfo]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillInfo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idBill] [int] NOT NULL,
	[idPerfume] [int] NOT NULL,
	[count] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassMember]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassMember](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nameClass] [nvarchar](100) NOT NULL,
	[point] [float] NOT NULL,
	[discountRate] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nameCustomer] [nvarchar](100) NOT NULL,
	[codeCustomer] [nvarchar](100) NOT NULL,
	[class] [nvarchar](100) NOT NULL,
	[point] [float] NOT NULL,
	[discountRate] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Discount]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discount](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nameDiscount] [nvarchar](100) NOT NULL,
	[codeDiscount] [nvarchar](100) NOT NULL,
	[discountRate] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nameOrder] [nvarchar](100) NOT NULL,
	[hasBill] [int] NOT NULL,
	[status] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Perfume]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Perfume](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[PerfumeName] [nvarchar](100) NOT NULL,
	[idCategory] [int] NOT NULL,
	[price] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PerfumeCategory]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PerfumeCategory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nameCategory] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Staff]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staff](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nameStaff] [nvarchar](100) NOT NULL,
	[codeStaff] [nvarchar](100) NOT NULL,
	[Type] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([id], [UserName], [DisplayName], [idStaff], [Password], [Type]) VALUES (1, N'51800793', N'Long', 1, N'20720532132149213101239102231223249249135100218', 0)
INSERT [dbo].[Account] ([id], [UserName], [DisplayName], [idStaff], [Password], [Type]) VALUES (2, N'51800380', N'Hao', 2, N'20720532132149213101239102231223249249135100218', 0)
INSERT [dbo].[Account] ([id], [UserName], [DisplayName], [idStaff], [Password], [Type]) VALUES (3, N'admin', N'admin', 3, N'20720532132149213101239102231223249249135100218', 1)
INSERT [dbo].[Account] ([id], [UserName], [DisplayName], [idStaff], [Password], [Type]) VALUES (4, N'guest', N'guest', 4, N'20720532132149213101239102231223249249135100218', 0)
SET IDENTITY_INSERT [dbo].[Account] OFF
SET IDENTITY_INSERT [dbo].[ClassMember] ON 

INSERT [dbo].[ClassMember] ([id], [nameClass], [point], [discountRate]) VALUES (1, N'Khách Hàng Sắt', 500, 2)
INSERT [dbo].[ClassMember] ([id], [nameClass], [point], [discountRate]) VALUES (2, N'Khách Hàng Đồng', 1000, 5)
INSERT [dbo].[ClassMember] ([id], [nameClass], [point], [discountRate]) VALUES (3, N'Khách Hàng Bạc', 3000, 10)
INSERT [dbo].[ClassMember] ([id], [nameClass], [point], [discountRate]) VALUES (4, N'Khách Hàng Vàng', 5000, 15)
INSERT [dbo].[ClassMember] ([id], [nameClass], [point], [discountRate]) VALUES (5, N'Khách Hàng Bạch Kim', 7500, 20)
SET IDENTITY_INSERT [dbo].[ClassMember] OFF
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([id], [nameCustomer], [codeCustomer], [class], [point], [discountRate]) VALUES (1, N'Võ Hoàng Anh', N'VHATSCS', N'Khách Hàng Bạch Kim', 7891.24, 20)
INSERT [dbo].[Customer] ([id], [nameCustomer], [codeCustomer], [class], [point], [discountRate]) VALUES (2, N'Trần Thanh Phước', N'TTPTSGV', N'Khách Hàng Vàng', 6841.57, 15)
INSERT [dbo].[Customer] ([id], [nameCustomer], [codeCustomer], [class], [point], [discountRate]) VALUES (3, N'Đặng Mình Thắng', N'DMTCB', N'Khách Hàng Bạc', 3101.57, 10)
INSERT [dbo].[Customer] ([id], [nameCustomer], [codeCustomer], [class], [point], [discountRate]) VALUES (4, N'Trần Trung Tín', N'TTTMLK', N'Khách Hàng Đồng', 1019.24, 5)
INSERT [dbo].[Customer] ([id], [nameCustomer], [codeCustomer], [class], [point], [discountRate]) VALUES (5, N'Mai Ngọc Thắng', N'MNTCO', N'Khách Hàng Sắt', 722.84, 2)
SET IDENTITY_INSERT [dbo].[Customer] OFF
SET IDENTITY_INSERT [dbo].[Discount] ON 

INSERT [dbo].[Discount] ([id], [nameDiscount], [codeDiscount], [discountRate]) VALUES (1, N'Black Friday', N'BLK6TH50', 50)
INSERT [dbo].[Discount] ([id], [nameDiscount], [codeDiscount], [discountRate]) VALUES (2, N'Christmas', N'XMAS25EVE', 40)
INSERT [dbo].[Discount] ([id], [nameDiscount], [codeDiscount], [discountRate]) VALUES (3, N'New Year', N'NYNS35', 35)
INSERT [dbo].[Discount] ([id], [nameDiscount], [codeDiscount], [discountRate]) VALUES (4, N'Lunar New Year', N'LNY2CN', 25)
INSERT [dbo].[Discount] ([id], [nameDiscount], [codeDiscount], [discountRate]) VALUES (5, N'Valentine', N'ALOVE2U', 10)
SET IDENTITY_INSERT [dbo].[Discount] OFF
SET IDENTITY_INSERT [dbo].[Perfume] ON 

INSERT [dbo].[Perfume] ([id], [PerfumeName], [idCategory], [price]) VALUES (1, N'ACE LIPS Matte Liquid Lipstick (plum)', 1, 575000)
INSERT [dbo].[Perfume] ([id], [PerfumeName], [idCategory], [price]) VALUES (2, N'Christian Louboutin Silky Satin Lip Red', 1, 2070000)
INSERT [dbo].[Perfume] ([id], [PerfumeName], [idCategory], [price]) VALUES (3, N'Khol Œil Vinyle Luminous Ink Liner', 4, 1725000)
INSERT [dbo].[Perfume] ([id], [PerfumeName], [idCategory], [price]) VALUES (4, N'Rouge Louboutin Œil Vinyle Luminous Ink Liner', 4, 1725000)
INSERT [dbo].[Perfume] ([id], [PerfumeName], [idCategory], [price]) VALUES (5, N'Tom Ford Boys & Girls Lip Red', 1, 828000)
INSERT [dbo].[Perfume] ([id], [PerfumeName], [idCategory], [price]) VALUES (6, N'TRANSLUCENT FINISHING POWDER', 2, 1426000)
INSERT [dbo].[Perfume] ([id], [PerfumeName], [idCategory], [price]) VALUES (7, N'SHEER HIGHLIGHTING DUO', 2, 1426000)
INSERT [dbo].[Perfume] ([id], [PerfumeName], [idCategory], [price]) VALUES (8, N'EYE DEFINING PEN', 4, 1058000)
INSERT [dbo].[Perfume] ([id], [PerfumeName], [idCategory], [price]) VALUES (9, N'ROUGE PUR COUTURE HOLIDAY EDITION Red', 1, 874000)
INSERT [dbo].[Perfume] ([id], [PerfumeName], [idCategory], [price]) VALUES (10, N'LES SAHARIENNES BRONZING STONES', 2, 1265000)
INSERT [dbo].[Perfume] ([id], [PerfumeName], [idCategory], [price]) VALUES (11, N'COUTURE LIQUID EYELINER FALL LOOK 2019', 4, 782000)
INSERT [dbo].[Perfume] ([id], [PerfumeName], [idCategory], [price]) VALUES (12, N'TOUCHE ÉCLAT SHIMMER STICK HIGHLIGHTER', 3, 782000)
SET IDENTITY_INSERT [dbo].[Perfume] OFF
SET IDENTITY_INSERT [dbo].[PerfumeCategory] ON 

INSERT [dbo].[PerfumeCategory] ([id], [nameCategory]) VALUES (1, N'Lipsticks - Son')
INSERT [dbo].[PerfumeCategory] ([id], [nameCategory]) VALUES (2, N'Face Powder - Phấn phủ')
INSERT [dbo].[PerfumeCategory] ([id], [nameCategory]) VALUES (3, N'Highlighter - Phấn')
INSERT [dbo].[PerfumeCategory] ([id], [nameCategory]) VALUES (4, N'Eye Liner - Kẻ mắt')
SET IDENTITY_INSERT [dbo].[PerfumeCategory] OFF
SET IDENTITY_INSERT [dbo].[Staff] ON 

INSERT [dbo].[Staff] ([id], [nameStaff], [codeStaff], [Type]) VALUES (1, N'Trần Bảo Long', N'51800793', 0)
INSERT [dbo].[Staff] ([id], [nameStaff], [codeStaff], [Type]) VALUES (2, N'Bùi Nhựt Hào', N'51800380', 0)
INSERT [dbo].[Staff] ([id], [nameStaff], [codeStaff], [Type]) VALUES (3, N'Quản Trị Viên', N'00ADMIN00', 1)
INSERT [dbo].[Staff] ([id], [nameStaff], [codeStaff], [Type]) VALUES (4, N'Khách', N'NEWGUEST', 0)
SET IDENTITY_INSERT [dbo].[Staff] OFF
ALTER TABLE [dbo].[Account] ADD  DEFAULT (user_name()) FOR [UserName]
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT (N'Tên') FOR [DisplayName]
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT (N'20720532132149213101239102231223249249135100218') FOR [Password]
GO
ALTER TABLE [dbo].[Account] ADD  DEFAULT ((0)) FOR [Type]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT (getdate()) FOR [DateOrder]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT ((0)) FOR [discount]
GO
ALTER TABLE [dbo].[Bill] ADD  DEFAULT ((0)) FOR [totalPrices]
GO
ALTER TABLE [dbo].[BillInfo] ADD  DEFAULT ((0)) FOR [count]
GO
ALTER TABLE [dbo].[ClassMember] ADD  DEFAULT (N'Thường') FOR [nameClass]
GO
ALTER TABLE [dbo].[ClassMember] ADD  DEFAULT ((0.0)) FOR [point]
GO
ALTER TABLE [dbo].[ClassMember] ADD  DEFAULT ((0)) FOR [discountRate]
GO
ALTER TABLE [dbo].[Customer] ADD  DEFAULT (N'Tên Khách Hàng') FOR [nameCustomer]
GO
ALTER TABLE [dbo].[Customer] ADD  DEFAULT (N'KH0000') FOR [codeCustomer]
GO
ALTER TABLE [dbo].[Customer] ADD  DEFAULT (N'Thường') FOR [class]
GO
ALTER TABLE [dbo].[Customer] ADD  DEFAULT ((0.0)) FOR [point]
GO
ALTER TABLE [dbo].[Customer] ADD  DEFAULT ((0)) FOR [discountRate]
GO
ALTER TABLE [dbo].[Discount] ADD  DEFAULT (N'Tên Sự Kiện') FOR [nameDiscount]
GO
ALTER TABLE [dbo].[Discount] ADD  DEFAULT (N'NULL') FOR [codeDiscount]
GO
ALTER TABLE [dbo].[Discount] ADD  DEFAULT ((0)) FOR [discountRate]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (N'Chưa đặt tên') FOR [nameOrder]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT ((0)) FOR [hasBill]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (N'Trống ') FOR [status]
GO
ALTER TABLE [dbo].[Perfume] ADD  DEFAULT (N'Chưa đặt tên') FOR [PerfumeName]
GO
ALTER TABLE [dbo].[Perfume] ADD  DEFAULT ((0.0)) FOR [price]
GO
ALTER TABLE [dbo].[PerfumeCategory] ADD  DEFAULT (N'Chưa đặt tên') FOR [nameCategory]
GO
ALTER TABLE [dbo].[Staff] ADD  DEFAULT (N'Tên Nhân Viên') FOR [nameStaff]
GO
ALTER TABLE [dbo].[Staff] ADD  DEFAULT (N'NEW0000') FOR [codeStaff]
GO
ALTER TABLE [dbo].[Staff] ADD  DEFAULT ((0)) FOR [Type]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD FOREIGN KEY([idStaff])
REFERENCES [dbo].[Staff] ([id])
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD FOREIGN KEY([idCustomer])
REFERENCES [dbo].[Customer] ([id])
GO
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([idBill])
REFERENCES [dbo].[Bill] ([id])
GO
ALTER TABLE [dbo].[BillInfo]  WITH CHECK ADD FOREIGN KEY([idPerfume])
REFERENCES [dbo].[Perfume] ([id])
GO
ALTER TABLE [dbo].[Perfume]  WITH CHECK ADD FOREIGN KEY([idCategory])
REFERENCES [dbo].[PerfumeCategory] ([id])
GO
/****** Object:  StoredProcedure [dbo].[USP_DeleteOrder]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_DeleteOrder]
@idOrder INT
AS
BEGIN
	UPDATE dbo.orders SET status = N'Ẩn ' WHERE id = @idOrder
	
END
GO
/****** Object:  StoredProcedure [dbo].[USP_FindDeletedOrder]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_FindDeletedOrder]
AS
BEGIN
	SELECT TOP 1 id FROM dbo.Orders WHERE status = N'Ẩn '

END
GO
/****** Object:  StoredProcedure [dbo].[USP_GetAccountByUserName]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_GetAccountByUserName]
@userName nvarchar(100)
AS
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GetListBillByDate]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_GetListBillByDate]
@dateOrder DATE, @datePayment DATE
AS
BEGIN
	SELECT o.nameOrder AS N'Yêu Cầu', DateOrder AS N'Ngày Đặt', DatePayment AS N'Ngày Thanh Toán', p.PerfumeName AS N'Tên Sản Phẩm', bi.count AS N'Số Lượng', p.price AS N'Đơn Giá', b.totalPrices AS N'Thành Tiền', b.discount AS N'Giảm Giá', b.totalPrices - (b.totalPrices/ 100) * b.discount AS N'Tổng Tiền'
	FROM dbo.Bill AS b, dbo.BillInfo AS bi, dbo.Orders AS o, dbo.Perfume AS p
	WHERE DateOrder >= @dateOrder AND DatePayment <=  @datePayment AND b.status = 1 AND idOrder = o.id AND bi.idBill = b.id AND bi.idPerfume = p.id
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GetListBillByDateAndPage]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_GetListBillByDateAndPage]
@dateOrder DATE, @datePayment DATE, @page INT
AS 
BEGIN
	DECLARE @pageRows INT = 10

	DECLARE @selectRows INT = @pageRows

	DECLARE @exceptRows INT = (@page - 1) * @pageRows
		
	;WITH BillShow AS( SELECT b.ID, o.nameOrder AS [Yêu Cầu], b.totalPrices AS [Tổng tiền], DateOrder AS [Ngày yêu cầu], DatePayment AS [Ngày thanh toán], discount AS [Giảm giá]
	
	FROM dbo.Bill AS b, dbo.Orders AS o
	
	WHERE DateOrder >= @dateOrder AND DatePayment <= @datePayment AND b.status = 1 AND o.id = b.idOrder)
	
	SELECT TOP (@selectRows) * FROM BillShow 
	WHERE id NOT IN (SELECT TOP (@exceptRows) id FROM BillShow)
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GetListBillByDateForReport]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_GetListBillByDateForReport]
@dateOrder DATE, @datePayment DATE
AS 
BEGIN
	SELECT o.NameOrder, b.totalPrices, DateOrder, DatePayment, discount
	FROM dbo.Bill AS b,dbo.Orders AS o
	WHERE DateOrder >= @dateOrder AND DatePayment <= @datePayment AND b.status = 1
	AND o.NameOrder = b.idOrder
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GetNumBillByDate]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_GetNumBillByDate]
@dateOrder DATE, @datePayment DATE
AS 
BEGIN
	SELECT COUNT(*)
	
	FROM dbo.Bill AS b, dbo.Orders AS o
	
	WHERE DateOrder >= @dateOrder AND DatePayment <= @datePayment AND b.status = 1 AND o.id = b.idOrder
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GetOrderList]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_GetOrderList]
AS SELECT * FROM dbo.Orders WHERE status <> N'Đã thanh toán ' AND status <> N'Ẩn '
GO
/****** Object:  StoredProcedure [dbo].[USP_InsertBill]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_InsertBill]
@idOrder INT
AS
BEGIN
	INSERT dbo.Bill
	( DateOrder, DatePayment, idOrder, discount, status)
	VALUES 
	( GETDATE() , NULL , @idOrder , 0 , 0)

END
GO
/****** Object:  StoredProcedure [dbo].[USP_InsertBillInfo]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_InsertBillInfo]
@idBill INT, @idPerfume INT, @count INT
AS
BEGIN
	DECLARE @isExistBillInfo INT

	DECLARE @perfumeCount INT = 1

	SELECT @isExistBillInfo = id , @perfumeCount = bi.count 
	
	FROM dbo.BillInfo AS bi
	
	WHERE idBill = @idBill AND idPerfume = @idPerfume

	IF (@isExistBillInfo > 0)
	BEGIN
		DECLARE @newcount INT = @perfumeCount + @count

		IF (@newcount > 0)
			UPDATE dbo.BillInfo SET count = @perfumeCount + @count WHERE idPerfume = @idPerfume

		ELSE
			DELETE dbo.BillInfo WHERE idBill = @idBill AND idPerfume = @idPerfume
	END

	ELSE 
	BEGIN
		INSERT dbo.BillInfo(  idBill, idPerfume, count)
		VALUES ( @idBill, @idPerfume, @count) 
		
	END
	
END
GO
/****** Object:  StoredProcedure [dbo].[USP_InsertDeletedOrder]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_InsertDeletedOrder]
@idDeletedOrder INT 
AS
BEGIN
	UPDATE dbo.Orders SET status = N'Trống ' WHERE id = @idDeletedOrder

END
GO
/****** Object:  StoredProcedure [dbo].[USP_InsertOrder]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_InsertOrder]
@nOrders INT
AS
BEGIN
	INSERT dbo.Orders(  nameOrder, hasBill, status )
	VALUES ( N'Yêu cầu số ' + CAST(@nOrders as NVARCHAR(100)), 1, N'Trống ') 
END
GO
/****** Object:  StoredProcedure [dbo].[USP_UpdateAccountInfo]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_UpdateAccountInfo]
@userName NVARCHAR(100), @displayName NVARCHAR(100), @password NVARCHAR(100), @newPassword NVARCHAR(100)
AS
BEGIN
	DECLARE @isRightPass INT = 0

	SELECT @isRightPass = COUNT (*) FROM dbo.Account WHERE UserName = @userName AND Password = @password

	IF (@isRightPass > 0)
	BEGIN
		IF(@newPassword = NULL OR @newPassword = N'')
		
			UPDATE dbo.Account SET DisplayName = @displayName WHERE UserName = @userName

		
		ELSE
			UPDATE dbo.Account SET DisplayName = @displayName, Password = @newPassword WHERE UserName = @userName

	END
END
GO
/****** Object:  StoredProcedure [dbo].[USP_Validate]    Script Date: 12/3/2019 8:46:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[USP_Validate]
@userName nvarchar(100), @Password nvarchar(100)
AS
BEGIN
	SELECT * FROM dbo.Account WHERE UserName = @userName AND Password = @Password
END
GO
USE [master]
GO
ALTER DATABASE [QuanLyCuaHangMyPham] SET  READ_WRITE 
GO
