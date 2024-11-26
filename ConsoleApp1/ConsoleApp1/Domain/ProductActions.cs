using ConsoleApp1.Data;
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

        public static void ShowProductsOfSellerByCategory(Marketplace marketplace, Seller seller)
        {
            Console.WriteLine("\nOdaberite kategoriju proizvoda (Elektronika, Odjeća, Knjige, Namještaj):");

            Category selectedCategory;

            Console.Write("\nUnos: ");
            while (!FunctionalityFunctions.ValidateCategory(Console.ReadLine(), out selectedCategory))
            {
                Console.Write("\nUnos: ");
            }

            var productsOfCategory = marketplace.products
             .Where(p => p.seller == seller && p.category == selectedCategory)
             .ToList();

            if (!productsOfCategory.Any())
            {
                Console.WriteLine($"\nProdavac {seller.name} nema proizvoda u kategoriji {selectedCategory}.");
                return;
            }

            Console.WriteLine($"\nProizvodi prodavaca {seller.name} u kategoriji {selectedCategory}:");
            foreach (var product in productsOfCategory)
            {
                Console.WriteLine($"\n Naziv proizvoda: {product.productName}\n Cijena proizvoda: {product.price}" +
                                  $"\n Opis proizvoda: {product.productDescription}\n Id proizvoda: {product.GetId()}" +
                                  $"\n Status: {product.status}");
            }
        }

        public static void ShowAllProductsOfSeller(Marketplace marketplace, Seller seller) {
            Console.WriteLine($"\nProizvodi od prodavaca {seller.name}:");

            var filteredProducts = marketplace.products.Where(p => p.seller == seller);

            foreach (var product in filteredProducts) {
                Console.WriteLine($"\n Naziv proizvoda: {product.productName}\n Cijena proizvoda: {product.price}" +
                                  $"\n Opis proizvoda: {product.productDescription}\n Id proizvoda: {product.GetId()}" +
                                  $"\n Status: {product.status}");
            }
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
                Console.WriteLine("\nNe mozete dodati nevazeci proizvod ili proizvod koji nije kupljen u favorite. Pokusajte sa drugim id-em");
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

            double refundAmount = productToReturn.price * 0.8;

            BuyerActions.ReturnAmount(buyer,refundAmount);
            SellerActions.DeductSaleIncome(productToReturn.seller, refundAmount, productToReturn.price);

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

        public static void AddProduct(Marketplace marketplace, Seller seller)
        {
            string productName;
            string productDescription;
            double productPrice;

            Console.WriteLine("\nUnesite naziv proizvoda: ");
            Console.Write("\nUnos: ");
            productName = Console.ReadLine();

            while (!FunctionalityFunctions.ValidateProductName(productName))
            {
                Console.Write("\nUnos: ");
                productName = Console.ReadLine();
            }

            Console.WriteLine("\nUnesite opis proizvoda: ");
            Console.Write("\nUnos: ");
            productDescription = Console.ReadLine();

            while (!FunctionalityFunctions.ValidateProductDesc(productDescription))
            {
                Console.Write("\nUnos: ");
                productDescription = Console.ReadLine();
            }

            Console.WriteLine("\nUnesite cijenu proizvoda: ");
            Console.Write("\nUnos: ");
            string inputPrice = Console.ReadLine();

            while (!double.TryParse(inputPrice, out productPrice) || productPrice <= 0)
            {
                Console.WriteLine("\nNeispravan unos cijene (mora biti pozitivan broj). Pokušajte ponovo.");
                Console.Write("\nUnos: ");
                inputPrice = Console.ReadLine(); 
            }

            Product product = new Product(productName, productDescription, productPrice, Category.Elektronika, seller);
            marketplace.products.Add(product);
            Console.WriteLine($"\nProizvod: {productName} uspjesno dodan");
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