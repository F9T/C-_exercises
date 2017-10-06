using System;

namespace ex2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Entrez un nombre positif : ");
            string input = Console.ReadLine();
            //Controle si le nombre est un de type double et positif
            if (double.TryParse(input, out var number) && number > 0)
            {
                double xj = number, xj1 = 0, error = number, limit = Math.Pow(number, -9);
                short countIteration = 0;
                //Date actuelle
                DateTime startTime = DateTime.Now;
                while (error > limit)
                {
                    //Calcul de l'approximation
                    xj1 = (xj + number / xj) / 2;
                    error = xj - xj1;
                    xj = xj1;
                    ++countIteration;
                    Console.WriteLine($"Approximation de la racine carré de {number} est {xj1:N5}");
                }
                //Calcul temps écoulé entre les deux dates
                TimeSpan elapsedTime = DateTime.Now.Subtract(startTime);
                double residualError = Math.Sqrt(number) - xj1;
                Console.WriteLine("Result :");
                Console.WriteLine($"Iteration number : {countIteration}");
                Console.WriteLine($"Elapsed time : {elapsedTime.TotalMilliseconds} ms");
                Console.WriteLine($"Error compare with sqrt : {residualError}");
            }
            else
            {
                Console.WriteLine("Number incorrect!");
            }
            Console.ReadLine();
        }
    }
}
