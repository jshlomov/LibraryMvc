using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryMvc.Models
{
    public class Set
    {
        public int Id { get; set; }

        [DisplayName("שם")]
        public string Name { get; set; }

        [DisplayName("קטגוריה")]
        public string Genre { get; set; }

        [DisplayName("גובה"), NotMapped]
        public int Hight
        { 
            get { return Books != null && Books.Any() ? Books.Max(b => b.Hight): 0; }
            set { }
        }

        [DisplayName("רוחב"), NotMapped]
        public int Width
        {
            get { return Books != null ? Books.Sum(b => b.Width) : 0; }
            set { }
        }

        public List<Book>? Books { get; set; }
    }
}
