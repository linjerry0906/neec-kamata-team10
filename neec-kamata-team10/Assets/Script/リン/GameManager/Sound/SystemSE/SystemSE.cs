//------------------------------------------------------
// 作成日：2018.6.11
// 作成者：林 佳叡
// 内容：System音を鳴らすスクリプト
//------------------------------------------------------
using System.Collections.Generic;
using UnityEngine;

public class SystemSE : MonoBehaviour {

    [SerializeField]
    private AudioClip[] seResource;
    [SerializeField]
    private GameObject soundBuffer;
    [SerializeField]
    private float maxVolume = 0.8f;                 //最大音量

    private List<GameObject> buffers = new List<GameObject>();


    private void Update()
    {
        for (int i = 0; i < buffers.Count;)
        {
            SoundBuffer se = buffers[i].GetComponent<SoundBuffer>();
            if (se.IsEnd())
            {
                Destroy(buffers[i]);
                buffers.RemoveAt(i);
                continue;
            }
            ++i;
        }
    }

    /// <summary>
    /// System音を流す
    /// </summary>
    /// <param name="seType">System音のタイプ</param>
    public void PlaySystemSE(SystemSoundEnum seType)
    {
        GameObject se = Instantiate(soundBuffer, transform);
        buffers.Add(se);
        SoundBuffer buffer = se.GetComponent<SoundBuffer>();
        buffer.Initialize();                        //初期化
        buffer.SetVolume(maxVolume);                //音量を設定
        buffer.SetClip(seResource[(int)seType]);    //音源設定
        buffer.Play(false);                         //流す
    }

    /// <summary>
    /// 最大音量を設定
    /// </summary>
    /// <param name="maxVolume">音量</param>
    public void SetSystemVolume(float maxVolume)
    {
        this.maxVolume = maxVolume;
    }
}
