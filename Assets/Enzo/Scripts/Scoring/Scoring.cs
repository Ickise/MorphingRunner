using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public uint score;

    public float timer;

    public float timeToGainScore = 1f;

    [SerializeField] Text scoreText;

    void Update()
    {
        timer += Time.deltaTime;
        scoreText.text = "" + score;

        if (timer >= timeToGainScore)
        {
            score++;
            timer = 0f;
        }
    }
}
