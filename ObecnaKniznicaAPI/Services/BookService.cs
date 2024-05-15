using Microsoft.EntityFrameworkCore;
using ObecnaKniznicaAPI.Data;
using ObecnaKniznicaLogic.DataModels;
using ObecnaKniznicaLogic.Models;

namespace ObecnaKniznicaAPI.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext appDbContext;

        public BookService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Response> AddBookAsync(Book bookModel)
        {
            if (bookModel is null)
                return new Response { Success = false, Message = "At least one of required fields wasn't given." };
            else       // model is not null
            {
                if (bookModel.Id <= 0)
                    return new Response { Success = false, Message = $"'{bookModel.Title}' not found" };
                else   // bookMode.Id > 0
                {
                    Book result = await GetBookAsync(bookModel.Id);
                    if (result is null)    // try CREATE book
                    {
                        appDbContext.Books.Add(bookModel);
                        try
                        {
                            await appDbContext.SaveChangesAsync();
                            return new Response();
                        }
                        catch (Exception e)
                        {
                            return new Response { Success = false, Message = $"Error during saving new book '{bookModel.Title}' to DB. Info: {e.Message}" };
                        }
                    }
                    else                     // try UPDATE book
                    {
                        result.Title = bookModel.Title;
                        result.Description = bookModel.Description;
                        result.ReservedAmount = bookModel.ReservedAmount;
                        result.TotalAmount = bookModel.TotalAmount;
                        result.ReleaseDate = bookModel.ReleaseDate;
                        result.Created = bookModel.Created;
                        try
                        {
                            await appDbContext.SaveChangesAsync();
                            return new Response();
                        } catch (Exception e)
                        {
                            return new Response { Success = false, Message = $"Error occured during updating book '{result.Title}'. Info: {e.Message}" };
                        }
                    }
                }
            }
        }

        public async Task<Response> DeleteBookAsync(int id)
        {
            Book result = await GetBookAsync(id);
            if (result == null)
            {
                return new Response { Success = false, Message = "Book with given ID to delete does not exist." };
            }
            else
            {
                appDbContext.Books.Remove(result);
                try
                {
                    await appDbContext.SaveChangesAsync();
                    return new Response { Success = true, Message = $"{result.Title} delete succesfully" };
                }
                catch (Exception ex)
                {
                    return new Response { Success = false, Message = $"Unable to save changes after deleting book to DB. Info: {ex.Message}" };
                }

            }
        }

        public async Task<Book> GetBookAsync(int id)
        {
            var result = await appDbContext.Books.FirstOrDefaultAsync(i => i.Id == id);
            return result!;
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            var result = await appDbContext.Books.ToListAsync();
            return result;
        }
    }
}
