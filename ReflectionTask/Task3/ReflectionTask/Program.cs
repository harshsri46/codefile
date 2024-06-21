using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ReflectionTask
{
   
        class Program
        {
            static void Main(string[] args)
            {
                // Hard-code the path to the DLL file
                string assemblyPath = @"C:\wipro\InvokeDynamically\bin\Debug\InvokeDynamically.dll"; // Change this path to the actual path of your DLL

                try
                {
                    // Load the assembly
                    Assembly assembly = Assembly.LoadFrom(assemblyPath);

                    // Get the specific type
                    Type type = assembly.GetType("InvokeDynamically.Class2");
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

                    // Ask the user for the method name
                    Console.WriteLine("Enter the method name to invoke:");
                    string methodName = Console.ReadLine();

                    // Get the method
                    MethodInfo method = type.GetMethod(methodName);
                    if (method == null)
                    {
                        Console.WriteLine("Method not found.");
                        return;
                    }

                    // Get the method parameters
                    ParameterInfo[] parameters = method.GetParameters();
                    object[] parameterValues = new object[parameters.Length];

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        Console.WriteLine($"Enter value for parameter '{parameters[i].Name}' of type '{parameters[i].ParameterType}':");
                        string input = Console.ReadLine();
                        parameterValues[i] = Convert.ChangeType(input, parameters[i].ParameterType);
                    }

                    // Invoke the method
                    try
                    {
                        object result = method.Invoke(instance, parameterValues);
                        if (method.ReturnType != typeof(void))
                        {
                            Console.WriteLine($"Method returned: {result}");
                        }
                    }
                    catch (TargetInvocationException ex)
                    {
                        Console.WriteLine($"Error: {ex.InnerException?.Message}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }


