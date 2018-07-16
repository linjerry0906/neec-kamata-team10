using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupAppearBlock : AppearBlockBase
{

    [SerializeField]
    protected GameObject appearObject; //スイッチで切り替えさせるObject
    private GroupAppearParent parent;
    [SerializeField]
    private float addTime; //追加でfade所要時間を設定できる

    // Use this for initialization
    void Start()
    {
        base.appearObject = this.appearObject;
        parent = GetComponentInParent<GroupAppearParent>();
        IsReverseAppear = parent.IsReverseAppear;
        fadeTime = parent.FadeTime + addTime;

        base.OriginStart();
    }

    // Update is called once per frame
    void Update()
    {
        base.newActive = parent.IsAppear; //Parent側で既にreverse含めて判定されている

        base.OriginUpdate();
    }
}
