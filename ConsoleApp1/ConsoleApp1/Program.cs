using ConsoleApp1.Data;
using ConsoleApp1.Domain;
using ConsoleApp1.Presentation;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Marketplace marketplace = new Marketplace();

            Seed.SeedData(marketplace);

        }
    }
}
