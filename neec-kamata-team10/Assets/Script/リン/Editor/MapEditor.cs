//------------------------------------------------------
// 作成日：2018.4.4
// 作成者：林 佳叡
// 内容：マップ編集用拡張機能
//------------------------------------------------------
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEditor;
using UnityEditor.SceneManagement;

public class MapEditor : EditorWindow
{

    [MenuItem("Window/マップ編集")]
    static void Open()
    {
        GetWindow<MapEditor>("マップ編集");             //Window表示（すでにある場合は既存のWindowを最上階）
    }

    private enum Mode
    {
        EditMode = 0,       //マップ編集
        SaveLoad,           //データ保存など
    }

    private int currentMode = (int)Mode.EditMode;       //編集モード

    private int infoLength = 100;                       //説明欄の長さ

    private string saveFileName;                        //保存名前
    private string savePath = "Assets/";                //保存場所

    private GameObject tileMapPrefab;                   //TileMapのPrefab
    private GameObject gameManagerPrefab;               //GameManagerのPrefab

    private GameObject stage;                           //Stage

    /// <summary>
    /// GUI配置
    /// </summary>
    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("マップ エディター");

        currentMode = GUILayout.Toolbar(currentMode,
            new string[] { "マップ編集", "書出し / 読み込み" });

        switch ((Mode)currentMode)                      //モードにより機能が変わる
        {
            case Mode.EditMode:                         //マップ編集モード
                EditMap();
                break;
            case Mode.SaveLoad:                         //マップ保存や読み込み
                SaveLoad();
                break;
        }
        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// UI更新
    /// </summary>
    private void Refresh()
    {
        GUI.SetNextControlName("");
        GUI.FocusControl("");
    }

    private void EditMap()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        OpenNewScene();                                //新しいSceneを作る
        OpenPalette();                                 //マップチップを開く
        EditorGUILayout.EndVertical();
    }

    private void OpenNewScene()
    {
        if (!GUILayout.Button("新しいステージ"))                            //新しいステージボタン
            return;

        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();        //今のシーンを保存するか
        Scene scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);       //新しいシーン
        LoadPrefab();                                                       //Prefabをロードする
        InitNewScene();                                                     //Sceneを初期化
    }

    /// <summary>
    /// PrefabをLoadする
    /// </summary>
    private void LoadPrefab()
    {
        tileMapPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/Editor/Grid.prefab");
        gameManagerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefab/GameManager/GameManager.prefab");
    }

    private void InitNewScene()
    {
        GameObject gameManager = Instantiate(gameManagerPrefab, Vector3.zero, Quaternion.identity);             //GameManagerを作成
        gameManager.name = "GameManager";                                   //名前修正

        stage = new GameObject();                                           //StageのEmptyObject
        stage.name = "Stage";                                               //名前修正

        string[] layout = { "Last", "Back", "Default", "Front", "First" };  //名前
        int stageZ = 1;
        int interval = 3;                                                   //Layoutの間隔
        for (int i = 0; i < layout.Length; i++)
        {
            GameObject grid = Instantiate(tileMapPrefab, Vector3.zero, Quaternion.identity, stage.transform);    //Gridを作成する
            grid.transform.position = new Vector3(0, 0, interval * 2 - i * interval - stageZ);                  //Z軸修正
            grid.name = layout[i] + "Grid";                                  //名前修正
            grid.transform.GetChild(0).name = layout[i] + "Layout";          //名前修正
        }
    }

    /// <summary>
    /// EditWindowを開く
    /// </summary>
    private void OpenPalette()
    {
        if (GUILayout.Button("Map Chipを開く"))                                      //開くボタン
        {
            EditorApplication.ExecuteMenuItem("Window/Tile Palette");                //Paletteを開く
        }
    }

    /// <summary>
    /// マップをセーブ/ロード
    /// </summary>
    private void SaveLoad()
    {
        Save();                                                                      //セーブUI
        Load();                                                                      //ロードUI
    }

    private void Load()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("読み込み");
        EditorGUILayout.EndVertical();
    }

    private void Save()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("書出し");

        FileName(ref saveFileName);                                                 //ファイル名欄

        #region File Path
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("保存先：", GUILayout.Width(infoLength));        //説明欄
        savePath = EditorGUILayout.TextField(savePath);                             //パス欄
        FolderBrowser(ref savePath);                                                //ブラウザボタン
        EditorGUILayout.EndHorizontal();
        #endregion

        #region Save Reset Button
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("セーブ"))                                             //セーブボタン
        {
            GameObject a = new GameObject();                                        //Todo：Mapに代わる
            PrefabUtility.CreatePrefab(savePath + "/" + saveFileName + ".prefab", a);
        }
        if (GUILayout.Button("リセット"))                                           //リセットボタン
        {
            saveFileName = "";                                                      //初期設定
            savePath = "Assets/";
            Refresh();                                                              //UI再描画
        }
        EditorGUILayout.EndHorizontal();
        #endregion

        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// ファイル名の欄
    /// </summary>
    /// <param name="fileName">ファイル名を指定する変数</param>
    private void FileName(ref string fileName)
    {
        EditorGUILayout.BeginHorizontal();                                          //横並び
        EditorGUILayout.LabelField("ファイル名：", GUILayout.Width(infoLength));    //説明欄
        fileName = EditorGUILayout.TextField(fileName);                             //入力欄
        EditorGUILayout.EndHorizontal();                                            //横並び
    }

    /// <summary>
    /// フォルダブラウザを開く
    /// </summary>
    /// <param name="path">指定パスの変数</param>
    private void FolderBrowser(ref string path)
    {
        bool click = GUILayout.Button("...", GUILayout.Width(25));                  //ブラウザボタン

        if (!click)                                                                 //クリックされなかったらリターン
            return;

        path = EditorUtility.SaveFolderPanel("", "Assets/", "");                    //パス指定
        if (path.Contains("Assets/"))                                               //プロジェクト内なら
        {
            int index = path.IndexOf("Assets/");                                    //パス修正
            path = path.Remove(0, index);
            Refresh();                                                              //UI再表示
            return;
        }

        path = "Assets/";                                                           //プロジェクト以外はパス修正
        Refresh();
    }
}
