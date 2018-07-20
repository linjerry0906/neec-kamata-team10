using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TransitionTitleScene : MonoBehaviour {

    VideoPlayer videoPlayer;
    FadeManager fadeManager;
	// Use this for initialization
	void Start () {
		videoPlayer = GetComponent<VideoPlayer>();
        fadeManager = transform.GetChild(0).gameObject
            .GetComponent<FadeManager>();
	}
	
	// Update is called once per frame
	void Update () {
        //タイトルシーンへ遷移する条件を満たしていれば遷移する。
        if(IsTransitionCondistions())
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
