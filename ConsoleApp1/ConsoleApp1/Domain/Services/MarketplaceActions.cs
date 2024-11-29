using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ConsoleApp1.Domain
{
    public class MarketplaceActions
    {
        public static void AddSeller(Marketplace marketplace,Seller seller)
        {
            if (marketplace.Sellers.Any(s => s.Email == seller.Email))
            {
                Console.WriteLine("\nProdavač s ovim emailom već postoji.");
                return;
            }
            marketplace.Sellers.Add(seller);
        }

        public static void AddBuyer(Marketplace marketplace,Buyer buyer)
        {
            if (marketplace.Buyers.Any(b => b.Email == buyer.Email))
            {
                Console.WriteLine("\nKupac s ovim emailom već postoji.");
                return;
            }
            marketplace.Buyers.Add(buyer);
        }

        public static void AddProduct(Marketplace marketplace,Product product)
        {
            if (marketplace.Products.Any(p => p.GetId() == product.GetId()))
            {
                Console.WriteLine("\nProizvod s ovim ID-om već postoji.");
                return;
            }
            marketplace.Products.Add(product);
        }

        public static void AddTransaction(Marketplace marketplace,Transaction transaction)
        {
            marketplace.Transactions.Add(transaction);
        }

        public static void AddPromoCode(Marketplace marketplace,PromoCode promoCode)
        {
            marketplace.PromoCodes.Add(promoCode);
        }
    }
}
