using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2_Sales.Models
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        // Kilogram
        public decimal Price { get; set; }
        // CAD price per Kg

        public Product() { }
        public Product(int id, string name, int weight, decimal price)
        {
            this.Id = id;
            this.Name = name;
            this.Weight = weight;
            this.Price = price;
        }

        public Product(Product product, int weight)
        {
            this.Id = product.Id;
            this.Name = product.Name;
            this.Price = product.Price;
            this.Weight = weight;
        }

        override
        public string ToString()
        {
            return $"{Name,-10}{Price,10}$";
        }
    }
}
