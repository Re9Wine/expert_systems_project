using DAL;
using Domain.Entity;

namespace Tests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();

            Console.WriteLine("End!");
        }
    }
}