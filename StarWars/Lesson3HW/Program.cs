using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson3HW
{
    public delegate T MyDelegate<T>(object o);

    class Source
    {
        public event  MyDelegate Run;
        public void Start()
        {
            Console.WriteLine("RUN");
            if (Run != null) Run(this);
        }
    }
    class Observer1 // Наблюдатель 1
    {
        public void Do(object o)
        {
            Console.WriteLine("Первый. Принял, что объект {0} побежал", o);
        }
    }
    class Observer2 // Наблюдатель 2
    {
        public void Do(object o)
        {
            Console.WriteLine("Второй. Принял, что объект {0} побежал", o);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Source s = new Source();
            Observer1 o1 = new Observer1();
            Observer2 o2 = new Observer2();
            MyDelegate<> d1 = new MyDelegate<>(o1.Do);
            s.Run += d1;
            s.Run += o2.Do;
            s.Start();
            s.Run -= d1;
            s.Start();
        }
    }
}
