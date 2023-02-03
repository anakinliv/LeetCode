using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using LeetCode.Define;

namespace LeetCode
{
    public partial class Solution
    {
        public int[] ShortestAlternatingPaths(int n, int[][] redEdges, int[][] blueEdges)
        {
            //整理数据
            Dictionary<int,List<int>> redNextDic = new Dictionary<int,List<int>>();
            Dictionary<int,List<int>> blueNextDic = new Dictionary<int, List<int>>();

            foreach (int[] edge in redEdges)
            {
                if (redNextDic.ContainsKey(edge[0])) redNextDic[edge[0]].Add(edge[1]);
                else redNextDic.Add(edge[0], new List<int>() { edge[1] });
            }
            foreach (int[] edge in blueEdges)
            {
                if (blueNextDic.ContainsKey(edge[0])) blueNextDic[edge[0]].Add(edge[1]);
                else blueNextDic.Add(edge[0], new List<int>() { edge[1] });
            }

            //动态结果（Red表示从红色到，Blue表示从蓝色到）
            int[] resultRed = new int[n];
            int[] resultBlue = new int[n];
            for(int i = 1; i < n;i++)
            {
                resultRed[i] = int.MaxValue;
                resultBlue[i] = int.MaxValue;
            }

            //0是找红的下一个，1是找蓝的
            Queue<int[]> queue = new Queue<int[]>();

            int layer = 1;
            if (redNextDic.TryGetValue(0,out List<int> redFirstList))
            {
                foreach (int next in redFirstList)
                {
                    if(next == 0) continue;
                    int[] element = new int[] { 1, next };
                    resultRed[next] = layer;
                    queue.Enqueue(element);
                }
            }

            if (blueNextDic.TryGetValue(0, out List<int> blueFirstList))
            {
                foreach (int next in blueFirstList)
                {
                    if (next == 0) continue;
                    int[] element = new int[] { 0, next };
                    resultBlue[next] = layer;
                    queue.Enqueue(element);
                }
            }

            int qCount = queue.Count;
            layer = 2;
            while (queue.Count > 0)
            {
                int[] element = queue.Dequeue();
                if (element[0] == 0)
                {
                    if (redNextDic.TryGetValue(element[1], out List<int> redList))
                    {
                        foreach (int next in redList)
                        {
                            if (resultRed[next] != int.MaxValue) continue;
                            int[] newElement = new int[] { 1, next };
                            resultRed[next] = layer;
                            queue.Enqueue(newElement);
                        }
                    }
                }
                else
                {
                    if (blueNextDic.TryGetValue(element[1], out List<int> blueList))
                    {
                        foreach (int next in blueList)
                        {
                            if (resultBlue[next] != int.MaxValue) continue;
                            int[] newElement = new int[] { 0, next };
                            resultBlue[next] = layer;
                            queue.Enqueue(newElement);
                        }
                    }
                }
                qCount --;
                if(qCount == 0) { qCount = queue.Count;layer++; }
            }

            for(int i = 0; i < n; i++)
            {
                resultRed[i] = Math.Min(resultRed[i], resultBlue[i]);
                if (resultRed[i] == int.MaxValue) resultRed[i] = -1;
            }
            return resultRed;
        }

        /// <summary>
        /// 初始解法
        /// </summary>
        /// <param name="root"></param>
        /// <param name="n"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public bool BtreeGameWinningMove_FirstTry(TreeNode root, int n, int x)
        {
            var xNode = Find(root, x);
            int xLeft = GetSubTreeSize(xNode.left);
            int xRight = GetSubTreeSize(xNode.right);
            int xParent = n - 1 - xLeft - xRight;

            int max = Math.Max(Math.Max(xLeft, xRight), xParent);
            return (max > n - max);
        }

        //private TreeNode Find(TreeNode root, int x) 
        //{
        //    //假设全是顺序
        //    Stack<bool> byList = new Stack<bool>();
        //    while(x != 0)
        //    {
        //        byList.Push(x % 2 == 0);
        //        x = x / 2;
        //    }
        //    byList.Pop();
        //    while(byList.Count > 0)
        //    {
        //        if(byList.Pop())
        //        {
        //            root = root.left;
        //        }
        //        else
        //        {
        //            root = root.right;
        //        }
        //    }
        //    return root;
        //}

        private TreeNode Find(TreeNode root, int x) 
        {
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            TreeNode node;
            while((node = queue.Dequeue()) != null)
            {
                if(node.val == x) return node;
                else
                {
                    if (node.left != null) queue.Enqueue(node.left);
                    if (node.right != null) queue.Enqueue(node.right);
                }
            }
            return null;
        }

        private int GetSubTreeSize(TreeNode root)
        {
            if(root == null) return 0;
            var size = 1;
            if (root.left != null) size += GetSubTreeSize(root.left);
            if (root.right != null) size += GetSubTreeSize(root.right);
            return size;
        }

        public bool BtreeGameWinningMove(TreeNode root, int n, int x)
        {
            GetSubTreeSizeWithAndFindX(root,x);
            int xParent = n - 1 - LCount - RCount;

            int max = Math.Max(Math.Max(LCount, RCount), xParent);
            return (max > n - max);
        }

        int LCount = 0;
        int RCount = 0;

        private int GetSubTreeSizeWithAndFindX(TreeNode node,int x) 
        {
            if (node == null) return 0;
            int lCount = GetSubTreeSizeWithAndFindX(node.left, x);
            int rCount = GetSubTreeSizeWithAndFindX(node.right, x);
            if(node.val == x)
            {
                LCount= lCount;
                RCount= rCount;
            }
            return lCount + rCount + 1;
        }
    }

    public static partial class SolutionTester
    {
        /// <summary>
        /// 1129. 颜色交替的最短路径
        /// https://leetcode.cn/problems/shortest-path-with-alternating-colors/
        /// </summary>
        public static void Test1129()
        {
            var solution = new Solution();
            int[] result;
            StringBuilder resultInfo = new StringBuilder();

            //1
            int[][] redEdges = { new int[] { 0, 1 }, new int[] { 1, 2 } };
            int[][] blueEdges = { };
            result = solution.ShortestAlternatingPaths(3, redEdges, blueEdges);
            for(int i = 0; i < result.Length;i++) { resultInfo.Append($"{result[i]},"); }
            Console.WriteLine($"{resultInfo} should be [0,1,-1]");
            resultInfo.Clear();

            //2
            redEdges = new int[][]{ new int[] { 0, 1 } };
            blueEdges = new int[][] { new int[] { 2, 1 } };
            result = solution.ShortestAlternatingPaths(3, redEdges, blueEdges);
            for (int i = 0; i < result.Length; i++) { resultInfo.Append($"{result[i]},"); }
            Console.WriteLine($"{resultInfo} should be [0,1,-1]");
            resultInfo.Clear();

            //3
            redEdges = new int[][] { new int[] { 1, 0 } };
            blueEdges = new int[][] { new int[] { 2, 1 } };
            result = solution.ShortestAlternatingPaths(3, redEdges, blueEdges);
            for (int i = 0; i < result.Length; i++) { resultInfo.Append($"{result[i]},"); }
            Console.WriteLine($"{resultInfo} should be [0,-1,-1]");
            resultInfo.Clear();

            //4
            redEdges = new int[][] { new int[] { 0, 1 } };
            blueEdges = new int[][] { new int[] { 1, 2 } };
            result = solution.ShortestAlternatingPaths(3, redEdges, blueEdges);
            for (int i = 0; i < result.Length; i++) { resultInfo.Append($"{result[i]},"); }
            Console.WriteLine($"{resultInfo} should be [0,1,2]");
            resultInfo.Clear();

            //5
            redEdges = new int[][] { new int[] { 0, 1 }, new int[] { 0, 2 } };
            blueEdges = new int[][] { new int[] { 1, 0 } };
            result = solution.ShortestAlternatingPaths(3, redEdges, blueEdges);
            for (int i = 0; i < result.Length; i++) { resultInfo.Append($"{result[i]},"); }
            Console.WriteLine($"{resultInfo} should be [0,1,1]");
            resultInfo.Clear();

            //6
            redEdges = new int[][] { new int[] { 0, 1 }, new int[] { 1, 2 }, new int[] { 2, 3 }, new int[] { 3, 4 } };
            blueEdges = new int[][] { new int[] { 1, 2 } , new int[] { 2, 3 } , new int[] { 3, 1 } };
            result = solution.ShortestAlternatingPaths(5, redEdges, blueEdges);
            for (int i = 0; i < result.Length; i++) { resultInfo.Append($"{result[i]},"); }
            Console.WriteLine($"{resultInfo} should be [0,1,2,3,7]");
            resultInfo.Clear();

            //7
            redEdges = new int[][] { new int[] { 2, 2 }, new int[] { 0, 1 }, new int[] { 0, 3 }, new int[] { 0, 0 }, new int[] { 0, 4 }, new int[] { 2, 1 }, new int[] { 2, 0 }, new int[] { 1, 4 }, new int[] { 3, 4 } };
            blueEdges = new int[][] { new int[] { 1, 3 }, new int[] { 0, 0 }, new int[] { 0, 3 }, new int[] { 4, 2 }, new int[] { 1, 0 } };
            result = solution.ShortestAlternatingPaths(5, redEdges, blueEdges);
            for (int i = 0; i < result.Length; i++) { resultInfo.Append($"{result[i]},"); }
            Console.WriteLine($"{resultInfo} should be [0,1,2,1,1]");
            resultInfo.Clear();
        }

        public static void Test1145()
        {
            Solution solution = new Solution();

            List<object> nodes = new List<object>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            TreeNode root = new TreeNode(nodes);
           

            Console.WriteLine($"{solution.BtreeGameWinningMove(root, 11, 3)} should be [TRUE]");

            nodes = new List<object>() { 1, 2, 3 };
            root = new TreeNode(nodes);

            Console.WriteLine($"{solution.BtreeGameWinningMove(root, 3, 1)} should be [FALSE]");

            nodes = new List<object>() { 1,null, 2, 3 };
            root = new TreeNode(nodes);

            Console.WriteLine($"{solution.BtreeGameWinningMove(root, 3, 1)} should be [TRUE]");
        }
    }
}