USE [master]
GO
/****** Object:  Database [UserEncrypt]    Script Date: 13/12/2024 13:41:36 ******/
CREATE DATABASE [UserEncrypt]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'UserEncrypt', FILENAME = N'D:\Programming\DataBase\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\UserEncrypt.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'UserEncrypt_log', FILENAME = N'D:\Programming\DataBase\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\UserEncrypt_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [UserEncrypt] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [UserEncrypt].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [UserEncrypt] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [UserEncrypt] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [UserEncrypt] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [UserEncrypt] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [UserEncrypt] SET ARITHABORT OFF 
GO
ALTER DATABASE [UserEncrypt] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [UserEncrypt] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [UserEncrypt] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [UserEncrypt] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [UserEncrypt] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [UserEncrypt] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [UserEncrypt] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [UserEncrypt] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [UserEncrypt] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [UserEncrypt] SET  ENABLE_BROKER 
GO
ALTER DATABASE [UserEncrypt] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [UserEncrypt] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [UserEncrypt] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [UserEncrypt] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [UserEncrypt] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [UserEncrypt] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [UserEncrypt] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [UserEncrypt] SET RECOVERY FULL 
GO
ALTER DATABASE [UserEncrypt] SET  MULTI_USER 
GO
ALTER DATABASE [UserEncrypt] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [UserEncrypt] SET DB_CHAINING OFF 
GO
ALTER DATABASE [UserEncrypt] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [UserEncrypt] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [UserEncrypt] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [UserEncrypt] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'UserEncrypt', N'ON'
GO
ALTER DATABASE [UserEncrypt] SET QUERY_STORE = ON
GO
ALTER DATABASE [UserEncrypt] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [UserEncrypt]
GO
/****** Object:  Table [dbo].[People]    Script Date: 13/12/2024 13:41:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[People](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[IdentificationNumber] [varchar](10) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[CreatedAt] [datetime] NULL,
	[DocumentType] [varchar](5) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 13/12/2024 13:41:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[PasswordHash] [varchar](70) NOT NULL,
	[NewPassword] [varchar](70) NULL,
	[HashKey] [binary](32) NULL,
	[HashIV] [binary](16) NULL,
	[CreatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[People] ON 

INSERT [dbo].[People] ([Id], [FirstName], [LastName], [IdentificationNumber], [Email], [CreatedAt], [DocumentType]) VALUES (1, N'Julian', N'perez', N'1245777877', N'juli@gmail.com', CAST(N'2024-12-07T01:24:48.167' AS DateTime), N'TI')
INSERT [dbo].[People] ([Id], [FirstName], [LastName], [IdentificationNumber], [Email], [CreatedAt], [DocumentType]) VALUES (3, N'Andres', N'Diez T.', N'1039476063', N'andresdieztuberquia@gmail.com', CAST(N'2024-12-10T17:29:24.000' AS DateTime), N'CC')
INSERT [dbo].[People] ([Id], [FirstName], [LastName], [IdentificationNumber], [Email], [CreatedAt], [DocumentType]) VALUES (4, N'Alejandra', N'Rodriguez', N'1231135291', N'alejandra@gmail.com', CAST(N'2024-12-13T18:31:13.127' AS DateTime), N'CC')
SET IDENTITY_INSERT [dbo].[People] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Username], [PasswordHash], [NewPassword], [HashKey], [HashIV], [CreatedAt]) VALUES (1, N'Andres Diez', N'/Aj3497ENm/4XrJco9kcCw==', NULL, 0x9DD22C14AE9D0ED32B0F4BA5230AA37CC991E7BF3DF8A90DDE044D0AAE30B39E, 0x20F90DF635E5747D344525A0A4D07EC2, CAST(N'2024-12-06T22:18:32.000' AS DateTime))
INSERT [dbo].[Users] ([Id], [Username], [PasswordHash], [NewPassword], [HashKey], [HashIV], [CreatedAt]) VALUES (9, N'Alejandra', N'wNIKRi4/W4bTf+FS/ckPUQ==', NULL, 0xEC701AD7DC8A62AED39DE8225C51DCED6DBA9310A84969E5144E70591BA6B60A, 0x6FF661205816303987659DD27EE3A69C, CAST(N'2024-12-12T21:54:59.450' AS DateTime))
INSERT [dbo].[Users] ([Id], [Username], [PasswordHash], [NewPassword], [HashKey], [HashIV], [CreatedAt]) VALUES (13, N'Tes1234', N'lkUFEMsTvy/V9gIIHD3aUQ==', NULL, 0x1E75451B65ADDC0FE3014B0CCFAC24E68704C091300F3D81E7122B2669DD1757, 0x5178F87D2D0B471F839945EE91C74DC6, CAST(N'2024-12-13T18:33:35.637' AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__People__9CD146942E3C7DE9]    Script Date: 13/12/2024 13:41:36 ******/
ALTER TABLE [dbo].[People] ADD UNIQUE NONCLUSTERED 
(
	[IdentificationNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__People__A9D105341BD34904]    Script Date: 13/12/2024 13:41:36 ******/
ALTER TABLE [dbo].[People] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[People] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
USE [master]
GO
ALTER DATABASE [UserEncrypt] SET  READ_WRITE 
GO
