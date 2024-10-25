-- STORE PROCEDURES FOR BOOK INFORMATION DATABASE

-- Add books to the database
CREATE PROCEDURE AddBook
    @BookName NVARCHAR(100),
    @Author NVARCHAR(100),
    @ISBN NVARCHAR(100),
    @YearPublished BIGINT
AS
BEGIN
    INSERT INTO BookInfo (BookName, Author, ISBN, YearPublished, Is_Deleted, Created_At, Updated_At, Deleted_At)
    VALUES (@BookName, @Author, @ISBN, @YearPublished, 0, GETDATE(), NULL, NULL);
END;

-- Read/Get all the books from the database

CREATE PROCEDURE GetAllBooks
AS
BEGIN
    SELECT * FROM BookInfo WHERE Is_Deleted = 0;
END;

-- Read/ Get the books by ID

CREATE PROCEDURE GetBookByID
    @ID BIGINT
AS
BEGIN
    SELECT * FROM BookInfo WHERE ID = @ID AND Is_Deleted = 0;
END;

-- Update the books in the database by ID
CREATE PROCEDURE UpdateBook
    @ID BIGINT,
    @BookName NVARCHAR(100),
    @Author NVARCHAR(100),
    @ISBN NVARCHAR(100),
    @YearPublished BIGINT,
	@Updated_At DATETIME
AS
BEGIN
    UPDATE BookInfo
    SET BookName = @BookName,
        Author = @Author,
        ISBN = @ISBN,
        YearPublished = @YearPublished,
        Updated_At = @Updated_At
    WHERE ID = @ID AND Is_Deleted = 0;
END;

-- Delete the book
CREATE PROCEDURE DeleteBook
	@ID BIGINT
AS
BEGIN
    UPDATE BookInfo
    SET Is_Deleted = 1,
        Deleted_At = GETDATE()
    WHERE ID = @ID;
END;


/*EXEC sp_helptext 'GetAllBooks'; (CHECK FOR "GetAllBooks" STORED PROCEDURE IN THE DATABASE */
