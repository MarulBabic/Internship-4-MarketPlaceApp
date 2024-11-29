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
        public string Code { get; }
        public double DiscountPercentage { get; }
        public DateTime ExpiryDate { get; }
        public List<Category> Categories { get; }

        public PromoCode(string code, double discountPercentage, DateTime expiryDate, List<Category> categories)
        {
            Code = code;
            DiscountPercentage = discountPercentage;
            ExpiryDate = expiryDate;
            Categories = categories;
        }

        public bool IsValid()
        {
            return DateTime.Now <= ExpiryDate;
        }

        public bool IsApplicableToCategory(Category category)
        {
            return Categories.Contains(category);
        }
    }
}
