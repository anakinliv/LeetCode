using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace LeetCode.Define
{ 
//Definition for a binary tree node.
    public class TreeNode {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
            this.val = val;
            this.left = left;
            this.right = right;
        }

        public TreeNode(List<object> objects)
        {
            Queue<TreeNode> queue = new Queue<TreeNode>();
            val = (int)objects[0];
            queue.Enqueue(this);
            bool left = true;
            for (int i = 1; i < objects.Count; i++)
            {
                var obj = objects[i];
                TreeNode node = null;
                if (obj != null)
                {
                    node = new TreeNode((int)obj);
                    queue.Enqueue(node);
                }
                var peek = queue.Peek();
                if (left)
                {
                    peek.left = node;
                }
                else
                {
                    peek.right = node;
                    queue.Dequeue();
                }
                left = !left;
            }
        }
    }
}
