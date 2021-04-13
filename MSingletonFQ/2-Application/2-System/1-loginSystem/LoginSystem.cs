/****************************************************
    文件：LoginSystem.cs
    作者：MRL Liu
    博客：https://blog.csdn.net/qq_41959920
    GitHub：https://github.com/MagicDeveloperDRL
    日期：2019/12/1 15:31:27
    功能：注册登陆业务系统
*****************************************************/

using UnityEngine;
using System;
namespace MSingletonFQ
{
  public class LoginSystem : BaseSystem<LoginSystem> {

        public override void EnterSystem()
        {
            Debug.Log("进入注册登录业务系统");
            resMgr.AsyncLoadScene("LoginScene",()=>{

            });//导入场景
        }

        public override void ExitSystem()
        {
            Debug.Log("退出注册登录业务系统"); ;
        }
    }
}

