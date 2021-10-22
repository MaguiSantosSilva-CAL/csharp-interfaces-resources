USE maguiss
GO

select * from polygon.users	
select * from polygon.sessionID

DECLARE @FIRST  varbinary(100) = (select polygon.fn_stringToPassword('LetMeIn'))
DECLARE @SECOND varbinary(100) = (select polygon.fn_stringToPassword('letmein'))

DECLARE @pwd_nvarchar nvarchar(100) = 'letmein'
DECLARE @pwd_varchar   varchar(100) = 'letmein'
DECLARE @VARIABLE varbinary(64)		= convert(varbinary(64),@pwd_nvarchar)
SELECT  @VARIABLE as variable, convert(varbinary(64),'letmein') as pwd_varchar, convert(varbinary(64),@pwd_nvarchar) as pwd_nvarchar

select convert(nvarchar(100),pwd) as converted, convert(varbinary(64),cast('letmein' as nvarchar)) as letmein, * from polygon.users --where convert(varbinary(64),pwd) = convert(varbinary(64),'letmein')

IF (@FIRST = @SECOND) 
BEGIN
	SELECT 'true'
END
ELSE
	SELECT 'false'
					  
--select *
--from polygon.catalogue