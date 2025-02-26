using System;
using System.Collections;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // Le ScoreManager me permet de gérer le score et d'ajouter du score lorsque certaine action se passe. 
    public static ScoreManager instance;
    
    [SerializeField, Header("Settings")] private float timeToGainScore = 1f;
    [SerializeField] private int scoreToAdd = 1;
    
    private GameManager gameManager;
    private UIManager uiManager;
    
    private int playerScore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // J'initialise les variables et la coroutine.
        gameManager = GameManager.instance;
        uiManager = UIManager.instance;
        StartCoroutine(GainScore());
    }

    // J'ai créé des void pour ajouter du score et connaître le score du joueur pour mettre à jour certaines données.
    public void AddScore(int addingValue)
    {
        playerScore += addingValue;
    }

    public int GetScore()
    {
        return playerScore;
    }

    // Une simple coroutine pour gagner du score tant que la partie n'est pas perdu. 
    private IEnumerator GainScore()
    {
        while (!gameManager.GameIsOver())
        {
            yield return new WaitForSecondsRealtime(timeToGainScore);
            playerScore += scoreToAdd;
            uiManager.UpdateScoreUI(playerScore);
        }
    }
}