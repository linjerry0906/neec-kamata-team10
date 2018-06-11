//------------------------------------------------------
// 作成日：2018.4.13
// 作成者：林 佳叡
// 内容：Pauseの背景
//------------------------------------------------------
using UnityEngine;
using UnityEngine.UI;

public class PauseBackGround : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private Image imageRenderer;
    private Animator animator;
    private MirrorAnime mirrorAnime;

	void Start ()
    {
        animator = GetComponent<Animator>();
        mirrorAnime = GetComponent<MirrorAnime>();
        imageRenderer = GetComponent<Image>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
	}
	
	void Update ()
    {
        mirrorAnime.PauseUpdate();
        animator.Update(Time.unscaledDeltaTime);
        Sprite sprite = spriteRenderer.sprite;
        imageRenderer.sprite = sprite;
	}
}
