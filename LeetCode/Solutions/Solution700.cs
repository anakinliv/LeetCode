using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode
{
    public partial class Solution
    {
        public int[] DailyTemperatures(int[] T)
        {
            int[] result = new int[T.Length];
            Stack<int> stack = new Stack<int>();
            for(int i =0;i<T.Length;i++)
            {
                int now = T[i];
                while(stack.Count > 0)
                {
                    var last = stack.Peek();
                    if(now > T[last])
                    {
                        result[last] = i - last;
                        stack.Pop();
                    }
                    else
                    {
                        break;
                    }
                }
                stack.Push(i);
            }
            return result;
        }

        public string CrackSafe(int n, int k)
        {
            ISet<int> seen = new HashSet<int>();
            StringBuilder result = new StringBuilder();
            int highest = (int)Math.Pow(10, n - 1);
            int finalK = k;
            DFS753(0, k, result, seen,highest);
            for(int i = 1;i<n;i++)
            {
                result.Append('0');
            }
            return result.ToString();
        }

        public void DFS753(int node,int k ,StringBuilder result,ISet<int> seen,int highest)
        {
            for(int i = 0; i < k; ++i)
            {
                int nei = node * 10 + i;
                if(!seen.Contains(nei))
                {
                    seen.Add(nei);
                    DFS753(nei % highest,k,result,seen,highest);
                    result.Append(i);
                }
            }
        }
    }

    public static partial class SolutionTester
    {
        /// <summary>
        /// 739. 每日温度
        /// https://leetcode-cn.com/problems/daily-temperatures/
        /// </summary>
        public static void Test0739()
        {
            var solution = new Solution();
            int[] result = null;
            result = solution.DailyTemperatures(new int[] { 73, 74, 75, 71, 69, 72, 76, 73 });
            Console.WriteLine($"{string.Join(',',result)} should be [1, 1, 4, 2, 1, 1, 0, 0]");
            result = solution.DailyTemperatures(new int[] { 1, 2, 3, 4, 5 });
            Console.WriteLine($"{string.Join(',', result)} should be [1, 1, 1, 1, 0]");
        }

        public static void Test0753()
        {
            /// <summary>
            /// 753. 破解保险箱
            /// https://leetcode.cn/problems/cracking-the-safe/submissions/
            /// </summary>
            var solution = new Solution();
            string result = "";
            result = solution.CrackSafe(1, 2);
            Console.WriteLine($"{string.Join(',', result)} should be 01");
            result = solution.CrackSafe(2, 2);
            Console.WriteLine($"{string.Join(',', result)} should be 00110");
            result = solution.CrackSafe(3, 2);
            Console.WriteLine($"{string.Join(',', result)} should be 0001110100");
            result = solution.CrackSafe(2, 3);
            Console.WriteLine($"{string.Join(',', result)} should be 0011221020");
            result = solution.CrackSafe(3, 3);
            Console.WriteLine($"{string.Join(',', result)} should be 0011221020");
        }
    }
}
