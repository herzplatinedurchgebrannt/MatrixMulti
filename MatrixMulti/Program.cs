using System;


//using System.Collections.Generic;
//using System.Linq;

namespace MatrixMulti 
{
    public static class Matrix
    {
        static Matrix()
        {

        }

        public enum RotateAxis
        {
            X,
            Y,
            Z
        }

        public static double[,] multiplyMatrix(double[,] A, double[,] B)
        {
            int ARows = A.GetLength(0);
            int AColumns = A.Length / A.GetLength(0);

            int BRows = B.GetLength(0);
            int BColumns = B.Length / B.GetLength(0);

            double[,] result = new double[ARows, BColumns];

            double sum = 0;

            for (int i = 0; i < ARows; i++)
            {
                for (int j = 0; j < BColumns; j++)
                {
                    for (int k = 0; k < BRows; k++)
                    {
                        sum += A[i, k] * B[k, j];
                    }
                    result[i, j] = sum;
                    sum = 0;
                }
            }
            return result;
        }

        public static double[,] getInverseMatrix(double[,] A)
        {
            int ARows = A.GetLength(0);
            int AColumns = A.Length / A.GetLength(0);

            double[,] result = new double[ARows, AColumns];

            // quick + dirty
            double nSkalar = A[0, 0] * A[0, 3] + A[1, 0] * A[1, 3] + A[2, 0] * A[2, 3];
            double oSkalar = A[0, 1] * A[0, 3] + A[1, 1] * A[1, 3] + A[2, 1] * A[2, 3];
            double aSkalar = A[0, 2] * A[0, 3] + A[1, 2] * A[1, 3] + A[2, 2] * A[2, 3];

            for (int r = 0; r < ARows-1; r++)
            {
                for (int c = 0; c < AColumns-1; c++)
                {
                    result[c, r] = A[r, c];
                }
                Console.WriteLine();
            }

            result[0, 3] = -nSkalar;
            result[1, 3] = -oSkalar;
            result[2, 3] = -aSkalar;
            result[3, 3] = 1;

            return result;
        }


        public static double[,] translationPoint (double[,] p , double x = 0, double y = 0, double z = 0)
        {
            int pRows = p.GetLength(0);
            int pColumns = p.Length / p.GetLength(0);

            double[,] translationMatrix = { { 1, 0, 0, x },
                                            { 0, 1, 0, y },
                                            { 0, 0, 1, z },
                                            { 0, 0, 0, 1 } };

            double[,] result = multiplyMatrix(translationMatrix, p);

            return result;
        }

        public static double[,] rotationPoint(double[,] p, RotateAxis axis, double angle)
        {
            int pRows = p.GetLength(0);
            int pColumns = p.Length / p.GetLength(0);

            double cosO = Math.Cos((angle * Math.PI) / 180);
            double sinO = Math.Sin((angle * Math.PI) / 180);

            double[,] rotationMatrix = new double[4, 4];

            double[,] rotationMatrixX =   { {  1,     0,     0,     0 },
                                            {  0,     cosO, -sinO,  0 },
                                            {  0,     sinO,  cosO,  0 },
                                            {  0,     0,     0,     1 } };

            double[,] rotationMatrixY =   { {  cosO,  0,     sinO,  0 },
                                            {  0,     1,     0,     0 },
                                            { -sinO,  0,     cosO,  0 },
                                            {  0,     0,     0,     1 } };

            double[,] rotationMatrixZ =   { {  cosO, -sinO,  0,     0 },
                                            {  sinO,  cosO,  0,     0 },
                                            {  0,     0,     1,     0 },
                                            {  0,     0,     0,     1 } };

            switch (axis)
            {
                case RotateAxis.X:
                    rotationMatrix = rotationMatrixX;
                    break;
                case RotateAxis.Y:
                    rotationMatrix = rotationMatrixY;
                    break;
                case RotateAxis.Z:
                    rotationMatrix = rotationMatrixZ;
                    break;

                default:
                    
                    break;
            }

            double[,] result = multiplyMatrix(rotationMatrix, p);

            return result;
        }






        public static void print(double[,] a)
        {
            int arows = a.GetLength(0);
            int acolumns = a.Length / a.GetLength(0);

            foreach (double d in a)
            {
                //Console.Write(d + " ");
            }

            for (int r = 0; r < arows; r++)
            {
                for (int c = 0; c < acolumns; c++)
                {
                    Console.Write( a[r, c] + " " );
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

    }


    // [3,4] = 3 Reihen, 4 Spalten

    // Vektor = 1 Spalte, 4 Reihen = [4,1]
    // VektorTransponiert [1,4]

    public class Program
    {
        public static void Main(string[] args)
        {
            //Console.Write(Math.Cos(Math.PI / 180 * 90));



            double[,] A = { { 1, 2, 3 ,3 }, 
                            { 0, 5, 6, 2 }, 
                            { 1, 2, 0, 2 } , 
                            { 1, 4, 0, 2 } };

            double[,] B = { { 1, 1, 0, 1 }, 
                            { 0, 2, 6, 1 }, 
                            { 1, 3, 4, 1 }, 
                            { 0, 2, 1, 3 } };

            double[,] vektorTransposed = new double[1,4] {{ 1, 1, 0, 1 }};

            double[,] v = new double [4, 1] {{ 1 },
                                             { 1 },
                                             { 0 },
                                             { 1 }};

            double[,] c;

            // result AxB
            //4 20 27 15
            //6 32 56 17
            //1 9 14 9
            //1 13 26 11

            double[,] p = new double [4,1] {{ 1 },
                                            { 2 },
                                            { 3 },
                                            { 1 }};


            double[,] D = { { 1, 0, 0 ,4 },
                            { 0, 1, 0, 0 },
                            { 0, 0, 1, 0 } ,
                            { 0, 0, 0, 1 } };

            //c = Matrix.translationPoint(p, 20, 25, 2);

            //c = Matrix.multiplyMatrix(A, B);

            c = Matrix.getInverseMatrix(D);

            Matrix.print(c);


        }

    }
}