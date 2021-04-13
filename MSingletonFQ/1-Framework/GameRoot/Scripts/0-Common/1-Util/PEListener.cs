/****************************************************
    文件：PEListener.cs
	作者：MRL Liu
    博客：https://blog.csdn.net/qq_41959920
    GitHub：https://github.com/MagicDeveloperDRL
    日期：2019/10/24 15:6:6
	功能：UI事件监听插件
*****************************************************/
using System;
using UnityEngine;
using UnityEngine.EventSystems;//使用unity事件系统
using Object = System.Object;

namespace MSingletonFQ
{
    public class PEListener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public Action<object> onCLick;//鼠标点击事件
        public Action<PointerEventData> onCLickDown;//鼠标点击事件
        public Action<PointerEventData> onClickUp;//鼠标抬起事件
        public Action<PointerEventData> onDrag;//鼠标拖拽事件
        public Object args;//点击时传递的参数
                           //继承接口
        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (onCLick != null)//当这个事件不为空时
            {
                onCLick(args);//执行该事件
            }
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (onCLickDown != null)
            {
                onCLickDown(eventData);
            }
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            if (onClickUp != null)
            {
                onClickUp(eventData);
            }
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (onDrag != null)
            {
                onDrag(eventData);
            }
        }


    }
}

