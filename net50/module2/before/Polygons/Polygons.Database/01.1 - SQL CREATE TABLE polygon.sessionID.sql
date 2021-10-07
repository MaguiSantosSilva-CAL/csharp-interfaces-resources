USE MAGUISS
GO

if exists (select name from sys.objects where name = 'sessionID' and schema_id = schema_id('polygon'))
begin
	drop table polygon.sessionID
	create table polygon.sessionID (
		 iD			int	identity (0,1)	 Primary Key
		,sessionID	uniqueidentifier default newid()
		,username	nvarchar(40)
	)
end

insert into polygon.sessionID(username) values ('seed')

if exists (select name from sys.objects where name = 'sessionActivity' and schema_id = schema_id('polygon'))
begin
	drop table polygon.sessionActivity
	create table polygon.sessionActivity (
		sessionID							int
		,created							datetime	default current_timestamp
		,lastAccountActivityDuringSession	datetime	default current_timestamp
		,eventType							nvarchar	default 'login'										   
	)
end

SELECT * from polygon.sessionID
select * from polygon.users	

select polygon.getNextSessionID()