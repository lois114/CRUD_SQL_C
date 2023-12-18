CREATE TABLE [dbo].[PERSONNAGES]
(
	[Id_Personnages] INT NOT NULL PRIMARY KEY identity (1,1), 
	Nom_Personnage varchar(50),
	[Id_Roles] int,
	[Image] varchar(255),
    CONSTRAINT [FK_PERSONNAGES_ToTable] FOREIGN KEY ([Id_Roles]) REFERENCES ROLES([Id_Roles]) 



)
