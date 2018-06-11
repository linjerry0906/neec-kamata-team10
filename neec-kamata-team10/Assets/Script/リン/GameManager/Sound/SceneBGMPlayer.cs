//------------------------------------------------------
// 作成日：2018.6.08
// 作成者：林 佳叡
// 内容：PlayしたいBGMを流してくれるクラス
//------------------------------------------------------
using UnityEngine;

public class SceneBGMPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClip;
    [SerializeField]
    private FadeState fadeState;
    [SerializeField]
    private float fadeSpeed = 0.005f;
    [SerializeField]
    private float startVolume = 0.0f;

    // 2018.6.11 本田 修正 -> BGMのマスター音量を調整できるようにした
    [SerializeField]
    private float masterVolume = 0.5f;
    // ここまで
    
    [SerializeField]
    private bool loop = true;

	// Use this for initialization
	void Start ()
    {
        SoundManager soundManager = GameManager.Instance.GetSoundManager();

        // ここからマスター設定
        soundManager.SetMaxVolume(masterVolume);
        // ここまで

        soundManager.PlayBGM(audioClip, fadeState, fadeSpeed, loop, startVolume);
	}
}
