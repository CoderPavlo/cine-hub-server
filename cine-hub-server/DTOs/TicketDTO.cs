namespace cine_hub_server.DTOs.Ticket
{
    public class TicketResponseDto
    {
        public string Id { get; set; }
        public decimal Price { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string SessionId { get; set; }
        public string FilmName { get; set; }
        public string CinemaLocation { get; set; }
        public string HallName { get; set; }
        public string FormatType { get; set; }
        public DateTime StartTime { get; set; }
    }

    public class TicketSeatDto
    {
        public int Row { get; set; }
        public int Seat { get; set; }

    }

    public class CreateTicketBodyDto
    {
        public decimal Price { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public string SessionId { get; set; }
    }

    public class CreateTicketDto
    {
        public decimal Price { get; set; }
        public int RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public string UserId { get; set; }
        public string SessionId { get; set; }
    }
}