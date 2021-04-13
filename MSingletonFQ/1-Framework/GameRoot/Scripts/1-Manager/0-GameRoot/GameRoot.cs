/****************************************************
    文件：GameRoot.cs
    作者：MRL Liu
    博客：https://blog.csdn.net/qq_41959920
    GitHub：https://github.com/MagicDeveloperDRL
    日期：2019/11/29 22:17:55
    功能：游戏根节点
*****************************************************/

using UnityEngine;
namespace MSingletonFQ
{
    public class GameRoot : MonoBehaviour
    {
        public static GameRoot Instance = null;//单例模式
        [Header("[运行模式]")]
        public EnvironmentMode environmentMode=EnvironmentMode.Developing;//运行环境类型，外部面板设置变量（如果是static则无法外部设置）
        [Tooltip("测试模式下该设置起作用")]
        public SystemType testSystemType = SystemType.None;//要测试的业务系统类型
        [Header("[基本窗口]")]
        public DynamicWnd dynamicWnd=null;//加载进度界面
        public LoadingWnd loadingWnd=null;//加载进度界面
        [Header("[基本节点]")]
        public GameObject managerRoot=null;
        public GameObject systemRoot=null;
        // 启动游戏
        private void Start()
        {
            DontDestroyOnLoad(this);//不要跨场景销毁
            Instance = this;
            Debug.Log("游戏客户端启动....");
            //清理所有UI窗口
            ClearUIRoot();
            //显示信息提示窗口
            dynamicWnd.gameObject.SetActive(true);
            Debug.Log("显示动态窗口");
            //初始化基础节点
            if (managerRoot!=null)
            {
                managerRoot.GetComponent<CfgMgr>().InitMgr();
                managerRoot.GetComponent<ResMgr>().InitMgr();
                managerRoot.GetComponent<TimerMgr>().InitMgr();
                managerRoot.GetComponent<AudioMgr>().InitMgr();
                managerRoot.GetComponent<WndMgr>().InitMgr();
                //managerRoot.GetComponent<NetMgr>().InitMgr();
            }
            if (systemRoot!=null)
            {
                systemRoot.GetComponent<LoginSystem>().InitSys();
            }
            
            
            Debug.Log("【初始化工作完成】");
            SwitchEnvironmentMode();//切换运行环境模式，并启动运行
        }
        // 实时更新
        private void Update()
        {
            //serviceManager.UpdateAllService();//实时更新所有服务
        }
        // 卸载游戏
        private void OnDestroy()
        {
            //serviceManager.DestroyAllService();//卸载所有服务
        }

 #region 启动与清理
        /// <summary>
        /// 开发模式下运行
        /// </summary>
        protected  void LaunchInDevelopingMode()
        {
            Debug.Log("【开发模式下运行】");
            EnterSystem(SystemType.LoginSystem);//进入登录系统
        }
        /// <summary>
        /// 测试模式下运行
        /// </summary>
        protected  void LaunchInTestingMode()
        {
            Debug.Log("【测试模式下运行】");
            EnterSystem();//进入测试的业务系统
        }
        /// <summary>
        /// 发布模式下运行
        /// </summary>
        protected  void LaunchInProductionMode()
        {
            Debug.Log("【发布模式下运行】");
            Debug.unityLogger.logEnabled = false;//关闭日志打印的开销
            EnterSystem(SystemType.LoginSystem);//进入登录系统
        }
        /// <summary>
        /// 确保所有窗口初始化前处于正常的激活状态
        /// </summary>
        private void ClearUIRoot()
        {
            Transform canvas = transform.Find(Constants.UIRootName);
            if (canvas != null)
            {
                for (int i = 0; i < canvas.childCount; i++)
                {
                    canvas.GetChild(i).gameObject.SetActive(false);
                }
            }
            Debug.Log("清理UI窗口成功");
        }
        /// <summary>
        /// 切换运行环境模式
        /// </summary>
        private void SwitchEnvironmentMode()
        {
#if !UNITY_EDITOR
                environmentMode=EnvironmentMode.Production; //如果发布游戏则自动切换成发布模式
#endif
            switch (environmentMode)
            {
                case EnvironmentMode.Developing:
                    LaunchInDevelopingMode();
                    break;
                case EnvironmentMode.Testing:
                    LaunchInTestingMode();
                    break;
                case EnvironmentMode.Production:
                    LaunchInProductionMode();
                    break;
                default:
                    break;
            }
        }
        #endregion

#region 外部接口
        /// <summary>
        /// 进入某个业务模块
        /// </summary>
        public void EnterSystem(SystemType type=SystemType.None) {
            if (type!=SystemType.None)
            {
                testSystemType = type;
            }
            switch (testSystemType)
            {
                case SystemType.None:
                    break;
                case SystemType.LoginSystem:
                    LoginSystem.Instance.EnterSystem();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 添加提示信息
        /// </summary>
        public void AddTips(string txtTips) {
            if (dynamicWnd!=null)
            {
                dynamicWnd.AddTips(txtTips);
            }
            else
            {
                Debug.LogError("dynamicWnd为空");
            }
        }
#endregion
       


    }
}

