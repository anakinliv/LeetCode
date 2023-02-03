using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode
{
    public partial class Solution
    {
        public string Evaluate(string s, IList<IList<string>> knowledge)
        {
            //knowledge to dictionary
            Dictionary<string,string> dic = new Dictionary<string,string>();
            foreach(var item in knowledge)
            {
                dic[item[0]] = item[1];
            }
            StringBuilder stringBuilder= new StringBuilder();
            int left = -1;
            for(int i = 0; i < s.Length;i++)
            {
                if (s[i] == '(')
                {
                    left = i;
                }
                else if (s[i] == ')')
                {
                    string key = s.Substring(left + 1, i - left - 1);
                    bool find = false;
                    if(dic.TryGetValue(key,out var word))
                    {
                        stringBuilder.Append(word);
                    }
                    else
                    {
                        stringBuilder.Append("?");
                    }
                    left = -1;
                }
                else if(left == -1)
                {
                    stringBuilder.Append(s[i]);
                }
            }
            return stringBuilder.ToString();
        }
    }

    public static partial class SolutionTester
    {
        /// <summary>
        /// 2283. 判断一个数的数字计数是否等于数位的值
        /// https://leetcode.cn/problems/check-if-number-has-equal-digit-count-and-digit-value/
        /// </summary>
        public static void Test1807()
        {
            var solution = new Solution();
            string result;
            var knowledge = new List<IList<string>>() 
            { 
                new List<string>{ "name", "bob"},
                new List<string>{ "age", "two" }
            };
            result = solution.Evaluate("(name)is(age)yearsold", knowledge);
            Console.WriteLine($"{result} should be [bobistwoyearsold]");

            knowledge = new List<IList<string>>()
            {
                new List<string> { "a", "b" }
            };
            result = solution.Evaluate("hi(name)", knowledge);
            Console.WriteLine($"{result} should be [hi?]");

            knowledge = new List<IList<string>>()
            {
                new List<string> { "a", "yes" }
            };
            result = solution.Evaluate("(a)(a)(a)aaa", knowledge);
            Console.WriteLine($"{result} should be [yesyesyesaaa]");
        }
    }
}