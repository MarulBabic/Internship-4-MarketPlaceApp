﻿using ConsoleApp1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Presentation
{
    public class SellerMenu
    {
        public static void ViewSellerMenu(Marketplace  marketplace, Seller seller)
        {
            var option = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("\n1 - Dodajte proizvod\n2 - Pregled svih proizvoda od prodavaca\n3 - Pregled ukupne zarade" +
                    "\n4 - Pregled prodanih proizvoda po kategoriji\n5 - Pregled zarade u odredenom vremenskom razdoblju\n6 - Promjena cijene proizvoda" +
                    "\n7 - Ukupno stanje na racunu\n8 - Povratak na pocetni izbornik");

                Console.Write("\nUnos: ");
                int.TryParse(Console.ReadLine(), out option );

                switch (option) {
                    case 1:
                        ProductActions.AddProduct(marketplace, seller);
                        break;
                    case 2:
                        ProductActions.ShowAllProductsOfSeller(marketplace, seller);
                        break;
                    case 3:
                        SellerActions.ShowTotalSalesIncome(seller);
                        break;
                    case 4:
                        ProductActions.ShowProductsOfSellerByCategory(marketplace, seller);
                        break;
                    case 5:
                        SellerActions.ShowTotalSalesInTimePeriod(marketplace, seller);
                        break;
                    case 6:
                        SellerActions.ChangeProductPrice(seller, marketplace);
                        break;
                    case 7:
                        SellerActions.ShowSellersBalance(seller);
                        break;
                    case 8:
                        return;
                    default:
                        Console.WriteLine("\nPogresan unos, pokusajte ponovno");
                        break;
                }
                Console.WriteLine("\nPritisnite bilo koju tipku za nastavak...");
                Console.ReadKey();
            } while (true);
        }
    }
}
