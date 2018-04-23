//------------------------------------------------------
// 作成日：2018.4.2
// 作成者：林 佳叡
// 内容：ゲームマネージャー
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;                     //GameManagerのインスタンス

    private ControllerManager controllerManager;            //コントローラーのマネージャー
    private SceneChange sceneManager;                       //シーンマネージャー

    private void Awake()
    {
        CheckInstance();                                    //Instanceをチェックする

        controllerManager = new ControllerManager();        //実体生成
        sceneManager = new SceneChange();                   //実体生成
    }

    /// <summary>
    /// Instanceをチェックする
    /// </summary>
    private void CheckInstance()
    {
        if (Instance != null)                               //Nullじゃない場合
        {
            Destroy(this.gameObject);                       //削除
            return;
        }
        Instance = this;                                    //Instance指定
        DontDestroyOnLoad(this.gameObject);                 //削除されないように
    }

    void Start () {
		
	}
	
	void Update () {
		
	}

    /// <summary>
    /// シーンを切り替わる
    /// </summary>
    /// <param name="nextScene">次のシーン</param>
    public void ChangeScene(EScene nextScene)
    {
        sceneManager.ChangeScene(nextScene);                //シーンChange
    }

    /// <summary>
    /// 指定のコントローラーを取得
    /// </summary>
    /// <param name="eController">キーボードか、パッドか</param>
    /// <returns></returns>
    public ICharacterController GetController(EController eController)
    {
        if(eController == EController.KEYBOARD)             //キーボードの場合
            return controllerManager.Keyboard();            //キーボードのコントローラーを返す

        return controllerManager.Pad();                     //パッドのコントローラーを返す
    }
}
