﻿//------------------------------------------------------
// 作成日：2018.6.22
// 作成者：林 佳叡
// 内容：鏡の後ろに映されない背景
//------------------------------------------------------
using UnityEngine;

public class MirrorBackStage : MonoBehaviour
{
    [SerializeField]
    private Material backStageMaterial;

	void Start ()
    {
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        Material origin = mesh.material;
        Material back = new Material(backStageMaterial);

        mesh.material = back;
        back.color = origin.color;
        back.mainTexture = origin.mainTexture;
        back.SetTexture("_EmissionMap", origin.GetTexture("_EmissionMap"));
        back.SetColor("_EmissionColor", origin.GetColor("_EmissionColor"));
	}
}
