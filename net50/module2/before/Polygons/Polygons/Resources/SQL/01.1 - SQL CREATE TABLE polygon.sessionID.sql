USE MAGUISS
GO

if not exists (select name from sys.objects where name = 'sessionID' and schema_id = schema_id('polygon'))
create table polygon.sessionID (
	sessionID int identity (0,1),
	username nvarchar(40)
)

SELECT * from polygon.sessionID