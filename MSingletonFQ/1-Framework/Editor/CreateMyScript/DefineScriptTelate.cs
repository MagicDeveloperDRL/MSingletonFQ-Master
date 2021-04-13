/****************************************************
    文件：DefineScriptTelate.cs
    作者：MRL Liu
    博客：https://blog.csdn.net/qq_41959920
    GitHub：https://github.com/MagicDeveloperDRL
    日期：2019/12/1 15:31:27
    功能：自定义脚本模版类（根据模版进行替换）
*****************************************************/
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Text;
using UnityEditor.ProjectWindowCallback;
using System.Text.RegularExpressions;

#if UNITY_EDITOR
using ADB = UnityEditor.AssetDatabase;
using System.Linq;
#endif
namespace MSingletonFQ
{
    public class DefineScriptTelate
    {
        //脚本模板所目录Script_04_01
        private const string MY_SCRIPT_DEFAULT_PATH = "ScriptTemplates/C# Script-MyNewBehaviourScript.cs.txt";

        [MenuItem("Assets/Create/C# MyScript", false, 80)]
        public static void CreatMyScript()
        {
            //获取当前脚本的所在路径
            string path = ADB.FindAssets("t:Script").Where(v => Path.GetFileNameWithoutExtension(ADB.GUIDToAssetPath(v)) == "DefineScriptTelate")
               .Select(id => ADB.GUIDToAssetPath(id))
               .FirstOrDefault()
               .ToString();
            //拼接模版文件所在路径
            path = path.Replace("DefineScriptTelate.cs", MY_SCRIPT_DEFAULT_PATH);
            //判断模版文件是否存在
            if (File.Exists(path))
            {
                //Debug.Log("存在:" + path);
                //存在则创建脚本
                string locationPath = GetSelectedPathOrFallback();
                ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
                ScriptableObject.CreateInstance<MyDoCreateScriptAsset>(),
                locationPath + "/MyNewBehaviourScript.cs",
                null, path);
            }
            else {
                Debug.LogError("不存在:" + path);
                return;
            }
            
        }

        public static string GetSelectedPathOrFallback()
        {
            string path = "Assets";
            foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetPath(obj);
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    path = Path.GetDirectoryName(path);
                    break;
                }
            }
            return path;
        }
    }


    class MyDoCreateScriptAsset : EndNameEditAction
    {

        public override void Action(int instanceId, string pathName, string resourceFile)
        {
            UnityEngine.Object o = CreateScriptAssetFromTemplate(pathName, resourceFile);
            ProjectWindowUtil.ShowCreatedAsset(o);
        }
        /// <summary>
        /// 按照模版中创建脚本
        /// </summary>
        internal static UnityEngine.Object CreateScriptAssetFromTemplate(string pathName, string resourceFile)
        {
            string fullPath = Path.GetFullPath(pathName);
            StreamReader streamReader = new StreamReader(resourceFile);
            string text = streamReader.ReadToEnd();
            streamReader.Close();
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(pathName);
            //替换文件名
            text = Regex.Replace(text, "#NAME#", fileNameWithoutExtension);//替换类名
            text = text.Replace("#SCRIPTNAME#", fileNameWithoutExtension);//替换注释中的文件名
            text = text.Replace("#CreateAuthor#", Environment.UserName);//替换注释中的作者名
            text = text.Replace("#CreateTime#", string.Concat(DateTime.Now.Year, "/", DateTime.Now.Month, "/",
                                        DateTime.Now.Day, " ", DateTime.Now.Hour, ":", DateTime.Now.Minute, ":", DateTime.Now.Second));//替换注释中的创建日期
            bool encoderShouldEmitUTF8Identifier = true;
            bool throwOnInvalidBytes = false;
            UTF8Encoding encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier, throwOnInvalidBytes);

            bool append = false;

            StreamWriter streamWriter = new StreamWriter(fullPath, append, encoding);
            streamWriter.Write(text);
            streamWriter.Close();

            AssetDatabase.ImportAsset(pathName);
            return AssetDatabase.LoadAssetAtPath(pathName, typeof(UnityEngine.Object));
        }
    }  
}