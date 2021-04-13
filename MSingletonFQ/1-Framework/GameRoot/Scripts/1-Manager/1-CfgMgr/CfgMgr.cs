/****************************************************
    文件：CfgMgr.cs
    作者：MRL Liu
    博客：https://blog.csdn.net/qq_41959920
    GitHub：https://github.com/MagicDeveloperDRL
    日期：2020/1/16 0:27:42
    功能：配置管理服务类
*****************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace MSingletonFQ
{
    
    public class CfgMgr : BaseManager<CfgMgr>
    {
       
        /// <summary>
        /// 初始化服务
        /// </summary>
        public override void InitMgr()
        {
            base.InitMgr();
            LoadMapXml(PathDefine.MapCfgPath);
            /*Debug.Log("####"+ playerCfgDic.Count);
            PlayerCfg cfg = GetPlayerCfg(1011);
            Debug.Log("####" + cfg.userName);
            Debug.Log("####" + cfg.passWord);
            Debug.Log("####" + cfg.playerName);
            Debug.Log("####" + playerCfgDic.Count);
            cfg = GetPlayerCfg(1012);
            Debug.Log("####" + cfg.userName);
            Debug.Log("####" + cfg.passWord);
            Debug.Log("####" + cfg.playerName);*/
            Debug.Log("配置管理服务初始化成功！");
        }

        /// <summary>
        /// 导入XML配置文件
        /// </summary>
        private Dictionary<int, PlayerCfg> playerCfgDic = new Dictionary<int, PlayerCfg>();
        private void LoadPlayerXml(string path)
        {
            //加载配置文件
            TextAsset xml = Resources.Load<TextAsset>(path);
            if (xml == null)
            {
                Debug.LogError("XML配置文件（" + path + ")不存在");
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml.text);
                //解析配置文件
                XmlNodeList nodLst = doc.SelectSingleNode("root").ChildNodes;
                for (int i = 0; i < nodLst.Count; i++)
                {
                    XmlElement ele = nodLst[i] as XmlElement;//将某一个节点转化为一个XmlElement:
                    if (ele.GetAttributeNode("ID") == null)
                    {
                        continue;
                    }
                    int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
                    PlayerCfg cfg = new PlayerCfg();
                    cfg.ID = ID;
                    foreach (XmlElement e in nodLst[i].ChildNodes)
                    {
                        cfg.switchParseData(e);
                    }
                    playerCfgDic.Add(cfg.ID, cfg);//将配置缓存起来
                }
                Debug.Log("加载XML配置文件（" + path + ")成功");
            }
        }
        public PlayerCfg GetPlayerCfg(int id) {
            PlayerCfg cfg = null;
            playerCfgDic.TryGetValue(id, out cfg);
            if (cfg==null)//如果是第一次加载
            {
                LoadPlayerXml(PathDefine.PlayerCfgPath);//加载XML
                playerCfgDic.TryGetValue(id, out cfg);
            }
            return cfg;
        }

        private Dictionary<int, MapCfg> mapCfgDic = new Dictionary<int, MapCfg>();
        private void LoadMapXml(string path)
        {
            //加载配置文件
            TextAsset xml = Resources.Load<TextAsset>(path);
            if (xml == null)
            {
                Debug.LogError("XML配置文件（" + path + ")不存在");
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml.text);
                //解析配置文件
                XmlNodeList nodLst = doc.SelectSingleNode("root").ChildNodes;
                for (int i = 0; i < nodLst.Count; i++)
                {
                    XmlElement ele = nodLst[i] as XmlElement;//将某一个节点转化为一个XmlElement:
                    if (ele.GetAttributeNode("ID") == null)
                    {
                        continue;
                    }
                    int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
                    MapCfg cfg = new MapCfg();
                    cfg.ID = ID;
                    foreach (XmlElement e in nodLst[i].ChildNodes)
                    {
                        cfg.switchParseData(e);
                    }
                    mapCfgDic.Add(cfg.ID, cfg);//将配置缓存起来
                }
                Debug.Log("加载XML配置文件（" + path + ")成功");
            }
        }
        public MapCfg GetMapCfg(int id)
        {
            MapCfg cfg = null;
            mapCfgDic.TryGetValue(id, out cfg);
            if (cfg == null)//如果是第一次加载
            {
                LoadPlayerXml(PathDefine.MapCfgPath);//加载XML
                mapCfgDic.TryGetValue(id, out cfg);
            }
            return cfg;
        }
    }
}