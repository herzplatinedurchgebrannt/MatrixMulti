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

            if (AColumns != BRows)
            {
                Console.WriteLine("Columns of matrix A not equal to rows of matrix B!");
                Console.WriteLine("Multiplication not possible!");
                return result;
            }

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

        public static double calculateScalarProduct(double[,] vectorA, double[,] vectorB)
        {
            return 0;
        }

        public static bool checkSingularity(double[,] A)
        {
            int ARows = A.GetLength(0);
            int AColumns = A.Length / A.GetLength(0);

            double[,] sub3x3Matrix1 = new double[3, 3];
            double[,] sub3x3Matrix2 = new double[3, 3];
            double[,] sub3x3Matrix3 = new double[3, 3];
            double[,] sub3x3Matrix4 = new double[3, 3];

            double[,,] test = new double[4, 3, 3];

            double[,] S = new double[3, 3];

            for (int r = 1; r < ARows; r++)
            {
                int h = 0;
                int j = 0;
                int k = 0;
                int l = 0;
                for (int c = 0; c < AColumns; c++)
                {
                    if (c == 0) { }
                    else { sub3x3Matrix1[r - 1, h] = A[r, c]; test[0, r - 1, h] = A[r, c]; h += 1; }

                    if (c == 1) { }
                    else { sub3x3Matrix2[r - 1, j] = A[r, c]; test[1, r - 1, j] = A[r, c]; j += 1; }

                    if (c == 2) { }
                    else { sub3x3Matrix3[r - 1, k] = A[r, c]; test[2, r - 1, k] = A[r, c]; k += 1; }

                    if (c == 3) { }
                    else { sub3x3Matrix4[r - 1, l] = A[r, c]; test[3, r - 1, l] = A[r, c]; l += 1; }
                }
            }

            // https://matheguru.com/lineare-algebra/determinante.html

            double resultSubMatrix = S[0, 0] * S[1, 1] * S[2, 2]
                                     + S[0, 1] * S[1, 2] * S[2, 0]
                                     + S[0, 2] * S[1, 0] * S[2, 1]
                                     - S[0, 2] * S[1, 1] * S[2, 0]
                                     - S[0, 1] * S[1, 0] * S[2, 2]
                                     - S[0, 0] * S[1, 2] * S[2, 1];

            for (int i = 0; i < 4; i++)
            {

            }
            return true;
        }

        public static double[,] getInverseMatrix(double[,] A)
        {
            // PRÜFUNG OB SINGULARITÄT VORLIEGT
            // FUNKTION BERECHNUNG DETERMINANTE

            int ARows = A.GetLength(0);
            int AColumns = A.Length / A.GetLength(0);

            double[,] result = new double[ARows, AColumns];

            // build n,o,a,p vectors
            double[,] vectorN = new double[3, 1];
            double[,] vectorO = new double[3, 1];
            double[,] vectorA = new double[3, 1];
            double[,] vectorP = new double[3, 1];

            for (int r = 0; r < ARows - 1; r++)
            {
                vectorN[r, 0] = A[r, 0];
                vectorO[r, 0] = A[r, 1];
                vectorA[r, 0] = A[r, 2];
                vectorP[r, 0] = A[r, 3];
            }

            // calculate scalar product
            double nSkalar = 0;
            double oSkalar = 0;
            double aSkalar = 0;

            for (int r = 0; r < ARows - 1; r++)
            {
                nSkalar += vectorN[r, 0] * vectorP[r, 0];
                oSkalar += vectorO[r, 0] * vectorP[r, 0];
                aSkalar += vectorA[r, 0] * vectorP[r, 0];
            }

            for (int r = 0; r < ARows - 1; r++)
            {
                for (int c = 0; c < AColumns - 1; c++)
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


        public static double[,] getTranslationMatrix(double x = 0, double y = 0, double z = 0)
        {
            // Verschiebung vom Punkt im Raum
            double[,] translationMatrix = { { 1, 0, 0, x },
                                            { 0, 1, 0, y },
                                            { 0, 0, 1, z },
                                            { 0, 0, 0, 1 } };

            return translationMatrix;
        }

        public static double[,] getRotationMatrix(RotationAxis rotationaxis, double angle)
        {
            // Drehung vom Achse
            double cosO = Math.Round(Math.Cos(angle * Math.PI / 180), 9);
            double sinO = Math.Round(Math.Sin(angle * Math.PI / 180), 9);

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
            return rotationMatrix;
        }

        // FUNKTION ZUM SKALIEREN

        ///<summary>
        ///Rotates matrix intrinsic 
        ///</summary>
        public static double[,] rotateMatrixIntrinsic(double A, double B, double C, RotationOrder rotationorder = RotationOrder.ZYX)
        {
            // A Rotation um Z
            // B Rotation um Y'
            // C Rotation um X''

            double[,] result = new double[4, 4];

            double[,] RotationA = Matrix.getRotationMatrix(RotationAxis.Z, A);
            double[,] RotationB = Matrix.getRotationMatrix(RotationAxis.Y, B);
            double[,] RotationC = Matrix.getRotationMatrix(RotationAxis.X, C);

            switch (rotationorder)
            {
                case RotationOrder.ZYZ:
                    // Z  Y' Z''
                    result = Matrix.multiplyMatrix(RotationA, RotationB);
                    result = Matrix.multiplyMatrix(result, RotationA);
                    break;
                case RotationOrder.ZYX:
                    // Z  Y' X''
                    result = Matrix.multiplyMatrix(RotationA, RotationB);
                    result = Matrix.multiplyMatrix(result, RotationC);
                    break;

                default:
                    break;
            }
            return result;
        }

        public static double[,] axisToAxis(double[,] P, double theta, double d, double a, double alpha)
        {
            double[,] result = new double[4, 4];

            double[,] rotTheta = Matrix.getRotationMatrix(RotationAxis.Z, theta);
            double[,] transD = Matrix.getTranslationMatrix(0, 0, d);
            double[,] transA = Matrix.getTranslationMatrix(a, 0, 0);
            double[,] rotAlpha = Matrix.getRotationMatrix(RotationAxis.X, alpha);

            result = Matrix.multiplyMatrix(P, rotTheta);
            result = Matrix.multiplyMatrix(result, transD);
            result = Matrix.multiplyMatrix(result, transA);
            result = Matrix.multiplyMatrix(result, rotAlpha);

            return result;

        }

        public static double[] getOrientationEuler(double[,] P)
        {
            double[] result = new double[3];

            double A = Math.Atan2(P[1, 0], P[0, 0]) * 180 / Math.PI;
            double B = Math.Atan2(-P[2, 0], Math.Sqrt(Math.Pow(P[0, 0], 2) + Math.Pow(P[1, 0], 2))) * 180 / Math.PI;
            double C = Math.Atan2(P[2, 1], P[2, 2]) * 180 / Math.PI;

            if (B == 90)
            {
                A = 0;
                C = Math.Atan2(P[0, 1], P[1, 1]) * 180 / Math.PI;
            }
            else if (B == -90)
            {
                A = 0;
                C = -Math.Atan2(P[0, 1], P[1, 1]) * 180 / Math.PI;
            }


            result[0] = A;
            result[1] = B;
            result[2] = C;
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
                    Console.Write(A[r, c] + " ");
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

            Matrix.checkSingularity(A);

            double[,] B = { { 1, 1, 0, 1 },
                            { 0, 2, 6, 1 },
                            { 1, 3, 4, 1 },
                            { 0, 2, 1, 3 } };

            double[,] E = { { 1, 2, 3 ,3 },
                            { 0, 5, 6, 2 },
                            { 1, 2, 0, 2 }};

            double[,] F = { { 1, 1, 0 },
                            { 0, 2, 6 },
                            { 1, 3, 4 },
                            { 0, 2, 1 } };

            double[,] vektorTransposed = new double[1, 4] { { 1, 1, 0, 1 } };

            double[,] v = new double[4, 1] {{ 1 },
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

            double[,] p = new double[4, 1] {{ 130 },
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

            //Matrix.print(c);

            double[,] g = Matrix.rotateMatrixIntrinsic(65, -40, 30, Matrix.RotationOrder.ZYX);

            //Matrix.print(g);

            g = Matrix.rotateMatrixIntrinsic(65, -40, 30, Matrix.RotationOrder.ZYZ);

            //Matrix.print(g);

            g = Matrix.multiplyMatrix(E, F);


        
            // start position/ orientation
            double[,] calc3 = { { 1, 0, 0 ,0 },
                                { 0, 1, 0, 0 },
                                { 0, 0, 1, 0 },
                                { 0, 0, 0, 1 } };


            // robot mada
            // PUMA 560 Vorlesung
            //double[] theta = { 45, 45, 45, 45, 45, 45 };
            //double[] d = { 671.83, 139.70, 0, 431.80, 0, 56.50 };
            //double[] r = { 0, 431.80, -20.32, 0, 0, 0 };
            //double[] alpha = { -90, 0, 90, -90, 90, 0 };

            // PUMA RoboAnalyzer -> DH Parameter / Umrechnung passt, 3d Modell liefert andere Werte?!
            //double[] theta = { 0, 0, 0, 0, 0, 0 };
            //double[] d = { 774.21, 101.592, -38.1, 267.969, 0, 58.42 };
            //double[] r = { 0, 506.628, 20, 0, 0, 0 };
            //double[] alpha = { -90, 180, -90, 90, -90, 0 };


            //
            // KR 210
            // double[] d = { 675, 0,    0,    1200,   0,  215 };
            // double[] r = { 0,   350,  1150, -41,    0,  0, };
            // double[] alpha = { 0, -90, 0,  -90,    90, -90 };

            // DH PARAMETER KR 60 https://www.youtube.com/watch?v=qZB3_gKBwf8&list=WL&index=15
            //double[] d = { 815, 0, 0, 820, 0, 170 };
            //double[] r = { 350, 850, 145, 0, 0, 0, };
            //double[] alpha = { -90, 0, -90, -90, 90, 0 };


            // KR5 http://www.diag.uniroma1.it/~deluca/rob1_en/Robotics1_Homework1_10-11.pdf
            //double[] theta = { 90, -90, 0, 0, 0, 0 };
            //double[] d = { 335, 0, 0, -295, 0, -80 };
            //double[] r = { 75, 270, 90, 0, 0, 0, };
            //double[] alpha = { -90, 0, 90, -90, 90, 180 };


            // RoboAnalyzer KR5 ARC // Berechnung DH Parameter passt, 3d Modell Position passt nicht ?!
            //double[] theta = { 0, 90, 30, 10, 50, 0 };
            //double[] d = { 400, 135, 135, 620, 0, 115 };
            //double[] r = { 180, 600, 120, 0, 0, 0, };
            //double[] alpha = { 90, 180, -90, 90, -90, 0 }; // sollte passen
            ////double[] alpha = { 90, 0, 90, 90, 90, 0 };
            ////double[] alpha = { 90, -180, -270, 90, 90, 0 };

            // KR 210 IC
            //double[] theta = { 74.24, -64.02, 106.72, 72.83, -79.26, 132.54 }; // 677.35, -1658.05, 883.45 // -155.77°, -84.36°, -27.93°  110 010010 bin
            //double[] d = { -675, 0, 0, -1200, 0, -215 };
            //double[] r = { 350, 1150, -41, 0, 0, 0, };
            // double[] alpha = { 90, 0, 90, 90, -90, 0 }; KUKA QUELLE
            //double[] alpha = { 90, 0, 90, 90, -90, 0 };


            // KR 210 http://www.oemg.ac.at/Mathe-Brief/fba2015/VWA_Prutsch.pdf
            // double[] theta = { -66.146, -18.726, -90 + 53.908, 178.344, -110.097, 180-117.526 };
            // double[] theta = { 66.146 * (-1), -18.726, -90 + 53.908, -178.344 * (-1), -110.097, 180 - 117.526 }; IO!

            double[] pos_1 = { 90.54, -79.94, 110.80, 86.36, -87.36, 120.96 }; // SOLL: 561.24, -1667.26, 785.58, -171.77°, -86.55°, -25.38° 110 010010
            double[] theta = { pos_1[0] * (-1), pos_1[1], -90 + pos_1[2], pos_1[3] * (-1), pos_1[4], 180 - pos_1[5] }; 

            // double[] d = { 675, 0, 0, 1200, 0, 190 }; // Script
            double[] d = { 675, 0, 0, 1200, 0, 215 }; // InnoCenter
            double[] r = { 350, 1150, -41, 0, 0, 0 };
            double[] alpha = { -90, 0, -90, 90, -90, 0 };




            // current joint angles
            //double[] theta = { 45, 45, 45, 45, 45, 45 };
            //double[] theta = { 45, 45, 45, 45, 45, 45 };
            //double[] theta = { 3.7, -91.2,56.8, 0, 0, 0 };

            // forward kinematics
            calc3 = Matrix.axisToAxis(calc3, theta[0], d[0], r[0], alpha[0]);
            //Matrix.print(calc3);
            calc3 = Matrix.axisToAxis(calc3, theta[1], d[1], r[1], alpha[1]);
            //Matrix.print(calc3);
            calc3 = Matrix.axisToAxis(calc3, theta[2], d[2], r[2], alpha[2]);
            //Matrix.print(calc3);
            calc3 = Matrix.axisToAxis(calc3, theta[3], d[3], r[3], alpha[3]);
            //Matrix.print(calc3);
            calc3 = Matrix.axisToAxis(calc3, theta[4], d[4], r[4], alpha[4]);
            //Matrix.print(calc3);
            calc3 = Matrix.axisToAxis(calc3, theta[5], d[5], r[5], alpha[5]);

            Console.WriteLine("Forward kinematics: (Tool 0, Base 0)");
            Matrix.print(calc3);

            // TCP: -494.921, 0.878, 238.185, 0, -90, 0

            calc3 = Matrix.multiplyMatrix(calc3, Matrix.getTranslationMatrix(-494.921, 0.878, 238.185));
            calc3 = Matrix.multiplyMatrix(calc3, Matrix.getRotationMatrix(Matrix.RotationAxis.Y, -90));

            Console.WriteLine();
            Console.WriteLine("Forward kinematics: (TCP, Base 0)");
            Matrix.print(calc3);

            double[] orientation = new double[3];
            orientation = Matrix.getOrientationEuler(calc3);

            /*
            double Orientation_A = Math.Atan2(calc3[1, 0], calc3[0, 0]) * 180 / Math.PI;
            double Orientation_B = Math.Atan2(-calc3[2, 0], Math.Sqrt(Math.Pow(calc3[0, 0], 2) + Math.Pow(calc3[1, 0], 2))) * 180 / Math.PI;
            double Orientation_C = Math.Atan2(calc3[2, 1], calc3[2, 2]) * 180 / Math.PI;

            if (Orientation_B == 90)
            {
                Orientation_A = 0;
                Orientation_C = Math.Atan2(calc3[0, 1], calc3[1, 1]) * 180 / Math.PI;
            }
            else if (Orientation_B == -90)
            {
                Orientation_A = 0;
                Orientation_C = - Math.Atan2(calc3[0, 1], calc3[1, 1]) * 180 / Math.PI;
            }*/


            Console.WriteLine("Position:");
            Console.WriteLine("X: " + calc3[0, 3]);
            Console.WriteLine("Y: " + calc3[1, 3]);
            Console.WriteLine("Z: " + calc3[2, 3]);
            Console.WriteLine("A: " + orientation[0]);
            Console.WriteLine("B: " + orientation[1]);
            Console.WriteLine("C: " + orientation[2]);

            Console.ReadLine();












            // INVERSE KINEMATICS PUMA560
            double[] joints = new double[6];

            double[] P5 = { calc3[0, 3] - d[5] * calc3[0, 2], calc3[1, 3] - d[5] * calc3[1, 2], calc3[2, 3] - d[5] * calc3[2, 2] };

            double C1 = Math.Sqrt(Math.Pow(P5[0], 2) + Math.Pow(P5[1], 2));
            double D1 = d[1] / C1;

            joints[0] = Math.Round((Math.Atan2(P5[1], P5[0]) - Math.Atan2(D1, Math.Sqrt(1 - Math.Pow(D1, 2)))) * 180 / Math.PI);

            double C2 = P5[2] - d[0];
            double C3 = Math.Sqrt(Math.Pow(C1, 2) + Math.Pow(C2, 2));
            double C4 = Math.Sqrt(Math.Pow(r[2], 2) + Math.Pow(d[3], 2));

            double D2 = (Math.Pow(C3, 2) + Math.Pow(r[1], 2) - Math.Pow(C4, 2)) / (2 * r[1] * C3);
            double D3 = (Math.Pow(C4, 2) + Math.Pow(r[1], 2) - Math.Pow(C3, 2)) / (2 * r[1] * C4);

            double a_1 = Math.Atan2(D1, Math.Sqrt(Math.Abs(1 - Math.Pow(D1, 2))));
            double a_2 = Math.Atan2(Math.Sqrt(Math.Abs(1 - Math.Pow(D2, 2))), D2);

            double b = Math.Atan2(Math.Sqrt(Math.Abs(1 - Math.Pow(D3, 2))), D3);

            double p_1 = Math.Atan2(P5[1], P5[0]);
            double p_2 = Math.Atan2(C2, C1);

            joints[1] = Math.Round((a_2 - p_2) * 180 / Math.PI);
            joints[2] = Math.Round((Math.Atan2(Math.Sqrt(1 - Math.Pow(D3, 2)), D3) * 180 / Math.PI) - 90);



            double[,] T0_3 = { { 1, 0, 0 ,0 },
                               { 0, 1, 0, 0 },
                               { 0, 0, 1, 0 },
                               { 0, 0, 0, 1 } };

            T0_3 = Matrix.axisToAxis(T0_3, joints[0], d[0], r[0], alpha[0]);
            T0_3 = Matrix.axisToAxis(T0_3, joints[1], d[1], r[1], alpha[1]);
            T0_3 = Matrix.axisToAxis(T0_3, joints[2], d[2], r[2], alpha[2]);

            //Matrix.print(T0_3);
            //Console.WriteLine();

            double[,] T0_3_inv = Matrix.getInverseMatrix(T0_3);

            //Matrix.print(T0_3_inv);
            //Console.WriteLine();


            double[,] T3_6 = Matrix.multiplyMatrix(T0_3_inv, calc3);

            //Matrix.print(T3_6);
            //Console.WriteLine();

            joints[3] = Math.Round((Math.Atan2(T3_6[1, 2], T3_6[0, 2])) * 180 / Math.PI);
            joints[4] = Math.Round((Math.Atan2(Math.Sqrt(Math.Abs(1 - Math.Pow(T3_6[2, 2], 2))), T3_6[2, 2])) * 180 / Math.PI);
            joints[5] = Math.Round((Math.Atan2(T3_6[2, 1], -T3_6[2, 0])) * 180 / Math.PI);

            Console.WriteLine("Calculated joints: ");

            foreach (double j in joints)
            {
                //Console.WriteLine(j);
            }

            //Console.ReadLine();



        }

    }
}
