using BookLibrary.DTO;
using BookLibrary.Entities;

namespace BookLibrary.Contracts
{
    public interface IBookLibraryService
    {
        Task<int> AddAsync(BookLibraryDto bookLibrary);
        Task<IEnumerable<BookLibraryDto>> GetAllAsync();
        Task<BookLibraryDto?> GetByIdAsync(int bookId);
        Task<bool> UpdateAsync(BookLibraryDto bookLibrary);
        Task<bool> DeleteAsync(int bookId);
        Task<PagedResultDto<BookLibraryDto>> GetAllPagedAsync(
            string? isbn,
            string? title,
            string? author,
            int pageNumber,
            int pageSize);
    }
}
