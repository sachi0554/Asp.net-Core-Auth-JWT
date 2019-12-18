CREATE TABLE [dbo].[RefreshTokens] (
    [Token]        NVARCHAR (450) NOT NULL,
    [JwtId]        NVARCHAR (MAX) NULL,
    [CreationDate] DATETIME2 (7)  NOT NULL,
    [ExpiryDate]   DATETIME2 (7)  NOT NULL,
    [Used]         BIT            NOT NULL,
    [Invalidated]  BIT            NOT NULL,
    [UserId]       NVARCHAR (450) NULL,
    [Id] NCHAR(50) NULL, 
    CONSTRAINT [PK_RefreshTokens] PRIMARY KEY CLUSTERED ([Token] ASC),
    CONSTRAINT [FK_RefreshTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_RefreshTokens_UserId]
    ON [dbo].[RefreshTokens]([UserId] ASC);

