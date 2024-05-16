using Microsoft.AspNetCore.Mvc;
using ObecnaKniznicaAPI.Services;
using ObecnaKniznicaLogic.DataModels;
using ObecnaKniznicaLogic.Models;

namespace ObecnaKniznicaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]                              // robi validaciu parametrov modelu
    public class BookController : ControllerBase // trieda 'ControllerBase' sa dedi pre Web APIs, 'Controller' sa dedi pre Web aplikacie
    {
        private readonly IBookService service;

        public BookController(IBookService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ActionResult<Response>> AddBookAsync(Book bookModel)
        {
            if (bookModel is null)
                return BadRequest();
            return Ok(await service.AddBookAsync(bookModel));
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooksAsync()
        {
            var books = await service.GetBooksAsync();
            if (books is null)
                return BadRequest( new Response { Success = false, Message = "No data found" });
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Book>> GetBookByIdAsync(int id)
        {
            if (id <= 0)
                return BadRequest( new Response { Success = false, Message = $"Given id: {id} is invalid for data query."});

            var book = await service.GetBookByIdAsync(id);
            if (book is null)
                return BadRequest(new Response { Success = false, Message = $"Book (id={id}) was not found." });

            return Ok(book);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Response>> DeleteBookAsync(int id)
        {
            if (id <= 0)
                return BadRequest(new Response { Success = false, Message = $"Given id: {id} is invalid for data query." });

            return Ok( await service.DeleteBookAsync(id));
        }
    }
}


