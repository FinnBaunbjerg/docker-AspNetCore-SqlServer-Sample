
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApiColourData
{
    public class PrepareDatabase
    {
        public static void InitDatabase( ColourContext dbContext )
        {
            dbContext.Database.Migrate();
            
            SeedDatabase(dbContext);
        }

        private static void SeedDatabase(ColourContext dbContext)
        {
            if (!dbContext.Colours.Any())
            {
                dbContext.Colours.AddRange(
       new Colour() { Name = "blue" },
                    new Colour() { Name = "green" },
                    new Colour() { Name = "red" },
                    new Colour() { Name = "orange" },
                    new Colour() { Name = "yellow" },
                    new Colour() { Name = "black" }
                );

                dbContext.SaveChanges();
            }
        }
    }
}
