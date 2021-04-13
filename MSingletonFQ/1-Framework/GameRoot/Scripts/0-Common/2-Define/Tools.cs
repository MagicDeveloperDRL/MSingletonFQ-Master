/****************************************************
    文件：Tools.cs
    作者：MRL Liu
    博客：https://blog.csdn.net/qq_41959920
    GitHub：https://github.com/MagicDeveloperDRL
    日期：2019/12/2 11:47:1
    功能：实用工具库
*****************************************************/

using System;

namespace MSingletonFQ
{
  public class Tools
  {
        /// <summary>
        /// 随机获取一个整数
        /// </summary>
        /// <param name="min">最小整数</param>
        /// <param name="max">最大整数</param>
        /// <param name="rd">随机种子</param>
        /// <returns>随机整数</returns>
        public static int RDInt(int min, int max, Random rd = null)
        {
            if (rd == null)
            {
                rd = new Random();
            }
            int val = rd.Next(min, max + 1);
            return val;
        }
    }
}

