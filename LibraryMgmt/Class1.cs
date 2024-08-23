using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryMgmt
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int BId { get; set; }
        public Book(string title, string author, int bId)
        {
            Title = title;
            Author = author;
            BId = bId;
        }
        public void DisplayInfo()
        {
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"BId: {BId}");
        }
    }
    public class Library
    {

        public List<Book> books = new List<Book>();
        public void Add()
        {   
            Console.WriteLine("Enter the no of books:");
            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Enter Title, Author and BID:");
                string title = Console.ReadLine();
                string author = Console.ReadLine();
                int bId = int.Parse(Console.ReadLine());
                books.Add (new Book(title, author, bId));
            }
        }
        public void Search()
        {
            Console.WriteLine("Enter the title to search:");
            string searchTitle = Console.ReadLine();

            var query = books.FindAll(b => b.Title.Equals(searchTitle, StringComparison.OrdinalIgnoreCase));
            if (query.Count > 0)
            {
                foreach (var book in query)
                {
                        book.DisplayInfo();
                }
            }
            else
            {
                Console.WriteLine("No books found with the given title.");
            }
        }
        public void Remove()
        {
            Console.WriteLine("Enter the title to Remove");
            string removeTitle = Console.ReadLine();
            books.RemoveAll(b => b.Title.Equals(removeTitle, StringComparison.OrdinalIgnoreCase));
        }
        public void DisplayAll()
        { 
            if(books.Count==0)
            {
                Console.WriteLine("Empty List");
            }
            else
            {
                for (int i = 0; i < books.Count; i++)
                {
                    books[i].DisplayInfo();
                }
            }
        
        }

    }
}


   