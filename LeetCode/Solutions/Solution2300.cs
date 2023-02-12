using System;
using System.Collections.Generic;
using System.Text;
using LeetCode.Define;

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

        public bool EvaluateTree(TreeNode root)
        {
            if(root == null) return false;
            switch(root.val)
            {
                case 0: return false;
                case 1: return true;
                case 2: return EvaluateTree(root.left) || EvaluateTree(root.right);
                case 3: return EvaluateTree(root.left) && EvaluateTree(root.right);
            }
            return false;
        }

        public int FillCups(int[] amount)
        {
            int half = (int)Math.Ceiling((amount[0] + amount[1] + amount[2]) / 2f);
            return Math.Max(Math.Max(amount[0], half), Math.Max(amount[1], amount[2]));
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

        /// <summary>
        /// 2325. 解密消息
        /// https://leetcode.cn/problems/decode-the-message/
        /// </summary>
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

        /// <summary>
        /// 2331. 计算布尔二叉树的值
        /// https://leetcode.cn/problems/evaluate-boolean-binary-tree/
        /// </summary>
        public static void Test2331()
        {
            var solution = new Solution();
            bool result;

            //1
            var root = new TreeNode(new List<object>() { 2, 1, 3, null, null, 0, 1 });
            result = solution.EvaluateTree(root);
            Console.WriteLine($"{result} should be [TRUE]");

            //2
            root = new TreeNode(new List<object>() { 0 });
            result = solution.EvaluateTree(root);
            Console.WriteLine($"{result} should be [FALSE]");
        }

        /// <summary>
        /// 2335. 装满杯子需要的最短总时长
        /// https://leetcode.cn/problems/minimum-amount-of-time-to-fill-cups/
        /// </summary>
        public static void Test2335()
        {
            var solution = new Solution();
            int result;

            //1
            result = solution.FillCups(new int[] { 1, 4, 2 });
            Console.WriteLine($"{result} should be [4]");

            //2
            result = solution.FillCups(new int[] { 5, 4, 4 });
            Console.WriteLine($"{result} should be [7]");

            //3
            result = solution.FillCups(new int[] { 5, 0, 0 });
            Console.WriteLine($"{result} should be [5]");
        }
    }
}