using ObecnaKniznicaAPI.Data;
using ObecnaKniznicaLogic.Models;
using ObecnaKniznicaLogic.DataModels;

namespace ObecnaKniznicaAPI.Services
{
    public class RightService : IRightService
    {
        private readonly AppDbContext appDbContext;
        public RightService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Response> AddRights(int bookId, List<int> authorIds)
        {
            List<Task> tasks = new List<Task>();
            foreach (int authorId in authorIds)
            {
                appDbContext.Rights.Add( new BookAuthor { AuthorId = authorId, BookId = bookId} );
            }
            try
            {
                await appDbContext.SaveChangesAsync();
                return new Response();
            } catch (Exception ex)
            {
                return new Response { Success = false, Message = "Could not create BookAuthor relationships"};
            }
        }
    }
}
