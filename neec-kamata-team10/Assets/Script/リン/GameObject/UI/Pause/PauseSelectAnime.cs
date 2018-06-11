//------------------------------------------------------
// 作成日：2018.6.11
// 作成者：林 佳叡
// 内容：PauseSelectのアニメーション
//------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

public class PauseSelectAnime : MonoBehaviour {

    private SpriteRenderer spriteRenderer;      //アニメションが設定されるレンダラー
    private Image imageRenderer;                //UIのレンダラー
    private Animator animator;                  //アニメター

    private bool isVisible = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = 0.3f;
        imageRenderer = GetComponent<Image>();
        imageRenderer.enabled = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;         //レンダーされないようにOFF
    }

    void Update()
    {
        if (!isVisible)
            return;

        animator.Update(Time.unscaledDeltaTime);    //Update
        Sprite sprite = spriteRenderer.sprite;      //Spriteを取得
        imageRenderer.sprite = sprite;              //UIレンダラーに設定
    }

    /// <summary>
    /// 見えるかどうかを設定
    /// </summary>
    /// <param name="isVisible">見えるか</param>
    public void SetVisible(bool isVisible)
    {
        this.isVisible = isVisible;
        imageRenderer.enabled = isVisible;
    }
}
