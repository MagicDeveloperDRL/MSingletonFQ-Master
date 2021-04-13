/****************************************************
    文件：ResMgr.cs
    作者：MRL Liu
    博客：https://blog.csdn.net/qq_41959920
    GitHub：https://github.com/MagicDeveloperDRL
    日期：2019/11/29 23:20:10
    功能：资源加载服务类
*****************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace MSingletonFQ
{

    public class ResMgr : BaseManager<ResMgr>
    {
        //全局的资源对象池(可以确保对象池中只存在一份资源)
        public ResPool<AudioClip> audioPool = new ResPool<AudioClip>();
        public ResPool<GameObject> prefabPool = new ResPool<GameObject>();
        public ResPool<Sprite> spritePool = new ResPool<Sprite>();


        /// <summary>
        /// 初始化服务模块
        /// </summary>
        public override void InitMgr()
        {
            base.InitMgr();
            Debug.Log("资源加载服务初始化成功！");
        }

        /// <summary>
        /// 实时更新
        /// </summary>
        public  void Update()
        {
            //如果加载新的场景事件不为空，则加载场景
            if (proCB != null)
            {
                proCB();
            }
        }


        #region 游戏场景切换的接口

        /// <summary>
        /// 异步加载场景接口
        /// </summary>
        /// <param name="sceneName">加载场景名称</param>
        /// <param loaded="loaded">加载完成后委托事件</param>
        /// <param loaded="loaded">开始加载前委托事件</param>
        /// <param loaded="loaded">正在加载中委托事件</param>
        private Action proCB = null;//定义委托事件
        public void AsyncLoadScene(string sceneName, Action loaded, Action beforeLoad = null, Action loading = null)
        {
            if (beforeLoad != null)
            {
                beforeLoad();//加载前的事件
            }
            GameRoot.Instance.loadingWnd.gameObject.SetActive(true);
            AsyncOperation asyncOper = SceneManager.LoadSceneAsync(sceneName);//开启异步加载场景
            //设置委托事件
            proCB = () =>
            {
                float val = asyncOper.progress;//获取异步加载的进度
                GameRoot.Instance.loadingWnd.SetProgress(val);
                if (loading != null)
                {
                    loading();//加载中的事件
                }
                //如果加载完成
                if (val == 1)
                {
                    proCB = null;//委托设置为空，防止继续执行
                    asyncOper = null;//清除资源
                    GameRoot.Instance.loadingWnd.gameObject.SetActive(false);
                    if (loaded != null)
                    {
                        loaded();//加载完成后的事件
                    }
                }
            };
        }
       

        #endregion


        #region 游戏资源加载的接口
        /// <summary>
        /// 加载并且实例化预设体
        /// </summary>
        public GameObject LoadAndIntantiatePrefab(string path) {
            GameObject prefab = prefabPool.Load(path);//向对象池中加载预设体
            if (prefab)
            {
                //将预设体进行实例化
                GameObject go = null;
                if (prefab != null)
                {
                    go = GameObject.Instantiate(prefab);
                }
                return go;
            }
            return null;
           
        }
        /// <summary>
        /// 加载精灵图片
        /// </summary>
        /// <param name="path"></param>
        /// <param name="isCache"></param>
        /// <returns></returns>
        public Sprite LoadSprite(string path, bool isCache = true) {
            return spritePool.Load(path,isCache);
        }
        #endregion
        
   }

    
}

