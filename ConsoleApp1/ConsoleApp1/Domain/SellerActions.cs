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

        public static void ShowTotalSalesIncome(Seller seller) {
            Console.WriteLine($"\nUkupna zarada od prodaje za prodavaca {seller.name} iznosi: {seller.GetIncome():F2}");
        }
        public static void AddSaleIncome(Seller seller,double price)
        {
            price = price*0.95;

            seller.AddIncome(price);
            Console.WriteLine($"\nProdavacev racun uvecan za {price}");
        }

        public static void ShowTotalSalesInTimePeriod(Marketplace marketplace, Seller seller) {

            DateTime startDate;
            bool validStartDate = FunctionalityFunctions.GetDateFromUser(out startDate);
            while (!validStartDate)
            {
                Console.WriteLine("\nNeispravan pocetni datum.");
                validStartDate = FunctionalityFunctions.GetDateFromUser(out startDate);
            }

            
            DateTime endDate;
            bool validEndDate = FunctionalityFunctions.GetDateFromUser(out endDate);
            while (!validEndDate || startDate > endDate)
            {
                Console.WriteLine("\nNeispravan zavrsni datum.");
                validEndDate = FunctionalityFunctions.GetDateFromUser(out endDate);
            }


            double totalEarnings = 0;
            foreach (var transaction in marketplace.transactions)
            {
                if (transaction.seller == seller)
                {
                    if (transaction.transactionDate >= startDate && transaction.transactionDate <= endDate)
                    {

                        var product = marketplace.products.FirstOrDefault(p => p.GetId() == transaction.productId);
                        if (product != null)
                        {
                            double sellerEarnings = product.price * 0.95;
                            totalEarnings += sellerEarnings;
                        }
                    }
                }
            }

            Console.WriteLine($"Ukupna zarada prodavaca {seller.name} između {startDate:dd.MM.yyyy.} i {endDate:dd.MM.yyyy.} je: {totalEarnings:F2}");
        }

        public static void DeductSaleIncome(Seller seller, double refundAmount, double totalPrice)
        {
            double sellerIncome = totalPrice * 0.95;

            double finalSellerIncome = sellerIncome - refundAmount;

            seller.AddIncome(-refundAmount);
            Console.WriteLine($"\nProdavacev racun umanjen za {refundAmount:F2}.");
            Console.WriteLine($"\nUkupan saldo prodavaca nakon povrata: {seller.GetIncome():F2}");
        }
    }
}
