/****************************************************
    文件：EnumDefine.cs
    作者：MRL Liu
    博客：https://blog.csdn.net/qq_41959920
    GitHub：https://github.com/MagicDeveloperDRL
    日期：2019/11/29 23:12:30
    功能：枚举类型
*****************************************************/

using UnityEngine;
namespace MSingletonFQ
{
    /// <summary>
    /// 项目运行环境类型
    /// </summary>
    public enum EnvironmentMode
    {
        Developing,//开发阶段
        Testing,//测试阶段
        Production//发布阶段
    }
    
    /// <summary>
    /// 项目中的业务系统类型
    /// </summary>
    public enum SystemType
    {
        None,//根节点初始化模块
        LoginSystem,//登录模块
        //MainCitySystem,//主城模块
        //BattleSystem,//战斗系统模块
    }
   
}

