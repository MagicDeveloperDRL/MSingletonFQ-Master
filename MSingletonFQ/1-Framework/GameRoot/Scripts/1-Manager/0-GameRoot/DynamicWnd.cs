/****************************************************
	文件：DynamicWnd.cs
	作者：MRL Liu
    博客：https://blog.csdn.net/qq_41959920
    GitHub：https://github.com/MagicDeveloperDRL
	日期：2019/10/20 0:7:10
	功能：动态UI元素界面
*****************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MSingletonFQ
{	
	public class DynamicWnd : BaseWindow
    {
	    public Animation tipsAni;//动画组件
	    public Text tipsTxt;//显示信息文本
	    private bool isTipsShow = false;//是否在显示消息
	    private Queue<string> tipsQue = new Queue<string>();//消息队列
	    
	    /// <summary>
	    /// 实时更新
	    /// </summary>
	    private void Update()
	    {
	        //当消息队列中有消息且当前没有消息显示的时候
	        if (tipsQue.Count > 0 && isTipsShow == false)
	        {
	            lock (tipsQue)
	            {
	                string tips = tipsQue.Dequeue();//获得当前消息
	                isTipsShow = true;//当前消息正在显示
	                SetTips(tips);//显示当前消息
	            }
	        }
	    }
	
	    /// <summary>
	    /// 初始化动态UI元素窗口
	    /// </summary>
	    public override void OnEnter()
	    {
	        base.OnEnter();//调用基类窗口
	        SetActive(tipsTxt, false);//关闭动态窗口文本
	    }
	    
    #region 动态消息提示
	   /// <summary>
	    /// 添加到消息队列
	    /// </summary>
	    /// <param name="tips">消息文本</param>
	    public void AddTips(string tips) {
	        lock (tipsQue) {
	            tipsQue.Enqueue(tips);
	        }
	    }
	    /// <summary>
	    /// 设置当前显示消息的文本
	    /// </summary>
	    /// <param name="tips"></param>
	    private void SetTips(string tips)
	    {
	        SetActive(tipsTxt);//激活文本
	        SetText(tipsTxt, tips);//显示文本
	
	        AnimationClip clip = tipsAni.GetClip("TipsShowAni");//获取动画组件
	        tipsAni.Play();//播放动画
	        //延时关闭激活状态
	        StartCoroutine(AniPlayDone(clip.length, () => {
	            SetActive(tipsTxt, false);//关闭消息
	            isTipsShow = false;//当前消息显示完毕
	        }));
	    }
	    //延时关闭激活状态的协程
	    private IEnumerator AniPlayDone(float sec, Action cb)
	    {
	        yield return new WaitForSeconds(sec);
	        if (cb != null)
	        {
	            cb();
	        }
	    }
	
    #endregion
	
	
    
	
	}
}
