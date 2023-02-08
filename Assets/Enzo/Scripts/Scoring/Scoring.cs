using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public static Scoring scoring;

    public int score;

    public float timer;
    public float timeToGainScore = 1f;

    [SerializeField] Text scoreText;

    void Awake()
    {
        scoring = this;
    }

    void Update()
    {
        timer += Time.deltaTime;
        scoreText.text = "" + score;

        if (timer >= timeToGainScore && !TransformationsChoices.transformationsChoices.isTrex)
        {
            score++;
            timer = 0f;
        }
    }
}
