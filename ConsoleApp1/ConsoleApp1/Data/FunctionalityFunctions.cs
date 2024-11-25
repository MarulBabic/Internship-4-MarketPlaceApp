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

        public static void ChooseUserType(Marketplace marketplace) {

            
            var option = 0;
            while (true)
            {
                Console.WriteLine("\n1 - Registracija kao kupac\n2 - Registracija kao prodavac\n3 - Povratak na pocetni izbornik");
                Console.Write("\nUnos: ");
                int.TryParse(Console.ReadLine(), out option);

                switch (option) {
                    case 1:
                        Buyer buyer = BuyerActions.BuyerRegistration(marketplace);
                        if (buyer == null)
                        {
                            Console.WriteLine("Registracija nije uspjela");
                            return;
                        }
                        marketplace.buyers.Add(buyer);
                        Console.WriteLine("Kupac uspjesno registriran");
                        return;
                    case 2:
                        Seller seller = SellerActions.SellerRegistration(marketplace);
                        if (seller == null)
                        {
                            Console.WriteLine("Registracija nije uspjela");
                            return;
                        }
                        marketplace.sellers.Add(seller);
                        Console.WriteLine("Prodavac uspjesno registriran");
                        return;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Pogresan unos, pokusajte ponovno");
                        break;
                }
            }
        }

        public static Buyer CheckIfIsBuyer(Marketplace marketplace, string email) {

            return marketplace.buyers.FirstOrDefault(buyer => buyer.email == email);
        }

        public static Seller CheckIfIsSeller(Marketplace marketplace, string email)
        {

            return marketplace.sellers.FirstOrDefault(seller => seller.email == email);
        }

        public static bool CheckIfEmailAlreadyExists(string email, Marketplace marketplace) {

            bool emailExistsAsBuyer = marketplace.buyers.Any(buyer => buyer.email == email);

            bool emailExistsAsSeller = marketplace.sellers.Any(seller => seller.email == email);

            return emailExistsAsBuyer || emailExistsAsSeller;

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
