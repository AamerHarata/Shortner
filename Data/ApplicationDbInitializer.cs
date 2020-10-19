using Microsoft.EntityFrameworkCore;

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