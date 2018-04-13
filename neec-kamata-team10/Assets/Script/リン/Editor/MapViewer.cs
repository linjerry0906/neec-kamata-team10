using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapViewer : EditorWindow
{
    private static int WINDOW_WIDTH = 300;
    private static int WINDOW_HEIGHT = 200;

    private MapEditor parent;

    public static MapViewer CreateMapViewer(MapEditor parent)
    {
        MapViewer window = (MapViewer)EditorWindow.GetWindow(typeof(MapViewer), false, "Map Viewer");
        window.Show();
        window.minSize = new Vector2(WINDOW_WIDTH, WINDOW_HEIGHT);
        window.SetParent(parent);
        window.Initialize();
        return window;
    }

    private void SetParent(MapEditor parent)
    {
        this.parent = parent;
    }

    private void Initialize()
    {
    }
}
