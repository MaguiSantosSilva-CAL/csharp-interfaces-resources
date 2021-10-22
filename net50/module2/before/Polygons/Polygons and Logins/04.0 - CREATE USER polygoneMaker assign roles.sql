USE maguiss
GO

/****** Object:  User [lowestPrivilege]    Script Date: 22/09/2021 17:03:52 ******/
CREATE USER [polygonMaker] FOR LOGIN [polygonMaker] WITH DEFAULT_SCHEMA=[polygon]
GO
ALTER ROLE [db_datareader] ADD MEMBER [polygonMaker]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [polygonMaker]
GO