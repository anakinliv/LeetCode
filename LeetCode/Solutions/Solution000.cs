using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode
{
    public partial class Solution
    {
        public IList<IList<int>> ThreeSum(int[] nums)
        {
            var result = new List<IList<int>>();
            Dictionary<int, int> dic = new Dictionary<int, int>();
            Array.Sort(nums);
            int lasti = 1;
            for(int i = 0;i<nums.Length-2;i++)
            {
                if (nums[i] == lasti) continue;
                else lasti = nums[i];
                if (lasti > 0) break;
                int j = i + 1;
                int k = nums.Length - 1;
                while( k > j )
                {
                    if(nums[j] + nums[k] > -nums[i])
                    {
                        var last = nums[k];
                        do
                        {
                            k--;
                        } while (k > j && last == nums[k]);
                    }
                    else if (nums[j] + nums[k] < -nums[i])
                    {
                        var last = nums[j];
                        do
                        {
                            j++;
                        } while (k > j && last == nums[j]);
                    }
                    else
                    {
                        result.Add(new List<int> { nums[i], nums[j], nums[k] });
                        var last = nums[j];
                        do
                        {
                            j++;
                        } while (k > j && last == nums[j]);
                    }
                }
            }
            return result;
        }
        public bool IsValid(string s)
        {
            Stack<char> stack = new Stack<char>();
            foreach(var character in s)
            {
                if(character == '(' ||
                    character == '[' ||
                    character == '{')
                {
                    stack.Push(character);
                }
                else
                {
                    if(stack.TryPop(out char last))
                    {
                        switch(last)
                        {
                            case '(':
                                if (character != ')') return false;break;
                            case '[':
                                if (character != ']') return false; break;
                            case '{':
                                if (character != '}') return false; break;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            if(stack.TryPop(out var a))
            {
                return false;
            }
            return true;
        }
    }

    public static partial class SolutionTester
    {
        /// <summary>
        /// 15. 三数之和
        /// https://leetcode-cn.com/problems/3sum/
        /// </summary>
        public static void Test0015()
        {
            var solution = new Solution();
            var result = solution.ThreeSum(new int[] { -1, 0, 1, 2, -1, -4});
            foreach(var r in result)
            {
                Console.WriteLine(string.Join(',', r));
            }

        }

        /// <summary>
        /// 20. 有效的括号
        /// https://leetcode-cn.com/problems/valid-parentheses/
        /// </summary>
        public static void Test0020()
        {
            var solution = new Solution();
            var result = solution.IsValid("()");
            Console.WriteLine($"{result} should be T");
            result = solution.IsValid("()[]{}");
            Console.WriteLine($"{result} should be T");
            result = solution.IsValid("(]");
            Console.WriteLine($"{result} should be F");
            result = solution.IsValid("([)]");
            Console.WriteLine($"{result} should be F");
            result = solution.IsValid("{[]}");
            Console.WriteLine($"{result} should be T");
        }
    }
}
