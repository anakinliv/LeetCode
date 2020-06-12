using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode
{
    public partial class Solution
    {
        private class Group
        {
            public byte id;
        }
        public bool EquationsPossible(string[] equations)
        {
            Dictionary<char, Group> groups = new Dictionary<char, Group>();
            byte groupIndex = 1;
            for(int i = 0; i < equations.Length;i++)
            {
                string eq = equations[i];
                if(eq[1].Equals('='))
                {
                    var a = eq[0];
                    var b = eq[3];
                    if(groups.ContainsKey(a))
                    {
                        if(groups.ContainsKey(b))
                        {
                            groups[b].id = groups[a].id;
                        }
                        else
                        {
                            groups.Add(b, groups[a]);
                        }
                    }
                    else
                    {
                        if (groups.ContainsKey(b))
                        {
                            groups.Add(a, groups[b]);
                        }
                        else
                        {
                            var newGroup = new Group() { id = groupIndex };
                            groupIndex++;
                            groups[a] = newGroup;
                            groups[b] = newGroup;
                        }
                    }
                }
            }

            for (int i = 0; i < equations.Length; i++)
            {
                string eq = equations[i];
                if (eq[1].Equals('!'))
                {
                    var a = eq[0];
                    var b = eq[3];
                    if (a == b) return false;
                    if (groups.ContainsKey(a))
                    {
                        if (groups.ContainsKey(b))
                        {
                            if(groups[a].id == groups[b].id)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
    }

    public static partial class SolutionTester
    {
        /// <summary>
        /// 990. 等式方程的可满足性
        /// https://leetcode-cn.com/problems/satisfiability-of-equality-equations/
        /// </summary>
        public static void Test0990()
        {
            var solution = new Solution();
            var result = solution.EquationsPossible(new string[] { "b==a", "a==b" });
            Console.WriteLine(result);
            result = solution.EquationsPossible(new string[] { "a!=a" });
            Console.WriteLine(result);
            result = solution.EquationsPossible(new string[] { "a==b", "b!=c", "c==a" });
            Console.WriteLine(result);
        }
    }
}
