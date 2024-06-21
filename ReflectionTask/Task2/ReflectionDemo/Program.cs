using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ReflectionDemo
{
   
        class Program
        {
            static void Main(string[] args)
            {
                // Hard-code the path to the DLL file
                string assemblyPath = @"C:\wipro\RetrivingData\bin\Debug\RetrivingData.dll"; // Change this path to the actual path of your DLL

                try
                {
                    // Load the assembly
                    Assembly assembly = Assembly.LoadFrom(assemblyPath);

                    // Get the specific type
                    Type type = assembly.GetType("RetrivingData.Class2");
                    if (type == null)
                    {
                        Console.WriteLine("Type not found in the assembly.");
                        return;
                    }

                    // Print the type's name
                    Console.WriteLine($"Type: {type.FullName}");

                    // Create an instance of the type
                    object instance = Activator.CreateInstance(type);

                    // Set some values for the instance (for demonstration)
                    type.GetProperty("Property1")?.SetValue(instance, "Hello");
                    type.GetProperty("Property2")?.SetValue(instance, 3.14);
                    type.GetField("Field1")?.SetValue(instance, 42);
                    type.GetField("Field2")?.SetValue(instance, true);

                    // Print all public properties
                    Console.WriteLine("Properties:");
                    foreach (var prop in type.GetProperties())
                    {
                        Console.WriteLine($"  {prop.Name} ({prop.PropertyType}): {prop.GetValue(instance)}");
                    }

                    // Print all public fields
                    Console.WriteLine("Fields:");
                    foreach (var field in type.GetFields())
                    {
                        Console.WriteLine($"  {field.Name} ({field.FieldType}): {field.GetValue(instance)}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }


