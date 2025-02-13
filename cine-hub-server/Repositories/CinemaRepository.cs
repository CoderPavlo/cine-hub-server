using cine_hub_server.Data_access;
using cine_hub_server.DTOs;
using cine_hub_server.Interfaces;
using cine_hub_server.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace cine_hub_server.Repositories
{
    public class CinemaRepository : GenericRepository<Cinema>, ICinemaRepository
    {
        public CinemaRepository(CineDbContext context) : base(context)
        {
        }

        public async Task<PaginationResponseDto<Cinema>> GetAllPagination(int page, int itemsPerPage)
        {
            int totalResults = await dbSet.CountAsync();
            int totalPages = (int)Math.Ceiling(totalResults / (double)itemsPerPage);
            var results = await dbSet.Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToListAsync();
            return new PaginationResponseDto<Cinema>(totalPages, totalResults, page, results);
        }
    }
}
