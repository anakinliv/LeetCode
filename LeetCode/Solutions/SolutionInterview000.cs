using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode
{
    public partial class Solution
    {
        public int TranslateNum(int num)
        {
            //2147483647
            var numStr = num.ToString();
            var ss = new int[] { 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144 };
            var count = 1;
            var sum = 1;
            var lastChar = '0';
            for (int i = 0; i < numStr.Length; i++)
            {
                if (lastChar == '1')
                {
                    count++;
                }
                else if (lastChar == '2' && numStr[i] < '6')
                {
                    count++;
                }
                else
                {
                    sum *= ss[count];
                    count = 1;
                }
                lastChar = numStr[i];
            }
            sum *= ss[count];
            return sum;
        }
    }

    public static partial class SolutionTester
    {
        /// <summary>
        /// 面试题46. 把数字翻译成字符串
        /// https://leetcode-cn.com/problems/ba-shu-zi-fan-yi-cheng-zi-fu-chuan-lcof/
        /// </summary>
        public static void Test5046()
        {
            var solution = new Solution();
            var result = solution.TranslateNum(12258);
            Console.WriteLine(result);
            Console.WriteLine("should be 5");
        }
    }
}
