using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Ce script est un simple UIManager qui fait apparaitre les canvas lorsque la fonction est appelé, etc... 
    public static UIManager instance;

    [SerializeField, Header("References")] private GameObject deathCanvas;
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
    }

    public void GameOverUI()
    {
        deathCanvas.SetActive(true);
    }
}
