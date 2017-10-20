using System;

namespace ex1
{
    class Program
    {
        public static void Main(string[] args)
        {
            int num1, num2;
            Console.WriteLine("DIVISION. Entrez 2 nombres, je calcule le quotient");
            Console.Write("Entrez le 1er nombre: ");
            try
            {
                num1 = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException e)
            {
                num1 = 1;
                Console.WriteLine($"{e.Message}\n1er number = {num1}");
            }                   
            catch (OverflowException e)
            {
                num1 = 1;
                Console.WriteLine($"{e.Message}\n1er number = {num1}");
            }
            Console.Write("Entrez le 2ème nombre: ");
            try
            {
                num2 = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException e)
            {
                num2 = 1;
                Console.WriteLine($"{e.Message}\n2nd number = {num2}");
            }
            catch (OverflowException e)
            {
                num2 = 1;
                Console.WriteLine($"{e.Message}\n2nd number = {num2}");
            }
            try
            {
                decimal result = (decimal) num1 / (decimal) num2;
                Console.WriteLine($"Divide : {num1}/{num2}={result.ToString()}");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Division par zéro"); 
                
            }
            Console.ReadLine();
        }
    }
}
