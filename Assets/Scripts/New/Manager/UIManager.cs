using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
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
