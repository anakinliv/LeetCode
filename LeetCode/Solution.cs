using LeetCode.Test;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace LeetCode
{
public class Solution
{
    public int Reverse(int x)
    {
        int cap = int.MaxValue / 10;
        int result = 0;
        while(x != 0)
        {
            if(result > cap || (result == cap && x % 10 >= 7))
            {
                return 0;
            }
            else if(result < -cap || (result == -cap && x%10 <= -8))
            {
                return 0;
            }
            result *= 10;
                result += x % 10;
            x = x / 10;
        }
        return result;
    }

        public bool IsPalindrome(int x)
        {
            if (x >=0 && Reverse(x) == x)
            {
                return true;
            }
            return false;
        }

        public int[] romanValues = new[] {1, 5, 10, 50, 100, 500, 1000};

        public int RomanIndex(char character)
        {
            switch (character)
            {
                case 'I':
                    return 0;
                case 'V':
                    return 1;
                case 'X':
                    return 2;
                case 'L':
                    return 3;
                case 'C':
                    return 4;
                case 'D':
                    return 5;
                case 'M':
                    return 6;
            }
            return -1;
        }

        public int RomanToInt(string s)
        {
            int result = 0;
            int temp = 0;
            int lastIndex = 7;
            foreach(var character in s)
            {
                var index = RomanIndex(character);
                if (lastIndex == 7) lastIndex = index;
                if (index > lastIndex)
                {
                    result -= temp;
                    temp = romanValues[index];
                }
                else if(index==lastIndex)
                {
                    temp += romanValues[index];
                }
                else
                {
                    result += temp;
                    temp = romanValues[index];
                }
                lastIndex = index;
            }
            result += temp;
            return result;
        }

        public int[] TwoSum(int[] nums, int target)
        {
            int delta = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                delta = target - nums[i];
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (delta == nums[j])
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return new int[] { 0, 0 };
        }
        int[] temp;
        public int Rob(int[] nums)
        {
            if (nums.Length == 0) return 0;
            if (nums.Length == 1)
            {
                return nums[0];
            }
            temp = new int[nums.Length + 2];
            for (int i = 0; i < nums.Length;i++)
            {
                temp[i+2] = Math.Max(temp[i] + nums[i], temp[i + 1]);
            }
            return temp[temp.Length - 1];
        }

        private int RobSum(int[] nums, int houseIndex)
        {
            if (houseIndex < 0) return 0;
            if (temp[houseIndex] != -1) return temp[houseIndex];
            int a = RobSum(nums, houseIndex - 2)+nums[houseIndex];
            int b = RobSum(nums, houseIndex - 3) + nums[houseIndex];
            temp[houseIndex] = a > b ? a : b;
            return temp[houseIndex];
        }

        public int LargestRectangleArea(int[] heights)
        {
            for (int i = 0; i < heights.Length; i++)
            {
                
            }
            return 0;
        }

        public bool IsSymmetric(TreeNode root)
        {
            return IsSymmetric(root, root);
        }

        private bool IsSymmetric(TreeNode a, TreeNode b)
        {
            if (a == null && b == null) return true;
            if (a == null || b == null) return false;
            if (a.val != b.val)
            {
                return false;
            }
            if (!IsSymmetric(a.left, b.right)) return false;
            if (!IsSymmetric(a.right, b.left)) return false;
            return true;
        }


        public IList<bool> KidsWithCandies(int[] candies, int extraCandies)
        {
            //找到最大值
            int max = 0;
            for(int i = 0;i<candies.Length;i++)
            {
                if (candies[i] > max) max = candies[i];
            }
            var result = new List<bool>();
            var min = max - extraCandies;
            for(int i = 0;i<candies.Length;i++)
            {
                result.Add(candies[i] >= min);
            }
            return result;
        }

        public int SumNums(int n)
        {
            var a = (n > 0) && ((n += SumNums(n-1)) > 0);
            return n;
        }
        
        public Dictionary<int, double> tempDic = new Dictionary<int, double>();

        public double New21GameRe(int N, int K, int W)
        {
            if (K < 1)
                return 1;
            if (N < 1) 
                return 0;
            if (tempDic.ContainsKey(K)) return tempDic[K];
            double result = 0;
            for (int i = 1; i < W+1; i++)
            {
                if(i < K)
                {
                    result += (1d / W) * New21GameRe(N - i, K - i, W);
                }
                else if(i <= N)
                {
                    result += 1d / W;
                }
            }
            tempDic[K] = result;
            return result;
        }


        public double New21Game(int N, int K, int W)
        {
            FixedTest.TestNew21Game(N, K, W);
            if (K < 1)
                return 1;
            if (N < 1)
                return 0;
            double[] rates = new double[K + 1];
            double singleRate = 1d / W;
            for(int i = 1; i <= K; i++)
            {
                if(i == 1)
                {
                    rates[i] = Math.Min(N - K + 1, W) * singleRate;
                }
                else
                {
                    var temp = 0d;
                    //待计算
                    int j = 1;
                    for(;j<Math.Min(i,W+1);j++)
                    {
                        temp += rates[i - j];
                    }
                    rates[i] += temp * singleRate;
                    if (j == W + 1) continue;
                    //安全区
                    int newN = N - K + i;
                    if (newN > W)
                    {
                        rates[i] += (W - j + 1) * singleRate;
                    }
                    else
                    {
                        rates[i] += (newN - j + 1) * singleRate;
                    }
                }
                Console.WriteLine($"{i} : {rates[i]}");
            }
            return rates[K];
        }

        public double New21GameCopy(int N, int K, int W)
        {
            if (K == 0)
            {
                return 1.0;
            }
            double[] dp = new double[K + W];
            for (int i = K; i <= N && i < K + W; i++)
            {
                dp[i] = 1.0;
            }
            dp[K - 1] = 1.0 * Math.Min(N - K + 1, W) / W;
            for (int i = K - 2; i >= 0; i--)
            {
                dp[i] = dp[i + 1] - (dp[i + W + 1] - dp[i + 1]) / W;
            }
            return dp[0];
        }

        public int[] ProductExceptSelf(int[] nums)
        {
            int[] front = new int[nums.Length];
            int[] back = new int[nums.Length];
            front[0] = 1;
            for (int i = 1; i < nums.Length; i++)
            {
                front[i] = nums[i - 1] * front[i - 1];
            }
            back[nums.Length - 1] = 1;
            int temp = 1;
            for (int i = nums.Length - 2; i >= 0; i--)
            {
                temp *= nums[i + 1];
                front[i] *= temp;
            }
            return front;
        }

        public int[] SpiralOrder(int[][] matrix)
        {
            if (matrix.Length == 0 || matrix[0].Length == 0)
            {
                return new int[0];
            }
            int row = matrix.Length;
            int column = matrix[0].Length;

            int[] result = new int[row * column];

            int roundNum = (Math.Min(row, column) + 1) / 2;
            int i = 0;
            for (int round = 0; round < roundNum; round++)
            {
                int c = round;
                int r = round;
                for (; c < column - round; c++)
                {
                    result[i] = matrix[r][c];
                    i++;
                }
                c--;
                r++;
                for (; r < row - round; r++)
                {
                    result[i] = matrix[r][c];
                    i++;
                }
                r--;
                c--;
                if (r == round)
                    break;
                for (; c >= round; c--)
                {
                    result[i] = matrix[r][c];
                    i++;
                }
                c++;
                r--;
                if (c == column - round - 1)
                    break;
                for (; r > round; r--)
                {
                    result[i] = matrix[r][c];
                    i++;
                }
            }

            return result;
        }

        public int LongestConsecutive(int[] nums)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (dic.ContainsKey(nums[i])) continue;
                if (dic.ContainsKey(nums[i] + 1))
                {
                    dic[nums[i]] = dic[nums[i] + 1] + 1;
                    dic[nums[i] + 1] = 0;
                }
                else
                {
                    dic[nums[i]] = 1;
                }
            }
            int max = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (dic.ContainsKey(nums[i]))
                {
                    if (dic[nums[i]] == 0) continue;
                    var next = nums[i] + dic[nums[i]];
                    if (dic.ContainsKey(next))
                    {
                        dic[nums[i]] += dic[next];
                        if (dic[nums[i]] > max)
                            max = dic[nums[i]];
                        dic.Remove(next);
                        i--;
                    }
                    else
                    {
                        if (dic[nums[i]] > max)
                            max = dic[nums[i]];
                    }
                }
            }
            return max;
        }


    }
}
