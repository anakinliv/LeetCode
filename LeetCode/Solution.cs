using System;
using System.Collections.Generic;
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
    }
}
