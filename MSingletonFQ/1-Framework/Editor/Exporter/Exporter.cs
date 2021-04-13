/****************************************************
    文件：Exporter.cs
	作者：MRL Liu
    博客：https://blog.csdn.net/qq_41959920
    GitHub：https://github.com/MagicDeveloperDRL
    日期：2019/12/1 15:31:27
	功能：导出器
*****************************************************/
using System;
using System.IO;

using UnityEngine;
using UnityEditor;


namespace MSingletonFQ
{
    public class Exporter
    {
        
        
        [MenuItem("MSingletonFQ/Editor/导出资源包/ 导出MSingletonFQ为UnityPackage ", false, 0)]
        private static void ExportMFramework()
        {
            EditorUtil.ExportPackage("Assets/MSingletonFQ", GeneratePackageName() + ".unitypackage");//导出指定包
            EditorUtil.OpenInFolder(Path.Combine(Application.dataPath, "../"));//打开保存的文件夹
        }

        [MenuItem("MSingletonFQ/Editor/导出资源包/ 导出选中的文件为UnityPackage %e", false, 1)]
        private static void ExportSelectedFile()
        {
            string[] strs = Selection.assetGUIDs;//获取选中的资源信息
            string path = AssetDatabase.GUIDToAssetPath(strs[0]);//将资源信息转化为资源路径
            Debug.Log("选中的文件路径:" + path);
            string[] fileNames = path.Split('/');
            if (path.EndsWith(".cs"))
            {
                Debug.LogError("默认不可以单独输出脚本文件"+ fileNames[fileNames.Length - 1] + "，请选中其他资源");
                return;
            }
            string packageName = fileNames[fileNames.Length - 1] + DateTime.Now.ToString("_yyyyMMdd_HHmm");//导出资源包名字
            EditorUtil.ExportPackage(path, packageName + ".unitypackage");//导出指定包
            EditorUtil.OpenInFolder(Path.Combine(Application.dataPath, "../"));//打开保存的文件夹
            Debug.Log("导出资源包:"+packageName);
           
        }

        /// <summary>
        /// 根据时间自动生成包的名字
        /// </summary>
        private static string GeneratePackageName()
        {
            return "MSingletonFQ_" + DateTime.Now.ToString("yyyyMMdd_HHmm");
        }
    }
}

