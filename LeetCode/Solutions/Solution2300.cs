using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode
{
    public partial class Solution
    {
        public bool CheckXMatrix(int[][] grid)
        {
            int n = grid.Length;
            for(int i = 0; i < n;i++)
            {
                for(int j = 0; j < n; j++)
                {
                    if(i == j || i+j+1 == n)
                    {
                        if (grid[i][j] == 0) return false;
                    }
                    else
                    {
                        if (grid[i][j] != 0) return false;
                    }
                }
            }
            return true;
        }

        public string DecodeMessage(string key, string message)
        {
            Dictionary<char,char> map = new Dictionary<char,char>();
            map.Add(' ', ' ');
            char character = 'a';
            for(int i = 0;i<key.Length;i++)
            {
                char c = key[i];
                if(!map.ContainsKey(c))
                {
                    map.Add(c, character);
                    character++;
                }
            }
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < message.Length; i++) 
            {
                sb.Append(map[message[i]]);
            }
            return sb.ToString();
        }
    }

    public static partial class SolutionTester
    {
        /// <summary>
        /// 2319. 判断矩阵是否是一个 X 矩阵
        /// https://leetcode.cn/problems/check-if-matrix-is-x-matrix/
        /// </summary>
        public static void Test2319()
        {
            var solution = new Solution();
            int[][] inputs;
            bool result;

            inputs = new int[][] { new int[] { 2, 0, 0, 1 }, new int[] { 0, 3, 1, 0 }, new int[] { 0, 5, 2, 0 }, new int[] { 4, 0, 0, 2 } };
            result = solution.CheckXMatrix(inputs);
            Console.WriteLine($"{result} should be [True]");

            inputs = new int[][] { new int[] { 5, 7, 0 }, new int[] { 0, 3, 1 }, new int[] { 0, 5, 0 } };
            result = solution.CheckXMatrix(inputs);
            Console.WriteLine($"{result} should be [False]");
        }

        public static void Test2325()
        {
            var solution = new Solution();
            string result;
            string key,message;

            key = "the quick brown fox jumps over the lazy dog";
            message = "vkbs bs t suepuv";
            result = solution.DecodeMessage(key, message);
            Console.WriteLine($"{result} should be [this is a secret]");

            key = "eljuxhpwnyrdgtqkviszcfmabo";
            message = "zwx hnfx lqantp mnoeius ycgk vcnjrdb";
            result = solution.DecodeMessage(key, message);
            Console.WriteLine($"{result} should be [the five boxing wizards jump quickly]");
        }
    }
}