using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublisherConsole;

internal class Section3
{

    PubContext _context = new PubContext();
    internal void Run()
    {
        //QueryFilters();
        //QueryFiltersWithLike();
        //FindIt();
        //SkipAndTakeAuthors();

        //SortAuthors();

        //SortAuthorsByMultipleProperties();

        QueryAggregate();
    }

    void QueryAggregate()
    {
        Console.WriteLine("----- Query and Aggregate -----");
        var author = _context.Authors.OrderByDescending(a=> a.FirstName).FirstOrDefault(a => a.LastName == "Lerman");
        Print(author);
    }

    void SortAuthors()
    {
        Console.WriteLine("----- Sort Authors -----");
        var authorsByLastName = _context.Authors.OrderBy(a => a.LastName).ToList();
        authorsByLastName.ForEach(a => Console.WriteLine($"{a.LastName}, {a.FirstName}"));
    }

    void SortAuthorsByMultipleProperties()
    {
        Console.WriteLine("----- Sort Authors By Last Name then by First Name -----");
        var authorsByLastName = _context.Authors.OrderBy(a => a.LastName).ThenBy(a => a.FirstName).ToList();
        authorsByLastName.ForEach(a => Console.WriteLine($"{a.LastName}, {a.FirstName}"));


        Console.WriteLine("----- Descending Sort Authors By Last Name then by First Name -----");
        var authorsDescending = _context.Authors.OrderByDescending(a => a.LastName).ThenByDescending(a=>a.FirstName).ToList();
        authorsDescending.ForEach(a => Console.WriteLine($"{a.LastName}, {a.FirstName}"));
    }


    void SkipAndTakeAuthors()
    {
        Console.WriteLine("----- Skip And Take Authors -----");
        var groupSize = 2;
        for (int i = 0; i < 5; i++)
        {
            var authors = _context.Authors.Skip(groupSize * i).Take(groupSize).ToList();
            Console.WriteLine($"Group {i}:");
            Print(authors);
        }
    }


    void QueryFilters()
    {
        Console.WriteLine("----- Query Filters -----");
        var name = "Josie";
        var authors = _context.Authors.Where(s => s.FirstName == name).ToList();
        Print(authors);
    }

    void QueryFiltersWithLike()
    {
        Console.WriteLine("----- Query Filters with Like -----");
        var filter = "L%";
        var authors = _context.Authors.Where(a => EF.Functions.Like(a.LastName, filter)).ToList();
        Print(authors);
    }

    void FindIt()
    {
        Console.WriteLine("----- Find It -----");
        var authorIdTwo = _context.Authors.Find(2);
        Print(authorIdTwo);
    }

    void Print(List<Author> authors)
    {
        if (authors == null)
            Console.WriteLine("No authors found");
        else
            authors.ForEach(x => Print(x));

    }

    void Print(Author author)
    {

        Console.WriteLine($"{author.FirstName} {author.LastName}");
    }
}
