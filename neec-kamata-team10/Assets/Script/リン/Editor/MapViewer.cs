using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapViewer : EditorWindow
{
    private static int WINDOW_WIDTH = 300;
    private static int WINDOW_HEIGHT = 200;
    private MapEditor parent;

    private float gridSize;                     //グリッドサイズ

    private Rect[,] mapGrid;                    //マップグリッド

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
    }

    /// <summary>
    /// グリッドを作成
    /// </summary>
    /// <param name="width">横タイル数</param>
    /// <param name="height">縦タイル数</param>
    /// <returns></returns>
    private Rect[,] CreateGrid(int width, int height)
    {
        float xPos = 0.0f;                      //タイルの左上X座標
        float yPos = 0.0f;                      //タイルの左上Y座標

        Rect[,] destRects = new Rect[height, width];    //指定のチップ数の配列を確保

        for (int y = 0; y < height; ++y)        //各グリッドを作成していく
        {
            xPos = 0.0f;                        //Xを先端にする
            for (int x = 0; x < width; ++x)     //X軸を回していく
            {
                Rect grid = new Rect(xPos, yPos, gridSize, gridSize);  //グリッドを一個作成
                destRects[y, x] = grid;                                //配列に入れる
                xPos += gridSize;               //X座標をグリッドサイズにずらす
            }
            yPos += gridSize;                   //Y座標をグリッドサイズにずらす
        }

        return destRects;                       //作成したグリッドを返す
    }
}
