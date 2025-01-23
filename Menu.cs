using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace scrap
{
    public class Menu
    {
        public async Task DisplayMenu(int input)
        {
            switch (input)
            {
                case 1:
                    Console.WriteLine("Enter the name of the band you want to search for:");
                    string name = Console.ReadLine();
                    if (name == null)
                    {
                        throw new ArgumentNullException(nameof(name));
                    }
                    Console.WriteLine("Enter the genre of the band you want to search for:");
                    string? genre = Console.ReadLine();

                    CollectData data = new CollectData();

                    await data.SearchForDataAsync(name, genre);
                    break;
            }
        }
    }
}