using System;

namespace ex4
{
    public class Program
    {
        public struct OddEvenArray
        {
            public short[] even, odd;
            public short countEven, countOdd;

            /// <summary>
            /// Constructeur de la structure prenant deux tableaux, un tableau des nombres pairs
            /// et un tableau des nombres impairs
            /// </summary>
            /// <param name="even">list des nombres pairs</param>
            /// <param name="odd">list des nombres impairs</param>
            /// <param name="countEven">nombre de nombres pairs trouvés</param>
            /// <param name="countOdd">nombre de nombres impairs trouvés</param>
            public OddEvenArray(short[] even, short[] odd, short countEven, short countOdd)
            {
                this.even = even;
                this.odd = odd;
                this.countEven = countEven;
                this.countOdd = countEven;
            }
        }

        /// <summary>
        /// Trie les nombre pairs et impairs du tableau passé en argument
        /// </summary>
        /// <param name="array">list de nombres entier aléatoire</param>
        /// <returns>structure OddEvenArray</returns>
        public static OddEvenArray EvenOdd(short[] array)
        {
            int length = array.Length;
            short countEven = 0, countOdd = 0;
            //On créé un tableau de la taille maximale possible
            short[] even = new short[length];
            short[] odd =  new short[length];
            foreach (short value in array)
            {
                //Si c'est pair
                if (value % 2 == 0)
                {
                    even[countEven++] = value;
                }
                else
                {
                    odd[countOdd++] = value;
                }
            }
            return new OddEvenArray(even, odd, countEven, countOdd);
        }

        /// <summary>
        /// Genére un tableau à une dimension de nombres aléatoires entre 0 et 99
        /// </summary>
        /// <param name="length">la taille du tableau à créé</param>
        /// <returns></returns>
        public static short[] GenerateRandomArray(short length)
        {
            if (length <= 0 && length >= short.MaxValue) return null;
            short[] array = new short[length];
            Random random = new Random();
            for (short i = 0; i < array.Length; i++)
            {
                array[i] = (short) random.Next(0, 99);
            }
            return array;
        }

        /// <summary>
        /// Affiche les N premiers éléments d'un tableau
        /// </summary>
        /// <param name="array">le tableau à afficher</param>
        /// <param name="N">les N premiers éléments à afficher</param>
        public static void PrintArray(short[] array, short N)
        {
            if (N < 0 && N > array.Length) return;
            Console.Write("[");
            for (short i = 0; i < N; i++)
            {
                Console.Write(array[i]);
                if (i + 1 < N)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine("]");
        }

        public static void Main(string[] args)
        {
            short[] array = GenerateRandomArray(20);
            if (array != null)
            {
                OddEvenArray oddEvenArrays = EvenOdd(array);
                PrintArray(oddEvenArrays.even, oddEvenArrays.countEven);
                PrintArray(oddEvenArrays.odd, oddEvenArrays.countOdd);
            }
            else
            {
                Console.WriteLine("Error");
            }
            Console.ReadLine();
        }
    }
}
