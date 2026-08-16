using BookLibrary.Contracts;
using BookLibrary.Data;
using BookLibrary.DTO;
using BookLibrary.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;



namespace BookLibrary.Repositories;

public class BookLibraryRepositories : IBookLibraryRepository
{
    private readonly AppDbContext _dbContext;

    public BookLibraryRepositories(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> AddSync(BookLibraryMaster bookLibrary)
    {
        var result = await _dbContext.Database.ExecuteSqlRawAsync(
            @"EXEC sp_BookMaster_Insert,
            @ISBN,
            @Title,
            @Author,
            @Publisher,
            @Genre,
            @Language,
            @Edition,
            @TotalCopies,
            @AvailableCopies,
            @Price,
            @ShelfLocation,
            @IsActive",
        new SqlParameter("@ISBN", bookLibrary.ISBN),
        new SqlParameter("@Title", bookLibrary.Title),
        new SqlParameter("@Author", bookLibrary.Author),
        new SqlParameter("@Publisher", bookLibrary.Publisher),
        new SqlParameter("@Genre", bookLibrary.Genre),
        new SqlParameter("Language", bookLibrary.Language),
        new SqlParameter("@Edition", bookLibrary.Edition),
        new SqlParameter("@TotalCopies", bookLibrary.TotalCopies),
        new SqlParameter("@AvailableCopies", bookLibrary.AvailableCopies),
        new SqlParameter("@Price", bookLibrary.Price),
        new SqlParameter("@ShelfLocation", bookLibrary.ShelfLocation),
        new SqlParameter("@IsActive", bookLibrary.IsActive)
        );

        return result;
    }

    public async Task<bool> UpdateAsync(BookLibraryMaster bookLibrary)
    {
        var affectedRows = await _dbContext.Database.ExecuteSqlRawAsync(
            @"EXEC sp_BookMaster_Update
             @ISBN,
            @Title,
            @Author,
            @Publisher,
            @Genre,
            @Language,
            @Edition,
            @TotalCopies,
            @AvailableCopies,
            @Price,
            @ShelfLocation,
            @IsActive",
        new SqlParameter("@ISBN", bookLibrary.ISBN),
        new SqlParameter("@Title", bookLibrary.Title),
        new SqlParameter("@Author", bookLibrary.Author),
        new SqlParameter("@Publisher", bookLibrary.Publisher),
        new SqlParameter("@Genre", bookLibrary.Genre),
        new SqlParameter("Language", bookLibrary.Language),
        new SqlParameter("@Edition", bookLibrary.Edition),
        new SqlParameter("@TotalCopies", bookLibrary.TotalCopies),
        new SqlParameter("@AvailableCopies", bookLibrary.AvailableCopies),
        new SqlParameter("@Price", bookLibrary.Price),
        new SqlParameter("@ShelfLocation", bookLibrary.ShelfLocation),
        new SqlParameter("@IsActive", bookLibrary.IsActive)
        );

        return affectedRows > 0;
    }

    public async Task<BookLibraryMaster?> GetByIdAsync(int bookid)
    {
        var book = await _dbContext.BookLibraryMasters.FromSqlRaw(
            "EXEC sp_BookMaster_GetById @BookId",
            new SqlParameter("@BookId", bookid)).AsNoTracking().ToListAsync();

        return book.FirstOrDefault();
    }

    public async Task<IEnumerable<BookLibraryMaster>> GetAllAsync()
    {
        return await _dbContext.BookLibraryMasters.FromSqlRaw("EXEC sp_BookMaster_GetAll").ToListAsync();
    }

    public async Task<bool> DeleteAsync(int bookid)
    {
        var affectedRows = await _dbContext.Database.ExecuteSqlRawAsync(
            "EXEC sp_BookMaster_Delete @BookId",
            new SqlParameter("@BookId", bookid));

        return affectedRows > 0;
    }

    public async Task<PagedResultDto<BookLibraryMaster>> GetAllPagedAsync(
        string? isbn,
        string? title,
        string? author,
        int pageNumber,
        int pageSize)
    {
        using var connection = _dbContext.Database.GetDbConnection();

        await connection.OpenAsync();

        using var command = connection.CreateCommand();

        command.CommandText = "sp_BookMaster_GetPaged";
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(new SqlParameter("@ISBN", (object?)isbn ?? DBNull.Value));
        command.Parameters.Add(new SqlParameter("@Title", (object?)title ?? DBNull.Value));
        command.Parameters.Add(new SqlParameter("@Author", (object?)author ?? DBNull.Value));
        command.Parameters.Add(new SqlParameter("@PageNumber", pageNumber)); 
        command.Parameters.Add(new SqlParameter("Pagesize", pageSize));

        using var reader = await command.ExecuteReaderAsync();

        var books = new List<BookLibraryMaster>();

        while (await reader.ReadAsync())
        {   
            books.Add(new BookLibraryMaster
            {
                BookId = reader.GetInt32(0),
                ISBN = reader.GetString(1),
                Title = reader.GetString(2),
                Author = reader.GetString(3),
                Publisher = reader.GetString(4),
                Genre = reader.GetString(5),
                Language = reader.IsDBNull(6) ? null : reader.GetString(6),
                Edition = reader.IsDBNull(7) ? null : reader.GetString(7),
                TotalCopies = reader.GetInt32(8),
                AvailableCopies = reader.GetInt32(9),
                Price = reader.GetDecimal(10),
                ShelfLocation = reader.IsDBNull(11) ? null : reader.GetString(11),
                IsActive = reader.GetBoolean(12)
            });
        }

        await reader.NextResultAsync();

        int totalRecords = 0;

        if (await reader.ReadAsync())
        {
            totalRecords = reader.GetInt32(0);
        }

        return new PagedResultDto<BookLibraryMaster>
        {
            Data = books,
            TotalRecords = totalRecords
        };
    }
}


