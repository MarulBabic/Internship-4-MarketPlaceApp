using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    public class Marketplace
    {
        public List<Seller> Sellers { get; private set; } = new List<Seller>();
        public List<Buyer> Buyers { get; private set; } = new List<Buyer>();
        public List<Product> Products { get; private set; } = new List<Product>();
        public List<Transaction> Transactions { get; private set; } = new List<Transaction>();
        public List<PromoCode> PromoCodes { get; private set; } = new List<PromoCode>();
        public double TotalFunds {  get; set; }

        public Marketplace()
        {
            TotalFunds = 0.0;
        }
    }
}
