using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson3HW
{
    class MyDelegate_Generic : EventArgs
    {
        public MyDelegate_Generic(T Target)
        {
            _TargetObject = Target;
        }
        private T _TargetObject;
        public T TargetObject
        {
            get
            {
                return _TargetObject;
            }
            set
            {
                _TargetObject = value;
            }
        }
    }
}
