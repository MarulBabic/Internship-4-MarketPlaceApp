using ConsoleApp1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    public class Product
    {
        private static int NextId = 0;
        private int Id { get; }
        public string ProductName { get;  }
        public string ProductDescription { get; }
        public double Price { get; protected set; }
        public Status Status { get; set; }
        public Category Category {  get; set; }
        public Seller Seller {  get; protected set; }

        public Product( string productName, string productDescription, double price, Category category, Seller seller)
        {
            Id = ++NextId;
            ProductName = productName;
            ProductDescription = productDescription;
            Price = price;
            Status = Status.Na_prodaju;
            Category = category;
            Seller = seller;
            
        }

        public int GetId()
        {
            return Id;
        }

        public void SetPrice(double price)
        {
            this.Price = price;
        }
    }
}
