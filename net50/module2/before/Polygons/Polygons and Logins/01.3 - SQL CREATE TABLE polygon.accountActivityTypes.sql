USE MAGUISS
GO

drop table polygon.accountActivityTypes
CREATE TABLE polygon.accountActivityTypes (

	id				 int			identity(0,1) 
   ,eventType		 nvarchar(50)	not null
   ,eventDescription nvarchar(250)
   
 CONSTRAINT [PK_accountActivityTypes] PRIMARY KEY CLUSTERED 
(
	id, eventType
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


insert into polygon.accountActivityTypes(eventType,eventDescription) 
values  ('','')
	   ,('login','User attempts login')
	   ,('logout','User logs out')

select * from polygon.accountActivityTypes

