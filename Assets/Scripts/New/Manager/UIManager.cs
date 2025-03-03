using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Ce script est un simple UIManager qui fait apparaître les canvas lorsque la fonction est appelée, etc... Il n'existait pas auparavant. 
    public static UIManager instance;

    [SerializeField, Header("References")] private GameObject deathCanvas;
    [SerializeField] private GameObject scoreCanvas;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI pachyDNAStock;
    [SerializeField] private TextMeshProUGUI humanDNAStock;

    private void Awake()
    {
        PreInitialize();
    }

    private void PreInitialize()
    {
        // Initialisation correcte du Singleton pour qu'il n'y ait qu'une seule instance de UIManager.
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
        Initialize(); // Initialisation des éléments d'UI.
    }

    private void Initialize()
    {
        deathCanvas.SetActive(false);
        scoreText.text = "Score: 0";
    }

    // Active ou désactive l'UI et la met à jour lorsque la partie est perdue.
    public void GameOverUI()
    {
        deathCanvas.SetActive(true);
        scoreCanvas.SetActive(false);
        finalScoreText.text = "Score: " + ScoreManager.instance.GetScore();
    }

    // Cette fonction permet de mettre à jour le score du joueur pendant la partie.
    public void UpdateScoreUI(int scoreValue)
    {
        scoreText.text = "Score: " + scoreValue;
    }

    // Cette fonction permet de mettre à jour les stocks d'ADN (Pachy et Humain) dans l'UI pendant la partie.
    public void UpdateDNAStockUI(int pachyStock, int humanStock)
    {
        pachyDNAStock.text = "" + pachyStock;
        humanDNAStock.text = "" + humanStock;
    }
}