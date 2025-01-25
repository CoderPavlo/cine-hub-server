namespace cine_hub_server.Models
{
    public class Genre
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<FilmGenre> FilmGenres { get; set; }
    }
}
