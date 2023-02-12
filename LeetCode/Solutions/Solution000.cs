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
            if (n == 0)
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
                for (int j = 0; j < leftList.Count; j++)
                {
                    string left = $"({leftList[j]})";
                    var rightList = GenerateParenthesis(n - i - 1);
                    for (int k = 0; k < rightList.Count; k++)
                    {
                        result.Add($"{left}{rightList[k]}");
                    }
                }
            }
            generatedParentheseList.Add(n, result);
            return result;
        }

        /// <summary>
        /// 动态规划
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public List<string> GenerateParenthesis(int n, bool check)
        {
            return null;
        }

        /// <summary>
        /// 正则表达式匹配
        /// </summary>
        /// <param name="s"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool IsMatch(string s, string p)
        {
            if (s == null || p == null) return false;
            if (s.Length == 0 || p.Length == 0) return false;
            int sIndex = 0;
            int pIndex = 0;
            char lastP = ' ';
            for (; sIndex < s.Length; sIndex++, pIndex++)
            {
                if (pIndex >= p.Length) return false;
                var currentP = p[sIndex];
                if (currentP == '*')
                {
                    if (lastP == '.' || s[sIndex] == lastP)
                    {
                        pIndex--;
                        continue;
                    }
                }
                else if (currentP == '.' || currentP == s[sIndex])
                {
                    lastP = currentP;
                    continue;
                }
                return false;
            }

            return pIndex == p.Length || (pIndex == p.Length - 1 && p[pIndex] == '*');
        }

        public bool IsValidSudoku_Iter(char[][] board)
        {
            //行检查
            HashSet<char> visitedH = new HashSet<char>();
            //列检查
            HashSet<char> visitedV = new HashSet<char>();
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board.Length; j++)
                {
                    if (board[i][j] != '.')
                    {
                        if (visitedH.Contains(board[i][j]))
                        {
                            return false;
                        }
                        visitedH.Add(board[i][j]);
                    }
                    if (board[j][i] != '.')
                    {
                        if (visitedV.Contains(board[j][i]))
                        {
                            return false;
                        }
                        visitedV.Add(board[j][i]);
                    }
                }
                visitedH.Clear();
                visitedV.Clear();
            }

            //格子检查
            for (int gi = 0; gi < 3; gi++)
            {
                for (int gj = 0; gj < 3; gj++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (board[gi * 3 + i][gj * 3 + j] != '.')
                            {
                                if (visitedH.Contains(board[gi * 3 + i][gj * 3 + j]))
                                {
                                    return false;
                                }
                                visitedH.Add(board[gi * 3 + i][gj * 3 + j]);
                            }
                        }
                    }
                    visitedH.Clear();
                }
            }

            return true;
        }

        public bool IsValidSudoku(char[][] board)
        {
            bool[,] hLineCheck = new bool[9, 9];
            bool[,] vLineCheck = new bool[9, 9];
            bool[,] boxCheck = new bool[9, 9];
            for(int i = 0; i < 9;i++)
            {
                for(int j = 0;j<9;j++)
                {
                    if (board[i][j] == '.') continue;
                    int num = board[i][j] - '1';
                    int boxIndex = i / 3 * 3 + j / 3;
                    if (hLineCheck[i, num] || vLineCheck[j, num] || boxCheck[boxIndex, num]) return false;
                    hLineCheck[i, num] = true;
                    vLineCheck[j, num] = true;
                    boxCheck[boxIndex, num] = true;
                }
            }
            return true;
        }

        public int UniquePaths(int m, int n)
        {
            int[,] path = new int[m, n];
            for(int i = 0; i < m;i++)
            {
                for(int j = 0; j < n;j++)
                {
                    if (i == 0 || j == 0) path[i, j] = 1;
                    else path[i, j] = path[i - 1, j] + path[i, j - 1];
                }
            }
            return path[m - 1, n - 1];
        }

        public int UniquePathsWithObstacles(int[][] obstacleGrid)
        {
            int m = obstacleGrid.Length;
            int n = obstacleGrid[0].Length;
            int[,] path = new int[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (obstacleGrid[i][j] == 1)
                    {
                        path[i, j] = 0;
                        continue;
                    }
                    if (i == 0 && j == 0) path[i, j] = 1;
                    else if (i == 0)
                    {
                        path[i, j] = path[i, j - 1];
                    }
                    else if (j == 0)
                    {
                        path[i, j] = path[i - 1, j];
                    }
                    else
                    {
                        path[i, j] = path[i - 1, j] + path[i, j - 1];
                    }
                }
            }
            return path[m - 1, n - 1];
        }
    }

    public static partial class SolutionTester
    {
        /// <summary>
        /// 10. 正则表达式匹配
        /// https://leetcode.cn/problems/regular-expression-matching/
        /// </summary>
        public static void Test0010()
        {
            var solution = new Solution();
            var result = solution.IsMatch("aa", "a");
            Console.WriteLine($"{result} should be false");
            result = solution.IsMatch("aa", "a*");
            Console.WriteLine($"{result} should be true");
            result = solution.IsMatch("ab", ".*");
            Console.WriteLine($"{result} should be true");
            result = solution.IsMatch("aaaaaaab", "a*b");
            Console.WriteLine($"{result} should be true");
            result = solution.IsMatch("abc", "a*b*c");
            Console.WriteLine($"{result} should be true");
            result = solution.IsMatch("aaaaaaab", "a*ab");
            Console.WriteLine($"{result} should be true");
            result = solution.IsMatch("abababababab", "a.*ab");
            Console.WriteLine($"{result} should be true");
        }

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

        /// <summary>
        /// 36. 有效的数独
        /// https://leetcode.cn/problems/valid-sudoku/
        /// </summary>
        public static void Test0036()
        {
            var solution = new Solution();
            char[][] chars = {
                new char[] { '5', '3', '.', '.', '7', '.', '.', '.', '.' },
                new char[] { '6', '.', '.', '1', '9', '5', '.', '.', '.' },
                new char[] { '.', '9', '8', '.', '.', '.', '.', '6', '.' },
                new char[] { '8', '.', '.', '.', '6', '.', '.', '.', '3' },
                new char[] { '4', '.', '.', '8', '.', '3', '.', '.', '1' },
                new char[] { '7', '.', '.', '.', '2', '.', '.', '.', '6' },
                new char[] { '.', '6', '.', '.', '.', '.', '2', '8', '.' },
                new char[] { '.', '.', '.', '4', '1', '9', '.', '.', '5' },
                new char[] { '.', '.', '.', '.', '8', '.', '.', '7', '9' }
            };

            bool result = solution.IsValidSudoku(chars);
            Console.WriteLine($"{result} should be [TRUE]");

            chars = new char[][]{
                new char[] { '8', '3', '.', '.', '7', '.', '.', '.', '.' },
                new char[] { '6', '.', '.', '1', '9', '5', '.', '.', '.' },
                new char[] { '.', '9', '8', '.', '.', '.', '.', '6', '.' },
                new char[] { '8', '.', '.', '.', '6', '.', '.', '.', '3' },
                new char[] { '4', '.', '.', '8', '.', '3', '.', '.', '1' },
                new char[] { '7', '.', '.', '.', '2', '.', '.', '.', '6' },
                new char[] { '.', '6', '.', '.', '.', '.', '2', '8', '.' },
                new char[] { '.', '.', '.', '4', '1', '9', '.', '.', '5' },
                new char[] { '.', '.', '.', '.', '8', '.', '.', '7', '9' }
            };

            result = solution.IsValidSudoku(chars);
            Console.WriteLine($"{result} should be [FALSE]");
        }

        public static void Test0062()
        {
            Solution solution = new Solution();

            int result = solution.UniquePaths(3, 7);
            Console.WriteLine($"{result} should be [28]");

            result = solution.UniquePaths(3, 2);
            Console.WriteLine($"{result} should be [3]");

            result = solution.UniquePaths(7, 3);
            Console.WriteLine($"{result} should be [28]");

            result = solution.UniquePaths(3, 3);
            Console.WriteLine($"{result} should be [6]");
        }

        public static void Test0063()
        {
            Solution solution = new Solution();
            
            //1
            int[][] obstacleGrid =
            {
                new int[] {0, 0, 0},
                new int[] {0, 1, 0},
                new int[] {0, 0, 0}
            };
            int result = solution.UniquePathsWithObstacles(obstacleGrid);
            Console.WriteLine($"{result} should be [2]");

            //2
            obstacleGrid = new int[][] {
                new int[] {0, 1},
                new int[] {0, 0}
            };
            result = solution.UniquePathsWithObstacles(obstacleGrid);
            Console.WriteLine($"{result} should be [1]");
        }
    }
}
