# Book Information API

## Overview

The **Book Information API** is a backend API developed using **ASP.NET Core**, which allows users to manage information about books. The API supports CRUD operations (Create, Read, Update, Delete) on book data stored in a **SQL Server** database. This project includes SQL scripts to create the necessary database tables and stored procedures for efficient data operations.

### Key Features:
- **Create a book**: Add a new book information to the database
- **Get All Books**: Retrieve all books from the database.
- **Get Book by ID**: Retrieve a book from the database by ID.
- **Update Book Information**: Modify details of a specific book.
- **Delete Book**: Remove a book from the database.
- **Stored Procedures**: Used for database interaction to enhance performance and security.
- **SQL Table Creation**: SQL script to create the required database table for the project.

---

## Project Structure

- **Controllers**: Contains the `BookInfoController.cs`, which manages API requests, including methods for getting all books, updating, and deleting book records.
- **Models**: Contains the data model classes like `Book.cs` and `BookInfoDTO.cs` that define the structure of the data being managed.
- **SQL Scripts**:
  - `CreateTable.sql`: This SQL script creates the necessary table in the database to store book information.
  - `StoredProcedure.sql`: This script contains stored procedures that the API uses to interact with the database (e.g., procedures for getting and updating book data).

---

## Setup Instructions

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/CodeWithMastu/Book-Information-API.git
   cd Book-Information-API
2. **Database Setup**:

- Run the `CreateTable.sql` script in your SQL Server instance to create the `Books` table.
- Execute the stored procedures in `StoredProcedure.sql` to add the necessary database functionalities.

3. **Configuration**:

- Update the `appsettings.json` file with your SQL Server connection string.

4. **Run the API**:

1. Open the project in your preferred IDE.
2. Build and run the solution to start the API on `localhost`.

## Endpoints

- **POST /CreateBook**: Create a book
- **GET /GetAllBooks**: Retrieve all books.
- **GET /GetBookbyID/{ID}**: Retrieve a book by its ID.
- **PUT /Updatebook/{ID}**: Update a specific book’s information.
- **DELETE /Deletebook/{ID}**: Delete a book from the database.

## Dependencies

- **ASP.NET Core**
- **Dapper**: For interacting with the SQL Server database.
- **SQL Server**: Used for data storage.
