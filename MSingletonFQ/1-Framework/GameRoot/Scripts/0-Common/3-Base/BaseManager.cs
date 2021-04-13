/****************************************************
    文件：BaseManager.cs
    作者：MRL Liu
    博客：https://blog.csdn.net/qq_41959920
    GitHub：https://github.com/MagicDeveloperDRL
    日期：2019/11/29 23:43:22
    功能：管理器基类
*****************************************************/

using UnityEngine;
namespace MSingletonFQ
{
  public abstract class BaseManager<T> : MonoBehaviour where T : Object
  {
        public static T Instance = null;//单例模式

        // 初始化管理器
        public virtual void InitMgr() {
            Instance = this as T;//单例模式
        }
       
    }
}

