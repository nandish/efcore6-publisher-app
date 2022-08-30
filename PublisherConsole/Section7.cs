using Microsoft.EntityFrameworkCore;
using PublisherData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherConsole
{
    internal class Section7
    {
        PubContext _context = new PubContext();
        internal void Run()
        {

            //var authors = _context.Authors.ToList();
            var name = "Ozeki";
            var authors = _context.Authors.Where(x => x.LastName == name).ToList();
        }

        void GetAuthorsWithBooks()
        {
            //using (var context = new PubContext())
            //{
            //    var authors = context.Authors.Include(a => a.Books).ToList();
            //    foreach (var author in authors)
            //    {
            //        Console.WriteLine($"{author.FirstName} {author.LastName}");
            //        foreach (var book in author.Books)
            //        {
            //            Console.WriteLine($"*{book.Title}");
            //        }
            //    }
            //}
        }
    }
}
