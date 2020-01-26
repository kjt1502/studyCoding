using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class Class1
    {
        private string value1 = "defalut value";

        public ref string returnParam ()
        {
            return ref value1;
        }

        public void printString ()
        {
            Console.WriteLine("from Class1 :" + value1);
        }
    }
}
