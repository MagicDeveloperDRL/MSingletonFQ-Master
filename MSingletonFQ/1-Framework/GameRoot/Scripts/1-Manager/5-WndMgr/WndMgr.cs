/****************************************************
    文件：WndMgr.cs
    作者：MRL Liu
    博客：https://blog.csdn.net/qq_41959920
    GitHub：https://github.com/MagicDeveloperDRL
    日期：2019/12/12 22:47:57
    功能：UI窗口管理器
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSingletonFQ
{
    //UI窗口类型
    public enum UIWindowType {
        None,
        LoadingWnd,//场景导入窗口
        PlayerControllWnd,
        
    }
	public class WndMgr : BaseManager<WndMgr> {

        private Stack<BaseWindow> mShowWndStack;//窗口显示栈
        private Transform canvasTransform;
        private Transform CanvasTransform
        {
            get
            {
                if (canvasTransform == null)
                {
                    canvasTransform = GameObject.Find("Canvas").transform;
                }
                return canvasTransform;
            }
        }
        //private Dictionary<UIWindowType, string> mWndPathDict;//UI窗口类型与预设体路径
        private Dictionary<UIWindowType, BaseWindow> mWndDict;//UI窗口类型与UI窗口实体
        /// <summary>
        /// 初始化管理器
        /// </summary>
        public override void InitMgr()
        {
            base.InitMgr();
        }

        /// <summary>
        /// 把某个页面入栈，  把某个页面显示在界面上
        /// </summary>
        public void ShowWnd(UIWindowType wndType)
        {
            if (mShowWndStack == null)
                mShowWndStack = new Stack<BaseWindow>();

            //判断一下栈里面是否有页面
            if (mShowWndStack.Count > 0)
            {
                BaseWindow topPanel = mShowWndStack.Peek();
                topPanel.OnPause();//暂停窗口
            }
            Debug.Log("显示窗口："+wndType);
            BaseWindow panel = GetWnd(wndType);
            panel.OnEnter();//进入窗口
            mShowWndStack.Push(panel);//压入窗口
        }
        /// <summary>
        /// 出栈 ，把页面从界面上移除
        /// </summary>
        public void HideWnd()
        {
            
            if (mShowWndStack == null)
                mShowWndStack = new Stack<BaseWindow>();

            if (mShowWndStack.Count <= 0) return;

            //关闭栈顶页面的显示
            BaseWindow topPanel = mShowWndStack.Pop();
            topPanel.OnExit();
            Debug.Log("关闭窗口:"+ topPanel);
            if (mShowWndStack.Count <= 0) return;
            BaseWindow topPanel2 = mShowWndStack.Peek();
            topPanel2.OnResume();

        }

        /// <summary>
        /// 根据面板类型 得到实例化的面板
        /// </summary>
        /// <returns></returns>
        public BaseWindow GetWnd(UIWindowType wndType)
        {
            if (mWndDict == null)
            {
                mWndDict = new Dictionary<UIWindowType, BaseWindow>();
            }


            BaseWindow wnd = null;

            if (mWndDict.TryGetValue(wndType,out wnd))//如果有缓存，直接返回
            {
                return wnd;
            }
            else{//如果没有缓存，就尝试根据路径实例化窗口
                string path = GetPath(wndType);
                GameObject instWnd = GameObject.Instantiate(Resources.Load(path)) as GameObject;//实例化窗口
                if (instWnd!=null)
                {
                    
                    instWnd.transform.SetParent(CanvasTransform, false);//设置父类对象
                    mWndDict.Add(wndType, instWnd.GetComponent<BaseWindow>());//添加到缓存中
                    return instWnd.GetComponent<BaseWindow>();
                }
                else {
                    Debug.LogError("UI窗口管理器中没有"+ wndType + "对应的路径");
                    return null;
                }
            }
            
        }
        /// <summary>
        /// 根据面板类型，得到其预设体对应的路径
        /// </summary>
        /// <returns></returns>
        public string GetPath(UIWindowType wndType) {
            string path=PathDefine.UIWndPrefabPath+ wndType;
            return path;
        }
    }
}