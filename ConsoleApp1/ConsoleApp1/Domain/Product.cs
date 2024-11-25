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
        private Guid id { get; }
        public string productName { get;  }
        public string productDescription { get; }
        public double price { get; protected set; }
        public Status status { get; protected set; }
        public Category category {  get; set; }
        public Seller seller {  get; protected set; }

        public Product( string productName, string productDescription, double price, Category category, Seller seller)
        {
            id = Guid.NewGuid();
            this.productName = productName;
            this.productDescription = productDescription;
            this.price = price;
            status = Status.Na_prodaju;
            this.category = category;
            this.seller = seller;
            
        }
    }
}
