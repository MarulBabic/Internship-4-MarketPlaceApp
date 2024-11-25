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

            foreach (var product in availableProducts) {
                Console.WriteLine($"\n Naziv proizvoda: {product.productName}\n Cijena proizvoda: {product.price}" +
                    $"\n Opis proizvoda: {product.productDescription}");
            }
        }
    }
}
