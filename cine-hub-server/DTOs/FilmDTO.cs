using System;
using System.Collections.Generic;

namespace cine_hub_server.DTOs.Film
{
    public class FilmResponseDto
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
        public ICollection<string> Genres { get; set; }
    }

    public class CreateFilmDto
    {
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
        public ICollection<string> GenreIds { get; set; }
    }

    public class UpdateFilmDto
    {
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
    }
}