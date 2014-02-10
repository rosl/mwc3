using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuidCreator
{
    using Ru.Logging;

    class Program
    {
        private static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                var myGuid = Guid.NewGuid().ToString();
                Console.WriteLine(myGuid);
                myLog.WriteToLog(myGuid);
            }
            Console.ReadLine();
        }
    }
}
