using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces.Models;

namespace Infrastructure.Context
{
    public class MeetupDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public MeetupDbContext(DbContextOptions<MeetupDbContext> options) : base(options) {}

        public DbSet<Meetup> Meetups => Set<Meetup>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var updatedAt = nameof(IBaseModel.UpdatedAt);
            var createdAt = nameof(IBaseModel.CreatedAt);

            var entries = ChangeTracker.Entries();
            if (!entries.Any())
                return await base.SaveChangesAsync(cancellationToken);

            var modifed = entries
                .Where(e => e.State == EntityState.Modified)
                .Where(e => e.Properties.Where(p => p.Metadata.Name == updatedAt).Any());

            var added = entries
                .Where(e => e.State == EntityState.Added)
                .Where(e => e.Properties.Where(p => p.Metadata.Name == createdAt).Any());

            foreach (var entityEntry in added)
                entityEntry.Property(createdAt).CurrentValue = DateTime.UtcNow;

            foreach (var entityEntry in modifed)
                entityEntry.Property(updatedAt).CurrentValue = DateTime.UtcNow;

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
