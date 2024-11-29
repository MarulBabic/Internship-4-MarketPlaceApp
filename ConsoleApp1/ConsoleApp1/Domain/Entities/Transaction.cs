using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    public class Transaction
    {
        public int ProductId { get; }
        public Buyer Buyer { get; set; }
        public Seller Seller { get; set; }
        public DateTime TransactionDate {  get; set; }
        public string PromoCode;
        public double FinalPrice;
        public bool IsReturnTransaction { get; set; } 
        public double RefundAmount { get; set; }

        public Transaction(int productId, Buyer buyer, Seller seller,string promoCode=null)
        {
            ProductId = productId;
            Buyer = buyer;
            Seller = seller;
            TransactionDate = DateTime.Now;
            PromoCode = promoCode;
            FinalPrice = -1;
            IsReturnTransaction = false; 
            RefundAmount = 0;
        }

        public Transaction(int productId, Buyer buyer, Seller seller,DateTime transactionDate, string promoCode = null)
        {
            this.ProductId = productId;
            this.Buyer = buyer;
            this.Seller = seller;
            this.TransactionDate = transactionDate;
            this.PromoCode = promoCode;
            FinalPrice = -1;
            IsReturnTransaction = false;
            RefundAmount = 0;
        }
    }
}
