//------------------------------------------------------
// 作成日：2018.4.4
// 作成者：林 佳叡
// 内容：マップ編集用拡張機能
//------------------------------------------------------
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

public class MapEditor : EditorWindow
{

    [MenuItem("Window/マップ編集")]
    static void Open()
    {
        //Window表示（すでにある場合は既存のWindowを最上階）
        GetWindow<MapEditor>("マップ編集");
    }

    private enum Mode
    {
        EditMode = 0,       //マップ編集
        SaveLoad,           //データ保存など
        Chips,              //マップチップ関連
    }

    private int currentMode = (int)Mode.EditMode;       //編集モード
    private MapViewer mapViewer;

    private int infoLength = 100;                       //説明欄の長さ

    private string saveFileName;                        //保存名前
    private string savePath;                            //保存場所

    /// <summary>
    /// GUI配置
    /// </summary>
    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("マップ エディター");

        currentMode = GUILayout.Toolbar(currentMode,
            new string[] { "マップ編集", "書出し / 読み込み", "マップチップ" });

        switch ((Mode)currentMode)                      //モードにより機能が変わる
        {
            case Mode.EditMode:                         //マップ編集モード
                EditMap();
                break;
            case Mode.SaveLoad:                         //マップ保存や読み込み
                SaveLoad();
                break;
            case Mode.Chips:                            //マップチップの読み込み
                MapChips();
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
        OpenEditWindow();
    }

    private void OpenEditWindow()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        if (GUILayout.Button("Map Viewerを開く"))
        {
            if (mapViewer == null)
            {
                mapViewer = MapViewer.CreateMapViewer(this);
            }
            else
            {
                mapViewer.Focus();
            }
        }
        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// マップをセーブ/ロード
    /// </summary>
    private void SaveLoad()
    {
        Save();                                         //セーブUI
        Load();                                         //ロードUI
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

    private void MapChips()
    {
    }
}
