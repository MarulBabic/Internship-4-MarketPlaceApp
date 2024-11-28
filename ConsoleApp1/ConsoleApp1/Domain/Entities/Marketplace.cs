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
        public List<PromoCode> promoCodes = new List<PromoCode>();
        public double totalFunds {  get; set; }

        public Marketplace()
        {
            totalFunds = 0.0;
        }
    }
}
