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