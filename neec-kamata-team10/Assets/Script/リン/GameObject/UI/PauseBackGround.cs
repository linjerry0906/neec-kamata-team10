//------------------------------------------------------
// 作成日：2018.4.13
// 作成者：林 佳叡
// 内容：Pauseの背景
//------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

public class PauseBackGround : MonoBehaviour {

    private SpriteRenderer spriteRenderer;      //アニメションが設定されるレンダラー
    private Image imageRenderer;                //UIのレンダラー
    private Animator animator;                  //アニメター
    private MirrorAnime mirrorAnime;            //鏡のリフレクションを制御するスクリプト

	void Start ()
    {
        animator = GetComponent<Animator>();
        mirrorAnime = GetComponent<MirrorAnime>();
        imageRenderer = GetComponent<Image>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;         //レンダーされないようにOFF
	}
	
	void Update ()
    {
        mirrorAnime.PauseUpdate();                  //Update
        animator.Update(Time.unscaledDeltaTime);    //Update
        Sprite sprite = spriteRenderer.sprite;      //Spriteを取得
        imageRenderer.sprite = sprite;              //UIレンダラーに設定
	}
}
