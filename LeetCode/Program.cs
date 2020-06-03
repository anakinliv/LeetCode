using System;
using System.Diagnostics;

namespace LeetCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var solution = new Solution();
            //Console.WriteLine(solution.RomanToInt("MCMXCIV"));
            solution.tempDic.Clear();
            //Console.WriteLine(solution.New21Game(21, 17, 10));
            Console.WriteLine(solution.New21Game(3, 2, 3));
            foreach (var kvp in solution.tempDic)
            {
                Console.WriteLine($"{kvp.Key} : {kvp.Value}");
            }

            return;
            Console.WriteLine(solution.New21Game(1, 0, 2));

            Console.WriteLine(solution.New21Game(6, 1, 10));
            Console.WriteLine(solution.New21Game(10, 1, 10));
            Console.WriteLine(solution.New21Game(10, 2, 10));
            Console.WriteLine(solution.New21Game(20, 16, 10));
            return;
            var a = new int[] { 2,7,9,3,1};
            Console.WriteLine(solution.Rob(a));
            a = new int[] { 226, 174, 214, 16, 218, 48, 153, 131, 128, 17, 157, 142, 88, 43, 37, 157, 43, 221, 191, 68, 206, 23, 225, 82, 54, 118, 111, 46, 80, 49, 245, 63, 25, 194, 72, 80, 143, 55, 209, 18, 55, 122, 65, 66, 177, 101, 63, 201, 172, 130, 103, 225, 142, 46, 86, 185, 62, 138, 212, 192, 125, 77, 223, 188, 99, 228, 90, 25, 193, 211, 84, 239, 119, 234, 85, 83, 123, 120, 131, 203, 219, 10, 82, 35, 120, 180, 249, 106, 37, 169, 225, 54, 103, 55, 166, 124 };
            Console.WriteLine(solution.Rob(a));
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
