//------------------------------------------------------
// 作成日：2018.4.4
// 作成者：林 佳叡
// 内容：オブジェクト配置モード
//------------------------------------------------------
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectEditor
{
    private List<GameObject> loadedObject = new List<GameObject>();     //読み込んだコンテンツ
    private GameObject currentObject = null;                            //現在選択中のオブジェクト

    private Vector2 scrollPos = Vector2.zero;                           //スクロール用
    private string tag = "Untagged";                                    //タグ

    public ObjectEditor() { }

    /// <summary>
    /// 更新処理
    /// </summary>
	public void Update()
    {
        EditorGUILayout.BeginVertical();
        ReLoadPrefab();                             //コンテンツを再度読み込む
        CurrentObject();                            //選択中のオブジェクトを表示
        ObjectButton();                             //オブジェクト一覧
        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// モデルやPrefabをリロードする
    /// </summary>
    private void ReLoadPrefab()
    {
        if (!GUILayout.Button("リロード コンテンツ"))
            return;

        loadedObject.Clear();                       //クリアする
        LoadPrefab();                               //コンテンツを読み込む
    }

    /// <summary>
    /// モデルやPrefabをロードする
    /// </summary>
    private void LoadPrefab()
    {
        LoadContents(
            "Assets/Prefab/Maya/Assets/", "*.fbx",
            SearchOption.TopDirectoryOnly, true);                         //Mayaコンテンツ
        LoadContents(
            "Assets/Prefab/GameObject/", "*.prefab",
            SearchOption.TopDirectoryOnly);                               //Prefabコンテンツ
    }

    /// <summary>
    /// ロード専用のメソッド
    /// </summary>
    /// <param name="folder">フォルダ</param>
    /// <param name="extension">拡張子</param>
    /// <param name="option">探す範囲</param>
    /// <param name="scale">Mayaのスケール変換するか</param>
    private void LoadContents(string folder, string extension, SearchOption option, bool scale = false)
    {
        string[] fileName = Directory.GetFiles(folder, extension, option);              //ファイル名取得
        foreach (string file in fileName)
        {
            if (file.Contains("Editor"))                                                //Editor用は無視
                continue;

            GameObject gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(file);    //ロードする
            if (scale)
                gameObject.transform.localScale = new Vector3(100, 100, 100);
            loadedObject.Add(gameObject);                                               //リストに保存
        }
    }

    /// <summary>
    /// 選択中のオブジェクトを表示
    /// </summary>
    private void CurrentObject()
    {
        if (!currentObject)                         //選択していない場合
        {
            EditorGUILayout.BeginHorizontal(GUI.skin.box, GUILayout.MinHeight(25));
            EditorGUILayout.LabelField("選択中：", GUILayout.MaxWidth(50));
            EditorGUILayout.LabelField("Null");
            EditorGUILayout.EndHorizontal();
            return;
        }

        EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.MinHeight(150));

#region 名前表示
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginHorizontal(GUILayout.MinWidth(200));
        EditorGUILayout.LabelField("選択中：", GUILayout.MaxWidth(50));
        EditorGUILayout.LabelField(currentObject.name);
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("クリア", GUILayout.MaxWidth(150)))                        //クリアボタン
        {
            currentObject = null;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            return;
        }
        EditorGUILayout.EndHorizontal();
        #endregion
#region 画像表示
        EditorGUILayout.BeginHorizontal();
        Texture2D texture = AssetPreview.GetAssetPreview(currentObject);                //画像取得
        EditorGUILayout.BeginHorizontal(GUILayout.MinWidth(260));
        GUILayout.Box(texture);                                                         //表示
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Spaceキー でブロック配置");
        EditorGUILayout.LabelField("タグ種類：");
        tag = EditorGUILayout.TagField(tag, GUILayout.MaxWidth(150));
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
#endregion

        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// Objectボタン
    /// </summary>
    private void ObjectButton()
    {
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);     //スクロールできるように

        float buttonSize = 120;                                     //ボタンサイズ
        int count = 0;                                              //ボタンの数
        int maxLength = 5;                                          //横並びの最大数
        EditorGUILayout.BeginHorizontal();
        foreach (GameObject g in loadedObject)
        {
            if (count >= maxLength)                                 //最大数に達したら改行処理
            {
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
            }
            Texture2D texture = AssetPreview.GetAssetPreview(g);    //画像取得
            EditorGUILayout.BeginVertical(GUI.skin.box);            //一セットにする
            EditorGUILayout.LabelField(g.name, GUILayout.MaxWidth(buttonSize));                                 //名前表示
            if (GUILayout.Button(texture, GUILayout.MaxWidth(buttonSize), GUILayout.MaxHeight(buttonSize)))     //画像ボタン
            {
                currentObject = g;                                  //クリックされたら記録
                tag = currentObject.tag;
            }
            EditorGUILayout.EndVertical();
            ++count;                                                //数追加
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndScrollView();                            //スクロール終了
    }

    /// <summary>
    /// 選択中のオブジェクト（Null：選択なし）
    /// </summary>
    /// <returns></returns>
    public GameObject GetCurrentObject()
    {
        return currentObject;
    }

    /// <summary>
    /// 選択中のタグ
    /// </summary>
    /// <returns></returns>
    public string CurrentTag()
    {
        return tag;
    }
}
