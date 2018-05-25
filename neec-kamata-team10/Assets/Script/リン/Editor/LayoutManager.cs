//------------------------------------------------------
// 作成日：2018.4.22
// 作成者：林 佳叡
// 内容：Layer編集用拡張機能
//------------------------------------------------------
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class LayoutManager
{
    private GameObject stagePrefab;                    //Stage
    private GameObject gameManagerPrefab;              //GameManager
    private GameObject gameSystemPrefab;               //GameSystem
    private GameObject cameraPrefab;                   //カメラ
    private GameObject canvasPrefab;                   //キャンバス

    public LayoutManager() { }

    /// <summary>
    /// 毎フレーム処理
    /// </summary>
    public void Update()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);

        OpenNewScene();                                //新しいSceneを作る
        OpenPalette();                                 //マップチップを開く
        AddDefaultObj();

        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// 新しいシーンを作成
    /// </summary>
    private void OpenNewScene()
    {
        if (!GUILayout.Button("新しいステージ"))                            //新しいステージボタン
            return;

        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();        //今のシーンを保存するか

        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);                        //新しいシーン
        EditorSettings.defaultBehaviorMode = EditorBehaviorMode.Mode2D;     //2Dモード
        LoadPrefab();                                                       //Prefabをロードする
        InitNewScene();                                                     //Sceneを初期化
    }

    /// <summary>
    /// PrefabをLoadする
    /// </summary>
    private void LoadPrefab()
    {
        stagePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Editor/BackgroundLayer.prefab");
        gameManagerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/GameManager/GameManager.prefab");
        gameSystemPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Editor/GameSystem.prefab");
        cameraPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Editor/Main Camera.prefab");
        canvasPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Editor/Canvas.prefab");
    }

    /// <summary>
    /// シーン初期化
    /// </summary>
    private void InitNewScene()
    {
        CreateObj("GameManager", gameManagerPrefab);            //GameManager
        CreateObj("GameSystem", gameSystemPrefab);              //GameSystem
        CreateObj("BackgroundLayer", stagePrefab);              //Background
        CreateObj("Main Camera", cameraPrefab);                 //カメラ
        CreateObj("Canvas", canvasPrefab);                      //キャンバス
    }

    /// <summary>
    /// デフォルトオブジェクトを追加
    /// </summary>
    private void AddDefaultObj()
    {
        if (!GUILayout.Button("デフォルト オブジェクト"))                   //新しいステージボタン
            return;

        LoadPrefab();
        GameObject[] rootObj = EditorSceneManager.GetActiveScene().GetRootGameObjects();
        string name = "";
        for (int i = 0; i < rootObj.Length; i++)
        {
            name += rootObj[i].name;
            name += ",";
        }

        GameObject gameManager;
        GameObject gameSystem;
        GameObject mainCamera;
        GameObject Canvas;

        if (!name.Contains("GameManager"))
            CreateObj("GameManager", gameManagerPrefab);            //GameManager
        if (!name.Contains("GameSystem"))
            CreateObj("GameSystem", gameSystemPrefab);              //GameSystem
        if (!name.Contains("BackgroundLayer"))
            CreateObj("BackgroundLayer", stagePrefab);              //Background
        if (!name.Contains("Main Camera"))
            CreateObj("Main Camera", cameraPrefab);                 //カメラ
        if (!name.Contains("Canvas"))
            CreateObj("Canvas", canvasPrefab);                      //キャンバス
    }

    /// <summary>
    /// オブジェクトを作成
    /// </summary>
    /// <param name="name">名前</param>
    /// <param name="obj">クローン先</param>
    private GameObject CreateObj(string name, GameObject obj)
    {
        GameObject clone = Object.Instantiate(obj);
        clone.name = name;
        return clone;
    }

    /// <summary>
    /// EditWindowを開く
    /// </summary>
    private void OpenPalette()
    {
        if (GUILayout.Button("Map Chipを開く"))                             //開くボタン
        {
            EditorApplication.ExecuteMenuItem("Window/Tile Palette");       //Paletteを開く
        }
    }
}
