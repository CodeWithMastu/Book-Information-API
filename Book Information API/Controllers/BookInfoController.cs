using Book_Information_API.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace Book_Information_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookInfoController : ControllerBase
    {
        private readonly string connectionString;

        public BookInfoController(IConfiguration configuration)
        {
            connectionString = configuration["ConnectionStrings:DefaultConnection"] ?? "";
        }

        // Create Book
        [HttpPost("Createbook")]
        public async Task<IActionResult> CreateBookInfo(BookInfoDTO bookInfoDTO)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                var parameters = new DynamicParameters();
                parameters.Add("@BookName", bookInfoDTO.BookName);
                parameters.Add("@Author", bookInfoDTO.Author);
                parameters.Add("@ISBN", bookInfoDTO.ISBN);
                parameters.Add("@YearPublished", bookInfoDTO.YearPublished);
               // parameters.Add("Created_At", DateTime.Now);

                await connection.ExecuteAsync("AddBook", parameters,
                    commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                return BadRequest(new {message = $"Sorry, we encountered an error: {ex.Message}"});
            }
            return Ok("Book Information added successfully");
        }

        // Get All Books
        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            List<Book> books = new List<Book>();
            try
            {
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                var results = await connection.QueryAsync<Book>("GetAllBooks",
                    commandType: CommandType.StoredProcedure);
                books = results.ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Sorry, we encountered an error: {ex.Message}" });
            }
            return Ok(books);
        }

        // Get book by ID
        [HttpGet("GetBookbyID")]
        public async Task<IActionResult> GetBookbyID([FromQuery] long ID)
        {
            try
            {
                using var connection = new SqlConnection(connectionString); 
                await connection.OpenAsync();

                var parameter = new DynamicParameters();
                parameter.Add("@ID", ID);

                var book = await connection.QueryFirstOrDefaultAsync<BookInfoDTO>("GetBookbyID", parameter,
                    commandType: CommandType.StoredProcedure);
                if (book == null)
                {
                    return NotFound();
                }

                return Ok(book);
            }
            catch (Exception ex) 
            {
                return BadRequest(new { message = $"Sorry, we encountered an error: {ex.Message}" });
            }
        }

        // Update book
        [HttpPut("Updatebook")]
        public async Task<IActionResult> UpdateBook(long ID, BookInfoDTO bookInfoDTO)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                // Check if the book exists before updating
                var existingBook = await connection.QueryFirstOrDefaultAsync<Book>(
                    "GetBookByID",
                    new { ID },
                    commandType: CommandType.StoredProcedure);

                if (existingBook == null)
                {
                    return NotFound($"No book found with ID = {ID}");
                }

                var parameters = new DynamicParameters();
                parameters.Add("@ID", ID);
                parameters.Add("@BookName", bookInfoDTO.BookName);
                parameters.Add("@Author", bookInfoDTO.Author);
                parameters.Add("@ISBN", bookInfoDTO.ISBN);
                parameters.Add("@YearPublished", bookInfoDTO.YearPublished);
                parameters.Add("@Updated_At", DateTime.Now);

                await connection.ExecuteAsync("UpdateBook", parameters,
                    commandType: CommandType.StoredProcedure);

                return Ok("Book updated successfully");
            }
            catch(Exception ex) 
            {
                return BadRequest(new { message = $"Sorry, we encountered an error: {ex.Message}" });
            }
        }

        // Delete a book
        [HttpDelete("DeleteBook")]
        public async Task<IActionResult> DeleteBook(long ID)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                // Check if the book exists before deleting
                var existingBook = await connection.QueryFirstOrDefaultAsync<Book>(
                    "GetBookByID",
                    new { ID },
                    commandType: CommandType.StoredProcedure);

                if (existingBook == null)
                {
                    return NotFound($"No book found with ID = {ID}");
                }

                await connection.ExecuteAsync("DeleteBook", new {ID},
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex) 
            {
                return BadRequest(new { message = $"Sorry, we encountered an error: {ex.Message}" });
            }
            return Ok("Book deleted successfully");
        }
    }
}
