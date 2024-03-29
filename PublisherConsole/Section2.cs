﻿namespace PublisherConsole;
using PublisherData;
using PublisherDomain;
using Microsoft.EntityFrameworkCore;

internal class Section2
{
    internal void Run()
    {
        using (PubContext context = new PubContext())
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            
        }

        //GetAuthors();
        
        //AddAuthorWithBook();
        //AddAuthors();
        //GetAuthors();

        //GetAuthorsWithBooks();
    }
    
    void AddAuthorWithBook()
    {
        var author = new Author { FirstName = "Julie", LastName = "Lerman" };
        author.Books.Add(new Book
        {
            Title = "Programming Entity Framework",
            PublishDate = new DateTime(2009, 1, 1)
        });
        author.Books.Add(new Book
        {
            Title = "Programming Entity Framework 2nd Ed",
            PublishDate = new DateTime(2010, 8, 1)
        });

        using (var context = new PubContext())
        {
            context.Authors.Add(author);
            context.SaveChanges();
        }

    }

    void GetAuthorsWithBooks()
    {
        using (var context = new PubContext())
        {
            var authors = context.Authors.Include(a => a.Books).ToList();
            foreach (var author in authors)
            {
                Console.WriteLine($"{author.FirstName} {author.LastName}");
                foreach (var book in author.Books)
                {
                    Console.WriteLine($"*{book.Title}");
                }
            }
        }
    }

    void GetAuthors()
    {
        using (var context = new PubContext())
        {
            var authors = context.Authors.ToList();
            foreach (var author in authors)
            {
                Console.WriteLine($"{author.FirstName} {author.LastName}");
            }
        }
    }

    void AddAuthors()
    {
        using (var context = new PubContext())
        {
            context.Authors.Add(new Author { FirstName = "Josie", LastName = "Newf" });
            context.Authors.Add(new Author { FirstName = "Julie", LastName = "Lerman" });
            context.Authors.Add(new Author { FirstName = "Rhoda", LastName = "Lerman" });
            context.Authors.Add(new Author { FirstName = "Don", LastName = "Jones" });
            context.Authors.Add(new Author { FirstName = "Jim", LastName = "Christopher" });
            context.Authors.Add(new Author { FirstName = "Stephen", LastName = "Haunts" });
            context.SaveChanges();
        }
    }
}
