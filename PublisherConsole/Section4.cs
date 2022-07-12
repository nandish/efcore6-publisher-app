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
    internal class Section4
    {
        PubContext _context = new PubContext();

        internal void Run()
        {
            //RetrieveAndUpdateAuthor();

            //RetrieveAndUpdateMultipleAuthors();

            //VariousOperations();

            //CoordinatedRetrieveAndUpdateAuthor();

            //DeleteAnAuthor();

            InsertMultipleAuthors();

            //BulkAddUpdate();
            
            GetAuthorsWithBooks();
        }

        void InsertMultipleAuthors()
        {
            _context.Authors.AddRange(new Author { FirstName = "Ruth", LastName = "Ozeki" }
                                        , new Author { FirstName = "Sofia", LastName = "Segovia" }
                                        , new Author { FirstName = "Ursula K.", LastName = "LeGuin" }
                                        , new Author { FirstName = "Hugh", LastName = "Howey" }
                                        , new Author { FirstName = "Isabelle", LastName = "Allende" });

            _context.SaveChanges();
        }

        void BulkAddUpdate()
        {
            var newAuthors = new Author[]
            {
                new Author { FirstName = "Tsitsi", LastName = "Dangarembga" }
                , new Author { FirstName = "Lisa", LastName = "See" }
                , new Author { FirstName = "Zhang", LastName = "Ling" }
                , new Author { FirstName = "Marilynne", LastName = "Robinson" }
            };

            _context.Authors.AddRange(newAuthors);

            var book = _context.Books.Find(2);
            book.Title = "Programming Entity Framework 2nd Edition";

            _context.SaveChanges();
        }

        void DeleteAnAuthor()
        {
            var extraJL = _context.Authors.Find(3);
            if(extraJL != null)
            {
                _context.Authors.Remove(extraJL);
                _context.SaveChanges();
            }
        }

        void CoordinatedRetrieveAndUpdateAuthor()
        {
            var author = FindThatAuthor(1);
            if(author?.FirstName == "Julie")
            {
                author.FirstName = "Julia";
                SaveThatAuthor(author);
            }
        }

        Author FindThatAuthor(int authorId)
        {
            using var shortLivedContext = new PubContext();
            return shortLivedContext.Authors.Find(authorId);
        }

        void SaveThatAuthor(Author author)
        {
            using var shortLivedContext = new PubContext();
            shortLivedContext.Authors.Update(author);
            shortLivedContext.SaveChanges();
        }

        void VariousOperations()
        {
            var author = _context.Authors.Find(2);
            author.LastName = "Newfoundland";
            var newAuthor = new Author { LastName = "Appleman", FirstName = "Dan" };
            _context.Authors.Add(newAuthor);
            _context.SaveChanges();
        }

        void RetrieveAndUpdateAuthor()
        {
            var author = _context.Authors.FirstOrDefault(a => a.FirstName == "Julie" && a.LastName == "Lerman");
            if(author != null)
            {
                author.FirstName = "Julia";
                _context.SaveChanges();
            }
        }

        void RetrieveAndUpdateMultipleAuthors()
        {
            var lermanAuthors = _context.Authors.Where(x => x.LastName == "Lehrman");
            foreach(var author in lermanAuthors)
            {
                author.LastName = "Lerman";
            }

            Console.WriteLine("Before:");
            Console.WriteLine(_context.ChangeTracker.DebugView.ShortView);

            _context.ChangeTracker.DetectChanges();

            Console.WriteLine("After:");
            Console.WriteLine(_context.ChangeTracker.DebugView.ShortView);

            _context.SaveChanges();
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
    }
}
