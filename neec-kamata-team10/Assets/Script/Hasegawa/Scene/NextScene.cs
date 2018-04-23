using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScene : MonoBehaviour {

    [SerializeField]
    private EScene nextScene;

    public void Change()
    {
        GameManager.Instance.GetComponent<Transform>().GetChild(0).GetComponent<SceneChange>().ChangeScene(nextScene);
    }

    void OnTriggerEnter(Collider t)
    {
        Debug.Log("in");
        Change();
    }

}
