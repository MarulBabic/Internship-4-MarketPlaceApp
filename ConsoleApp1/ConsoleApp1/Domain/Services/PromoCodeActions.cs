using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    public class PromoCodeActions
    {

        public static double ApplyPromotionalCode(Product product, string code,Marketplace marketplace)
        {
            var promoCode = marketplace.PromoCodes.FirstOrDefault(p => p.Code.Equals(code));

            if (promoCode == null)
            {
                Console.WriteLine("\nPromo kod nije pronaden.");
                return AskIfUserWantsToBuyOldPrice(product);
            }

            if (!promoCode.IsValid())
            {
                Console.WriteLine("\nPromo kod je istekao.");

                return AskIfUserWantsToBuyOldPrice(product);
            }

            if (!promoCode.IsApplicableToCategory(product.Category))
            {
                Console.WriteLine("\nPromo kod nije primjenjiv na kategoriju ovog proizvoda.");
                return AskIfUserWantsToBuyOldPrice(product);
            }

            double discount = product.Price * (promoCode.DiscountPercentage / 100);
            double discountedPrice = product.Price - discount;

            Console.WriteLine($"\nPromo kod uspjesno primjenjen! Nova cijena proizvoda je: {discountedPrice:F2}");
            return discountedPrice;
        }


        private static double AskIfUserWantsToBuyOldPrice(Product product)
        {
            string response;
            Console.WriteLine("\nZelite li kupiti proizvod po staroj cijeni? (Da / Ne)");
            do
            {
                Console.Write("\nUnos: ");
                response = Console.ReadLine().ToLower().Trim();

                if (response == "da")
                {
                    Console.WriteLine("\nKupit ćete proizvod po staroj cijeni.");
                    return product.Price;
                }
                else if (response == "ne")
                {
                    Console.WriteLine("\nKupnja je otkazana.");
                    return -1;
                }
                else
                {
                    Console.WriteLine("\nNeispravan odgovor. Molimo upisite 'Da' ili 'Ne'.");
                }

            } while (response != "da" && response != "ne");

            return -1;
        }
    }
}
