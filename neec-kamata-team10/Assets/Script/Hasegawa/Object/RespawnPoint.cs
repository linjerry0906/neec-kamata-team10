using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour {

    [SerializeField]
    private int number;

    //本田による編集部分
    [SerializeField]
    private Sprite notCheckedSprite; //通過していない時のSprite
    [SerializeField]
    private Sprite checkedSprite;    //通過した後のSprite

    private SpriteRenderer spriteRenderer; //実際に表示させるSpriteRenderer

    private bool isChecked = false;  //この地点を通過されたか?

    //ここまで

    public int GetNumber()
    {
        return number;
    }

    //この先も本田による編集部分

    void Start() //開始時にRenderer取得
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); //Sprite側ObjectのRenderer取得
        spriteRenderer.sprite = notCheckedSprite;        //一応Set
    }

    public bool IsChecked()
    {
        return isChecked;
    }


    void OnTriggerEnter(Collider other)
    {
        if (isChecked || !other.tag.Equals("Player")) return; //既に通過された後だったり、Player以外がぶつかった時は無視する

        if (other.GetComponent<AliveFlag>().IsDead()) return; //Playerが死にながら突撃しても無駄だよ
        isChecked = true;
        spriteRenderer.sprite = checkedSprite; //Spriteを変更
        //パーティクル出すならここでActive化かな？
    }

    //ここまで
}
