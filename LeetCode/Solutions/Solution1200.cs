using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Numerics;
using System.Text;
using System.Xml.Linq;
using LeetCode.Define;

namespace LeetCode
{
    public partial class Solution
    {
        public int MinimumMoves(int[][] grid)
        {
            int n = grid.Length;

            //BFS
            Queue<Tuple<int, int, bool>> bfsQueue = new Queue<Tuple<int, int, bool>>();
            int[,,] steps = new int[n, n, 2];
            //头位置
            bfsQueue.Enqueue(new Tuple<int, int, bool>(0, 1, false));
            while(bfsQueue.Count > 0)
            {
                var info = bfsQueue.Dequeue();
                int headRow = info.Item1, headColumn = info.Item2;
                bool vertical = info.Item3;
                int currentStep = steps[headRow, headColumn, vertical ? 1 : 0];
                int nextStep = 0;
                //向右
                if (headColumn + 1 < n && grid[headRow][headColumn + 1] == 0)   //头右边有格子
                {
                    if (!vertical || grid[headRow - 1][headColumn + 1] == 0)    //尾巴右边有格子
                    {
                        nextStep = steps[headRow, headColumn + 1, vertical ? 1 : 0];
                        if (nextStep == 0 || nextStep > currentStep + 1)
                        {
                            bfsQueue.Enqueue(new Tuple<int, int, bool>(headRow, headColumn + 1, vertical));
                            steps[headRow, headColumn + 1, vertical ? 1 : 0] = currentStep + 1;
                        }
                    }
                }
                //向下
                if (headRow + 1 < n && grid[headRow + 1][headColumn] == 0)          //头下面有格子
                {
                    if (vertical || grid[headRow + 1][headColumn - 1] == 0)         //尾巴下面有格子
                    {
                        nextStep = steps[headRow + 1, headColumn, vertical ? 1 : 0];
                        if (nextStep == 0 || nextStep > currentStep + 1)
                        {
                            bfsQueue.Enqueue(new Tuple<int, int, bool>(headRow + 1, headColumn, vertical));
                            steps[headRow + 1, headColumn, vertical ? 1 : 0] = currentStep + 1;
                        }
                    }
                }
                //转向
                if (vertical)
                {
                    if (headColumn + 1 < n && grid[headRow - 1][headColumn + 1] == 0 && grid[headRow][headColumn+1] == 0)   //转向有格子
                    {
                        nextStep = steps[headRow - 1, headColumn + 1, vertical ? 0 : 1];
                        if (nextStep == 0 || nextStep > currentStep + 1)
                        {
                            bfsQueue.Enqueue(new Tuple<int, int, bool>(headRow - 1, headColumn + 1, !vertical));
                            steps[headRow - 1, headColumn + 1, vertical ? 0 : 1] = currentStep + 1;
                        }
                    }
                }
                else
                {
                    if (headRow + 1 < n && grid[headRow + 1][headColumn - 1] == 0 && grid[headRow + 1][headColumn] == 0)   //转向有格子
                    {
                        nextStep = steps[headRow + 1, headColumn - 1, vertical ? 0 : 1];
                        if (nextStep == 0 || nextStep > currentStep + 1)
                        {
                            bfsQueue.Enqueue(new Tuple<int, int, bool>(headRow + 1, headColumn - 1, !vertical));
                            steps[headRow + 1, headColumn - 1, vertical ? 0 :1] = currentStep + 1;
                        }
                    }
                }
            }

            if (steps[n-1,n-1,0] == 0)
            {
                return -1;
            }
            else
            {
                return steps[n - 1, n - 1, 0];
            }
        }

        public IList<string> RemoveSubfolders(string[] folder)
        {
            Array.Sort(folder);
            var result = new List<string>();
            int j = 0;
            for (int i = 0; i < folder.Length; i++)
            {
                if (folder[i].Length <= folder[j].Length || !folder[i][folder[j].Length].Equals('/') || !folder[i].StartsWith(folder[j]))
                {
                    result.Add(folder[i]);
                    j = i;
                }
            }
            return result;
        }

        public IList<string> RemoveSubfolders_Dic(string[] folder)
        {

            return null;
        }

        /// <summary>
        /// 初次想法，一塌糊涂
        /// </summary>
        /// <param name="n"></param>
        /// <param name="rollMax"></param>
        /// <returns></returns>
        public int DieSimulator_alpha(int n, int[] rollMax)
        {
            int mod = 1000000007;
            int sum = 6;
            int[][] simCount = new int[6][];
            int[] simDieCount = new int[6];
            for(int i = 0; i < 6;i++)
            {
                simCount[i] = new int[rollMax[i]];
                simCount[i][0] = 1;
                simDieCount[i] = 1;
            }
            for(int i = 1;i<n;i++)
            {
                for(int dieNum = 0;dieNum < 6;dieNum++)
                {
                    for(int count = simCount[dieNum].Length - 1; count >0; count--)
                    {
                        simCount[dieNum][count] = simCount[dieNum][count - 1];
                    }
                }
                for (int dieNum = 0; dieNum < 6; dieNum++)
                {
                    simCount[dieNum][0] = sum - simDieCount[dieNum];
                }
                sum = 0;
                for (int dieNum =0;dieNum<6;dieNum++)
                {
                    simDieCount[dieNum] = 0;
                    for(int count = 0; count < simCount[dieNum].Length;count++)
                    {
                        simDieCount[dieNum] += simCount[dieNum][count];
                        simDieCount[dieNum] = simDieCount[dieNum] % mod;
                    }
                    sum += simDieCount[dieNum];
                }
            }
            return sum % mod;
        }

        public int DieSimulator(int n, int[] rollMax)
        {
            int face = rollMax.Length;
            int[,] matrix = new int[n, face + 1];
            for(int die = 1; die <= face; die++)
            {
                matrix[0, die] = 1;
            }
            matrix[0, 0] = face;
            for(int i = 1; i < n; i++)
            {
                for(int die = 1; die <= face;die ++)
                {
                    matrix[i, die] = matrix[i - 1, 0];
                    int k = i - rollMax[die - 1] + 1;
                    if (k == 1)
                        matrix[i, die]--;
                    else if (k > 1)
                    {
                        matrix[i, die] -= matrix[k - 2, 0];
                        matrix[i, die] = (matrix[i, die] + MOD) % MOD;
                        matrix[i, die] += matrix[k - 2, die];
                        matrix[i, die] = matrix[i, die] % MOD;
                    }
                }
                matrix[i, 0] = sum(matrix, i, 0);
            }
            return matrix[n - 1, 0];
        }
        static int MOD = 1000000007;
        private int sum(int[,] matrix, int n, int die,bool without = false)
        {
            int sum = 0;
            for(int dieNum = 1; dieNum <= 6; dieNum++)
            {
                if (!without || die != dieNum)
                {
                    sum = (sum + matrix[n, dieNum]) % MOD;
                }
            }
            return sum;
        }
    }

    public static partial class SolutionTester
    {
        /// <summary>
        /// 1210. 穿过迷宫的最少移动次数
        /// https://leetcode.cn/problems/minimum-moves-to-reach-target-with-rotations/
        /// </summary>
        public static void Test1210()
        {
            var solution = new Solution();

            //1
            int[][] grid = {
                new int[] { 0, 0, 0, 0, 0, 1 } ,
                new int[] { 1, 1, 0, 0, 1, 0 },
                new int[]{ 0, 0, 0, 0, 1, 1 },
                new int[]{ 0, 0, 1, 0, 1, 0 },
                new int[]{ 0, 1, 1, 0, 0, 0 },
                new int[]{ 0, 1, 1, 0, 0, 0 }
            };
            var result = solution.MinimumMoves(grid);
            Console.WriteLine($"{result} should be [11]");

            //2
            grid = new int[][]{ 
                new int[] { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new int[] { 0, 1, 0, 1, 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0 },
                new int[] { 0, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 0, 0, 1, 0, 0 },
                new int[] { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new int[] { 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
                new int[] { 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1 },
                new int[] { 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            };
            result = solution.MinimumMoves(grid);
            Console.WriteLine($"{result} should be [-1]");
        }

        /// <summary>
        /// 1223. 掷骰子模拟
        /// https://leetcode.cn/problems/dice-roll-simulation/
        /// </summary>
        public static void Test1223()
        {
            var solution = new Solution();

            //1
            var result = solution.DieSimulator(2, new int[] { 1, 1, 2, 2, 2, 3 });
            Console.WriteLine($"{result} should be [34]");

            //2
            result = solution.DieSimulator(2, new int[] { 1, 1, 1, 1, 1, 1 });
            Console.WriteLine($"{result} should be [30]");

            //3
            result = solution.DieSimulator(3, new int[] { 1, 1, 1, 2, 2, 3 });
            Console.WriteLine($"{result} should be [181]");

            //4
            result = solution.DieSimulator(20, new int[] { 8, 5, 10, 8, 7, 2 });
            Console.WriteLine($"{result} should be [822005673]");
        }

        /// <summary>
        /// 1233. 删除子文件夹
        /// https://leetcode.cn/problems/remove-sub-folders-from-the-filesystem/
        /// </summary>
        public static void Test1233()
        {
            var solution = new Solution();

            //1
            string[] folder = { "/a", "/a/b", "/c/d", "/c/d/e", "/c/f" };
            IList<string> result = solution.RemoveSubfolders(folder);
            Console.WriteLine($"\"{string.Join("\",\"", result)}\" should be [\"/a\",\"/c/d\",\"/c/f\"]");

            //2
            folder = new string[] { "/a", "/a/b/c", "/a/b/d" };
            result = solution.RemoveSubfolders(folder);
            Console.WriteLine($"\"{string.Join("\",\"", result)}\" should be [\"/a\"]");

            //3
            folder = new string[] { "/a/b/c", "/a/b/ca", "/a/b/d" };
            result = solution.RemoveSubfolders(folder);
            Console.WriteLine($"\"{string.Join("\",\"", result)}\" should be [\"/a/b/c\",\"/a/b/ca\",\"/a/b/d\"]");
        }
    }
}