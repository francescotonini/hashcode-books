using System.Collections.Generic;
using System.Linq;

namespace HashCode_Books
{
    class Library
    {
        public int Index { get; set; }
        public int SignupTime { get; set; }
        public int BooksPerDay { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();

        public int GetBooksNotScanned()
        {
            return Books.Select(x => !x.Scanned).Count();
        }

        public int GetScore(int days)
        {
            int totalBooksScannableInTime = days * BooksPerDay;
            int totalBooksScannable = Books.FindAll(x => !x.Scanned).Count();

            if (totalBooksScannableInTime <= totalBooksScannable)
            {
                // Since books on dataset d have the same score, we can do something much faster
                // return totalBooksScannableInTime * 65;

                return Books.Take(totalBooksScannableInTime).ToList().FindAll(x => !x.Scanned).Sum(x => x.Score);
            }
            else
            {
                // Since books on dataset d have the same score, we can do something much faster
                // return totalBooksScannable * 65;

                return Books.Take(totalBooksScannable).ToList().FindAll(x => !x.Scanned).Sum(x => x.Score);
            }
        }

        public void Signup()
        {
            foreach (Book book in Books)
            {
                book.Scanned = true;
            }
        }
    }
}
