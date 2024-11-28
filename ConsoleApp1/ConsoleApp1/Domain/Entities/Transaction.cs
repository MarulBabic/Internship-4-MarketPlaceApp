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
        public string promoCode;
        public double finalPrice;
        public bool isReturnTransaction { get; set; } 
        public double refundAmount { get; set; }

        public Transaction(int productId, Buyer buyer, Seller seller,string promoCode=null)
        {
            this.productId = productId;
            this.buyer = buyer;
            this.seller = seller;
            transactionDate = DateTime.Now;
            this.promoCode = promoCode;
            finalPrice = -1;
            isReturnTransaction = false; 
            refundAmount = 0;
        }

        public Transaction(int productId, Buyer buyer, Seller seller,DateTime transactionDate, string promoCode = null)
        {
            this.productId = productId;
            this.buyer = buyer;
            this.seller = seller;
            this.transactionDate = transactionDate;
            this.promoCode = promoCode;
            finalPrice = -1;
            isReturnTransaction = false;
            refundAmount = 0;
        }
    }
}
