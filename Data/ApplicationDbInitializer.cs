using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Shortner.Data
{
    public class ApplicationDbInitializer
    {
        public static void Initializer(ApplicationDbContext context, bool isDevelopment)
        {
            
            
            if (!isDevelopment)
            {
                context.Database.Migrate();
                return;
            }

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }
}