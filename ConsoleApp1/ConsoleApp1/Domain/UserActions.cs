using ConsoleApp1.Data;
using ConsoleApp1.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    public class UserActions
    {
        public static string  GetUserDataName()
        {
            string name = "";

            Console.WriteLine("\nUnesite ime: ");
            do {
                Console.Write("\nUnos: ");
                name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("\nIme ne smije biti prazno! Ponovite unos");
                    continue;
                }

                if (!name.All(c => char.IsLetter(c) || c == ' '))
                {
                    Console.WriteLine("\nIme smije sadrzavati samo slova i razmake! Ponovite unos");
                    continue;
                }

                break;

            } while (true);

            return name;
        }

        public static string GetUserDataEmail()
        {
            string email = "";
            Console.WriteLine("\nUnesite email: ");
            do
            {
                Console.Write("\nUnos: ");
                email = Console.ReadLine().Trim();

            } while (!FunctionalityFunctions.CheckIsValidEmail(email));

            return email;
        }

        public static void LoginUser(Marketplace marketplace)
        {
            var email = GetUserDataEmail();

            var buyer = FunctionalityFunctions.CheckIfIsBuyer(marketplace, email);

            if (buyer != null)
            {
                BuyerMenu.ViewBuyerMenu(marketplace, buyer);
                return;
            }

            var seller = FunctionalityFunctions.CheckIfIsSeller(marketplace, email);

            if (seller != null)
            {
                SellerMenu.ViewSellerMenu(marketplace, seller);
                return;
            }

            Console.WriteLine($"\nNema korisnika sa emailom: {email}");
        }
    }
}
