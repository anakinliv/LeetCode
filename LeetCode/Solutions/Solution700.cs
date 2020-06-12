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
    }
}
