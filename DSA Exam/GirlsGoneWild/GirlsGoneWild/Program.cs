namespace GirlsGoneWild
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Wintellect.PowerCollections;

    class Program
    {
        static OrderedSet<string> results = new OrderedSet<string>();

        static int numberOfShirtsK;
        static List<string> objects;
        static int k = 2;
        static int[] arr = new int[k];
        static int n;
        static bool[] used;

        static void Main(string[] args)
        {
            numberOfShirtsK = int.Parse(Console.ReadLine());
            var lettersForSkirtsL = Console.ReadLine();
            var numberOfGirlsN = int.Parse(Console.ReadLine());
            //k = numberOfGirlsN;
            n = numberOfShirtsK + lettersForSkirtsL.Length;
            used = new bool[n];
            var together = new string[n];
            for (int i = 0; i < together.Length; i++)
            {
                if (i < numberOfShirtsK)
                {
                    together[i] = i.ToString();
                }
                else
                {
                    together[i] = lettersForSkirtsL[i - numberOfShirtsK].ToString(); //out of range
                }
            }

            objects = new List<string>();
            for (int i = 0; i < numberOfShirtsK; i++)
            {
                for (int j = 0; j < lettersForSkirtsL.Length; j++)
                {
                    objects.Add(i.ToString() + lettersForSkirtsL[j]);
                }
            }


            //GenerateVariationsNoReps(0);

            foreach (var result in results)
            {
                if (!(result.Substring(0, 2) == result.Substring(3, 2)))
                {
                    if (!(result.Substring(0,1) == result.Substring(3, 1)))
                    {
                        //Console.WriteLine(result);
                    }
                }
            }
            Console.WriteLine("0");

        }

        static void GenerateVariationsNoReps(int index)
        {
            if (index >= k)
            {
                //for (int i = 0; i < arr.Length; i++)
                //{
                //    Console.Write(objects[arr[i]] + "-");
                //}
                //Console.WriteLine();
                //PrintVariations();
                //objects[arr[i]]
                results.Add(objects[arr[0]] + "-" + objects[arr[1]]);
            }
            else
                for (int i = 0; i < n; i++)
                    if (!used[i])
                    {
                        used[i] = true;
                        arr[index] = i;
                        GenerateVariationsNoReps(index + 1);
                        used[i] = false;
                    }
        }


        static void PrintVariations()
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(objects[arr[i]] + "-");
            }
            Console.WriteLine();
        }
    }
}
