using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    public class Transaction
    {
        public int productId { get; }
        public Buyer buyer { get; set; }
        public Seller seller { get; set; }
        public DateTime transactionDate {  get; set; }

        public Transaction(int productId, Buyer buyer, Seller seller)
        {
            this.productId = productId;
            this.buyer = buyer;
            this.seller = seller;
            transactionDate = DateTime.Now;
        }
    }
}
