using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextScene : MonoBehaviour {

    [SerializeField]
    private EScene nextScene;

    public void Change()
    {
        GameManager.Instance.ChangeScene(nextScene);
    }

    void OnTriggerEnter(Collider t)
    {
        //Time.timeScale = 0.01f;
        Debug.Log("in");
        Change();
    }

}
