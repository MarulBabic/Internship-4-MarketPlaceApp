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
        private static int counter = 0;
        private int id { get; }
        public string productName { get;  }
        public string productDescription { get; }
        public double price { get; protected set; }
        public Status status { get; set; }
        public Category category {  get; set; }
        public Seller seller {  get; protected set; }

        public Product( string productName, string productDescription, double price, Category category, Seller seller)
        {
            id = ++counter;
            this.productName = productName;
            this.productDescription = productDescription;
            this.price = price;
            status = Status.Na_prodaju;
            this.category = category;
            this.seller = seller;
            
        }

        public int GetId()
        {
            return id;
        }
    }
}
