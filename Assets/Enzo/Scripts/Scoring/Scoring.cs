using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public int score;

    public float timer;

    public float timeToGainScore = 2f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeToGainScore)
        {
            score++;
            timer = 0f;
        }
    }
}
