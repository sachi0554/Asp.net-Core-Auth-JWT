CREATE TABLE [dbo].[UserClaim] (
    [Id]        NVARCHAR (450) NOT NULL,
    [ClaimName] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_UserClaim] PRIMARY KEY CLUSTERED ([Id] ASC)
);

