using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryMvc.Models
{
    public class Shelf
    {
        [DisplayName("מספר מדף")]
        public int Id { get; set; }

        [DisplayName("גובה")]
        public int Hight { get; set; }

        [DisplayName("רוחב")]
        public int Width { get; set; }

        public int LibraryId { get; set; }
        [DisplayName("ספריה")]
        public Library? Library { get; set; }

        public List<Book>? Books { get; set; }

        [DisplayName("מקום פנוי") ,NotMapped]
        public int FreeSpace
        {
            get { return Books != null ? (Width - Books.Sum(x => x.Width)) : Width; }
            set { }
        }
    }
}
