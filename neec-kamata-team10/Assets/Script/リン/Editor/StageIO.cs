//------------------------------------------------------
// 作成日：2018.4.22
// 作成者：林 佳叡
// 内容：ステージの保存
//------------------------------------------------------
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

public class StageIO
{
    private int infoLength = 100;                       //説明欄の長さ

    private string saveFileName;                        //保存名前
    private string savePath = "Assets/Scene/";          //保存場所

    public StageIO() { }

    /// <summary>
    /// 更新処理
    /// </summary>
    public void Update ()
    {
        SaveUI();                                       //セーブUI
        LoadUI();                                       //ロードUI
    }

    /// <summary>
    /// UI更新
    /// </summary>
    private void Refresh()
    {
        GUI.SetNextControlName("");
        GUI.FocusControl("");
    }

    /// <summary>
    /// ロードUI
    /// </summary>
    private void LoadUI()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("読み込み");
        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// セーブUI
    /// </summary>
    private void SaveUI()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUILayout.LabelField("書出し");

        FileName();                                                                 //ファイル名欄

        #region File Path
        EditorGUILayout.BeginHorizontal();
        SavePath();                                                                 //セーブパス欄
        FolderBrowser();                                                            //ブラウザボタン
        EditorGUILayout.EndHorizontal();
        #endregion

        #region Save Reset Button
        EditorGUILayout.BeginHorizontal();
        SaveButton();                                                               //セーブボタン
        ResetButton();                                                              //リセットボタン
        EditorGUILayout.EndHorizontal();
        #endregion

        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// セーブボタン
    /// </summary>
    private void SaveButton()
    {
        if (GUILayout.Button("セーブ"))                                             //セーブボタン
        {
            SaveScene();                                                            //シーン保存
        }
    }

    /// <summary>
    /// リセットボタン
    /// </summary>
    private void ResetButton()
    {
        if (GUILayout.Button("リセット"))                                           //リセットボタン
        {
            saveFileName = "";                                                      //初期設定
            savePath = "Assets/Scene/";
            Refresh();                                                              //UI再描画
        }
    }

    /// <summary>
    /// シーンを保存
    /// </summary>
    private void SaveScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();                         //現在のシーンを取得
        if (!currentScene.IsValid())                                                //Nullの場合
            return;

        EditorSceneManager.SaveScene(currentScene, savePath + saveFileName + ".unity");     //保存
    }

    /// <summary>
    /// ファイル名の欄
    /// </summary>
    /// <param name="fileName">ファイル名を指定する変数</param>
    private void FileName()
    {
        EditorGUILayout.BeginHorizontal();                                          //横並び
        EditorGUILayout.LabelField("ファイル名：", GUILayout.Width(infoLength));    //説明欄
        saveFileName = EditorGUILayout.TextField(saveFileName);                     //入力欄
        EditorGUILayout.EndHorizontal();                                            //横並び
    }

    /// <summary>
    /// セーブパス
    /// </summary>
    private void SavePath()
    {
        EditorGUILayout.LabelField("保存先：", GUILayout.Width(infoLength));        //説明欄
        savePath = EditorGUILayout.TextField(savePath);                             //パス欄
    }

    /// <summary>
    /// フォルダブラウザを開く
    /// </summary>
    /// <param name="path">指定パスの変数</param>
    private void FolderBrowser()
    {
        bool click = GUILayout.Button("...", GUILayout.Width(25));                  //ブラウザボタン

        if (!click)                                                                 //クリックされなかったらリターン
            return;

        savePath = EditorUtility.SaveFolderPanel("", "Assets/", "");                //パス指定
        if (savePath.Contains("Assets/"))                                           //プロジェクト内なら
        {
            int index = savePath.IndexOf("Assets/");                                //パス修正
            savePath = savePath.Remove(0, index);
            Refresh();                                                              //UI再表示
            return;
        }

        savePath = "Assets/Scene";                                                  //プロジェクト以外はパス修正
        Refresh();
    }
}
