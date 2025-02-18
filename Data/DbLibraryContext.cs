using Library.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;


namespace Library.Data
{
    public class DbLibraryContext : DbContext
    {
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public DbLibraryContext(DbContextOptions<DbLibraryContext> options) : base(options) 
        { 
            //Database.EnsureCreated();
        }

        public void Load()
        {
            Readers.Load();
            Books.Include(b => b.Authors).Load();
            Authors.Include(a => a.Books).Load();
        }

        public IEnumerable<Reader> GetReaders() => Readers;
        public IEnumerable<Author> GetAuthors() => Authors;
        public IEnumerable<Book> GetBooks() => Books;

        // фабричный метод удобней для создания контекста бД
        // нежели конструктор
        // так как позволяет использовать параметр-тип

        // построители (Builder) позволяют настройку класса
        // с большим числом параметров
        // без использования параметров метода

        public class ContextFactory :
            IDesignTimeDbContextFactory<DbLibraryContext>
        {
            public DbLibraryContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<DbLibraryContext>();

                ConfigurationBuilder configurationBuilder = new();
                configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
                configurationBuilder.AddJsonFile("config.json");
                IConfigurationRoot config = configurationBuilder.Build();

                string connectionString = config.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
                return new DbLibraryContext(optionsBuilder.Options);
            }
        }
    }
}
