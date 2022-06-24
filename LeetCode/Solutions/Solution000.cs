using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode
{
    public partial class Solution
    {
        public IList<IList<int>> ThreeSum(int[] nums)
        {
            var result = new List<IList<int>>();
            Dictionary<int, int> dic = new Dictionary<int, int>();
            Array.Sort(nums);
            int lasti = 1;
            for (int i = 0; i < nums.Length - 2; i++)
            {
                if (nums[i] == lasti) continue;
                else lasti = nums[i];
                if (lasti > 0) break;
                int j = i + 1;
                int k = nums.Length - 1;
                while (k > j)
                {
                    if (nums[j] + nums[k] > -nums[i])
                    {
                        var last = nums[k];
                        do
                        {
                            k--;
                        } while (k > j && last == nums[k]);
                    }
                    else if (nums[j] + nums[k] < -nums[i])
                    {
                        var last = nums[j];
                        do
                        {
                            j++;
                        } while (k > j && last == nums[j]);
                    }
                    else
                    {
                        result.Add(new List<int> { nums[i], nums[j], nums[k] });
                        var last = nums[j];
                        do
                        {
                            j++;
                        } while (k > j && last == nums[j]);
                    }
                }
            }
            return result;
        }
        public bool IsValid(string s)
        {
            Stack<char> stack = new Stack<char>();
            foreach (var character in s)
            {
                if (character == '(' ||
                    character == '[' ||
                    character == '{')
                {
                    stack.Push(character);
                }
                else
                {
                    if (stack.TryPop(out char last))
                    {
                        switch (last)
                        {
                            case '(':
                                if (character != ')') return false; break;
                            case '[':
                                if (character != ']') return false; break;
                            case '{':
                                if (character != '}') return false; break;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            if (stack.TryPop(out var a))
            {
                return false;
            }
            return true;
        }

        readonly Dictionary<int, List<string>> generatedParentheseList = new Dictionary<int, List<string>>();
        struct ParentheseNode
        {
            public int left;
            public int right;
            public string text;
        }

        readonly Dictionary<int, Stack<ParentheseNode>> treeDic = new Dictionary<int, Stack<ParentheseNode>>();

        /// <summary>
        /// 递归法，剪枝还不够
        /// </summary>
        /// <param name="parentheseCount"></param>
        /// <returns></returns>
        public List<string> GenerateParentheseList(int parentheseCount)
        {
            //缓存已经处理过的内容
            if (generatedParentheseList.TryGetValue(parentheseCount, out var generatedParenthese))
            {
                return generatedParenthese;
            }

            treeDic.Add(parentheseCount, new Stack<ParentheseNode>());
            var tree = treeDic[parentheseCount];
            var result = new List<string>();

            var node = new ParentheseNode() { left = 1, right = 0, text = "(" };
            tree.Push(node);

            while (tree.TryPop(out var currentNode))
            {
                if (currentNode.left == currentNode.right)
                {
                    var lastResult = GenerateParentheseList(parentheseCount - currentNode.left);
                    foreach (var item in lastResult)
                    {
                        result.Add(currentNode.text + item);
                    }
                    continue;
                }
                if (currentNode.left > currentNode.right)
                {
                    //右边可以塞下括号
                    if (currentNode.right + 1 == parentheseCount)
                    {
                        //已经放完了
                        result.Add(currentNode.text + ")");
                    }
                    else
                    {
                        //右边加一个括号，继续便利
                        var rightNode = new ParentheseNode() { left = currentNode.left, right = currentNode.right + 1, text = currentNode.text + ")" };
                        tree.Push(rightNode);
                    }
                }
                if (currentNode.left < parentheseCount)
                {
                    //左边可以塞下括号
                    if (currentNode.left + 1 == parentheseCount)
                    {
                        currentNode.text = currentNode.text + "(";
                        //左括号已经放完了
                        for (int i = currentNode.right; i < parentheseCount; i++)
                        {
                            currentNode.text = currentNode.text + ")";
                        }
                        result.Add(currentNode.text);
                    }
                    else
                    {
                        var leftNode = new ParentheseNode() { left = currentNode.left + 1, right = currentNode.right, text = currentNode.text + "(" };
                        tree.Push(leftNode);
                    }
                }
            }
            generatedParentheseList.Add(parentheseCount, result);
            return result;
        }

        /// <summary>
        /// 递归法，第一个间隔
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public List<string> GenerateParenthesis(int n)
        {
            //返回已经处理过的内容
            if (generatedParentheseList.TryGetValue(n, out var generatedParenthese))
            {
                return generatedParenthese;
            }
            if(n == 0)
            {
                var resultT = new List<string>() { "" };
                generatedParentheseList.Add(0, resultT);
                return resultT;
            }
            if (n == 1)
            {
                var resultT = new List<string>() { "()" };
                generatedParentheseList.Add(1, resultT);
                return resultT;
            }

            var result = new List<string>();
            for (int i = n - 1; i >= 0; i--)
            {
                var leftList = GenerateParenthesis(i);
                for(int j = 0; j < leftList.Count; j++)
                {
                    string left = $"({leftList[j]})";
                    var rightList = GenerateParenthesis(n - i - 1);
                    for(int k = 0; k < rightList.Count; k++)
                    {
                        result.Add($"{left}{rightList[k]}");
                    }
                }
            }
            generatedParentheseList.Add(n, result);
            return result;
        }
    }

    public static partial class SolutionTester
    {
        /// <summary>
        /// 15. 三数之和
        /// https://leetcode-cn.com/problems/3sum/
        /// </summary>
        public static void Test0015()
        {
            var solution = new Solution();
            var result = solution.ThreeSum(new int[] { -1, 0, 1, 2, -1, -4 });
            foreach (var r in result)
            {
                Console.WriteLine(string.Join(',', r));
            }

        }

        /// <summary>
        /// 20. 有效的括号
        /// https://leetcode-cn.com/problems/valid-parentheses/
        /// </summary>
        public static void Test0020()
        {
            var solution = new Solution();
            var result = solution.IsValid("()");
            Console.WriteLine($"{result} should be T");
            result = solution.IsValid("()[]{}");
            Console.WriteLine($"{result} should be T");
            result = solution.IsValid("(]");
            Console.WriteLine($"{result} should be F");
            result = solution.IsValid("([)]");
            Console.WriteLine($"{result} should be F");
            result = solution.IsValid("{[]}");
            Console.WriteLine($"{result} should be T");
        }

        /// <summary>
        /// 22. 括号生成
        /// https://leetcode.cn/problems/generate-parentheses/
        /// </summary>
        public static void Test0022()
        {
            var solution = new Solution();

            //1
            var result = solution.GenerateParenthesis(1);
            foreach (var r in result)
            {
                Console.WriteLine(string.Join(',', r));
            }
            Console.WriteLine($" should be [\"()\"]");

            //2
            result = solution.GenerateParenthesis(2);
            foreach (var r in result)
            {
                Console.WriteLine(string.Join(',', r));
            }
            Console.WriteLine($" should be [(()),()()]");

            //3
            result = solution.GenerateParenthesis(3);
            foreach (var r in result)
            {
                Console.WriteLine(string.Join(',', r));
            }
            Console.WriteLine($" should be [((())), (()()), (())(),()(()), ()()()]");

            //4
            result = solution.GenerateParenthesis(4);
            foreach (var r in result)
            {
                Console.WriteLine(string.Join(',', r));
            }
            Console.WriteLine($" should be  [(((()))),((()())),((())()),((()))(),(()(())),(()()()),(()())(),(())(()),(())()(),()((())),()(()()),()(())(),()()(()),()()()()]");

        }
    }
}
