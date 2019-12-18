CREATE TABLE [dbo].[Game]
(
	[Id] NVARCHAR(450) NOT NULL PRIMARY KEY, 
    [GameName] NVARCHAR(200) NULL, 
    [GameCode] NVARCHAR(50) NULL, 
    [Game_Description] NVARCHAR(MAX) NULL, 
    [GamePoint] NVARCHAR(10) NULL, 
    [GameType] NVARCHAR(50) NULL, 
    [GameOrigin] NVARCHAR(50) NULL
)
