using ConsoleApp1.Data;
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
        public List<Category> Categories { get; }

        public PromoCode(string code, double discountPercentage, DateTime expiryDate, List<Category> categories)
        {
            this.code = code;
            this.discountPercentage = discountPercentage;
            this.expiryDate = expiryDate;
            Categories = categories;
        }

        public bool IsValid()
        {
            return DateTime.Now <= expiryDate;
        }

        public bool IsApplicableToCategory(Category category)
        {
            return Categories.Contains(category);
        }
    }
}
