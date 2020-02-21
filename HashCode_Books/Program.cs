using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HashCode_Books
{
    class Program
    {
        static List<Book> books = new List<Book>();
        static List<Library> libraries = new List<Library>();

        static int nDays;

        static void Main(string[] args)
        {
            string name = "d_tough_choices";

            ReadInput(name);

            List<Library> schedule = new List<Library>();
            int daysRemaining = nDays;
            while (daysRemaining > 0 && libraries.Count > 0)
            {
                Library best = libraries.OrderBy(x => x.SignupTime).ThenByDescending(x => x.GetScore(daysRemaining)).First();
                best.Signup();

                daysRemaining -= best.SignupTime;

                schedule.Add(best);
                libraries.Remove(best);

                if (libraries.Count % 100 == 0)
                {
                    Console.WriteLine("Remaining: " + libraries.Count);
                }
            }

            WriteOutput(name, schedule);

            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        static void ReadInput(string name)
        {
            string inputPath = @"C:\Users\tonin\Downloads\" + name + ".txt";

            StreamReader reader = new StreamReader(inputPath);

            string line = reader.ReadLine();

            nDays = int.Parse(line.Split(' ')[2]);

            string[] rawBooks = reader.ReadLine().Split(' ');

            for (int i = 0; i < rawBooks.Length; i++)
            {
                books.Add(new Book()
                {
                    Index = i,
                    Score = int.Parse(rawBooks[i])
                });
            }

            int libraryCounter = 0;
            while (!reader.EndOfStream)
            {
                Library library = new Library();

                line = reader.ReadLine();

                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                library.Index = libraryCounter++;
                library.SignupTime = int.Parse(line.Split(' ')[1]);
                library.BooksPerDay = int.Parse(line.Split(' ')[2]);

                line = reader.ReadLine();

                foreach (string s in line.Split(' '))
                {
                    int bookIndex = int.Parse(s);

                    library.Books.Add(books[bookIndex]);
                }

                library.Books = library.Books.OrderByDescending(key => key.Score).ToList();
                libraries.Add(library);
            }
        }

        static void WriteOutput(string name, List<Library> libraries)
        {
            string outputPath = @"C:\Users\tonin\Downloads\out_" + name + ".txt";

            StreamWriter writer = new StreamWriter(outputPath);
            writer.WriteLine(libraries.Count);

            foreach (Library library in libraries)
            {

                writer.WriteLine(library.Index + " " + library.Books.Count);
                foreach (Book book in library.Books)
                {
                    writer.Write(book.Index + " ");
                }
                writer.Write("\n");
            }

            writer.Close();
        }
    }
}
