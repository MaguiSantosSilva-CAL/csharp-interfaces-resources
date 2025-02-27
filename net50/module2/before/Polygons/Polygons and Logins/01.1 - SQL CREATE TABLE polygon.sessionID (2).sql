USE MAGUISS
GO

if exists (select name from sys.objects where name = 'sessionID' and schema_id = schema_id('polygon'))
begin
	drop table polygon.sessionID
	create table polygon.sessionID (
		 id			int					identity (0,1)	 Primary Key
		,sessionID	uniqueidentifier	default newid()
		,username	nvarchar(40)
	)
end
 
insert into polygon.sessionID(username) values ('seed')

if exists (select name from sys.objects where name = 'sessionActivity' and schema_id = schema_id('polygon'))
begin
	drop table polygon.sessionActivity
	create table polygon.sessionActivity (
		sessionID							uniqueidentifier
		,created							datetime	 
		,lastAccountActivityDuringSession	datetime	 default current_timestamp
		,eventType							int			 default 1				
		,eventInformation					varchar(100) default ''  
	)
end

SELECT * from polygon.sessionID
select * from polygon.users	

