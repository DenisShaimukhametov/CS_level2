using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    abstract class Worker: IComparable
    {
        public static List<Worker> Workers= new List<Worker>();

        public string Name { get; set; }
        public int Payment { get; set; }

        private Worker() { }
        public Worker(string Name, int Payment)
        {
            this.Name = Name;
            this.Payment = Payment;
        }
        /// <summary>
        /// Подсчет среднемесячной зарплаты сотрудника
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        public abstract float MiddleMounthPayment(Worker work);

        public int CompareTo(object obj)
        {
            Worker w = obj as Worker;
            if (w != null)
            {
                return this.Payment.CompareTo(w.Payment);
            }
            else
            {
                throw new Exception("Невозможно сравнить два объекта");
            }
        }
    }
}
