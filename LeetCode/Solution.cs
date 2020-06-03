using System;
using System.Collections.Generic;
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

        public Dictionary<int, double> tempDic = new Dictionary<int, double>();

        public double New21Game(int N, int K, int W)
        {
            if (K < 1)
                return 1;
            if (N < 1) 
                return 0;
            if (tempDic.ContainsKey(K)) return tempDic[K];
            double result = 0;
            for (int i = 1; i < K; i++)
            {
                result += (1d / W) * New21Game(N - i, K - i, W);
            }
            result += (Math.Min(N,W) - K + 1) * 1d / W;
            tempDic[K] = result;
            return result;
        }
    }
}
