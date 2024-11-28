using ConsoleApp1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    public class BuyerActions
    {
        public static Buyer BuyerRegistration(Marketplace marketplace)
        {
            var buyerName = UserActions.GetUserDataName();
            var buyerEmail = UserActions.GetUserDataEmail();

            while(FunctionalityFunctions.CheckIfEmailAlreadyExists(buyerEmail, marketplace))
            {
                Console.WriteLine("\nEmail koji ste unijeli se vec koristi. Pokusajte ponovo");
                buyerEmail = UserActions.GetUserDataEmail();
            }

            var balance = FunctionalityFunctions.CheckIfIsValidBalance();

            Console.WriteLine($"\nKupac uspjesno registriran!\n\n Ime: {buyerName}\n Email: {buyerEmail}\n Pocetno stanje: {balance:F2}\n");
            Console.WriteLine("\nPritisnite bilo koju tipku za nastavak kao kupac...");
            Console.ReadKey();
            return new Buyer(buyerName,buyerEmail, balance);
        }

        public static void DeductAmount(Buyer buyer,double price)
        {
            buyer.UpdateBalance(-price);
            Console.WriteLine($"\nKupcev racun umanjen je za {price:F2}");
        }

        public static void ReturnAmount(Buyer buyer, double price)
        {
            buyer.UpdateBalance(price);
            Console.WriteLine($"\nKupcev racun uvecan je za {price:F2}");
        }

        public static void ShowBuyersBalance(Buyer buyer) {
            if (buyer == null)
            {
                Console.WriteLine("\nKupac ne postoji.");
                return;
            }

            Console.WriteLine($"\nStanje na racunu kupca {buyer.name}: {buyer.balance:F2}");
        }
    }
}
