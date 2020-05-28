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
}
}
