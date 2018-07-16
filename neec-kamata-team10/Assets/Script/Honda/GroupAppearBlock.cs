using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 7.16 本田
/// Groupで出現設定したいAppearの本体側(Child)
/// EmptyなParent(ScriptはAddしてね)に入れて使う
/// </summary>

public class GroupAppearBlock : AppearBlock {

    private GroupAppearParent parent;
    [SerializeField]
    private float addTime; //追加でfade所要時間を設定できる

	// Use this for initialization
	void Start ()
    {
        parent = GetComponentInParent<GroupAppearParent>();
        IsReverseAppear = parent.IsReverseAppear;
        fadeTime = parent.FadeTime + addTime;

        base.OriginStart();
	}
	
	// Update is called once per frame
	void Update ()
    {
        base.newActive = parent.IsAppear; //Parent側で既にreverse含めて判定されている

        base.OriginUpdate();
	}

    protected override void SetActive(bool isActive)
    {
        //外から制御するので何もしない
        return;
    }
}
