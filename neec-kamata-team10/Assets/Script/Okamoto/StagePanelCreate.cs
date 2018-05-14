using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagePanelCreate : MonoBehaviour {

    [SerializeField]
    private GameObject originPanel;
    [SerializeField]
    private GameObject buttonPanel;
    [SerializeField]
    private GameObject rightButton;
    [SerializeField]
    private GameObject leftButton;
    [SerializeField]
    private GameObject originButton;
    [SerializeField]
    private int stageCount;
    private int currentStage;

    [SerializeField]
    private int panelPerButton;

    List<GameObject> panels;

	// Use this for initialization
	void Start () {
        currentStage = 1;
        panels = new List<GameObject>();
        

        while(currentStage <= stageCount)
        {
            if(currentStage%panelPerButton == 1)
            {
                //Panel1個作成
                PanelCreate();
                //RightButton追加
                RightButtonCreate();
            }
            //Button作成
            int index = (currentStage - 1) / panelPerButton;
            Debug.Log(index);
            GameObject b = Instantiate(originButton, panels[index].transform.GetChild(0));
            //作成されたボタンにステージを割り当てる
            b.GetComponent<StageSelectScript>().StageSet(currentStage);
            currentStage++;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void PanelCreate()
    {
        GameObject p = Instantiate(originPanel, transform);
        panels.Add(p);
        if (panels.Count == 1) return;
        GameObject lb = Instantiate(leftButton, panels[(currentStage - 1) / panelPerButton].transform);
        lb.GetComponent<Button>().onClick.AddListener(GetComponent<StageSelectScrollScript>().OnClickLeft);
    }

    void RightButtonCreate()
    {
        if (currentStage / panelPerButton <= 0) return;
        GameObject rb = Instantiate(rightButton, panels[(currentStage / panelPerButton) - 1].transform);
        rb.GetComponent<Button>().onClick.AddListener(GetComponent<StageSelectScrollScript>().OnClickRight);
        
    }
}
