using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    public class Marketplace
    {
        public List<Seller> sellers = new List<Seller>();
        public List<Buyer> buyers = new List<Buyer>();
        public List<Product> products = new List<Product>();
        public List<Transaction> transactions = new List<Transaction>();
        public double totalFunds = 0.0;

        public Marketplace()
        {
            sellers = new List<Seller>();
            buyers = new List<Buyer>();
            products = new List<Product>();
            transactions = new List<Transaction>();
        }
    }
}
