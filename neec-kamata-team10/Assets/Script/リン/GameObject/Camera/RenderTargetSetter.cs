//------------------------------------------------------
// 作成日：2018.5.28
// 作成者：林 佳叡
// 内容：ステージを管理するマネージャー
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTargetSetter : MonoBehaviour
{
    private RenderTexture renderTarget;                                     //レンダーターゲット
    private Texture2D texture;

    /// <summary>
    /// レンダーターゲットを設定
    /// </summary>
    public void SetRenderTarget()
    {
        renderTarget = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
    }

    /// <summary>
    /// レンダーターゲットを取得
    /// </summary>
    /// <returns></returns>
    public Texture2D GetRenderTarget()
    {
        return texture;
    }

    public void WriteTexture()
    {
        RenderTexture currentActiveRT = RenderTexture.active;

        RenderTexture.active = renderTarget;

        texture = new Texture2D(renderTarget.width, renderTarget.height);
        texture.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);

        RenderTexture.active = currentActiveRT;
    }
}
