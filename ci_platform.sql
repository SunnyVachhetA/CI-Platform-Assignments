USE [master]
GO
/****** Object:  Database [CI_PLATFORM]    Script Date: 27-04-2023 14:05:10 ******/
CREATE DATABASE [CI_PLATFORM]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CI_PLATFORM', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\CI_PLATFORM.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CI_PLATFORM_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\CI_PLATFORM_log.ldf' , SIZE = 139264KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [CI_PLATFORM] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CI_PLATFORM].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CI_PLATFORM] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CI_PLATFORM] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CI_PLATFORM] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CI_PLATFORM] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CI_PLATFORM] SET ARITHABORT OFF 
GO
ALTER DATABASE [CI_PLATFORM] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CI_PLATFORM] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CI_PLATFORM] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CI_PLATFORM] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CI_PLATFORM] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CI_PLATFORM] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CI_PLATFORM] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CI_PLATFORM] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CI_PLATFORM] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CI_PLATFORM] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CI_PLATFORM] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CI_PLATFORM] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CI_PLATFORM] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CI_PLATFORM] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CI_PLATFORM] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CI_PLATFORM] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CI_PLATFORM] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CI_PLATFORM] SET RECOVERY FULL 
GO
ALTER DATABASE [CI_PLATFORM] SET  MULTI_USER 
GO
ALTER DATABASE [CI_PLATFORM] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CI_PLATFORM] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CI_PLATFORM] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CI_PLATFORM] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CI_PLATFORM] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CI_PLATFORM] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'CI_PLATFORM', N'ON'
GO
ALTER DATABASE [CI_PLATFORM] SET QUERY_STORE = ON
GO
ALTER DATABASE [CI_PLATFORM] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [CI_PLATFORM]
GO
/****** Object:  Table [dbo].[admin]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[admin](
	[admin_id] [tinyint] IDENTITY(1,1) NOT NULL,
	[first_name] [varchar](16) NOT NULL,
	[last_name] [varchar](16) NOT NULL,
	[email] [varchar](128) NOT NULL,
	[password] [varchar](255) NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_admin] PRIMARY KEY CLUSTERED 
(
	[admin_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[banner]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[banner](
	[banner_id] [bigint] IDENTITY(1,1) NOT NULL,
	[title] [varchar](255) NOT NULL,
	[text] [text] NULL,
	[sort_order] [int] NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
	[path] [varchar](255) NOT NULL,
	[status] [bit] NULL,
 CONSTRAINT [PK_banner] PRIMARY KEY CLUSTERED 
(
	[banner_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[city]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[city](
	[city_id] [int] IDENTITY(1,1) NOT NULL,
	[country_id] [tinyint] NOT NULL,
	[name] [varchar](255) NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_city] PRIMARY KEY CLUSTERED 
(
	[city_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cms_page]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cms_page](
	[cms_page_id] [smallint] IDENTITY(1,1) NOT NULL,
	[title] [varchar](255) NOT NULL,
	[description] [text] NULL,
	[slug] [varchar](255) NOT NULL,
	[status] [bit] NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_cms_page] PRIMARY KEY CLUSTERED 
(
	[cms_page_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[comment]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[comment](
	[comment_id] [bigint] IDENTITY(1,1) NOT NULL,
	[user_id] [bigint] NOT NULL,
	[mission_id] [bigint] NOT NULL,
	[approval_status] [bit] NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
	[comment_text] [text] NULL,
 CONSTRAINT [PK_comment] PRIMARY KEY CLUSTERED 
(
	[comment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[contact_us]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[contact_us](
	[contact_id] [bigint] IDENTITY(1,1) NOT NULL,
	[user_id] [bigint] NOT NULL,
	[subject] [varchar](255) NOT NULL,
	[message] [varchar](6000) NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[status] [tinyint] NULL,
	[response] [varchar](6000) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
	[is_deleted] [bit] NULL,
 CONSTRAINT [PK_contact_us] PRIMARY KEY CLUSTERED 
(
	[contact_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[country]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[country](
	[country_id] [tinyint] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NOT NULL,
	[ISO] [varchar](16) NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_country] PRIMARY KEY CLUSTERED 
(
	[country_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[favourite_mission]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[favourite_mission](
	[favourite_mission_id] [bigint] IDENTITY(1,1) NOT NULL,
	[mission_id] [bigint] NOT NULL,
	[user_id] [bigint] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_favourite_mission] PRIMARY KEY CLUSTERED 
(
	[favourite_mission_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[goal_mission]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[goal_mission](
	[goal_mission_id] [bigint] IDENTITY(1,1) NOT NULL,
	[mission_id] [bigint] NOT NULL,
	[goal_objective_text] [varchar](255) NULL,
	[goal_value] [int] NOT NULL,
	[goal_achived] [int] NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_goal_mission] PRIMARY KEY CLUSTERED 
(
	[goal_mission_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mission]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mission](
	[mission_id] [bigint] IDENTITY(1,1) NOT NULL,
	[theme_id] [smallint] NULL,
	[country_id] [tinyint] NULL,
	[title] [varchar](128) NULL,
	[short_description] [text] NULL,
	[start_date] [datetimeoffset](7) NULL,
	[end_date] [datetimeoffset](7) NULL,
	[mission_type] [bit] NOT NULL,
	[status] [bit] NULL,
	[organization_name] [varchar](255) NULL,
	[organization_detail] [text] NULL,
	[availability] [tinyint] NULL,
	[total_seat] [bigint] NULL,
	[registration_deadline] [datetimeoffset](7) NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
	[description] [text] NULL,
	[rating] [tinyint] NULL,
	[city_id] [int] NULL,
	[is_active] [bit] NULL,
 CONSTRAINT [PK_mission] PRIMARY KEY CLUSTERED 
(
	[mission_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mission_application]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mission_application](
	[mission_application_id] [bigint] IDENTITY(1,1) NOT NULL,
	[mission_id] [bigint] NOT NULL,
	[user_id] [bigint] NOT NULL,
	[approval_status] [tinyint] NULL,
	[applied_at] [datetimeoffset](7) NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_mission_application] PRIMARY KEY CLUSTERED 
(
	[mission_application_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mission_document]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mission_document](
	[mission_document_id] [bigint] IDENTITY(1,1) NOT NULL,
	[mission_id] [bigint] NOT NULL,
	[document_name] [varchar](255) NULL,
	[document_type] [varchar](5) NULL,
	[document_path] [varchar](255) NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
	[document_title] [varchar](255) NULL,
 CONSTRAINT [PK_mission_document] PRIMARY KEY CLUSTERED 
(
	[mission_document_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mission_invite]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mission_invite](
	[mission_invite_id] [bigint] IDENTITY(1,1) NOT NULL,
	[mission_id] [bigint] NOT NULL,
	[from_user_id] [bigint] NOT NULL,
	[to_user_id] [bigint] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_mission_invite] PRIMARY KEY CLUSTERED 
(
	[mission_invite_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mission_media]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mission_media](
	[mission_media_id] [bigint] IDENTITY(1,1) NOT NULL,
	[mission_id] [bigint] NOT NULL,
	[media_name] [varchar](64) NULL,
	[media_type] [varchar](5) NULL,
	[media_path] [varchar](255) NULL,
	[default] [bit] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_mission_media] PRIMARY KEY CLUSTERED 
(
	[mission_media_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mission_rating]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mission_rating](
	[mission_rating_id] [bigint] IDENTITY(1,1) NOT NULL,
	[user_id] [bigint] NOT NULL,
	[mission_id] [bigint] NOT NULL,
	[rating] [tinyint] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_mission_rating] PRIMARY KEY CLUSTERED 
(
	[mission_rating_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mission_skill]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mission_skill](
	[mission_skill_id] [bigint] IDENTITY(1,1) NOT NULL,
	[mission_id] [bigint] NOT NULL,
	[skill_id] [smallint] NOT NULL,
	[status] [bit] NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_mission_skill] PRIMARY KEY CLUSTERED 
(
	[mission_skill_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[mission_theme]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[mission_theme](
	[theme_id] [smallint] IDENTITY(1,1) NOT NULL,
	[title] [varchar](255) NULL,
	[status] [bit] NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_mission_theme] PRIMARY KEY CLUSTERED 
(
	[theme_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[password_reset]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[password_reset](
	[email] [varchar](128) NOT NULL,
	[token] [varchar](191) NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_password_reset] PRIMARY KEY CLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[skill]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[skill](
	[skill_id] [smallint] IDENTITY(1,1) NOT NULL,
	[name] [varchar](64) NOT NULL,
	[status] [bit] NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_skill] PRIMARY KEY CLUSTERED 
(
	[skill_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[story]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[story](
	[story_id] [bigint] IDENTITY(1,1) NOT NULL,
	[user_id] [bigint] NOT NULL,
	[mission_id] [bigint] NOT NULL,
	[title] [varchar](255) NULL,
	[description] [text] NULL,
	[status] [tinyint] NULL,
	[published_at] [datetimeoffset](7) NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
	[video_url] [varchar](255) NULL,
	[short_description] [varchar](255) NULL,
	[story_view] [bigint] NULL,
	[is_deleted] [bit] NULL,
 CONSTRAINT [PK_story] PRIMARY KEY CLUSTERED 
(
	[story_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[story_invite]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[story_invite](
	[story_invite_id] [bigint] IDENTITY(1,1) NOT NULL,
	[story_id] [bigint] NOT NULL,
	[from_user_id] [bigint] NOT NULL,
	[to_user_id] [bigint] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_story_invite] PRIMARY KEY CLUSTERED 
(
	[story_invite_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[story_media]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[story_media](
	[story_media_id] [bigint] IDENTITY(1,1) NOT NULL,
	[story_id] [bigint] NOT NULL,
	[media_name] [varchar](255) NULL,
	[media_type] [varchar](5) NULL,
	[media_path] [varchar](255) NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_story_media] PRIMARY KEY CLUSTERED 
(
	[story_media_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[timesheet]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[timesheet](
	[timesheet_id] [bigint] IDENTITY(1,1) NOT NULL,
	[user_id] [bigint] NOT NULL,
	[mission_id] [bigint] NOT NULL,
	[time] [time](7) NULL,
	[action] [int] NULL,
	[date_volunteered] [datetimeoffset](7) NULL,
	[notes] [text] NULL,
	[status] [tinyint] NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_timesheet] PRIMARY KEY CLUSTERED 
(
	[timesheet_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[user_id] [bigint] IDENTITY(1,1) NOT NULL,
	[first_name] [varchar](16) NOT NULL,
	[last_name] [varchar](16) NOT NULL,
	[email] [varchar](128) NOT NULL,
	[password] [varchar](255) NOT NULL,
	[phone_number] [varchar](15) NOT NULL,
	[avatar] [varchar](255) NULL,
	[why_i_volunteer] [text] NULL,
	[employee_id] [varchar](16) NULL,
	[department] [varchar](16) NULL,
	[city_id] [int] NULL,
	[country_id] [tinyint] NULL,
	[profile_text] [text] NULL,
	[linked_in_url] [varchar](255) NULL,
	[title] [varchar](255) NULL,
	[status] [bit] NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
	[availability] [tinyint] NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user_skill]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_skill](
	[user_skill_id] [bigint] IDENTITY(1,1) NOT NULL,
	[user_id] [bigint] NOT NULL,
	[skill_id] [smallint] NOT NULL,
	[created_at] [datetimeoffset](7) NOT NULL,
	[updated_at] [datetimeoffset](7) NULL,
	[deleted_at] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_user_skill] PRIMARY KEY CLUSTERED 
(
	[user_skill_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[verify_email]    Script Date: 27-04-2023 14:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[verify_email](
	[email] [varchar](255) NOT NULL,
	[token] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[admin] ON 

INSERT [dbo].[admin] ([admin_id], [first_name], [last_name], [email], [password], [created_at], [updated_at], [deleted_at]) VALUES (1, N'Admin', N'Victor', N'sunnyvachheta89+admin@gmail.com', N'57TwI1F81HzomqZgko1FJPjKfwg3pliYLLjnZkX8yaQ=', CAST(N'2023-04-27T00:00:00.0000000+05:30' AS DateTimeOffset), NULL, NULL)
SET IDENTITY_INSERT [dbo].[admin] OFF
GO
SET IDENTITY_INSERT [dbo].[banner] ON 

INSERT [dbo].[banner] ([banner_id], [title], [text], [sort_order], [created_at], [updated_at], [deleted_at], [path], [status]) VALUES (1, N'One piece banner image', NULL, 0, CAST(N'2023-04-20T10:53:39.2627857+05:30' AS DateTimeOffset), CAST(N'2023-04-26T15:08:40.2023700+05:30' AS DateTimeOffset), CAST(N'2023-04-26T15:08:30.3236229+05:30' AS DateTimeOffset), N'\images\banner\f0800377-8d5a-4ed1-b423-3a5acfea1829.jpg', 1)
INSERT [dbo].[banner] ([banner_id], [title], [text], [sort_order], [created_at], [updated_at], [deleted_at], [path], [status]) VALUES (2, N'One piece banner image', NULL, 100, CAST(N'2023-04-20T11:21:20.5523061+05:30' AS DateTimeOffset), CAST(N'2023-04-25T10:15:34.3489840+05:30' AS DateTimeOffset), CAST(N'2023-04-25T10:15:30.6256884+05:30' AS DateTimeOffset), N'\images\banner\6cb26513-454c-4847-96d6-0f19e644a10e.jpg', 1)
INSERT [dbo].[banner] ([banner_id], [title], [text], [sort_order], [created_at], [updated_at], [deleted_at], [path], [status]) VALUES (3, N'Background image', N'Some random text', 0, CAST(N'2023-04-20T12:22:41.4595123+05:30' AS DateTimeOffset), CAST(N'2023-04-20T12:58:13.4728002+05:30' AS DateTimeOffset), CAST(N'2023-04-20T12:58:07.7247306+05:30' AS DateTimeOffset), N'\images\banner\db9a25e0-eb09-4add-85f4-9bb377300e8d.jpg', 1)
INSERT [dbo].[banner] ([banner_id], [title], [text], [sort_order], [created_at], [updated_at], [deleted_at], [path], [status]) VALUES (4, N'jlakjd ajsdlka ', NULL, 100, CAST(N'2023-04-25T19:13:30.3273048+05:30' AS DateTimeOffset), CAST(N'2023-04-26T15:11:03.9182318+05:30' AS DateTimeOffset), CAST(N'2023-04-26T15:10:55.2141546+05:30' AS DateTimeOffset), N'\images\banner\4d4db1ae-1bd8-4259-b962-38684995bbfd.png', 1)
INSERT [dbo].[banner] ([banner_id], [title], [text], [sort_order], [created_at], [updated_at], [deleted_at], [path], [status]) VALUES (5, N'Home picture', NULL, 101, CAST(N'2023-04-26T15:07:17.6321208+05:30' AS DateTimeOffset), NULL, NULL, N'\images\banner\df9df101-d843-4eff-bd89-c98d083f8f7e.png', 1)
SET IDENTITY_INSERT [dbo].[banner] OFF
GO
SET IDENTITY_INSERT [dbo].[city] ON 

INSERT [dbo].[city] ([city_id], [country_id], [name], [created_at], [updated_at], [deleted_at]) VALUES (2, 1, N'Ahmedabad', CAST(N'2023-03-06T15:34:55.6033333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[city] ([city_id], [country_id], [name], [created_at], [updated_at], [deleted_at]) VALUES (3, 1, N'Delhi', CAST(N'2023-03-06T15:34:55.6033333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[city] ([city_id], [country_id], [name], [created_at], [updated_at], [deleted_at]) VALUES (4, 1, N'Mumbai', CAST(N'2023-03-06T15:34:55.6033333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[city] ([city_id], [country_id], [name], [created_at], [updated_at], [deleted_at]) VALUES (5, 2, N'Sydney', CAST(N'2023-03-06T15:34:55.6033333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[city] ([city_id], [country_id], [name], [created_at], [updated_at], [deleted_at]) VALUES (6, 2, N'Canbera', CAST(N'2023-03-06T15:34:55.6033333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[city] ([city_id], [country_id], [name], [created_at], [updated_at], [deleted_at]) VALUES (7, 2, N'Capetown', CAST(N'2023-03-06T15:34:55.6033333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[city] ([city_id], [country_id], [name], [created_at], [updated_at], [deleted_at]) VALUES (8, 3, N'Auckland', CAST(N'2023-03-06T15:34:55.6033333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[city] ([city_id], [country_id], [name], [created_at], [updated_at], [deleted_at]) VALUES (9, 3, N'Valington', CAST(N'2023-03-06T15:34:55.6033333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[city] ([city_id], [country_id], [name], [created_at], [updated_at], [deleted_at]) VALUES (10, 4, N'Tokyo', CAST(N'2023-03-06T15:34:55.6033333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[city] ([city_id], [country_id], [name], [created_at], [updated_at], [deleted_at]) VALUES (11, 4, N'Karakura', CAST(N'2023-03-06T15:34:55.6033333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[city] ([city_id], [country_id], [name], [created_at], [updated_at], [deleted_at]) VALUES (12, 4, N'Hamamatsu', CAST(N'2023-03-06T15:34:55.6033333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[city] ([city_id], [country_id], [name], [created_at], [updated_at], [deleted_at]) VALUES (13, 5, N'Texas', CAST(N'2023-03-06T15:34:55.6033333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[city] ([city_id], [country_id], [name], [created_at], [updated_at], [deleted_at]) VALUES (14, 5, N'NewYork', CAST(N'2023-03-06T15:34:55.6033333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[city] ([city_id], [country_id], [name], [created_at], [updated_at], [deleted_at]) VALUES (15, 5, N'San Fransisco', CAST(N'2023-03-06T15:34:55.6033333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[city] ([city_id], [country_id], [name], [created_at], [updated_at], [deleted_at]) VALUES (16, 6, N'Helsinki', CAST(N'2023-03-06T15:34:55.6033333+00:00' AS DateTimeOffset), NULL, NULL)
SET IDENTITY_INSERT [dbo].[city] OFF
GO
SET IDENTITY_INSERT [dbo].[cms_page] ON 

INSERT [dbo].[cms_page] ([cms_page_id], [title], [description], [slug], [status], [created_at], [updated_at], [deleted_at]) VALUES (2, N'What is CMS?', N'<p><strong>What is a content management system</strong>? A content management system (CMS) is an application that helps you create and manage a website via a human-friendly interface rather than needing to work directly with code.</p>
<p>Over the rest of this post, we&rsquo;ll dig into the question of &ldquo;what is a content management system&rdquo; with a more detailed CMS definition and share some examples of the most popular content management systems.</p>', N'privacy-policy', 1, CAST(N'2023-04-12T14:06:37.4225466+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[cms_page] ([cms_page_id], [title], [description], [slug], [status], [created_at], [updated_at], [deleted_at]) VALUES (3, N'Privacy Policy', N'<p>Change: Environmental policy refers to the set of laws, regulations, and guidelines that govern the management and protection of natural resources and the environment. The primary goal of environmental policy is to minimize the negative impact of human activities on the environment while promoting sustainable development.</p>
<p>One of the main challenges facing environmental policy is balancing economic growth with environmental protection. Environmental policies must take into account the needs of businesses and industries while ensuring that their activities do not harm the environment or compromise the health and well-being of local communities.</p>
<p>To achieve this goal, environmental policy may include a range of strategies such as pollution prevention, conservation of natural resources, and promotion of renewable energy. These strategies may be implemented through various mechanisms such as tax incentives, regulations, and voluntary initiatives.</p>
<p>One example of an effective environmental policy is the Clean Air Act in the United States. This law was enacted in 1970 and has since been updated several times to regulate emissions from various sources such as factories, power plants, and transportation. As a result, air quality has improved significantly in many parts of the country, reducing the risk of respiratory diseases and other health problems.</p>
<p>Another example is the Paris Agreement on climate change, which was adopted in 2015 by nearly 200 countries. The agreement aims to limit global warming to well below 2 degrees Celsius above pre-industrial levels and to pursue efforts to limit the temperature increase to 1.5 degrees Celsius. To achieve this goal, countries have committed to reducing their greenhouse gas emissions and transitioning to a low-carbon economy.</p>', N'what-is-ci-cms', 1, CAST(N'2023-04-12T15:02:28.8053223+05:30' AS DateTimeOffset), CAST(N'2023-04-12T17:22:52.7784882+05:30' AS DateTimeOffset), NULL)
INSERT [dbo].[cms_page] ([cms_page_id], [title], [description], [slug], [status], [created_at], [updated_at], [deleted_at]) VALUES (4, N'CSI Platform', N'<p>A community investment platform is a digital platform that allows individuals and organizations to invest in local projects and businesses. These platforms serve as a bridge between investors and the community by providing a platform for people to pool their resources and support local initiatives.</p>
<p>The main goal of a community investment platform is to create a sustainable local economy by providing access to capital for small businesses and startups. This type of platform is often used by local communities to fund projects such as community gardens, affordable housing, and renewable energy initiatives.</p>
<p>One of the key benefits of a community investment platform is that it allows people to invest in causes they care about. Investors can choose to support local businesses, social enterprises, or environmental initiatives, among others. This type of investment is often seen as more meaningful than investing in large corporations or traditional financial instruments.</p>
<p>Another advantage of a community investment platform is that it can help to democratize access to capital. Traditional investment channels often favor wealthy individuals and institutional investors, making it difficult for small businesses and startups to access funding. By leveraging the power of the crowd, community investment platforms can help to level the playing field and provide opportunities for underrepresented groups.</p>', N'what-is-ci-platform', 0, CAST(N'2023-04-12T15:07:14.8952365+05:30' AS DateTimeOffset), CAST(N'2023-04-19T10:48:04.4077886+05:30' AS DateTimeOffset), NULL)
INSERT [dbo].[cms_page] ([cms_page_id], [title], [description], [slug], [status], [created_at], [updated_at], [deleted_at]) VALUES (5, N'Environment Policy CI', N'<p>Change: Environmental policy refers to the set of laws, regulations, and guidelines that govern the management and protection of natural resources and the environment. The primary goal of environmental policy is to minimize the negative impact of human activities on the environment while promoting sustainable development.</p>
<p>One of the main challenges facing environmental policy is balancing economic growth with environmental protection. Environmental policies must take into account the needs of businesses and industries while ensuring that their activities do not harm the environment or compromise the health and well-being of local communities.</p>
<p>To achieve this goal, environmental policy may include a range of strategies such as pollution prevention, conservation of natural resources, and promotion of renewable energy. These strategies may be implemented through various mechanisms such as tax incentives, regulations, and voluntary initiatives.</p>
<p>One example of an effective environmental policy is the Clean Air Act in the United States. This law was enacted in 1970 and has since been updated several times to regulate emissions from various sources such as factories, power plants, and transportation. As a result, air quality has improved significantly in many parts of the country, reducing the risk of respiratory diseases and other health problems.</p>
<p>Another example is the Paris Agreement on climate change, which was adopted in 2015 by nearly 200 countries. The agreement aims to limit global warming to well below 2 degrees Celsius above pre-industrial levels and to pursue efforts to limit the temperature increase to 1.5 degrees Celsius. To achieve this goal, countries have committed to reducing their greenhouse gas emissions and transitioning to a low-carbon economy.</p>', N'ci-environment-policy', 1, CAST(N'2023-04-12T15:49:33.8495029+05:30' AS DateTimeOffset), CAST(N'2023-04-12T16:24:57.7892457+05:30' AS DateTimeOffset), NULL)
INSERT [dbo].[cms_page] ([cms_page_id], [title], [description], [slug], [status], [created_at], [updated_at], [deleted_at]) VALUES (9, N'random text', N'<p>ranodnfsj sfjdsljf slfjsf sjf</p>', N'random-url', 1, CAST(N'2023-04-19T16:39:20.7913959+05:30' AS DateTimeOffset), NULL, NULL)
SET IDENTITY_INSERT [dbo].[cms_page] OFF
GO
SET IDENTITY_INSERT [dbo].[comment] ON 

INSERT [dbo].[comment] ([comment_id], [user_id], [mission_id], [approval_status], [created_at], [updated_at], [deleted_at], [comment_text]) VALUES (1, 4, 12, 1, CAST(N'2023-03-21T11:16:04.5766667+00:00' AS DateTimeOffset), NULL, NULL, N'This is the best mission I came across')
INSERT [dbo].[comment] ([comment_id], [user_id], [mission_id], [approval_status], [created_at], [updated_at], [deleted_at], [comment_text]) VALUES (2, 2, 12, 1, CAST(N'2023-03-21T11:16:04.5766667+00:00' AS DateTimeOffset), NULL, NULL, N'Nice Mission')
INSERT [dbo].[comment] ([comment_id], [user_id], [mission_id], [approval_status], [created_at], [updated_at], [deleted_at], [comment_text]) VALUES (3, 3, 12, 1, CAST(N'2023-03-21T11:16:04.5766667+00:00' AS DateTimeOffset), NULL, NULL, N'Great Experience')
INSERT [dbo].[comment] ([comment_id], [user_id], [mission_id], [approval_status], [created_at], [updated_at], [deleted_at], [comment_text]) VALUES (4, 4, 4, 0, CAST(N'2023-03-21T16:24:28.2864995+05:30' AS DateTimeOffset), NULL, NULL, N'Great mission by *NGO*')
INSERT [dbo].[comment] ([comment_id], [user_id], [mission_id], [approval_status], [created_at], [updated_at], [deleted_at], [comment_text]) VALUES (5, 4, 4, 0, CAST(N'2023-03-21T21:19:05.0801804+05:30' AS DateTimeOffset), NULL, NULL, N'another great opportunity')
INSERT [dbo].[comment] ([comment_id], [user_id], [mission_id], [approval_status], [created_at], [updated_at], [deleted_at], [comment_text]) VALUES (6, 4, 8, 0, CAST(N'2023-03-27T09:46:48.1414902+05:30' AS DateTimeOffset), NULL, NULL, N'hello there')
SET IDENTITY_INSERT [dbo].[comment] OFF
GO
SET IDENTITY_INSERT [dbo].[contact_us] ON 

INSERT [dbo].[contact_us] ([contact_id], [user_id], [subject], [message], [created_at], [updated_at], [status], [response], [deleted_at], [is_deleted]) VALUES (10014, 4, N'How missions are going?', N'This is sample message, read it and reply!', CAST(N'2023-04-19T11:27:18.3268340+05:30' AS DateTimeOffset), NULL, 1, N'Fire as hell! you should join as well!', NULL, 0)
INSERT [dbo].[contact_us] ([contact_id], [user_id], [subject], [message], [created_at], [updated_at], [status], [response], [deleted_at], [is_deleted]) VALUES (10016, 15, N'Process for volunteer registration', N'Process for volunteer registration, what to do', CAST(N'2023-04-19T11:31:59.4916985+05:30' AS DateTimeOffset), NULL, 1, N'Timepass response', NULL, 0)
INSERT [dbo].[contact_us] ([contact_id], [user_id], [subject], [message], [created_at], [updated_at], [status], [response], [deleted_at], [is_deleted]) VALUES (10017, 16, N'How to post mission story?', N'Process to post mission story in platform.', CAST(N'2023-04-19T11:33:04.2881467+05:30' AS DateTimeOffset), NULL, 1, N'You can post story after selected as volunteer in any mission. Have fun!', NULL, 0)
INSERT [dbo].[contact_us] ([contact_id], [user_id], [subject], [message], [created_at], [updated_at], [status], [response], [deleted_at], [is_deleted]) VALUES (10018, 9, N'How to activate user account?', N'Necessary steps to activate user account.', CAST(N'2023-04-19T11:35:06.1948335+05:30' AS DateTimeOffset), NULL, 1, N'ljfsd jflksd fjskfkls', NULL, 0)
SET IDENTITY_INSERT [dbo].[contact_us] OFF
GO
SET IDENTITY_INSERT [dbo].[country] ON 

INSERT [dbo].[country] ([country_id], [name], [ISO], [created_at], [updated_at], [deleted_at]) VALUES (1, N'India', N'IN', CAST(N'2023-03-05T21:04:55.2433333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[country] ([country_id], [name], [ISO], [created_at], [updated_at], [deleted_at]) VALUES (2, N'Australia', N'AU', CAST(N'2023-03-05T21:04:55.2433333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[country] ([country_id], [name], [ISO], [created_at], [updated_at], [deleted_at]) VALUES (3, N'Newzealand', N'NZ', CAST(N'2023-03-05T21:04:55.2433333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[country] ([country_id], [name], [ISO], [created_at], [updated_at], [deleted_at]) VALUES (4, N'Japan', N'JP', CAST(N'2023-03-05T21:04:55.2433333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[country] ([country_id], [name], [ISO], [created_at], [updated_at], [deleted_at]) VALUES (5, N'America', N'USA', CAST(N'2023-03-05T21:04:55.2433333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[country] ([country_id], [name], [ISO], [created_at], [updated_at], [deleted_at]) VALUES (6, N'Finland', N'FI', CAST(N'2023-03-05T21:04:55.2433333+00:00' AS DateTimeOffset), NULL, NULL)
SET IDENTITY_INSERT [dbo].[country] OFF
GO
SET IDENTITY_INSERT [dbo].[favourite_mission] ON 

INSERT [dbo].[favourite_mission] ([favourite_mission_id], [mission_id], [user_id], [created_at], [updated_at], [deleted_at]) VALUES (3, 10, 3, CAST(N'2023-03-06T12:03:00.9866667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[favourite_mission] ([favourite_mission_id], [mission_id], [user_id], [created_at], [updated_at], [deleted_at]) VALUES (4, 4, 3, CAST(N'2023-03-06T12:03:00.9866667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[favourite_mission] ([favourite_mission_id], [mission_id], [user_id], [created_at], [updated_at], [deleted_at]) VALUES (5, 3, 2, CAST(N'2023-03-06T12:03:00.9866667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[favourite_mission] ([favourite_mission_id], [mission_id], [user_id], [created_at], [updated_at], [deleted_at]) VALUES (19, 5, 4, CAST(N'2023-03-21T12:45:43.0446579+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[favourite_mission] ([favourite_mission_id], [mission_id], [user_id], [created_at], [updated_at], [deleted_at]) VALUES (22, 3, 4, CAST(N'2023-03-27T09:45:14.7101807+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[favourite_mission] ([favourite_mission_id], [mission_id], [user_id], [created_at], [updated_at], [deleted_at]) VALUES (23, 6, 4, CAST(N'2023-04-02T16:40:32.5179504+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[favourite_mission] ([favourite_mission_id], [mission_id], [user_id], [created_at], [updated_at], [deleted_at]) VALUES (25, 8, 12, CAST(N'2023-04-02T16:42:45.8924288+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[favourite_mission] ([favourite_mission_id], [mission_id], [user_id], [created_at], [updated_at], [deleted_at]) VALUES (10021, 3, 15, CAST(N'2023-04-17T15:33:23.1056420+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[favourite_mission] ([favourite_mission_id], [mission_id], [user_id], [created_at], [updated_at], [deleted_at]) VALUES (10022, 12, 16, CAST(N'2023-04-17T21:28:24.0437562+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[favourite_mission] ([favourite_mission_id], [mission_id], [user_id], [created_at], [updated_at], [deleted_at]) VALUES (10023, 8, 4, CAST(N'2023-04-18T11:26:11.9098954+05:30' AS DateTimeOffset), NULL, NULL)
SET IDENTITY_INSERT [dbo].[favourite_mission] OFF
GO
SET IDENTITY_INSERT [dbo].[goal_mission] ON 

INSERT [dbo].[goal_mission] ([goal_mission_id], [mission_id], [goal_objective_text], [goal_value], [goal_achived], [created_at], [updated_at], [deleted_at]) VALUES (1, 4, N'Plant 10,000 Trees', 10000, 0, CAST(N'2023-03-06T12:17:47.6566667+00:00' AS DateTimeOffset), CAST(N'2023-04-26T15:04:36.0729505+05:30' AS DateTimeOffset), NULL)
INSERT [dbo].[goal_mission] ([goal_mission_id], [mission_id], [goal_objective_text], [goal_value], [goal_achived], [created_at], [updated_at], [deleted_at]) VALUES (2, 7, N'Collect $ 50K', 50000, 30000, CAST(N'2023-03-06T12:17:47.6566667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[goal_mission] ([goal_mission_id], [mission_id], [goal_objective_text], [goal_value], [goal_achived], [created_at], [updated_at], [deleted_at]) VALUES (3, 8, N'Solar Plant 1000 Houses', 1000, 0, CAST(N'2023-03-06T12:17:47.6566667+00:00' AS DateTimeOffset), CAST(N'2023-04-26T14:36:19.1030852+05:30' AS DateTimeOffset), NULL)
INSERT [dbo].[goal_mission] ([goal_mission_id], [mission_id], [goal_objective_text], [goal_value], [goal_achived], [created_at], [updated_at], [deleted_at]) VALUES (4, 10, N'5 Cultural Program', 5, 0, CAST(N'2023-03-06T12:17:47.6566667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[goal_mission] ([goal_mission_id], [mission_id], [goal_objective_text], [goal_value], [goal_achived], [created_at], [updated_at], [deleted_at]) VALUES (7, 19, N'Plant 12K Solar Plant', 12000, 0, CAST(N'2023-04-25T11:07:40.5669813+05:30' AS DateTimeOffset), NULL, NULL)
SET IDENTITY_INSERT [dbo].[goal_mission] OFF
GO
SET IDENTITY_INSERT [dbo].[mission] ON 

INSERT [dbo].[mission] ([mission_id], [theme_id], [country_id], [title], [short_description], [start_date], [end_date], [mission_type], [status], [organization_name], [organization_detail], [availability], [total_seat], [registration_deadline], [created_at], [updated_at], [deleted_at], [description], [rating], [city_id], [is_active]) VALUES (3, 2, 1, N'Let''s save the environment', N'It''s our duty to keep our environment clean.', CAST(N'2023-03-10T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-05-20T00:00:00.0000000+05:30' AS DateTimeOffset), 1, 1, N'NGO', N'<p>organization details</p>', 1, 1000, CAST(N'2023-05-10T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-03-03T18:00:45.3633333+00:00' AS DateTimeOffset), NULL, NULL, N'<p>The environment is facing numerous challenges today, such as climate change, pollution, and loss of biodiversity. It is critical that we take action to address these issues and save the environment for future generations. One of the most effective ways to save the environment is by reducing our carbon footprint. This can be done by reducing energy consumption, promoting renewable energy, using public transportation or carpooling, and reducing waste. By making small changes in our daily lives, such as turning off lights when we leave a room or using reusable bags instead of plastic ones, we can all contribute to a healthier planet. Another way to save the environment is by conserving water. This can be done by taking shorter showers, fixing leaky faucets, and using water-efficient appliances. Water is a precious resource, and we must use it wisely to ensure that we have enough for future generations. We also need to protect our natural resources, such as forests, oceans, and wildlife. This can be done by supporting conservation efforts and reducing our impact on these ecosystems. For example, we can reduce our use of single-use plastics, avoid products made from unsustainable materials, and support sustainable agriculture and fishing practices.</p>', 0, 2, 1)
INSERT [dbo].[mission] ([mission_id], [theme_id], [country_id], [title], [short_description], [start_date], [end_date], [mission_type], [status], [organization_name], [organization_detail], [availability], [total_seat], [registration_deadline], [created_at], [updated_at], [deleted_at], [description], [rating], [city_id], [is_active]) VALUES (4, 4, 4, N'God''s blessing as education', N'Education is the best way to separate ourselves from beast!', CAST(N'2023-03-08T00:00:00.0000000+05:30' AS DateTimeOffset), NULL, 0, 1, N'NGO', N'<p>organization details</p>', 1, 500, CAST(N'2023-05-10T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-03-03T18:00:45.3633333+00:00' AS DateTimeOffset), CAST(N'2023-04-03T14:35:09.7243975+05:30' AS DateTimeOffset), NULL, N'<p>Education is often considered a blessing because it can open doors and provide opportunities for individuals to achieve their full potential. With education, people are empowered to make informed decisions, pursue their passions and contribute to their communities in meaningful ways. Education is a powerful tool that can help break the cycle of poverty and improve the quality of life for individuals and their families. It can provide access to better-paying jobs, healthcare, and social services. Education also promotes critical thinking, creativity, and problem-solving skills, which are essential in today''s fast-paced and ever-changing world. In addition to its practical benefits, education can also have a transformative impact on individuals'' personal and spiritual growth. Education can help individuals gain a deeper understanding of themselves, others, and the world around them. It can promote empathy, compassion, and a sense of social responsibility, which are important qualities for building strong and inclusive communities.</p>', 0, 10, 1)
INSERT [dbo].[mission] ([mission_id], [theme_id], [country_id], [title], [short_description], [start_date], [end_date], [mission_type], [status], [organization_name], [organization_detail], [availability], [total_seat], [registration_deadline], [created_at], [updated_at], [deleted_at], [description], [rating], [city_id], [is_active]) VALUES (5, 3, 1, N'Agriculture Evolution', N'Agriculture for better new world', CAST(N'2023-04-15T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-06-09T00:00:00.0000000+05:30' AS DateTimeOffset), 1, 1, N'NGO', N'organization details', 2, 300, CAST(N'2023-05-10T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-03-03T18:00:45.3633333+00:00' AS DateTimeOffset), NULL, NULL, N'Agriculture has come a long way since its earliest origins, with advancements in technology, science, and social organization driving its evolution over time. Here are some key milestones in the evolution of agriculture:

    Domestication: One of the earliest developments in agriculture was the domestication of plants and animals. This allowed humans to settle in one place and cultivate crops for food and other uses, leading to the development of farming.

    Irrigation: The development of irrigation techniques allowed farmers to cultivate crops in areas with little rainfall, leading to the establishment of large-scale agriculture in regions like Mesopotamia and Egypt.

    Crop rotation: The practice of rotating crops in different fields helped to maintain soil fertility and reduce the risk of disease and pests, leading to increased yields and more sustainable farming practices.

    Machinery: The invention of the plow and other agricultural machinery, such as tractors and combines, revolutionized agriculture by increasing efficiency and productivity.', 0, 2, 1)
INSERT [dbo].[mission] ([mission_id], [theme_id], [country_id], [title], [short_description], [start_date], [end_date], [mission_type], [status], [organization_name], [organization_detail], [availability], [total_seat], [registration_deadline], [created_at], [updated_at], [deleted_at], [description], [rating], [city_id], [is_active]) VALUES (6, 2, 3, N'Let''s show some charity work', N'Organizing charity event and have blessing', CAST(N'2023-02-20T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-11-11T00:00:00.0000000+05:30' AS DateTimeOffset), 1, 1, N'NGO-s', N'organization details', 2, 100, NULL, CAST(N'2023-03-05T20:46:37.2166667+00:00' AS DateTimeOffset), NULL, NULL, N'A charity event is a gathering or activity that is organized to raise money, awareness, or support for a charitable cause or organization. Charity events can take many forms, from fundraising galas and benefit concerts to marathons and charity auctions.

Organizing a successful charity event involves careful planning and execution, and often requires the help of volunteers, sponsors, and donors. Here are some key steps to consider when planning a charity event:

    Set a goal: Determine what you hope to accomplish with your event, whether it is raising a certain amount of money, increasing awareness for a cause, or recruiting volunteers.

    Choose a cause: Select a cause or organization that aligns with your goals and resonates with your audience. Consider the impact of the cause, the urgency of the need, and the reach of the organization.', 0, 8, 1)
INSERT [dbo].[mission] ([mission_id], [theme_id], [country_id], [title], [short_description], [start_date], [end_date], [mission_type], [status], [organization_name], [organization_detail], [availability], [total_seat], [registration_deadline], [created_at], [updated_at], [deleted_at], [description], [rating], [city_id], [is_active]) VALUES (7, 3, 4, N'Social services for better society', N'Play our duty faithfully that will eventually improve the surrounding', CAST(N'2023-01-24T00:00:00.0000000+05:30' AS DateTimeOffset), NULL, 0, 1, N'NGO-g', N'organization details', 2, 20, CAST(N'2023-07-08T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-03-05T20:46:37.2166667+00:00' AS DateTimeOffset), NULL, NULL, N'Social services refer to a range of programs and activities provided by governments, non-profit organizations, and community groups to support individuals and families in need. Social services aim to improve people''s quality of life, promote social justice, and address issues related to poverty, health, education, housing, and employment.

Here are some examples of social services:

    Social welfare programs: These programs provide financial assistance to individuals and families in need, such as Temporary Assistance for Needy Families (TANF), Supplemental Nutrition Assistance Program (SNAP), and Medicaid.

    Healthcare services: Social services also include healthcare services like community clinics, health education programs, and mental health services.

    Housing assistance: Social services provide housing assistance through programs like Section 8 Housing Vouchers, homeless shelters, and transitional housing programs.

    Education services: Social services provide education services through programs like Head Start, after-school programs, and adult education programs.', 0, 12, 1)
INSERT [dbo].[mission] ([mission_id], [theme_id], [country_id], [title], [short_description], [start_date], [end_date], [mission_type], [status], [organization_name], [organization_detail], [availability], [total_seat], [registration_deadline], [created_at], [updated_at], [deleted_at], [description], [rating], [city_id], [is_active]) VALUES (8, 2, 4, N'Health is wealth', N'Human health is considered as treasure, a health mind in healthy body.', CAST(N'2023-06-11T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-03-10T00:00:00.0000000+05:30' AS DateTimeOffset), 0, 1, N'NGO-r', N'<p>organization details</p>', 1, 50, CAST(N'2023-03-05T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-03-05T20:46:37.2166667+00:00' AS DateTimeOffset), CAST(N'2023-04-18T11:27:25.6656048+05:30' AS DateTimeOffset), NULL, N'<p>Health refers to a state of physical, mental, and social well-being, and not just the absence of disease or illness. Achieving and maintaining good health is important for living a fulfilling life and requires a balanced approach to physical, emotional, and social well-being. Here are some key factors that contribute to good health: Nutrition: Eating a balanced diet that is rich in nutrients and low in processed foods is important for maintaining a healthy body weight, preventing chronic diseases, and promoting overall health. Physical activity: Regular exercise is essential for maintaining good cardiovascular health, building strength and endurance, and preventing chronic diseases like diabetes and obesity. Sleep: Getting enough sleep is crucial for maintaining good mental health, supporting immune function, and reducing the risk of chronic diseases. Mental health: Good mental health involves managing stress, practicing self-care, and seeking support when needed. Mental health is just as important as physical health and should be prioritized.</p>', 4, 12, 1)
INSERT [dbo].[mission] ([mission_id], [theme_id], [country_id], [title], [short_description], [start_date], [end_date], [mission_type], [status], [organization_name], [organization_detail], [availability], [total_seat], [registration_deadline], [created_at], [updated_at], [deleted_at], [description], [rating], [city_id], [is_active]) VALUES (9, 5, 4, N'Arts and Culture', N'Arts and culture is considered as heart of any nation.', CAST(N'2023-03-10T00:00:00.0000000+05:30' AS DateTimeOffset), NULL, 1, 1, N'NGO-b', N'organization details', 2, 700, CAST(N'2023-03-05T20:46:37.2166667+00:00' AS DateTimeOffset), CAST(N'2023-03-05T20:46:37.2166667+00:00' AS DateTimeOffset), NULL, NULL, N'description values', 0, 12, 1)
INSERT [dbo].[mission] ([mission_id], [theme_id], [country_id], [title], [short_description], [start_date], [end_date], [mission_type], [status], [organization_name], [organization_detail], [availability], [total_seat], [registration_deadline], [created_at], [updated_at], [deleted_at], [description], [rating], [city_id], [is_active]) VALUES (10, 7, 4, N'Mission Title 2 - Education purpose', N'short description', CAST(N'2023-03-10T00:00:00.0000000+05:30' AS DateTimeOffset), NULL, 0, 1, N'NGO-r', N'organization details', 2, 30, NULL, CAST(N'2023-03-05T20:46:37.2166667+00:00' AS DateTimeOffset), NULL, NULL, N'description values', 0, 12, 1)
INSERT [dbo].[mission] ([mission_id], [theme_id], [country_id], [title], [short_description], [start_date], [end_date], [mission_type], [status], [organization_name], [organization_detail], [availability], [total_seat], [registration_deadline], [created_at], [updated_at], [deleted_at], [description], [rating], [city_id], [is_active]) VALUES (11, 6, 4, N'Mission Title 7 - Education purpose', N'short description', NULL, NULL, 1, 1, N'NGO-z', N'organization details', 2, 25, NULL, CAST(N'2023-03-05T20:46:37.2166667+00:00' AS DateTimeOffset), NULL, NULL, N'description values', 0, 12, 1)
INSERT [dbo].[mission] ([mission_id], [theme_id], [country_id], [title], [short_description], [start_date], [end_date], [mission_type], [status], [organization_name], [organization_detail], [availability], [total_seat], [registration_deadline], [created_at], [updated_at], [deleted_at], [description], [rating], [city_id], [is_active]) VALUES (12, 4, 3, N'Mission Title - 10', N'lorem jlsdjflsdj jfslj fjsdlfjsldjfkls sdjfskjfs sdjfsdjfsd sdjfsdjs fsdfjsdljsl fsdlkfjslfsjfsdjl sfjsljfsjsdjlfjslfsjfsldjfsdllskjfsjfljfsa ljlfsjsdfljfsien jn22j2idssjn', NULL, NULL, 1, 0, N'FinSky', N'org detail', 1, NULL, CAST(N'2023-05-10T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-03-09T14:14:41.5566667+00:00' AS DateTimeOffset), NULL, NULL, N'description', 0, 2, 1)
INSERT [dbo].[mission] ([mission_id], [theme_id], [country_id], [title], [short_description], [start_date], [end_date], [mission_type], [status], [organization_name], [organization_detail], [availability], [total_seat], [registration_deadline], [created_at], [updated_at], [deleted_at], [description], [rating], [city_id], [is_active]) VALUES (14, 2, 1, N'Building Bridges: Connecting Communities', N'We need to create bridges between community for better environment.', CAST(N'2023-04-24T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-06-30T00:00:00.0000000+05:30' AS DateTimeOffset), 1, 1, N'Sun Pirates', N'<div class="flex flex-grow flex-col gap-3">
<div class="min-h-[20px] flex flex-col items-start gap-4 whitespace-pre-wrap">
<div class="markdown prose w-full break-words dark:prose-invert light">
<p>Building Bridges: Connecting Communities is a great title for a project that aims to foster greater understanding, communication, and cooperation between different groups of people. The concept of building bridges is a powerful metaphor for bringing people together, bridging divides, and creating new connections.</p>
<p>In this project, the focus could be on bringing together diverse communities and promoting cultural exchange, mutual learning, and collaborative problem-solving. The project could involve a variety of activities such as cultural festivals, language exchange programs, community service projects, and social events.</p>
<p>The goal of Building Bridges: Connecting Communities is to create a more harmonious, inclusive, and resilient society where people from all backgrounds can work together to tackle common challenges and create a better future for everyone. This project can have a significant impact in promoting social cohesion, reducing prejudice and discrimination, and building stronger, more vibrant communities.</p>
</div>
</div>
</div>', 3, NULL, NULL, CAST(N'2023-04-24T17:41:20.2113076+05:30' AS DateTimeOffset), NULL, NULL, N'<div class="flex flex-grow flex-col gap-3">
<div class="min-h-[20px] flex flex-col items-start gap-4 whitespace-pre-wrap">
<div class="markdown prose w-full break-words dark:prose-invert light">
<p>Building Bridges: Connecting Communities is a great title for a project that aims to foster greater understanding, communication, and cooperation between different groups of people. The concept of building bridges is a powerful metaphor for bringing people together, bridging divides, and creating new connections.</p>
<p>In this project, the focus could be on bringing together diverse communities and promoting cultural exchange, mutual learning, and collaborative problem-solving. The project could involve a variety of activities such as cultural festivals, language exchange programs, community service projects, and social events.</p>
<p>The goal of Building Bridges: Connecting Communities is to create a more harmonious, inclusive, and resilient society where people from all backgrounds can work together to tackle common challenges and create a better future for everyone. This project can have a significant impact in promoting social cohesion, reducing prejudice and discrimination, and building stronger, more vibrant communities.</p>
</div>
</div>
</div>', NULL, 2, 1)
INSERT [dbo].[mission] ([mission_id], [theme_id], [country_id], [title], [short_description], [start_date], [end_date], [mission_type], [status], [organization_name], [organization_detail], [availability], [total_seat], [registration_deadline], [created_at], [updated_at], [deleted_at], [description], [rating], [city_id], [is_active]) VALUES (15, 2, 5, N'Space Exploration: Charting New Frontiers', N'Space exploration is the the peak of human race!', CAST(N'2023-04-29T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-11-23T00:00:00.0000000+05:30' AS DateTimeOffset), 1, 0, N'NASA', N'<p>Space exploration has been a fascinating topic for humans for centuries. It represents the ultimate adventure, as we seek to uncover the mysteries of the universe and discover what lies beyond our own planet. With each passing year, our understanding of space grows, and our technological capabilities expand, allowing us to venture further and further into the cosmos.</p>
<p>Charting new frontiers in space exploration is not just about satisfying our curiosity. It also holds tremendous potential for scientific and technological advancements that can benefit humanity in numerous ways. From developing new materials and technologies to improving our understanding of our own planet and the universe, space exploration can pave the way for a better future.</p>
<p>But space exploration is not without its challenges. The harsh environment of space presents numerous obstacles, and the risks involved in manned missions are significant. However, with careful planning, cutting-edge technology, and the right resources, we can overcome these challenges and continue to push the boundaries of what is possible.</p>
<p>As we look to the future of space exploration, we can expect to see continued advancements in both technology and our understanding of the universe. From exploring our own solar system to venturing out into the depths of space, the possibilities are endless. Charting new frontiers in space exploration will require vision, determination, and a commitment to pushing the limits of what we can achieve.</p>', 3, 1000, CAST(N'2023-09-20T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-04-24T17:56:57.1163208+05:30' AS DateTimeOffset), NULL, NULL, N'<p>Space exploration has been a fascinating topic for humans for centuries. It represents the ultimate adventure, as we seek to uncover the mysteries of the universe and discover what lies beyond our own planet. With each passing year, our understanding of space grows, and our technological capabilities expand, allowing us to venture further and further into the cosmos.</p>
<p>Charting new frontiers in space exploration is not just about satisfying our curiosity. It also holds tremendous potential for scientific and technological advancements that can benefit humanity in numerous ways. From developing new materials and technologies to improving our understanding of our own planet and the universe, space exploration can pave the way for a better future.</p>
<p>But space exploration is not without its challenges. The harsh environment of space presents numerous obstacles, and the risks involved in manned missions are significant. However, with careful planning, cutting-edge technology, and the right resources, we can overcome these challenges and continue to push the boundaries of what is possible.</p>
<p>As we look to the future of space exploration, we can expect to see continued advancements in both technology and our understanding of the universe. From exploring our own solar system to venturing out into the depths of space, the possibilities are endless. Charting new frontiers in space exploration will require vision, determination, and a commitment to pushing the limits of what we can achieve.</p>', NULL, 14, 1)
INSERT [dbo].[mission] ([mission_id], [theme_id], [country_id], [title], [short_description], [start_date], [end_date], [mission_type], [status], [organization_name], [organization_detail], [availability], [total_seat], [registration_deadline], [created_at], [updated_at], [deleted_at], [description], [rating], [city_id], [is_active]) VALUES (16, 2, 4, N'Ocean exploration', N'Ocean Exploration: Develop technology to better explore and understand the ocean, including mapping uncharted areas and studying marine life and ecosystems.', CAST(N'2023-04-28T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-09-28T00:00:00.0000000+05:30' AS DateTimeOffset), 1, 1, N'Thunder Org.', N'<p>Ocean exploration is the study of the ocean and its inhabitants. It involves investigating the ocean''s depths and learning about the organisms that live there, as well as the geological and environmental features that shape the ocean floor. Ocean exploration can help us understand how the ocean functions and how we can better manage its resources.</p>
<p>One of the most exciting aspects of ocean exploration is the discovery of new species and ecosystems. Scientists have found deep-sea creatures that are unlike anything seen on land or in shallow waters. They have also discovered hydrothermal vents, which are openings in the ocean floor where hot water and minerals spew out. These vents support unique communities of organisms that can survive in the extreme conditions.</p>
<p>In addition to scientific research, ocean exploration has practical applications. For example, it can help us find new sources of food and energy, such as deep-sea fish and oil reserves. It can also aid in the search for missing aircraft and ships, and help us understand and predict natural disasters such as hurricanes and tsunamis.</p>
<p>However, ocean exploration also poses many challenges. The deep sea is difficult and expensive to access, and the pressure and darkness at those depths can be dangerous for humans and equipment. It is important to balance the benefits of ocean exploration with the potential risks and impacts on the ocean environment.</p>
<p>Despite the challenges, ocean exploration remains an important field of study with much to discover and learn. With advances in technology and a growing interest in preserving our planet''s natural resources, we can continue to explore and understand the ocean and its many mysteries.</p>', 4, NULL, NULL, CAST(N'2023-04-24T18:10:06.8451092+05:30' AS DateTimeOffset), NULL, NULL, N'<p>Ocean exploration is the study of the ocean and its inhabitants. It involves investigating the ocean''s depths and learning about the organisms that live there, as well as the geological and environmental features that shape the ocean floor. Ocean exploration can help us understand how the ocean functions and how we can better manage its resources.</p>
<p>One of the most exciting aspects of ocean exploration is the discovery of new species and ecosystems. Scientists have found deep-sea creatures that are unlike anything seen on land or in shallow waters. They have also discovered hydrothermal vents, which are openings in the ocean floor where hot water and minerals spew out. These vents support unique communities of organisms that can survive in the extreme conditions.</p>
<p>In addition to scientific research, ocean exploration has practical applications. For example, it can help us find new sources of food and energy, such as deep-sea fish and oil reserves. It can also aid in the search for missing aircraft and ships, and help us understand and predict natural disasters such as hurricanes and tsunamis.</p>
<p>However, ocean exploration also poses many challenges. The deep sea is difficult and expensive to access, and the pressure and darkness at those depths can be dangerous for humans and equipment. It is important to balance the benefits of ocean exploration with the potential risks and impacts on the ocean environment.</p>
<p>Despite the challenges, ocean exploration remains an important field of study with much to discover and learn. With advances in technology and a growing interest in preserving our planet''s natural resources, we can continue to explore and understand the ocean and its many mysteries.</p>', NULL, 11, 1)
INSERT [dbo].[mission] ([mission_id], [theme_id], [country_id], [title], [short_description], [start_date], [end_date], [mission_type], [status], [organization_name], [organization_detail], [availability], [total_seat], [registration_deadline], [created_at], [updated_at], [deleted_at], [description], [rating], [city_id], [is_active]) VALUES (19, 2, 1, N'Solar Energy Plant', N'Let''s use the natural light of sun for creating energy in day to day use.', CAST(N'2023-05-01T00:00:00.0000000+05:30' AS DateTimeOffset), NULL, 0, NULL, N'PROINSO', N'<h3>About PROINSO</h3>
<p>&nbsp;</p>
<p class="p1">PROINSO is a global leader in the solar energy market, with business units focused on global equipment supply services and project development. We offer a full-service portfolio for solar projects of all scales that includes procurement, engineering, project development, finance, and construction services.</p>
<p class="p1">Facts and figures:</p>
<ul class="ul1">
<li class="li2">Founded: 2006</li>
<li class="li2">Global presence: sales offices in over 20 countries</li>
<li class="li2">Supplied over 3.5 GW of projects</li>
<li class="li2">Board has experience of developing over 9 GW of solar plant projects</li>
<li class="li2">Won numerous awards: in recent years these include Asia Power Award 2021, 2019 &amp; 2018, Solar Portal Award 2018 and Queen&rsquo;s Award for Enterprise for International Trade in 2018</li>
</ul>', 4, NULL, NULL, CAST(N'2023-04-25T11:07:39.3094742+05:30' AS DateTimeOffset), NULL, NULL, N'<div class="bg-box padd30 wpb_column vc_column_container vc_col-sm-6">
<div class="vc_column-inner">
<div class="wpb_wrapper">
<div class="feature-box-small-icon ">
<div class="text">
<p class="p1">We aim to be at the forefront of the energy revolution. PROINSO&rsquo;s guiding principle is to expand decentralised energy, and increase access to it, through:</p>
<ul class="ul1">
<li class="li2">Highest return on investment over the lifecycle of the project</li>
<li class="li2">Unrivalled global reach in our sector</li>
<li class="li2">Localised capabilities</li>
<li class="li2">Entrepreneurial spirit coupled with rigorous risk management</li>
</ul>
<p>&nbsp;</p>
</div>
</div>
</div>
</div>
</div>', NULL, 2, 1)
INSERT [dbo].[mission] ([mission_id], [theme_id], [country_id], [title], [short_description], [start_date], [end_date], [mission_type], [status], [organization_name], [organization_detail], [availability], [total_seat], [registration_deadline], [created_at], [updated_at], [deleted_at], [description], [rating], [city_id], [is_active]) VALUES (20, 2, 2, N'jflkdsj jklsfj ', N'jldksfjsl jkldsjfsjlk sdf', CAST(N'2023-12-31T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-05-02T00:00:00.0000000+05:30' AS DateTimeOffset), 1, 1, N'sdfjlsfk jlksj fklsj', N'<p>sdjflksfj slkjslkjfd</p>', 3, NULL, NULL, CAST(N'2023-04-25T19:18:50.7610511+05:30' AS DateTimeOffset), NULL, NULL, N'<p>jflkdsjfsjjkl kjsfdk jslkjl</p>', NULL, 6, 1)
SET IDENTITY_INSERT [dbo].[mission] OFF
GO
SET IDENTITY_INSERT [dbo].[mission_application] ON 

INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (1, 3, 3, 1, CAST(N'2023-03-06T11:55:46.8500000+00:00' AS DateTimeOffset), CAST(N'2023-03-06T11:55:46.8500000+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (2, 4, 2, 1, CAST(N'2023-03-06T11:56:35.9666667+00:00' AS DateTimeOffset), CAST(N'2023-03-06T11:56:35.9666667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (3, 5, 3, 1, CAST(N'2023-03-06T11:56:35.9666667+00:00' AS DateTimeOffset), CAST(N'2023-03-06T11:56:35.9666667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (4, 10, 1, 1, CAST(N'2023-03-06T11:56:35.9666667+00:00' AS DateTimeOffset), CAST(N'2023-03-06T11:56:35.9666667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (5, 7, 3, 1, CAST(N'2023-03-06T11:56:35.9666667+00:00' AS DateTimeOffset), CAST(N'2023-03-06T11:56:35.9666667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (6, 8, 1, 1, CAST(N'2023-03-06T11:56:35.9666667+00:00' AS DateTimeOffset), CAST(N'2023-03-06T11:56:35.9666667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (8, 4, 4, 1, CAST(N'2023-03-11T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-03-11T00:00:00.0000000+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (9, 4, 3, 1, CAST(N'2023-03-21T18:43:26.8866667+00:00' AS DateTimeOffset), CAST(N'2023-03-21T18:43:26.8866667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (10, 4, 5, 1, CAST(N'2023-03-21T18:43:26.8866667+00:00' AS DateTimeOffset), CAST(N'2023-03-21T18:43:26.8866667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (11, 4, 6, 1, CAST(N'2023-03-21T18:43:26.8866667+00:00' AS DateTimeOffset), CAST(N'2023-03-21T18:43:26.8866667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (12, 5, 2, 1, CAST(N'2023-03-21T18:43:26.8866667+00:00' AS DateTimeOffset), CAST(N'2023-03-21T18:43:26.8866667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (13, 8, 4, 1, CAST(N'2023-03-21T18:43:26.8866667+00:00' AS DateTimeOffset), CAST(N'2023-03-21T18:43:26.8866667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (14, 12, 4, 1, CAST(N'2023-03-15T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-03-23T00:00:00.0000000+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (15, 5, 4, 1, CAST(N'2023-04-07T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-04-07T00:00:00.0000000+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (16, 3, 4, 1, CAST(N'2023-04-07T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-04-04T00:00:00.0000000+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (17, 3, 12, 1, CAST(N'2023-04-07T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-04-07T00:00:00.0000000+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (18, 4, 12, 1, CAST(N'2023-04-07T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-04-07T00:00:00.0000000+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (19, 8, 12, 1, CAST(N'2023-04-07T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-04-07T00:00:00.0000000+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (20, 11, 12, 1, CAST(N'2023-04-07T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-04-07T00:00:00.0000000+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (24, 8, 4, 1, CAST(N'2023-04-20T00:00:00.0000000+05:30' AS DateTimeOffset), CAST(N'2023-04-20T00:00:00.0000000+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (27, 4, 15, 0, CAST(N'2023-04-21T12:47:00.3790084+05:30' AS DateTimeOffset), CAST(N'2023-04-21T12:47:00.3790030+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (32, 5, 15, 2, CAST(N'2023-04-21T13:06:18.4793265+05:30' AS DateTimeOffset), CAST(N'2023-04-21T13:06:18.4793233+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (33, 6, 15, 0, CAST(N'2023-04-21T13:08:18.2777254+05:30' AS DateTimeOffset), CAST(N'2023-04-21T13:08:18.2777215+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (34, 11, 15, 1, CAST(N'2023-04-21T13:08:32.9407685+05:30' AS DateTimeOffset), CAST(N'2023-04-21T13:08:32.9407659+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (35, 3, 15, 0, CAST(N'2023-04-21T14:31:27.2654169+05:30' AS DateTimeOffset), CAST(N'2023-04-21T14:31:27.2653045+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (36, 7, 15, 0, CAST(N'2023-04-21T14:31:47.7675373+05:30' AS DateTimeOffset), CAST(N'2023-04-21T14:31:47.7675352+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (37, 4, 16, 0, CAST(N'2023-04-21T14:42:25.3435844+05:30' AS DateTimeOffset), CAST(N'2023-04-21T14:42:25.3434703+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (38, 6, 16, 0, CAST(N'2023-04-21T14:42:43.0204673+05:30' AS DateTimeOffset), CAST(N'2023-04-21T14:42:43.0204652+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (39, 10, 16, 0, CAST(N'2023-04-21T14:42:55.4120027+05:30' AS DateTimeOffset), CAST(N'2023-04-21T14:42:55.4119978+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (40, 3, 9, 0, CAST(N'2023-04-21T14:46:00.4310244+05:30' AS DateTimeOffset), CAST(N'2023-04-21T14:46:00.4310224+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (41, 6, 9, 0, CAST(N'2023-04-21T14:46:14.1166441+05:30' AS DateTimeOffset), CAST(N'2023-04-21T14:46:14.1166423+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_application] ([mission_application_id], [mission_id], [user_id], [approval_status], [applied_at], [created_at], [updated_at], [deleted_at]) VALUES (42, 11, 9, 0, CAST(N'2023-04-21T14:46:25.7044536+05:30' AS DateTimeOffset), CAST(N'2023-04-21T14:46:25.7044517+05:30' AS DateTimeOffset), NULL, NULL)
SET IDENTITY_INSERT [dbo].[mission_application] OFF
GO
SET IDENTITY_INSERT [dbo].[mission_document] ON 

INSERT [dbo].[mission_document] ([mission_document_id], [mission_id], [document_name], [document_type], [document_path], [created_at], [updated_at], [deleted_at], [document_title]) VALUES (1, 4, N'DB', N'.xlsx', N'\documents\', CAST(N'2023-03-23T11:22:58.1166667+00:00' AS DateTimeOffset), NULL, NULL, N'DB')
INSERT [dbo].[mission_document] ([mission_document_id], [mission_id], [document_name], [document_type], [document_path], [created_at], [updated_at], [deleted_at], [document_title]) VALUES (2, 4, N'lorem-ipsum', N'.docx', N'\documents\', CAST(N'2023-03-23T11:22:58.1166667+00:00' AS DateTimeOffset), NULL, NULL, N'Lorem')
INSERT [dbo].[mission_document] ([mission_document_id], [mission_id], [document_name], [document_type], [document_path], [created_at], [updated_at], [deleted_at], [document_title]) VALUES (3, 4, N'lorem-pdf', N'.pdf', N'\documents\', CAST(N'2023-03-23T11:22:58.1166667+00:00' AS DateTimeOffset), NULL, NULL, N'Lorem')
INSERT [dbo].[mission_document] ([mission_document_id], [mission_id], [document_name], [document_type], [document_path], [created_at], [updated_at], [deleted_at], [document_title]) VALUES (5, 20, N'747f9531-c7b9-4dd4-bdc6-1e9f18267181', N'.docx', N'\documents\mission\', CAST(N'2023-04-25T19:18:51.3005623+05:30' AS DateTimeOffset), NULL, NULL, N'G-48_Corporate_Social_Investment')
INSERT [dbo].[mission_document] ([mission_document_id], [mission_id], [document_name], [document_type], [document_path], [created_at], [updated_at], [deleted_at], [document_title]) VALUES (11, 8, N'422b7117-9816-45f4-a776-58965632365e', N'.docx', N'\documents\mission\', CAST(N'2023-04-26T14:36:18.3559556+05:30' AS DateTimeOffset), NULL, NULL, N'Quick Guide-')
INSERT [dbo].[mission_document] ([mission_document_id], [mission_id], [document_name], [document_type], [document_path], [created_at], [updated_at], [deleted_at], [document_title]) VALUES (12, 8, N'd3473762-f871-4314-9a11-c38b6dffcb36', N'.pdf', N'\documents\mission\', CAST(N'2023-04-26T14:36:18.4014485+05:30' AS DateTimeOffset), NULL, NULL, N'Training Plan-2022-23-Plan-Part2-SQL')
INSERT [dbo].[mission_document] ([mission_document_id], [mission_id], [document_name], [document_type], [document_path], [created_at], [updated_at], [deleted_at], [document_title]) VALUES (13, 8, N'0fdd1847-d3cd-4e1b-98f2-7f84135d0021', N'.pdf', N'\documents\mission\', CAST(N'2023-04-26T14:36:18.4016175+05:30' AS DateTimeOffset), NULL, NULL, N'Training Plan-23_Part3-')
INSERT [dbo].[mission_document] ([mission_document_id], [mission_id], [document_name], [document_type], [document_path], [created_at], [updated_at], [deleted_at], [document_title]) VALUES (14, 4, N'33dc143f-91ef-4a1d-94e2-92978297a2f1', N'.docx', N'\documents\mission\', CAST(N'2023-04-26T14:50:32.2598905+05:30' AS DateTimeOffset), NULL, NULL, N'Quick Guide-')
SET IDENTITY_INSERT [dbo].[mission_document] OFF
GO
SET IDENTITY_INSERT [dbo].[mission_invite] ON 

INSERT [dbo].[mission_invite] ([mission_invite_id], [mission_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (3, 4, 4, 7, CAST(N'2023-03-23T10:45:24.8886889+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_invite] ([mission_invite_id], [mission_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (4, 4, 4, 8, CAST(N'2023-03-23T10:45:24.8888042+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_invite] ([mission_invite_id], [mission_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (5, 4, 4, 9, CAST(N'2023-03-23T11:18:06.2826702+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_invite] ([mission_invite_id], [mission_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (6, 3, 4, 7, CAST(N'2023-03-23T13:14:42.3567558+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_invite] ([mission_invite_id], [mission_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (7, 3, 4, 8, CAST(N'2023-03-23T13:14:42.3568883+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_invite] ([mission_invite_id], [mission_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (8, 3, 4, 9, CAST(N'2023-03-23T13:14:42.3568915+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_invite] ([mission_invite_id], [mission_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (9, 8, 4, 7, CAST(N'2023-03-27T09:47:40.1113619+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_invite] ([mission_invite_id], [mission_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (10, 8, 4, 8, CAST(N'2023-03-27T09:47:40.1115831+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_invite] ([mission_invite_id], [mission_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (11, 8, 4, 9, CAST(N'2023-03-27T09:47:40.1115850+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_invite] ([mission_invite_id], [mission_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (10009, 4, 15, 4, CAST(N'2023-04-17T12:30:37.1574092+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_invite] ([mission_invite_id], [mission_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (10010, 4, 15, 7, CAST(N'2023-04-17T12:30:37.1575547+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_invite] ([mission_invite_id], [mission_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (10011, 4, 15, 5, CAST(N'2023-04-17T12:30:37.1575560+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_invite] ([mission_invite_id], [mission_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (10012, 3, 4, 15, CAST(N'2023-04-17T12:38:40.7825579+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_invite] ([mission_invite_id], [mission_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (10013, 12, 16, 4, CAST(N'2023-04-17T21:30:05.0113490+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_invite] ([mission_invite_id], [mission_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (10014, 12, 16, 5, CAST(N'2023-04-17T21:30:05.0114686+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_invite] ([mission_invite_id], [mission_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (10015, 12, 16, 15, CAST(N'2023-04-17T21:30:05.0114700+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_invite] ([mission_invite_id], [mission_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (10016, 12, 16, 11, CAST(N'2023-04-17T21:32:18.9574265+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_invite] ([mission_invite_id], [mission_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (10017, 8, 4, 16, CAST(N'2023-04-18T11:26:41.0082082+05:30' AS DateTimeOffset), NULL, NULL)
SET IDENTITY_INSERT [dbo].[mission_invite] OFF
GO
SET IDENTITY_INSERT [dbo].[mission_media] ON 

INSERT [dbo].[mission_media] ([mission_media_id], [mission_id], [media_name], [media_type], [media_path], [default], [created_at], [updated_at], [deleted_at]) VALUES (7, 5, N'CSR-initiative-stands-for-Coffee--and-Farmer-Equity-4', N'.png', N'\images\static\', 1, CAST(N'2023-03-06T11:42:30.2500000+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_media] ([mission_media_id], [mission_id], [media_name], [media_type], [media_path], [default], [created_at], [updated_at], [deleted_at]) VALUES (9, 10, N'CSR-initiative-stands-for-Coffee--and-Farmer-Equity-4', N'.png', N'\images\static\', 1, CAST(N'2023-03-06T11:42:30.2500000+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_media] ([mission_media_id], [mission_id], [media_name], [media_type], [media_path], [default], [created_at], [updated_at], [deleted_at]) VALUES (10, 3, N'CSR-initiative-stands-for-Coffee--and-Farmer-Equity-4', N'.png', N'\images\static\', 1, CAST(N'2023-03-06T11:42:30.2500000+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_media] ([mission_media_id], [mission_id], [media_name], [media_type], [media_path], [default], [created_at], [updated_at], [deleted_at]) VALUES (11, 4, N'CSR-initiative-stands-for-Coffee--and-Farmer-Equity-4', N'.png', N'\images\static\', 1, CAST(N'2023-03-06T11:42:30.2500000+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_media] ([mission_media_id], [mission_id], [media_name], [media_type], [media_path], [default], [created_at], [updated_at], [deleted_at]) VALUES (12, 6, N'CSR-initiative-stands-for-Coffee--and-Farmer-Equity-4', N'.png', N'\images\static\', 1, CAST(N'2023-03-06T11:42:30.2500000+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_media] ([mission_media_id], [mission_id], [media_name], [media_type], [media_path], [default], [created_at], [updated_at], [deleted_at]) VALUES (13, 7, N'CSR-initiative-stands-for-Coffee--and-Farmer-Equity-4', N'.png', N'\images\static\', 1, CAST(N'2023-03-07T17:48:27.7033333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_media] ([mission_media_id], [mission_id], [media_name], [media_type], [media_path], [default], [created_at], [updated_at], [deleted_at]) VALUES (14, 9, N'CSR-initiative-stands-for-Coffee--and-Farmer-Equity-4', N'.png', N'\images\static\', 1, CAST(N'2023-03-07T17:48:27.7033333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_media] ([mission_media_id], [mission_id], [media_name], [media_type], [media_path], [default], [created_at], [updated_at], [deleted_at]) VALUES (15, 11, N'CSR-initiative-stands-for-Coffee--and-Farmer-Equity-4', N'.png', N'\images\static\', 1, CAST(N'2023-03-07T17:48:27.7033333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_media] ([mission_media_id], [mission_id], [media_name], [media_type], [media_path], [default], [created_at], [updated_at], [deleted_at]) VALUES (16, 12, N'img2', N'.png', N'\images\static\', 1, CAST(N'2023-03-09T14:18:59.2466667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_media] ([mission_media_id], [mission_id], [media_name], [media_type], [media_path], [default], [created_at], [updated_at], [deleted_at]) VALUES (17, 4, N'Education-Supplies-for-Every--Pair-of-Shoes-Sold-2', N'.png', N'\images\static\', 0, CAST(N'2023-03-23T12:28:02.4200000+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_media] ([mission_media_id], [mission_id], [media_name], [media_type], [media_path], [default], [created_at], [updated_at], [deleted_at]) VALUES (18, 4, N'Nourish-the-Children-in--African-country-1', N'.png', N'\images\static\', 0, CAST(N'2023-03-23T12:28:02.4200000+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_media] ([mission_media_id], [mission_id], [media_name], [media_type], [media_path], [default], [created_at], [updated_at], [deleted_at]) VALUES (19, 4, N'Plantation-and-Afforestation-programme-1', N'.png', N'\images\static\', 0, CAST(N'2023-03-23T12:54:57.2866667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_media] ([mission_media_id], [mission_id], [media_name], [media_type], [media_path], [default], [created_at], [updated_at], [deleted_at]) VALUES (20, 14, N'36898ffd-4d0e-42f5-aedd-071d81b57b72', N'.png', N'\images\mission\', 1, CAST(N'2023-04-24T17:41:21.6246684+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_media] ([mission_media_id], [mission_id], [media_name], [media_type], [media_path], [default], [created_at], [updated_at], [deleted_at]) VALUES (21, 14, N'5b89256a-cbc1-4cae-bc39-38da0c489b33', N'.png', N'\images\mission\', 1, CAST(N'2023-04-24T17:41:21.6876826+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_media] ([mission_media_id], [mission_id], [media_name], [media_type], [media_path], [default], [created_at], [updated_at], [deleted_at]) VALUES (22, 15, N'e13b90b2-1d02-4a85-afa0-e3111994e89f', N'.jpg', N'\images\mission\', 1, CAST(N'2023-04-24T17:56:57.9536241+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_media] ([mission_media_id], [mission_id], [media_name], [media_type], [media_path], [default], [created_at], [updated_at], [deleted_at]) VALUES (23, 15, N'20eed8bf-1e36-49e8-a764-d613177afd18', N'.jpg', N'\images\mission\', 1, CAST(N'2023-04-24T17:56:57.9868891+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_media] ([mission_media_id], [mission_id], [media_name], [media_type], [media_path], [default], [created_at], [updated_at], [deleted_at]) VALUES (25, 16, N'76ec953a-5b3f-46c4-a617-6e305b8baa75', N'.jpg', N'\images\mission\', 1, CAST(N'2023-04-24T18:10:08.3172845+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_media] ([mission_media_id], [mission_id], [media_name], [media_type], [media_path], [default], [created_at], [updated_at], [deleted_at]) VALUES (26, 16, N'f7e68646-a063-48d4-a816-6f9291621190', N'.jpg', N'\images\mission\', 1, CAST(N'2023-04-24T18:10:08.3174113+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_media] ([mission_media_id], [mission_id], [media_name], [media_type], [media_path], [default], [created_at], [updated_at], [deleted_at]) VALUES (33, 19, N'54118944-3b17-4e1a-859e-4e36ef6aeed6', N'.png', N'\images\mission\', 1, CAST(N'2023-04-25T18:18:07.5316119+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_media] ([mission_media_id], [mission_id], [media_name], [media_type], [media_path], [default], [created_at], [updated_at], [deleted_at]) VALUES (34, 3, N'35bdc034-a7e2-4750-b60a-c3e9d9d4a734', N'.png', N'\images\mission\', 1, CAST(N'2023-04-25T18:38:43.9373332+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_media] ([mission_media_id], [mission_id], [media_name], [media_type], [media_path], [default], [created_at], [updated_at], [deleted_at]) VALUES (35, 20, N'9a3ed0d0-4e93-4b8b-9203-641d6a9c26c9', N'.png', N'\images\mission\', 1, CAST(N'2023-04-25T19:18:51.0799852+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_media] ([mission_media_id], [mission_id], [media_name], [media_type], [media_path], [default], [created_at], [updated_at], [deleted_at]) VALUES (36, 14, N'009c46e8-05b6-4458-93e7-7a5939b91393', N'.png', N'\images\mission\', 1, CAST(N'2023-04-26T11:12:26.2192081+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_media] ([mission_media_id], [mission_id], [media_name], [media_type], [media_path], [default], [created_at], [updated_at], [deleted_at]) VALUES (37, 8, N'5fb9afdd-5dba-4c5c-82f4-941794ff38c2', N'.png', N'\images\mission\', 1, CAST(N'2023-04-26T14:36:18.0992544+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_media] ([mission_media_id], [mission_id], [media_name], [media_type], [media_path], [default], [created_at], [updated_at], [deleted_at]) VALUES (38, 8, N'10ae1899-e807-4117-a131-b97db398d3c2', N'.png', N'\images\mission\', 1, CAST(N'2023-04-26T14:36:18.2335430+05:30' AS DateTimeOffset), NULL, NULL)
SET IDENTITY_INSERT [dbo].[mission_media] OFF
GO
SET IDENTITY_INSERT [dbo].[mission_rating] ON 

INSERT [dbo].[mission_rating] ([mission_rating_id], [user_id], [mission_id], [rating], [created_at], [updated_at], [deleted_at]) VALUES (7, 4, 4, 4, CAST(N'2023-03-20T12:47:31.9181627+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_rating] ([mission_rating_id], [user_id], [mission_id], [rating], [created_at], [updated_at], [deleted_at]) VALUES (8, 4, 8, 4, CAST(N'2023-03-27T09:48:36.3693217+05:30' AS DateTimeOffset), NULL, NULL)
SET IDENTITY_INSERT [dbo].[mission_rating] OFF
GO
SET IDENTITY_INSERT [dbo].[mission_skill] ON 

INSERT [dbo].[mission_skill] ([mission_skill_id], [mission_id], [skill_id], [status], [created_at], [updated_at], [deleted_at]) VALUES (1, 3, 1, 1, CAST(N'2023-03-06T18:26:18.3300000+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_skill] ([mission_skill_id], [mission_id], [skill_id], [status], [created_at], [updated_at], [deleted_at]) VALUES (2, 8, 2, 1, CAST(N'2023-03-06T18:26:18.3300000+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_skill] ([mission_skill_id], [mission_id], [skill_id], [status], [created_at], [updated_at], [deleted_at]) VALUES (3, 5, 5, 1, CAST(N'2023-03-06T18:26:18.3300000+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_skill] ([mission_skill_id], [mission_id], [skill_id], [status], [created_at], [updated_at], [deleted_at]) VALUES (4, 9, 9, 1, CAST(N'2023-03-06T18:26:18.3300000+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_skill] ([mission_skill_id], [mission_id], [skill_id], [status], [created_at], [updated_at], [deleted_at]) VALUES (5, 5, 5, 1, CAST(N'2023-03-06T18:26:18.3300000+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_skill] ([mission_skill_id], [mission_id], [skill_id], [status], [created_at], [updated_at], [deleted_at]) VALUES (6, 10, 7, 1, CAST(N'2023-03-06T18:26:18.3300000+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_skill] ([mission_skill_id], [mission_id], [skill_id], [status], [created_at], [updated_at], [deleted_at]) VALUES (7, 11, 1, 1, CAST(N'2023-03-06T18:26:18.3300000+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_skill] ([mission_skill_id], [mission_id], [skill_id], [status], [created_at], [updated_at], [deleted_at]) VALUES (8, 9, 8, 1, CAST(N'2023-03-06T18:26:18.3300000+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_skill] ([mission_skill_id], [mission_id], [skill_id], [status], [created_at], [updated_at], [deleted_at]) VALUES (10, 8, 6, 1, CAST(N'2023-03-06T18:26:18.3300000+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_skill] ([mission_skill_id], [mission_id], [skill_id], [status], [created_at], [updated_at], [deleted_at]) VALUES (11, 14, 1, 1, CAST(N'2023-04-24T17:41:21.3351806+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_skill] ([mission_skill_id], [mission_id], [skill_id], [status], [created_at], [updated_at], [deleted_at]) VALUES (12, 15, 2, 1, CAST(N'2023-04-24T17:56:57.7729852+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_skill] ([mission_skill_id], [mission_id], [skill_id], [status], [created_at], [updated_at], [deleted_at]) VALUES (13, 15, 3, 1, CAST(N'2023-04-24T17:56:57.7971126+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_skill] ([mission_skill_id], [mission_id], [skill_id], [status], [created_at], [updated_at], [deleted_at]) VALUES (14, 16, 3, 1, CAST(N'2023-04-24T18:10:08.0121534+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_skill] ([mission_skill_id], [mission_id], [skill_id], [status], [created_at], [updated_at], [deleted_at]) VALUES (15, 16, 4, 1, CAST(N'2023-04-24T18:10:08.0652215+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_skill] ([mission_skill_id], [mission_id], [skill_id], [status], [created_at], [updated_at], [deleted_at]) VALUES (24, 19, 1, 1, CAST(N'2023-04-25T17:10:32.8549352+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_skill] ([mission_skill_id], [mission_id], [skill_id], [status], [created_at], [updated_at], [deleted_at]) VALUES (25, 19, 2, 1, CAST(N'2023-04-25T17:10:32.9457488+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_skill] ([mission_skill_id], [mission_id], [skill_id], [status], [created_at], [updated_at], [deleted_at]) VALUES (26, 19, 3, 1, CAST(N'2023-04-25T17:10:32.9460708+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_skill] ([mission_skill_id], [mission_id], [skill_id], [status], [created_at], [updated_at], [deleted_at]) VALUES (27, 20, 12, 1, CAST(N'2023-04-25T19:18:50.9676487+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_skill] ([mission_skill_id], [mission_id], [skill_id], [status], [created_at], [updated_at], [deleted_at]) VALUES (28, 20, 13, 1, CAST(N'2023-04-25T19:18:50.9862059+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_skill] ([mission_skill_id], [mission_id], [skill_id], [status], [created_at], [updated_at], [deleted_at]) VALUES (29, 4, 7, 1, CAST(N'2023-04-26T12:59:49.3827562+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_skill] ([mission_skill_id], [mission_id], [skill_id], [status], [created_at], [updated_at], [deleted_at]) VALUES (30, 4, 13, 1, CAST(N'2023-04-26T12:59:49.6134110+05:30' AS DateTimeOffset), NULL, NULL)
SET IDENTITY_INSERT [dbo].[mission_skill] OFF
GO
SET IDENTITY_INSERT [dbo].[mission_theme] ON 

INSERT [dbo].[mission_theme] ([theme_id], [title], [status], [created_at], [updated_at], [deleted_at]) VALUES (2, N'Environment', 1, CAST(N'2023-03-03T17:22:38.8833333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_theme] ([theme_id], [title], [status], [created_at], [updated_at], [deleted_at]) VALUES (3, N'Education', 1, CAST(N'2023-03-03T17:22:38.8833333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_theme] ([theme_id], [title], [status], [created_at], [updated_at], [deleted_at]) VALUES (4, N'Children', 1, CAST(N'2023-03-03T17:22:38.8833333+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_theme] ([theme_id], [title], [status], [created_at], [updated_at], [deleted_at]) VALUES (5, N'Charity', 1, CAST(N'2023-03-05T20:43:52.6766667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_theme] ([theme_id], [title], [status], [created_at], [updated_at], [deleted_at]) VALUES (6, N'Sports', 1, CAST(N'2023-03-05T20:43:52.6766667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_theme] ([theme_id], [title], [status], [created_at], [updated_at], [deleted_at]) VALUES (7, N'Cultural Fest.', 0, CAST(N'2023-03-05T20:43:52.6766667+00:00' AS DateTimeOffset), CAST(N'2023-04-16T11:20:11.3901445+05:30' AS DateTimeOffset), NULL)
INSERT [dbo].[mission_theme] ([theme_id], [title], [status], [created_at], [updated_at], [deleted_at]) VALUES (8, N'Save Water', 0, CAST(N'2023-04-14T16:00:02.3149375+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_theme] ([theme_id], [title], [status], [created_at], [updated_at], [deleted_at]) VALUES (9, N'Solar Power', 0, CAST(N'2023-04-14T16:24:10.3565401+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_theme] ([theme_id], [title], [status], [created_at], [updated_at], [deleted_at]) VALUES (10, N'Solar ', 1, CAST(N'2023-04-14T16:26:00.4807110+05:30' AS DateTimeOffset), CAST(N'2023-04-16T11:26:38.5211561+05:30' AS DateTimeOffset), NULL)
INSERT [dbo].[mission_theme] ([theme_id], [title], [status], [created_at], [updated_at], [deleted_at]) VALUES (11, N'SolarPower', 1, CAST(N'2023-04-14T16:33:47.7363586+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_theme] ([theme_id], [title], [status], [created_at], [updated_at], [deleted_at]) VALUES (20, N'experiment', 0, CAST(N'2023-04-14T17:02:44.7106434+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_theme] ([theme_id], [title], [status], [created_at], [updated_at], [deleted_at]) VALUES (21, N'experiment2', 1, CAST(N'2023-04-14T17:02:56.5045999+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_theme] ([theme_id], [title], [status], [created_at], [updated_at], [deleted_at]) VALUES (22, N'try1', 1, CAST(N'2023-04-14T17:03:56.5735778+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_theme] ([theme_id], [title], [status], [created_at], [updated_at], [deleted_at]) VALUES (23, N'try2', 0, CAST(N'2023-04-14T17:04:10.4976463+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_theme] ([theme_id], [title], [status], [created_at], [updated_at], [deleted_at]) VALUES (24, N'title1', 0, CAST(N'2023-04-14T17:06:18.2651880+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_theme] ([theme_id], [title], [status], [created_at], [updated_at], [deleted_at]) VALUES (25, N'title2', 0, CAST(N'2023-04-14T17:06:27.3518103+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_theme] ([theme_id], [title], [status], [created_at], [updated_at], [deleted_at]) VALUES (26, N'new1', 0, CAST(N'2023-04-14T17:26:58.2519455+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_theme] ([theme_id], [title], [status], [created_at], [updated_at], [deleted_at]) VALUES (27, N'new2', 1, CAST(N'2023-04-14T17:27:06.3995052+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_theme] ([theme_id], [title], [status], [created_at], [updated_at], [deleted_at]) VALUES (28, N'timepass619', 0, CAST(N'2023-04-16T10:49:39.4977612+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[mission_theme] ([theme_id], [title], [status], [created_at], [updated_at], [deleted_at]) VALUES (29, N'timepass620', 1, CAST(N'2023-04-16T10:50:06.5452959+05:30' AS DateTimeOffset), NULL, NULL)
SET IDENTITY_INSERT [dbo].[mission_theme] OFF
GO
INSERT [dbo].[password_reset] ([email], [token], [created_at]) VALUES (N'sunnyvachheta26+myapp@gmail.com', N'831a6b61-8952-451e-883f-6019e0a12992', CAST(N'2023-04-26T21:25:55.7852506+05:30' AS DateTimeOffset))
INSERT [dbo].[password_reset] ([email], [token], [created_at]) VALUES (N'sunnyvachheta26@gmail.com', N'bf7ad6c0-9e34-4a2c-94ab-b3bd1fb0ed20', CAST(N'2023-04-12T18:36:32.9579413+05:30' AS DateTimeOffset))
INSERT [dbo].[password_reset] ([email], [token], [created_at]) VALUES (N'temp@gmail.com', N'd0e53c2b-3c90-4c99-aee0-c1e2a48613de', CAST(N'2023-04-12T18:14:31.1733913+05:30' AS DateTimeOffset))
GO
SET IDENTITY_INSERT [dbo].[skill] ON 

INSERT [dbo].[skill] ([skill_id], [name], [status], [created_at], [updated_at], [deleted_at]) VALUES (1, N'Archeology', 0, CAST(N'2023-03-06T18:23:31.2166667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[skill] ([skill_id], [name], [status], [created_at], [updated_at], [deleted_at]) VALUES (2, N'Astronomy', 1, CAST(N'2023-03-06T18:23:31.2166667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[skill] ([skill_id], [name], [status], [created_at], [updated_at], [deleted_at]) VALUES (3, N'Computer Science', 1, CAST(N'2023-03-06T18:23:31.2166667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[skill] ([skill_id], [name], [status], [created_at], [updated_at], [deleted_at]) VALUES (4, N'History', 1, CAST(N'2023-03-06T18:23:31.2166667+00:00' AS DateTimeOffset), CAST(N'2023-04-16T15:45:28.7669828+05:30' AS DateTimeOffset), NULL)
INSERT [dbo].[skill] ([skill_id], [name], [status], [created_at], [updated_at], [deleted_at]) VALUES (5, N'Music Theory', 1, CAST(N'2023-03-06T18:23:31.2166667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[skill] ([skill_id], [name], [status], [created_at], [updated_at], [deleted_at]) VALUES (6, N'Research', 1, CAST(N'2023-03-06T18:23:31.2166667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[skill] ([skill_id], [name], [status], [created_at], [updated_at], [deleted_at]) VALUES (7, N'Farming', 1, CAST(N'2023-03-06T18:23:31.2166667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[skill] ([skill_id], [name], [status], [created_at], [updated_at], [deleted_at]) VALUES (8, N'Executive Admin', 1, CAST(N'2023-03-06T18:23:31.2166667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[skill] ([skill_id], [name], [status], [created_at], [updated_at], [deleted_at]) VALUES (9, N'Data Entry', 1, CAST(N'2023-03-06T18:23:31.2166667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[skill] ([skill_id], [name], [status], [created_at], [updated_at], [deleted_at]) VALUES (10, N'Mathematics', 1, CAST(N'2023-03-06T18:23:31.2166667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[skill] ([skill_id], [name], [status], [created_at], [updated_at], [deleted_at]) VALUES (11, N'Customer Service', 1, CAST(N'2023-03-06T18:23:31.2166667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[skill] ([skill_id], [name], [status], [created_at], [updated_at], [deleted_at]) VALUES (12, N'Office Reception', 1, CAST(N'2023-03-06T18:23:31.2166667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[skill] ([skill_id], [name], [status], [created_at], [updated_at], [deleted_at]) VALUES (13, N'Botany', 1, CAST(N'2023-03-06T18:23:31.2166667+00:00' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[skill] ([skill_id], [name], [status], [created_at], [updated_at], [deleted_at]) VALUES (16, N'Writing', 0, CAST(N'2023-04-16T15:21:29.1098425+05:30' AS DateTimeOffset), CAST(N'2023-04-16T15:45:20.9181067+05:30' AS DateTimeOffset), NULL)
SET IDENTITY_INSERT [dbo].[skill] OFF
GO
SET IDENTITY_INSERT [dbo].[story] ON 

INSERT [dbo].[story] ([story_id], [user_id], [mission_id], [title], [description], [status], [published_at], [created_at], [updated_at], [deleted_at], [video_url], [short_description], [story_view], [is_deleted]) VALUES (3, 4, 8, N'Here we have story title for mission 3', N'<p>Community investment is a great tool for empowering society. Above mission is great example to corporate.</p>
<p>Everything was great.<br>Great team work.<br>Enjoy.</p>', 2, NULL, CAST(N'2023-03-28T14:13:27.8580889+05:30' AS DateTimeOffset), NULL, NULL, NULL, NULL, 60, 0)
INSERT [dbo].[story] ([story_id], [user_id], [mission_id], [title], [description], [status], [published_at], [created_at], [updated_at], [deleted_at], [video_url], [short_description], [story_view], [is_deleted]) VALUES (4, 4, 4, N'Here we have story title for mission 2', N'<p>Community investment is a great tool for empowering society. Above mission is great example to corporate.</p>
<p>Everything was great.<br>Great team work.<br>Enjoy.</p>', 3, NULL, CAST(N'2023-03-28T14:17:45.0415431+05:30' AS DateTimeOffset), NULL, NULL, NULL, NULL, 12, 1)
INSERT [dbo].[story] ([story_id], [user_id], [mission_id], [title], [description], [status], [published_at], [created_at], [updated_at], [deleted_at], [video_url], [short_description], [story_view], [is_deleted]) VALUES (9, 4, 8, N'Here we have story title for mission 3', N'<p>The <code>e.preventDefault()</code> function in jQuery is used to prevent the default behavior of an HTML element. It is often used in event handlers to prevent the browser from performing its default action when a user interacts with an element.</p>', 3, NULL, CAST(N'2023-03-29T17:05:44.1924931+05:30' AS DateTimeOffset), NULL, NULL, NULL, N'The e.preventDefault() function in jQuery is used to prevent the default behavior of an HTML element. It is often used in event handlers to prevent the browser from performing its default action when a user interacts with an element.', 11, 0)
INSERT [dbo].[story] ([story_id], [user_id], [mission_id], [title], [description], [status], [published_at], [created_at], [updated_at], [deleted_at], [video_url], [short_description], [story_view], [is_deleted]) VALUES (10, 4, 8, N'Education purpose 2 story title this is for testing', N'<p>Note: The FileList interface should be considered "at risk" since the general trend on the Web Platform is to replace such interfaces with the Array platform object in ECMAScript [ECMA-262]. In particular, this means syntax of the sort filelist.item(0) is at risk; most other programmatic use of FileList is unlikely to be affected by the eventual migration to an Array type.</p>', 2, NULL, CAST(N'2023-03-29T18:24:35.1223555+05:30' AS DateTimeOffset), NULL, NULL, NULL, N'It is better to add [HttpPost] attribute to your action so that your action is only available via POST method. This way, your action method would no longer be called with the GET method, and you could debug your code more easily and find the problem.', 1, 0)
INSERT [dbo].[story] ([story_id], [user_id], [mission_id], [title], [description], [status], [published_at], [created_at], [updated_at], [deleted_at], [video_url], [short_description], [story_view], [is_deleted]) VALUES (13, 4, 8, N'JetBrains Resharper JS extension', N'<p id="c3dc698e" class="article__p h1-related">ReSharper fully supports JavaScript up to ECMAScript 2016, including experimental features such as async/await, exponentiation operator and rest/spread in object literals/destructuring. <a id="a2fbd6a6" class="link link--external" href="http://jquery.com/" rel="" data-test="external-link ">jQuery</a> and JSX syntax are supported as well.</p>
<p id="level" class="article__p h1-related">By default, <a id="2c32e02e" class="link" href="https://www.jetbrains.com/help/resharper/ReSharper_by_Language__JavaScript__Code_Analysis_and_Coding_Assistance.html" rel="" data-test="internal-link ">code inspection</a> and other ReSharper features analyze JavaScript code according to the ECMAScript 5 standard, which is supported universally. If you use more advanced JavaScript code in your project, you can change the target ECMAScript level on the <span id="319ee09b" class="menupath ">Code Editing | JavaScript | Inspections</span> page of ReSharper options .</p>
<p id="ef137c64" class="article__p h1-related">Descriptions of ReSharper features for JavaScript are grouped in the following topics:</p>
<ul class="article__list list _ul h1-related">
<li class="list__item"><a class="link child" href="https://www.jetbrains.com/help/resharper/ReSharper_by_Language__JavaScript__Code_Analysis_and_Coding_Assistance.html" rel="" data-test="internal-link ">Code inspection and quick-fixes</a></li>
<li class="list__item"><a class="link child" href="https://www.jetbrains.com/help/resharper/Coding_Assistance_in_JavaScript.html" rel="" data-test="internal-link ">Coding assistance</a></li>
<li class="list__item"><a class="link child" href="https://www.jetbrains.com/help/resharper/ReSharper_by_Language__JavaScript__Navigation.html" rel="" data-test="internal-link ">Navigation and search</a></li>
<li class="list__item"><a class="link child" href="https://www.jetbrains.com/help/resharper/Code_Style_Assistance_in_JavaScript.html" rel="" data-test="internal-link ">Code style assistance</a></li>
<li class="list__item"><a class="link child" href="https://www.jetbrains.com/help/resharper/ReSharper_by_Language__JavaScript__Templates.html" rel="" data-test="internal-link ">Code templates</a></li>
<li class="list__item"><a class="link child" href="https://www.jetbrains.com/help/resharper/ReSharper_by_Language__JavaScript__Refactorings.html" rel="" data-test="internal-link ">Refactorings</a></li>
<li class="list__item"><a class="link child" href="https://www.jetbrains.com/help/resharper/ReSharper_by_Language__JavaScript__Unit_Testing.html" rel="" data-test="internal-link ">Unit testing assistance</a></li>
</ul>
<section class="chapter h1-related">
<h2 id="ignoringCode" class="article__h2" data-toc="ignoringCode"><span class="article__header"><span class="article__title">Make ReSharper ignore specific code</span></span></h2>
</section>
<article class="layout layout--grow app__article" data-test="app__article">
<div class="article text2 " data-test="article">
<section class="chapter h1-related">
<p id="241eb004" class="article__p h2-related">To exclude parts of your solution''s code from code analysis, navigation, and other features, ReSharper allows you to <a id="e6576fb7" class="link" href="https://www.jetbrains.com/help/resharper/Ignore_Parts_of_Code.html" rel="" data-test="internal-link ">ignore specific files, folders and file masks in different ways</a>.</p>
<p id="b16aa8a6" class="article__p h2-related">To improve performance, ReSharper also automatically detects and starts ignoring large web files that have no references and were probably added to the solution by mistake.</p>
<p id="3c9c8b6b" class="article__p h2-related">If any of such files are detected, you will see a notification where you can stop ignoring any of those files if you need them. You can also find all automatically ignored files on the <span id="12a13486" class="menupath ">Code Editing | Third-Party Code</span> page of ReSharper options .</p>
<p id="a320793" class="article__p h2-related">To stop auto-detection and ignoring large unused web files, clear the <span id="2c1014a2" class="control ">Search for web files that can affect performance and exclude them from indexing</span> checkbox on the <span id="75284e1f" class="menupath ">Code Editing | Third-Party Code</span> page of ReSharper options .</p>
</section>
<div class="h1-related" data-feedback-placeholder="true">&nbsp;</div>
</div>
</article>
<aside class="layout" data-test="layout">
<div class="stretch__wrapper app__virtual-toc-sidebar">
<div class="stretch" style="height: 156px;" data-test="virtual-toc-sidebar">
<div class="layout layout--scroll-container app__virtual-toc-scroll-disabler" data-test="scroller">
<div class="layout layout--scroll-element" data-test="layout">
<ul class="toc app__virtual-toc" data-test="virtual-toc">
<li class="toc-node toc-node--selected" data-toc-scroll="ReSharper_by_Language__JavaScript" data-test="toc-node--selected"><a class="toc-item toc-item--anchor toc-item--theme-light" style="padding-left: 0px;" href="https://www.jetbrains.com/help/resharper/ReSharper_by_Language__JavaScript.html" data-test="internal-link toc-item"> ReSharper by Language: JavaScript</a></li>
<li class="toc-node" data-toc-scroll="ignoringCode" data-test="toc-node"><a class="toc-item toc-item--anchor toc-item--theme-light" style="padding-left: 0px;" href="https://www.jetbrains.com/help/resharper/ReSharper_by_Language__JavaScript.html#ignoringCode" data-test="internal-link toc-item"> Make ReSharper ignore specific code</a></li>
</ul>
</div>
</div>
</div>
</div>
</aside>', 2, NULL, CAST(N'2023-03-30T11:04:45.6252835+05:30' AS DateTimeOffset), NULL, NULL, N'https://www.jetbrains.com/help/resharper/ReSharper_by_Language__JavaScript.html', N'Starting from ReSharper 2022.2, active development of productivity features for JavaScript, TypeScript, JSON, and Protobuf is suspended, and these features are disabled by default. To enable them, select JavaScript and TypeScript and/or Protobuf on the En', 16, 0)
INSERT [dbo].[story] ([story_id], [user_id], [mission_id], [title], [description], [status], [published_at], [created_at], [updated_at], [deleted_at], [video_url], [short_description], [story_view], [is_deleted]) VALUES (15, 4, 4, N'Here we have story title for mission 3', N'<p>In this article, I explained how to populate a dropdown list from database values in ASP.NET Core MVC application. I created model classes to define properties for the dropdown list control and dbset properties to connect to database. A controller has been created which selects a view to be displayed to the user and provides the necessary data model to retrieve data from the SQL Server database. A view page has been created for index action method in which dropdown list control has been designed. Proper coding snippets along with output have been provided for each and every part of the application.&nbsp;</p>', 2, NULL, CAST(N'2023-04-03T14:29:54.3901042+05:30' AS DateTimeOffset), CAST(N'2023-04-03T14:30:13.5791060+05:30' AS DateTimeOffset), NULL, N'https://www.c-sharpcorner.com/article/binding-data-to-dropdown-list-in-asp-net-core-mvc-web-application/', N'Education purpose 2 story title this is for testing', NULL, 0)
INSERT [dbo].[story] ([story_id], [user_id], [mission_id], [title], [description], [status], [published_at], [created_at], [updated_at], [deleted_at], [video_url], [short_description], [story_view], [is_deleted]) VALUES (10016, 4, 12, N'Concern Global warming', N'<p>Global warming refers to the long-term increase in the Earth''s average surface temperature due to the buildup of greenhouse gases in the atmosphere. The most significant greenhouse gases include carbon dioxide, methane, and nitrous oxide, which trap heat from the sun and prevent it from escaping into space.</p>
<p>The scientific consensus is that human activities, such as burning fossil fuels, deforestation, and agriculture, are the primary drivers of global warming. These activities release large amounts of greenhouse gases into the atmosphere, which contribute to the warming of the planet.</p>
<p>The consequences of global warming are widespread and potentially catastrophic. They include more frequent and severe heatwaves, droughts, and wildfires, rising sea levels, and increased frequency of extreme weather events like hurricanes, floods, and storms. These changes in climate can have significant impacts on human health, agriculture, ecosystems, and infrastructure.</p>
<p>To address the problem of global warming, countries around the world have taken steps to reduce their greenhouse gas emissions and transition to a low-carbon economy. This includes increasing the use of renewable energy sources like solar and wind power, improving energy efficiency, and implementing policies and regulations to encourage more sustainable practices.</p>
<p>Individuals can also play a role in mitigating global warming by reducing their own carbon footprint through actions such as driving less, eating a plant-based diet, and reducing energy consumption at home and at work.</p>', 2, NULL, CAST(N'2023-04-12T19:14:15.4571744+05:30' AS DateTimeOffset), NULL, NULL, N'https://youtu.be/oJAbATJCugs', N'My thoughts about increasing global warming due to CO2 emission', NULL, 0)
INSERT [dbo].[story] ([story_id], [user_id], [mission_id], [title], [description], [status], [published_at], [created_at], [updated_at], [deleted_at], [video_url], [short_description], [story_view], [is_deleted]) VALUES (10017, 4, 3, N'Industrialization Overhead', N'<div class="flex flex-grow flex-col gap-3">
<div class="min-h-[20px] flex flex-col items-start gap-4 whitespace-pre-wrap">
<div class="markdown prose w-full break-words dark:prose-invert light">
<p>Industrialization is the process of transitioning from an agricultural-based economy to one that is centered around manufacturing and industry. This process is typically characterized by the growth of factories, mass production, and mechanization.</p>
<p>Industrialization has had a significant impact on the world economy and society as a whole. It has led to increased productivity and economic growth, created new jobs and industries, and enabled the production of goods on a large scale.</p>
<p>One of the primary benefits of industrialization is the increased efficiency and productivity it brings. With the use of machines and assembly-line production, factories can produce goods at a much faster rate and with greater consistency than manual labor alone. This has led to a decrease in the cost of goods and an increase in their availability, making them more accessible to people around the world.</p>
<p>However, industrialization has also had some negative impacts, particularly on the environment and public health. The mass production of goods requires a significant amount of energy, much of which comes from non-renewable sources such as coal and oil. This has led to increased greenhouse gas emissions and pollution, contributing to global warming and other environmental problems.</p>
<p>Industrialization has also led to the creation of urban areas and the growth of cities, which can result in overcrowding and inadequate living conditions for workers. This has been particularly true in developing countries where industrialization has occurred rapidly and without sufficient regulation.</p>
<p>In conclusion, industrialization has had both positive and negative impacts on society and the environment. While it has contributed to economic growth and increased productivity, it has also had negative consequences, particularly in terms of environmental degradation and public health. As such, it is important to balance the benefits of industrialization with efforts to mitigate its negative impacts and promote sustainable development.</p>
</div>
</div>
</div>', 3, NULL, CAST(N'2023-04-12T19:19:07.0867499+05:30' AS DateTimeOffset), NULL, NULL, NULL, N'Industrialization is new problem', NULL, 1)
SET IDENTITY_INSERT [dbo].[story] OFF
GO
SET IDENTITY_INSERT [dbo].[story_invite] ON 

INSERT [dbo].[story_invite] ([story_invite_id], [story_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (1, 3, 4, 7, CAST(N'2023-03-31T12:22:00.5467034+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_invite] ([story_invite_id], [story_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (2, 3, 4, 9, CAST(N'2023-03-31T12:22:00.8707255+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_invite] ([story_invite_id], [story_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (3, 13, 4, 7, CAST(N'2023-04-03T14:32:40.1087280+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_invite] ([story_invite_id], [story_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (4, 13, 4, 8, CAST(N'2023-04-03T14:32:40.1834972+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_invite] ([story_invite_id], [story_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (5, 13, 4, 9, CAST(N'2023-04-03T14:32:40.1837004+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_invite] ([story_invite_id], [story_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (10002, 3, 15, 4, CAST(N'2023-04-17T12:31:30.8441536+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_invite] ([story_invite_id], [story_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (10003, 3, 15, 5, CAST(N'2023-04-17T12:31:30.9371198+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_invite] ([story_invite_id], [story_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (10004, 3, 15, 7, CAST(N'2023-04-17T12:31:30.9372615+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_invite] ([story_invite_id], [story_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (10005, 3, 4, 15, CAST(N'2023-04-17T12:37:35.1982265+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_invite] ([story_invite_id], [story_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (10006, 3, 4, 5, CAST(N'2023-04-18T11:26:00.2836013+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_invite] ([story_invite_id], [story_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (10007, 3, 4, 6, CAST(N'2023-04-18T11:26:00.3574980+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_invite] ([story_invite_id], [story_id], [from_user_id], [to_user_id], [created_at], [updated_at], [deleted_at]) VALUES (10008, 3, 4, 16, CAST(N'2023-04-18T11:26:00.3576455+05:30' AS DateTimeOffset), NULL, NULL)
SET IDENTITY_INSERT [dbo].[story_invite] OFF
GO
SET IDENTITY_INSERT [dbo].[story_media] ON 

INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (4, 3, N'dbb31459-d95b-4f32-9b4d-8df5c4822c41', N'.png', N'\images\story\', CAST(N'2023-03-28T14:13:28.5379266+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (5, 3, N'caed2d86-a4b1-47fd-ac65-88875e774a55', N'.png', N'\images\story\', CAST(N'2023-03-28T14:13:28.5380518+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (6, 3, N'daa63a4b-b39e-4654-903f-73690faaa9c0', N'.png', N'\images\story\', CAST(N'2023-03-28T14:13:28.5380532+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (7, 3, N'95804278-2828-4d0c-9812-f858067c022c', N'.png', N'\images\story\', CAST(N'2023-03-28T14:13:28.5380537+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (8, 4, N'cf089fcd-0b8c-4662-8886-456d78e1b901', N'.png', N'\images\story\', CAST(N'2023-03-28T14:17:45.6104253+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (9, 4, N'a1c9877b-f9ab-4faa-9ad4-104b2dc85d95', N'.png', N'\images\story\', CAST(N'2023-03-28T14:17:45.6105495+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (10, 4, N'cef9def9-d288-4dc3-a9b4-80d91daa5b10', N'.png', N'\images\story\', CAST(N'2023-03-28T14:17:45.6105511+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (11, 4, N'e9bc690e-ec21-4173-8a15-6b1141da1185', N'.png', N'\images\story\', CAST(N'2023-03-28T14:17:45.6105515+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (12, 3, N'27d0614b-08a7-46e9-9395-17a55861d120', N'.png', N'\images\story\', CAST(N'2023-03-28T15:49:47.2017157+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (13, 3, N'7a5524b6-42e0-4954-b188-be018fb5b0fc', N'.png', N'\images\story\', CAST(N'2023-03-28T15:49:47.2018221+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (14, 3, N'e288bdac-68f8-4464-ab53-9fc7c77d72ee', N'.png', N'\images\story\', CAST(N'2023-03-28T15:49:47.2018230+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (15, 3, N'3dd81315-8fa6-43e6-a813-c64da775f575', N'.png', N'\images\story\', CAST(N'2023-03-28T15:49:47.2018232+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (16, 3, N'4c7f73f9-27b7-42a5-9228-ed1c8014c605', N'.png', N'\images\story\', CAST(N'2023-03-28T17:25:42.1610618+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (17, 3, N'590caedb-717a-4f43-bd6b-66934a14f546', N'.png', N'\images\story\', CAST(N'2023-03-28T17:25:42.1611653+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (18, 3, N'863fc221-f1e1-4d18-9d4b-bbf95865cf7b', N'.png', N'\images\story\', CAST(N'2023-03-28T17:25:42.1611663+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (25, 9, N'c7b757e7-08b7-49d3-8839-c5aac8025181', N'.png', N'\images\story\', CAST(N'2023-03-29T17:05:44.5549817+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (26, 9, N'3c05c8e9-53db-4d88-854e-48df711fdfe5', N'.png', N'\images\story\', CAST(N'2023-03-29T17:05:44.5550899+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (27, 10, N'17a79331-6bb0-4d40-8e69-0825bed9ccfa', N'.png', N'\images\story\', CAST(N'2023-03-29T18:24:36.1266586+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (28, 10, N'deb5ed0c-b2cf-43fd-8690-cfbe6178c68b', N'.png', N'\images\story\', CAST(N'2023-03-29T18:24:36.1267833+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (29, 10, N'6e48c32e-a0ee-4546-ad22-14746262c8fd', N'.png', N'\images\story\', CAST(N'2023-03-29T18:24:36.1267842+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (30, 13, N'f7952ec9-905b-456c-82f7-8961e25b3af4', N'.png', N'\images\story\', CAST(N'2023-03-30T11:04:46.0440708+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (31, 13, N'9835d0a5-92d3-4d23-b956-e67d63c7753b', N'.png', N'\images\story\', CAST(N'2023-03-30T11:04:46.0441816+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (37, 15, N'9cfd0168-6bde-4d19-8aa3-e6b3c026bb92', N'.png', N'\images\story\', CAST(N'2023-04-03T14:29:54.9334233+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (38, 15, N'56248bff-4e53-460e-9dc1-9240976aa607', N'.png', N'\images\story\', CAST(N'2023-04-03T14:29:54.9334261+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (39, 15, N'ea1f70ca-9b45-40c6-a89a-76069323eaf2', N'.png', N'\images\story\', CAST(N'2023-04-03T14:30:13.6976328+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (40, 15, N'd294d7a5-b792-4aca-8ac3-8e0be7c9f3a5', N'.png', N'\images\story\', CAST(N'2023-04-03T14:30:13.6976354+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (41, 15, N'88cd8179-9ca0-4862-84d3-f7b6d5d17dac', N'.png', N'\images\story\', CAST(N'2023-04-03T14:30:13.6976359+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (10002, 10016, N'8e24ddb2-a21b-46d7-a68f-ccc980efe3b8', N'.jpg', N'\images\story\', CAST(N'2023-04-12T19:14:35.9398348+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (10003, 10016, N'34cb650e-f38a-4226-bedd-e9f394bb4422', N'.jpg', N'\images\story\', CAST(N'2023-04-12T19:14:35.9399427+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[story_media] ([story_media_id], [story_id], [media_name], [media_type], [media_path], [created_at], [updated_at], [deleted_at]) VALUES (10004, 10017, N'f93d1941-85dd-4887-a59e-b044683a2f49', N'.jpg', N'\images\story\', CAST(N'2023-04-12T19:19:07.1071745+05:30' AS DateTimeOffset), NULL, NULL)
SET IDENTITY_INSERT [dbo].[story_media] OFF
GO
SET IDENTITY_INSERT [dbo].[timesheet] ON 

INSERT [dbo].[timesheet] ([timesheet_id], [user_id], [mission_id], [time], [action], [date_volunteered], [notes], [status], [created_at], [updated_at]) VALUES (2, 4, 12, CAST(N'02:40:00' AS Time), NULL, CAST(N'2022-03-06T00:00:00.0000000+05:30' AS DateTimeOffset), N'Done!', 1, CAST(N'2023-04-07T10:25:25.0113973+05:30' AS DateTimeOffset), NULL)
INSERT [dbo].[timesheet] ([timesheet_id], [user_id], [mission_id], [time], [action], [date_volunteered], [notes], [status], [created_at], [updated_at]) VALUES (5, 12, 12, CAST(N'12:10:00' AS Time), NULL, CAST(N'2022-03-06T00:00:00.0000000+05:30' AS DateTimeOffset), N'Extra work!', 2, CAST(N'2023-04-07T10:41:53.7992514+05:30' AS DateTimeOffset), NULL)
INSERT [dbo].[timesheet] ([timesheet_id], [user_id], [mission_id], [time], [action], [date_volunteered], [notes], [status], [created_at], [updated_at]) VALUES (6, 4, 5, CAST(N'04:15:00' AS Time), NULL, CAST(N'2022-03-06T00:00:00.0000000+05:30' AS DateTimeOffset), N'No comment!', 2, CAST(N'2023-04-07T11:56:53.6029502+05:30' AS DateTimeOffset), NULL)
INSERT [dbo].[timesheet] ([timesheet_id], [user_id], [mission_id], [time], [action], [date_volunteered], [notes], [status], [created_at], [updated_at]) VALUES (12, 4, 5, CAST(N'01:01:00' AS Time), NULL, CAST(N'2001-01-01T00:00:00.0000000+05:30' AS DateTimeOffset), N'lorem', 1, CAST(N'2023-04-07T15:52:55.4540862+05:30' AS DateTimeOffset), NULL)
INSERT [dbo].[timesheet] ([timesheet_id], [user_id], [mission_id], [time], [action], [date_volunteered], [notes], [status], [created_at], [updated_at]) VALUES (17, 4, 3, CAST(N'10:10:00' AS Time), NULL, CAST(N'2020-12-31T00:00:00.0000000+05:30' AS DateTimeOffset), N'this is work!', 2, CAST(N'2023-04-14T10:09:27.8764769+05:30' AS DateTimeOffset), NULL)
INSERT [dbo].[timesheet] ([timesheet_id], [user_id], [mission_id], [time], [action], [date_volunteered], [notes], [status], [created_at], [updated_at]) VALUES (18, 4, 4, NULL, 12, CAST(N'2022-03-13T00:00:00.0000000+05:30' AS DateTimeOffset), N'We are!', 1, CAST(N'2023-04-14T10:10:45.9775621+05:30' AS DateTimeOffset), NULL)
INSERT [dbo].[timesheet] ([timesheet_id], [user_id], [mission_id], [time], [action], [date_volunteered], [notes], [status], [created_at], [updated_at]) VALUES (19, 4, 5, CAST(N'23:15:00' AS Time), NULL, CAST(N'2020-04-20T00:00:00.0000000+05:30' AS DateTimeOffset), N'done', 2, CAST(N'2023-04-18T11:23:40.5833615+05:30' AS DateTimeOffset), NULL)
INSERT [dbo].[timesheet] ([timesheet_id], [user_id], [mission_id], [time], [action], [date_volunteered], [notes], [status], [created_at], [updated_at]) VALUES (21, 4, 8, NULL, -18, CAST(N'2020-02-20T00:00:00.0000000+05:30' AS DateTimeOffset), N'done!', 2, CAST(N'2023-04-24T10:53:32.7694193+05:30' AS DateTimeOffset), NULL)
SET IDENTITY_INSERT [dbo].[timesheet] OFF
GO
SET IDENTITY_INSERT [dbo].[user] ON 

INSERT [dbo].[user] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [avatar], [why_i_volunteer], [employee_id], [department], [city_id], [country_id], [profile_text], [linked_in_url], [title], [status], [created_at], [updated_at], [deleted_at], [availability]) VALUES (1, N'Sunny', N'Vachheta', N'sun@gmail.com', N'pass', N'8923472389', N'\images\static\anon-profile.png', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2023-02-28T18:09:41.0700000+00:00' AS DateTimeOffset), NULL, NULL, NULL)
INSERT [dbo].[user] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [avatar], [why_i_volunteer], [employee_id], [department], [city_id], [country_id], [profile_text], [linked_in_url], [title], [status], [created_at], [updated_at], [deleted_at], [availability]) VALUES (2, N'First', N'Second', N'sun1@gmail.com', N'password', N'8923472389', N'\images\static\anon-profile.png', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, CAST(N'2023-03-01T09:54:43.8900000+00:00' AS DateTimeOffset), NULL, NULL, NULL)
INSERT [dbo].[user] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [avatar], [why_i_volunteer], [employee_id], [department], [city_id], [country_id], [profile_text], [linked_in_url], [title], [status], [created_at], [updated_at], [deleted_at], [availability]) VALUES (3, N'Muse', N'Asia', N'sun11@gmail.com', N'password', N'7898243342', N'\images\static\anon-profile.png', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2023-03-01T16:33:28.6000000+00:00' AS DateTimeOffset), NULL, NULL, NULL)
INSERT [dbo].[user] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [avatar], [why_i_volunteer], [employee_id], [department], [city_id], [country_id], [profile_text], [linked_in_url], [title], [status], [created_at], [updated_at], [deleted_at], [availability]) VALUES (4, N'John', N'Statham', N'sunnyvachheta26@gmail.com', N'aIBUZuUsZ4/KJprhV3Svgvv1gKDMNa30APFB6YXl750=', N'3432424234', N'\images\user\1b4bed82-562e-4a52-a6af-7722b170affe.png', NULL, NULL, NULL, 3, 1, N'Random text sljfldsjf l', NULL, NULL, 1, CAST(N'2023-03-02T12:39:56.6266667+00:00' AS DateTimeOffset), CAST(N'2023-04-25T19:08:48.5017589+05:30' AS DateTimeOffset), NULL, 2)
INSERT [dbo].[user] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [avatar], [why_i_volunteer], [employee_id], [department], [city_id], [country_id], [profile_text], [linked_in_url], [title], [status], [created_at], [updated_at], [deleted_at], [availability]) VALUES (5, N'Mac', N'Clover', N'mac123@gmail.com', N'password', N'8932129933', N'\images\user\e4de3c52-05b5-4c56-b858-b6f25c77c143.jpg', NULL, NULL, NULL, 6, 2, N'It looks like there aren''t many great matches for your search', NULL, NULL, 1, CAST(N'2023-03-21T18:35:45.8033333+00:00' AS DateTimeOffset), CAST(N'2023-04-04T17:31:03.7105197+05:30' AS DateTimeOffset), NULL, 2)
INSERT [dbo].[user] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [avatar], [why_i_volunteer], [employee_id], [department], [city_id], [country_id], [profile_text], [linked_in_url], [title], [status], [created_at], [updated_at], [deleted_at], [availability]) VALUES (6, N'Ray', N'Ramsay', N'ramsay619@gmail.com', N'password', N'8898321233', N'\images\static\anon-profile.png', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2023-03-21T18:38:20.8966667+00:00' AS DateTimeOffset), NULL, NULL, NULL)
INSERT [dbo].[user] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [avatar], [why_i_volunteer], [employee_id], [department], [city_id], [country_id], [profile_text], [linked_in_url], [title], [status], [created_at], [updated_at], [deleted_at], [availability]) VALUES (7, N'John', N'Cena', N'sunnyrv312@gmail.com', N'johncena', N'8932894322', N'\images\user\4265fd2f-d78c-4749-a090-656c40764288.png', NULL, NULL, NULL, 5, 2, N'You can''t see me!', NULL, NULL, 1, CAST(N'2023-03-22T18:23:10.6466667+00:00' AS DateTimeOffset), CAST(N'2023-04-22T20:30:51.0800765+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[user] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [avatar], [why_i_volunteer], [employee_id], [department], [city_id], [country_id], [profile_text], [linked_in_url], [title], [status], [created_at], [updated_at], [deleted_at], [availability]) VALUES (8, N'Joy', N'Jason', N'sunnyvachheta89@gmail.com', N'password', N'8923134312', N'\images\static\anon-profile.png', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2023-03-22T18:27:31.4866667+00:00' AS DateTimeOffset), NULL, NULL, NULL)
INSERT [dbo].[user] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [avatar], [why_i_volunteer], [employee_id], [department], [city_id], [country_id], [profile_text], [linked_in_url], [title], [status], [created_at], [updated_at], [deleted_at], [availability]) VALUES (9, N'Oro', N'Jackson', N'sunnyvachheta26+myapp@gmail.com', N'aIBUZuUsZ4/KJprhV3Svgvv1gKDMNa30APFB6YXl750=', N'8923134312', N'\images\static\anon-profile.png', N'I''m a curious individual who enjoys learning new things and challenging myself. In my spare time, I love to paint and create art, and I''m always looking for new sources of inspiration. I''m also an avid gamer and enjoy playing all kinds of games, from board games to video games. I''m a huge fan of animals, especially dogs and cats, and enjoy volunteering at local animal shelters whenever I can. I''m looking to connect with other like-minded individuals who share my interests and passions. Let''s chat and see where our conversations take us!', NULL, NULL, 10, 4, N'I''m a curious individual who enjoys learning new things and challenging myself. In my spare time, I love to paint and create art, and I''m always looking for new sources of inspiration. I''m also an avid gamer and enjoy playing all kinds of games, from board games to video games. I''m a huge fan of animals, especially dogs and cats, and enjoy volunteering at local animal shelters whenever I can. I''m looking to connect with other like-minded individuals who share my interests and passions. Let''s chat and see where our conversations take us!', NULL, NULL, 1, CAST(N'2023-03-22T18:30:24.3200000+00:00' AS DateTimeOffset), CAST(N'2023-04-21T14:45:52.7340458+05:30' AS DateTimeOffset), NULL, 3)
INSERT [dbo].[user] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [avatar], [why_i_volunteer], [employee_id], [department], [city_id], [country_id], [profile_text], [linked_in_url], [title], [status], [created_at], [updated_at], [deleted_at], [availability]) VALUES (10, N'Jimmy', N'Rez', N'abc619@gmail.com', N'password', N'8932929288', N'\images\static\anon-profile.png', NULL, NULL, NULL, 16, 6, N'Helsinki wood city!', NULL, NULL, 1, CAST(N'2023-03-22T18:33:00.2800000+00:00' AS DateTimeOffset), CAST(N'2023-04-22T20:44:39.8470780+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[user] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [avatar], [why_i_volunteer], [employee_id], [department], [city_id], [country_id], [profile_text], [linked_in_url], [title], [status], [created_at], [updated_at], [deleted_at], [availability]) VALUES (11, N'Lorem', N'Ipsum', N'lorem123@gmail.com', N'password', N'9032984902', N'\images\static\anon-profile.png', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2023-03-22T18:36:29.5566667+00:00' AS DateTimeOffset), NULL, NULL, NULL)
INSERT [dbo].[user] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [avatar], [why_i_volunteer], [employee_id], [department], [city_id], [country_id], [profile_text], [linked_in_url], [title], [status], [created_at], [updated_at], [deleted_at], [availability]) VALUES (12, N'Robin', N'Christ', N'sunnyvachheta26+first@gmail.com', N'i2R1LiDk5qjE6UYEmogVC7WeUg16CoSxudIjDyUzKg4=', N'8938438233', N'\images\static\anon-profile.png', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2023-03-24T21:18:13.6400000+00:00' AS DateTimeOffset), NULL, NULL, NULL)
INSERT [dbo].[user] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [avatar], [why_i_volunteer], [employee_id], [department], [city_id], [country_id], [profile_text], [linked_in_url], [title], [status], [created_at], [updated_at], [deleted_at], [availability]) VALUES (15, N'Rozvik', N'Dimitriv', N'sunnyvachheta26+test@gmail.com', N'aIBUZuUsZ4/KJprhV3Svgvv1gKDMNa30APFB6YXl750=', N'8823123443', N'\images\user\454d12bf-2cf3-459d-a2d1-ad5d90ec0e40.png', N'I''m an adventurer at heart and love exploring new places and trying new things. When I''m not traveling, you can usually find me curled up with a good book or hiking in the great outdoors. I''m passionate about sustainability and try to live a low-waste lifestyle as much as possible. In my free time, I enjoy practicing yoga, trying out new recipes in the kitchen, and spending time with my dog. I''m always up for a good conversation, so feel free to reach out and say hi!', NULL, NULL, 2, 1, N'I''m an adventurer at heart and love exploring new places and trying new things. When I''m not traveling, you can usually find me curled up with a good book or hiking in the great outdoors. I''m passionate about sustainability and try to live a low-waste lifestyle as much as possible. In my free time, I enjoy practicing yoga, trying out new recipes in the kitchen, and spending time with my dog. I''m always up for a good conversation, so feel free to reach out and say hi!', NULL, NULL, 1, CAST(N'2023-04-16T09:10:24.7800000+00:00' AS DateTimeOffset), CAST(N'2023-04-21T14:30:44.1936239+05:30' AS DateTimeOffset), NULL, 2)
INSERT [dbo].[user] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [avatar], [why_i_volunteer], [employee_id], [department], [city_id], [country_id], [profile_text], [linked_in_url], [title], [status], [created_at], [updated_at], [deleted_at], [availability]) VALUES (16, N'Rusewolt', N'Baker', N'sunnyvachheta26+test2@gmail.com', N'aIBUZuUsZ4/KJprhV3Svgvv1gKDMNa30APFB6YXl750=', N'8823123443', N'\images\static\anon-profile.png', N'I''m an adventurer at heart and love exploring new places and trying new things. When I''m not traveling, you can usually find me curled up with a good book or hiking in the great outdoors. I''m passionate about sustainability and try to live a low-waste lifestyle as much as possible. In my free time, I enjoy practicing yoga, trying out new recipes in the kitchen, and spending time with my dog. I''m always up for a good conversation, so feel free to reach out and say hi!', NULL, NULL, 8, 3, N'I''m an adventurer at heart and love exploring new places and trying new things. When I''m not traveling, you can usually find me curled up with a good book or hiking in the great outdoors. I''m passionate about sustainability and try to live a low-waste lifestyle as much as possible. In my free time, I enjoy practicing yoga, trying out new recipes in the kitchen, and spending time with my dog. I''m always up for a good conversation, so feel free to reach out and say hi!', NULL, NULL, 1, CAST(N'2023-04-16T09:16:38.9400000+00:00' AS DateTimeOffset), CAST(N'2023-04-21T14:42:13.1818732+05:30' AS DateTimeOffset), NULL, 2)
INSERT [dbo].[user] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [avatar], [why_i_volunteer], [employee_id], [department], [city_id], [country_id], [profile_text], [linked_in_url], [title], [status], [created_at], [updated_at], [deleted_at], [availability]) VALUES (17, N'xyz', N'xyz', N'sunnyvachheta26+pre@gmail.com', N'gxvvDcHDnp6lvc6Wbw/vlmcEgqhFbMFXxk6DQKGzqZw=', N'6776655544', N'\images\user\b265b328-7e33-4a36-bbe6-88dc57b53bd6.png', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2023-04-18T11:31:18.8400000+00:00' AS DateTimeOffset), NULL, NULL, NULL)
INSERT [dbo].[user] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [avatar], [why_i_volunteer], [employee_id], [department], [city_id], [country_id], [profile_text], [linked_in_url], [title], [status], [created_at], [updated_at], [deleted_at], [availability]) VALUES (18, N'Jay', N'Kishan', N'sunnyvachheta26+jay@gmail.com', N'gxvvDcHDnp6lvc6Wbw/vlmcEgqhFbMFXxk6DQKGzqZw=', N'8823123443', N'\images\static\anon-profile.png', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2023-04-18T12:26:45.1233333+00:00' AS DateTimeOffset), NULL, NULL, NULL)
INSERT [dbo].[user] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [avatar], [why_i_volunteer], [employee_id], [department], [city_id], [country_id], [profile_text], [linked_in_url], [title], [status], [created_at], [updated_at], [deleted_at], [availability]) VALUES (19, N'Jay', N'Kishan', N'sunnyvachheta26+kishan@gmail.com', N'gxvvDcHDnp6lvc6Wbw/vlmcEgqhFbMFXxk6DQKGzqZw=', N'8823123443', N'\images\static\anon-profile.png', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, CAST(N'2023-04-18T12:29:08.0666667+00:00' AS DateTimeOffset), NULL, NULL, NULL)
INSERT [dbo].[user] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [avatar], [why_i_volunteer], [employee_id], [department], [city_id], [country_id], [profile_text], [linked_in_url], [title], [status], [created_at], [updated_at], [deleted_at], [availability]) VALUES (22, N'Armin', N'Latham', N'sunnyvachheta26+armin@gmail.com', N'aIBUZuUsZ4/KJprhV3Svgvv1gKDMNa30APFB6YXl750=', N'8823123443', N'', NULL, NULL, NULL, 5, 2, N'this is some random text here.', NULL, NULL, 0, CAST(N'2023-04-22T16:54:49.5446548+05:30' AS DateTimeOffset), NULL, NULL, NULL)
INSERT [dbo].[user] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [avatar], [why_i_volunteer], [employee_id], [department], [city_id], [country_id], [profile_text], [linked_in_url], [title], [status], [created_at], [updated_at], [deleted_at], [availability]) VALUES (23, N'Eren', N'Yeager', N'sunnyvachheta89+eren@gmail.com', N'aIBUZuUsZ4/KJprhV3Svgvv1gKDMNa30APFB6YXl750=', N'2343282893', N'', NULL, NULL, NULL, 2, 1, N'fjsdjflsdjf sdjsdljfkls flskjfklsd', NULL, NULL, 0, CAST(N'2023-04-22T17:06:28.3056099+05:30' AS DateTimeOffset), NULL, NULL, NULL)
INSERT [dbo].[user] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [avatar], [why_i_volunteer], [employee_id], [department], [city_id], [country_id], [profile_text], [linked_in_url], [title], [status], [created_at], [updated_at], [deleted_at], [availability]) VALUES (24, N'Liskov', N'Substitution', N'sunnyvachheta26+lis@gmail.com', N'aIBUZuUsZ4/KJprhV3Svgvv1gKDMNa30APFB6YXl750=', N'8823123443', N'\images\user\02593b14-68bb-43c4-9e3d-4ae4e3ebb04b.png', NULL, NULL, NULL, 14, 5, N'some random text here.', NULL, NULL, 0, CAST(N'2023-04-22T17:22:34.6038842+05:30' AS DateTimeOffset), NULL, NULL, NULL)
INSERT [dbo].[user] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [avatar], [why_i_volunteer], [employee_id], [department], [city_id], [country_id], [profile_text], [linked_in_url], [title], [status], [created_at], [updated_at], [deleted_at], [availability]) VALUES (25, N'Evanka', N'Maradona', N'sunnyvachheta26+eva@gmail.com', N'aIBUZuUsZ4/KJprhV3Svgvv1gKDMNa30APFB6YXl750=', N'7234973298', N'\images\user\44439fd9-e9fc-4223-acf4-c29a48b97e21.png', NULL, NULL, NULL, 5, 2, N'Some random profile text goes here', NULL, NULL, 0, CAST(N'2023-04-22T17:29:38.7158102+05:30' AS DateTimeOffset), NULL, NULL, NULL)
INSERT [dbo].[user] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [avatar], [why_i_volunteer], [employee_id], [department], [city_id], [country_id], [profile_text], [linked_in_url], [title], [status], [created_at], [updated_at], [deleted_at], [availability]) VALUES (26, N'Milim', N'Nava', N'sunny_milim@gmail.com', N'aIBUZuUsZ4/KJprhV3Svgvv1gKDMNa30APFB6YXl750=', N'2848924982', N'\images\static\anon-profile.png', NULL, NULL, NULL, 2, 1, N'Some random profile text goes here', NULL, NULL, 0, CAST(N'2023-04-22T17:37:30.5118152+05:30' AS DateTimeOffset), NULL, NULL, NULL)
INSERT [dbo].[user] ([user_id], [first_name], [last_name], [email], [password], [phone_number], [avatar], [why_i_volunteer], [employee_id], [department], [city_id], [country_id], [profile_text], [linked_in_url], [title], [status], [created_at], [updated_at], [deleted_at], [availability]) VALUES (27, N'jflsdkf ', N'jfklsdj ', N'sunnyvachheta26+pres@gmail.com', N'aIBUZuUsZ4/KJprhV3Svgvv1gKDMNa30APFB6YXl750=', N'2848327432', N'\images\static\anon-profile.png', NULL, NULL, NULL, 12, 4, N'jklfdkls jfklsjf lkjsfkdjs', NULL, NULL, 0, CAST(N'2023-04-25T19:09:41.5282932+05:30' AS DateTimeOffset), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[user] OFF
GO
SET IDENTITY_INSERT [dbo].[user_skill] ON 

INSERT [dbo].[user_skill] ([user_skill_id], [user_id], [skill_id], [created_at], [updated_at], [deleted_at]) VALUES (10004, 15, 4, CAST(N'2023-04-21T14:30:44.6245229+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[user_skill] ([user_skill_id], [user_id], [skill_id], [created_at], [updated_at], [deleted_at]) VALUES (10005, 15, 6, CAST(N'2023-04-21T14:30:44.6247885+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[user_skill] ([user_skill_id], [user_id], [skill_id], [created_at], [updated_at], [deleted_at]) VALUES (10006, 15, 11, CAST(N'2023-04-21T14:30:44.6247897+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[user_skill] ([user_skill_id], [user_id], [skill_id], [created_at], [updated_at], [deleted_at]) VALUES (10007, 4, 2, CAST(N'2023-04-24T10:47:37.2885416+05:30' AS DateTimeOffset), NULL, NULL)
INSERT [dbo].[user_skill] ([user_skill_id], [user_id], [skill_id], [created_at], [updated_at], [deleted_at]) VALUES (10008, 4, 12, CAST(N'2023-04-24T10:47:37.2889185+05:30' AS DateTimeOffset), NULL, NULL)
SET IDENTITY_INSERT [dbo].[user_skill] OFF
GO
INSERT [dbo].[verify_email] ([email], [token]) VALUES (N'sunny_milim@gmail.com', N'cc1af10a-d49e-4e4e-98bf-7c50c533f380')
INSERT [dbo].[verify_email] ([email], [token]) VALUES (N'sunnyvachheta26+armin@gmail.com', N'2743ca53-83bb-492e-9eb0-ef7600030807')
INSERT [dbo].[verify_email] ([email], [token]) VALUES (N'sunnyvachheta26+eva@gmail.com', N'6b4ceb7f-4cc8-4597-a644-de769b223ea1')
INSERT [dbo].[verify_email] ([email], [token]) VALUES (N'sunnyvachheta26+jay@gmail.com', N'35a55331-418c-4dae-a047-3d9371d1c35f')
INSERT [dbo].[verify_email] ([email], [token]) VALUES (N'sunnyvachheta26+kishan@gmail.com', N'7bfa436a-23e0-4705-a871-09ef2344e108')
INSERT [dbo].[verify_email] ([email], [token]) VALUES (N'sunnyvachheta26+lis@gmail.com', N'57019648-c8a1-4509-ba42-2766fbf6f619')
INSERT [dbo].[verify_email] ([email], [token]) VALUES (N'sunnyvachheta26+pres@gmail.com', N'9b8b4d2c-118c-4667-80e5-889742cf449a')
INSERT [dbo].[verify_email] ([email], [token]) VALUES (N'sunnyvachheta89+eren@gmail.com', N'b3cdc31b-8480-4c47-9d4d-3b0b3b29b481')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_admin_email]    Script Date: 27-04-2023 14:05:11 ******/
ALTER TABLE [dbo].[admin] ADD  CONSTRAINT [UQ_admin_email] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__cms_page__32DD1E4C53E1E875]    Script Date: 27-04-2023 14:05:11 ******/
ALTER TABLE [dbo].[cms_page] ADD UNIQUE NONCLUSTERED 
(
	[slug] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_skill_name]    Script Date: 27-04-2023 14:05:11 ******/
ALTER TABLE [dbo].[skill] ADD  CONSTRAINT [UQ_skill_name] UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_user_email]    Script Date: 27-04-2023 14:05:11 ******/
ALTER TABLE [dbo].[user] ADD  CONSTRAINT [UQ_user_email] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[admin] ADD  CONSTRAINT [DF_admin_created_at]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[banner] ADD  CONSTRAINT [DF_banner_sort_order]  DEFAULT ((0)) FOR [sort_order]
GO
ALTER TABLE [dbo].[banner] ADD  CONSTRAINT [DF_banner_created_at]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[banner] ADD  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[city] ADD  CONSTRAINT [DF_city_created_at]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[cms_page] ADD  CONSTRAINT [DF_cms_page_status]  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[cms_page] ADD  CONSTRAINT [DF_cms_page_created_at]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[comment] ADD  CONSTRAINT [DF_comment_approval_status]  DEFAULT ((0)) FOR [approval_status]
GO
ALTER TABLE [dbo].[contact_us] ADD  CONSTRAINT [DF_contact_us_status]  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[contact_us] ADD  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[country] ADD  CONSTRAINT [DF_country_created_at]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[goal_mission] ADD  CONSTRAINT [DF_goal_mission_goal_achieved]  DEFAULT ((0)) FOR [goal_achived]
GO
ALTER TABLE [dbo].[mission] ADD  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [dbo].[mission_application] ADD  CONSTRAINT [DF_mission_application_approval_status]  DEFAULT ((1)) FOR [approval_status]
GO
ALTER TABLE [dbo].[mission_media] ADD  CONSTRAINT [DF_mission_media_default]  DEFAULT ((0)) FOR [default]
GO
ALTER TABLE [dbo].[mission_skill] ADD  CONSTRAINT [DF_mission_skill]  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[mission_theme] ADD  CONSTRAINT [DF_mission_theme]  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[password_reset] ADD  CONSTRAINT [DF_password_reset_created_at]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[skill] ADD  CONSTRAINT [DF_skill_status]  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[skill] ADD  CONSTRAINT [DF_skill_created_at]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[story] ADD  CONSTRAINT [DF_story_status]  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[story] ADD  DEFAULT ((0)) FOR [is_deleted]
GO
ALTER TABLE [dbo].[timesheet] ADD  CONSTRAINT [DF_timesheet_status]  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[user] ADD  CONSTRAINT [DF_user_status]  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[user] ADD  CONSTRAINT [DF_user_created_at]  DEFAULT (getdate()) FOR [created_at]
GO
ALTER TABLE [dbo].[city]  WITH CHECK ADD  CONSTRAINT [FK_city_country] FOREIGN KEY([country_id])
REFERENCES [dbo].[country] ([country_id])
GO
ALTER TABLE [dbo].[city] CHECK CONSTRAINT [FK_city_country]
GO
ALTER TABLE [dbo].[comment]  WITH CHECK ADD  CONSTRAINT [FK_comment_mission] FOREIGN KEY([mission_id])
REFERENCES [dbo].[mission] ([mission_id])
GO
ALTER TABLE [dbo].[comment] CHECK CONSTRAINT [FK_comment_mission]
GO
ALTER TABLE [dbo].[comment]  WITH CHECK ADD  CONSTRAINT [FK_comment_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[comment] CHECK CONSTRAINT [FK_comment_user]
GO
ALTER TABLE [dbo].[contact_us]  WITH CHECK ADD  CONSTRAINT [FK_contact_us_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[contact_us] CHECK CONSTRAINT [FK_contact_us_user]
GO
ALTER TABLE [dbo].[favourite_mission]  WITH CHECK ADD  CONSTRAINT [FK_favourite_mission_mission] FOREIGN KEY([mission_id])
REFERENCES [dbo].[mission] ([mission_id])
GO
ALTER TABLE [dbo].[favourite_mission] CHECK CONSTRAINT [FK_favourite_mission_mission]
GO
ALTER TABLE [dbo].[favourite_mission]  WITH CHECK ADD  CONSTRAINT [FK_favourite_mission_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[favourite_mission] CHECK CONSTRAINT [FK_favourite_mission_user]
GO
ALTER TABLE [dbo].[goal_mission]  WITH CHECK ADD  CONSTRAINT [FK_goal_mission_mission] FOREIGN KEY([mission_id])
REFERENCES [dbo].[mission] ([mission_id])
GO
ALTER TABLE [dbo].[goal_mission] CHECK CONSTRAINT [FK_goal_mission_mission]
GO
ALTER TABLE [dbo].[mission]  WITH CHECK ADD  CONSTRAINT [FK_mission_city] FOREIGN KEY([city_id])
REFERENCES [dbo].[city] ([city_id])
GO
ALTER TABLE [dbo].[mission] CHECK CONSTRAINT [FK_mission_city]
GO
ALTER TABLE [dbo].[mission]  WITH CHECK ADD  CONSTRAINT [FK_mission_country] FOREIGN KEY([country_id])
REFERENCES [dbo].[country] ([country_id])
GO
ALTER TABLE [dbo].[mission] CHECK CONSTRAINT [FK_mission_country]
GO
ALTER TABLE [dbo].[mission]  WITH CHECK ADD  CONSTRAINT [FK_mission_mission_theme] FOREIGN KEY([theme_id])
REFERENCES [dbo].[mission_theme] ([theme_id])
GO
ALTER TABLE [dbo].[mission] CHECK CONSTRAINT [FK_mission_mission_theme]
GO
ALTER TABLE [dbo].[mission_application]  WITH CHECK ADD  CONSTRAINT [FK_mission_application_mission] FOREIGN KEY([mission_id])
REFERENCES [dbo].[mission] ([mission_id])
GO
ALTER TABLE [dbo].[mission_application] CHECK CONSTRAINT [FK_mission_application_mission]
GO
ALTER TABLE [dbo].[mission_application]  WITH CHECK ADD  CONSTRAINT [FK_mission_application_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[mission_application] CHECK CONSTRAINT [FK_mission_application_user]
GO
ALTER TABLE [dbo].[mission_document]  WITH CHECK ADD  CONSTRAINT [FK_mission_document_mission] FOREIGN KEY([mission_id])
REFERENCES [dbo].[mission] ([mission_id])
GO
ALTER TABLE [dbo].[mission_document] CHECK CONSTRAINT [FK_mission_document_mission]
GO
ALTER TABLE [dbo].[mission_invite]  WITH CHECK ADD  CONSTRAINT [FK_mission_invite_mission] FOREIGN KEY([mission_id])
REFERENCES [dbo].[mission] ([mission_id])
GO
ALTER TABLE [dbo].[mission_invite] CHECK CONSTRAINT [FK_mission_invite_mission]
GO
ALTER TABLE [dbo].[mission_invite]  WITH CHECK ADD  CONSTRAINT [FK_mission_invite_user] FOREIGN KEY([from_user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[mission_invite] CHECK CONSTRAINT [FK_mission_invite_user]
GO
ALTER TABLE [dbo].[mission_invite]  WITH CHECK ADD  CONSTRAINT [FK_mission_invite_user2] FOREIGN KEY([to_user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[mission_invite] CHECK CONSTRAINT [FK_mission_invite_user2]
GO
ALTER TABLE [dbo].[mission_media]  WITH CHECK ADD  CONSTRAINT [FK_mission_media_mission] FOREIGN KEY([mission_id])
REFERENCES [dbo].[mission] ([mission_id])
GO
ALTER TABLE [dbo].[mission_media] CHECK CONSTRAINT [FK_mission_media_mission]
GO
ALTER TABLE [dbo].[mission_rating]  WITH CHECK ADD  CONSTRAINT [FK_mission_rating_mission] FOREIGN KEY([mission_id])
REFERENCES [dbo].[mission] ([mission_id])
GO
ALTER TABLE [dbo].[mission_rating] CHECK CONSTRAINT [FK_mission_rating_mission]
GO
ALTER TABLE [dbo].[mission_rating]  WITH CHECK ADD  CONSTRAINT [FK_mission_rating_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[mission_rating] CHECK CONSTRAINT [FK_mission_rating_user]
GO
ALTER TABLE [dbo].[mission_skill]  WITH CHECK ADD  CONSTRAINT [FK_mission_skill_mission] FOREIGN KEY([mission_id])
REFERENCES [dbo].[mission] ([mission_id])
GO
ALTER TABLE [dbo].[mission_skill] CHECK CONSTRAINT [FK_mission_skill_mission]
GO
ALTER TABLE [dbo].[mission_skill]  WITH CHECK ADD  CONSTRAINT [FK_mission_skill_skill] FOREIGN KEY([skill_id])
REFERENCES [dbo].[skill] ([skill_id])
GO
ALTER TABLE [dbo].[mission_skill] CHECK CONSTRAINT [FK_mission_skill_skill]
GO
ALTER TABLE [dbo].[story]  WITH CHECK ADD  CONSTRAINT [FK_story_mission] FOREIGN KEY([mission_id])
REFERENCES [dbo].[mission] ([mission_id])
GO
ALTER TABLE [dbo].[story] CHECK CONSTRAINT [FK_story_mission]
GO
ALTER TABLE [dbo].[story]  WITH CHECK ADD  CONSTRAINT [FK_story_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[story] CHECK CONSTRAINT [FK_story_user]
GO
ALTER TABLE [dbo].[story_invite]  WITH CHECK ADD  CONSTRAINT [FK_story_invite_story] FOREIGN KEY([story_id])
REFERENCES [dbo].[story] ([story_id])
GO
ALTER TABLE [dbo].[story_invite] CHECK CONSTRAINT [FK_story_invite_story]
GO
ALTER TABLE [dbo].[story_invite]  WITH CHECK ADD  CONSTRAINT [FK_story_invite_user] FOREIGN KEY([from_user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[story_invite] CHECK CONSTRAINT [FK_story_invite_user]
GO
ALTER TABLE [dbo].[story_invite]  WITH CHECK ADD  CONSTRAINT [FK_story_invite_user2] FOREIGN KEY([to_user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[story_invite] CHECK CONSTRAINT [FK_story_invite_user2]
GO
ALTER TABLE [dbo].[story_media]  WITH CHECK ADD  CONSTRAINT [FK_story_media_story] FOREIGN KEY([story_id])
REFERENCES [dbo].[story] ([story_id])
GO
ALTER TABLE [dbo].[story_media] CHECK CONSTRAINT [FK_story_media_story]
GO
ALTER TABLE [dbo].[timesheet]  WITH CHECK ADD  CONSTRAINT [FK_timesheet_mission] FOREIGN KEY([mission_id])
REFERENCES [dbo].[mission] ([mission_id])
GO
ALTER TABLE [dbo].[timesheet] CHECK CONSTRAINT [FK_timesheet_mission]
GO
ALTER TABLE [dbo].[timesheet]  WITH CHECK ADD  CONSTRAINT [FK_timesheet_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[timesheet] CHECK CONSTRAINT [FK_timesheet_user]
GO
ALTER TABLE [dbo].[user]  WITH CHECK ADD  CONSTRAINT [FK_user_city] FOREIGN KEY([city_id])
REFERENCES [dbo].[city] ([city_id])
GO
ALTER TABLE [dbo].[user] CHECK CONSTRAINT [FK_user_city]
GO
ALTER TABLE [dbo].[user]  WITH CHECK ADD  CONSTRAINT [FK_user_country] FOREIGN KEY([country_id])
REFERENCES [dbo].[country] ([country_id])
GO
ALTER TABLE [dbo].[user] CHECK CONSTRAINT [FK_user_country]
GO
ALTER TABLE [dbo].[user_skill]  WITH CHECK ADD  CONSTRAINT [FK_user_skill_skill] FOREIGN KEY([skill_id])
REFERENCES [dbo].[skill] ([skill_id])
GO
ALTER TABLE [dbo].[user_skill] CHECK CONSTRAINT [FK_user_skill_skill]
GO
ALTER TABLE [dbo].[user_skill]  WITH CHECK ADD  CONSTRAINT [FK_user_skill_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([user_id])
GO
ALTER TABLE [dbo].[user_skill] CHECK CONSTRAINT [FK_user_skill_user]
GO
USE [master]
GO
ALTER DATABASE [CI_PLATFORM] SET  READ_WRITE 
GO
