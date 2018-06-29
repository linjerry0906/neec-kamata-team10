using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMoon : MonoBehaviour {

    [SerializeField]
    private float fadeTime;

    private ColorState state;
    private SpriteRenderer sun;
    private SpriteRenderer moon;

    private float sunAlpha;
    private float moonAlpha;

    // Use this for initialization
    void Start () {
        sun = transform.GetChild(1).GetChild(3).GetComponent<SpriteRenderer>();
        moon = transform.GetChild(1).GetChild(4).GetComponent<SpriteRenderer>();

        sunAlpha = sun.color.a;
        moonAlpha = moon.color.a;
    }
	
    void Update()
    {
        //Debug.Log("sunmoon"+state);
        if (state == ColorState.Third)
        {
            //Debug.Log("フェード");
            if (sunAlpha >= 0)
            {
                sunAlpha -= 1 * Time.deltaTime;
                sun.color = new Color(sun.color.r, sun.color.g, sun.color.b, sunAlpha);
            }
            if (moonAlpha <= 1) 
            {
                moonAlpha += 1 * Time.deltaTime;
                moon.color = new Color(moon.color.r, moon.color.g, moon.color.b, moonAlpha);
            }
        }
        else
        {
            if (sunAlpha <= 1)
            {
                sunAlpha += 1 * Time.deltaTime;
                sun.color = new Color(sun.color.r, sun.color.g, sun.color.b, sunAlpha);
            }
            if (moonAlpha >= 0)
            {
                moonAlpha -= 1 * Time.deltaTime;
                moon.color = new Color(moon.color.r, moon.color.g, moon.color.b, moonAlpha);
            }
        }
    }

	public void SetState(ColorState state)
    {
        this.state = state;
    }

    //public void Change(bool isSun)
    //{
    //    if (isSun)
    //    {
    //        transform.GetChild(1).GetChild(3).gameObject.SetActive(true);
    //        transform.GetChild(1).GetChild(4).gameObject.SetActive(false);
    //    }
    //    else
    //    {
    //        transform.GetChild(1).GetChild(3).gameObject.SetActive(false);
    //        transform.GetChild(1).GetChild(4).gameObject.SetActive(true);
    //    }
    //}
}
