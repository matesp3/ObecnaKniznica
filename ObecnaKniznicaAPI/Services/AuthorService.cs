using Microsoft.EntityFrameworkCore;
using ObecnaKniznicaAPI.Data;
using ObecnaKniznicaLogic.DataModels;
using ObecnaKniznicaLogic.Models;

namespace ObecnaKniznicaAPI.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext appDbContext;

        public AuthorService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Response> AddAuthorAsync(Author authorModel)
        {
            if (authorModel is null)
                return new Response { Success = false, Message = "At least one of the author's arguments was not given" };

            Author result = await GetAuthorByIdAsync(authorModel.Id);
            if (result is null)    // try CREATE
            {
                Author? match = await appDbContext.Authors.FirstOrDefaultAsync(a =>
                                a.FirstName.Equals(authorModel.FirstName) && a.LastName.Equals(authorModel.LastName)
                                && a.PrefixTitles == authorModel.PrefixTitles && a.SuffixTitles == authorModel.SuffixTitles
                              );
                if (match is not null)
                    return new Response { Success = false, Message = "The same person already exists and cannot be created duplicate with the same properties" };

                appDbContext.Authors.Add(authorModel);
                try
                {
                    await appDbContext.SaveChangesAsync();
                    return new Response();
                }
                catch (Exception e)
                {
                    return new Response { Success = false, Message = $"Error during saving new author '{authorModel.FirstName} {authorModel.LastName}' to DB. Info: {e.Message}" };
                }
            }
            else                     // try UPDATE
            {
                result.FirstName    = authorModel.FirstName;
                result.LastName     = authorModel.LastName;
                result.PrefixTitles = authorModel.PrefixTitles;
                result.SuffixTitles = authorModel.SuffixTitles;
                try
                {
                    await appDbContext.SaveChangesAsync();
                    return new Response();
                }
                catch (Exception e)
                {
                    return new Response { Success = false, Message = $"Error occured during updating author '{authorModel.FirstName} {authorModel.LastName}'. Info: {e.Message}" };
                }
            }
        }

        public async Task<Response> DeleteAuthorByIdAsync(int id)
        {
            if (id < 1)
                return new Response { Success = false, Message = $"Given ID={id} is not valid" };

            Author? match = await appDbContext.Authors.Where(a => a.Id == id).FirstOrDefaultAsync();
            if (match is not null)
            {
                appDbContext.Remove(match);
                return new Response { };
            }
            return new Response { Success = false, Message = $"Given ID={id} does not exist" };
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            var author = await appDbContext.Authors.FirstOrDefaultAsync(a => a.Id == id);
            return author!;
        }

        public async Task<List<Author>> GetAuthorsAsync()
        {
            var fetchedData = await appDbContext.Authors.ToListAsync();
            return fetchedData;
        }
    }
}
