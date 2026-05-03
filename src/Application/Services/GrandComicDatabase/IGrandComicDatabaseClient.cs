using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.GrandComicDatabase
{
    public interface IGrandComicDatabaseClient
    {
        Task<Issue> GetIssue(int id, CancellationToken cancellationToken);
        Task<byte[]> GetIssueCover(Issue issue, CancellationToken cancellationToken);
    }
}
