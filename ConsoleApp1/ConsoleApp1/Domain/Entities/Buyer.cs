using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    public class Buyer : User
    {
        public double Balance { get; private set; }
        public List<Product> Favorites { get; private set; }
        public Buyer(string name, string email, double balance) : base(name, email)
        {
            Balance = balance;
            Favorites = new List<Product>();
        }

        public void UpdateBalance(double price)
        {
            Balance += price;
        }

        public void AddToFavorites(Product product) {

            if (Favorites.Contains(product))
            {
                Console.WriteLine("\nProizvod je već u listi favorita.");
                return;
            }

            Favorites.Add(product);
            Console.WriteLine($"\nProizvod '{product.ProductName}' uspjesno dodan u favorite.");
        }

    }
}
