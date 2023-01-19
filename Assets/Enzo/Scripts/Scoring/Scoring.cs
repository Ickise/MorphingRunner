using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    public int score;

    public float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 2f)
        {
            score++;
            timer = 0f;
        }
    }
}
