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
        GameObject gameManager = Object.Instantiate(gameManagerPrefab, Vector3.zero, Quaternion.identity); //GameManagerを作成
        gameManager.name = "GameManager";
        GameObject gameSystem = Object.Instantiate(gameSystemPrefab, Vector3.zero, Quaternion.identity);   //GameSystemを作成
        gameManager.name = "GameSystem";
        GameObject stage = Object.Instantiate(stagePrefab, Vector3.zero, Quaternion.identity);             //編集レイヤー
        stage.name = "BackgroundLayer";
        GameObject camera = Object.Instantiate(cameraPrefab);                                              //カメラ
        camera.name = "Main Camera";
        GameObject canvas = Object.Instantiate(canvasPrefab);                                              //キャンバス
        canvas.name = "Canvas";
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
