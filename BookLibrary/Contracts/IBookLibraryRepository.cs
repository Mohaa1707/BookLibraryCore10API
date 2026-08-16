using BookLibrary.DTO;
using BookLibrary.Entities;

namespace BookLibrary.Contracts;

public interface IBookLibraryRepository
{
    Task<int> AddSync(BookLibraryMaster bookLibrary);

    Task<IEnumerable<BookLibraryMaster>> GetAllAsync();

    Task<BookLibraryMaster?> GetByIdAsync(int bookId);

    Task<bool> UpdateAsync(BookLibraryMaster bookLibrary);

    Task<bool> DeleteAsync(int bookid);

    Task<PagedResultDto<BookLibraryMaster>> GetAllPagedAsync(
        string? isbn,
        string? title,
        string? author,
        int pageNumber,
        int pageSize);


}
