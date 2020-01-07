CREATE TABLE [dbo].[UserClaim] (
    [Id]        NVARCHAR (450) NOT NULL,
    [ClaimName] NVARCHAR (250) NULL,
    CONSTRAINT [PK_UserClaim] PRIMARY KEY CLUSTERED ([Id] ASC)
);

