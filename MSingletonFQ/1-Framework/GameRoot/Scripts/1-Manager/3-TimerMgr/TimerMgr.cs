/****************************************************
    文件：TimerMgr.cs
    作者：MRL Liu
    博客：https://blog.csdn.net/qq_41959920
    GitHub：https://github.com/MagicDeveloperDRL
    日期：2019/12/2 11:18:7
    功能：计时定时服务类
*****************************************************/

using System;
using UnityEngine;
namespace MSingletonFQ
{
  public class TimerMgr : BaseManager<TimerMgr>
  {
        private PETimer pt = null;
        /// <summary>
        /// 初始化服务类
        /// </summary>
        public override void InitMgr()
        {
            base.InitMgr();
            pt = new PETimer();
            Debug.Log("计时定时服务初始化成功！");
        }
        
        /// <summary>
        /// 实时更新
        /// </summary>
        public  void Update()
        {
            if (pt != null)
            {
                pt.Update();
            }
        }
       
        /// <summary>
        /// 添加定时任务
        /// </summary>
        public int AddTimeTask(Action<int> callback, double delay, PETimeUnit timeUnit = PETimeUnit.Millisecond, int count = 1)
        {
            return pt.AddTimeTask(callback, delay, timeUnit, count);
        }

        /// <summary>
	    /// 删除计时任务
	    /// </summary>
	    public void DelTimeTask(int tid)
        {
            pt.DeleteTimeTask(tid);
            return;
        }
        
        /// <summary>
        /// 获取游戏运行到现在的时间（毫秒）
        /// </summary>
        public double GetMillisecondsTime()
        {
            return pt.GetMillisecondsTime();
        }
    }
}

