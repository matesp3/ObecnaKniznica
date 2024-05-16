using ObecnaKniznicaLogic.Models;
using ObecnaKniznicaLogic.DataModels;
namespace ObecnaKniznicaAPI.Services
{
    public interface IAuthorService
    {
        // ----------- AUTHORS --------------
        Task<Response> AddAuthorAsync(Author authorModel);
        Task<List<Author>> GetAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int id);
        Task<Response> DeleteAuthorByIdAsync(int id);
    }
}
