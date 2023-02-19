using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Context
{
    public class SeededAdministrator
    {
        public static User GetSeeded()
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                UserName = "Administrator",
                NormalizedUserName = "Administrator",
                Email = "Administrator@mail.com",
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var passHasher = new PasswordHasher<User>();
            user.PasswordHash = passHasher.HashPassword(user, "rootpass");
            return user;
        }
    }
}
