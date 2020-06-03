using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode.Test
{
    public static class FixedTest
    {
        public static void TestNew21Game(int N, int K, int W)
        {
            int count = 1000000;
            int success = 0;
            var rnd = new Random();
            for(int i=0;i< count; i++)
            {
                int sum = 0;
                while(sum <K)
                {
                    sum += rnd.Next(1, W+1);
                }
                if(sum<=N)
                {
                    success++;
                }
            }
            Console.WriteLine(1f * success / count);
        }
    }
}
