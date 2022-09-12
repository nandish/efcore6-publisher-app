using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherConsole
{
    internal class Section8
    {
        PubContext _context = new PubContext();
        public void Run()
        {
            //InsertNewAuthorWithNewBook();
            //InsertNewAuthorWith2NewBooks();

            //AddNewBookToExistingAuthorInMemory();
            //AddNewBookToExistingAuthorInMemoryViaBook();

            //EagerLoadBooksWithAuthors();
            //EagerLoadBooksWithAuthorsWithBookFilter();

            //Projections();

            //FilterUsingRelatedData();

            //ModifyingRelatedDataWhenTracked();

            //ModifyingRelatedDataWhenNotTracked();


            CascadeDeleteInActionWhenTracked();
        }

        void CascadeDeleteInActionWhenTracked()
        {
            var author = _context.Authors.Include(a => a.Books)
                    .FirstOrDefault(a => a.AuthorId == 7);
            _context.Authors.Remove(author);

            var state = _context.ChangeTracker.DebugView.ShortView;

            Console.WriteLine(state);
        }

        void ModifyingRelatedDataWhenNotTracked()
        {
            var author = _context.Authors.Include(a => a.Books)
                    .FirstOrDefault(a => a.AuthorId == 5);

            author.Books[0].BasePrice = (decimal)12.00;

            var newContext = new PubContext();
            newContext.Books.Update(author.Books[0]);
            var state = newContext.ChangeTracker.DebugView.ShortView;

            Console.WriteLine(state);
        }

        void ModifyingRelatedDataWhenTracked()
        {
            var author = _context.Authors.Include(a => a.Books)
                    .FirstOrDefault(a => a.AuthorId == 5);

            //author.Books[0].BasePrice = (decimal)10.00;
            author.Books.Remove(author.Books[1]);
            _context.ChangeTracker.DetectChanges();

            var state = _context.ChangeTracker.DebugView.ShortView;
            Console.WriteLine(state);   
        }


        void FilterUsingRelatedData()
        {
            var recentAuthors = _context.Authors
                    .Where(a => a.Books.Any(b => b.PublishDate.Year >= 2015))
                    //.Include(a => a.Books)
                    .ToList();

            recentAuthors.ForEach(a =>
            {
                Console.WriteLine($"{a.LastName} ({a.Books.Count})");
                a.Books.ForEach(b => Console.WriteLine("\t" + b.Title));
            });
        }

        void Projections()
        {
            var unknownTypes = _context.Authors
                .Select(a => new
                {
                    AuthorId = a.AuthorId,
                    Name = a.FirstName.First() + " " + a.LastName,
                    Books = a.Books
                })
                .ToList();
            var debugView = _context.ChangeTracker.DebugView.ShortView;
        }

        void EagerLoadBooksWithAuthorsWithBookFilter()
        {
            var pubDateStart = new DateTime(2010, 1, 1);

            var authors = _context.Authors
                    .Include(a => a.Books
                                    .Where( b=> b.PublishDate >= pubDateStart)
                                    .OrderBy(b => b.Title))
                    .ToList();

            authors.ForEach(a =>
            {
                Console.WriteLine($"{a.LastName} ({a.Books.Count})");
                a.Books.ForEach(b => Console.WriteLine("\t" + b.Title));
            });
        }

        void EagerLoadBooksWithAuthors()
        {
            var authors = _context.Authors.Include(a => a.Books).ToList();

            authors.ForEach(a =>
            {
                Console.WriteLine($"{a.LastName} ({a.Books.Count})");
                a.Books.ForEach(b => Console.WriteLine("\t" + b.Title));
            });
        }

        void AddNewBookToExistingAuthorInMemoryViaBook()
        {
            var book = new Book
            {
                Title = "Shift",
                PublishDate = new DateTime(2012, 1, 1)
            };

            book.Author = _context.Authors.Find(5);
            _context.Books.Add(book);
            _context.SaveChanges();
        }


        void AddNewBookToExistingAuthorInMemory()
        {
            var author = _context.Authors.FirstOrDefault(a => a.LastName == "Howey");
            if (author != null)
            {
                author.Books.Add(
                    new Book { Title = "Wool", PublishDate = new DateTime(2012, 1, 1) }
                    );
            }
            _context.SaveChanges();
        }
        void InsertNewAuthorWithNewBook()
        {
            var author = new Author { FirstName = "Lynda", LastName = "Rutledge" };
            author.Books.Add(new Book
            {
                Title = "West With Giraffes",
                PublishDate = new DateTime(2021, 2, 1)
            });

            _context.Authors.Add(author);
            _context.SaveChanges();

        }

        void InsertNewAuthorWith2NewBooks()
        {
            var author = new Author { FirstName = "Don", LastName = "Jones" };
            author.Books.AddRange(new List<Book> {
                new Book { Title = "The Never", PublishDate = new DateTime(2019,12,1)},
                new Book { Title = "Alabaster", PublishDate = new DateTime(2019,4,1)}
            });

            _context.Authors.Add(author);
            _context.SaveChanges();

        }
    }
}

