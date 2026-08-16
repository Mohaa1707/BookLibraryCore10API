using AutoMapper;
using BookLibrary.Contracts;
using BookLibrary.DTO;
using BookLibrary.Entities;

namespace BookLibrary.Service
{
    public class BookService : IBookLibraryService
    {
        private readonly IBookLibraryRepository _repository;
        private readonly IMapper _mapper;

        public BookService(IBookLibraryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> AddAsync(BookLibraryDto dto)
        {
            var entity = _mapper.Map<BookLibraryMaster>(dto);
            return await _repository.AddSync(entity);
        }

        public async Task<IEnumerable<BookLibraryDto>> GetAllAsync()
        {
            var bookList = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookLibraryDto>>(bookList);
        }

        public async Task<BookLibraryDto?> GetByIdAsync(int bookid)
        {
            var book = await _repository.GetByIdAsync(bookid);
            return book == null ? null : _mapper.Map<BookLibraryDto>(book);
        }

        public async Task<bool> UpdateAsync(BookLibraryDto dto)
        {
            var entity = _mapper.Map<BookLibraryMaster>(dto);
            return await _repository.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(int bookId)
        {
            return await _repository.DeleteAsync(bookId);
        }

        public async Task<PagedResultDto<BookLibraryDto>> GetAllPagedAsync(
        string? isbn,
        string? title,
        string? author,
        int pageNumber,
        int pageSize)
        {
            var result = await _repository.GetAllPagedAsync(
                isbn,
                title,
                author,
                pageNumber,
                pageSize);

            return new PagedResultDto<BookLibraryDto>
            {
                Data = _mapper.Map<IEnumerable<BookLibraryDto>>(result.Data),
                TotalRecords = result.TotalRecords
            };
        }
    }
}
