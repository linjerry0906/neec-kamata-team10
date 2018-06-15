using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectScrollScript : MonoBehaviour {

    private Vector2 minAnchor = new Vector2(0, 0);
    private ICharacterController controller;
    private int allPage;
    private int page;

	// Use this for initialization
	void Start () {
        controller = GameManager.Instance.GetController();
        allPage = 0;
        page = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if(allPage == 0)
        {
            allPage = transform.childCount;
        }
        Vector2 min = GetComponent<RectTransform>().anchorMin;
        min += (minAnchor - min) * 0.1f;
        GetComponent<RectTransform>().anchorMin = min;
        GetComponent<RectTransform>().anchorMax = min + new Vector2(1, 1);

        if (page < allPage)
        {
            if (controller.SwitchToTheRight())
            {
                minAnchor.x--;
                page++;
            }
        }

        if (page > 1)
        {
            if (controller.SwitchToTheLeft())
            {
                minAnchor.x++;
                page--;
            }
        }
    }
}
