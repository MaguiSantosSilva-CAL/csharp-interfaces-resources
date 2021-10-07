use maguiss
go

CREATE TABLE polygon.catalogue (
	id int identity(0,1),
	userReference int,
	polygonName varchar(100),
	sides int,
	length int,
	notes varchar(200)
 CONSTRAINT [PK_catalogue] PRIMARY KEY CLUSTERED 
(
 userReference, polygonName
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
