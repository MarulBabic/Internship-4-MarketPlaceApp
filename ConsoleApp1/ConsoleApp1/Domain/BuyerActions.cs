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
            return new Buyer(buyerName,buyerEmail, balance);
        }
    }
}
