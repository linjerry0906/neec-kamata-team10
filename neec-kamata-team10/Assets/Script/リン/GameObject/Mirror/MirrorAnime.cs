//------------------------------------------------------
// 作成日：2018.4.27
// 作成者：林 佳叡
// 内容：鏡のアニメーション
//------------------------------------------------------
using UnityEngine;

public class MirrorAnime : MonoBehaviour
{
    [SerializeField]
    private float minInterval;      //最小間隔
    [SerializeField]
    private float maxInterval;      //最大間隔

    private float max;              //今回最大間隔
    private float current;          //現在時間
    private Animator animator;      //アニメター
	
	void Start ()
    {
        animator = GetComponent<Animator>();
        Initalize();
	}
	
	void Update ()
    {
        animator.SetBool("reflect", false);             //光らせないようにする
        current += Time.deltaTime;                      //時間加算
        if (current >= max)                             //最大値超えたら
            Initalize();                                //初期化
	}

    /// <summary>
    /// 初期化
    /// </summary>
    private void Initalize()
    {
        animator.SetBool("reflect", true);              //光らせる
        max = Random.Range(minInterval, maxInterval);   //最大値ランダムする
        current = 0;                                    //現在時間を初期化
    }
}
