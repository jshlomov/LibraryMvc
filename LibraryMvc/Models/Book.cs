using System.ComponentModel;

namespace LibraryMvc.Models
{
    public class Book
    {
        public int Id { get; set; }

        [DisplayName("שם")]
        public string Name { get; set; }
        [DisplayName("גובה")]
        public int Hight { get; set; }

        [DisplayName("רוחב")]
        public int Width { get; set; }

        [DisplayName("קטגוריה")]
        public string? genre { get; set; }

        [DisplayName("מספר מדף")]
        public int? ShelfId { get; set; }
        public Shelf? Shelf { get; set; }

        public int? SetId { get; set; }
        [DisplayName("שיוך לסט")]
        public Set? Set { get; set; }
    }
}
