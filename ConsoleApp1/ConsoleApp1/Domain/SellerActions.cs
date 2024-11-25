using ConsoleApp1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    public class SellerActions
    {
        public static Seller SellerRegistration(Marketplace marketplace)
        {
            var sellerName = UserActions.GetUserDataName();
            var sellerEmail = UserActions.GetUserDataEmail();

            while (FunctionalityFunctions.CheckIfEmailAlreadyExists(sellerEmail, marketplace))
            {
                Console.WriteLine("\nEmail koji ste unijeli se vec koristi. Pokusajte ponovo");
                sellerEmail = UserActions.GetUserDataEmail();
            }

            Console.WriteLine($"\nProdavac uspjesno registriran!\n\n Ime: {sellerName}\n Email: {sellerEmail}\n");
            return new Seller(sellerName, sellerEmail);
        }

        public static void AddSaleIncome(Seller seller,double price)
        {
            price = price*0.95;

            seller.AddIncome(price);
            Console.WriteLine($"\nProdavacev racun uvecan za {price}");

        }

        public static void DeductSaleIncome(Seller seller,double price)
        {
            seller.AddIncome(-price);
            Console.WriteLine($"\nProdavacev racun umanjen za {price}");
        }
    }
}
