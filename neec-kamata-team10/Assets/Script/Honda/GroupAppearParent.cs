using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

/// <summary>
/// 7.16 本田 
/// グループで一括制御する為のScript
/// このScriptをEmptyにくっつけてChildに本体(GroupAppearBlock)を入れる。
/// </summary>

public class GroupAppearParent : MonoBehaviour {

    [SerializeField]
    private SwitchObject switchObject;
    [SerializeField]
    private bool isReverseAppear;

    [SerializeField]
    private float fadeTime = 0.2f;  //fade時間


    public bool IsReverseAppear { get { return isReverseAppear; } }

    public bool IsAppear { get { return switchObject.IsTurnOn ^ IsReverseAppear; } }

    public float FadeTime { get { return fadeTime; } }
}
