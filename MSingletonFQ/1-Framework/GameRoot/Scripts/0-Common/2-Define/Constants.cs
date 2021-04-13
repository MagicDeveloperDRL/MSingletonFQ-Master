/****************************************************
    文件：Constant.cs
    作者：MRL Liu
    博客：https://blog.csdn.net/qq_41959920
    GitHub：https://github.com/MagicDeveloperDRL
    日期：2019/11/29 23:11:31
    功能：项目中的常量定义
*****************************************************/

using UnityEngine;
namespace MSingletonFQ
{
  public class Constants{

        public const string UIRootName = "Canvas";//UI根节点的名称
        //屏幕
        public const int ScreenStandomWidth = 1334;//屏幕标准宽度
        public const int ScreenStandomHeight = 750;//屏幕标准高度
        public const int MaxPointLen = 100;//操控杆距离操控盘中心的最大移动距离
        //玩家角色属性
        public const int PlayerMoveSpeed = 8;//玩家移动倍数
        public const float AccelerSpeed = 5;//待机和移动动画混合倍数
        public const float AniBlendIdle = 0;//待机动画混合参数
        public const float AniBlendMove = 1;//移动动画混合参数
        public const int AniActionDefault = -1;//非攻击动画
        public const int AniActionMonsterBorn = 0;//怪物出生动画
        public const int AniActionMonsterHit = 101;//怪物受击打动画s
        public const int AniActionMonsterDie = 100;//怪物死亡动画
        public const int ComboSpace = 500;//普招连攻的时间间隔
    }
}

