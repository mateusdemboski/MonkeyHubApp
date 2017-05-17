using System.Collections.Generic;
using System.Threading.Tasks;
using MonkeyHubApp.Models;

namespace MonkeyHubApp.Services
{
    public interface IMonkeyHubApiService
    {
        Task<List<Tag>> GeTagsAsync();
        Task<List<Content>> GetContentsByTagIdAsync(string tagId);
        Task<List<Content>> GetContentsByFilterAsync(string filter);
    }
}
