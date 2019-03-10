CREATE DATABASE [EnrollmentCommandDB]
GO
USE [EnrollmentCommandDB]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Course](
	[CourseID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Maximum] [int] NOT NULL,
	[TeacherID] [bigint] NOT NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[TeacherID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,	
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[TeacherID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enrollment](
	[EnrollmentID] [bigint] IDENTITY(1,1) NOT NULL,
	[StudentID] [bigint] NOT NULL,
	[CourseID] [bigint] NOT NULL	
 CONSTRAINT [PK_Enrollment] PRIMARY KEY CLUSTERED 
(
	[EnrollmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[StudentID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Age] [int] NOT NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Course] ON 

GO
INSERT [dbo].[Course] ([CourseID], [Name], [Maximum], [TeacherID]) VALUES (1, N'Calculus', 3, 1)
GO
INSERT [dbo].[Course] ([CourseID], [Name], [Maximum], [TeacherID]) VALUES (2, N'Chemistry', 2, 2)
GO
INSERT [dbo].[Course] ([CourseID], [Name], [Maximum], [TeacherID]) VALUES (3, N'Composition', 3, 3)
GO
SET IDENTITY_INSERT [dbo].[Course] OFF
GO


SET IDENTITY_INSERT [dbo].[Teacher] ON 
GO
INSERT [dbo].[Teacher] ([TeacherID], [Name]) VALUES (1, N'Son')
GO
INSERT [dbo].[Teacher] ([TeacherID], [Name]) VALUES (2, N'Kim')
GO
INSERT [dbo].[Teacher] ([TeacherID], [Name]) VALUES (3, N'Park')
GO
SET IDENTITY_INSERT [dbo].[Teacher] OFF
GO


SET IDENTITY_INSERT [dbo].[Enrollment] ON 
GO
INSERT [dbo].[Enrollment] ([EnrollmentID], [StudentID], [CourseID]) VALUES (5, 2, 2)
GO
INSERT [dbo].[Enrollment] ([EnrollmentID], [StudentID], [CourseID]) VALUES (13, 2, 3)
GO
INSERT [dbo].[Enrollment] ([EnrollmentID], [StudentID], [CourseID]) VALUES (20, 1, 1)
GO
INSERT [dbo].[Enrollment] ([EnrollmentID], [StudentID], [CourseID]) VALUES (38, 1, 2)
GO
SET IDENTITY_INSERT [dbo].[Enrollment] OFF
GO


SET IDENTITY_INSERT [dbo].[Student] ON 
GO
INSERT [dbo].[Student] ([StudentID], [Name], [Email], [Age]) VALUES (1, N'Alice', N'alice@gmail.com', 20)
GO
INSERT [dbo].[Student] ([StudentID], [Name], [Email], [Age]) VALUES (2, N'Bob', N'bob@outlook.com', 30)
GO
SET IDENTITY_INSERT [dbo].[Student] OFF


ALTER TABLE [dbo].[Course]  WITH CHECK ADD  CONSTRAINT [FK_Course_Teacher] FOREIGN KEY([TeacherID])
REFERENCES [dbo].[Teacher] ([TeacherID])
GO
ALTER TABLE [dbo].[Course] CHECK CONSTRAINT [FK_Course_Teacher]
GO

ALTER TABLE [dbo].[Enrollment]  WITH CHECK ADD  CONSTRAINT [FK_Enrollment_Course] FOREIGN KEY([CourseID])
REFERENCES [dbo].[Course] ([CourseID])
GO
ALTER TABLE [dbo].[Enrollment] CHECK CONSTRAINT [FK_Enrollment_Course]
GO
ALTER TABLE [dbo].[Enrollment]  WITH CHECK ADD  CONSTRAINT [FK_Enrollment_Student] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Student] ([StudentID])
GO
ALTER TABLE [dbo].[Enrollment] CHECK CONSTRAINT [FK_Enrollment_Student]
GO


CREATE DATABASE [EnrollmentReadDB]
go
USE [EnrollmentReadDB]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[StudentQuery](
	[StudentQueryID] [bigint] IDENTITY(1,1) NOT NULL,
	[Student] [bigint] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Age] [int] NULL,
	[NumberOfEnrollments] [int] NULL,
	[CourseName] [nvarchar](50) NULL,
	[TeacherName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[StudentQueryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
