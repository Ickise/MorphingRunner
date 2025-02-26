using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Ce script est un simple UIManager qui fait apparaitre les canvas lorsque la fonction est appelé, etc... 
    public static UIManager instance;

    [SerializeField, Header("References")] private GameObject deathCanvas;

    [SerializeField] private TextMeshProUGUI scoreText;
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
        deathCanvas.SetActive(false);
        scoreText.text = "Score: 0";
    }

    public void GameOverUI()
    {
        deathCanvas.SetActive(true);
    }

    public void UpdateScoreUI(int scoreValue)
    {
        scoreText.text = "Score: " + scoreValue;
    }
}
