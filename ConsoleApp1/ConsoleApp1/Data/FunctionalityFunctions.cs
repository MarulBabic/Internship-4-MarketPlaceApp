using ConsoleApp1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1.Data
{
    public class FunctionalityFunctions
    {
        public static bool CheckIsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("\nEmail ne smije biti prazan!");
                return false;
            }

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (!Regex.IsMatch(email, emailPattern))
            {
                Console.WriteLine("\nNeispravan format! Molimo unesite valjanu email adresu.");
                return false;
            }

            return true;
        }

        public static Buyer CheckIfIsBuyer(Marketplace marketplace, string email) { 
            
            foreach(var buyer in marketplace.buyers)
            {
                if (buyer.email == email) {
                    return buyer;
                }
            }

            return null;
        }

        public static Seller CheckIfIsSeller(Marketplace marketplace, string email)
        {

            foreach (var seller in marketplace.sellers)
            {
                if (seller.email == email)
                {
                    return seller;
                }
            }

            return null;
        }

        public static bool CheckIfEmailAlreadyExists(string email, Marketplace marketplace) {

            if (CheckIfIsSeller(marketplace, email) != null)
            {
                return true;
            }
            else if (CheckIfIsBuyer(marketplace, email) != null)
            {
                return true;
            }

            return false;

        }

        public static double CheckIfIsValidBalance()
        {
            double balance = 0.0;

            Console.WriteLine("\nUnesite pocetno stanje vaseg racuna: ");
            Console.Write("\nUnos: ");

            while (!double.TryParse(Console.ReadLine(), out balance) || balance <= 0)
            {
                Console.WriteLine("\nNeispravan unos. Molimo unesite validni broj za početno stanje računa (ne može biti 0 ili negativan).");
                Console.Write("\nUnos: ");
            }

            return balance;
        }
    }
}
