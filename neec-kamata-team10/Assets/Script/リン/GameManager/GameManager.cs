//------------------------------------------------------
// 作成日：2018.4.2
// 作成者：林 佳叡
// 内容：ゲームマネージャー
//------------------------------------------------------
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;                     //GameManagerのインスタンス

    [SerializeField]
    private EController debugController;

    private ControllerManager controllerManager;            //コントローラーのマネージャー
    private SceneChange sceneManager;                       //シーンマネージャー
    private StageManager stageManager;                      //ステージマネージャー

    private void Awake()
    {
        CheckInstance();                                    //Instanceをチェックする

        controllerManager = new ControllerManager();
        sceneManager = new SceneChange();
        stageManager = new StageManager();
        stageManager.Initialize(0);                         //Debug Test
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

    void Start ()
    {
	}
	
	void Update ()
    {
        stageManager.Update();                              //Stage時間を更新
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
    public ICharacterController GetController()
    {
        if(debugController == EController.KEYBOARD)         //キーボードの場合
            return controllerManager.Keyboard();            //キーボードのコントローラーを返す

        return controllerManager.Pad();                     //パッドのコントローラーを返す
    }

#region Stage関連

    /// <summary>
    /// 次のステージを指定
    /// </summary>
    /// <param name="stage">ステージ数</param>
    public void SelectStage(int stage)
    {
        sceneManager.ChangeScene((EScene)stage);            //シーン切り替え
        stageManager.Initialize(stage);                     //ステージ初期化
    }

    /// <summary>
    /// ステージマネージャーを取得
    /// </summary>
    /// <returns></returns>
    public StageManager GetStageManager()
    {
        return stageManager;
    }

#endregion
}
