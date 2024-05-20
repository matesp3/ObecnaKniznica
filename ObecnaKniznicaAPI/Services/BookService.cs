using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ObecnaKniznicaAPI.Data;
using ObecnaKniznicaLogic.DataModels;
using ObecnaKniznicaLogic.Models;
using System.Text.RegularExpressions;

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
                if (newBook.Authors.IsNullOrEmpty())
                    return new Response { Success = false, Message = "Book must contain at least one author." };

                List<Author> authorsToAdd;
                List<int> existingAuthorsIds;

                Book oldBook = await GetBookByIdAsync(newBook.Id); // skus najst knihu v DB
                if (oldBook is null)    // try CREATE book
                {
                    CheckAuthorsExistenceInDB(out existingAuthorsIds, out authorsToAdd, newBook.Authors!);
                    newBook.Authors = authorsToAdd;
                    appDbContext.Books.Add(newBook);
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
                    int i, y;
                    bool wasFound; // ci aj po update bude existovat. Ak nie, treba ju odstranit zo stareho zoznamu
                    for (i = 0; i < oldBook.Authors!.Count(); i++)
                    {
                        wasFound = false;
                        for (y = 0; y < newBook.Authors!.Count(); y++)
                        {
                            if (oldBook.Authors!.ElementAt(i).Id == newBook.Authors.ElementAt(y).Id)
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
                            oldBook.Authors!.RemoveAt(i);
                            i--; // na indexe tejto iteracie musim ostat aj v nasledovnej, lebo vsetky nasledovne prvky sa posunuli o jeden nizsie
                        }
                    }
                    CheckAuthorsExistenceInDB(out existingAuthorsIds, out authorsToAdd, newBook.Authors!);
                    oldBook.Authors!.AddRange(authorsToAdd);   // pridanie novych autorov ku starym
                }
                try
                {
                    await appDbContext.SaveChangesAsync(); // (nova) kniha s novymi autormi bola ulozena
                    int bookId = (oldBook is null)
                        ? appDbContext.Books.OrderByDescending(b => b.Created).FirstOrDefault()?.Id ?? 0
                        : oldBook.Id;
                    if (bookId > 0)
                    {
                        await new RightService(appDbContext).AddRights(bookId, existingAuthorsIds); // dodatocne pridanie prav pre uz existujucich autorov
                        return new Response();
                    }
                    return new Response { Success = false, Message = "Id for creating book rights was not found" };
                    
                }
                catch (Exception e)
                {
                    return new Response { Success = false, Message = $"Error occured during saving book '{newBook.Title}' to DB. Info: {e.Message}" };
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

        private void CheckAuthorsExistenceInDB(out List<int> existingAuthorsIds, out List<Author> authorsToAdd, in List<Author> allAuthors)
        {
            var authorService = new AuthorService(appDbContext);
            existingAuthorsIds = new List<int>();
            authorsToAdd = new List<Author>();
            foreach (Author author in allAuthors)
            {
                Author? match = appDbContext.Authors.Where(a =>
                    a.FirstName == author.FirstName
                    & a.LastName == author.LastName
                    & a.PrefixTitles == author.PrefixTitles
                    & a.SuffixTitles == author.SuffixTitles)
                    .FirstOrDefault();

                if (match is not null)
                    existingAuthorsIds.Add(match.Id);
                else
                    authorsToAdd.Add(author);
            }
        }
    }
}
