﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublisherDomain;
using System.Diagnostics;

namespace PublisherData
{
    public class PubContext : DbContext
    {
        StreamWriter _writer = new StreamWriter("EFCoreLog.txt", append: true);

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        // Hardcoding connection string for Demo
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = PubDatabase"
            )
            //.LogTo(log => Debug.WriteLine(log))
            //.LogTo(_writer.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
            .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
            .EnableSensitiveDataLogging(); 
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, FirstName = "Rhoda", LastName = "Lerman" });


            // Sample configuration to One-to-Many relationships
            //modelBuilder.Entity<Author>()
            //    .HasMany<Book>()
            //    .WithOne();

            var authorList = new Author[]
            {
               new Author { AuthorId = 2, FirstName = "Ruth", LastName="Ozeki"}
                , new Author { AuthorId = 3, FirstName = "Sofia", LastName = "Segovia" }
                , new Author { AuthorId = 4, FirstName = "Ursula K.", LastName = "LeGuin" }
                , new Author { AuthorId = 5, FirstName = "Hugh", LastName = "Howey" }
                , new Author { AuthorId = 6, FirstName = "Isabelle", LastName = "Allende" }
            };

            modelBuilder.Entity<Author>().HasData(authorList);

            var someBooks = new Book[]
            {
                new Book { BookId = 1, AuthorId = 1, Title = "In God's Ear", PublishDate = new DateTime(1989, 3, 1)},
                new Book { BookId = 2, AuthorId = 2, Title = "A Tale for the Time Being", PublishDate = new DateTime(2013,12,31)},
                new Book { BookId = 3, AuthorId = 3, Title = "The Left Hand of Darkness", PublishDate = new DateTime(1969,3,1)}
            };
            modelBuilder.Entity<Book>().HasData(someBooks);

        }

        public override void Dispose()
        {
            _writer.Dispose();
            base.Dispose();
        }
    }
}