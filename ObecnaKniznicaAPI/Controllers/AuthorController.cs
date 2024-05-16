using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObecnaKniznicaAPI.Services;
using ObecnaKniznicaLogic.DataModels;
using ObecnaKniznicaLogic.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace ObecnaKniznicaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService service;

        public AuthorController(IAuthorService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<Author>>> GetAuthorsAsync()
        {
            var authors = await service.GetAuthorsAsync();
            if (authors is null)
                return StatusCode(StatusCodes.Status204NoContent, new Response { Success = false, Message = "No author data found" });
            return Ok(authors);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Author>> GetAuthorByIdAsync(int id)
        {
            if (id <= 0)
                return BadRequest(new Response { Success = false, Message = $"Invalid query parameter for ID (id={id})" } );
            else
            {
                var author = await service.GetAuthorByIdAsync(id);
                if (author is null)
                    return BadRequest(new Response { Success = false, Message = $"No author data found for id={id}" });
                return Ok(author);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Response>> AddAuthorAsync(Author authorModel)
        {
            if (authorModel is null)
                return BadRequest(new Response { Success = false, Message = "Data for creation of author was not provided" });

            return Ok(await service.AddAuthorAsync(authorModel));
        }

    }
}
