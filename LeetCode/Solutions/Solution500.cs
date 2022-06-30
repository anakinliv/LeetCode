using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode.Solutions
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

    }

    public static partial class SolutionTester
    {
        public static void Test0515()
        {

        }
    }

    //Definition for a binary tree node.
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }
}
