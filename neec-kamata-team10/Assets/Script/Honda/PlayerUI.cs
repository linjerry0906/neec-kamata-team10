using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour {

    [SerializeField]
    private GameObject[] PlayerMirrorUI;

    private int currentIndex = 0;
    private bool isAnim;
    private Animator anim;
    [SerializeField]
    private float fadeTime;
    private Timer timer;

	// Use this for initialization
	void Start () {
        timer = new Timer(fadeTime);
	}
	
	// Update is called once per frame
	void Update () {
        if (!isAnim) return;

        timer.TimerUpdate();
        SetFade();
	}

    private void SetFade()
    {
        PlayerMirrorUI[currentIndex].GetComponent<Renderer>().material.color
            = Color.Lerp(Color.white, Color.clear, timer.Rate() * timer.Rate());
    }

    public void ChangeMirrorUI(int index)
    {
        //範囲外はエラーとして無視
        if (index < 0 || PlayerMirrorUI.Length <= index) return;
        if (isAnim) SleepAnim();
        currentIndex = index;
        AwakeAnim();
    } 

    private void AwakeAnim()
    {
        PlayerMirrorUI[currentIndex].SetActive(true);
        PlayerMirrorUI[currentIndex].GetComponent<Renderer>().material.color = Color.white;
        anim = PlayerMirrorUI[currentIndex].GetComponent<Animator>();
        anim.SetBool("Selected", true);
        isAnim = true;
        timer.Initialize();
    } 

    private void SleepAnim()
    {
        anim.SetBool("Selected", false);
        PlayerMirrorUI[currentIndex].SetActive(false);
        isAnim = false;
    }
}
