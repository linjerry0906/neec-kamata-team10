using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapViewer : EditorWindow
{
    private static int WINDOW_WIDTH = 300;
    private static int WINDOW_HEIGHT = 200;
    private MapEditor parent;
    private Vector2 scrollPos;

    private float gridSize;                     //グリッドサイズ

    private List<List<Rect>> mapGrid;           //マップグリッド
    private string[,] mapChip;                  //マップチップ

    public static MapViewer CreateMapViewer(MapEditor parent)
    {
        MapViewer window = (MapViewer)EditorWindow.GetWindow(typeof(MapViewer), false, "Map Viewer");
        window.Show();                          //Windowを表示
        window.minSize = new Vector2(WINDOW_WIDTH, WINDOW_HEIGHT);
        window.SetParent(parent);
        return window;
    }

    /// <summary>
    /// 親ウィンドウを記録
    /// </summary>
    /// <param name="parent">親ウィンドウ</param>
    private void SetParent(MapEditor parent)
    {
        this.parent = parent;
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="gridSize">グリッドサイズ</param>
    /// <param name="width">横タイル数</param>
    /// <param name="height">縦タイル数</param>
    public void Initialize(float gridSize, int width, int height)
    {
        this.gridSize = gridSize;               //グリッドサイズを記録
        mapGrid = CreateGrid(width, height);    //マップグリッドを作成
        InitMapChip(width, height);             //マップチップ作成
    }

    /// <summary>
    /// 新しいマップ作成
    /// </summary>
    /// <param name="gridSize">グリッドサイズ</param>
    /// <param name="width">横タイル数</param>
    /// <param name="height">縦タイル数</param>
    public void NewMap(float gridSize, int width, int height)
    {
        this.gridSize = gridSize;               //グリッドサイズを記録
        mapGrid = CreateGrid(width, height);    //マップグリッドを作成
        mapChip = new string[height, width];    //実体生成
    }

    private void OnGUI()
    {
        DrawGrid();                             //グリッド描画
        CheckMouseClick();                      //クリックされたかをチェック
        DrawMap();                              //マップチップ描画
    }

    /// <summary>
    /// グリッドを作成
    /// </summary>
    /// <param name="width">横タイル数</param>
    /// <param name="height">縦タイル数</param>
    /// <returns></returns>
    private List<List<Rect>> CreateGrid(int width, int height)
    {
        float xPos = 0.0f;                      //タイルの左上X座標
        float yPos = 0.0f;                      //タイルの左上Y座標

        List<List<Rect>> destRects = new List<List<Rect>>();            //List作成

        for (int y = 0; y < height; ++y)        //各グリッドを作成していく
        {
            destRects.Add(new List<Rect>());    //一列追加
            xPos = 0.0f;                        //Xを先端にする
            for (int x = 0; x < width; ++x)     //X軸を回していく
            {
                Rect grid = new Rect(xPos, yPos, gridSize, gridSize);  //グリッドを一個作成
                destRects[y].Add(grid);                                //配列に入れる
                xPos += gridSize;               //X座標をグリッドサイズにずらす
            }
            yPos += gridSize;                   //Y座標をグリッドサイズにずらす
        }

        return destRects;                       //作成したグリッドを返す
    }

    /// <summary>
    /// マップチップ作成
    /// </summary>
    /// <param name="width">横サイズ</param>
    /// <param name="height">縦サイズ</param>
    private void InitMapChip(int width, int height)
    {
        if (mapChip == null)                                //初期化されてない状態
        {
            mapChip = new string[height, width];            //実体生成
            return;
        }

        string[,] newChip = new string[height, width];      //新しいマップチップ生成
        for (int y = 0; y < mapChip.GetLength(0); ++y)      //現在あったチップを一周
        {
            if (y >= height)                                //新しいチップの縦サイズを超えたら脱出
                break;
            for (int x = 0; x < mapChip.GetLength(1); ++x)
            {
                if (x >= width)                             //新しいチップの横サイズを超えたら脱出
                    break;
                newChip[y, x] = mapChip[y, x];              //チップを保存
            }
        }
        mapChip = newChip;                                  //旧チップを新チップに指定
    }

    /// <summary>
    /// マウスクリックをチェック
    /// </summary>
    private void CheckMouseClick()
    {
        Event e = Event.current;                            //現在のイベント取得
        if (e.type == EventType.MouseDown)                  //マウスがクリックされたら
        {
            Vector2 mousePos = Event.current.mousePosition; //マウスの位置を取得
            int xPos = 0;
            for (xPos = 0; xPos < mapGrid[0].Count; ++xPos) //グリッドのX軸をチェック
            {
                Rect grid = mapGrid[0][xPos];
                if (grid.x <= mousePos.x && mousePos.x <= grid.x + grid.width)  //範囲内なら現在のX座標を保つので、脱出
                    break;
            }

            if (xPos >= mapGrid[0].Count)                       //Indexを超えた処理
                xPos = mapGrid[0].Count - 1;

            for (int yPos = 0; yPos < mapGrid.Count; ++yPos)    //Y軸をチェック
            {
                if (mapGrid[yPos][xPos].Contains(mousePos))     //指定のグリッドぼ場合
                {
                    mapChip[yPos, xPos] = parent.CurrentChip();
                    Repaint();                                  //描画更新
                    break;
                }
            }
        }
    }

    /// <summary>
    /// グリッド描画
    /// </summary>
    private void DrawGrid()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        for (int y = 0; y < mapGrid.Count; ++y)                 //Y軸
        {
            for (int x = 0; x < mapGrid[y].Count; ++x)          //X軸
            {
                DrawGridLine(mapGrid[y][x]);                    //描画
            }
        }
        EditorGUILayout.EndScrollView();
    }

    /// <summary>
    /// グリッドラインを描画
    /// </summary>
    /// <param name="rect">グリッド</param>
    private void DrawGridLine(Rect rect)
    {
        Handles.color = new Color(1f, 1f, 1f, 0.5f);            //線の色

        Handles.DrawLine(                                       //上
            new Vector2(rect.position.x, rect.position.y),
            new Vector2(rect.position.x + rect.size.x, rect.position.y));

        Handles.DrawLine(                                       //下
            new Vector2(rect.position.x, rect.position.y + rect.size.y),
            new Vector2(rect.position.x + rect.size.x, rect.position.y + rect.size.y));

        Handles.DrawLine(                                       //左
            new Vector2(rect.position.x, rect.position.y),
            new Vector2(rect.position.x, rect.position.y + rect.size.y));

        Handles.DrawLine(                                       //右
            new Vector2(rect.position.x + rect.size.x, rect.position.y),
            new Vector2(rect.position.x + rect.size.x, rect.position.y + rect.size.y));
    }

    /// <summary>
    /// マップチップ描画
    /// </summary>
    private void DrawMap()
    {
        for (int y = 0; y < mapChip.GetLength(0); ++y)
        {
            for (int x = 0; x < mapChip.GetLength(1); ++x)
            {
                if (mapChip[y, x] == null || mapChip[y, x] == "Null" || mapChip[y, x] == "")
                    continue;

                if (mapChip[y, x].Length > 0)
                {
                    Texture2D tex = (Texture2D)AssetDatabase.LoadAssetAtPath(mapChip[y, x], typeof(Texture2D));
                    GUI.DrawTexture(mapGrid[y][x], tex);
                }
            }
        }
    }
}
