USE maguiss
GO

IF EXISTS(Select name from sys.objects where object_id = object_id('polygon.users')) DROP TABLE polygon.users
GO

CREATE TABLE [polygon].[users](
	 [id]				[int]			IDENTITY(0,1) UNIQUE NOT NULL
	,[username]			[nvarchar](40)	NOT NULL
	,[pwd]				[varbinary](32)	NULL
	,[information]		[nvarchar](500)	NULL
	,[dateCreate]		datetime		default current_timestamp
	,[dateLastLogin]	datetime 
	,[loginSuccessful]	tinyint			default 0
	,[sessionID]		int				

 CONSTRAINT [PK_MyUsers] PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

DECLARE @PasswordForUser1 nvarchar(100) = 'hello'
DECLARE @PasswordForUser2 nvarchar(100) = 'letmein'
DECLARE @PasswordForUser3 nvarchar(100) = 'LetMeIn'
											 
insert into polygon.users(username,pwd,information) values ('Admin',	polygon.fn_stringToPassword(@PasswordForUser1 ),'This information is in the database.')
insert into polygon.users(username,pwd,information) values ('Magui',	polygon.fn_stringToPassword(@PasswordForUser2 ),'Successfully retrieved this from the db')
insert into polygon.users(username,pwd,information) values ('NewUser',	polygon.fn_stringToPassword(@PasswordForUser3 ),'Successfully retrieved this from the db')

select * from polygon.users
GO
						
BEGIN /* Grant Permissions */

	GRANT DELETE ON		[polygon].[catalogue] TO [polygonSys]
	GRANT INSERT ON		[polygon].[catalogue] TO [polygonSys]
	GRANT REFERENCES ON	[polygon].[catalogue] TO [polygonSys]
	GRANT SELECT ON		[polygon].[catalogue] TO [polygonSys]
	GRANT UPDATE ON		[polygon].[catalogue] TO [polygonSys]
	GRANT SELECT ON		[polygon].[users]	  TO [polygonSys]
	GRANT UPDATE ON		[polygon].[users]	  TO [polygonSys]

END /* Permissions */