using System;

namespace Objectifying_Resizable_Array
{
    class Program
    {
        static void Main(string[] args)
        {
            ResizableIntArray numbers = new ResizableIntArray();
            bool repeat = true;

            do
            {
                Console.WriteLine("Please enter an integer: ");
                string strInputNum = Console.ReadLine();
                if (strInputNum == "exit")
                {
                    repeat = false;
                }
                else
                {
                    try
                    {
                        int intInputNum = int.Parse(strInputNum);
                        Console.WriteLine("The integer that you entered was: " + intInputNum);
                        numbers.Add(intInputNum);
                        
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("That was not an integer!");
                    }
                }
            }
            while (repeat);

            numbers.BubbleSort();
            numbers.WriteContentsToConsole();
            Console.WriteLine("Average: " + numbers.Average());
            Console.WriteLine("Max: " + numbers.Max());
            Console.WriteLine("Min: " + numbers.Min());

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }
    }
}
