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

        public int RomanToInt(string s)
        {
            int result = 0;
            int temp = 0;
            foreach(var character in s)
            {
                switch (character)
                {
                    case 'I':
                        {
                            temp += 1;
                            break;
                        }
                    case 'V':
                        {
                            temp += 5;
                            break;
                        }
                    case 'X':
                        {
                            temp += 10;
                            break;
                        }
                    case 'L':
                        {
                            temp += 50;
                            break;
                        }
                    case 'C':
                        {
                            temp += 100;
                            break;
                        }
                    case 'D':
                        {
                            temp += 500;
                            break;
                        }
                    case 'M':
                        {
                            temp += 1000;
                            break;
                        }
                }
            }
            result += temp;
            return result;
        }
    }
}
