using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorState
{
    First,
    Second,
    Third
}

public class BackGround : MonoBehaviour
{
    //private float red;
    //private float green;
    //private float blue;
    private float rate = 0;
    private Color currentColor;
    private ColorState colorState = ColorState.First;

    [SerializeField]
    private Color firstColor = new Color(6 / 255.0f, 73 / 255.0f, 176 / 255.0f,0);
    [SerializeField]
    private Color secondColor = new Color(212 / 255.0f, 93 / 255.0f, 39, 0 / 255.0f);
    [SerializeField]
    private Color thirdColor = new Color(16 / 255.0f, 18 / 255.0f, 68, 0 / 255.0f);
    [SerializeField]
    private float time = 30;

    //private int time = 100;

    // Use this for initialization
    void Start()
    {
        Camera.main.backgroundColor = firstColor;
        //Debug.Log("background" + Camera.main.backgroundColor);
        currentColor = Camera.main.backgroundColor;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("background" + Camera.main.backgroundColor);

        if (colorState == ColorState.First) ColorChange(secondColor, rate);
        else if (colorState == ColorState.Second) ColorChange(thirdColor, rate);
        else if (colorState == ColorState.Third) ColorChange(firstColor, rate);

        rate += 1 / time * Time.deltaTime;

        //Debug.Log("rate" + rate);

        if (rate >= 1)
        {
            if (colorState == ColorState.First) colorState = ColorState.Second;
            else if (colorState == ColorState.Second) colorState = ColorState.Third;
            else if (colorState == ColorState.Third) colorState = ColorState.First;
            rate = 0;
            currentColor = Camera.main.backgroundColor;
        }
        //Debug.Log(colorState);
    }

    void ColorChange(Color color, float rate)
    {
        Camera.main.backgroundColor = Color.Lerp(currentColor, color, rate);
    }

    public ColorState GetState()
    {
        return colorState;
    }
}
