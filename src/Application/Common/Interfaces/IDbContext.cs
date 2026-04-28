using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IDbContext
    {
        public DbSet<CLIENT> CLIENTS { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken, string? userId);
    }
}