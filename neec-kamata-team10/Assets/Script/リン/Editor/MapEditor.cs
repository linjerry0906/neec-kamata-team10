//------------------------------------------------------
// 作成日：2018.4.4
// 作成者：林 佳叡
// 内容：マップ編集用拡張機能
//------------------------------------------------------
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEditor;

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
        Chips,              //マップチップ関連
        SaveLoad,           //データ保存など
    }

    private int currentMode = (int)Mode.EditMode;       //編集モード
    private MapViewer mapViewer;

    private int infoLength = 100;                       //説明欄の長さ

    private string saveFileName;                        //保存名前
    private string savePath;                            //保存場所

    private float tileSize = 20;                        //タイルのサイズ
    private int xTile = 0;                              //マップの横サイズ
    private int yTile = 0;                              //マップの縦サイズ

    private string currentTilePath = "";                //現在選択のチップ
    private Vector2 scrollPos = Vector2.zero;           //Scroll Position

    /// <summary>
    /// GUI配置
    /// </summary>
    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("マップ エディター");

        currentMode = GUILayout.Toolbar(currentMode,
            new string[] { "マップ編集", "マップチップ", "書出し / 読み込み" });

        switch ((Mode)currentMode)                      //モードにより機能が変わる
        {
            case Mode.EditMode:                         //マップ編集モード
                EditMap();
                break;
            case Mode.Chips:                            //マップチップの読み込み
                MapChips();
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
        TileInfo();                                             //Mapの大きさを設定

        EditorGUILayout.BeginVertical(GUI.skin.box);
        OpenEditWindow();                                       //Viewerを開く
        ResizetMap();                                           //マップサイズ調整
        NewMap();                                               //新しいマップ作成
        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// EditWindowを開く
    /// </summary>
    private void OpenEditWindow()
    {
        if (GUILayout.Button("Map Viewerを開く"))               //開くボタン
        {
            if (mapViewer == null)                              //開いてない場合
            {
                mapViewer = MapViewer.CreateMapViewer(this);    //開く
                mapViewer.Initialize(tileSize, xTile, yTile);   //初期化
            }
            else
            {
                mapViewer.Focus();                              //Windowを最上階にする
            }
        }
    }

    /// <summary>
    /// マップの大きさを設定
    /// </summary>
    private void TileInfo()
    {
        EditorGUILayout.BeginVertical(GUI.skin.box);

        EditorGUILayout.BeginHorizontal();                                           //横並び
        EditorGUILayout.LabelField("グリッドサイズ：", GUILayout.Width(infoLength)); //説明欄
        tileSize = EditorGUILayout.FloatField(tileSize);                             //入力欄
        EditorGUILayout.EndHorizontal();                                             //横並び

        EditorGUILayout.BeginHorizontal();                                           //横並び
        EditorGUILayout.LabelField("横ブロック数：", GUILayout.Width(infoLength));   //説明欄
        xTile = EditorGUILayout.IntField(xTile);                                     //入力欄
        EditorGUILayout.EndHorizontal();                                             //横並び

        EditorGUILayout.BeginHorizontal();                                           //横並び
        EditorGUILayout.LabelField("縦ブロック数：", GUILayout.Width(infoLength));   //説明欄
        yTile = EditorGUILayout.IntField(yTile);                                     //入力欄
        EditorGUILayout.EndHorizontal();                                             //横並び

        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// Mapの大きさを適用
    /// </summary>
    private void ResizetMap()
    {
        if (!GUILayout.Button("マップサイズを適用"))                                 //リサイズボタン
            return;

        if (mapViewer == null)                                                       //Viewer開いてない場合
            mapViewer = MapViewer.CreateMapViewer(this);                             //開く

        mapViewer.Initialize(tileSize, xTile, yTile);                                //初期化
        mapViewer.Focus();
    }

    /// <summary>
    /// 新しいマップ作成
    /// </summary>
    private void NewMap()
    {
        if (!GUILayout.Button("新しいマップ作成"))                                   //マップチップ作成ボタン
            return;

        if (mapViewer == null)                                                       //Viewer開いてない場合
            mapViewer = MapViewer.CreateMapViewer(this);                             //開く

        mapViewer.NewMap(tileSize, xTile, yTile);                                    //新しく生成
        mapViewer.Focus();
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

    private void MapChips()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUI.skin.box);
        DrawMapChip();                                                              //MapChip一覧を描画
        EditorGUILayout.EndScrollView();
    }

    /// <summary>
    /// マップチップ一覧
    /// </summary>
    private void DrawMapChip()
    {
        string path = "Assets/MapChip";                                             //アセットパス
        string[] textureNames = Directory.GetFiles(path, "*.png");                  //Pngファイルを探す

        float xPos = 0.0f;                                      //セルの左上X座標
        float yPos = 0.0f;                                      //セルの左上Y座標
        float cellWidth = 100.0f;                               //セルの横サイズ
        float cellHeight = 100.0f;                              //セルの縦サイズ
        int maxCell = 5;                                        //セルの縦サイズ

        EditorGUILayout.BeginVertical(GUI.skin.box);
        #region Null Chip
        EditorGUILayout.BeginHorizontal();                      //横並び始め
        if (GUILayout.Button("Null", GUILayout.MaxWidth(cellWidth), GUILayout.MaxHeight(cellHeight),
                GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false)))                           //ボタン設置（テクスチャ表示）
            currentTilePath = "Null";                           //クリックされたらテクスチャ指定
        xPos += cellWidth;                                      //X座標を右にずらす
        #endregion

        foreach (string textureDir in textureNames)             //アセットを一周する
        {
            if (xPos > (maxCell - 1) * cellWidth)               //横の数を超えた場合（0から始まるので-1）
            {
                xPos = 0.0f;                                    //X座標をリセット
                yPos += cellHeight;                             //Y座標を一段下にずらす
                EditorGUILayout.EndHorizontal();                //横並び終了
            }

            if (xPos == 0.0f)                                   //最初のセルの場合
                EditorGUILayout.BeginHorizontal();              //横並び始め

            GUILayout.FlexibleSpace();
            Texture2D tex = (Texture2D)AssetDatabase.LoadAssetAtPath(textureDir, typeof(Texture2D));    //テクスチャを読み込む

            if (GUILayout.Button(tex, GUILayout.MaxWidth(cellWidth), GUILayout.MaxHeight(cellHeight),
                GUILayout.ExpandWidth(false), GUILayout.ExpandHeight(false)))                           //ボタン設置（テクスチャ表示）
                currentTilePath = textureDir;                   //クリックされたらテクスチャ指定
            GUILayout.FlexibleSpace();
            xPos += cellWidth;                                  //X座標を右にずらす
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.LabelField("Current Tile：" + currentTilePath);                                //現在選択しているチップ
        if (currentTilePath != "Null" || currentTilePath != "")                                        //選択している場合
        {
            Texture2D currentTile = (Texture2D)AssetDatabase.LoadAssetAtPath(currentTilePath, typeof(Texture2D));
            GUILayout.Box(currentTile);
        }
    }

    /// <summary>
    /// 現在選択のチップ
    /// </summary>
    /// <returns></returns>
    public string CurrentChip()
    {
        return currentTilePath;
    }
}
