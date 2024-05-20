using ObecnaKniznicaLogic.DataModels;

namespace ObecnaKniznicaAPI.Services
{
    public interface IRightService
    {
        public Task<Response> AddRights(int bookId, List<int> authorIds);
    }
}
