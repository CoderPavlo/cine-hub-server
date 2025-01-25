namespace cine_hub_server.Models
{
    public class FilmGenre
    {
        public string FilmId { get; set; }
        public Film Film { get; set; }

        public string GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
