using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Alladin.Models;

namespace Alladin.Data
{
    public class AlladinContext : DbContext
    {
        public AlladinContext (DbContextOptions<AlladinContext> options)
            : base(options)
        {
        }

        public DbSet<Alladin.Models.Cart> Cart { get; set; }

        public DbSet<Alladin.Models.Customer> Customer { get; set; }

        public DbSet<Alladin.Models.Product> Product { get; set; }

        public DbSet<Alladin.Models.Supplier> Supplier { get; set; }
    }
}
