//------------------------------------------------------
// 作成日：2018.4.4
// 作成者：林 佳叡
// 内容：マップ編集用拡張機能
//------------------------------------------------------
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class MapEditor : EditorWindow
{
    [MenuItem("Window/マップ編集")]
    static void Open()
    {
        GetWindow<MapEditor>("マップ編集");             //Window表示（すでにある場合は既存のWindowを最上階）
    }

    void OnFocus()
    {
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
        SceneView.onSceneGUIDelegate += OnSceneGUI;
        Repaint();
    }

    void OnDestroy()
    {
        DestroyImmediate(onMouse);                      //MouseObjを削除
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
    }

    private enum Mode
    {
        EditMode = 0,       //マップ編集
        SaveLoad,           //データ保存など
        Object,             //オブジェクト配置など
    }

    private int currentMode = (int)Mode.EditMode;       //編集モード

    private LayoutManager layoutManager = new LayoutManager();
    private StageIO stageIO = new StageIO();
    private ObjectEditor objectEditor = new ObjectEditor();

    private GameObject stageObj;
    private GameObject onMouse = null;

    /// <summary>
    /// GUI配置
    /// </summary>
    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();

        currentMode = GUILayout.Toolbar(currentMode,
            new string[] { "マップ編集", "書出し / 読み込み", "オブジェクト" });

        EditorGUILayout.EndVertical();

        switch ((Mode)currentMode)                      //モードにより機能が変わる
        {
            case Mode.EditMode:                         //マップ編集モード
                layoutManager.Update();
                break;
            case Mode.SaveLoad:                         //マップ保存や読み込み
                stageIO.Update();
                break;
            case Mode.Object:                           //オブジェクト配置
                objectEditor.Update();
                break;
        }
    }

    /// <summary>
    /// シーンの処理
    /// </summary>
    private void UpdateScene()
    {
        UpdateMouse();

        SceneView.RepaintAll();                         //Editor描画
        HandleUtility.Repaint();                        //Editor描画
    }

    /// <summary>
    /// Mouseを更新
    /// </summary>
    private void UpdateMouse()
    {
        if (objectEditor.GetCurrentObject() == null)    //選択なし状態
        {
            if (onMouse != null)                        //マウスにオブジェクトある場合
                DestroyImmediate(onMouse);              //削除
            onMouse = null;                             //Nullにする
            return;
        }
        if (onMouse == null ||
            onMouse.name != objectEditor.GetCurrentObject().name)   //マウスのオブジェクトが異なる場合
        {
            if (onMouse != null)                                    //マウスにオブジェクトある場合
                DestroyImmediate(onMouse);                          //削除
            CreateMouseObj();                                       //マウスのObjectを作成
        }
        Event e = Event.current;
        Camera camera = SceneView.lastActiveSceneView.camera;       //カメラ取得
        Vector2 mousePos = e.mousePosition;                         //マウス位置取得
        Vector3 world = camera.ScreenToWorldPoint(HandleUtility.GUIPointToScreenPixelCoordinate(mousePos)); //座標変換
        world.z = -0.5f;                                            //深度固定
        world = GridHelper.SetOnGrid(world);
        onMouse.transform.position = world;                         //座標設定

        SetTrigger();
    }

    /// <summary>
    /// マウスのObjectを作成
    /// </summary>
    private void CreateMouseObj()
    {
        onMouse = PrefabUtility.InstantiatePrefab(objectEditor.GetCurrentObject()) as GameObject;           //作成
        onMouse.name = objectEditor.GetCurrentObject().name;        //名前変更
        CheckStage();                                               //ステージオブジェクト確認
        onMouse.transform.SetParent(stageObj.transform);            //親オブジェクト設定
    }

    /// <summary>
    /// Spaceキーで設置
    /// </summary>
    private void SetTrigger()
    {
        Event e = Event.current;
        int controlID = GUIUtility.GetControlID(FocusType.Passive);
        if (e.GetTypeForControl(controlID) == EventType.KeyDown &&
            e.keyCode == KeyCode.Space)                             //Spaceキーが押したら
        {
            onMouse.tag = objectEditor.CurrentTag();                //タグ設定
            MapBlockComponent();
            CreateMouseObj();                                       //設置
            Scene currentScene = SceneManager.GetActiveScene();     //scene取得
            EditorSceneManager.MarkSceneDirty(currentScene);        //変更があると表記する
        }
    }

    /// <summary>
    /// 鏡に映れるようにする
    /// </summary>
    private void MapBlockComponent()
    {
        if (!objectEditor.CurrentTag().Equals("stage_block"))
            return;
        Collider collider = onMouse.GetComponent<Collider>();
        if (collider)
            return;

        onMouse.AddComponent<BoxCollider>();
        Rigidbody rigidbody = onMouse.AddComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    /// <summary>
    /// ステージオブジェクトあるかを確認
    /// </summary>
    private void CheckStage()
    {
        if (stageObj != null)
            return;

        Scene currentScene = SceneManager.GetActiveScene();         //scene取得
        foreach (GameObject g in currentScene.GetRootGameObjects()) //Rootオブジェクトから探す
        {
            if (g.name == "Stage")                                  //ステージがある場合
            {
                stageObj = g;                                       //指定
                return;
            }
        }
        stageObj = new GameObject("Stage");                         //新しく作成
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        UpdateScene();                                              //シーンを更新
    }
}
