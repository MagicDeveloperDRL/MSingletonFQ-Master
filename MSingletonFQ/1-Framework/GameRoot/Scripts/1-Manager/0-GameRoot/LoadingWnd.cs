/****************************************************
	文件：LoadingWnd.cs
	作者：MRL Liu
    博客：https://blog.csdn.net/qq_41959920
    GitHub：https://github.com/MagicDeveloperDRL
	日期：2019/10/19 20:1:28
	功能：加载进度界面
*****************************************************/
using UnityEngine;
using UnityEngine.UI;
namespace MSingletonFQ
{	

	public class LoadingWnd : BaseWindow
    {
	    public Text tipsText;//提示文本
	    public Text txtPro;//进度百分比
	    public Image ImgFg;//进度条前景图
	    public Image ImgFgPoint;//当前进度点
	
	    private float ImgFgWidth;//前景图的宽度

        /// <summary>
        /// 初始化进度条窗口（重载）
        /// </summary>
        public override void OnEnter() {
	        base.OnEnter();//调用基类方法
	
	        ImgFgWidth = ImgFg.GetComponent<RectTransform>().sizeDelta.x;
	        SetText(tipsText, "这是一条提示信息");
	        SetText(txtPro, "0%");
	        ImgFg.fillAmount = 0;//进度为0
	        ImgFgPoint.transform.position = new Vector3(-ImgFgWidth/2,0,0);
	    }
	    
	    /// <summary>
	    /// 设置进度条的当前进度
	    /// </summary>
	    /// <param name="pro">0-1</param>
	    public void SetProgress(float pro) {
	        SetText(txtPro, (int)(pro * 100) + "%");
	        ImgFg.fillAmount = pro;
	        float posX = pro*ImgFgWidth-ImgFgWidth/2;
	        ImgFgPoint.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX,0);
	    }
	}
}
