using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode
{
    public partial class Solution
    {
        public bool DigitCount(string num)
        {
            if (num == null) throw new ArgumentNullException();
            int[] count = new int[10];
            for(int i = 0; i < num.Length;i++)
            {
                int a = num[i] - 48;
                count[a]++;
            }
            for (int i = 0; i < num.Length; i++)
            {
                if (num[i] - 48 != count[i]) return false;
            }
            return true;
        }

        public int RearrangeCharacters(string s, string target)
        {
            HashSet<char> chars = new HashSet<char>();
            int result = int.MaxValue;
            for(int i = 0;i<target.Length;i++)
            {
                var c = target[i];
                if (chars.Contains(c)) continue;
                chars.Add(c);
                int count = 1;
                for(int j = i+1;j< target.Length;j++)
                {
                    if (target[j] == c) count++;
                }
                int sCount = 0;
                for(int j = 0; j < s.Length; j++)
                {
                    if (s[j] == c) sCount++;
                }
                result = Math.Min(result, sCount / count);
                if(result == 0) break;
            }
            return result;
        }
    }

    public static partial class SolutionTester
    {
        /// <summary>
        /// 2283. 判断一个数的数字计数是否等于数位的值
        /// https://leetcode.cn/problems/check-if-number-has-equal-digit-count-and-digit-value/
        /// </summary>
        public static void Test2283()
        {
            var solution = new Solution();
            bool result;
            result = solution.DigitCount("1210");
            Console.WriteLine($"{result} should be [True]");
            result = solution.DigitCount("030");
            Console.WriteLine($"{result} should be [False]");
        }

        /// <summary>
        /// 2287. 重排字符形成目标字符串
        /// https://leetcode.cn/problems/rearrange-characters-to-make-target-string/
        /// </summary>
        public static void Test2287() 
        {
            var solution = new Solution();
            int result;
            string s, target;
            s = "ilovecodingonleetcode"; target = "code";
            result = solution.RearrangeCharacters(s, target);
            Console.WriteLine($"{result} should be [2]");

            s = "abcba"; target = "abc";
            result = solution.RearrangeCharacters(s, target);
            Console.WriteLine($"{result} should be [1]");

            s = "abbaccaddaeea"; target = "aaaaa";
            result = solution.RearrangeCharacters(s, target);
            Console.WriteLine($"{result} should be [1]");
        }
    }
}