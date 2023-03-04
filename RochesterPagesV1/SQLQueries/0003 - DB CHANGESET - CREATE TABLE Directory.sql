CREATE TABLE [dbo].[Directory] (
    [D_ID]         VARCHAR (12)  NOT NULL,
    [D_Address]    VARCHAR (MAX) NULL,
    [D_Number]     VARCHAR (10)  NULL,
    [D_Name]       VARCHAR (50)  NULL,
    [D_CreatedBy]  VARCHAR (12)  NULL ,
    [D_CreatedOn]  DATETIME      DEFAULT (getdate()) NOT NULL,
    [D_UpdatedBy]  DATETIME      NULL ,
    [D_UpdatedOn]  DATETIME      DEFAULT (getdate()) NOT NULL,
    [D_ApprovedBy] VARCHAR (12)  NULL,
    [D_ApprovedOn] DATETIME      NULL,
    [D_IsApproved] BIT NOT NULL DEFAULT 0, 
    PRIMARY KEY CLUSTERED ([D_ID] ASC)
);

