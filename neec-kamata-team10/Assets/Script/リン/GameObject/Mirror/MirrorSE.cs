//------------------------------------------------------
// 作成日：2018.6.11
// 作成者：林 佳叡
// 内容：鏡のSE
//------------------------------------------------------
using UnityEngine;

public class MirrorSE : MonoBehaviour
{
    [SerializeField]
    private AudioClip setSE;
    [SerializeField]
    private AudioClip breakSE;

    private AudioSource audioPlayer;

	void Start ()
    {
        audioPlayer = GetComponent<AudioSource>();
	}

    /// <summary>
    /// セットのSEを流す
    /// </summary>
    public void PlaySetSE()
    {
        if (!audioPlayer)
            Start();
        audioPlayer.clip = setSE;
        audioPlayer.Play();
    }

    /// <summary>
    /// 割れたSEを流す
    /// </summary>
    public void PlayBreakSE()
    {
        if (!audioPlayer)
            Start();
        audioPlayer.clip = breakSE;
        audioPlayer.Play();
    }
}
