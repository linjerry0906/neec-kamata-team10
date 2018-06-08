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
    private SoundState fadeState;
    [SerializeField]
    private float fadeSpeed = 0.005f;
    [SerializeField]
    private float startVolume = 0.0f;
    [SerializeField]
    private bool loop = true;

	// Use this for initialization
	void Start ()
    {
        SoundManager soundManager = GameManager.Instance.GetSoundManager();
        soundManager.PlayBGM(audioClip, fadeState, fadeSpeed, loop, startVolume);
	}
}
