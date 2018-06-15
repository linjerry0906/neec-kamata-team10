//------------------------------------------------------
// 作成日：2018.6.04
// 作成者：林 佳叡
// 内容：鏡の色設定
//------------------------------------------------------
using UnityEngine;

public class MirrorColor : MonoBehaviour {

    [SerializeField]
    private Color maskColor;

	void Start ()
    {
        GetComponent<MeshRenderer>().material.color = maskColor;
	}
}
