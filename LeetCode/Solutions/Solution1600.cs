using System;
using System.Collections.Generic;
using System.Text;

namespace LeetCode
{
    public partial class Solution
    {
        public IList<string> AlertNames(string[] keyName, string[] keyTime)
        {
            List<string> result = new List<string>();
            Dictionary<string, List<int>> keyTimeDic = new Dictionary<string, List<int>>();
            for(int i = 0; i < keyName.Length;i++)
            {
                string time = keyTime[i];
                if (!keyTimeDic.ContainsKey(keyName[i])) keyTimeDic[keyName[i]] = new List<int>();
                keyTimeDic[keyName[i]].Add(((time[0] - '0') * 10 + time[1]) * 60 + (time[3] * 10 + time[4]));
            }
            foreach (var kvp in keyTimeDic)
            {
                if (kvp.Value.Count < 3) continue;    //三次以下不用管了
                kvp.Value.Sort();
                for(int i = 2; i < kvp.Value.Count;i++)
                {
                    if (kvp.Value[i-2] + 60 >= kvp.Value[i])
                    {
                        result.Add(kvp.Key);
                        break;
                    }
                }
            }
            result.Sort();
            return result;
        }
    }

    public static partial class SolutionTester
    {
        /// <summary>
        /// 1604. 警告一小时内使用相同员工卡大于等于三次的人
        /// https://leetcode.cn/problems/alert-using-same-key-card-three-or-more-times-in-a-one-hour-period/
        /// </summary>
        public static void Test1604()
        {
            var solution = new Solution();
            IList<string> result;
            string[] keyName;
            string[] keyTime;

            //1
            keyName = new string[] { "daniel", "daniel", "daniel", "luis", "luis", "luis", "luis" };
            keyTime = new string[] { "10:00", "10:40", "11:00", "09:00", "11:00", "13:00", "15:00" };
            //keyTime = new string[] { "10:30", "10:20", "19:00", "09:00", "18:00", "08:30", "08:25" };
            result = solution.AlertNames(keyName, keyTime);
            Console.WriteLine($"\"{string.Join(',', result)}\" should be [\"daniel\"]");

            //2
            keyName = new string[] { "alice", "alice", "alice", "bob", "bob", "bob", "bob" };
            keyTime = new string[] { "12:01", "12:00", "18:00", "21:00", "21:20", "21:30", "23:00" };
            result = solution.AlertNames(keyName, keyTime);
            Console.WriteLine($"\"{string.Join(',', result)}\" should be [\"bob\"]");

            //3
            keyName = new string[] { "john", "john", "john" };
            keyTime = new string[] { "23:58", "23:59", "00:01" };
            result = solution.AlertNames(keyName, keyTime);
            Console.WriteLine($"\" {string.Join(',', result)}\" should be []");

            //4
            keyName = new string[] { "leslie", "leslie", "leslie", "clare", "clare", "clare", "clare" };
            keyTime = new string[] { "13:00", "13:20", "14:00", "18:00", "18:51", "19:30", "19:49" };
            result = solution.AlertNames(keyName, keyTime);
            Console.WriteLine($"\"{string.Join(',', result)}\" should be [\"clare\",\"leslie\"]");
        }
    }
}