USE [WorkerCash]
GO
/****** Object:  Table [dbo].[EmployeeCash]    Script Date: 20.03.2023 23:38:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeCash](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[SecondName] [varchar](50) NULL,
	[PodrazdName] [varchar](50) NULL,
	[DateWork] [datetime] NOT NULL,
	[CashSumm] [int] NOT NULL,
	[Experience] [int] NOT NULL,
	[Category] [int] NOT NULL,
	[Post] [varchar](max) NOT NULL,
 CONSTRAINT [PK_EmployeeCash_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
