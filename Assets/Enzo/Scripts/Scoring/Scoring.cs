using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public static Scoring scoring;

    public int score;

    public float timer;
    public float timeToGainScore = 1f;

    [SerializeField] Text scoreText;
    [SerializeField] Text scoreTextM;

    void Awake()
    {
        scoring = this;
    }

    void Update()
    {
        timer += Time.deltaTime;
        scoreText.text = "" + score;
        scoreTextM.text = "" + score;

        if (timer >= timeToGainScore)
        {
            score++;
            timer = 0f;
        }
    }
}
