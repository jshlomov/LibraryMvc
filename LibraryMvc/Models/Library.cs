namespace LibraryMvc.Models
{
    public class Library
    {
        public int Id { get; set; }
        public string genre { get; set; }

        public List<Shelf>? Shelves { get; set; }
    }
}
