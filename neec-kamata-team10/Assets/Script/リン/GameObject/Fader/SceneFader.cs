//------------------------------------------------------
// 作成日：2018.4.23
// 作成者：林 佳叡
// 内容：シーンフェーダー
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    [SerializeField]
    private Material shaderMaterial;
    [SerializeField]
    private float fadeFactor = 1.0f;

    private void UpdateShader()
    {
        fadeFactor -= 0.01f;

        if (fadeFactor <= -0.1f)
            fadeFactor = -0.1f;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        UpdateShader();
        shaderMaterial.SetFloat("_Factor", fadeFactor);
        Graphics.Blit(source, destination, shaderMaterial);
    }

}
