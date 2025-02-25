using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
   
   [SerializeField, Header("References")] private InputManager inputManager;

   private UIManager uiManager;
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
      inputManager.EnablePlayerInputs();
   }
   
   private void OnDisable()
   {
      inputManager.DisablePlayerInputs();
   }

   private void Start()
   {
      uiManager = UIManager.instance;
   }

   public void GameOver()
   {
      Debug.Log("GameOver");
      Time.timeScale = 0f;
      uiManager.GameOverUI();
   }
}
