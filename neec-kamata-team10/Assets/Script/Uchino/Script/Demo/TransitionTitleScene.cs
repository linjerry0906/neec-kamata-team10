using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TransitionTitleScene : MonoBehaviour {

    VideoPlayer videoPlayer;
    FadeManager fadeManager;
    float frameCount = 1;
	// Use this for initialization
	void Start () {
		videoPlayer = GetComponent<VideoPlayer>();
        fadeManager = transform.GetChild(0).gameObject
            .GetComponent<FadeManager>();
        frameCount = videoPlayer.frameCount;
	}
	
	// Update is called once per frame
	void Update () {
        //動画再生時間を正確に取得するため、1秒遅れて実行させる。
        Invoke("FadeOutToTitle", 1);
	}

    //タイトルシーンへ遷移する条件を満たしていれば遷移する。
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
        if (Input.anyKey) return true;
        if (videoPlayer.frameCount == (ulong)videoPlayer.frame) return true;
        
        return false;
    }


}
