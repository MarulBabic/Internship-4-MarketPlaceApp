using ConsoleApp1.Data;
using ConsoleApp1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Presentation
{
    public class HomeMenu
    {
        public static void ViewHomeMenu(Marketplace marketplace)
        {
            var option = 0;

            do
            {
                Console.WriteLine("\n1 - Registracija novog korisnika\n2 - Prijava vec postojeceg korisnika\n3 - Prikaz svih transakcija" +
               "\n4 - Izlaz iz aplikacije");
                Console.Write("\nUnos: ");
                int.TryParse(Console.ReadLine(), out option);
                switch (option)
                {
                    case 1:
                        FunctionalityFunctions.ChooseUserType(marketplace);
                        break;
                    case 2:
                        UserActions.LoginUser(marketplace);
                        break;
                    case 3:
                        TransactionActions.ViewAllTransactions(marketplace);
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Pogresan unos, pokusajte ponovo");
                        break;
                }

            } while (true);

        }
    }
}
