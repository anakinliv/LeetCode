using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LeetCode
{
    public static class NonogramSolution
    {
        /// <summary>
        /// 将长度为length的线段，分成count段，(无限制)
        /// </summary>
        /// <param name="length"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static void AllSplitResult(int length,int count)
        {
            var result = SubSplit(length, count);
            foreach(var val in result)
            {
                string str = "{";
                foreach(int i in val)
                {
                    str += i;
                    str += ",";
                }
                str += "}";
                str.Replace(".}", "}");
                Console.WriteLine(str);
            }
        }

        public static List<List<int>> SubSplit(int length,int count)
        {
            var result = new List<List<int>>();
            if (count == 1)
            {
                result.Add(new List<int> { length });
                return result;
            }
            else
            {
                for(int i = 0; i <= length; i++)
                {
                    var next = SubSplit(length - i, count - 1);
                    foreach(var nextResult in next)
                    {
                        var a = new List<int> { i };
                        a.AddRange(nextResult);
                        result.Add(a);
                    }
                }
            }
            return result;
        }
    }
}
