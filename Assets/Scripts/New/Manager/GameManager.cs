using UnityEngine;

public class GameManager : MonoBehaviour
{
   // Ce script est un simple GameManager pour lancer un GameOver, enable les inputs etc... pour manager le jeu. 
   public static GameManager instance;
   
   [SerializeField, Header("References")] private InputManager inputManager;

   private bool gameIsOver;

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
      gameIsOver = true;
      Time.timeScale = 0f;
      uiManager.GameOverUI();
   }

   public bool GameIsOver()
   {
      return gameIsOver;
   }
}
