using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class WorkerHourPayment: Worker
    {
        public int Hour { get; set; }
        public WorkerHourPayment(string Name, int Payment, int Hour)
            : base(Name, Payment)
        {
            this.Hour = Hour;
        }
        const float coefficient = 20.8f;
        const int hourADay = 8;

       public override float MiddleMounthPayment(Worker work)
        {
            return coefficient*hourADay*this.Hour;
        }
    }
}
