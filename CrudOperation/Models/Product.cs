using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudOperation.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string ProductName { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
