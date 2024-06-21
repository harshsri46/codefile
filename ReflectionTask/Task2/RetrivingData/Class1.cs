using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetrivingData
{
    
        public class Class1 : IExampleInterface
        {
            public string Property1 { get; set; }
            public int Field1;

            public void Method1() { }
        }

        public class Class2 : Class1
        {
            public double Property2 { get; set; }
            public bool Field2;

            public void Method2() { }
        }

        public interface IExampleInterface
        {
            void Method1();
        }
    

}
