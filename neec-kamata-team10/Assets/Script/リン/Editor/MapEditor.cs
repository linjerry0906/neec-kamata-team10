//------------------------------------------------------
// 作成日：2018.4.4
// 作成者：林 佳叡
// 内容：マップ編集用拡張機能
//------------------------------------------------------
using UnityEngine;
using UnityEditor;

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
        Object,             //オブジェクト配置など
    }

    private int currentMode = (int)Mode.EditMode;       //編集モード

    private LayoutManager layoutManager = new LayoutManager();
    private StageIO stageIO = new StageIO();
    private ObjectEditor objectEditor = new ObjectEditor();

    private GameObject OnMouse = null;

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

        UpdateScene();                                  //シーンと連動する処理
    }

    /// <summary>
    /// シーンの処理
    /// </summary>
    private void UpdateScene()
    {
        if (objectEditor.GetCurrentObject() == null)
        {
            OnMouse = null;
            return;
        }
        if (OnMouse == null ||
            OnMouse.name != objectEditor.GetCurrentObject().name)
        {
            OnMouse = PrefabUtility.InstantiatePrefab(objectEditor.GetCurrentObject()) as GameObject;
            OnMouse.name = objectEditor.GetCurrentObject().name;
        }

        Camera camera = SceneView.lastActiveSceneView.camera;

        Vector2 mousePos = Event.current.mousePosition;

        Vector3 world = camera.ScreenToWorldPoint(HandleUtility.GUIPointToScreenPixelCoordinate(mousePos));

        world.z = -0.5f;
        OnMouse.transform.position = world;
    }

    
}
