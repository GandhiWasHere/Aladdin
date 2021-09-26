using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Aladdin.Models;

namespace Aladdin.Data
{
    public class AladdinContext : DbContext
    {
        public AladdinContext (DbContextOptions<AladdinContext> options)
            : base(options)
        {
        }

        public DbSet<Aladdin.Models.Customer> Customer { get; set; }

        public DbSet<Aladdin.Models.Product> Product { get; set; }

        public DbSet<Aladdin.Models.Cart> Cart { get; set; }
<<<<<<< HEAD

=======
>>>>>>> 2d20bc529dbb0b3d26d18b0a3585298a81d4862f
    }
}
