using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderGameView : MonoBehaviour
{
    private RenderTargetSetter setter;
    private Texture2D texture;

    private void Start()
    {
        setter = GameManager.Instance.GetComponent<RenderTargetSetter>();
        texture = setter.GetRenderTarget();
    }

    private void OnPreRender()
    {
        Graphics.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
    }

    
}
