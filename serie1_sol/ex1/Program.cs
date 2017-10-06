using System;

namespace ex1
{
    public class Program
    {
        public struct Point3D
        {
            /// <summary>
            /// Constructeur d'un point 3D
            /// </summary>
            /// <param name="x">coordonée x</param>
            /// <param name="y">coordonnée y</param>
            /// <param name="z">coordonnée z</param>
            public Point3D(double x, double y, double z)
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
            }

            //Propriété X, Y et Z
            public double X { get; set; }

            public double Y { get; set; }

            public double Z { get; set; }

            /// <summary>
            /// Calcule la distance d'un point dans l'espace à l'origine
            /// </summary>
            /// <returns>une distance du point à l'origine</returns>
            public double DistanceToOrigin()
            {
                return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
            }
       
            public override string ToString()
            {
                return "[x: " + X + ", y: " + Y + ", z: " + Z + "]";
            }
        }

        /// <summary>
        /// Echange les coordonnées x, y et z de deux points
        /// </summary>
        /// <param name="point3D1">point 3D</param>
        /// <param name="point3D2">point 3D</param>
        public void SwapPoints(ref Point3D point3D1, ref Point3D point3D2)
        {
            double tmp = point3D1.X;
            point3D1.X = point3D2.X;
            point3D2.X = tmp;
            tmp = point3D1.Y;
            point3D1.Y = point3D2.Y;
            point3D2.Y = tmp;
            tmp = point3D1.Z;
            point3D1.Z = point3D2.Z;
            point3D2.Z = tmp;
        }

        static void Main(string[] args)
        {
            Point3D point3D1 = new Point3D(10, -5, 2);
            Point3D point3D2 = new Point3D(-4, 15, -9);
            Console.WriteLine("point 1 : "+point3D1);
            Console.WriteLine("distance : " + point3D1.DistanceToOrigin());
            Console.WriteLine("point 2 : " + point3D2);
            Console.WriteLine("distance : " + point3D2.DistanceToOrigin()+"\n");
            Console.WriteLine("SWAP");
            new Program().SwapPoints(ref point3D1, ref point3D2);
            Console.WriteLine("point 1 : " + point3D1);
            Console.WriteLine("distance : " + point3D1.DistanceToOrigin());
            Console.WriteLine("point 2 : " + point3D2);
            Console.WriteLine("distance : " + point3D2.DistanceToOrigin());
            Console.ReadLine();
        }
    }
}
