using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectScrollScript : MonoBehaviour {

    private Vector2 minAnchor = new Vector2(0, 0);

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 min = GetComponent<RectTransform>().anchorMin;
        min += (minAnchor - min) * 0.1f;
        GetComponent<RectTransform>().anchorMin = min;
        GetComponent<RectTransform>().anchorMax = min + new Vector2(1, 1);
    }

    public void OnClickRight()
    {
        minAnchor.x--;
    }

    public void OnClickLeft()
    {
        minAnchor.x++;
    }
}
