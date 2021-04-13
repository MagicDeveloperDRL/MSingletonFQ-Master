/****************************************************
    文件：NetMgr.cs
    作者：MRL Liu
    博客：https://blog.csdn.net/qq_41959920
    GitHub：https://github.com/MagicDeveloperDRL
    日期：2019/12/2 11:18:23
    功能：网络通信服务类
*****************************************************/

using MNetProtocal;
using PENet;
using System.Collections.Generic;
using UnityEngine;
namespace MSingletonFQ
{
  public class NetMgr : BaseManager<NetMgr>
  {
        PESocket<ClientSession, NetMsg> client = null;//客户端套接字

        /// <summary>
        /// 初始化服务类
        /// </summary>
        public override void InitMgr( )
        {
            if (!enabled)//如果自身没有开启则不进行初始化
            {
                return;
            }
            base.InitMgr();
            InitNet();//重新初始化网络服务模块
        }
        /// <summary>
        /// 初始化网络服务
        /// </summary>
        public void InitNet() {
            if (client!=null)
            {
                client = null;
            }
            //初始化客户端套接字
            client = new PESocket<ClientSession, NetMsg>();

            //设置客户端套接字的日志输出接口
            client.SetLog(true, (string msg, int lv) =>
            {
                switch (lv)
                {
                    case 0:
                        msg = "Log:" + msg;
                        Debug.Log(msg);
                        break;
                    case 1:
                        msg = "Warn:" + msg;
                        Debug.LogWarning(msg);
                        break;
                    case 2:
                        msg = "Error:" + msg;
                        Debug.LogError(msg);
                        break;
                    case 3:
                        msg = "Info:" + msg;
                        Debug.Log(msg);
                        break;
                }
            });
            //启动客户端套接字
            client.StartAsClient(NetCfg.srvIP, NetCfg.srvPort);

            Debug.Log("网络通信服务初始化成功！");
        }
        /// <summary>
	    /// 发送消息
	    /// </summary>
	    /// <param name="msg"></param>
	    public void SendMsg(NetMsg msg)
        {
            if (client.session != null)
            {
                client.session.SendMsg(msg);
            }
            else
            {
                Debug.Log("服务器连接失败！");
                InitNet();//重新初始化网络服务模块
            }
        }

        /// <summary>
        /// 添加到网络消息队列
        /// </summary>
        /// <param name="msg">网络消息</param> 
        public static readonly string obj = "lock";
        private Queue<NetMsg> msgPackQue = new Queue<NetMsg>();
        public void AddMsgQue(NetMsg msg)
        {
            lock (obj)
            {
                msgPackQue.Enqueue(msg);
            }

        }

        /// <summary>
        /// 实时更新服务，不断分发消息
        /// </summary>
        public  void Update()
        {
            Debug.Log("更新中");
            if (msgPackQue.Count > 0)
            {
                Debug.Log("PackCount:" + msgPackQue.Count);//打印出收到信息的数量
                lock (obj)
                {
                    NetMsg msg = msgPackQue.Dequeue();//取出消息包
                    HandOutMsg(msg);//分发消息包
                }
            }
        }

        /// <summary>
        /// 分发网络消息
        /// </summary>
        /// <param name="msg">网络消息</param>
        private void HandOutMsg(NetMsg msg)
        {
            //出现错误码
            if (msg.err != (int)ErrorCode.None)
            {
                switch ((ErrorCode)msg.err)
                {
                    case ErrorCode.None:
                        break;
                    case ErrorCode.ServerDataError:
                        Debug.LogError("数据库和客户端数据不匹配");
                        //GameRoot.AddTips("服务器数据异常");
                        break;
                    case ErrorCode.ClientDataError:
                        Debug.LogError("数据库和客户端数据不匹配");
                        //GameRoot.AddTips("客户端数据异常");
                        break;
                    case ErrorCode.UpdateDBError:
                        Debug.LogError("数据库数据更新异常");
                        //GameRoot.AddTips("客户端网络不稳定");
                        break;
                    case ErrorCode.AcctIsOnline:
                        //GameRoot.AddTips("当前账号已经上线");
                        break;
                    case ErrorCode.WrongPass:
                        //GameRoot.AddTips("密码验证错误");
                        break;
                    default:
                        break;
                }
                return;
            }
            //其他消息
            else
            {
                switch ((CommondType)msg.cmd)
                {
                    case CommondType.RspLogin:
                        break;
                    case CommondType.RspRename://玩家重命名回复
                        break;
                    
                    default:
                        Debug.LogError("客户端接收到一条无法解析的消息");
                        break;
                }

            }
        }
        
    }
}

