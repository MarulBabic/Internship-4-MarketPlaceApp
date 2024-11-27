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
            if (marketplace.sellers.Any(s => s.email == seller.email))
            {
                Console.WriteLine("\nProdavač s ovim emailom već postoji.");
                return;
            }
            marketplace.sellers.Add(seller);
        }

        public static void AddBuyer(Marketplace marketplace,Buyer buyer)
        {
            if (marketplace.buyers.Any(b => b.email == buyer.email))
            {
                Console.WriteLine("\nKupac s ovim emailom već postoji.");
                return;
            }
            marketplace.buyers.Add(buyer);
        }

        public static void AddProduct(Marketplace marketplace,Product product)
        {
            if (marketplace.products.Any(p => p.GetId() == product.GetId()))
            {
                Console.WriteLine("\nProizvod s ovim ID-om već postoji.");
                return;
            }
            marketplace.products.Add(product);
        }

        public static void AddTransaction(Marketplace marketplace,Transaction transaction)
        {
            marketplace.transactions.Add(transaction);
        }

        public static void AddPromoCode(Marketplace marketplace,PromoCode promoCode)
        {
            marketplace.promoCodes.Add(promoCode);
        }
    }
}
