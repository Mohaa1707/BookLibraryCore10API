using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookLibrary.Entities
{
    [Table("BookLibraryMaster")]
    public class BookLibraryMaster
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [StringLength(20)]
        public string ISBN { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        [StringLength(100)]
        public string Publisher { get; set; }

        [Required]
        [StringLength(50)]
        public string Genre { get; set; }

        [StringLength(30)]
        public string?  Language { get; set; }

        [StringLength(20)]
        public string? Edition { get; set; }
     
        public int TotalCopies { get; set; }

        public int AvailableCopies { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(30)]

        public string? ShelfLocation { get; set; }

        public bool IsActive { get; set; }



    }
}
