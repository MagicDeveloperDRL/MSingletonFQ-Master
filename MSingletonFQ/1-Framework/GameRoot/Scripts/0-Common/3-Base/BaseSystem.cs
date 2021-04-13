/****************************************************
    文件：BaseSystem.cs
    作者：MRL Liu
    博客：https://blog.csdn.net/qq_41959920
    GitHub：https://github.com/MagicDeveloperDRL
    日期：2019/12/6 16:43:33
    功能：业务系统基类
*****************************************************/

using UnityEngine;
namespace MSingletonFQ
{
  public abstract class BaseSystem<T> : MonoBehaviour where T : Object
  {
        public static T Instance = null;//单例模式
        protected ResMgr resMgr=null;//资源加载服务
        protected AudioMgr audioMgr=null;//声音播放服务
        protected NetMgr netMgr=null;//网络通信服务
        protected TimerMgr timerMgr=null;//计时服务
        protected WndMgr wndMgr = null;//UI管理
        /// <summary>
        /// 构造函数
        /// </summary>
        protected BaseSystem() { }
        
        // 初始系统
        public virtual void InitSys()
        {
            Instance = this as T;//单例模式
            resMgr = ResMgr.Instance;
            audioMgr = AudioMgr.Instance;
            netMgr = NetMgr.Instance;
            timerMgr = TimerMgr.Instance;
            wndMgr = WndMgr.Instance;
        }
        // 进入系统
        public abstract void EnterSystem();
        
        // 退出系统 
        public abstract void ExitSystem();
    }
}

