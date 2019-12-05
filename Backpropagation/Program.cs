using System;

namespace ConsoleApp1

{

	class Program

	{

		// Initializare 

		static double[] θ = new double[5];

		static double[,] w = new double[4, 5];

		static int[,] instructionExample = new int[3, 4];

		static double Eadmis = 0.001;

		static double[] Os = new double[4];

		static double Os5;

		static double[] Oc = new double[5];

		static double e = Math.E;

		static double e5;

		static double[] δ = new double[5];

		static double[,] Δw = new double[4, 5];

		static double α = 0.1;

		static double[] omega = new double[5];

		static double[] Δθ = new double[5];

		static int exemplu = 3;

		static double[,] x = new double[4, 5];

		static int nrInstruire = 1;

		static string myCharLines = "--------------Instruire: ";

		static string myError = "----Eroare: ";

		static double[] error = new double[5];



		static void Main(string[] args)

		{

			defineValues();



			Activare();



			Instruire();



			while (findIfErrorsBiggerTnanEadmis())
			{

				nrInstruire = nrInstruire + 1;

                if (exemplu == 3)
                {
                    exemplu = 0;
                }
                else
                {
                    exemplu++;
                }

				Activare();

			}



			Console.ReadKey();

		}



		static void defineValues()
		{

			θ[2] = 0.8;

			θ[3] = -0.1;

			θ[4] = 0.3;

			instructionExample[0, 0] = 0;

			instructionExample[0, 1] = 1;

			instructionExample[0, 2] = 0;

			instructionExample[0, 3] = 1;

			instructionExample[1, 0] = 0;

			instructionExample[1, 1] = 0;

			instructionExample[1, 2] = 1;

			instructionExample[1, 3] = 1;

			Os[0] = 0;

			Os[1] = 1;

			Os[2] = 1;

			Os[3] = 0;

			w[0, 2] = 0.5; // w13 

			w[1, 2] = 0.4; // w23 

			w[0, 3] = 0.9; // w14 

			w[1, 3] = 1.0; // w24 

			w[2, 4] = -1.2; // w35 

			w[3, 4] = 1.1; // w45 

			x[0, 2] = instructionExample[0, exemplu]; // x13 

			x[0, 3] = instructionExample[0, exemplu]; // x14 

			x[1, 2] = instructionExample[1, exemplu]; // x23 

			x[1, 3] = instructionExample[1, exemplu]; // x24 

			Os5 = Os[exemplu];

		}



		static void Activare()
		{

			// Activare 

			Oc[2] = 1 / (1 + Math.Pow(e, -(x[0, 2] * w[0, 2] + x[1, 2] * w[1, 2] - θ[2]))); // Oc,3 

			Oc[3] = 1 / (1 + Math.Pow(e, -(x[0, 3] * w[0, 3] + x[1, 3] * w[1, 3] - θ[3]))); // Oc,4 

			x[2, 4] = Oc[2]; // x35 

			x[3, 4] = Oc[3]; // x45 

			Oc[4] = 1 / (1 + Math.Pow(e, -(x[2, 4] * w[2, 4] + x[3, 4] * w[3, 4] - θ[4]))); // Oc,5 

			e5 = Os5 - Oc[4];

			Console.WriteLine("Oc,3 = " + Oc[2]);

			Console.WriteLine("Oc,4 = " + Oc[3]);

			Console.WriteLine("Oc,5 = " + Oc[4]);

			Console.WriteLine("Eroare neuron 5, e5 = " + e5);



			Instruire();

		}



		static bool findIfErrorsBiggerTnanEadmis()
		{

			double sum = 0;

			for (int i = 0; i < δ.Length; i++)
			{

				sum += δ[i] * δ[i];

			}

            Console.WriteLine(myError + sum);

            Console.WriteLine();

            if (sum >= Eadmis)
			{
				return true;

			}
			else
			{

				return false;

			}



		}



		static void Instruire()

		{

            Console.WriteLine();
			Console.WriteLine(myCharLines + nrInstruire);
            Console.WriteLine("Se utilizeaza exemplul " + (exemplu + 1));

            Console.WriteLine();

			// Instruirea ponderilor 

			δ[4] = Oc[4] * (1 - Oc[4]) * e5; // δ 5 

			Console.WriteLine();

			Console.WriteLine("δ5 = " + δ[4]);





			Console.WriteLine();

			Console.WriteLine("Determinam corectia");

			Δw[2, 4] = α * Oc[2] * δ[4]; // Δw35 

			Δw[3, 4] = α * Oc[3] * δ[4]; // Δw45 

			Δθ[4] = α * (-1) * δ[4]; // Δθ5 

			Console.WriteLine("Delta w35 = " + Δw[2, 4]);

			Console.WriteLine("Delta w45 = " + Δw[3, 4]);

			Console.WriteLine("Delta Teta  5 = " + Δθ[4]);







			Console.WriteLine();

			Console.WriteLine("Prin calculul gradientului erorii se determina:");

			δ[2] = Oc[2] * (1 - Oc[2]) * δ[4] * w[2, 4];

			δ[3] = Oc[3] * (1 - Oc[3]) * δ[4] * w[3, 4];

			Console.WriteLine("δ3 = " + δ[2]);

			Console.WriteLine("δ4 = " + δ[3]);





			Console.WriteLine();

			Console.WriteLine("Calculam apoi corectiile ponderilor si obtinem:");

			Δw[0, 2] = α * instructionExample[0, exemplu] * δ[2]; // Δw13 

			Δw[1, 2] = α * instructionExample[1, exemplu] * δ[2]; // Δw23 

			Δθ[2] = α * (-1) * δ[2]; // Δθ3 

			Δw[0, 3] = α * instructionExample[0, exemplu] * δ[3]; // Δw14 

			Δw[1, 3] = α * instructionExample[1, exemplu] * δ[3]; // Δw24 

			Δθ[3] = α * (-1) * δ[3]; // Δθ4 

			Console.WriteLine("Delta w13 = " + Δw[0, 2]);

			Console.WriteLine("Delta w23 = " + Δw[1, 2]);

			Console.WriteLine("Delta Teta 3 = " + Δθ[2]);

			Console.WriteLine("Delta w14 = " + Δw[0, 3]);

			Console.WriteLine("Delta w24 = " + Δw[1, 3]);

			Console.WriteLine("Delta Teta 4 = " + Δθ[3]);





			Console.WriteLine();

			Console.WriteLine("Ajustam toate ponderile si nivelurile pragurilor din retea (biasurile):");

			w[0, 2] += Δw[0, 2];

			w[0, 3] += Δw[0, 3];

			w[1, 2] += Δw[1, 2];

			w[1, 3] += Δw[1, 3];

			w[2, 4] += Δw[2, 4];

			w[3, 4] += Δw[3, 4];

			θ[2] = θ[2] + Δθ[2]; // θ 3 

			θ[3] = θ[3] + Δθ[3]; // θ 4 

			θ[4] = θ[4] + Δθ[4]; // θ 5 

			Console.WriteLine("w13 = " + w[0, 2]);

			Console.WriteLine("w14 = " + w[0, 3]);

			Console.WriteLine("w23 = " + w[1, 2]);

			Console.WriteLine("w24 = " + w[1, 3]);

			Console.WriteLine("w35 = " + w[2, 4]);

			Console.WriteLine("w45 = " + w[3, 4]);

			Console.WriteLine("Teta 2 = " + θ[2]);

			Console.WriteLine("Teta 3 = " + θ[3]);

			Console.WriteLine("Teta 4 = " + θ[4]);



		}

	}

}