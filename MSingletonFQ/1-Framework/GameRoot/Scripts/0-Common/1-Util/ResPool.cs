/****************************************************
    文件：ObjectPool.cs
    作者：MRL Liu
    博客：https://blog.csdn.net/qq_41959920
    GitHub：https://github.com/MagicDeveloperDRL
    日期：2019/11/30 1:22:36
    功能：资源对象池
*****************************************************/

using System.Collections.Generic;
using UnityEngine;
namespace MSingletonFQ
{
    public class ResPool<T> where T : Object
    {
        //缓存字典
        private Dictionary<string, T> dic = new Dictionary<string, T>();

        /// <summary>
        /// 加载单个资源
        /// </summary>
        /// <param name="path">资源所在路径</param>
        /// <param name="cache">是否进行缓存</param>
        /// <returns>初始化的路径</returns>
        public T Load(string path, bool cache = true)
        {
            T obj = null;
            //导入资源文件
            if (!dic.TryGetValue(path, out obj))
            {//如果没有当前要导入的缓存
                obj = Resources.Load<T>(path);//导入声音资源
                if (cache)
                {
                    dic.Add(path, obj);//添加声音缓存
                }
            }
            return obj;
        }

        /// <summary>
        /// 加载全部资源
        /// </summary>
        /// <returns></returns>
        public List<T> LoadAll()
        {
            List<T> list = null;
            foreach (var item in dic)
            {
                list.Add(item.Value);
            }
            return list;
        }

        /// <summary>
        /// 清空资源
        /// </summary>
        public void Clear()
        {
            dic.Clear();
        }
    }
}

