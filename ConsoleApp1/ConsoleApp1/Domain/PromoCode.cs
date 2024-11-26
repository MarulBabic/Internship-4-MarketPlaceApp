using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    public class PromoCode
    {
        public string code { get; }
        public double discountPercentage { get; }
        public DateTime expiryDate { get; }

        public PromoCode(string code, double discountPercentage, DateTime expiryDate)
        {
            this.code = code;
            this.discountPercentage = discountPercentage;
            this.expiryDate = expiryDate;
        }

        public bool IsValid()
        {
            return DateTime.Now <= expiryDate;
        }
    }
}
