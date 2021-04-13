/****************************************************
    文件：EditorUtil.cs
	作者：MRL Liu
    博客：https://blog.csdn.net/qq_41959920
    GitHub：https://github.com/MagicDeveloperDRL
    日期：2019/12/1 15:31:27
	功能：编辑器工具
*****************************************************/

using UnityEngine;

namespace MSingletonFQ
{
    public class EditorUtil
    {
#if UNITY_EDITOR
        /// <summary>
        /// 打开指定文件夹
        /// </summary>
        public static void OpenInFolder(string folderPath)
        {
            Application.OpenURL("file:///" + folderPath);
        }


        /// <summary>
        /// 导出资源包
        /// </summary>
        public static void ExportPackage(string assetPathName, string fileName)
        {
            UnityEditor.AssetDatabase.ExportPackage(assetPathName, fileName, UnityEditor.ExportPackageOptions.Recurse);
        }

        
        /// <summary>
        /// 调用编辑器菜单命令
        /// </summary>
        public static void CallMenuItem(string menuName)
        {
            UnityEditor.EditorApplication.ExecuteMenuItem(menuName);
        }
#endif
    }
}

