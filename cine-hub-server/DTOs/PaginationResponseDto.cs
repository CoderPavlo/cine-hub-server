namespace cine_hub_server.DTOs
{
    public class PaginationResponseDto<T>
    {
        public int Total_pages { get; set; }
        public int Total_results { get; set; }
        public int Page { get; set; }
        public IEnumerable<T> Results { get; set; }
        public PaginationResponseDto(int totalPages, int totalResults, int page, IEnumerable<T> results)
        {
            Total_pages = totalPages;
            Total_results = totalResults;
            Page = page;
            Results = results;
        }
    }
}
