using System;


namespace MatrixMulti
{
    public class Robot
    {
        public Robot(double[] DHParameter_Pos2Theta, double[] DHParameter_d, double[] DHParameter_a, double[] DHParameter_alpha, double[,] AxisLimits)
        {
            this.DHParameter_Pos2Theta = DHParameter_Pos2Theta;
            this.DHParameter_d = DHParameter_d;
            this.DHParameter_a = DHParameter_a;
            this.DHParameter_alpha = DHParameter_alpha;
            this.AxisLimits = AxisLimits;

            this.RobRoot = World;
            this.ToolAct = Tool0;
            this.BaseAct = RobRoot;
        }

        private string _RobotManufacturer;
        public string RobotManufacturer
        {
            get { return _RobotManufacturer; }
            set
            {
                _RobotManufacturer = value;
            }
        }

        private string _Model;
        public string Model
        {
            get { return _Model; }
            set
            {
                _Model = value;
            }
        }

        private double[] _DHParameter_Pos2Theta;
        public double[] DHParameter_Pos2Theta
        {
            get { return _DHParameter_Pos2Theta; }
            set
            {
                _DHParameter_Pos2Theta = value;
            }
        }

        private double[] _DHParameter_d;
        public double[] DHParameter_d
        {
            get { return _DHParameter_d; }
            set
            {
                _DHParameter_d = value;
            }
        }

        private double[] _DHParameter_a;
        public double[] DHParameter_a
        {
            get { return _DHParameter_a; }
            set
            {
                _DHParameter_a = value;
            }
        }

        private double[] _DHParameter_alpha;
        public double[] DHParameter_alpha
        {
            get { return _DHParameter_alpha; }
            set
            {
                _DHParameter_alpha = value;
            }
        }

        private double[,] _AxisLimits;
        public double[,] AxisLimits
        {
            get { return _AxisLimits; }
            set
            {
                _AxisLimits = value;
            }
        }

        public static readonly double[,] World =  { { 1, 0, 0 ,0 },
                                                    { 0, 1, 0, 0 },
                                                    { 0, 0, 1, 0 },
                                                    { 0, 0, 0, 1 } };

        public static readonly double[,] Tool0 =  { { 1, 0, 0 ,0 },
                                                    { 0, 1, 0, 0 },
                                                    { 0, 0, 1, 0 },
                                                    { 0, 0, 0, 1 } };

        private double[,] _RobRoot;
        public double[,] RobRoot
        {
            get { return _RobRoot; }
            set
            {
                _RobRoot = value;
            }
        }

        private double[,] _ToolAct;
        public double[,] ToolAct
        {
            get { return _ToolAct; }
            set
            {
                _ToolAct = value;
            }
        }

        private double[,] _BaseAct;
        public double[,] BaseAct
        {
            get { return _BaseAct; }
            set
            {
                _BaseAct = value;
            }
        }

        // INVERSE KINEMATICS
        // FORWARD KINEMATICS
    }

    public class PositionAbsolute
    {
        public PositionAbsolute()
        {

        }

        public PositionAbsolute(double J1, double J2, double J3, double J4, double J5, double J6)
        {

        }
    }

    public class PositionKartesian
    {
        public PositionKartesian()
        {

        }

        public PositionKartesian(double X, double Y, double Z, double A, double B, double C)
        {

        }

        public PositionKartesian(double X, double Y, double Z, double A, double B, double C, int S, int T)
        {

        }
    }

    public static class Matrix
    {
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
            double cosO = Math.Round(Math.Cos(angle * Math.PI / 180), 3);
            double sinO = Math.Round(Math.Sin(angle * Math.PI / 180), 3);

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
            // => ROBOT
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
            // => ROBOT
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
            double[,] RobRoot = { { 1, 0, 0 ,0 },
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

            //double[] pos_1 = { 90.54, -79.92, 110.80, 86.36, -87.36, 120.98 }; // SOLL: 561.24, -1667.26, 785.58, -171.77°, -86.55°, -25.38° 110 010010 // 110  010010




            double[] pos_1 = { 90.54, -79.92, 110.80, 86.36, -87.36, 120.98 }; // SOLL: 561.24, -1667.26, 785.58, -171.77°, -86.55°, -25.38° 110 010010 // 110  010010
            double[] theta = { pos_1[0] * (-1), pos_1[1], -90 + pos_1[2], pos_1[3] * (-1), pos_1[4], 180 - pos_1[5] };
            double[,] rangeAxis = { { -185, 185 }, { -140, -5 }, { -120, 155 }, { -350, 350 }, { -125, 125 }, { -350, 350 } };

            // double[] d = { 675, 0, 0, 1200, 0, 190 }; // Script
            double[] d = { 675, 0, 0, 1200, 0, 215 }; // InnoCenter
            double[] r = { 350, 1150, -41, 0, 0, 0 };
            double[] alpha = { -90, 0, -90, 90, -90, 0 };



            Robot Robert = new Robot(pos_1, d, r, alpha, rangeAxis );



            // Theta: (Gelenkwinkel) Rotation um z(n-1) Achse, damit x(n-1) parallel zu xn Achse liegt
            // d: Translation (Gelenkabstand) entlang z(n-1) Achse bis auf den Punkt wo sich z(n-1) und xn schneiden
            // a: Translation (Armelementlänge) entlang der xn Achse um die Ursprünge der Koordinatensysteme in Deckung zu bringen
            // Alpha: (Verwindung) um die xn Achse um die z(n-1) Achse in die zn Achse zu überführen


            double[,] T0_1;
            double[,] T0_2;
            double[,] T0_3;
            double[,] T0_4;
            double[,] T0_5;
            double[,] T0_6;

            // WCP
            double[,] T6_5;

            // current joint angles
            //double[] theta = { 45, 45, 45, 45, 45, 45 };
            //double[] theta = { 45, 45, 45, 45, 45, 45 };
            //double[] theta = { 3.7, -91.2,56.8, 0, 0, 0 };

            // forward kinematics

            // T0_1
            T0_1 = Matrix.axisToAxis(RobRoot, theta[0], d[0], r[0], alpha[0]);
            //Console.WriteLine("T0_1");
            //Matrix.print(T0_1);

            // T1_2
            T0_2 = Matrix.axisToAxis(T0_1, theta[1], d[1], r[1], alpha[1]);
            //Console.WriteLine("T1_2");
            //Matrix.print(T1_2);

            // T2_3
            T0_3 = Matrix.axisToAxis(T0_2, theta[2], d[2], r[2], alpha[2]);
            //Matrix.print(calc3);

            // T3_4
            T0_4 = Matrix.axisToAxis(T0_3, theta[3], d[3], r[3], alpha[3]);
            //Matrix.print(calc3);

            // T4_5
            T0_5 = Matrix.axisToAxis(T0_4, theta[4], d[4], r[4], alpha[4]);
            //Matrix.print(calc3);

            // T5_6
            T0_6 = Matrix.axisToAxis(T0_5, theta[5], d[5], r[5], alpha[5]);

            Console.WriteLine("Forward kinematics: (Tool 0, Base 0)");
            Matrix.print(T0_6);

            //T0_6 = T5_6;

            ////////////////////////////////////////////////////////////////////////////////////////////
            // TCP CALCULATION 
            // T6_7_EE
            // TCP: -494.921, 0.878, 238.185, 0, -90, 0

            //calc3 = Matrix.multiplyMatrix(calc3, Matrix.getTranslationMatrix(-494.921, 0.878, 238.185));
            //calc3 = Matrix.multiplyMatrix(calc3, Matrix.getRotationMatrix(Matrix.RotationAxis.Y, -90));

            //Console.WriteLine();
            //Console.WriteLine("Forward kinematics: (TCP, Base 0)");
            //Matrix.print(calc3);

            ////////////////////////////////////////////////////////////////////////////////////////////

            double[] orientation = new double[3];
            orientation = Matrix.getOrientationEuler(T0_6);

            Console.WriteLine("kartesian position:");
            Console.WriteLine("X: " + T0_6[0, 3]);
            Console.WriteLine("Y: " + T0_6[1, 3]);
            Console.WriteLine("Z: " + T0_6[2, 3]);
            Console.WriteLine("A: " + orientation[0]);
            Console.WriteLine("B: " + orientation[1]);
            Console.WriteLine("C: " + orientation[2]);
            Console.WriteLine();

            ////////////////////////////////////////////////////////////////////////

            // calculation back to wrist-center-point WCP

            T6_5 = Matrix.multiplyMatrix(T0_6, Matrix.getTranslationMatrix(0, 0, -215));

            Console.WriteLine("Wrist Center Point");
            Matrix.print(T6_5);

            // joint 1 ( * (-1))
            //Console.WriteLine(T6_5[0, 3]);
            //Console.WriteLine(T6_5[1, 3]);
            //Console.WriteLine(Math.Atan2(T6_5[1, 3], T6_5[0, 3]) * 180 / Math.PI);

            double krcJoint_1_1 = Math.Atan2(T6_5[1, 3], T6_5[0, 3]) * 180 / Math.PI;
            double krcJoint_1_2 = 0;

            if (krcJoint_1_1 <= 0)
            {
                krcJoint_1_2 = krcJoint_1_1 + 180;
            }
            else if (krcJoint_1_1 > 0)
            {
                krcJoint_1_2 = krcJoint_1_1 - 180;
            }
            // wenn x = y = 0 => Singularität bei Theta_1_l 

            // l1 = a2   l2 = d4

            // LINKS
            double[,] T0_1_ik_1 = Matrix.axisToAxis(RobRoot, krcJoint_1_1, d[0], r[0], alpha[0]);
            double[,] T1_0_ik_1 = Matrix.getInverseMatrix(T0_1_ik_1);
            double[,] T1_5_ik_1 = Matrix.multiplyMatrix(T1_0_ik_1, T0_5);

            double x = T1_5_ik_1[0, 3];
            double y = T1_5_ik_1[1, 3];

            // RECHTS
            double[,] T0_1_ik_2 = Matrix.axisToAxis(RobRoot, krcJoint_1_2, d[0], r[0], alpha[0]);
            double[,] T1_0_ik_2 = Matrix.getInverseMatrix(T0_1_ik_2);
            double[,] T1_5_ik_2 = Matrix.multiplyMatrix(T1_0_ik_2, T0_5);

            //double x = T1_5_ik_2[0, 3];
            //double y = T1_5_ik_2[1, 3];



            //Console.WriteLine("X & Y: ");
            //Console.WriteLine(x);
            //Console.WriteLine(y);

            /*
            double p_xy = 0.5 * (r[1] + d[3] + Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)));
            double r_xy = Math.Sqrt(((p_xy - r[1]) * (p_xy - d[3]) * (p_xy - Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2))) / (p_xy)));

            double test_j2_1 = (Math.Atan2(y, x) + (2 * Math.Atan2(r_xy, (p_xy - d[3])))) * 180 / Math.PI;     // -90 -> Ergebnis scheint bis auf Vorzeichen zu passen?!?
            double test_j2_2 = (Math.Atan2(y, x) - (2 * Math.Atan(r_xy / (p_xy - d[3])))) * 180 / Math.PI;

            double test_Alpha_1 = -2 * (Math.Atan(r_xy / (p_xy - r[1])));
            double test_Alpha_2 = 2 * (Math.Atan(r_xy / (p_xy - r[1])));

            test_Alpha_1 = test_Alpha_1 * 180 / Math.PI;
            test_Alpha_2 = test_Alpha_2 * 180 / Math.PI;

            double test_Beta = 2 * Math.Atan(r_xy / (p_xy - d[3]));

            test_Beta = test_Beta * 180 / Math.PI;

            double test_j3_1 = -2 * (Math.Atan(r_xy / (p_xy - r[1])) + Math.Atan(r_xy / (p_xy - d[3]))) * 180 / Math.PI - 90;
            double test_j3_2 = 2 * (Math.Atan(r_xy / (p_xy - r[1])) + Math.Atan(r_xy / (p_xy - d[3]))) * 180 / Math.PI - 90;
            */

            //double l1 = Math.Sqrt(Math.Pow(r[1],2) + Math.Pow(r[2],2));
            //double l2 = d[3];
            double l1 = r[1];
            double l2 = Math.Sqrt(Math.Pow(d[3], 2) - Math.Pow(r[2], 2));

            double winkel = 180 - 90 - Math.Atan(1200 / 41) * 180 / Math.PI;

            double p_xy = 0.5 * (l1 + l2 + Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)));
            double r_xy = Math.Sqrt((p_xy - l1) * (p_xy - l2) * (p_xy - Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2))) / (p_xy));

            double test_j2_1 = (Math.Atan2(y, x) + (2 * Math.Atan(r_xy / (p_xy - l2)))) * 180 / Math.PI ;     
            double test_j2_2 = (Math.Atan2(y, x) - (2 * Math.Atan(r_xy / (p_xy - l2)))) * 180 / Math.PI;

            double test_Alpha_1 = -2 * (Math.Atan(r_xy / (p_xy - r[1])));
            double test_Alpha_2 =  2 * (Math.Atan(r_xy / (p_xy - r[1])));

            test_Alpha_1 = test_Alpha_1 * 180 / Math.PI;
            test_Alpha_2 = test_Alpha_2 * 180 / Math.PI;

            double test_Beta = 2 * Math.Atan( r_xy / (p_xy - d[3]));

            test_Beta = test_Beta * 180 / Math.PI;

            double test_j3_1 = -2 * (Math.Atan(r_xy / (p_xy - r[1])) + Math.Atan(r_xy / (p_xy - d[3]))) * 180 / Math.PI - 90 - winkel;
            double test_j3_2 = 2 * (Math.Atan(r_xy / (p_xy - r[1])) + Math.Atan(r_xy / (p_xy - d[3]))) * 180 / Math.PI - 90 - winkel;


            double[,] T3_6 = Matrix.multiplyMatrix(Matrix.getInverseMatrix(T0_3), T0_6);

            double eulerBeta_1 = Math.Atan2(Math.Sqrt(Math.Pow(T3_6[2, 0], 2) + Math.Pow(T3_6[2, 1], 2)), T3_6[2, 2]);
            eulerBeta_1 = eulerBeta_1 * 180 / Math.PI;
            double eulerBeta_2 = Math.Atan2( - Math.Sqrt(Math.Pow(T3_6[2, 0], 2) + Math.Pow(T3_6[2, 1], 2)), T3_6[2, 2]);
            eulerBeta_2 = eulerBeta_2 * 180 / Math.PI;

            double eulerAlpha_1 = Math.Atan2(T3_6[1, 2] / eulerBeta_1, T3_6[0, 2] / eulerBeta_1);
            eulerAlpha_1 = eulerAlpha_1 * 180 / Math.PI;
            double eulerAlpha_2 = Math.Atan2(T3_6[1, 2] / eulerBeta_2, T3_6[0, 2] / eulerBeta_2);
            eulerAlpha_2 = eulerAlpha_2 * 180 / Math.PI;

            double eulerGamma_1 = Math.Atan2(T3_6[2, 1] / eulerBeta_1, -T3_6[2, 0] / eulerBeta_1);
            eulerGamma_1 = eulerGamma_1 * 180 / Math.PI;
            double eulerGamma_2 = Math.Atan2(T3_6[2, 1] / eulerBeta_2, -T3_6[2, 0] / eulerBeta_2);
            eulerGamma_2 = eulerGamma_2 * 180 / Math.PI;


            Console.WriteLine("given joint positions KRC:");
            foreach (double posJoint in pos_1)
            {
                Console.WriteLine(posJoint);
            }
            Console.WriteLine();

            Console.WriteLine("given joint DH Theta:");
            foreach (double posJoint in theta)
            {
                Console.WriteLine(posJoint);
            }
            Console.WriteLine();


            Console.WriteLine("krc joints:");
            Console.WriteLine("J1_1: " + Math.Round(krcJoint_1_1,2));
            Console.WriteLine("J1_2: " + Math.Round(krcJoint_1_2,2));
            Console.WriteLine();
            Console.WriteLine("J2_1: " + Math.Round(test_j2_1,2));
            Console.WriteLine("J2_2: " + Math.Round(test_j2_2,2));
            Console.WriteLine();
            Console.WriteLine("J3_1: " + Math.Round(test_j3_1,2));
            Console.WriteLine("J3_2: " + Math.Round(test_j3_2,2));
            Console.WriteLine();
            Console.WriteLine("J4_1: " + Math.Round(eulerAlpha_1,2));
            Console.WriteLine("J4_2: " + Math.Round(eulerAlpha_2,2));
            Console.WriteLine();
            Console.WriteLine("J5_1: " + Math.Round(eulerBeta_1,2));
            Console.WriteLine("J5_2: " + Math.Round(eulerBeta_2,2));
            Console.WriteLine();
            Console.WriteLine("J6_1: " + Math.Round(eulerGamma_1,2));
            Console.WriteLine("J6_2: " + Math.Round(eulerGamma_2,2));
            Console.WriteLine();
            Console.WriteLine();



            /////////////////////////////////////////////////////////////////////

            // INVERSE KINEMATICS PUMA560

            /*
            double[] joints = new double[6];

            double[] P5 = { RobRoot[0, 3] - d[5] * RobRoot[0, 2], RobRoot[1, 3] - d[5] * RobRoot[1, 2], RobRoot[2, 3] - d[5] * RobRoot[2, 2] };

            double C1 = Math.Sqrt(Math.Pow(P5[0], 2) + Math.Pow(P5[1], 2));
            double D1 = d[1] / C1;

            joints[0] = Math.Round((Math.Atan2(P5[1], P5[0]) - Math.Atan2(D1, Math.Sqrt(1 - Math.Pow(D1, 2)))) * 180 / Math.PI);
            joints[0] = Math.Atan2(P5[1], P5[0]) * 180 / Math.PI;




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


            double[,] T3_6 = Matrix.multiplyMatrix(T0_3_inv, RobRoot);

            //Matrix.print(T3_6);
            //Console.WriteLine();

            joints[3] = Math.Round((Math.Atan2(T3_6[1, 2], T3_6[0, 2])) * 180 / Math.PI);
            joints[4] = Math.Round((Math.Atan2(Math.Sqrt(Math.Abs(1 - Math.Pow(T3_6[2, 2], 2))), T3_6[2, 2])) * 180 / Math.PI);
            joints[5] = Math.Round((Math.Atan2(T3_6[2, 1], -T3_6[2, 0])) * 180 / Math.PI);

            //Console.WriteLine("Calculated joints: ");

            foreach (double j in joints)
            {
                //Console.WriteLine(j);
            }

            Console.ReadLine();

    */

            Console.ReadLine();

        }

    }
}
