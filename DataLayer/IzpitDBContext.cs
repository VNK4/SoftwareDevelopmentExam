using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;

namespace DataLayer
{
    public class IzpitDBContext : DbContext
    {
        public IzpitDBContext()
        {
            
        }
        public IzpitDBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(@"Server=127.0.0.1;Port=3306;Database=IzpitDB;Uid=root;Pwd=ivancom04");
            }
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Interest> Interests { get; set; }

        public DbSet<Region> Regions { get; set; }
        
    }
}
