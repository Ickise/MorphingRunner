using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Ce script est un simple UIManager qui fait apparaitre les canvas lorsque la fonction est appelé, etc... 
    public static UIManager instance;

    [SerializeField, Header("References")] private GameObject deathCanvas;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI tRexDNAStock;
    [SerializeField] private TextMeshProUGUI humanDNAStock;

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
        Initialize();
    }

    private void Initialize()
    {
        deathCanvas.SetActive(false);
        scoreText.text = "Score: 0";
    }

    // L'apparition du canvas lors de la mort.
    public void GameOverUI()
    {
        deathCanvas.SetActive(true);
    }
    
    // Cette fonction me permet tout simplement de mettre à jour l'UI du score du joueur lors de certaines actions.
    public void UpdateScoreUI(int scoreValue)
    {
        scoreText.text = "Score: " + scoreValue;
    }

    // Cette fonction me permet tout simplement de mettre à jour l'UI du stock d'ADN lors de certaines actions.
    // Je pourrai faire un système d'event avec de l'abonnement ou bien une interface et grâce à ça je notifie tous les subscribers.
    public void UpdateDNAStockUI(int tRexStock, int humanStock)
    {
        tRexDNAStock.text = "" + tRexStock;
        humanDNAStock.text = "" + humanStock;
    }
}