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

            do
            {
                Console.Clear();
                Console.WriteLine("\n1 - Pregled svih proizvoda\n2 - Kupnja proizvoda koristeci id proizvoda\n3 - Povratak kupljenog proizvoda" +
               "\n4 - Dodavanje proizvoda u listu omiljenih\n5 - Pregled povijesti kupljenih proizvoda\n6 - Pregled liste omiljenih proizvoda" +
               "\n7 - Ukupno stanje na racunu\n8 - Povratak na pocetni izbornik");
                Console.Write("\nUnos: ");
                int.TryParse(Console.ReadLine(), out option );
                switch (option)
                {
                    case 1:
                        ProductActions.ShowAllAvailableProducts(marketplace);
                        break;
                    case 2:
                        ProductActions.BuyProduct(marketplace,buyer);
                        break;
                    case 3:
                        ProductActions.ReturnProduct(marketplace,buyer);
                        break;
                    case 4:
                        ProductActions.AddToFavorites(marketplace, buyer);
                        break;
                    case 5:
                        ProductActions.ShowAllPurchasedProducts(marketplace, buyer);
                        break;
                    case 6:
                        ProductActions.ShowAllFavorites(buyer);
                        break;
                    case 7:
                        BuyerActions.ShowBuyersBalance(buyer);
                        break;
                    case 8:
                        return;
                    default:
                        Console.WriteLine("Pogresan unos, pokusajte ponovo");
                        break;
                }
                Console.WriteLine("\nPritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();

            } while (true);
        }
    }
}
