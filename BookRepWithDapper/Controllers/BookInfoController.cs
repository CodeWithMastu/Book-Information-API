using BookRepWithDapper.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace BookRepWithDapper.Controllers
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

        // CREATE BOOK
        [HttpPost]
        public async Task<IActionResult> CreateBook(BookInfoDTO bookInfoDTO)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("BookName", bookInfoDTO.BookName);
                parameters.Add("Author", bookInfoDTO.Author);
                parameters.Add("ISBN", bookInfoDTO.ISBN);
                parameters.Add("YearPublished", bookInfoDTO.YearPublished);

                await connection.ExecuteAsync("AddBook", parameters,
                    commandType: CommandType.StoredProcedure);
                //connection.Close();
                return Ok("Book created successfully");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Book", $"Sorry, but we have an exception {ex.Message}");
                return BadRequest(ModelState);
            }
        }
        // GET ALL BOOKS
        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            List<Book> books = [];
            try
            {
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                //string sql = "SELECT * FROM BookInfo";
                var results = await connection.QueryAsync<Book>("GetAllBooks",
                    commandType: CommandType.StoredProcedure);
                books = results.ToList();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Book", $"Sorry, but we have an exception {ex.Message}");
                return BadRequest(ModelState);
            }
            return Ok(books);
        }

        // GET BOOK BY ID
        [HttpGet("GetBookbyID")]
        public async Task<IActionResult> GetBookbyID([FromQuery] int ID)
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();

                var parameter = new DynamicParameters();
                parameter.Add("@ID", ID);

                var book = await connection.QueryFirstOrDefaultAsync<BookInfoDTO>("GetBookByID", parameter,
                    commandType: CommandType.StoredProcedure);

                if (book == null)
                {
                    return NotFound();
                }

                return Ok(book);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Book", $"Sorry, we have an exception {ex.Message}");
                return BadRequest(ModelState);
            }

        }

        // UPDATE BOOK
        [HttpPut("UpdateBook/{ID}")]
        public async Task<IActionResult> UpdateBook(int ID, BookInfoDTO bookInfoDTO)
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
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Book", $"Sorry, but we have an exception {ex.Message}");
                return BadRequest(ModelState);
            }
            return Ok("Book updated successfully");

        }


        // DELETE BOOK
        [HttpDelete("DeleteBook/{ID}")]
        public async Task<IActionResult> DeleteBook(int ID)
        {
            try
            {
                using var connection = new SqlConnection(connectionString); 
                await connection.OpenAsync();

                var parameter = new DynamicParameters();
                parameter.Add("@ID", ID);

                await connection.ExecuteAsync("DeleteBook", parameter,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Book", $"Sorry, but we have an exception {ex.Message}");
                return BadRequest(ModelState);
            }
            return Ok("Book deleted successfully :)");    
        }
    }
}

