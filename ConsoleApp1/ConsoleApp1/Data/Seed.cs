using ConsoleApp1.Domain;
using ConsoleApp1.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Data
{
    public static class Seed
    {
        public static void SeedData(Marketplace marketplace) {
            
            Buyer buyer1 = new Buyer("Ana", "ana@gmail.com", 2000);
            Buyer buyer2 = new Buyer("Petar", "petar@gmail.com", 1800);
            Buyer buyer3 = new Buyer("Maja", "maja@gmail.com", 2500);
            marketplace.Buyers.Add(buyer1);
            marketplace.Buyers.Add(buyer2);
            marketplace.Buyers.Add(buyer3);

            Seller seller1 = new Seller("Katarina", "katarina@gmail.com");
            Seller seller2 = new Seller("Luka", "luka@gmail.com");
            Seller seller3 = new Seller("Ivana", "ivana@gmail.com");
            marketplace.Sellers.Add(seller1);
            marketplace.Sellers.Add(seller2);
            marketplace.Sellers.Add(seller3);

            Product product4 = new Product("monitor", "LCD monitor", 700, Data.Category.Elektronika, seller1);
            Product product5 = new Product("tablet", "10'' ekran, 64GB", 900, Data.Category.Elektronika, seller2);
            Product product6 = new Product("projektor", "Full HD projektor", 1500, Data.Category.Elektronika, seller3);

            Product product7 = new Product("majica", "Pamucna majica", 50, Data.Category.Odjeca, seller3);
            Product product8 = new Product("jakna", "Zimska jakna", 300, Data.Category.Odjeca, seller2);
            Product product9 = new Product("trenerka", "Sportska trenerka", 120, Data.Category.Odjeca, seller1);

            Product product10 = new Product("roman", "Ljubavni roman", 80, Data.Category.Knjige, seller2);
            Product product11 = new Product("strip", "Superhero strip", 60, Data.Category.Knjige, seller1);
            Product product12 = new Product("enciklopedija", "Enciklopedija prirode", 200, Data.Category.Knjige, seller3);

            Product product13 = new Product("stol", "Drveni stol", 450, Data.Category.Namjestaj, seller2);
            Product product14 = new Product("stolica", "Udobna stolica", 150, Data.Category.Namjestaj, seller3);
            Product product15 = new Product("ormar", "Veliki ormar s ogledalom", 1000, Data.Category.Namjestaj, seller1);

            marketplace.Products.Add(product4);
            marketplace.Products.Add(product5);
            marketplace.Products.Add(product6);
            marketplace.Products.Add(product7);
            marketplace.Products.Add(product8);
            marketplace.Products.Add(product9);
            marketplace.Products.Add(product10);
            marketplace.Products.Add(product11);
            marketplace.Products.Add(product12);
            marketplace.Products.Add(product13);
            marketplace.Products.Add(product14);
            marketplace.Products.Add(product15);

            PromoCode promo1 = new PromoCode("Fall2024", 15.0, DateTime.ParseExact("12-12-2024", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture), new List<Category> { Category.Elektronika });
            PromoCode promo2 = new PromoCode("Winter2025", 20.0, DateTime.ParseExact("12-02-2025", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture), new List<Category> { Category.Knjige });
            PromoCode promo3 = new PromoCode("Summer2024", 25.0, DateTime.ParseExact("12-09-2024", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture), new List<Category> { Category.Knjige, Category.Odjeca });

            MarketplaceActions.AddPromoCode(marketplace, promo1);
            MarketplaceActions.AddPromoCode(marketplace, promo2);
            MarketplaceActions.AddPromoCode(marketplace, promo3);

            HomeMenu.ViewHomeMenu(marketplace);
        }
    }
}
