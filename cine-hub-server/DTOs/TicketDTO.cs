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