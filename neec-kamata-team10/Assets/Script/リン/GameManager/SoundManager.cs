﻿//------------------------------------------------------
// 作成日：2018.6.08
// 作成者：林 佳叡
// 内容：音声を管理するクラス
//------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [SerializeField]
    private List<AudioClip> sesSource;              //効果音源

    private Dictionary<string, AudioClip> bgms;     //BGM

    private string currentBgm = "";

    private void Start()
    {
    }

    public void PlayBGM(string name)
    {
        if (currentBgm.Equals(name))
            return;



        currentBgm = name;
    }
}
