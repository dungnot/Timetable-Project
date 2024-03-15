USE [master]
GO
/****** Object:  Database [ScheduleManagement]    Script Date: 3/5/2024 1:15:02 PM ******/
CREATE DATABASE [ScheduleManagement]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ScheduleManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ScheduleManagement.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ScheduleManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\ScheduleManagement_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ScheduleManagement] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ScheduleManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ScheduleManagement] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ScheduleManagement] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ScheduleManagement] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ScheduleManagement] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ScheduleManagement] SET ARITHABORT OFF 
GO
ALTER DATABASE [ScheduleManagement] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ScheduleManagement] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ScheduleManagement] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ScheduleManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ScheduleManagement] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ScheduleManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ScheduleManagement] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ScheduleManagement] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ScheduleManagement] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ScheduleManagement] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ScheduleManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ScheduleManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ScheduleManagement] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ScheduleManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ScheduleManagement] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ScheduleManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ScheduleManagement] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ScheduleManagement] SET RECOVERY FULL 
GO
ALTER DATABASE [ScheduleManagement] SET  MULTI_USER 
GO
ALTER DATABASE [ScheduleManagement] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ScheduleManagement] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ScheduleManagement] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ScheduleManagement] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ScheduleManagement] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ScheduleManagement] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ScheduleManagement', N'ON'
GO
ALTER DATABASE [ScheduleManagement] SET QUERY_STORE = ON
GO
ALTER DATABASE [ScheduleManagement] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ScheduleManagement]
GO
/****** Object:  Table [dbo].[Buildings]    Script Date: 3/5/2024 1:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Buildings](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](255) NOT NULL,
	[details] [nvarchar](255) NOT NULL,
 CONSTRAINT [buildings_id_primary] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Classes]    Script Date: 3/5/2024 1:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](255) NOT NULL,
 CONSTRAINT [classes_id_primary] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 3/5/2024 1:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](255) NOT NULL,
	[buildingId] [int] NULL,
 CONSTRAINT [rooms_id_primary] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 3/5/2024 1:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[classId] [int] NOT NULL,
	[subjectId] [int] NOT NULL,
	[teacherId] [int] NOT NULL,
	[roomId] [int] NOT NULL,
	[slotId] [int] NOT NULL,
	[date] [datetime] NOT NULL,
 CONSTRAINT [schedule_id_primary] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Slots]    Script Date: 3/5/2024 1:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Slots](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[details] [nvarchar](255) NOT NULL,
	[slotName] [nvarchar](255) NOT NULL,
 CONSTRAINT [slots_id_primary] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subjects]    Script Date: 3/5/2024 1:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subjects](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](255) NOT NULL,
	[details] [nvarchar](255) NOT NULL,
 CONSTRAINT [subjects_id_primary] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teachers]    Script Date: 3/5/2024 1:15:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teachers](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](255) NOT NULL,
	[fullName] [nvarchar](255) NOT NULL,
	[email] [nvarchar](255) NOT NULL,
 CONSTRAINT [teachers_id_primary] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Rooms]  WITH CHECK ADD  CONSTRAINT [rooms_buildingid_foreign] FOREIGN KEY([buildingId])
REFERENCES [dbo].[Buildings] ([id])
GO
ALTER TABLE [dbo].[Rooms] CHECK CONSTRAINT [rooms_buildingid_foreign]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [schedule_classid_foreign] FOREIGN KEY([classId])
REFERENCES [dbo].[Classes] ([id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [schedule_classid_foreign]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [schedule_roomid_foreign] FOREIGN KEY([roomId])
REFERENCES [dbo].[Rooms] ([id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [schedule_roomid_foreign]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [schedule_subjectid_foreign] FOREIGN KEY([subjectId])
REFERENCES [dbo].[Subjects] ([id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [schedule_subjectid_foreign]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [schedule_teacherid_foreign] FOREIGN KEY([teacherId])
REFERENCES [dbo].[Teachers] ([id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [schedule_teacherid_foreign]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [schedule_timeslotid_foreign] FOREIGN KEY([slotId])
REFERENCES [dbo].[Slots] ([id])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [schedule_timeslotid_foreign]
GO
USE [master]
GO
ALTER DATABASE [ScheduleManagement] SET  READ_WRITE 
GO
