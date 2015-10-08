namespace Labyrinth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        static char[,] lab;
        static List<int> results = new List<int>();
        static HashSet<int> bashResults = new HashSet<int>();

        static void Main(string[] args)
        {

            var currentPosition = Console.ReadLine().Split(' ');
            var x = int.Parse(currentPosition[0]);
            var y = int.Parse(currentPosition[1]);
            var labSizes = Console.ReadLine().Split(' ');
            lab = new char[int.Parse(labSizes[0]), int.Parse(labSizes[1])];

            for (int i = 0; i < lab.GetLength(0); i++)
            {
                var currentLine = Console.ReadLine().Split(' ');
                var currentLineAsString = string.Join("", currentLine);
                for (int j = 0; j < lab.GetLength(1); j++)
                {
                    char currentSymbol = currentLineAsString[j];
                    lab[i, j] = currentSymbol;
                }
            }

            var currentNode = new Node(x, y, int.Parse(lab[x, y].ToString()), -1); // TODO:

            FindPathToExit(currentNode.X, currentNode.Y, currentNode);

            Console.WriteLine(bashResults.Max());
        }

        public static void FindPathToExit(int row, int col, Node node)
        {
            if (!IsInRange(row, col))
            {
                int sum = 0;
                for (int i = 0; i < results.Count; i++)
                {
                    sum += results[i];
                }
                sum -= results[results.Count - 1];
                //Console.WriteLine(sum + "----> " +  string.Join(" ", results));
                bashResults.Add(sum);
                return;
            }

            if (lab[row, col] == 'v')
            {
                int sum = 0;
                for (int i = 0; i < results.Count; i++)
                {
                    sum += results[i];
                }
                //sum -= results[results.Count - 1];
                //Console.WriteLine(sum + "----> " + string.Join(" ", results));
                bashResults.Add(sum);
                return;
            }

            if (lab[row, col] == '#')
            {
                int sum = 0;
                for (int i = 0; i < results.Count; i++)
                {
                    sum += results[i];
                }
                //sum -= results[results.Count - 1];

                //Console.WriteLine(sum + "----> " + string.Join(" ", results));
                bashResults.Add(sum);
                return;
            }
            else
            {
                var nodeWeight = lab[row, col];
                var nodeWeightAsInt = int.Parse(lab[row, col].ToString());

                var currentNode = new Node(row, col, nodeWeightAsInt, node.Weight + nodeWeightAsInt);

                lab[row, col] = '#';
                // zapazi kato posetena

                results.Add(nodeWeightAsInt);

                if (IsInRange(row, col))
                {
                    if (col - nodeWeightAsInt >= 0 && row - nodeWeightAsInt >= 0 && col + nodeWeightAsInt < lab.GetLength(1) && row + nodeWeightAsInt < lab.GetLength(0))
                    {
                        if (lab[row, col - nodeWeightAsInt] == '#' && lab[row - nodeWeightAsInt, col] == '#' && lab[row, col + nodeWeightAsInt] == '#' && lab[row + nodeWeightAsInt, col] == '#')
                        {
                            Console.WriteLine("0");
                            Environment.Exit(0);
                        }
                    }

                }

                FindPathToExit(row, col - nodeWeightAsInt, currentNode);
                FindPathToExit(row - nodeWeightAsInt, col, currentNode);
                FindPathToExit(row, col + nodeWeightAsInt, currentNode);
                FindPathToExit(row + nodeWeightAsInt, col, currentNode);

                results.RemoveAt(results.Count - 1);
                lab[row, col] = nodeWeight;
            }
        }

        public static bool IsInRange(int row, int col)
        {
            bool rowInRange = row >= 0 && row < lab.GetLength(0);
            bool colInRange = col >= 0 && col < lab.GetLength(1);
            return rowInRange && colInRange;
        }
    }

    class Node
    {
        public Node(int x, int y, int weight, int moves)
        {
            this.X = x;
            this.Y = y;
            this.Weight = weight;
            this.Moves = moves;
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int Weight { get; set; }

        public int Moves { get; set; }
    }
}
