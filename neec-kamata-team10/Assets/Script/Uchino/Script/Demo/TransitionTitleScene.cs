using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TransitionTitleScene : MonoBehaviour {

    VideoPlayer videoPlayer;    //動画コンポーネント
    FadeManager fadeManager;    //フェードマネージャー
	// Use this for initialization
	void Start () {
		videoPlayer = GetComponent<VideoPlayer>();
        fadeManager = transform.GetChild(0).gameObject
            .GetComponent<FadeManager>();
	}
	
	// Update is called once per frame
	void Update () {
        //動画総再生時間を正確に取得するため、1秒遅れて実行させる。
        Invoke("FadeOutToTitle", 1);
	}

    /// <summary>
    /// タイトルシーンへ遷移する条件を満たしていれば遷移する。
    /// </summary>
    void FadeOutToTitle()
    {
        if (IsTransitionCondistions())
        {
            fadeManager.FadeOut();
        }
    }

    /// <summary>
    /// タイトルシーンへ遷移する条件を満たしているか
    /// </summary>
    /// <returns></returns>
    bool IsTransitionCondistions()
    {
        //何らかのキーが押されたら＆動画が再生を終えたら
        if (Input.anyKey) return true;
        if (videoPlayer.frameCount == (ulong)videoPlayer.frame) return true;
        
        return false;
    }


}
