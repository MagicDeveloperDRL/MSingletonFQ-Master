/****************************************************
    文件：AudioSvc.cs
    作者：MRL Liu
    博客：https://blog.csdn.net/qq_41959920
    GitHub：https://github.com/MagicDeveloperDRL
    日期：2019/12/2 10:53:45
    功能：声音管理服务类
*****************************************************/

using UnityEngine;
namespace MSingletonFQ
{
  public class AudioMgr : BaseManager<AudioMgr>
  {
        private AudioListener mAudioListener = null;//音乐监听器
        public AudioSource mBGAudioSource = null;//背景音乐播放音源
        public AudioSource mUIAudioSource = null;//UI音效播放器
        /// <summary>
        /// 初始化服务
        /// </summary>
        /// <param name="svcMgr"></param>
        public override void InitMgr()
        {
            base.InitMgr();
            CheckAudioListener();
            Debug.Log("音效控制服务初始化成功！");
        }


        #region 外部接口
        #region 背景音乐
        /// <summary>
        /// 播放音乐
        /// </summary>
        public void PlayBGMusic(string musicPath, bool isLoop = true)
        {
            AudioClip music =ResMgr.Instance.audioPool.Load(musicPath);
            //如果当前已经在播放或者要切换同一个音效
            if (mBGAudioSource.clip==null||mBGAudioSource.clip.name!=music.name)
            {
                mBGAudioSource.clip = music;
                mBGAudioSource.loop = isLoop;
                mBGAudioSource.Play();
                Debug.Log("开始播放背景音乐");
            }
           
        }
        /// <summary>
        /// 停止音乐
        /// </summary>
        public void StopBGMusic()
        {
            mBGAudioSource.Stop();
        }
        /// <summary>
        /// 暂停音乐
        /// </summary>
        public void PauseBGMusic()
        {
            mBGAudioSource.Pause();
        }
        /// <summary>
        /// 继续音乐
        /// </summary>
        public void ResumeBGMusic()
        {
            mBGAudioSource.UnPause();
        }
        /// <summary>
        /// 音乐关闭
        /// </summary>
        public void BGMusicOff()
        {
            mBGAudioSource.Pause();
            mBGAudioSource.mute = true;
        }
        /// <summary>
        /// 音乐打开
        /// </summary>
        public void BGMusicOn()
        {
            mBGAudioSource.UnPause();
            mBGAudioSource.mute = false;
        }
        #endregion

        #region UI音效
        /// <summary>
        /// 播放音效
        /// </summary>
        public void PlayUISound(string soundPath)
        {
            CheckAudioListener();
            
            mUIAudioSource.clip = ResMgr.Instance.audioPool.Load(soundPath);
            mUIAudioSource.Play();
        }
        /// <summary>
        /// 音效关闭
        /// </summary>
        public void UISoundOff()
        {
            mUIAudioSource.Pause();
            mUIAudioSource.mute = true;
        }
        /// <summary>
        /// 音效打开
        /// </summary>
        public void UISoundOn()
        {
            mUIAudioSource.UnPause();
            mUIAudioSource.mute = false;
        }
        #endregion

        #region 3D音效
        /// <summary>
	    /// 播放三维场景中的音效
	    /// </summary>
	    public void Play3DSound(string soundPath, AudioSource audio)
        {
            CheckAudioListener();
            audio.clip = ResMgr.Instance.audioPool.Load(soundPath);
            audio.Play();
        }
        #endregion

        #endregion


        #region 内部实现
        /// <summary>
        /// 检查声音监听器是否存在
        /// </summary>
        private void CheckAudioListener()
        {
            if (mAudioListener == null)
            {
                mAudioListener = gameObject.GetComponent<AudioListener>();
                if (mAudioListener==null)
                {
                    mAudioListener = gameObject.AddComponent<AudioListener>();
                }
            }
        }
        
        #endregion
    }
}

