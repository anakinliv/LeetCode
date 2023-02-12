using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;

namespace LeetCode
{
    public partial class Solution
    {
        public int GetMaximumConsecutive(int[] coins)
        {
            List<int> coinList = new List<int>(coins);
            coinList.Sort();
            int sum = 0;
            for(int i = 0;i< coinList.Count;i++)
            {
                if(sum + 1 >= coinList[i])
                {
                    sum += coinList[i];
                }
                else
                {
                    break;
                }
            }
            return sum + 1;
        }
    }

    public class AuthenticationManager
    {
        int timeToLive;
        Dictionary<string, int> authentication;
        public AuthenticationManager(int timeToLive)
        {
            this.timeToLive = timeToLive;
            authentication = new Dictionary<string, int>();
        }

        public void Generate(string tokenId, int currentTime)
        {
            authentication[tokenId] = currentTime + timeToLive;
        }

        public void Renew(string tokenId, int currentTime)
        {
            if(authentication.ContainsKey(tokenId))
            {
                if (authentication[tokenId] <= currentTime) { authentication.Remove(tokenId); }
                else authentication[tokenId] = currentTime + timeToLive;
            }
        }

        public int CountUnexpiredTokens(int currentTime)
        {
            int sum = 0;
            foreach(var time in authentication.Values)
            {
                if (time > currentTime)
                    sum++;
            }
            return sum;
        }
    }

    public static partial class SolutionTester
    {
        /// <summary>
        /// 1798. 你能构造出连续值的最大数目
        /// https://leetcode.cn/problems/maximum-number-of-consecutive-values-you-can-make/
        /// </summary>
        public static void Test1798()
        {
            var solution = new Solution();
            int result;
            result = solution.GetMaximumConsecutive(new int[] { 1, 3 });
            Console.WriteLine($"{result} should be [2]");

            result = solution.GetMaximumConsecutive(new int[] { 1, 1, 1, 4 });
            Console.WriteLine($"{result} should be [8]");

            result = solution.GetMaximumConsecutive(new int[] { 1, 4, 10, 3, 1 });
            Console.WriteLine($"{result} should be [20]");
        }

        public static void Test1797()
        {
            string[] inputKey = new string[] { "AuthenticationManager", "renew", "generate", "countUnexpiredTokens", "generate", "renew", "renew", "countUnexpiredTokens" };
            object[] inputVal = new object[] { 5, new object[] { "aaa", 1 }, new object[] { "aaa", 2 }, 6, new object[] { "bbb", 7 }, new object[] { "aaa", 8 }, new object[] { "bbb", 10 }, 15 };
            List<string> result = new List<string>();
            AuthenticationManager manager = null;
            for(int i = 0;i< inputKey.Length;i++)
            {
                switch(inputKey[i])
                {
                    case "AuthenticationManager":
                        manager = new AuthenticationManager((int)inputVal[i]);
                        result.Add(null);
                        break;
                    case "renew":
                    case "generate":
                        var tokenId = (string)((object[])inputVal[i])[0];
                        var currentTime = (int)((object[])inputVal[i])[1];
                        if (inputKey[i] == "renew")
                            manager.Renew(tokenId, currentTime);
                        if (inputKey[i] == "generate")
                            manager.Generate(tokenId, currentTime);
                        result.Add(null);
                        break;
                    case "countUnexpiredTokens":
                        var count = manager.CountUnexpiredTokens((int)inputVal[i]);
                        result.Add(count.ToString());
                        break;
                }
            }
            Console.WriteLine(string.Join(',', result));

            result.Clear();

            inputKey = new string[] { 
                "AuthenticationManager", "renew", "countUnexpiredTokens", "countUnexpiredTokens",
                "countUnexpiredTokens", "generate", "generate", "countUnexpiredTokens", 
                "countUnexpiredTokens", "countUnexpiredTokens", "countUnexpiredTokens",
                "countUnexpiredTokens", "renew", "countUnexpiredTokens", "countUnexpiredTokens",
                "renew", "countUnexpiredTokens", "generate", "renew",
                "countUnexpiredTokens", "countUnexpiredTokens", "renew", "renew",
                "renew", "generate", "renew", "generate",
                "countUnexpiredTokens", "countUnexpiredTokens", "generate", "countUnexpiredTokens",
                "countUnexpiredTokens", "countUnexpiredTokens", "countUnexpiredTokens", "renew",
                "generate", "generate", "generate", "countUnexpiredTokens",
                "renew", "generate", "countUnexpiredTokens", "countUnexpiredTokens",
                "generate", "generate", "generate", "countUnexpiredTokens",
                "countUnexpiredTokens", "countUnexpiredTokens", "countUnexpiredTokens", "countUnexpiredTokens",
                "renew", "renew", "renew", "countUnexpiredTokens",
                "countUnexpiredTokens" };
            inputVal = new object[] {
                104,new object[]{"ox",50},73,87,
                93,new object[]{"yyeu",104},new object[]{"r",131},167,
                172,191,206,232,
                new object[]{"r",235},239,257,new object[] { "vi", 268 },
                292,new object[] { "vi", 296 },new object[] { "yu", 303 },326,
                339,new object[] { "aimzm", 343 },new object[] { "umdzy", 346 },new object[] { "qyf", 347 },
                new object[] { "mfne", 353 },new object[] { "nn", 357 },new object[] { "hz", 359 },422,
                434,new object[] { "pel", 473 },494,498,
                508,524,new object[] { "pt", 552 },new object[] { "vbaa", 568 },
                new object[] { "gt", 592 },new object[] { "zxdv", 611 },618,new object[] { "fbp", 619 },
                new object[] { "giih", 622 },623,629,new object[] { "chmi", 659 },
                new object[] { "doohl", 671 },new object[] { "svxbv", 715 },722,749,
                754,771,794,new object[] { "pel", 865 },new object[] { "i", 919 },
                new object[] { "aqa", 962 },976,978
            };

            manager = null;
            for (int i = 0; i < inputKey.Length; i++)
            {
                switch (inputKey[i])
                {
                    case "AuthenticationManager":
                        manager = new AuthenticationManager((int)inputVal[i]);
                        result.Add(null);
                        break;
                    case "renew":
                    case "generate":
                        var tokenId = (string)((object[])inputVal[i])[0];
                        var currentTime = (int)((object[])inputVal[i])[1];
                        if (inputKey[i] == "renew")
                            manager.Renew(tokenId, currentTime);
                        if (inputKey[i] == "generate")
                            manager.Generate(tokenId, currentTime);
                        result.Add(null);
                        break;
                    case "countUnexpiredTokens":
                        var count = manager.CountUnexpiredTokens((int)inputVal[i]);
                        result.Add(count.ToString());
                        break;
                }
            }
            Console.WriteLine(string.Join(',', result));
        }
    }
}