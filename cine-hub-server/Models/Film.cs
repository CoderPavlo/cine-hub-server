namespace cine_hub_server.Models
{
    public class Film
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Cast { get; set; }
        public decimal Ratings { get; set; }
        public string AgeRestriction { get; set; }
        public int DurationTime { get; set; }
        public string Director { get; set; }
        public string Description { get; set; }
        public string TrailerUrl { get; set; }
        public string PosterUrl { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int ReleaseDuration { get; set; }

        public ICollection<FilmGenre> FilmGenres { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }

}
