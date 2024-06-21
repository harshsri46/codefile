using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Reflection;


namespace ReflectionExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Hard-code the path to the DLL file
            string assemblyPath = @"C:\wipro\MyClassLibrary\bin\Debug\MyClassLibrary.dll"; // Change this path to the actual path of your DLL

            try
            {
                // Load the assembly
                Assembly assembly = Assembly.LoadFrom(assemblyPath);

                // Get all types in the assembly
                Type[] types = assembly.GetTypes();

                foreach (var type in types)
                {
                    // Print the type's name
                    Console.WriteLine($"Type: {type.FullName}");

                    // Print the base type
                    Console.WriteLine($"  Base Type: {type.BaseType?.FullName}");

                    // Print the interfaces implemented by the type
                    Type[] interfaces = type.GetInterfaces();
                    if (interfaces.Length > 0)
                    {
                        Console.WriteLine("  Interfaces:");
                        foreach (var iface in interfaces)
                        {
                            Console.WriteLine($"    {iface.FullName}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("  Interfaces: None");
                    }

                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
