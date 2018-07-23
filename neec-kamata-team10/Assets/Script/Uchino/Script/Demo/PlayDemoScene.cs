using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayDemoScene : MonoBehaviour {

    [SerializeField]
    private int transitionTime = 12;    //遷移を開始する時間
    private Timer waitTimer;            //タイマー
    private FadeManager fadeManager;    //fadeManagerの取得

    // Use this for initialization
    void Start () {
        waitTimer   = new Timer(transitionTime);
        fadeManager = transform.GetChild(0).
            gameObject.GetComponent<FadeManager>();
	}
	
	// Update is called once per frame
	void Update () {
        //何らかのキーが押されたらタイマーを初期化
        InitTimer_IsAnyKeyDown();

        //遷移する時間になったら
        if (IsTransitionTime())
        {
            //Demoシーンへ遷移する。
            fadeManager.FadeOut();
        }

        //タイマーをカウントする
        waitTimer.TimerUpdate();

    }

    /// <summary>
    /// デモシーンへ遷移する時間になったか
    /// </summary>
    /// <returns></returns>
    bool IsTransitionTime()
    {
        if (!waitTimer.IsTime())
            return false;

        waitTimer.Initialize();
        return true;
    }

    /// <summary>
    /// 何らかのキーが押されたらタイマーを初期化
    /// </summary>
    void InitTimer_IsAnyKeyDown()
    {
        if (Input.anyKey)
            waitTimer.Initialize();
    }

}
