using BookLibrary.Contracts;
using BookLibrary.DTO;
using BookLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookLibraryController : ControllerBase
    {
        private readonly IBookLibraryService _service;

        public BookLibraryController(IBookLibraryService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]

        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _service.GetAllAsync();

                return Ok(new ApiResponse<IEnumerable<BookLibraryDto>>
                {
                    Success = true,
                    Message = "Library records retrieved successfully",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Error retrieving library records",
                    Error = new ApiError
                    {
                        Code = "500",
                        Details = ex.Message
                    }
                });
            }
        }

        [HttpGet("GetById/{bookId}")]

        public async Task<IActionResult> GetByIdAsync(int bookId)
        {
            try
            {
                var book = await _service.GetByIdAsync(bookId);

                if (book == null)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Book record not found"
                    });
                }

                return Ok(new ApiResponse<BookLibraryDto>
                {
                    Success = true,
                    Message = "Book record retrieved successfully",
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Error retrieving book record",
                    Error = new ApiError
                    {
                        Code = "500",
                        Details = ex.Message
                    }
                });
            }
        }

        [HttpPost("Create")]

        public async Task<IActionResult> Create(BookLibraryDto dto)
        {
            try
            {
                var id = await _service.AddAsync(dto);

                return Ok(new ApiResponse<int>
                {
                    Success = true,
                    Message = "Book record created Successfully",
                    Data = id
                });
            }

            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Error creating book record",
                    Error = new ApiError
                    {
                        Code = "500",
                        Details = ex.Message
                    }
                });
            }
        }

        [HttpPut("Update/{bookid}")]

        public async Task<IActionResult> Update(int bookid, BookLibraryDto dto)
        {
            try
            {
                dto.BookId = bookid;

                var updated = await _service.UpdateAsync(dto);

                if (!updated)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Book record not found"
                    });
                }

                return Ok(new ApiResponse<string>
                {
                    Success = true,
                    Message = "Book record updated successfully"
                });
            }

            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Error Updating book record",
                    Error = new ApiError
                    {
                        Code = "500",
                        Details = ex.Message
                    }
                });
            }
        }

        [HttpDelete("Delete/{bookid}")]

        public async Task<IActionResult> Delete(int bookid)
        {
            try
            {
                var deleted = await _service.DeleteAsync(bookid);

                if (!deleted)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Book record not found"
                    });
                }

                return Ok(new ApiResponse<string>
                {
                    Success = true,
                    Message = "Book record deleted successfully"
                });
            }

            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Error deleting book record",
                    Error = new ApiError
                    {
                        Code = "500",
                        Details = ex.Message
                    }
                });
            }
        }

        [HttpGet("GetAllPaged")]

        public async Task<IActionResult> GetAllPaged(
            string? isbn,
            string? title,
            string? author,
            int pageNumber = 1,
            int pageSize = 10)
        {
            try
            {
                var result = await _service.GetAllPagedAsync(
                   isbn,
                   title,
                   author,
                   pageNumber,
                   pageSize);

                return Ok(new ApiResponse<IEnumerable<BookLibraryDto>>
                {
                    Success = true,
                    Message = "Book records retrieved successfully",
                    Data = result.Data,
                    TotalRecords = result.TotalRecords
                });

            }

            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>
                {
                    Success = false,
                    Message = "Error retrieving insurance records",
                    Error = new ApiError
                    {
                        Code = "500",
                        Details = ex.Message
                    }
                });
            }
        }
    }
}
