using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Alladin.Models;

namespace Aladdin.Data
{
    public class AladdinContext : DbContext
    {
        public AladdinContext (DbContextOptions<AladdinContext> options)
            : base(options)
        {
        }

        public DbSet<Alladin.Models.Customer> Customer { get; set; }

        public DbSet<Alladin.Models.Product> Product { get; set; }
    }
}
