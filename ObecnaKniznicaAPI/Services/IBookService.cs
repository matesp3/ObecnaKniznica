using ObecnaKniznicaLogic.Models;
using ObecnaKniznicaLogic.DataModels;

namespace ObecnaKniznicaAPI.Services // interface sa pouziva preto, lebo implementacia ziskania dat moze zavisiet na relacnej databaze, XML dokumente, etc.
{
    public interface IBookService
    {

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
