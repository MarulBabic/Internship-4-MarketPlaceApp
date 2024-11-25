using ConsoleApp1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Presentation
{
    public class BuyerMenu
    {
        public static void ViewBuyerMenu(Marketplace marketplace, Buyer buyer)
        {
            var option = 0;
            Console.WriteLine("\n1 - Pregled svih proizvoda\n2 - Kupnja proizvoda koristeci id proizvoda\n3 - Povratak kupljenog proizvoda" + 
                "\n4 - Dodavanje proizvoda u listu omiljenih\n5 - Pregled povijesti kupljenih proizvoda\n6 - Pregled liste omiljenih proizvoda");

            do
            {
                Console.Write("\nUnos: ");
                int.TryParse(Console.ReadLine(), out option );
                switch (option)
                {
                    case 1:
                        ProductActions.ShowAllAvailableProducts(marketplace);
                        return;
                    default:
                        Console.WriteLine("Pogresan unos, pokusajte ponovo");
                        break;
                }

            } while (true);
        }
    }
}
