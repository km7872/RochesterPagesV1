CREATE TABLE [dbo].[Directory_Tag_Mapping] (
    [DTM_ID]           VARCHAR (12) NOT NULL,
    [DTM_Directory_ID] VARCHAR (12) NOT NULL FOREIGN KEY (DTM_Directory_ID) REFERENCES Directory(D_ID)  ,
    [DTM_Tag_ID]       VARCHAR (12) NOT NULL,
    PRIMARY KEY CLUSTERED ([DTM_ID] ASC)
);
