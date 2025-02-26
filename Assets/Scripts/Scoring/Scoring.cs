using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    // Ce script permettait d'augmenter le score du joueur mais également de mettre à jour l'UI. 
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

    // Encore une fois un manque d'optimisation puisque nous utilisions une Update pour augmenter le score et mettre à jour l'UI en permanence. 
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
