using AutoMapper;
using BookLibrary.DTO;
using BookLibrary.Entities;

namespace BookLibrary.Mapper
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookLibraryMaster, BookLibraryDto>().ReverseMap();
        }
    }
}
