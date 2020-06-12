using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode
{
    public partial class Solution
    {
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
