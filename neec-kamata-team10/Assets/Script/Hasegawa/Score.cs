using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

    [SerializeField]
    private int score;
    [SerializeField]
    private int scoreLimit;

    public void Initialize()
    {
        score = 0;
    }

    public void AddScore(int addScore)
    {
        score += addScore;
        if (score > scoreLimit) score = scoreLimit;
    }

    public void RemoveScore(int removeScore)
    {
        score -= removeScore;
        if (score < 0) score = 0;
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int score)
    {
        this.score = score;
    }
}
