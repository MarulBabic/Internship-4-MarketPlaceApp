using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    public class Buyer : User
    {
        public double balance { get; set; }
        public List<Product> favorites { get; private set; }
        public Buyer(string name, string email, double balance) : base(name, email)
        {
            this.balance = balance;
            favorites = new List<Product>();
        }

        public void SetBalance(double price)
        {
            balance += price;
        }

        public void AddToFavorites(Product product) {

            if (favorites.Contains(product))
            {
                Console.WriteLine("\nProizvod je već u listi favorita.");
                return;
            }

            favorites.Add(product);
            Console.WriteLine($"\nProizvod '{product.productName}' uspjesno dodan u favorite.");
        }

    }
}
