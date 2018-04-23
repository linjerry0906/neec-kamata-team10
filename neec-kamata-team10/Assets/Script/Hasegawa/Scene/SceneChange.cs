using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

    [SerializeField]
    private string sceneName;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (IsEnd())
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    //シーン変更の条件
    bool IsEnd()
    {
        return Input.GetMouseButtonDown(0);
    }
}
