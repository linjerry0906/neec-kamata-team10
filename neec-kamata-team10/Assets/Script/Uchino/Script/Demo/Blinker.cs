using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Blinker : MonoBehaviour {

    [SerializeField]
    private float interval = 1.0f; //点滅周期

	// Use this for initialization
	void Start () {
        StartCoroutine(Blink());
	}
	
    /// <summary>
    /// 点滅
    /// </summary>
    /// <returns></returns>
    IEnumerator Blink()
    {
        //無限ループ
        while(true)
        {
            //trueとfalseを交互に
            GetComponent<Text>().enabled = !this.GetComponent<Text>().enabled;

            //interval間、この処理を中断する（一時的にwhileから抜ける）
            yield return new WaitForSeconds(interval);
        }
    }
}
