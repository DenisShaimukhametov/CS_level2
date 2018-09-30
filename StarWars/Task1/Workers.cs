using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Workers
    {
        public Worker[] arr; 
        Random rand = new Random();

        public Workers()
        {
            arr = new Worker[5];
            for (int i = 0; i < 5; i++)
            {
                switch (rand.Next(1))
                {
                    case 0: arr[i] = (new WorkerHourPayment("имя_" + i, rand.Next(150), rand.Next(15000))); break;
                    default: arr[i] = (new WorkerFixedPayment("имя_" + i, rand.Next(20000))); break;
                }
            }
        }

        IEnumerator GetEnumerator(Worker[] array)
        {
            return array.GetEnumerator();
        }

        /// <summary>
        /// Метод выводит на консоль список работников
        /// </summary>
        /// <param name="arrayWorkers"> массив работников </param>
        public static void WriteListWorkers(Worker[] arrayWorkers)
        {
            foreach (var item in arrayWorkers)
            {
                Console.WriteLine("Имя: {0}, зарплата: {1}", item.Name, item.Payment);
            }
        }

    }

}
