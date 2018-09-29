using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Workers list = new Workers();
            Workers.WriteListWorkers(list.arr);
            Array.Sort(list.arr);
            Console.WriteLine("\n");
            Workers.WriteListWorkers(list.arr);



            Console.ReadKey();
        }
    }
}
