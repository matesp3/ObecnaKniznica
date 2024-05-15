using ObecnaKniznicaLogic.Models;
using ObecnaKniznicaLogic.DataModels;

namespace ObecnaKniznicaAPI.Services
{
    public interface IBookService
    {
        // ----------- AUTHORS --------------
        //Task<Response> AddAuthor(Author authorModel);
        //Task<List<Author>> GetAuthors();
        //Task<Author> GetAuthor(int id);
        //Task<Response> DeleteAuthor(int id);

        // ----------- BOOKS --------------
        Task<Response> AddBookAsync(Book bookModel);
        Task<List<Book>> GetBooksAsync();
        Task<Book> GetBookAsync(int id);
        Task<Response> DeleteBookAsync(int id);


        // ----------- RIGHTS --------------
        //Task<Response> AddRight(Author authorModel, Book BookModel);
        //Task<List<Author>> GetRights();
        //Task<List<Author>> GetRightsByAuthorId(int authorId);
        //Task<List<Author>> GetRightsByBookId(int bookId);
        //Task<Author> GetRight(int authorId, int bookId);
        //Task<Response> DeleteRight(int id);
        //Task<Response> DeleteRightsByAuthorId(int authorId);
        //Task<Response> DeleteRightsByBookId(int bookId);
    }
}
