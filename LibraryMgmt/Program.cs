using System;
using System.Linq.Expressions;
namespace LibraryMgmt
{    
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Library library = new Library();
                while (true)
                {
                    Console.WriteLine("-------------Welcome to Library Mgmt------------- \n");
                    Console.WriteLine("Enter  1.Add  2.Remove  3.Details  4.Search  5.Issue  6.Return");
                    int n = int.Parse(Console.ReadLine());

                    switch (n)
                    {
                        case 1:
                            {
                                library.Add(); break;
                            }
                        case 2:
                            {
                                library.Remove(); break;
                            }
                        case 3:
                            {
                                library.DisplayAll(); break;
                            }
                        case 4:
                            {
                                library.Search(); break;
                            }
                        default:
                            {
                                Environment.Exit(0);
                                break;
                            }
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }

}