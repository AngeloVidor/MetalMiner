using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace scrap
{
    public class Menu
    {
        private string _bandName;
        private string _bandGenre;

        public Menu()
        {
            _bandName = string.Empty;
            _bandGenre = string.Empty;
        }


        public async Task DisplayMenu(int input)
        {
            CollectData data = new CollectData();

            switch (input)
            {
                case 1:
                    Console.WriteLine("Enter the name of the band you want to search for:");
                    _bandName = Console.ReadLine();
                    if (_bandName == null)
                    {
                        throw new ArgumentNullException(nameof(_bandName));
                    }
                    Console.WriteLine("Enter the genre of the band you want to search for:");
                    _bandGenre = Console.ReadLine();

                    await data.SearchForDataAsync(_bandName, _bandGenre);
                    break;

                case 2:
                    Console.WriteLine("Enter the name of the band you want to see the discography of:");
                    _bandName = Console.ReadLine();
                    Console.WriteLine("Enter the genre of the band you want to see the discography of:");
                    _bandGenre = Console.ReadLine();
                    await data.GetBandDiscographyAsync(_bandName, _bandGenre);
                    break;

                case 3:
                    Console.WriteLine("Enter the name of the band you want to get the ID:");
                    _bandName = Console.ReadLine();
                    await data.GetBandIdAsync(_bandName);
                    break;

            }
        }
    }
}