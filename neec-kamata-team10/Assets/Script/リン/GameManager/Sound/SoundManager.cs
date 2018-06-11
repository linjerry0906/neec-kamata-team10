//------------------------------------------------------
// 作成日：2018.6.08
// 作成者：林 佳叡
// 内容：音声を管理するクラス
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [SerializeField]
    private List<AudioClip> sesSource;              //効果音源
    [SerializeField]
    private GameObject soundBuffer;                 //Prefab

    private List<GameObject> buffers;               //Sound Buffer
    private string currentBgm = "";                 //現在の音楽

    private float maxVolume = 1.0f;                 //最大音量

    private void Start()
    {
        buffers = new List<GameObject>();
    }

    private void Update()
    {
        foreach (GameObject buffer in buffers)
        {
            if (!buffer)                                                //削除された場合
                continue;

            SoundBuffer sound = buffer.GetComponent<SoundBuffer>();
            if (sound.CurrentState() != FadeState.None)
                sound.UpdateFade();
        }

        buffers.RemoveAll(g => g == null);
    }

    /// <summary>
    /// BGMをPlayする
    /// </summary>
    /// <param name="bgm">BGM</param>
    public void PlayBGM(AudioClip bgm, FadeState state, float fadeSpeed, bool loop, float startVolume = 0.0f)
    {
        if (currentBgm.Equals(bgm.name))
            return;

        FadeOutOtherBGM();                                              //他のBGMをFadeOut

        GameObject buffer = Instantiate(soundBuffer, transform);        //新しいBuffer作成
        buffer.GetComponent<SoundBuffer>().Initialize();
        buffer.GetComponent<SoundBuffer>().SetState(state);             //Fade状態設定
        buffer.GetComponent<SoundBuffer>().SetVolume(startVolume);      //StartVolume設定
        buffer.GetComponent<SoundBuffer>().SetMaxVolume(maxVolume);     //最大音量設定
        buffer.GetComponent<SoundBuffer>().SetFadeSpeed(fadeSpeed);     //FadeSpeedを設定
        buffer.GetComponent<SoundBuffer>().SetClip(bgm);                //音源設定
        buffers.Add(buffer);                                            //Listに追加
        currentBgm = bgm.name;

        buffer.GetComponent<SoundBuffer>().Play(loop);
    }

    /// <summary>
    /// 他のバッファをFadeOut
    /// </summary>
    private void FadeOutOtherBGM()
    {
        if (buffers.Count <= 0)
            return;
        foreach (GameObject buffer in buffers)
        {
            SoundBuffer sound = buffer.GetComponent<SoundBuffer>();
            sound.SetState(FadeState.FadeOut);
        }
    }

    /// <summary>
    /// 全体のBGM音量を設定
    /// </summary>
    public void SetMaxVolume(float maxVolume)
    {
        this.maxVolume = maxVolume;
        foreach (GameObject buffer in buffers)
        {
            SoundBuffer sound = buffer.GetComponent<SoundBuffer>();
            if (sound.CurrentState() == FadeState.FadeOut)         //FadeOut処理はそのまま
                continue;

            sound.SetMaxVolume(maxVolume);                          //最大音量設定
            if (maxVolume == 1.0f)
            {
                sound.SetState(FadeState.FadeIn);                  //最大の場合はFadeInを使う
                continue;
            }
            sound.SetState(FadeState.LerpToMax);
        }
    }

    /// <summary>
    /// 音量最大値
    /// </summary>
    /// <returns></returns>
    public float MaxVolume()
    {
        return maxVolume;
    }
}
