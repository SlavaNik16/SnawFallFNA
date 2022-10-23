using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    static class Programm
    {
        static void Main(string[] args)
        {
            using (SnowFall snowfall = new SnowFall())
            {
                snowfall.Run();
            }
        }
    }
}
