namespace cine_hub_server.DTOs
{
    public class PaginationResponseDto<T>
    {
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
        public int Page { get; set; }
        public IEnumerable<T> Results { get; set; }
        public PaginationResponseDto(int totalPages, int totalResults, int page, IEnumerable<T> results)
        {
            TotalPages = totalPages;
            TotalResults = totalResults;
            Page = page;
            Results = results;
        }
    }
}
