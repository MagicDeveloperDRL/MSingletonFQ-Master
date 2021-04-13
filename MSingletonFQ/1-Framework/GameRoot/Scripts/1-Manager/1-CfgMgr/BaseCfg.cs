/****************************************************
    文件：PlayerCfg.cs
    作者：MRL Liu
    博客：https://blog.csdn.net/qq_41959920
    GitHub：https://github.com/MagicDeveloperDRL
    日期：2020/1/16 0:52:41
    功能：玩家基础配置
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace MSingletonFQ
{
    //配置数据基类
    public abstract class BaseCfg
    {
        public int ID;//配置编号

        public abstract void switchParseData(XmlElement e);
    }

    //玩家基础数据类
    public class PlayerCfg:BaseCfg {
        public string userName;//用户账号
        public string passWord;//用户密码
        public string playerName;//玩家姓名
        /// <summary>
        /// 转化数据
        /// </summary>
        public override void switchParseData(XmlElement e)
        {
            switch (e.Name)
            {
                case "userName":
                    userName = e.InnerText;
                    break;
                case "passWord":
                    passWord = e.InnerText;
                    break;
                case "playerName":
                    playerName = e.InnerText;
                    break;

                default:
                    break;
            }
        }
    }

    //场景地图配置数据类
    public class MapCfg : BaseCfg
    {
        public string mapName;//地图名称
        public string sceneName;
        public int power;
        public Vector3 mainCamPos;//主摄像机位置
        public Vector3 mainCamRote;//主摄像机角度
        public Vector3 playerBornPos;//玩家出生位置
        public Vector3 playerBornRote;//玩家出生角度
        public Vector3 playerBornScale;//玩家出生比例
        //public List<MonsterDataCfg> monsterLst;//该场景地图生成的怪物

        public int coin;
        public int exp;
        public int crystal;
        //转化数据
        public override void switchParseData(XmlElement e)
        {
            switch (e.Name)
            {
                case "mapName":
                    mapName = e.InnerText;
                    break;
                case "sceneName":
                    sceneName = e.InnerText;
                    break;
                case "power":
                    power = int.Parse(e.InnerText);
                    break;
                case "mainCamPos":
                    string[] valArr = e.InnerText.Split(',');
                    mainCamPos = new Vector3(float.Parse(valArr[0]), float.Parse(valArr[1]), float.Parse(valArr[2]));
                    break;
                case "mainCamRote":
                    valArr = e.InnerText.Split(',');
                    mainCamRote = new Vector3(float.Parse(valArr[0]), float.Parse(valArr[1]), float.Parse(valArr[2]));
                    break;
                case "playerBornPos":
                    valArr = e.InnerText.Split(',');
                    playerBornPos = new Vector3(float.Parse(valArr[0]), float.Parse(valArr[1]), float.Parse(valArr[2]));
                    break;
                case "playerBornRote":
                    valArr = e.InnerText.Split(',');
                    playerBornRote = new Vector3(float.Parse(valArr[0]), float.Parse(valArr[1]), float.Parse(valArr[2]));
                    break;
                case "playerBornScale":
                    valArr = e.InnerText.Split(',');
                    playerBornScale = new Vector3(float.Parse(valArr[0]), float.Parse(valArr[1]), float.Parse(valArr[2]));
                    break;
                case "monsterLst":
                    /*valArr = e.InnerText.Split('#');
                    for (int waveIndex = 0; waveIndex < valArr.Length; waveIndex++)
                    {
                        if (waveIndex == 0)
                        {
                            continue;
                        }
                        string[] tempArr = valArr[waveIndex].Split('|');
                        for (int j = 0; j < tempArr.Length; j++)
                        {
                            if (j == 0)
                            {
                                continue;
                            }
                            string[] arr = tempArr[j].Split(',');
                            MonsterDataCfg data = new MonsterDataCfg
                            {
                                ID = int.Parse(arr[0]),
                                mWave = waveIndex,
                                mIndex = j,
                                mLevel = int.Parse(arr[5]),
                                monsterCfg = GetMonsterCfg(int.Parse(arr[0])),
                                mBornPos = new Vector3(float.Parse(arr[1]), float.Parse(arr[2]), float.Parse(arr[3])),
                                mBornRote = new Vector3(0, float.Parse(arr[4]), 0)
                            };
                            mapCfg.monsterLst.Add(data);
                        }
                    }*/
                    break;
                case "coin":
                    coin = int.Parse(e.InnerText);
                    break;
                case "exp":
                    exp = int.Parse(e.InnerText);
                    break;
                case "crytal":
                    crystal = int.Parse(e.InnerText);
                    break;
                default:
                    break;
            }
        }
    }


}