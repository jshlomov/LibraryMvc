using LibraryMvc.Models;

namespace LibraryMvc.ViewModels
{
    public class SetBooks
    {
        public Set Set { get; set; }
        public List<Book> Books { get; set; }

        public SetBooks()
        {
            Set = new Set();
            Books = new List<Book>();
        }
    }
}
