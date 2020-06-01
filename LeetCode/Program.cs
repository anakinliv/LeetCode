using System;
using System.Diagnostics;

namespace LeetCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //var solution = new Solution();
            //Console.WriteLine(solution.RomanToInt("MCMXCIV"));
            NonogramSolution.AllSplitResult(10, 4);
        }

        static void Main7(string[] args)
        {
            Console.WriteLine("-------------");
            var solution = new Solution();
            var result = solution.Reverse(123);
            Console.WriteLine(result);
            result = solution.Reverse(-121);
            Console.WriteLine(result);
            result = solution.Reverse(120);
            Console.WriteLine(result);
            result = solution.Reverse(1534236469);
            Console.WriteLine(result);
            result = solution.Reverse(1563847412);
            Console.WriteLine(result);
        }        
    }
}
