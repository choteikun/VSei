using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceController : MonoBehaviour
{
    public static Dictionary<string, AudioClip> audioDic = new Dictionary<string, AudioClip>();
    public static float volumeAllScale;

    /// <summary>
    /// 需要播放某個音效的時候需要呼叫此方法就可以了
    /// </summary>
    /// <param name="dir">這是你音效的路徑, 必須在Resources目錄下</param>
    /// <param name="name">音效的名稱</param>
    public static void PlaySE(string dir, string name)
    {
        AudioClip clip = LoadClip(dir, name.ToLower());
        if (clip != null)
            AudioSource.PlayClipAtPoint(clip, Vector3.zero, volumeAllScale);
        else
            Debug.LogError("Clip is Missing" + name);
    }
    public static AudioClip LoadClip(string dir, string name)
    {
        if (!audioDic.ContainsKey(name))
        {
            string dirMusic = dir + "/" + name;
            AudioClip clip = Resources.Load(dirMusic) as AudioClip;
            if (clip != null)
                audioDic.Add(clip.name, clip);
        }
        return audioDic[name];
    }

    ////呼叫測試
    //private void AudioSourceShow()
    //{
    //    //在其他類裡面呼叫的時候只需要類名點這個靜態方法
    //    //目前的音樂檔案放在(Resources/Cho_Sounds)目錄下，檔名為OnClick，
    //    AudioSourceController.PlaySE("Cho_Sounds", "Choose");  //(此音效播放完會自動刪除)
    //}
}
