using System;
using System.Collections.Generic;
using System.Text;
using LeetCode.Define;

namespace LeetCode
{
    public partial class Solution
    {
        public IList<int> LargestValuesDFS(TreeNode root)
        {
            if(root == null) return new List<int>();
            List<int> result = new List<int>();
            DFS(root, 0, result);
            return result;
        }

        private void DFS(TreeNode node,int layer, List<int> result)
        {
            if(result.Count <= layer)
            {
                result.Add(node.val);
            }
            else if (result[layer] < node.val)
            {
                result[layer] = node.val;
            }
            if(node.left != null)
                DFS(node.left, layer + 1, result);
            if (node.right != null)
                DFS(node.right, layer + 1, result);
        }

        public IList<int> LargestValuesBFS(TreeNode root)
        {
            if(root == null) return new List<int>();
            List<int> result = new List<int>();
            Queue<TreeNode> currentQ = new Queue<TreeNode>();
            Queue<TreeNode> anotherQ = new Queue<TreeNode>();
            currentQ.Enqueue(root);
            int max = int.MinValue;

            while(currentQ.Count != 0)
            {
                while (currentQ.TryDequeue(out var currentNode))
                {
                    if (currentNode.right != null)
                        anotherQ.Enqueue(currentNode.right);
                    if (currentNode.left != null)
                        anotherQ.Enqueue(currentNode.left);
                    if (currentNode.val > max)
                        max = currentNode.val;
                }
                result.Add(max);
                var temp = currentQ;
                currentQ = anotherQ;
                anotherQ = temp;
                max = int.MinValue;
            }
            return result;
        }

        public char[][] UpdateBoard(char[][] board, int[] click)
        {
            char clickedSquare = board[click[0]][click[1]];
            switch(clickedSquare)
            {
                case 'E':
                    UpdateBoardDFS(board, click);
                    break;
                case 'M':
                    board[click[0]][click[1]] = 'X';
                    break;
            }
            return board;
        }

        int[] dir_x = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] dir_y = { -1, 0, 1, -1, 1, -1, 0, 1 };

        public void UpdateBoardDFS(char[][] board, int[] click)
        {
            int mineCount = 0;
            for(int i = 0; i < dir_x.Length; i++)
            {
                int x = click[0] + dir_x[i];
                int y = click[1] + dir_y[i];
                if (x < 0 || y < 0 || x >= board.Length || y >= board[0].Length) continue;
                if (board[x][y] == 'M') mineCount++;
            }
            if(mineCount > 0)
            {
                board[click[0]][click[1]] = (char)(mineCount + '0');
            }
            else
            {
                board[click[0]][click[1]] = 'B';
                for (int i = 0; i < dir_x.Length; i++)
                {
                    int x = click[0] + dir_x[i];
                    int y = click[1] + dir_y[i];
                    if (x < 0 || y < 0 || x >= board.Length || y >= board[0].Length) continue;
                    if (board[x][y] == 'E')
                        UpdateBoardDFS(board, new int[] { x, y });
                }
            }
        }
    }

    public static partial class SolutionTester
    {
        public static void Test0515()
        {

        }

        /// <summary>
        /// 529. 扫雷游戏
        /// https://leetcode.cn/problems/minesweeper/
        /// </summary>
        public static void Test0529()
        {
            Solution solution = new Solution();

            char[][] board =
            {
                new char[]{ 'E', 'E', 'E', 'E', 'E' },
                new char[]{ 'E', 'E', 'M', 'E', 'E' },
                new char[]{ 'E', 'E', 'E', 'E', 'E' },
                new char[]{ 'E', 'E', 'E', 'E', 'E' }
            };
            int[] click =
            {
                3,0
            };
            var result = solution.UpdateBoard(board, click);

            foreach(var chars in result)
            {
                Console.WriteLine(string.Join(' ', chars));
            }
        }
    }
}
