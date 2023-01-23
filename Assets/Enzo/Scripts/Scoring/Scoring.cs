using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public int score;

    public float timer;

    public float timeToGainScore = 2f;

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
