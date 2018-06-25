using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollManager : MonoBehaviour
{

    [SerializeField]
    private float scrollSpeedDef;
    [SerializeField]
    private float scrollSpeedBack;

    private float xMax;
    private float xMin;

    [SerializeField]
    private float backXMax;
    [SerializeField]
    private float backXMin;

    private int maxBlockX = 18;

    // Use this for initialization
    void Start()
    {
        xMin = transform.GetChild(0).transform.position.x - maxBlockX;
        xMax = transform.GetChild(0).transform.position.x + maxBlockX;
    }

    // Update is called once per frame
    void Update()
    {
        //ステージのスクロール
        Vector3 stage1Position = transform.GetChild(0).transform.position;
        Vector3 stage2Position = transform.GetChild(1).transform.position;

        stage1Position -= new Vector3(scrollSpeedDef, 0, 0) * Time.deltaTime;
        transform.GetChild(0).transform.position = CheckDef(stage1Position);

        stage2Position -= new Vector3(scrollSpeedDef, 0, 0) * Time.deltaTime;
        transform.GetChild(1).transform.position = CheckDef(stage2Position);

        //背景のスクロール
        Vector3 ground1BackPosition = transform.GetChild(2).GetChild(1).transform.position;
        Vector3 ground2BackPosition = transform.GetChild(3).GetChild(1).transform.position;

        Vector3 ground1DefPosition = transform.GetChild(2).GetChild(2).transform.position;
        Vector3 ground2DefPosition = transform.GetChild(3).GetChild(2).transform.position;

        ground1BackPosition -= new Vector3(scrollSpeedBack, 0, 0)*Time.deltaTime;
        transform.GetChild(2).GetChild(1).transform.position = CheckBack(ground1BackPosition);
        //Debug.Log("ground1"+ground1BackPosition);
        ground2BackPosition -= new Vector3(scrollSpeedBack, 0, 0) * Time.deltaTime;
        transform.GetChild(3).GetChild(1).transform.position = CheckBack(ground2BackPosition);
        //Debug.Log("ground2" + ground2BackPosition);

        ground1DefPosition -= new Vector3(scrollSpeedDef, 0, 0) * Time.deltaTime;
        transform.GetChild(2).GetChild(2).transform.position = CheckDef(ground1DefPosition);
        ground2DefPosition -= new Vector3(scrollSpeedDef, 0, 0) * Time.deltaTime;
        transform.GetChild(3).GetChild(2).transform.position = CheckDef(ground2DefPosition);
    }

    Vector3 CheckDef(Vector3 position)
    {
        if (position.x <= xMin)
        {
            return new Vector3(xMax, position.y, position.z);
        }
        return position;
    }

    Vector3 CheckBack(Vector3 position)
    {
        if (position.x <= backXMin)
        {
            return new Vector3(backXMax, position.y, position.z);
        }
        return position;
    }

    //Vector3 Scroll()
    //{
    //    child1movement += scrollSpeed;
    //
    //    if (child1movement >= maxBlockX)
    //    {
    //        child1movement = 0;
    //        return new Vector3(maxBlockX, 0, 0);
    //    }
    //
    //    return new Vector3(-scrollSpeed,0,0);
    //}
}
