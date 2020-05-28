using System;
using System.Diagnostics;

namespace LeetCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------------");
            var solution = new Solution();
            var result = solution.Reverse(123);
            Console.WriteLine(result);
            result = solution.Reverse(-123);
            Console.WriteLine(result);
            result = solution.Reverse(120);
            Console.WriteLine(result);
            result = solution.Reverse(1534236469);
            Console.WriteLine(result);
        }        
    }
}
