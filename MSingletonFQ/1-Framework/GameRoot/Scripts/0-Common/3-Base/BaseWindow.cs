/****************************************************
    文件：Window.cs
    作者：MRL Liu
    博客：https://blog.csdn.net/qq_41959920
    GitHub：https://github.com/MagicDeveloperDRL
    日期：2019/12/7 23:39:37
    功能：UI窗口基类
*****************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = System.Object;

namespace MSingletonFQ
{
	public class BaseWindow : MonoBehaviour {
        protected ResMgr resMgr=null;//资源加载服务
        protected AudioMgr audioMgr=null;//声音播放服务
        protected WndMgr wndMgr = null;//UI管理

        /// <summary>
        /// 界面被显示出来
        /// </summary>
        public virtual void OnEnter()
        {
            resMgr = ResMgr.Instance;
            audioMgr = AudioMgr.Instance;
            wndMgr = WndMgr.Instance;
            SetActive(gameObject, true);
        }

        /// <summary>
        /// 界面暂停
        /// </summary>
        public virtual void OnPause()
        {

        }

        /// <summary>
        /// 界面继续
        /// </summary>
        public virtual void OnResume()
        {

        }

        /// <summary>
        /// 界面不显示,退出这个界面，界面被关闭
        /// </summary>
        public virtual void OnExit()
        {
            SetActive(gameObject, false);
        }

        //点击关闭按钮
        public virtual void ClickBtnClose()
        {
            wndMgr.HideWnd();//关闭按钮
        }

        #region 内部的逻辑处理
        // 初始窗口
        /* protected virtual void InitWnd()
         {
             resSvc = ServiceManager.Instance.ResService;
             audioSvc = ServiceManager.Instance.AudioService;
         }
         // 清除窗口
         protected virtual void ClearWnd()
         {
             resSvc = null;
             audioSvc = null;
         }*/
        #endregion


        #region 公共的外部接口
        // 设置窗口显示状态
        /*public void SetWndShowState(bool isActive = true)
        {
            //判断是否显示窗口
            if (gameObject.activeSelf != isActive)
            {
                SetActive(gameObject, isActive);
            }
            //初始/清除窗口
            if (isActive)
            {
                InitWnd();
            }
            else
            {
                ClearWnd();
            }
        }*/
        //设置UI的显示或者关闭
        protected void SetActive(GameObject go, bool state = true)
        {
            go.SetActive(state);
        }
        protected void SetActive(Transform trans, bool state = true)
        {
            trans.gameObject.SetActive(state);
        }
        protected void SetActive(RectTransform rectTrans, bool state = true)
        {
            rectTrans.gameObject.SetActive(state);
        }
        protected void SetActive(Image img, bool state = true)
        {
            img.transform.gameObject.SetActive(state);
        }
        protected void SetActive(Text txt, bool state = true)
        {
            txt.transform.gameObject.SetActive(state);
        }
        protected void SetSprite(Image img, string path)
        {
            Sprite sp = resMgr.LoadSprite(path, true);
            if (sp != null)
            {
                //PECommon.Log("任务图片设置成功");
                img.sprite = sp;
            }
            else
            {
                //PECommon.Log("任务图片没有设置成功");
            }
        }
        //设置文本UI
        protected void SetText(Text text, string str = "")
        {
            text.text = str;
        }
        protected void SetText(Transform trans, int num = 0)
        {
            SetText(trans.gameObject.GetComponent<Text>(), num);
        }
        protected void SetText(Transform trans, string context = "")
        {
            SetText(trans.GetComponent<Text>(), context);
        }
        protected void SetText(Text txt, int num = 0)
        {
            SetText(txt, num.ToString());
        }
        //获取一个对象的某个组件，如果没有这个组件就为它添加
        protected T GetOrAddComponent<T>(GameObject go) where T : Component
        {
            T t = go.GetComponent<T>();
            if (t == null)
            {
                t = go.AddComponent<T>();
            }
            return t;
        }
        protected Transform GetTrans(Transform trans, string name)
        {
            if (trans != null)
            {
                return trans.Find(name);
            }
            else
            {
                return transform.Find(name);
            }

        }
        #endregion


        #region 封装的点击事件
        protected void onClick(GameObject go, Action<Object> action, object args)
        {
            PEListener listener = GetOrAddComponent<PEListener>(go);
            listener.onCLick = action;
            listener.args = args;
        }
        protected void onClickDown(GameObject go, Action<PointerEventData> action)
        {
            PEListener listener = GetOrAddComponent<PEListener>(go);
            listener.onCLickDown = action;
        }
        protected void onClickUp(GameObject go, Action<PointerEventData> action)
        {
            PEListener listener = GetOrAddComponent<PEListener>(go);
            listener.onClickUp = action;
        }
        protected void onDrag(GameObject go, Action<PointerEventData> action)
        {
            PEListener listener = GetOrAddComponent<PEListener>(go);
            listener.onDrag = action;
        }
        #endregion

    }
}
