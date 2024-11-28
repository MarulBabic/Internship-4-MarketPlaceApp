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
            var availableProducts = marketplace.products.Where(p => p.status == Data.Status.Na_prodaju).ToList();

            if (!availableProducts.Any())
            {
                Console.WriteLine("\nNema proizvoda na prodaju.");
                return;
            }
            DisplayProductDetails(availableProducts);
        }

        public static bool ShowAllPurchasedProducts(Marketplace marketplace, Buyer buyer) {
            Console.WriteLine("\nKupljeni proizvodi:");

            var purchasedProducts = marketplace.transactions
                .Where(t => t.buyer == buyer)
                .Select(t => marketplace.products.FirstOrDefault(p => p.GetId() == t.productId))
                .Where(p => p != null)
                .Distinct()
                .ToList();

            if (!purchasedProducts.Any())
            {
                Console.WriteLine("\nNemate kupljenih proizvoda.");
                return false;
            }

            DisplayProductDetails(purchasedProducts);

            return true;
        }

        public static void ShowProductsOfSellerByCategory(Marketplace marketplace, Seller seller)
        {
            var products = GetProductsByCategory(marketplace, seller);

            if (!products.Any())
            {
                Console.WriteLine($"\nProdavač {seller.name} nema proizvoda u odabranoj kategoriji.");
                return;
            }

            var filteredProducts = products.Where(p => p.status == Status.Prodano).ToList();

            if (!filteredProducts.Any()) {
                Console.WriteLine($"\nProdavač {seller.name} nema prodanih proizvoda u odabranoj kategoriji.");
                return;
            }

            Console.WriteLine($"\nProdani proizvodi prodavača {seller.name} u odabranoj kategoriji:");
            DisplayProductDetails(filteredProducts);
        }

        public static void ShowProductsByCategory(Marketplace marketplace)
        {
            var products = GetProductsByCategory(marketplace);

            if (!products.Any())
            {
                Console.WriteLine("\nNema proizvoda u odabranoj kategoriji.");
                return;
            }

            Console.WriteLine("\nProizvodi u odabranoj kategoriji:");
            DisplayProductDetails(products);
        }

        public static List<Product> GetProductsByCategory(Marketplace marketplace, Seller seller = null)
        {
            Console.WriteLine("\nOdaberite kategoriju proizvoda (Elektronika, Odjeca, Knjige, Namjestaj):");

            Category selectedCategory;

            Console.Write("\nUnos: ");
            while (!FunctionalityFunctions.ValidateCategory(Console.ReadLine(), out selectedCategory))
            {;
                Console.Write("\nUnos: ");
            }

            return marketplace.products
                .Where(p => p.category == selectedCategory && (seller == null || p.seller == seller))
                .ToList();
        }

        public static void ShowAllProductsOfSeller(Marketplace marketplace, Seller seller) {
            Console.WriteLine($"\nProizvodi od prodavaca {seller.name}:");


            var filteredProducts = marketplace.products.Where(p => p.seller == seller).ToList();
            if (!filteredProducts.Any())
            {
                Console.WriteLine("\nProdavac nema proizvoda");
                return;
            }
            DisplayProductDetails(filteredProducts);
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

        private static void DisplayProductDetails(List<Product> products)
        {
            foreach (var product in products)
            {
                Console.WriteLine($"\n Naziv proizvoda: {product.productName}\n Cijena proizvoda: {product.price:F2}" +
                                  $"\n Opis proizvoda: {product.productDescription}\n Id proizvoda: {product.GetId()}" +
                                  $"\n Status: {product.status}");
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

            var choice = 'c';


            while (choice != 'q')
            {
                bool isAlreadyReturned = marketplace.transactions.Any(t => t.productId == productToReturn?.GetId() && t.isReturnTransaction);
                bool isInvalidProduct = productToReturn == null || productToReturn.status != Data.Status.Prodano;

                if (isAlreadyReturned)
                {
                    Console.WriteLine("\nOvaj proizvod je već vraćen. Povrat nije moguć.");
                }
                else if (isInvalidProduct)
                {
                    Console.WriteLine("\nNeispravan unos ili proizvod nije kupljen.");
                }
                else
                {
                    break; 
                }

                Console.WriteLine("Za prekid radnje unesite 'q', a za nastavak pritisnite bilo koju drugu tipku.");
                Console.Write("\nUnos: ");
                choice = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (choice == 'q')
                {
                    Console.WriteLine("\nRadnja je prekinuta.");
                    return;
                }

                ShowAllPurchasedProducts(marketplace, buyer);
                productToReturn = ChooseProduct(marketplace);
            }

            var transaction = marketplace.transactions.FirstOrDefault(t => t.productId == productToReturn.GetId() && t.buyer == buyer);
            if (transaction == null)
            {
                Console.WriteLine("\nTransakcija nije pronađena.");
                return;
            }

            double finalPrice = transaction.finalPrice;  
            double refundAmount = finalPrice * 0.8;
            BuyerActions.ReturnAmount(buyer, refundAmount);

            SellerActions.DeductSaleIncome(productToReturn.seller, refundAmount, finalPrice);

            var returnTransaction = new Transaction(productToReturn.GetId(), buyer, productToReturn.seller)
            {
                isReturnTransaction = true,
                refundAmount = refundAmount,
                finalPrice = transaction.finalPrice 
            };
            marketplace.transactions.Add(returnTransaction);

            productToReturn.status = Data.Status.Na_prodaju;
            Console.WriteLine($"\nProizvod '{productToReturn.productName}' je uspješno vraćen.");
        }

        public static void BuyProduct(Marketplace marketplace, Buyer buyer)
        {

            ShowAllAvailableProducts(marketplace);

            var product = ChooseProduct(marketplace);

            while (product.status == Status.Prodano) {
                Console.WriteLine("\nProizvod je vec kupljen, pokusajte sa drugim");
                product = ChooseProduct(marketplace);
            }

            Console.Write("\nUnesite promotivni kod (ili pritisnite Enter za nastavak bez koda): ");
            string promoCode = Console.ReadLine().Trim();

            double finalPrice = product.price;

            if (!string.IsNullOrWhiteSpace(promoCode))
            {
                finalPrice = PromoCodeActions.ApplyPromotionalCode(product, promoCode, marketplace);
            }

            if (finalPrice == -1)
            {
                Console.WriteLine("\nKupnja je otkazana. Vracate se na pocetni izbornik.");
                return; 
            }

            if (buyer.balance < finalPrice)
            {
                Console.WriteLine("\nNemate dovoljno sredstava za kupovinu ovog proizvoda.");
                return;
            }

            double marketplaceCommission = CalculateMarketplaceCommission(finalPrice);
            marketplace.totalFunds += marketplaceCommission;

            var transaction = new Transaction(product.GetId(), buyer, product.seller, promoCode);
            transaction.finalPrice = finalPrice;

            MarketplaceActions.AddTransaction(marketplace, transaction);
            SellerActions.AddSaleIncome(product.seller, finalPrice);
            BuyerActions.DeductAmount(buyer, finalPrice);
            product.status = Data.Status.Prodano;

            Console.WriteLine($"\nProizvod: {product.productName}, cijena: {finalPrice}, uspjesno kupljen");
        }

        private static double CalculateMarketplaceCommission(double finalPrice)
        {
            return finalPrice * 0.05;
        }

        public static void AddProduct(Marketplace marketplace, Seller seller)
        {
            var productName = EnterProductName();
            var productDescription = EnterProductDesc();
            var productPrice = EnterProductPrice();
            var productCategory = AssignCategoryBasedOnProductName(productName);

            Product product = new Product(productName, productDescription, productPrice, productCategory, seller);
            MarketplaceActions.AddProduct(marketplace,product);
            Console.WriteLine($"\nProizvod: {productName} uspjesno dodan");
        }

        private static string EnterProductName()
        {
            Console.WriteLine("\nUnesite naziv proizvoda: ");
            Console.Write("\nUnos: ");
            var productName = Console.ReadLine().Trim();

            while (!FunctionalityFunctions.ValidateProductName(productName))
            {
                Console.Write("\nUnos: ");
                productName = Console.ReadLine();
            }

            return productName;
        }

        private static Category AssignCategoryBasedOnProductName(string productName)
        {
            string lowerCaseProductName = productName.ToLower();

            var electronicsKeywords = new[] { "laptop", "monitor", "tipkovnica", "racunalo", "telefon", "tv", "kalkulator", "projektor", "tablet", "slusalice", "kamera" };
            var clothingKeywords = new[] { "majica", "hlace", "pulover", "jakna", "dzemper", "suknja", "kaput", "kosulja", "sal", "trenerka" };
            var booksKeywords = new[] { "knjiga", "roman", "poezija", "novela", "prirucnik", "enciklopedija", "biografija", "strip"};
            var furnitureKeywords = new[] { "stolica", "stol", "krevet", "kauc", "ormar", "vrata", "polica", "fotelja" };

            if (electronicsKeywords.Any(keyword => lowerCaseProductName.Equals(keyword)))
            {
                return Category.Elektronika;
            }
            else if (clothingKeywords.Any(keyword => lowerCaseProductName.Equals(keyword)))
            {
                return Category.Odjeca;
            }
            else if (booksKeywords.Any(keyword => lowerCaseProductName.Equals(keyword)))
            {
                return Category.Knjige;
            }
            else if (furnitureKeywords.Any(keyword => lowerCaseProductName.Equals(keyword)))
            {
                return Category.Namjestaj;
            }

            Console.WriteLine("\nNismo prepoznali kategoriju za ovaj proizvod. Molimo vas da odaberete kategoriju ručno:\n" +
                  "1. Elektronika\n" +
                  "2. Odjeća\n" +
                  "3. Knjige\n" +
                  "4. Namještaj");
            Console.Write("\nOdaberite kategoriju (1-4): ");
            int categoryChoice = int.Parse(Console.ReadLine());

            switch (categoryChoice)
            {
                case 1:
                    return Category.Elektronika;
                case 2:
                    return Category.Odjeca;
                case 3:
                    return Category.Knjige;
                case 4:
                    return Category.Namjestaj;
                default:
                    Console.WriteLine("\nNevažeći unos, postavljena je zadana kategorija: Elektronika.");
                    return Category.Elektronika;
            }
        }

        private static string EnterProductDesc()
        {
            Console.WriteLine("\nUnesite opis proizvoda: ");
            Console.Write("\nUnos: ");
            var productDescription = Console.ReadLine();

            while (!FunctionalityFunctions.ValidateProductDesc(productDescription))
            {
                Console.Write("\nUnos: ");
                productDescription = Console.ReadLine();
            }

            return productDescription;
        }

        private static double EnterProductPrice()
        {
            Console.WriteLine("\nUnesite cijenu proizvoda: ");
            Console.Write("\nUnos: ");
            double productPrice;
            string inputPrice = Console.ReadLine();

            while (!double.TryParse(inputPrice, out productPrice) || productPrice <= 0)
            {
                Console.WriteLine("\nNeispravan unos cijene (mora biti pozitivan broj). Pokušajte ponovo.");
                Console.Write("\nUnos: ");
                inputPrice = Console.ReadLine();
            }

            return productPrice;
        }

        public static Product ChooseProduct(Marketplace marketplace) {

            while (true)
            {
                Console.Write("\nUnesite ID proizvoda: ");
                Console.Write("\nUnos: ");

                if (int.TryParse(Console.ReadLine(), out var productId))
                {
                    var product = marketplace.products.FirstOrDefault(p => p.GetId() == productId);

                    if (product != null)
                    {
                        return product;
                    }
                    Console.WriteLine("\nProizvod s tim ID-em nije pronađen. Pokušajte ponovo.");
                }
                else
                {
                    Console.WriteLine("\nNeispravan ID proizvoda. Pokušajte ponovo.");
                }
            }
        }
    }
}