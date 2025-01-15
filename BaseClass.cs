using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class BaseClass
    {
        private decimal x1;
        public int X { get; set; }
        public BaseClass()
        {
            X++;
        }
    }
    public class Derived : BaseClass
    {
        public Derived(int x)
        {
            X = x;
        }
    }
}
