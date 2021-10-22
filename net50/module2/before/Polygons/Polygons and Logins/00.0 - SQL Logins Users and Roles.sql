USE [master]
GO
DROP LOGIN [polygonSystem] 
CREATE LOGIN [polygonSystem] WITH PASSWORD=N'BareMinimum1', DEFAULT_DATABASE=[maguiss], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF

USE [maguiss]
GO

DROP USER [polygonSystem]
CREATE USER [polygonSystem] 
ALTER USER [polygonSystem] WITH DEFAULT_SCHEMA=[user] 
GO

USE [maguiss]
GO
ALTER AUTHORIZATION ON SCHEMA::[user] TO [dbo]
DROP APPLICATION ROLE  [polygonApplication]
CREATE APPLICATION ROLE [polygonApplication] WITH DEFAULT_SCHEMA = [user], PASSWORD = N'Apazopaohabitacao1'
ALTER AUTHORIZATION ON SCHEMA::[user] TO [polygonApplication]
GO

