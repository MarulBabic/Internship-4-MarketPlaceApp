﻿using ConsoleApp1.Data;
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
            Console.WriteLine("\nPritisnite bilo koju tipku za nastavak kao prodavac...");
            Console.ReadKey();
            return new Seller(sellerName, sellerEmail);
        }

        public static void ChangeProductPrice(Seller seller, Marketplace marketplace)
        {
            Console.WriteLine("\nProizvodi u vlasništvu prodavača:");
            var sellerProducts = marketplace.Products.Where(p => p.Seller == seller && p.Status == Data.Status.Na_prodaju).ToList();

            if (!sellerProducts.Any())
            {
                Console.WriteLine("\nNemate proizvoda u prodaji.");
                return;
            }

            foreach (var product in sellerProducts)
            {
                Console.WriteLine($"\nID: {product.GetId()} | Naziv: {product.ProductName} | Trenutna cijena: {product.Price:F2}");
            }

            Console.WriteLine("\nOdaberite proizvod kojem želite promijeniti cijenu:\n");
            Product selectedProduct = null;

            while (selectedProduct == null || selectedProduct.Seller != seller || selectedProduct.Status != Data.Status.Na_prodaju)
            {
                selectedProduct = ProductActions.ChooseProduct(marketplace);

                if (selectedProduct == null || selectedProduct.Seller != seller || selectedProduct.Status != Data.Status.Na_prodaju)
                {
                    Console.WriteLine("\nNeispravan odabir. Molimo odaberite proizvod iz vašeg vlasništva koji je u prodaji.");
                }
            }

            Console.WriteLine($"\nOdabrali ste proizvod: {selectedProduct.ProductName}, trenutna cijena: {selectedProduct.Price:F2}");
            Console.WriteLine("\nUnesite novu cijenu:");

            double newPrice;
            Console.Write("\nUnos: ");
            while (!double.TryParse(Console.ReadLine(), out newPrice) || newPrice <= 0)
            {
                Console.WriteLine("\nNeispravna cijena. Unesite pozitivan broj.");
                Console.Write("\nUnos: ");
            }

            selectedProduct.SetPrice(newPrice);
            Console.WriteLine($"\nCijena proizvoda '{selectedProduct.ProductName}' uspješno promijenjena na {newPrice:F2}.");
        }

        public static void ShowTotalSalesIncome(Seller seller) {
            Console.WriteLine($"\nUkupna zarada od prodaje za prodavaca {seller.Name} iznosi: {seller.GetIncome():F2}");
        }
        public static void AddSaleIncome(Seller seller,double price)
        {
            price = price*0.95;

            seller.AddIncome(price);
            Console.WriteLine($"\nProdavacev racun uvecan za {price}");
        }

        public static void ShowTotalSalesInTimePeriod(Marketplace marketplace, Seller seller) {

            DateTime startDate;
            bool validStartDate = FunctionalityFunctions.GetDateFromUser(out startDate,true);
            while (!validStartDate)
            {
                Console.WriteLine("\nNeispravan pocetni datum.");
                validStartDate = FunctionalityFunctions.GetDateFromUser(out startDate,true);
            }

            
            DateTime endDate;
            bool validEndDate = FunctionalityFunctions.GetDateFromUser(out endDate,false);
            while (!validEndDate || startDate > endDate)
            {
                Console.WriteLine("\nNeispravan zavrsni datum.");
                validEndDate = FunctionalityFunctions.GetDateFromUser(out endDate,false);
            }


            double totalEarnings = 0;
            foreach (var transaction in marketplace.Transactions)
            {
                if (transaction.Seller == seller)
                {
                    if (transaction.TransactionDate >= startDate && transaction.TransactionDate <= endDate)
                    {

                        if (!transaction.IsReturnTransaction)
                        {
                            double sellerEarnings = transaction.FinalPrice * 0.95;
                            totalEarnings += sellerEarnings;
                        }
                        else
                        {
                            totalEarnings -= transaction.RefundAmount;
                        }
                    }
                }
            }

            Console.WriteLine($"\nUkupna zarada prodavaca {seller.Name} između {startDate:dd.MM.yyyy.} i {endDate:dd.MM.yyyy.} je: {totalEarnings:F2}");
        }

        public static void DeductSaleIncome(Seller seller, double refundAmount, double totalPrice)
        {
            double sellerIncome = totalPrice * 0.95;

            double finalSellerIncome = sellerIncome - refundAmount;

            seller.AddIncome(-refundAmount);
            Console.WriteLine($"\nProdavacev racun umanjen za {refundAmount:F2}.");
            Console.WriteLine($"\nUkupan saldo prodavaca nakon povrata: {seller.GetIncome():F2}");
        }

        public static void ShowSellersBalance(Seller seller)
        {
            if (seller == null)
            {
                Console.WriteLine("\nProdavac ne postoji.");
                return;
            }

            Console.WriteLine($"\nStanje na racunu kupca {seller.Name}: {seller.Income:F2}");
        }
    }
}
