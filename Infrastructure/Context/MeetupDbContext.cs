using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class MeetupDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public MeetupDbContext(DbContextOptions<MeetupDbContext> options) : base(options) {}

        public DbSet<Event> Events => Set<Event>();

    }
}
