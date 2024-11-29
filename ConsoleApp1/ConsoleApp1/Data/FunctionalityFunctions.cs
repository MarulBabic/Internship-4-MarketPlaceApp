using ConsoleApp1.Domain;
using ConsoleApp1.Presentation;
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

        public static bool GetDateFromUser(out DateTime date,bool isStart)
        {
            date = DateTime.MinValue;

            bool isValidDate = false;
            if (isStart)
            {
                Console.WriteLine("\nUnesite pocetni datum (dd-MM-yyyy): ");
            }
            else
            {
                Console.WriteLine("\nUnesite zavrsni datum (dd-MM-yyyy): ");
            }
            while (!isValidDate)
            {
                Console.Write("\nUnos: ");
                string input = Console.ReadLine().Trim();

                isValidDate = DateTime.TryParseExact(input, "dd-MM-yyyy",
                                                      System.Globalization.CultureInfo.InvariantCulture,
                                                      System.Globalization.DateTimeStyles.None, out date);

                if (!isValidDate)
                {
                    Console.WriteLine("\nNeispravan unos datuma. Molimo unesite datum u formatu dd-MM-yyyy");
                }
            }

            return true;
        }

        public static bool ValidateCategory(string categoryInput, out Category category)
        {
            if (Enum.TryParse(categoryInput, true, out category))
            {
                return true; 
            }

            Console.WriteLine("\nNeispravan unos kategorije. Pokušajte ponovo (Elektronika, Odjeća, Knjige ili Namještaj).");
            return false;
        }

        public static bool ValidateProductName(string productName)
        {
            if (string.IsNullOrEmpty(productName)) {
                Console.WriteLine("\nNaziv proizvoda ne može biti prazan.");
                return false;
            }

            if (productName.Length < 3)
            {
                Console.WriteLine("\nNaziv proizvoda mora biti barem 3 znaka.");
                return false;
            }

            return true;
        }

        public static bool ValidateProductDesc(string productDesc)
        {
            if (string.IsNullOrWhiteSpace(productDesc))
            {
                Console.WriteLine("\nOpis proizvoda ne može biti prazan.");
                return false;
            }

            return true;
        }

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


        public static void ChooseUserType(Marketplace marketplace)
        {
            while (true)
            {
                Console.WriteLine("\n1 - Registracija kao kupac\n2 - Registracija kao prodavac\n3 - Povratak na početni izbornik");
                Console.Write("\nUnos: ");
                var option=0;

                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("\nNeispravan unos. Molimo unesite broj.");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        HandleBuyerRegistration(marketplace);
                        return;

                    case 2:
                        HandleSellerRegistration(marketplace);
                        return;

                    case 3:
                        return;

                    default:
                        Console.WriteLine("\nPogrešan unos, pokušajte ponovno.");
                        break;
                }
            }
        }

        private static void HandleSellerRegistration(Marketplace marketplace)
        {
            Seller seller = SellerActions.SellerRegistration(marketplace);
            if (seller == null)
            {
                Console.WriteLine("\nRegistracija nije uspjela.");
                return;
            }

            MarketplaceActions.AddSeller(marketplace,seller);
            Console.WriteLine("\nProdavač uspješno registriran.");
            SellerMenu.ViewSellerMenu(marketplace, seller);
        }

        private static void HandleBuyerRegistration(Marketplace marketplace)
        {
            Buyer buyer = BuyerActions.BuyerRegistration(marketplace);
            if (buyer == null)
            {
                Console.WriteLine("\nRegistracija nije uspjela.");
                return;
            }

            MarketplaceActions.AddBuyer(marketplace, buyer);
            Console.WriteLine("\nKupac uspješno registriran.");
            BuyerMenu.ViewBuyerMenu(marketplace, buyer);
        }

        public static Buyer CheckIfIsBuyer(Marketplace marketplace, string email) {

            return marketplace.Buyers.FirstOrDefault(buyer => buyer.Email == email);
        }

        public static Seller CheckIfIsSeller(Marketplace marketplace, string email)
        {
            return marketplace.Sellers.FirstOrDefault(seller => seller.Email == email);
        }

        public static bool CheckIfEmailAlreadyExists(string email, Marketplace marketplace) {

            bool emailExistsAsBuyer = marketplace.Buyers.Any(buyer => buyer.Email == email);

            bool emailExistsAsSeller = marketplace.Sellers.Any(seller => seller.Email == email);

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
