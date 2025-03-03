using UnityEngine;

public class ScoreManager : MonoBehaviour, IUpdate
{
    // Le ScoreManager me permet de gérer le score et d'ajouter du score lorsque certaine action se passe. 
    public static ScoreManager instance;

    [SerializeField, Header("Settings")] private float timeToGainScore = 1f;
    [SerializeField] private int scoreToAdd = 1;

    private GameManager gameManager;
    private UIManager uiManager;

    private int playerScore;

    private float time;

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

    private void OnEnable()
    {
        UpdateManager.RegisterUpdate(this);
    }

    private void OnDisable()
    {
        UpdateManager.UnregisterUpdate(this);
    }

    private void Start()
    {
        // J'initialise les variables.
        gameManager = GameManager.instance;
        uiManager = UIManager.instance;
    }

    // J'ai créé des void pour ajouter du score et connaître le score du joueur pour mettre à jour certaines données.
    public void AddScore(int addingValue)
    {
        playerScore += addingValue;
        uiManager.UpdateScoreUI(playerScore);
    }

    public int GetScore()
    {
        return playerScore;
    }

    public void UpdateTick()
    {
        if (gameManager.GameIsOver()) return;

        time += Time.deltaTime;

        if (time <= timeToGainScore) return;

        playerScore += scoreToAdd;
        uiManager.UpdateScoreUI(playerScore);
        time = 0;
    }
}