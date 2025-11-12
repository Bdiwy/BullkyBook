
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BullkyBook.Models
{
    [Table("Catagories")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Column("DisbplayOrder")]
        public int DisbplayOrder { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        public ICollection<Product>? Product {get;set;}
    }
}