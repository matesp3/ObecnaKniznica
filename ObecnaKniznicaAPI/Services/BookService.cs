using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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

        public async Task<Response> AddBookAsync(Book newBook)
        {
            if (newBook is null)
                return new Response { Success = false, Message = "At least one of required fields wasn't given." };
            else       // model is not null
            {
                Book oldBook = await GetBookByIdAsync(newBook.Id);
                if (oldBook is null)    // try CREATE book
                {
                    appDbContext.Books.Add(newBook);
                    try
                    {
                        await appDbContext.SaveChangesAsync();
                        return new Response();
                    }
                    catch (Exception e)
                    {
                        return new Response { Success = false, Message = $"Error during saving new book '{newBook.Title}' to DB. Info: {e.Message}" };
                    }
                }
                else                     // try UPDATE book
                {
                    oldBook.Title          = newBook.Title;
                    oldBook.Description    = newBook.Description;
                    oldBook.ReservedAmount = newBook.ReservedAmount;
                    oldBook.TotalAmount    = newBook.TotalAmount;
                    oldBook.ReleaseDate    = newBook.ReleaseDate;
                    oldBook.Created        = newBook.Created;

                    // nasleduje algoritmus, lebo nemozem priradit novych autorov. Nefunguje to. Preto z povodneho odstranim tych, ktori v novom nie su, updatnem pozostalych a pridam novych.
                    if (newBook.Authors.IsNullOrEmpty())
                        oldBook.Authors = new List<Author>(0);
                    else
                    {
                        int i, y;
                        bool wasFound; // ci aj po update bude existovat. Ak nie, treba ju odstranit zo stareho zoznamu
                        for (i = 0; i < oldBook.Authors.Count(); i++)
                        {
                            wasFound = false;
                            for (y = 0; y < newBook.Authors.Count(); y++)
                            {
                                if (oldBook.Authors.ElementAt(i).Id == newBook.Authors.ElementAt(y).Id)
                                {      // tato kniha sa nasla, urob jej UPDATE a odstran ju z novych dat
                                       //  UPDATE authors (old_I) <-- (new_Y) {prekopiruj relevantne data}
                                    oldBook.Authors.ElementAt(i).FirstName = newBook.Authors.ElementAt(y).FirstName; // TODO: treba este osetrit duplicitu uplne rovnakeho autora, ci uz neexistuje
                                    oldBook.Authors.ElementAt(i).LastName = newBook.Authors.ElementAt(y).LastName;
                                    oldBook.Authors.ElementAt(i).PrefixTitles = newBook.Authors.ElementAt(y).PrefixTitles;
                                    oldBook.Authors.ElementAt(i).SuffixTitles = newBook.Authors.ElementAt(y).SuffixTitles;
                                    //--------------------- odstran z noveho. Tym je zaistena buducnost po update
                                    newBook.Authors.RemoveAt(y);
                                    wasFound = true;
                                    break;
                                }
                            }
                            if (!wasFound)
                            {
                                oldBook.Authors.RemoveAt(i);
                                i--; // na indexe tejto iteracie musim ostat aj v nasledovnej, lebo vsetky nasledovne prvky sa posunuli o jeden nizsie
                            }
                        }

                        for (int a = 0; a < newBook.Authors.Count(); a++) // pridanie novych autorov ku starym
                        {
                            oldBook.Authors.Add(newBook.Authors.ElementAt(a));
                        }
                    }

                    // z aplikacie potrebujes poslat novy model (co sa tyka autorov), kde budes posielat tych, ktori sa maju vymazat z povodneho,
                    // a na koniec povodneho iba pridas novych Authorov bez zadaneho Id, ako aj s prazdnym zoznamom knih
                    try
                    {
                        await appDbContext.SaveChangesAsync();
                        return new Response();
                    } catch (Exception e)
                    {
                        return new Response { Success = false, Message = $"Error occured during updating book '{oldBook.Title}'. Info: {e.Message}" };
                    }
                }
            }
        }

        public async Task<Response> DeleteBookAsync(int id)
        {
            Book result = await GetBookByIdAsync(id);
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
                    return new Response { Success = true, Message = $"'{result.Title}' deleted succesfully" };
                }
                catch (Exception ex)
                {
                    return new Response { Success = false, Message = $"Unable to save changes after deleting book to DB. Info: {ex.Message}" };
                }

            }
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var result = await appDbContext.Books.Where(b => b.Id == id).Include(b => b.Authors).FirstOrDefaultAsync();
            return result!;
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            var result = await appDbContext.Books.Include(b => b.Authors).ToListAsync();
            return result;
        }
    }
}
