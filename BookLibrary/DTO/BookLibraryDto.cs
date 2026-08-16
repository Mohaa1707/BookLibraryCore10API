using System;

namespace BookLibrary.DTO;

public class BookLibraryDto
{
    public int BookId { get; set; }

    public string ISBN { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public string Publisher { get; set; }

    public string Genre { get; set; }

    public string? Language { get; set; }

    public string? Edition { get; set; }

    public int TotalCopies { get; set; }

    public decimal Price { get; set; }

    public string ShelfLocation { get; set; }

    public bool IsActive { get; set; }
}
