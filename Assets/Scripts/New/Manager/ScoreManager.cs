using UnityEngine;

public class ScoreManager : MonoBehaviour, IUpdate
{
    // Le ScoreManager est responsable de la gestion du score du joueur. Auparavant, il correspondait à Scoring.
    // Il va ajouter des points à chaque fois qu'un certain intervalle de temps est écoulé ou lorsqu'une action spécifique se produit.
    
    public static ScoreManager instance;

    [SerializeField, Header("Settings")] private float timeToGainScore = 1f;
    
    [SerializeField] private int scoreToAdd = 1;

    private GameManager gameManager;
    private UIManager uiManager;

    private int playerScore;
    private float time;

    private void Awake()
    {
        PreInitialize();
    }

    private void PreInitialize()
    {
        // Comme d'habitude, nous initialisons correctement le Singleton.
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
        // Nous nous abonnons pour accèder à l'Update commune.
        UpdateManager.RegisterUpdate(this);
    }

    private void OnDisable()
    {
        // Nous nous désabonnons lorsque le script est désactivé pour éviter les éventuelles erreurs.
        UpdateManager.UnregisterUpdate(this);
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        // Nous initialisons les références aux autres managers du jeu.
        gameManager = GameManager.instance;
        uiManager = UIManager.instance;
    }

    // Cette fonction permet d'ajouter des points au score du joueur et de mettre à jour l'UI.
    public void AddScore(int addingValue)
    {
        playerScore += addingValue;
        uiManager.UpdateScoreUI(playerScore);
    }

    // Cette fonction renvoie le score actuel du joueur.
    public int GetScore()
    {
        return playerScore;
    }

    public void UpdateTick()
    {
        if (gameManager.GameIsOver()) return; // Si le jeu est terminé, nous ne faisons rien.

        time += Time.deltaTime; // Nous incrémentons le temps selon le deltaTime.

        if (time <= timeToGainScore) return; // Si le temps écoulé est inférieur à l'intervalle nécessaire, nous ne faisons rien.

        // En revanche, si le temps écoulé est supérieur à timeToGainScore alors nous ajoutons des points et nous mettons à jour l'UI.
        playerScore += scoreToAdd;
        uiManager.UpdateScoreUI(playerScore);
        time = 0; // Rénitialisation du timer.
    }
}