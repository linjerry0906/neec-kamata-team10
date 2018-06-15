//------------------------------------------------------
// 作成日：2018.6.08
// 作成者：林 佳叡
// 内容：マジックブロックのエフェクト
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBlockEffect : MonoBehaviour {

    [SerializeField]
    private float maxIntensity = 2.5f;
    [SerializeField]
    private float minIntensity = 0.0f;
    [SerializeField]
    private float flashSpeed = 1.8f;

    private float currentIntensity = 0.0f;
    private bool isIncrease = true;

    private Material material;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update ()
    {
        float amount = isIncrease ? Time.deltaTime * flashSpeed : Time.deltaTime * -flashSpeed;
        AddIntensity(amount);

        //色設定
        material.SetColor("_EmissionColor", new Color(currentIntensity, currentIntensity, currentIntensity, currentIntensity));
	}

    /// <summary>
    /// 明るさを足す
    /// </summary>
    /// <param name="amount">量</param>
    private void AddIntensity(float amount)
    {
        currentIntensity += amount;
        //Flag設定
        if (currentIntensity > maxIntensity)
            isIncrease = false;
        if (currentIntensity < minIntensity)
            isIncrease = true;
    }
}
