//------------------------------------------------------
// 作成日：2018.6.08
// 作成者：林 佳叡
// 内容：音声を流すクラス
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBuffer : MonoBehaviour
{
    private AudioSource audioSource;
    private FadeState state;               //Fade状態
    private float fadeSpeed = 0.01f;        //Fade速度
    private float maxVolume = 1.0f;         //最大音量

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        state = FadeState.None;
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 音量を更新
    /// </summary>
    /// <param name="amount"></param>
    public void UpdateFade()
    {
        if (state == FadeState.LerpToMax)
        {
            LerpToMax();
            return;
        }
        if (state == FadeState.FadeIn)
        {
            FadeIn(fadeSpeed);
            return;
        }
        FadeOut(fadeSpeed);
    }

    /// <summary>
    /// 音の大きさを設定
    /// </summary>
    /// <param name="volume">大きさ</param>
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    /// <summary>
    /// FadeInする
    /// </summary>
    /// <param name="amount">Fadeする量</param>
    private void FadeIn(float amount)
    {
        float volume = audioSource.volume;
        if (volume >= maxVolume)                 //完全にFadein
        {
            state = FadeState.None;
            return;
        }

        volume += amount;                   //Fade処理
        volume = Mathf.Min(volume, 1.0f);
        audioSource.volume = volume;        //適用
    }

    /// <summary>
    /// FadeOutする
    /// </summary>
    /// <param name="amount">Fadeする量</param>
    private void FadeOut(float amount)
    {
        float volume = audioSource.volume;

        if (volume <= 0.0f)                 //完全にFadeout
            Destroy(gameObject);

        volume -= amount;                   //Fade処理
        volume = Mathf.Max(volume, 0.0f);
        audioSource.volume = volume;        //適用
    }

    /// <summary>
    /// 現在最大音量にLerpする
    /// </summary>
    /// <param name="fadeSpeed">Fadeスピード</param>
    public void LerpToMax()
    {
        float volume = audioSource.volume;

        volume = Mathf.Lerp(volume, maxVolume, 0.03f);
        audioSource.volume = volume;        //適用
    }

    /// <summary>
    /// 音源を設定
    /// </summary>
    /// <param name="clip">音源</param>
    public void SetClip(AudioClip clip)
    {
        audioSource.clip = clip;
    }

    /// <summary>
    /// 現在状態
    /// </summary>
    /// <returns></returns>
    public FadeState CurrentState()
    {
        return state;
    }

    /// <summary>
    /// Fade状態を設定
    /// </summary>
    public void SetState(FadeState state)
    {
        this.state = state;
    }

    /// <summary>
    /// Fadeのスピードを設定
    /// </summary>
    public void SetFadeSpeed(float speed)
    {
        fadeSpeed = speed;
    }

    /// <summary>
    /// 最大音量設定
    /// </summary>
    /// <param name="maxVolume">最大音量</param>
    public void SetMaxVolume(float maxVolume)
    {
        this.maxVolume = maxVolume;
    }

    /// <summary>
    /// 流す
    /// </summary>
    /// <param name="loop">ループするか</param>
    public void Play(bool loop = true)
    {
        audioSource.loop = loop;
        audioSource.Play();
    }

    /// <summary>
    /// 終わっているか
    /// </summary>
    /// <returns></returns>
    public bool IsEnd()
    {
        return !audioSource.isPlaying;
    }
}
