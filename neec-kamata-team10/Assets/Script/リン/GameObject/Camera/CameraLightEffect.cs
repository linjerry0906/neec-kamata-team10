//------------------------------------------------------
// 作成日：2018.6.07
// 作成者：林 佳叡
// 内容：ライトエフェクト
//------------------------------------------------------
using UnityEngine;

public class CameraLightEffect : MonoBehaviour
{
    [SerializeField]
    private Material highLight;             //マテリアル
    [SerializeField]
    private float brightness = 0.4f;        //全体の明るさ
    [SerializeField]
    private float lightStrength = 5.0f;     //ライトの強さ
    [SerializeField]
    private float threshold = 0.3f;

    [SerializeField]
    private Vector2 offset1x;               //オフセット
    [SerializeField]
    private Vector2 offset2x;
    [SerializeField]
    private Vector2 offset4x;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Vector4 offset1 = new Vector4(offset1x.x / Screen.width, offset1x.y / Screen.height);
        Vector4 offset2 = new Vector4(offset2x.x / Screen.width, offset2x.y / Screen.height);
        Vector4 offset4 = new Vector4(offset4x.x / Screen.width, offset4x.y / Screen.height);
        //ハイライトを書き出す
        RenderTexture light = RenderTexture.GetTemporary(Screen.width, Screen.height, 0);
        light.filterMode = FilterMode.Bilinear;
        highLight.SetFloat("_Threshold", threshold);
        Graphics.Blit(source, light, highLight, 0);
        //ブラー
        RenderTexture blur1x = RenderTexture.GetTemporary(Screen.width / 2, Screen.height / 2, 0);
        blur1x.filterMode = FilterMode.Bilinear;
        highLight.mainTexture = light;
        highLight.SetVector("_Offset", offset1);
        Graphics.Blit(light, blur1x, highLight, 1);
        //ブラー
        RenderTexture blur2x = RenderTexture.GetTemporary(Screen.width / 4, Screen.height / 4, 0);
        blur2x.filterMode = FilterMode.Bilinear;
        highLight.mainTexture = light;
        highLight.SetVector("_Offset", offset1);
        Graphics.Blit(light, blur2x, highLight, 1);
        //ブラー
        RenderTexture blur2xD = RenderTexture.GetTemporary(Screen.width / 4, Screen.height / 4, 0);
        blur2xD.filterMode = FilterMode.Bilinear;
        highLight.mainTexture = blur2x;
        highLight.SetVector("_Offset", offset2);
        Graphics.Blit(blur2x, blur2xD, highLight, 1);
        //ブラー
        RenderTexture blur4x = RenderTexture.GetTemporary(Screen.width / 8, Screen.height / 8, 0);
        blur4x.filterMode = FilterMode.Bilinear;
        highLight.mainTexture = light;
        highLight.SetVector("_Offset", offset1);
        Graphics.Blit(light, blur4x, highLight, 1);
        //ブラー
        RenderTexture blur4xD = RenderTexture.GetTemporary(Screen.width / 8, Screen.height / 8, 0);
        blur4xD.filterMode = FilterMode.Bilinear;
        highLight.mainTexture = blur4x;
        highLight.SetVector("_Offset", offset4);
        Graphics.Blit(blur4x, blur4xD, highLight, 1);
        //合成
        highLight.mainTexture = source;
        highLight.SetTexture("_HighLight", blur1x);
        highLight.SetTexture("_HighLight2x", blur2xD);
        highLight.SetTexture("_HighLight4x", blur4xD);
        highLight.SetFloat("_Brightness", brightness);
        highLight.SetFloat("_LightStrength", lightStrength);
        Graphics.Blit(source, destination, highLight, 2);

        RenderTexture.ReleaseTemporary(light);
        RenderTexture.ReleaseTemporary(blur1x);
        RenderTexture.ReleaseTemporary(blur2x);
        RenderTexture.ReleaseTemporary(blur4x);
        RenderTexture.ReleaseTemporary(blur2xD);
        RenderTexture.ReleaseTemporary(blur4xD);
    }
}
