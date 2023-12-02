using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PyaterochkaSimpleSystem.Models
{
    internal class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        // Navigration property
        public List<Product>? Products { get; set; }
    }
}
