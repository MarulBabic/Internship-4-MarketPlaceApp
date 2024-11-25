using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    public class ProductActions
    {
        public static void ShowAllAvailableProducts(Marketplace marketplace)
        {
            var availableProducts = marketplace.products.Where(p => p.status == Data.Status.Na_prodaju);

            foreach (var product in availableProducts)
            {
                Console.WriteLine($"\n Naziv proizvoda: {product.productName}\n Cijena proizvoda: {product.price}" +
                    $"\n Opis proizvoda: {product.productDescription}\n Id proizvoda: {product.GetId()}");
            }
        }

        public static bool ShowAllPurchasedProducts(Marketplace marketplace, Buyer buyer) {
            Console.WriteLine("\nKupljeni proizvodi:");

            var purchasedProducts = marketplace.transactions
                .Where(t => t.buyer == buyer)
                .Select(t => marketplace.products.FirstOrDefault(p => p.GetId() == t.productId && p.status == Data.Status.Prodano))
                .Where(p => p != null)
                .Distinct()
                .ToList();

            if (!purchasedProducts.Any())
            {
                Console.WriteLine("\nNemate kupljenih proizvoda za povrat.");
                return false;
            }

            foreach (var product in purchasedProducts)
            {
                Console.WriteLine($"\n Naziv proizvoda: {product.productName}\n Cijena proizvoda: {product.price}" +
                                  $"\n Opis proizvoda: {product.productDescription}\n Id proizvoda: {product.GetId()}");
            }
            return true;
        }

        public static void ShowAllFavorites(Buyer buyer)
        {
            if (!buyer.favorites.Any())
            {
                Console.WriteLine("\nLista favorita je prazna.");
                return;
            }

            Console.WriteLine("\nVasi omiljeni proizvodi:");
            foreach (var product in buyer.favorites)
            {
                Console.WriteLine($"\n Naziv proizvoda: {product.productName}\n Cijena proizvoda: {product.price}" +
                                  $"\n Opis proizvoda: {product.productDescription}\n Id proizvoda: {product.GetId()}");
            }
        }

        public static void AddToFavorites(Marketplace marketplace, Buyer buyer)
        {
            if (!ShowAllPurchasedProducts(marketplace, buyer))
            {
                return;
            }

            Console.WriteLine("\nOdaberite proizvod koji zelite dodati u favorite:");
            var productToAdd = ChooseProduct(marketplace);

            while(productToAdd == null || productToAdd.status != Data.Status.Prodano)
            {
                Console.WriteLine("\nNe mozete dodati nevazeci proizvod ili proizvod koji nije kupljen u favorite.");
                productToAdd = ChooseProduct(marketplace);
            }

            buyer.AddToFavorites(productToAdd);

        }

        public static void ReturnProduct(Marketplace marketplace, Buyer buyer) {
           if(!ShowAllPurchasedProducts(marketplace, buyer)) 
            {
                return;
            }

            var productToReturn = ChooseProduct(marketplace);

            while(productToReturn == null || productToReturn.status != Data.Status.Prodano)
            {
                Console.WriteLine("\nNeispravan unos ili proizvod nije kupljen.");
                ShowAllPurchasedProducts(marketplace, buyer);
                productToReturn = ChooseProduct(marketplace);
            }

            BuyerActions.ReturnAmount(buyer,productToReturn.price);
            SellerActions.DeductSaleIncome(productToReturn.seller, productToReturn.price);
            productToReturn.status = Data.Status.Na_prodaju;
            Console.WriteLine($"\nProizvod '{productToReturn.productName}' je uspješno vraćen.");
        }

        public static void BuyProduct(Marketplace marketplace, Buyer buyer)
        {

            ShowAllAvailableProducts(marketplace);

            var product = ChooseProduct(marketplace);

            if(buyer.balance < product.price)
            {
                Console.WriteLine("\nNemate dovoljno novaca na racunu");
                return;
            }

            marketplace.transactions.Add(new Transaction(product.GetId(), buyer, product.seller));
            
            SellerActions.AddSaleIncome(product.seller, product.price);
            BuyerActions.DeductAmount(buyer,product.price);
            marketplace.totalFunds += (product.price * 0.05);
            product.status = Data.Status.Prodano;

            Console.WriteLine($"\nProizvod: {product.productName}, cijena: {product.price}, uspjesno kupljen");

        }

        public static Product ChooseProduct(Marketplace marketplace) {

            var productId = -1;

            while (true)
            {
                Console.Write("\nUnesite ID proizvoda: ");
                Console.Write("\nUnos: ");
                var input = Console.ReadLine();

                if (int.TryParse(input, out productId))
                {
                    var product = marketplace.products.FirstOrDefault(p => p.GetId() == productId);

                    if (product != null)
                    {
                        return product;
                    }
                    else
                    {
                        Console.WriteLine("\nProizvod s tim ID-em nije pronađen. Pokušajte ponovo.");
                    }
                }
                else
                {
                    Console.WriteLine("\nNeispravan ID proizvoda. Pokušajte ponovo.");
                }
            }
        }
    }
}