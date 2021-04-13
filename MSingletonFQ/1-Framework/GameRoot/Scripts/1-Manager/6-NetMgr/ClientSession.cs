/****************************************************
    文件：ClientSession.cs
    作者：MRL Liu
    博客：https://blog.csdn.net/qq_41959920
    GitHub：https://github.com/MagicDeveloperDRL
    日期：2019/12/2 11:37:25
    功能：客户端网络会话层
*****************************************************/

using MNetProtocal;
using UnityEngine;

namespace MSingletonFQ
{
  public class ClientSession : PENet.PESession<NetMsg> {
        //当连接到游戏服务器的时候
        protected override void OnConnected()
        {
            base.OnConnected();
            Debug.Log("连接游戏服务器成功");//打印日志
            //GameRoot.AddTips("连接游戏服务器成功");//游戏中显示提示信息
        }
        //当收到游戏服务器消息的时候
        protected override void OnReciveMsg(NetMsg msg)
        {
            base.OnReciveMsg(msg);
            Debug.Log("接收到一个网络消息CMD:" + ((CommondType)msg.cmd).ToString());
            //NetSvc.Instance.AddMsgQue(msg);//添加到消息队列
        }
        //当断开游戏服务器连接的时候
        protected override void OnDisConnected()
        {
            base.OnDisConnected();
            Debug.Log("游戏服务器断开连接");
            //GameRoot.AddTips("游戏服务器断开连接");//游戏中显示提示信息
        }
    }
}

