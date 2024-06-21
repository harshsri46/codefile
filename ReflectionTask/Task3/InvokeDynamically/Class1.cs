using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvokeDynamically
{
    
        public class Class1 : IExampleInterface
        {
            public string Property1 { get; set; }
            public int Field1;

            public void Method1()
            {
                Console.WriteLine("Method1 called");
            }

            public void MethodWithParameters(string param1, int param2)
            {
                Console.WriteLine($"MethodWithParameters called with param1: {param1}, param2: {param2}");
            }
        }

        public class Class2 : Class1
        {
            public double Property2 { get; set; }
            public bool Field2;

            public void Method2()
            {
                Console.WriteLine("Method2 called");
            }

            public void MethodWithMoreParameters(double param1, bool param2)
            {
                Console.WriteLine($"MethodWithMoreParameters called with param1: {param1}, param2: {param2}");
            }
        }

        public interface IExampleInterface
        {
            void Method1();
        }
    }

