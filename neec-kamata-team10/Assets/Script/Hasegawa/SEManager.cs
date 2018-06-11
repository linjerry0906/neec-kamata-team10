using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour {
    [SerializeField]
    List<AudioClip> soundList;

    public AudioClip GetSE(int number)
    {
        if (soundList.Count < number) return soundList[0];
        return soundList[number];
    }
}
