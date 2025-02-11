﻿using System;
using System.Collections.Generic;

namespace cine_hub_server.DTOs.Session
{
    public class SessionResponseDto
    {
        public string Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string FormatType { get; set; }
        public decimal Price { get; set; }
        public string FilmName { get; set; }
        public string CinemaLocation { get; set; }
        public string AuditoriumName { get; set; }
    }

    public class CreateSessionDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string FormatType { get; set; }
        public decimal Price { get; set; }
        public int FilmId { get; set; }
        public string CinemaId { get; set; }
        public string AuditoriumId { get; set; }
    }

    public class UpdateSessionDto
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string FormatType { get; set; }
        public decimal Price { get; set; }
    }
}