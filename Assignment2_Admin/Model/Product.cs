using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2_Admin.Model
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        // Kilogram
        public decimal Price { get; set; }
        // CAD price per Kg

        public Product(int id, string name, int weight, decimal price)
        {
            this.Id = id;
            this.Name = name;
            this.Weight = weight;
            this.Price = price;
        }
    }
}
