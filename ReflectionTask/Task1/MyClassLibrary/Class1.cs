using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyClassLibrary
{
    public class Class1 : IExampleInterface
    {
        public void Method1() { }
    }

    public class Class2 : Class1
    {
        public void Method2() { }
    }

    public interface IExampleInterface
    {
        void Method1();
    }
}

