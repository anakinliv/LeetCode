﻿using LeetCode.Test;
using System;
using System.Diagnostics;
using System.Reflection;

namespace LeetCode
{
    class Program
    {
        static void Main(string[] args)
        {
            int problemId = 1138;
            Type type = typeof(SolutionTester);
            type.InvokeMember($"Test{problemId.ToString("D4")}", BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public, null, null, new object[0]);
            return;

            //FixedTest.TestNew21Game(4, 3, 5);
            var solution = new Solution();

            Console.WriteLine(solution.LongestConsecutive(new int[] { 1,2,0,1}));
            return;

            var input = new int[4][];
            input[0] = new int[1] { 1 };
            input[1] = new int[1] { 2 };
            input[2] = new int[1] { 3 };
            input[3] = new int[1] { 4 };
            var result = solution.SpiralOrder(input);
            Console.WriteLine(string.Join(",", result));

            return;


            //Console.WriteLine(solution.RomanToInt("MCMXCIV"));
            //solution.tempDic.Clear();
            //Console.WriteLine(solution.New21Game(5710, 5070, 8516));
            solution.tempDic.Clear();
            Console.WriteLine(solution.New21Game(21, 17, 10));
            //Console.WriteLine(solution.New21Game(21, 17, 10));
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
            var a = new int[] { 2, 7, 9, 3, 1 };
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
