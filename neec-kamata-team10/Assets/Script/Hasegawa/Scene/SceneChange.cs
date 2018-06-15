using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange {

    //シーンの変更
    public void ChangeScene(EScene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void ChangeSceneAsync(EScene scene)
    {
        SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive);
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeSceneAsync(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    public void CloseScene(EScene scene)
    {
        SceneManager.UnloadSceneAsync(scene.ToString());
    }

    public string CurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    //勉強用メモ
    //public void TestLoad()
    //{
    //    SceneManager.LoadScene(EScene.PlayTest.ToString());
    //    transform.GetChild(0).GetComponent<SceneChange>();
    //    GameObject SceneManager = new GameObject("SceneManager");
    //    SceneManager.AddComponent<SceneChange>();
    //    transform.parent.
    //}
}
