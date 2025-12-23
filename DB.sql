-- =============================================
-- Database: CareerGuidance
-- =============================================

USE [CareerGuidance]
GO

-- =============================================
-- Table: Interest
-- =============================================

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Interest] (
    [InterestID] INT NOT NULL,
    [Interest] VARCHAR(50) NULL,
    CONSTRAINT [PK_Interest] PRIMARY KEY CLUSTERED
    (
        [InterestID] ASC
    )
) ON [PRIMARY]
GO

-- =============================================
-- Table: Skills
-- =============================================

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Skills] (
    [Rank] INT NULL,
    [SkillID] INT NOT NULL,
    [Skill] VARCHAR(MAX) NULL,
    [Career] VARCHAR(MAX) NULL,
    [Description] VARCHAR(MAX) NULL,
    [Photo] VARCHAR(MAX) NULL,
    [InterestID] INT NULL,
    CONSTRAINT [PK_Skills] PRIMARY KEY CLUSTERED
    (
        [SkillID] ASC
    )
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

-- =============================================
-- Optional: Foreign Key Relationship
-- =============================================

ALTER TABLE [dbo].[Skills]
ADD CONSTRAINT [FK_Skills_Interest]
FOREIGN KEY ([InterestID])
REFERENCES [dbo].[Interest] ([InterestID])
GO
