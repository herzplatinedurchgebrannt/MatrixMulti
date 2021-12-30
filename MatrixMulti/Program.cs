using System;


namespace MatrixMulti 
{
    public static class Matrix
    {
        static Matrix()
        {

        }

        public enum RotationAxis
        {
            X,
            Y,
            Z
        }

        public enum RotationOrder
        {
            ZYZ,
            ZYX
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
            // PRÜFUNG OB SINGULARITÄT VORLIEGT

            int ARows = A.GetLength(0);
            int AColumns = A.Length / A.GetLength(0);

            double[,] result = new double[ARows, AColumns];

            // SKALAR EIGENE FUNKTION 
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
            }

            result[0, 3] = -nSkalar;
            result[1, 3] = -oSkalar;
            result[2, 3] = -aSkalar;
            result[3, 3] = 1;

            return result;
        }


        public static double[,] getTranslationMatrix (double x = 0, double y = 0, double z = 0)
        {
            // Verschiebung vom Punkt im Raum

            double[,] translationMatrix = { { 1, 0, 0, x },
                                            { 0, 1, 0, y },
                                            { 0, 0, 1, z },
                                            { 0, 0, 0, 1 } };

            //double[,] result = multiplyMatrix(translationMatrix, p);

            return translationMatrix;
        }

        public static double[,] getRotationMatrix(RotationAxis rotationaxis, double angle)
        {
            // Drehung vom Achse

            double cosO =Math.Round(Math.Cos(angle * Math.PI / 180),9);
            double sinO =Math.Round(Math.Sin(angle * Math.PI / 180),9);

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

            switch (rotationaxis)
            {
                case RotationAxis.X:
                    rotationMatrix = rotationMatrixX;
                    break;
                case RotationAxis.Y:
                    rotationMatrix = rotationMatrixY;
                    break;
                case RotationAxis.Z:
                    rotationMatrix = rotationMatrixZ;
                    break;

                default:
                    
                    break;
            }

            //double[,] result = multiplyMatrix(rotationMatrix, p);

            return rotationMatrix;
        }

        // FUNKTION ZUM SKALIEREN

        ///<summary>
        ///Rotates matrix intrinsic 
        ///</summary>
        public static double[,] rotateMatrixIntrinsic (double A, double B, double C, RotationOrder rotationorder = RotationOrder.ZYX)
        {
            // A Rotation um Z
            // B Rotation um Y'
            // C Rotation um X''

            double[,] result = new double[4,4];

            double[,] RotationA = Matrix.getRotationMatrix(RotationAxis.Z, A);
            double[,] RotationB = Matrix.getRotationMatrix(RotationAxis.Y, B);
            double[,] RotationC = Matrix.getRotationMatrix(RotationAxis.X, C);

            switch (rotationorder)
            {
                case RotationOrder.ZYZ:
                    result = Matrix.multiplyMatrix(RotationA, RotationB);
                    result = Matrix.multiplyMatrix(result, RotationA);
                    break;
                case RotationOrder.ZYX:
                    result = Matrix.multiplyMatrix(RotationA, RotationB);
                    result = Matrix.multiplyMatrix(result, RotationC);
                    break;

                default:

                    break;
            }

            return result;
        }


        public static void print(double[,] A)
        {
            int arows = A.GetLength(0);
            int acolumns = A.Length / A.GetLength(0);

            for (int r = 0; r < arows; r++)
            {
                for (int c = 0; c < acolumns; c++)
                {
                    Console.Write( A[r, c] + " " );
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

            double[,] trans;
            double[,] rotat;

            double[,] complete;

            // result AxB
            //4 20 27 15
            //6 32 56 17
            //1 9 14 9
            //1 13 26 11

            double[,] p = new double [4,1] {{ 130 },
                                            { -55 },
                                            { 120 },
                                            { 1 }};


            double[,] D = { { 1, 0, 0 ,5 },
                            { 0, 1, 0, 7 },
                            { 0, 0, 1, -4 } ,
                            { 0, 0, 0, 1 } };


            // EXAMPLES
            trans = Matrix.getTranslationMatrix(150, 0, 125);
            rotat = Matrix.getRotationMatrix(Matrix.RotationAxis.Y, 135);

            complete = Matrix.multiplyMatrix(trans, rotat);

            complete = Matrix.getInverseMatrix(complete);

            c = Matrix.multiplyMatrix(complete, p);

            Matrix.print(c);

            Console.WriteLine("_----_");

            double[,] g = Matrix.rotateMatrixIntrinsic(65, -40, 30, Matrix.RotationOrder.ZYX);

            Matrix.print(g);

            g = Matrix.rotateMatrixIntrinsic(65, -40, 30, Matrix.RotationOrder.ZYZ);

            Matrix.print(g);

        }

    }
}