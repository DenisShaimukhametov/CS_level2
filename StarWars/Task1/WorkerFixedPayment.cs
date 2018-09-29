using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class WorkerFixedPayment: Worker
    {
        
        public WorkerFixedPayment(string Name, int Payment)
            : base(Name, Payment)
        {
 
        }

       public override float MiddleMounthPayment(Worker work)
        {
            return Convert.ToSingle(this.Payment);
        }
    }
}
