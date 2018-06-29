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
    private Color currentLightColor;
    private ColorState colorState = ColorState.First;

    [SerializeField]
    private Color firstColor; //= new Color(6 / 255.0f, 73 / 255.0f, 176 / 255.0f,0);
    [SerializeField]
    private Color firstLightColor;
    [SerializeField]
    private Color secondColor; //= new Color(212 / 255.0f, 93 / 255.0f, 39, 0 / 255.0f);
    [SerializeField]
    private Color thirdColor; //= new Color(16 / 255.0f, 18 / 255.0f, 68, 0 / 255.0f);
    [SerializeField]
    private float time = 30;

    //private int time = 100;

    // Use this for initialization
    void Start()
    {
        //firstLightColor = transform.GetChild(1).GetComponent<Light>().color;

        Camera.main.backgroundColor = firstColor;
        transform.GetChild(1).GetComponent<Light>().color = firstLightColor;
        currentColor = Camera.main.backgroundColor;
        currentLightColor = transform.GetChild(1).GetComponent<Light>().color;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("background" + Camera.main.backgroundColor);

        if (colorState == ColorState.First)
        {
            ColorChange(secondColor, rate);
            LightColorChange(secondColor, rate);
        }
        else if (colorState == ColorState.Second)
        {
            ColorChange(thirdColor, rate);
            LightColorChange(thirdColor, rate);
        }
        else if (colorState == ColorState.Third)
        {
            ColorChange(firstColor, rate);
            LightColorChange(firstLightColor, rate);
        }

        rate += 1 / time * Time.deltaTime;

        //Debug.Log("rate" + rate);

        if (rate >= 1)
        {
            if (colorState == ColorState.First) colorState = ColorState.Second;
            else if (colorState == ColorState.Second) colorState = ColorState.Third;
            else if (colorState == ColorState.Third) colorState = ColorState.First;
            rate = 0;
            currentColor = Camera.main.backgroundColor;
            currentLightColor = transform.GetChild(1).GetComponent<Light>().color;
        }
        //Debug.Log(colorState);
    }

    void ColorChange(Color color, float rate)
    {
        Color changeColor = Color.Lerp(currentColor, color, rate);
        Camera.main.backgroundColor = changeColor;

        //if (colorState == ColorState.First) changeColor += new Color(210 / 255.0f, 210 / 255.0f, 210 / 255.0f);
        //else changeColor += new Color(150 / 255.0f, 150 / 255.0f, 150 / 255.0f);

        //changeColor += new Color(180 / 255.0f, 180 / 255.0f, 180 / 255.0f);
        //changeColor += new Color(180 / 255.0f, 180 / 255.0f, 180 / 255.0f);
        //transform.GetChild(1).GetComponent<Light>().color= changeColor;
    }

    void LightColorChange(Color color, float rate)
    {
        Color changeColor = Color.Lerp(currentLightColor, color, rate);
        transform.GetChild(1).GetComponent<Light>().color = changeColor;
    }

    public ColorState GetState()
    {
        return colorState;
    }
}
