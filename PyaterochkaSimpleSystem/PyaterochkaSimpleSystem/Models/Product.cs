using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PyaterochkaSimpleSystem.Models
{
    internal class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Amount {  get; set; }
        public DateOnly ShelfLife { get; set; }
        public int CategoryId { get; set; }
        public DateTime DateUpdated { get; set; }

        // Navigration property
        public Category? Category { get; set; }
    }
}
