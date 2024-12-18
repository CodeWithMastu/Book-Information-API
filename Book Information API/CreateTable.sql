CREATE TABLE BookInfo (
	ID BIGINT NOT NULL PRIMARY KEY IDENTITY (1,1),
	BookName NVARCHAR(100) NOT NULL,
	Author NVARCHAR(100) NOT NULL,
	ISBN NVARCHAR(100) NOT NULL,
	YearPublished BIGINT NOT NULL,
	Created_At DATETIME DEFAULT GETDATE(),
	Updated_At DATETIME DEFAULT GETDATE(),
	Deleted_At DATETIME DEFAULT GETDATE(),
	Is_Deleted BIT NOT NULL
);
